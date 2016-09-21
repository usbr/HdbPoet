using System;
using System.Collections.Generic;
using System.Drawing;
using System.Data;
using System.Windows.Forms;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using System.Xml;
using System.Text;
using System.Linq;
using Reclamation.Core;

namespace HdbPoet
{
    /// <summary>
    /// Summary description for SqlView.
    /// </summary>
    public class SqlBuilder : System.Windows.Forms.UserControl
    {
        OracleServer oracle = null;
        private ListBox listBoxSqlFxns;
        private Label labelSqlFxns;
        private Label labelSqlDesc;

        private static string filePath = "sqlFunctions.xml";
        private static DataTable functionTable;
        private int sqlID;
        private string sqlName, sqlStmt, sqlDesc;
        private Button buttonDelSql;
        private GroupBox groupBoxDesc;
        private SplitContainer splitContainer1;
        private RichTextBox richTextBoxSql;
        private Button buttonExcelExport;
        private Button buttonSaveSql;
        private Button buttonSql;
        private DataGrid dataGrid1;

        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.Container components = null;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="oracle"></param>
        public SqlBuilder(OracleServer oracle)
        {
            this.oracle = oracle;
            InitializeComponent();
            populateFunctionList();
            this.Text = "SQL Builder";
        }

        /// <summary>
        /// Populate SQL function list using XML file
        /// </summary>
        private void populateFunctionList()
        {
            // Get XML file with list of SQL functions
            functionTable = new DataTable();
            functionTable.ReadXml(filePath);

            // Build custom C# List with SQL functions
            List<SqlFunctions> sqlList = new List<SqlFunctions>();
            foreach (DataRow row in functionTable.Rows)
            {
                sqlList.Add(new SqlFunctions()
                {
                    id = Convert.ToInt32(row["id"].ToString()),
                    name = row["name"].ToString(),
                    sql = row["sql"].ToString(),
                    desc = row["desc"].ToString()
                });
            }

            // Populate UI listbox with SQL functions
            listBoxSqlFxns.DisplayMember = "name";
            listBoxSqlFxns.DataSource = sqlList;
            listBoxSqlFxns.Refresh();
        }

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (components != null)
                {
                    components.Dispose();
                }
            }
            base.Dispose(disposing);
        }
        
        /// <summary>
        /// Populate the SQL UI textbox and description
        /// </summary>
        public void listBoxSqlFxns_SelectedIndexChanged(object sender, EventArgs args)
        {
            sqlID = (listBoxSqlFxns.SelectedItem as SqlFunctions).id;
            sqlName = (listBoxSqlFxns.SelectedItem as SqlFunctions).name;
            sqlDesc = (listBoxSqlFxns.SelectedItem as SqlFunctions).desc;
            sqlStmt = (listBoxSqlFxns.SelectedItem as SqlFunctions).sql;

            this.labelSqlDesc.Text = sqlDesc;
            this.richTextBoxSql.Text = sqlStmt;
            ChangeTextcolor("$", Color.Plum, this.richTextBoxSql, 0);
            this.richTextBoxSql.SelectionStart = 0;
            this.richTextBoxSql.ScrollToCaret();
        }

        /// <summary>
        /// Add SQL function to list and XML file
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonSaveSql_Click(object sender, EventArgs e)
        {
            sqlStmt = this.richTextBoxSql.Text;
            sqlName = "";
            sqlDesc = "";

            if (sqlStmt.Length == 0 ||
                    sqlStmt.ToLower().IndexOf("update") >= 0 || sqlStmt.ToLower().IndexOf("insert") >= 0 ||
                    sqlStmt.ToLower().IndexOf("drop") >= 0 || sqlStmt.ToLower().IndexOf("create") >= 0 ||
                    sqlStmt.ToLower().IndexOf("alter") >= 0 || sqlStmt.ToLower().IndexOf("truncate") >= 0 ||
                    sqlStmt.ToLower().IndexOf("rename") >= 0 || sqlStmt.ToLower().IndexOf("delete") >= 0 ||
                    sqlStmt.ToLower().IndexOf("merge") >= 0 || sqlStmt.ToLower().IndexOf("grant") >= 0 ||
                    sqlStmt.ToLower().IndexOf("revoke") >= 0 || sqlStmt.ToLower().IndexOf("analyze") >= 0 ||
                    sqlStmt.ToLower().IndexOf("audit") >= 0 || sqlStmt.ToLower().IndexOf("comment") >= 0 ||
                    sqlStmt.ToLower().IndexOf("commit") >= 0 || sqlStmt.ToLower().IndexOf("rollback") >= 0 ||
                    sqlStmt.ToLower().IndexOf("savepoint") >= 0 || sqlStmt.ToLower().IndexOf("set") >= 0)
            {
                MessageBox.Show("Check your SQL statement. It is either blank or uses non-SELECT statements.");
            }
            else
            {
                if (InputBox("SQL Function Name", "Type in a name for your SQL function:", ref sqlName) == DialogResult.OK)
                {
                    if (InputBox("SQL Function Description", "Type in a description for your SQL function:", ref sqlDesc) == DialogResult.OK)
                    {
                        XDocument doc = XDocument.Load(filePath);
                        XElement functions = doc.Element("SqlFunctions");
                        int maxId = Convert.ToInt32(functionTable.Compute("max(id)", string.Empty));
                        functions.Add(new XElement("function",
                                   new XElement("id", maxId + 1),
                                   new XElement("name", sqlName),
                                   new XElement("sql", sqlStmt),
                                   new XElement("desc", sqlDesc)
                                   ));
                        doc.Save(filePath);
                        populateFunctionList();
                        listBoxSqlFxns.SelectedIndex = listBoxSqlFxns.Items.Count - 1;
                    }
                }
            }
        }

        /// <summary>
        /// Delete SQL function from list and XML file
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonDelSql_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to delete this SQL function?", "Confirm Deletion", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                XDocument doc = XDocument.Load(filePath);
                string id = sqlID.ToString();
                var function = (from xml2 in doc.Descendants("function")
                                where xml2.Element("id").Value == id
                                select xml2).FirstOrDefault();
                function.Remove();
                doc.Save(filePath);
                populateFunctionList();
                listBoxSqlFxns.SelectedIndex = 0;
            }
        }

        /// <summary>
        /// Execute SQL Statement
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonSql_Click(object sender, System.EventArgs e)
        {
            if (this.richTextBoxSql.Text.Length > 0)
            {
                Cursor = Cursors.WaitCursor;
                try
                {
                    //oracle.SetDateFormat();
                    DataTable tbl;
                    string sql = this.richTextBoxSql.Text;
                    if (sql.ToLower().IndexOf("update") >= 0 || sql.ToLower().IndexOf("insert") >= 0 ||
                        sql.ToLower().IndexOf("drop") >= 0 || sql.ToLower().IndexOf("create") >= 0 ||
                        sql.ToLower().IndexOf("alter") >= 0 || sql.ToLower().IndexOf("truncate") >= 0 ||
                        sql.ToLower().IndexOf("rename") >= 0 || sql.ToLower().IndexOf("delete") >= 0 ||
                        sql.ToLower().IndexOf("merge") >= 0 || sql.ToLower().IndexOf("grant") >= 0 ||
                        sql.ToLower().IndexOf("revoke") >= 0 || sql.ToLower().IndexOf("analyze") >= 0 ||
                        sql.ToLower().IndexOf("audit") >= 0 || sql.ToLower().IndexOf("comment") >= 0 ||
                        sql.ToLower().IndexOf("commit") >= 0 || sql.ToLower().IndexOf("rollback") >= 0 ||
                        sql.ToLower().IndexOf("savepoint") >= 0 || sql.ToLower().IndexOf("set") >= 0)
                    {
                        tbl = new DataTable("queryerror");
                        tbl.Columns.Add("ERROR");
                        tbl.Rows.Add("You");
                        tbl.Rows.Add("may");
                        tbl.Rows.Add("only");
                        tbl.Rows.Add("execute");
                        tbl.Rows.Add("SELECT");
                        tbl.Rows.Add("statements");
                        tbl.Rows.Add("using");
                        tbl.Rows.Add("this");
                        tbl.Rows.Add("interface");
                    }
                    else if (sql.ToLower().IndexOf("$") >=0)
                    {
                        MessageBox.Show("Missing input found. Replace all $-variables with your desired inputs.", "Missing Input", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        tbl = new DataTable("queryerror");
                    }
                    else
                    {
                        tbl = oracle.Table("queryreturn", this.richTextBoxSql.Text);
                    }
                    this.dataGrid1.DataSource = tbl;
                }
                catch (Exception ex)
                {
                    string msg = "Error: " + ex.Message;
                    MessageBox.Show(msg, "HDB Poet", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }
                finally
                {
                    Cursor = Cursors.Default;
                }

            }
        }

        /// <summary>
        /// Generate and open a CSV file with queried data
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonExcelExport_Click(object sender, EventArgs e)
        {
            DataTable dTab  = (DataTable)dataGrid1.DataSource;
            StringBuilder sb = new StringBuilder();
            try
            {
                string[] columnNames = dTab.Columns.Cast<DataColumn>().Select(column => column.ColumnName.Replace(",", " ")).ToArray();
                sb.AppendLine(string.Join(",", columnNames));

                foreach (DataRow row in dTab.Rows)
                {
                    string[] fields = row.ItemArray.Select(field => field.ToString().Replace(",", " ")).ToArray();
                    sb.AppendLine(string.Join(",", fields));
                }
                string filename = Path.ChangeExtension(Path.GetTempFileName(), ".csv");
                File.WriteAllText(filename, sb.ToString());
                System.Diagnostics.Process.Start(filename);
            }
            catch
            { }
        }

        /// <summary>
        /// Build custom input box for SQL function name and description
        /// </summary>
        /// <param name="title"></param>
        /// <param name="promptText"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static DialogResult InputBox(string title, string promptText, ref string value)
        {
            Form form = new Form();
            Label label = new Label();
            TextBox textBox = new TextBox();
            Button buttonOk = new Button();
            Button buttonCancel = new Button();

            form.Text = title;
            label.Text = promptText;
            textBox.Text = value;

            buttonOk.Text = "OK";
            buttonCancel.Text = "Cancel";
            buttonOk.DialogResult = DialogResult.OK;
            buttonCancel.DialogResult = DialogResult.Cancel;

            label.SetBounds(5, 15, 400, 20);
            textBox.SetBounds(10, 35, 380, 75);
            textBox.Multiline = true;
            buttonOk.SetBounds(235, 120, 75, 23);
            buttonCancel.SetBounds(315, 120, 75, 23);

            label.AutoSize = true;
            textBox.Anchor = textBox.Anchor | AnchorStyles.Right;
            buttonOk.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            buttonCancel.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;

            form.ClientSize = new Size(400, 150);
            form.Controls.AddRange(new Control[] { label, textBox, buttonOk, buttonCancel });
            form.ClientSize = new Size(Math.Max(300, label.Right + 10), form.ClientSize.Height);
            form.FormBorderStyle = FormBorderStyle.FixedDialog;
            form.StartPosition = FormStartPosition.CenterScreen;
            form.MinimizeBox = false;
            form.MaximizeBox = false;
            form.AcceptButton = buttonOk;
            form.CancelButton = buttonCancel;

            DialogResult dialogResult = form.ShowDialog();
            value = textBox.Text;
            return dialogResult;
        }

        /// <summary>
        /// This method highlights the assigned text with the specified color.
        /// </summary>
        /// <param name="textToMark">The text to be marked.</param>
        /// <param name="color">The new Backgroundcolor.</param>
        /// <param name="richTextBox">The RichTextBox.</param>
        /// <param name="startIndex">The zero-based starting caracter position.</param>
        public static void ChangeTextcolor(string textToMark, Color color, RichTextBox richTextBox, int startIndex)
        {
            if (startIndex < 0 || startIndex > textToMark.Length - 1) startIndex = 0;

            System.Drawing.Font newFont = new Font("Consolas", 10f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 178, false);
            try
            {
                foreach (string line in richTextBox.Lines)
                {
                    if (line.Contains(textToMark))
                    {
                        richTextBox.Select(startIndex, line.Length);
                        richTextBox.SelectionBackColor = color;
                    }
                    startIndex += line.Length + 1;
                }
            }
            catch
            { }
        }

        #region Component Designer generated code
        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SqlBuilder));
            this.listBoxSqlFxns = new System.Windows.Forms.ListBox();
            this.labelSqlFxns = new System.Windows.Forms.Label();
            this.labelSqlDesc = new System.Windows.Forms.Label();
            this.buttonDelSql = new System.Windows.Forms.Button();
            this.groupBoxDesc = new System.Windows.Forms.GroupBox();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.dataGrid1 = new System.Windows.Forms.DataGrid();
            this.buttonSql = new System.Windows.Forms.Button();
            this.buttonSaveSql = new System.Windows.Forms.Button();
            this.buttonExcelExport = new System.Windows.Forms.Button();
            this.richTextBoxSql = new System.Windows.Forms.RichTextBox();
            this.groupBoxDesc.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGrid1)).BeginInit();
            this.SuspendLayout();
            // 
            // listBoxSqlFxns
            // 
            this.listBoxSqlFxns.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.listBoxSqlFxns.FormattingEnabled = true;
            this.listBoxSqlFxns.Location = new System.Drawing.Point(720, 25);
            this.listBoxSqlFxns.Name = "listBoxSqlFxns";
            this.listBoxSqlFxns.Size = new System.Drawing.Size(244, 394);
            this.listBoxSqlFxns.TabIndex = 3;
            this.listBoxSqlFxns.SelectedIndexChanged += new System.EventHandler(this.listBoxSqlFxns_SelectedIndexChanged);
            // 
            // labelSqlFxns
            // 
            this.labelSqlFxns.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.labelSqlFxns.AutoSize = true;
            this.labelSqlFxns.Location = new System.Drawing.Point(720, 6);
            this.labelSqlFxns.Name = "labelSqlFxns";
            this.labelSqlFxns.Size = new System.Drawing.Size(77, 13);
            this.labelSqlFxns.TabIndex = 4;
            this.labelSqlFxns.Text = "SQL Functions";
            // 
            // labelSqlDesc
            // 
            this.labelSqlDesc.AutoSize = true;
            this.labelSqlDesc.Location = new System.Drawing.Point(6, 16);
            this.labelSqlDesc.MaximumSize = new System.Drawing.Size(230, 150);
            this.labelSqlDesc.MinimumSize = new System.Drawing.Size(230, 150);
            this.labelSqlDesc.Name = "labelSqlDesc";
            this.labelSqlDesc.Size = new System.Drawing.Size(230, 150);
            this.labelSqlDesc.TabIndex = 5;
            this.labelSqlDesc.Text = "N/A";
            // 
            // buttonDelSql
            // 
            this.buttonDelSql.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonDelSql.Location = new System.Drawing.Point(835, 1);
            this.buttonDelSql.Name = "buttonDelSql";
            this.buttonDelSql.Size = new System.Drawing.Size(129, 23);
            this.buttonDelSql.TabIndex = 7;
            this.buttonDelSql.Text = "Delete SQL Function";
            this.buttonDelSql.UseVisualStyleBackColor = true;
            this.buttonDelSql.Click += new System.EventHandler(this.buttonDelSql_Click);
            // 
            // groupBoxDesc
            // 
            this.groupBoxDesc.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBoxDesc.Controls.Add(this.labelSqlDesc);
            this.groupBoxDesc.Location = new System.Drawing.Point(720, 425);
            this.groupBoxDesc.Name = "groupBoxDesc";
            this.groupBoxDesc.Size = new System.Drawing.Size(243, 174);
            this.groupBoxDesc.TabIndex = 8;
            this.groupBoxDesc.TabStop = false;
            this.groupBoxDesc.Text = "Selected Function Description";
            // 
            // splitContainer1
            // 
            this.splitContainer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.splitContainer1.Location = new System.Drawing.Point(3, 6);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.richTextBoxSql);
            this.splitContainer1.Panel1.Controls.Add(this.buttonExcelExport);
            this.splitContainer1.Panel1.Controls.Add(this.buttonSaveSql);
            this.splitContainer1.Panel1.Controls.Add(this.buttonSql);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.dataGrid1);
            this.splitContainer1.Size = new System.Drawing.Size(711, 591);
            this.splitContainer1.SplitterDistance = 189;
            this.splitContainer1.TabIndex = 2;
            // 
            // dataGrid1
            // 
            this.dataGrid1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGrid1.DataMember = "";
            this.dataGrid1.HeaderForeColor = System.Drawing.SystemColors.ControlText;
            this.dataGrid1.Location = new System.Drawing.Point(3, 3);
            this.dataGrid1.Name = "dataGrid1";
            this.dataGrid1.ReadOnly = true;
            this.dataGrid1.Size = new System.Drawing.Size(703, 390);
            this.dataGrid1.TabIndex = 1;
            // 
            // buttonSql
            // 
            this.buttonSql.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonSql.Image = global::HdbPoet.Properties.Resources.warning;
            this.buttonSql.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonSql.Location = new System.Drawing.Point(463, 159);
            this.buttonSql.Name = "buttonSql";
            this.buttonSql.Size = new System.Drawing.Size(120, 23);
            this.buttonSql.TabIndex = 6;
            this.buttonSql.Text = "Execute SQL";
            this.buttonSql.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.buttonSql.Click += new System.EventHandler(this.buttonSql_Click);
            // 
            // buttonSaveSql
            // 
            this.buttonSaveSql.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonSaveSql.Location = new System.Drawing.Point(3, 159);
            this.buttonSaveSql.Name = "buttonSaveSql";
            this.buttonSaveSql.Size = new System.Drawing.Size(172, 23);
            this.buttonSaveSql.TabIndex = 6;
            this.buttonSaveSql.Text = "Save Custom SQL Function";
            this.buttonSaveSql.UseVisualStyleBackColor = true;
            this.buttonSaveSql.Click += new System.EventHandler(this.buttonSaveSql_Click);
            // 
            // buttonExcelExport
            // 
            this.buttonExcelExport.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonExcelExport.Image = ((System.Drawing.Image)(resources.GetObject("buttonExcelExport.Image")));
            this.buttonExcelExport.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonExcelExport.Location = new System.Drawing.Point(589, 159);
            this.buttonExcelExport.Name = "buttonExcelExport";
            this.buttonExcelExport.Size = new System.Drawing.Size(117, 23);
            this.buttonExcelExport.TabIndex = 2;
            this.buttonExcelExport.Text = "CSV Export";
            this.buttonExcelExport.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.buttonExcelExport.UseVisualStyleBackColor = true;
            this.buttonExcelExport.Click += new System.EventHandler(this.buttonExcelExport_Click);
            // 
            // richTextBoxSql
            // 
            this.richTextBoxSql.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.richTextBoxSql.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.richTextBoxSql.Location = new System.Drawing.Point(3, 3);
            this.richTextBoxSql.Name = "richTextBoxSql";
            this.richTextBoxSql.Size = new System.Drawing.Size(703, 150);
            this.richTextBoxSql.TabIndex = 11;
            this.richTextBoxSql.Text = "";
            // 
            // SqlBuilder
            // 
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.groupBoxDesc);
            this.Controls.Add(this.buttonDelSql);
            this.Controls.Add(this.labelSqlFxns);
            this.Controls.Add(this.listBoxSqlFxns);
            this.Name = "SqlBuilder";
            this.Size = new System.Drawing.Size(967, 600);
            this.groupBoxDesc.ResumeLayout(false);
            this.groupBoxDesc.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGrid1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        #endregion






    }


    /// <summary>
    /// Custom class for SQL Functions
    /// </summary>
    public class SqlFunctions
    {
        public int id { get; set; }
        public string name { get; set; }
        public string sql { get; set; }
        public string desc { get; set; }
    }
}
