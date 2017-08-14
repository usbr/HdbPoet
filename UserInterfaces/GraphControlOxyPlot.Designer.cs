namespace HdbPoet
{
    partial class GraphControlOxyPlot
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GraphControlOxyPlot));
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
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItemDates,
            this.undoZoomToolStripMenuItem,
            this.propertieschartToolStripMenuItem,
            this.editSeriesToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(178, 92);
            // 
            // toolStripMenuItemDates
            // 
            this.toolStripMenuItemDates.Name = "toolStripMenuItemDates";
            this.toolStripMenuItemDates.Size = new System.Drawing.Size(177, 22);
            this.toolStripMenuItemDates.Text = "Dates...";
            this.toolStripMenuItemDates.Click += new System.EventHandler(this.toolStripMenuItemDates_Click);
            // 
            // undoZoomToolStripMenuItem
            // 
            this.undoZoomToolStripMenuItem.Name = "undoZoomToolStripMenuItem";
            this.undoZoomToolStripMenuItem.Size = new System.Drawing.Size(177, 22);
            this.undoZoomToolStripMenuItem.Text = "Undo Zoom";
            this.undoZoomToolStripMenuItem.Click += new System.EventHandler(this.undoZoomToolStripMenuItem_Click);
            // 
            // propertieschartToolStripMenuItem
            // 
            this.propertieschartToolStripMenuItem.Enabled = true;
            this.propertieschartToolStripMenuItem.Name = "propertieschartToolStripMenuItem";
            this.propertieschartToolStripMenuItem.Size = new System.Drawing.Size(177, 22);
            this.propertieschartToolStripMenuItem.Text = "Properties (chart) ...";
            this.propertieschartToolStripMenuItem.Click += new System.EventHandler(this.propertieschartToolStripMenuItem_Click);
            // 
            // editSeriesToolStripMenuItem
            // 
            this.editSeriesToolStripMenuItem.Enabled = false;
            this.editSeriesToolStripMenuItem.Name = "editSeriesToolStripMenuItem";
            this.editSeriesToolStripMenuItem.Size = new System.Drawing.Size(177, 22);
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
            this.toolStripButtonPrint.Size = new System.Drawing.Size(36, 22);
            this.toolStripButtonPrint.Text = "Print";
            this.toolStripButtonPrint.Click += new System.EventHandler(this.toolStripButtonPrint_Click);
            // 
            // toolStripButtonDragPoints
            // 
            this.toolStripButtonDragPoints.CheckOnClick = true;
            this.toolStripButtonDragPoints.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripButtonDragPoints.Enabled = false;
            this.toolStripButtonDragPoints.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonDragPoints.Image")));
            this.toolStripButtonDragPoints.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonDragPoints.Name = "toolStripButtonDragPoints";
            this.toolStripButtonDragPoints.Size = new System.Drawing.Size(72, 22);
            this.toolStripButtonDragPoints.Text = "Drag points";
            this.toolStripButtonDragPoints.Click += new System.EventHandler(this.toolStripButtonDragPoints_Click);
            // 
            // toolStripButtonUndoZoom
            // 
            this.toolStripButtonUndoZoom.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripButtonUndoZoom.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonUndoZoom.Image")));
            this.toolStripButtonUndoZoom.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonUndoZoom.Name = "toolStripButtonUndoZoom";
            this.toolStripButtonUndoZoom.Size = new System.Drawing.Size(75, 22);
            this.toolStripButtonUndoZoom.Text = "Undo Zoom";
            this.toolStripButtonUndoZoom.Click += new System.EventHandler(this.toolStripButtonUndoZoom_Click);
            // 
            // toolStripButtonProperties
            // 
            this.toolStripButtonProperties.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripButtonProperties.Enabled = false;
            this.toolStripButtonProperties.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonProperties.Image")));
            this.toolStripButtonProperties.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonProperties.Name = "toolStripButtonProperties";
            this.toolStripButtonProperties.Size = new System.Drawing.Size(64, 22);
            this.toolStripButtonProperties.Text = "Properties";
            this.toolStripButtonProperties.Click += new System.EventHandler(this.toolStripButtonProperties_Click);
            // 
            // toolStripButtonDates
            // 
            this.toolStripButtonDates.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripButtonDates.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonDates.Image")));
            this.toolStripButtonDates.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonDates.Name = "toolStripButtonDates";
            this.toolStripButtonDates.Size = new System.Drawing.Size(40, 22);
            this.toolStripButtonDates.Text = "Dates";
            this.toolStripButtonDates.Click += new System.EventHandler(this.toolStripButtonDates_Click);
            // 
            // GraphControlOxyPlot
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.toolStrip1);
            this.Name = "GraphControlOxyPlot";
            this.Size = new System.Drawing.Size(557, 382);
            this.contextMenuStrip1.ResumeLayout(false);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

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