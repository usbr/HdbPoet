namespace HdbPoet
{
    partial class GraphControl
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GraphControl));
            this.chart = new Steema.TeeChart.TChart();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStripMenuItemDates = new System.Windows.Forms.ToolStripMenuItem();
            this.undoZoomToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.propertieschartToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editSeriesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripButtonPrint = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonDragPoints = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonUndoZoom = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonProperties = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonDates = new System.Windows.Forms.ToolStripButton();
            this.contextMenuStrip1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // chart
            // 
            // 
            // 
            // 
            this.chart.Aspect.View3D = false;
            this.chart.Aspect.ZOffset = 0D;
            // 
            // 
            // 
            // 
            // 
            // 
            // 
            // 
            // 
            this.chart.Axes.Bottom.Title.Transparent = true;
            // 
            // 
            // 
            // 
            // 
            // 
            this.chart.Axes.Depth.Title.Transparent = true;
            // 
            // 
            // 
            // 
            // 
            // 
            this.chart.Axes.DepthTop.Title.Transparent = true;
            // 
            // 
            // 
            // 
            // 
            // 
            this.chart.Axes.Left.Title.Transparent = true;
            // 
            // 
            // 
            // 
            // 
            // 
            this.chart.Axes.Right.Title.Transparent = true;
            // 
            // 
            // 
            // 
            // 
            // 
            this.chart.Axes.Top.Title.Transparent = true;
            this.chart.ContextMenuStrip = this.contextMenuStrip1;
            this.chart.Dock = System.Windows.Forms.DockStyle.Fill;
            // 
            // 
            // 
            // 
            // 
            // 
            // 
            // 
            // 
            this.chart.Header.Font.Brush.Color = System.Drawing.Color.Black;
            this.chart.Header.Font.Size = 14;
            this.chart.Header.Font.SizeFloat = 14F;
            this.chart.Header.Lines = new string[] {
        ""};
            this.chart.Location = new System.Drawing.Point(0, 25);
            this.chart.Name = "chart";
            this.chart.Size = new System.Drawing.Size(557, 357);
            this.chart.TabIndex = 1;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItemDates,
            this.undoZoomToolStripMenuItem,
            this.propertieschartToolStripMenuItem,
            this.editSeriesToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(186, 92);
            // 
            // toolStripMenuItemDates
            // 
            this.toolStripMenuItemDates.Name = "toolStripMenuItemDates";
            this.toolStripMenuItemDates.Size = new System.Drawing.Size(185, 22);
            this.toolStripMenuItemDates.Text = "Dates...";
            this.toolStripMenuItemDates.Click += new System.EventHandler(this.toolStripMenuItemDates_Click);
            // 
            // undoZoomToolStripMenuItem
            // 
            this.undoZoomToolStripMenuItem.Name = "undoZoomToolStripMenuItem";
            this.undoZoomToolStripMenuItem.Size = new System.Drawing.Size(185, 22);
            this.undoZoomToolStripMenuItem.Text = "Undo Zoom";
            this.undoZoomToolStripMenuItem.Click += new System.EventHandler(this.undoZoomToolStripMenuItem_Click);
            // 
            // propertieschartToolStripMenuItem
            // 
            this.propertieschartToolStripMenuItem.Name = "propertieschartToolStripMenuItem";
            this.propertieschartToolStripMenuItem.Size = new System.Drawing.Size(185, 22);
            this.propertieschartToolStripMenuItem.Text = "Properties (chart) ...";
            this.propertieschartToolStripMenuItem.Click += new System.EventHandler(this.propertieschartToolStripMenuItem_Click);
            // 
            // editSeriesToolStripMenuItem
            // 
            this.editSeriesToolStripMenuItem.Name = "editSeriesToolStripMenuItem";
            this.editSeriesToolStripMenuItem.Size = new System.Drawing.Size(185, 22);
            this.editSeriesToolStripMenuItem.Text = "Edit Series ...";
            this.editSeriesToolStripMenuItem.Click += new System.EventHandler(this.editSeriesToolStripMenuItem_Click);
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButtonPrint,
            this.toolStripButtonDragPoints,
            this.toolStripButtonUndoZoom,
            this.toolStripButtonProperties,
            this.toolStripButtonDates});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(557, 25);
            this.toolStrip1.TabIndex = 2;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripButtonPrint
            // 
            this.toolStripButtonPrint.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripButtonPrint.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonPrint.Image")));
            this.toolStripButtonPrint.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonPrint.Name = "toolStripButtonPrint";
            this.toolStripButtonPrint.Size = new System.Drawing.Size(33, 22);
            this.toolStripButtonPrint.Text = "Print";
            this.toolStripButtonPrint.Click += new System.EventHandler(this.toolStripButtonPrint_Click);
            // 
            // toolStripButtonDragPoints
            // 
            this.toolStripButtonDragPoints.CheckOnClick = true;
            this.toolStripButtonDragPoints.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripButtonDragPoints.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonDragPoints.Image")));
            this.toolStripButtonDragPoints.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonDragPoints.Name = "toolStripButtonDragPoints";
            this.toolStripButtonDragPoints.Size = new System.Drawing.Size(66, 22);
            this.toolStripButtonDragPoints.Text = "Drag points";
            this.toolStripButtonDragPoints.Click += new System.EventHandler(this.toolStripButtonDragPoints_Click);
            // 
            // toolStripButtonUndoZoom
            // 
            this.toolStripButtonUndoZoom.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripButtonUndoZoom.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonUndoZoom.Image")));
            this.toolStripButtonUndoZoom.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonUndoZoom.Name = "toolStripButtonUndoZoom";
            this.toolStripButtonUndoZoom.Size = new System.Drawing.Size(65, 22);
            this.toolStripButtonUndoZoom.Text = "Undo Zoom";
            this.toolStripButtonUndoZoom.Click += new System.EventHandler(this.toolStripButtonUndoZoom_Click);
            // 
            // toolStripButtonProperties
            // 
            this.toolStripButtonProperties.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripButtonProperties.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonProperties.Image")));
            this.toolStripButtonProperties.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonProperties.Name = "toolStripButtonProperties";
            this.toolStripButtonProperties.Size = new System.Drawing.Size(60, 22);
            this.toolStripButtonProperties.Text = "Properties";
            this.toolStripButtonProperties.Click += new System.EventHandler(this.toolStripButtonProperties_Click);
            // 
            // toolStripButtonDates
            // 
            this.toolStripButtonDates.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripButtonDates.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonDates.Image")));
            this.toolStripButtonDates.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonDates.Name = "toolStripButtonDates";
            this.toolStripButtonDates.Size = new System.Drawing.Size(39, 22);
            this.toolStripButtonDates.Text = "Dates";
            this.toolStripButtonDates.Click += new System.EventHandler(this.toolStripButtonDates_Click);
            // 
            // GraphForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(557, 382);
            this.Controls.Add(this.chart);
            this.Controls.Add(this.toolStrip1);
            this.Name = "GraphForm";
            this.Text = "Graph";
            this.contextMenuStrip1.ResumeLayout(false);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Steema.TeeChart.TChart chart;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton toolStripButtonDragPoints;
        private System.Windows.Forms.ToolStripButton toolStripButtonUndoZoom;
        private System.Windows.Forms.ToolStripButton toolStripButtonProperties;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemDates;
        private System.Windows.Forms.ToolStripMenuItem undoZoomToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem propertieschartToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem editSeriesToolStripMenuItem;
        private System.Windows.Forms.ToolStripButton toolStripButtonDates;
        private System.Windows.Forms.ToolStripButton toolStripButtonPrint;
    }
}