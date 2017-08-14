namespace HdbPoet.MetaData
{
    partial class RatingTableEditor
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.panel1 = new System.Windows.Forms.Panel();
            this.linkLabelRefresh = new System.Windows.Forms.LinkLabel();
            this.linkLabelClear = new System.Windows.Forms.LinkLabel();
            this.labelSaveStatus = new System.Windows.Forms.Label();
            this.labelRatingTableName = new System.Windows.Forms.Label();
            this.labelSiteName = new System.Windows.Forms.Label();
            this.linkLabelSave = new System.Windows.Forms.LinkLabel();
            this.linkLabelBack = new System.Windows.Forms.LinkLabel();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.copyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pasteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemInterpolate = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.deleteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPageTable = new System.Windows.Forms.TabPage();
            this.tabPageGraph = new System.Windows.Forms.TabPage();
            this.graph1 = new HdbPoet.MetaData.RatingTableGraphOxyPlot();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPageTable.SuspendLayout();
            this.tabPageGraph.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.linkLabelRefresh);
            this.panel1.Controls.Add(this.linkLabelClear);
            this.panel1.Controls.Add(this.labelSaveStatus);
            this.panel1.Controls.Add(this.labelRatingTableName);
            this.panel1.Controls.Add(this.labelSiteName);
            this.panel1.Controls.Add(this.linkLabelSave);
            this.panel1.Controls.Add(this.linkLabelBack);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(580, 87);
            this.panel1.TabIndex = 0;
            // 
            // linkLabelRefresh
            // 
            this.linkLabelRefresh.AutoSize = true;
            this.linkLabelRefresh.Location = new System.Drawing.Point(91, 7);
            this.linkLabelRefresh.Name = "linkLabelRefresh";
            this.linkLabelRefresh.Size = new System.Drawing.Size(39, 13);
            this.linkLabelRefresh.TabIndex = 15;
            this.linkLabelRefresh.TabStop = true;
            this.linkLabelRefresh.Text = "refresh";
            this.linkLabelRefresh.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabelRefresh_LinkClicked);
            // 
            // linkLabelClear
            // 
            this.linkLabelClear.AutoSize = true;
            this.linkLabelClear.Location = new System.Drawing.Point(136, 7);
            this.linkLabelClear.Name = "linkLabelClear";
            this.linkLabelClear.Size = new System.Drawing.Size(56, 13);
            this.linkLabelClear.TabIndex = 14;
            this.linkLabelClear.TabStop = true;
            this.linkLabelClear.Text = "clear table";
            this.linkLabelClear.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabelClear_LinkClicked);
            // 
            // labelSaveStatus
            // 
            this.labelSaveStatus.AutoSize = true;
            this.labelSaveStatus.Location = new System.Drawing.Point(198, 7);
            this.labelSaveStatus.Name = "labelSaveStatus";
            this.labelSaveStatus.Size = new System.Drawing.Size(35, 13);
            this.labelSaveStatus.TabIndex = 13;
            this.labelSaveStatus.Text = "status";
            // 
            // labelRatingTableName
            // 
            this.labelRatingTableName.AutoSize = true;
            this.labelRatingTableName.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelRatingTableName.Location = new System.Drawing.Point(2, 52);
            this.labelRatingTableName.Name = "labelRatingTableName";
            this.labelRatingTableName.Size = new System.Drawing.Size(70, 25);
            this.labelRatingTableName.TabIndex = 12;
            this.labelRatingTableName.Text = "label2";
            // 
            // labelSiteName
            // 
            this.labelSiteName.AutoSize = true;
            this.labelSiteName.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelSiteName.Location = new System.Drawing.Point(2, 27);
            this.labelSiteName.Name = "labelSiteName";
            this.labelSiteName.Size = new System.Drawing.Size(70, 25);
            this.labelSiteName.TabIndex = 11;
            this.labelSiteName.Text = "label1";
            // 
            // linkLabelSave
            // 
            this.linkLabelSave.AutoSize = true;
            this.linkLabelSave.Location = new System.Drawing.Point(55, 7);
            this.linkLabelSave.Name = "linkLabelSave";
            this.linkLabelSave.Size = new System.Drawing.Size(30, 13);
            this.linkLabelSave.TabIndex = 10;
            this.linkLabelSave.TabStop = true;
            this.linkLabelSave.Text = "save";
            this.linkLabelSave.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabelSave_LinkClicked);
            // 
            // linkLabelBack
            // 
            this.linkLabelBack.AutoSize = true;
            this.linkLabelBack.Location = new System.Drawing.Point(6, 7);
            this.linkLabelBack.Name = "linkLabelBack";
            this.linkLabelBack.Size = new System.Drawing.Size(43, 13);
            this.linkLabelBack.TabIndex = 7;
            this.linkLabelBack.TabStop = true;
            this.linkLabelBack.Text = "<<back";
            this.linkLabelBack.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabelBack_LinkClicked);
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.ContextMenuStrip = this.contextMenuStrip1;
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.Location = new System.Drawing.Point(3, 3);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(566, 426);
            this.dataGridView1.TabIndex = 1;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.copyToolStripMenuItem,
            this.pasteToolStripMenuItem,
            this.toolStripMenuItemInterpolate,
            this.toolStripMenuItem1,
            this.deleteToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(157, 98);
            // 
            // copyToolStripMenuItem
            // 
            this.copyToolStripMenuItem.Name = "copyToolStripMenuItem";
            this.copyToolStripMenuItem.Size = new System.Drawing.Size(156, 22);
            this.copyToolStripMenuItem.Text = "&Copy";
            this.copyToolStripMenuItem.Click += new System.EventHandler(this.copyToolStripMenuItem_Click);
            // 
            // pasteToolStripMenuItem
            // 
            this.pasteToolStripMenuItem.Name = "pasteToolStripMenuItem";
            this.pasteToolStripMenuItem.Size = new System.Drawing.Size(156, 22);
            this.pasteToolStripMenuItem.Text = "&Paste";
            this.pasteToolStripMenuItem.Click += new System.EventHandler(this.pasteToolStripMenuItem_Click);
            // 
            // toolStripMenuItemInterpolate
            // 
            this.toolStripMenuItemInterpolate.Name = "toolStripMenuItemInterpolate";
            this.toolStripMenuItemInterpolate.Size = new System.Drawing.Size(156, 22);
            this.toolStripMenuItemInterpolate.Text = "Interpolated Fill";
            this.toolStripMenuItemInterpolate.Click += new System.EventHandler(this.toolStripMenuItemInterpolate_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(153, 6);
            // 
            // deleteToolStripMenuItem
            // 
            this.deleteToolStripMenuItem.Name = "deleteToolStripMenuItem";
            this.deleteToolStripMenuItem.Size = new System.Drawing.Size(156, 22);
            this.deleteToolStripMenuItem.Text = "&Delete";
            this.deleteToolStripMenuItem.Click += new System.EventHandler(this.deleteToolStripMenuItem_Click);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPageTable);
            this.tabControl1.Controls.Add(this.tabPageGraph);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 87);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(580, 458);
            this.tabControl1.TabIndex = 2;
            this.tabControl1.SelectedIndexChanged += new System.EventHandler(this.tabControl1_SelectedIndexChanged);
            // 
            // tabPageTable
            // 
            this.tabPageTable.Controls.Add(this.dataGridView1);
            this.tabPageTable.Location = new System.Drawing.Point(4, 22);
            this.tabPageTable.Name = "tabPageTable";
            this.tabPageTable.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageTable.Size = new System.Drawing.Size(572, 432);
            this.tabPageTable.TabIndex = 0;
            this.tabPageTable.Text = "table";
            this.tabPageTable.UseVisualStyleBackColor = true;
            // 
            // tabPageGraph
            // 
            this.tabPageGraph.Controls.Add(this.graph1);
            this.tabPageGraph.Location = new System.Drawing.Point(4, 22);
            this.tabPageGraph.Name = "tabPageGraph";
            this.tabPageGraph.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageGraph.Size = new System.Drawing.Size(572, 432);
            this.tabPageGraph.TabIndex = 1;
            this.tabPageGraph.Text = "graph";
            this.tabPageGraph.UseVisualStyleBackColor = true;
            // 
            // graph1
            // 
            this.graph1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.graph1.Location = new System.Drawing.Point(3, 3);
            this.graph1.Name = "graph1";
            this.graph1.Size = new System.Drawing.Size(566, 426);
            this.graph1.TabIndex = 0;
            // 
            // RatingTableEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.panel1);
            this.Name = "RatingTableEditor";
            this.Size = new System.Drawing.Size(580, 545);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tabPageTable.ResumeLayout(false);
            this.tabPageGraph.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.LinkLabel linkLabelBack;
        private System.Windows.Forms.LinkLabel linkLabelSave;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPageTable;
        private System.Windows.Forms.TabPage tabPageGraph;
        private RatingTableGraphOxyPlot graph1;
        private System.Windows.Forms.Label labelRatingTableName;
        private System.Windows.Forms.Label labelSiteName;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem copyToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem pasteToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemInterpolate;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem deleteToolStripMenuItem;
        private System.Windows.Forms.Label labelSaveStatus;
        private System.Windows.Forms.LinkLabel linkLabelClear;
        private System.Windows.Forms.LinkLabel linkLabelRefresh;
    }
}
