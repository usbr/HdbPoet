using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;
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

        #region Component Designer generated code
        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.dataGrid1 = new System.Windows.Forms.DataGrid();
            this.panel1 = new System.Windows.Forms.Panel();
            this.textBoxSql = new System.Windows.Forms.TextBox();
            this.buttonSql = new System.Windows.Forms.Button();
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
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(714, 325);
            this.panel1.TabIndex = 0;
            // 
            // textBoxSql
            // 
            this.textBoxSql.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxSql.Location = new System.Drawing.Point(3, 3);
            this.textBoxSql.Multiline = true;
            this.textBoxSql.Name = "textBoxSql";
            this.textBoxSql.Size = new System.Drawing.Size(708, 154);
            this.textBoxSql.TabIndex = 10;
            // 
            // buttonSql
            // 
            this.buttonSql.Location = new System.Drawing.Point(623, 163);
            this.buttonSql.Name = "buttonSql";
            this.buttonSql.Size = new System.Drawing.Size(88, 23);
            this.buttonSql.TabIndex = 6;
            this.buttonSql.Text = "Execute SQL";
            this.buttonSql.Click += new System.EventHandler(this.buttonSql_Click);
            // 
            // SqlBuilder
            // 
            this.Controls.Add(this.dataGrid1);
            this.Controls.Add(this.panel1);
            this.Name = "SqlBuilder";
            this.Size = new System.Drawing.Size(967, 600);
            ((System.ComponentModel.ISupportInitialize)(this.dataGrid1)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }
        #endregion






    }
}
