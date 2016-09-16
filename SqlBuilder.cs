using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;
using System.IO;
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
        string[] sqlCommands;
        private System.Windows.Forms.DataGrid dataGrid1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button buttonSql;
        private System.Windows.Forms.TextBox textBoxSql;
        private Button buttonExcelExport;
        private ListBox listBoxSqlFxns;
        private Label labelSqlFxns;
        private Label labelSqlDesc;

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
            this.Text = "SQL Builder";
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
        /// Execute SQL Statement
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonSql_Click(object sender, System.EventArgs e)
        {
            if (this.textBoxSql.Text.Length > 0)
            {
                Cursor = Cursors.WaitCursor;
                try
                {
                    //oracle.SetDateFormat();
                    DataTable tbl;
                    string sql = this.textBoxSql.Text;
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
                    else
                    {
                        tbl = oracle.Table("queryreturn", this.textBoxSql.Text);
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
                string[] columnNames = dTab.Columns.Cast<DataColumn>().Select(column => column.ColumnName).ToArray();
                sb.AppendLine(string.Join(",", columnNames));

                foreach (DataRow row in dTab.Rows)
                {
                    string[] fields = row.ItemArray.Select(field => field.ToString()).ToArray();
                    sb.AppendLine(string.Join(",", fields));
                }
                string filename = Path.ChangeExtension(Path.GetTempFileName(), ".csv");
                File.WriteAllText(filename, sb.ToString());
                System.Diagnostics.Process.Start(filename);
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
            this.dataGrid1 = new System.Windows.Forms.DataGrid();
            this.panel1 = new System.Windows.Forms.Panel();
            this.textBoxSql = new System.Windows.Forms.TextBox();
            this.buttonSql = new System.Windows.Forms.Button();
            this.buttonExcelExport = new System.Windows.Forms.Button();
            this.listBoxSqlFxns = new System.Windows.Forms.ListBox();
            this.labelSqlFxns = new System.Windows.Forms.Label();
            this.labelSqlDesc = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dataGrid1)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dataGrid1
            // 
            this.dataGrid1.DataMember = "";
            this.dataGrid1.HeaderForeColor = System.Drawing.SystemColors.ControlText;
            this.dataGrid1.Location = new System.Drawing.Point(0, 192);
            this.dataGrid1.Name = "dataGrid1";
            this.dataGrid1.ReadOnly = true;
            this.dataGrid1.Size = new System.Drawing.Size(711, 405);
            this.dataGrid1.TabIndex = 1;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.textBoxSql);
            this.panel1.Controls.Add(this.buttonSql);
            this.panel1.Controls.Add(this.buttonExcelExport);
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(714, 325);
            this.panel1.TabIndex = 0;
            // 
            // textBoxSql
            // 
            this.textBoxSql.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxSql.Font = new System.Drawing.Font("Consolas", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxSql.Location = new System.Drawing.Point(3, 3);
            this.textBoxSql.Multiline = true;
            this.textBoxSql.Name = "textBoxSql";
            this.textBoxSql.Size = new System.Drawing.Size(708, 154);
            this.textBoxSql.TabIndex = 10;
            // 
            // buttonSql
            // 
            this.buttonSql.Image = global::HdbPoet.Properties.Resources.warning;
            this.buttonSql.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonSql.Location = new System.Drawing.Point(468, 163);
            this.buttonSql.Name = "buttonSql";
            this.buttonSql.Size = new System.Drawing.Size(120, 23);
            this.buttonSql.TabIndex = 6;
            this.buttonSql.Text = "Execute SQL";
            this.buttonSql.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.buttonSql.Click += new System.EventHandler(this.buttonSql_Click);
            // 
            // buttonExcelExport
            // 
            this.buttonExcelExport.Image = ((System.Drawing.Image)(resources.GetObject("buttonExcelExport.Image")));
            this.buttonExcelExport.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonExcelExport.Location = new System.Drawing.Point(594, 163);
            this.buttonExcelExport.Name = "buttonExcelExport";
            this.buttonExcelExport.Size = new System.Drawing.Size(117, 23);
            this.buttonExcelExport.TabIndex = 2;
            this.buttonExcelExport.Text = "CSV Export";
            this.buttonExcelExport.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.buttonExcelExport.UseVisualStyleBackColor = true;
            this.buttonExcelExport.Click += new System.EventHandler(this.buttonExcelExport_Click);
            // 
            // listBoxSqlFxns
            // 
            this.listBoxSqlFxns.FormattingEnabled = true;
            this.listBoxSqlFxns.Location = new System.Drawing.Point(720, 25);
            this.listBoxSqlFxns.Name = "listBoxSqlFxns";
            this.listBoxSqlFxns.Size = new System.Drawing.Size(244, 433);
            this.listBoxSqlFxns.TabIndex = 3;
            // 
            // labelSqlFxns
            // 
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
            this.labelSqlDesc.Location = new System.Drawing.Point(720, 464);
            this.labelSqlDesc.Name = "labelSqlDesc";
            this.labelSqlDesc.Size = new System.Drawing.Size(146, 13);
            this.labelSqlDesc.TabIndex = 5;
            this.labelSqlDesc.Text = "Selected Function Desciption";
            // 
            // SqlBuilder
            // 
            this.Controls.Add(this.labelSqlDesc);
            this.Controls.Add(this.labelSqlFxns);
            this.Controls.Add(this.listBoxSqlFxns);
            this.Controls.Add(this.dataGrid1);
            this.Controls.Add(this.panel1);
            this.Name = "SqlBuilder";
            this.Size = new System.Drawing.Size(967, 600);
            ((System.ComponentModel.ISupportInitialize)(this.dataGrid1)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        #endregion






    }
}
