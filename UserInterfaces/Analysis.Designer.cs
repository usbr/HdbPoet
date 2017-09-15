namespace HdbPoet
{
    public partial class DataAnalysis
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
            this.selectAnalysisButton = new System.Windows.Forms.Button();
            this.selectedAnalysisLabel = new System.Windows.Forms.Label();
            this.availableSeriesLabel = new System.Windows.Forms.Label();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.rerunAnalysisButton = new System.Windows.Forms.Button();
            this.selectedAnalysisTextBox = new System.Windows.Forms.RichTextBox();
            this.selectedSeriesListBox1 = new HdbPoet.SelectedSeriesListBox();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // selectAnalysisButton
            // 
            this.selectAnalysisButton.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.selectAnalysisButton.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.selectAnalysisButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.selectAnalysisButton.Location = new System.Drawing.Point(6, 3);
            this.selectAnalysisButton.Name = "selectAnalysisButton";
            this.selectAnalysisButton.Size = new System.Drawing.Size(306, 36);
            this.selectAnalysisButton.TabIndex = 2;
            this.selectAnalysisButton.Text = "Select Analysis";
            this.selectAnalysisButton.UseVisualStyleBackColor = false;
            this.selectAnalysisButton.Click += new System.EventHandler(this.DisplayAnalysisOptions);
            // 
            // selectedAnalysisLabel
            // 
            this.selectedAnalysisLabel.AutoSize = true;
            this.selectedAnalysisLabel.Location = new System.Drawing.Point(3, 42);
            this.selectedAnalysisLabel.Name = "selectedAnalysisLabel";
            this.selectedAnalysisLabel.Size = new System.Drawing.Size(156, 13);
            this.selectedAnalysisLabel.TabIndex = 4;
            this.selectedAnalysisLabel.Text = "Selected Analysis and Options: ";
            // 
            // availableSeriesLabel
            // 
            this.availableSeriesLabel.AutoSize = true;
            this.availableSeriesLabel.Location = new System.Drawing.Point(3, 109);
            this.availableSeriesLabel.Name = "availableSeriesLabel";
            this.availableSeriesLabel.Size = new System.Drawing.Size(84, 13);
            this.availableSeriesLabel.TabIndex = 5;
            this.availableSeriesLabel.Text = "Selected Series:";
            // 
            // splitContainer1
            // 
            this.splitContainer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.splitContainer1.Location = new System.Drawing.Point(3, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.statusStrip1);
            this.splitContainer1.Panel1.Controls.Add(this.rerunAnalysisButton);
            this.splitContainer1.Panel1.Controls.Add(this.selectedAnalysisTextBox);
            this.splitContainer1.Panel1.Controls.Add(this.selectedSeriesListBox1);
            this.splitContainer1.Panel1.Controls.Add(this.selectAnalysisButton);
            this.splitContainer1.Panel1.Controls.Add(this.selectedAnalysisLabel);
            this.splitContainer1.Panel1.Controls.Add(this.availableSeriesLabel);
            this.splitContainer1.Panel1.Margin = new System.Windows.Forms.Padding(1);
            this.splitContainer1.Panel1MinSize = 250;
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Margin = new System.Windows.Forms.Padding(1);
            this.splitContainer1.Panel2MinSize = 250;
            this.splitContainer1.Size = new System.Drawing.Size(961, 597);
            this.splitContainer1.SplitterDistance = 319;
            this.splitContainer1.TabIndex = 6;
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1});
            this.statusStrip1.Location = new System.Drawing.Point(0, 571);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(315, 22);
            this.statusStrip1.TabIndex = 7;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(10, 17);
            this.toolStripStatusLabel1.Text = " ";
            // 
            // rerunAnalysisButton
            // 
            this.rerunAnalysisButton.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.rerunAnalysisButton.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.rerunAnalysisButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rerunAnalysisButton.Location = new System.Drawing.Point(3, 544);
            this.rerunAnalysisButton.Name = "rerunAnalysisButton";
            this.rerunAnalysisButton.Size = new System.Drawing.Size(306, 24);
            this.rerunAnalysisButton.TabIndex = 6;
            this.rerunAnalysisButton.Text = "(Re)Run Analysis";
            this.rerunAnalysisButton.UseVisualStyleBackColor = false;
            this.rerunAnalysisButton.Click += new System.EventHandler(this.ReRun);
            // 
            // selectedAnalysisTextBox
            // 
            this.selectedAnalysisTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.selectedAnalysisTextBox.Enabled = false;
            this.selectedAnalysisTextBox.Location = new System.Drawing.Point(6, 60);
            this.selectedAnalysisTextBox.Name = "selectedAnalysisTextBox";
            this.selectedAnalysisTextBox.Size = new System.Drawing.Size(306, 45);
            this.selectedAnalysisTextBox.TabIndex = 2;
            this.selectedAnalysisTextBox.Text = "";
            // 
            // selectedSeriesListBox1
            // 
            this.selectedSeriesListBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.selectedSeriesListBox1.Location = new System.Drawing.Point(6, 126);
            this.selectedSeriesListBox1.Margin = new System.Windows.Forms.Padding(0);
            this.selectedSeriesListBox1.Name = "selectedSeriesListBox1";
            this.selectedSeriesListBox1.Size = new System.Drawing.Size(342, 415);
            this.selectedSeriesListBox1.TabIndex = 0;
            // 
            // backgroundWorker1
            // 
            this.backgroundWorker1.WorkerSupportsCancellation = true;
            this.backgroundWorker1.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker1_DoWork);
            this.backgroundWorker1.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorker1_RunWorkerCompleted);
            // 
            // DataAnalysis
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitContainer1);
            this.Name = "DataAnalysis";
            this.Size = new System.Drawing.Size(967, 600);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        public SelectedSeriesListBox selectedSeriesListBox1;
        private System.Windows.Forms.Button selectAnalysisButton;
        private System.Windows.Forms.Label selectedAnalysisLabel;
        private System.Windows.Forms.Label availableSeriesLabel;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.RichTextBox selectedAnalysisTextBox;
        private System.Windows.Forms.Button rerunAnalysisButton;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
    }
}
