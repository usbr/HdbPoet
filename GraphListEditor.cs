using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Windows.Forms;
using System.IO;
using Reclamation.Core;

namespace HdbPoet
{
	/// <summary>
	/// 
	/// </summary>
	public class GraphListEditor : System.Windows.Forms.Form
	{
        //static int untitledCounter = 0;
		private System.Windows.Forms.ContextMenu contextMenuGraphFolder;
		private System.Windows.Forms.MenuItem menuItemFolderProperties;
        private System.Windows.Forms.Button buttonOk;
        private System.Windows.Forms.ListBox listBoxGraphs;
		private System.Windows.Forms.ContextMenu contextMenuGraphProperties;
        private System.Windows.Forms.MenuItem menuItemGraphProperties;
    private System.Windows.Forms.Button buttonMoveGraphUp;
    private System.Windows.Forms.Button buttonMoveGraphDown;
    private IContainer components;
    private ToolTip toolTip1;
    private TimeSeriesDataSet dataSet;


		public GraphListEditor()
		{
			InitializeComponent();
		}

        public GraphListEditor(TimeSeriesDataSet ds)
        {
            InitializeComponent();
            this.dataSet = ds;
            LoadGraphList();
        }

        public void LoadGraphList()
        {
            int idx = listBoxGraphs.SelectedIndex;

            listBoxGraphs.Items.Clear();
            int sz = dataSet.Graph.Rows.Count;
            for (int i = 0; i < sz; i++)
            {
                listBoxGraphs.Items.Add(dataSet.Graph[i].Name);
            }
            if (idx < listBoxGraphs.Items.Count)
                listBoxGraphs.SelectedIndex = idx;
        }

        private void buttonMoveGraphUp_Click(object sender, System.EventArgs e)
        {
            int idx = this.listBoxGraphs.SelectedIndex;
            if (idx < 0 || idx == 0)
                return;

            var g = dataSet.Graph;

            var r = g.Rows[idx];
            var array = r.ItemArray;
            g.Rows.Remove(r);

            var nr = g.NewRow();
            nr.ItemArray = array;

            g.Rows.InsertAt(nr, idx - 1);
            this.LoadGraphList();
            this.listBoxGraphs.SelectedIndex = idx - 1;
        }


        public int SelectedGraphNumber
        {
            get
            {
                return dataSet.Graph[listBoxGraphs.SelectedIndex].GraphNumber;
            }
        }

        private void buttonMoveGraphDown_Click(object sender, System.EventArgs e)
        {
            int idx = this.listBoxGraphs.SelectedIndex;
            if (idx < 0 || idx == this.listBoxGraphs.Items.Count - 1)
                return;

            var g = dataSet.Graph;

            var r = g.Rows[idx];
            var array = r.ItemArray;
            g.Rows.Remove(r);

            var nr = g.NewRow();
            nr.ItemArray = array;

            g.Rows.InsertAt(nr, idx + 1);
           


            this.LoadGraphList();
            this.listBoxGraphs.SelectedIndex = idx + 1;
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

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GraphListEditor));
            this.contextMenuGraphFolder = new System.Windows.Forms.ContextMenu();
            this.menuItemFolderProperties = new System.Windows.Forms.MenuItem();
            this.buttonOk = new System.Windows.Forms.Button();
            this.listBoxGraphs = new System.Windows.Forms.ListBox();
            this.contextMenuGraphProperties = new System.Windows.Forms.ContextMenu();
            this.menuItemGraphProperties = new System.Windows.Forms.MenuItem();
            this.buttonMoveGraphUp = new System.Windows.Forms.Button();
            this.buttonMoveGraphDown = new System.Windows.Forms.Button();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.SuspendLayout();
            // 
            // contextMenuGraphFolder
            // 
            this.contextMenuGraphFolder.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItemFolderProperties});
            // 
            // menuItemFolderProperties
            // 
            this.menuItemFolderProperties.Index = 0;
            this.menuItemFolderProperties.Text = "&Properties";
            // 
            // buttonOk
            // 
            this.buttonOk.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.buttonOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.buttonOk.Location = new System.Drawing.Point(425, 314);
            this.buttonOk.Name = "buttonOk";
            this.buttonOk.Size = new System.Drawing.Size(75, 23);
            this.buttonOk.TabIndex = 15;
            this.buttonOk.Text = "OK";
            // 
            // listBoxGraphs
            // 
            this.listBoxGraphs.ContextMenu = this.contextMenuGraphProperties;
            this.listBoxGraphs.Items.AddRange(new object[] {
            "BlueMesa_trib_daily.hdb",
            "CFRC_instant_hour_day.hdb",
            "RGRC_instant_day.hdb",
            "TPRC_daily.hdb",
            "TPRC_instant_hour_day.hdb",
            "VGRC-instant-hour-day.hdb"});
            this.listBoxGraphs.Location = new System.Drawing.Point(12, 12);
            this.listBoxGraphs.Name = "listBoxGraphs";
            this.listBoxGraphs.Size = new System.Drawing.Size(464, 277);
            this.listBoxGraphs.TabIndex = 17;
            // 
            // contextMenuGraphProperties
            // 
            this.contextMenuGraphProperties.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItemGraphProperties});
            // 
            // menuItemGraphProperties
            // 
            this.menuItemGraphProperties.Index = 0;
            this.menuItemGraphProperties.Text = "&Properties";
            // 
            // buttonMoveGraphUp
            // 
            this.buttonMoveGraphUp.BackColor = System.Drawing.Color.Silver;
            this.buttonMoveGraphUp.Image = ((System.Drawing.Image)(resources.GetObject("buttonMoveGraphUp.Image")));
            this.buttonMoveGraphUp.Location = new System.Drawing.Point(476, 92);
            this.buttonMoveGraphUp.Name = "buttonMoveGraphUp";
            this.buttonMoveGraphUp.Size = new System.Drawing.Size(24, 32);
            this.buttonMoveGraphUp.TabIndex = 25;
            this.buttonMoveGraphUp.UseVisualStyleBackColor = false;
            this.buttonMoveGraphUp.Click += new System.EventHandler(this.buttonMoveGraphUp_Click);
            // 
            // buttonMoveGraphDown
            // 
            this.buttonMoveGraphDown.BackColor = System.Drawing.Color.Silver;
            this.buttonMoveGraphDown.Image = ((System.Drawing.Image)(resources.GetObject("buttonMoveGraphDown.Image")));
            this.buttonMoveGraphDown.Location = new System.Drawing.Point(476, 124);
            this.buttonMoveGraphDown.Name = "buttonMoveGraphDown";
            this.buttonMoveGraphDown.Size = new System.Drawing.Size(24, 32);
            this.buttonMoveGraphDown.TabIndex = 26;
            this.buttonMoveGraphDown.UseVisualStyleBackColor = false;
            this.buttonMoveGraphDown.Click += new System.EventHandler(this.buttonMoveGraphDown_Click);
            // 
            // GraphListEditor
            // 
            this.AcceptButton = this.buttonOk;
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(513, 349);
            this.Controls.Add(this.buttonMoveGraphDown);
            this.Controls.Add(this.buttonMoveGraphUp);
            this.Controls.Add(this.listBoxGraphs);
            this.Controls.Add(this.buttonOk);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "GraphListEditor";
            this.Text = "Graph List Editor";
            this.ResumeLayout(false);

    }
		#endregion

		


	}
}
