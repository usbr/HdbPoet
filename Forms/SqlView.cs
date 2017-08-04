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
	public class SqlView : System.Windows.Forms.Form
	{
	OracleServer oracle = null;
	string[] sqlCommands;
    private System.Windows.Forms.DataGrid dataGrid1;
    private System.Windows.Forms.Panel panel1;
    private System.Windows.Forms.Button buttonSql;
		private System.Windows.Forms.ListBox listBoxSql;
		private System.Windows.Forms.TextBox textBoxSql;
		private System.Windows.Forms.Button buttonClear;
		private System.Windows.Forms.Label labelRecordCount;
    private System.Windows.Forms.Button buttonInfo;
		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public SqlView(OracleServer oracle )
		{
			this.oracle = oracle;
			InitializeComponent();
           this.Text = "SQL Viewer";
			this.SqlCommands = oracle.SqlHistory;
		}
    public string[] SqlCommands
    {
      set 
      {
		  if(value == null) 
			   return;
		  this.sqlCommands = new string[value.Length];
		  Array.Copy(value,sqlCommands,value.Length);

		  if( this.sqlCommands.Length <=0)
			  return;
		  this.listBoxSql.Items.Clear();

		  Array.Reverse(this.sqlCommands);

	     this.listBoxSql.Items.AddRange(this.sqlCommands);

		  //if(this.listBoxSql.Items.Count >0)
			//this.listBoxSql.SelectedIndex=0;
      }

    }

		/// <summary> 
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if(components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Component Designer generated code
		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SqlView));
            this.dataGrid1 = new System.Windows.Forms.DataGrid();
            this.panel1 = new System.Windows.Forms.Panel();
            this.buttonInfo = new System.Windows.Forms.Button();
            this.labelRecordCount = new System.Windows.Forms.Label();
            this.buttonClear = new System.Windows.Forms.Button();
            this.textBoxSql = new System.Windows.Forms.TextBox();
            this.listBoxSql = new System.Windows.Forms.ListBox();
            this.buttonSql = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGrid1)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dataGrid1
            // 
            this.dataGrid1.DataMember = "";
            this.dataGrid1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGrid1.HeaderForeColor = System.Drawing.SystemColors.ControlText;
            this.dataGrid1.Location = new System.Drawing.Point(0, 278);
            this.dataGrid1.Name = "dataGrid1";
            this.dataGrid1.ReadOnly = true;
            this.dataGrid1.Size = new System.Drawing.Size(761, 181);
            this.dataGrid1.TabIndex = 1;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.buttonInfo);
            this.panel1.Controls.Add(this.labelRecordCount);
            this.panel1.Controls.Add(this.buttonClear);
            this.panel1.Controls.Add(this.textBoxSql);
            this.panel1.Controls.Add(this.listBoxSql);
            this.panel1.Controls.Add(this.buttonSql);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(761, 278);
            this.panel1.TabIndex = 0;
            // 
            // buttonInfo
            // 
            this.buttonInfo.Location = new System.Drawing.Point(8, 238);
            this.buttonInfo.Name = "buttonInfo";
            this.buttonInfo.Size = new System.Drawing.Size(75, 23);
            this.buttonInfo.TabIndex = 14;
            this.buttonInfo.Text = "info...";
            this.buttonInfo.Click += new System.EventHandler(this.buttonInfo_Click);
            // 
            // labelRecordCount
            // 
            this.labelRecordCount.Location = new System.Drawing.Point(160, 246);
            this.labelRecordCount.Name = "labelRecordCount";
            this.labelRecordCount.Size = new System.Drawing.Size(288, 16);
            this.labelRecordCount.TabIndex = 13;
            this.labelRecordCount.Text = "0 Records";
            // 
            // buttonClear
            // 
            this.buttonClear.Location = new System.Drawing.Point(536, 246);
            this.buttonClear.Name = "buttonClear";
            this.buttonClear.Size = new System.Drawing.Size(64, 23);
            this.buttonClear.TabIndex = 12;
            this.buttonClear.Text = "Clear";
            this.buttonClear.Click += new System.EventHandler(this.buttonClear_Click_1);
            // 
            // textBoxSql
            // 
            this.textBoxSql.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxSql.Location = new System.Drawing.Point(8, 184);
            this.textBoxSql.Multiline = true;
            this.textBoxSql.Name = "textBoxSql";
            this.textBoxSql.Size = new System.Drawing.Size(745, 48);
            this.textBoxSql.TabIndex = 10;
            // 
            // listBoxSql
            // 
            this.listBoxSql.Dock = System.Windows.Forms.DockStyle.Top;
            this.listBoxSql.Location = new System.Drawing.Point(0, 0);
            this.listBoxSql.Name = "listBoxSql";
            this.listBoxSql.Size = new System.Drawing.Size(761, 173);
            this.listBoxSql.TabIndex = 9;
            this.listBoxSql.SelectedIndexChanged += new System.EventHandler(this.listBoxSql_SelectedIndexChanged);
            // 
            // buttonSql
            // 
            this.buttonSql.Location = new System.Drawing.Point(616, 246);
            this.buttonSql.Name = "buttonSql";
            this.buttonSql.Size = new System.Drawing.Size(88, 23);
            this.buttonSql.TabIndex = 6;
            this.buttonSql.Text = "Execute SQL";
            this.buttonSql.Click += new System.EventHandler(this.buttonSql_Click);
            // 
            // SqlView
            // 
            this.AcceptButton = this.buttonSql;
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(761, 459);
            this.Controls.Add(this.dataGrid1);
            this.Controls.Add(this.panel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "SqlView";
            this.Text = "SQL Viewer ";
            ((System.ComponentModel.ISupportInitialize)(this.dataGrid1)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

    }
		#endregion

    private void buttonSql_Click(object sender, System.EventArgs e)
    {
      if(this.textBoxSql.Text.Length>0)
      {
		Cursor = Cursors.WaitCursor;
		  try
		  {
			  //oracle.SetDateFormat();
		DataTable tbl ;
			  string sql =this.textBoxSql.Text;
			  if( sql.ToLower().IndexOf("update") >=0
				  || sql.ToLower().IndexOf("insert") >=0
				  || sql.ToLower().IndexOf("alter") >=0
				  )
			  {
				  oracle.RunSqlCommand(sql);
				  tbl = new DataTable();
			  }
			  else
			  {
				  tbl = oracle.Table("test",this.textBoxSql.Text);
			  }
			  this.dataGrid1.DataSource = tbl;

			  if( tbl.Columns.Contains("tmstp"))
			  {
//				SoiUtility.FormatDataColumn(dataGrid1,"tmstp","Time Stamp",SoiConstants.TimeFormat);
			  }
			
			  this.labelRecordCount.Text = tbl.Rows.Count.ToString()+" Records";

			  //this.dataGrid1.ReadOnly = true;
			  this.listBoxSql.Items.Add(this.textBoxSql.Text);
		  }
		  catch(Exception ex)
		  {
			  string msg = "Error: "+ex.Message;
			  MessageBox.Show (msg, "HDB Poet", 
				  MessageBoxButtons.OK, MessageBoxIcon.Error);

		  }
		  finally
		  {
			  Cursor = Cursors.Default;
		  }
        
      }
    }

    private void buttonClear_Click(object sender, System.EventArgs e)
    {
    this.dataGrid1.DataSource = null;
    }

    private void buttonSaveResults_Click(object sender, System.EventArgs e)
    {
      
    }


		private void listBoxSql_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			this.textBoxSql.Text = listBoxSql.Text;
		}


		private void buttonClear_Click_1(object sender, System.EventArgs e)
		{
			this.listBoxSql.Items.Clear();
			oracle.SqlHistory = new string[]{};
		}

    private void buttonInfo_Click(object sender, System.EventArgs e)
    {
      HdbInfo hdbinfo = new HdbInfo();
      hdbinfo.ShowDialog();
    }
	}
}
