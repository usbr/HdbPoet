namespace HdbPoet
{
    partial class TimeSeriesTableView
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
            this.timeSeriesSpreadsheet1 = new HdbPoet.TimeSeriesSpreadsheet();
            this.SuspendLayout();
            // 
            // timeSeriesSpreadsheet1
            // 
            this.timeSeriesSpreadsheet1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.timeSeriesSpreadsheet1.Location = new System.Drawing.Point(0, 0);
            this.timeSeriesSpreadsheet1.Name = "timeSeriesSpreadsheet1";
            this.timeSeriesSpreadsheet1.Size = new System.Drawing.Size(435, 424);
            this.timeSeriesSpreadsheet1.TabIndex = 1;
            // 
            // TimeSeriesTableView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.timeSeriesSpreadsheet1);
            this.Name = "TimeSeriesTableView";
            this.Size = new System.Drawing.Size(435, 424);
            this.ResumeLayout(false);

        }

        #endregion

        private TimeSeriesSpreadsheet timeSeriesSpreadsheet1;
    }
}
