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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DataAnalysis));
            this.graphExplorerView1 = new Reclamation.TimeSeries.Graphing.GraphExplorerView();
            this.cropDatesDataSet1 = new Reclamation.TimeSeries.AgriMet.CropDatesDataSet();
            this.selectAnalysisButton = new System.Windows.Forms.Button();
            this.selectedAnalysisTextBox = new System.Windows.Forms.TextBox();
            this.selectedAnalysisLabel = new System.Windows.Forms.Label();
            this.availableSeriesLabel = new System.Windows.Forms.Label();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.selectedSeriesListBox1 = new HdbPoet.SelectedSeriesListBox();
            ((System.ComponentModel.ISupportInitialize)(this.cropDatesDataSet1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // graphExplorerView1
            // 
            this.graphExplorerView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.graphExplorerView1.DataTable = null;
            this.graphExplorerView1.Location = new System.Drawing.Point(3, 3);
            this.graphExplorerView1.Messages = ((System.Collections.Generic.List<string>)(resources.GetObject("graphExplorerView1.Messages")));
            this.graphExplorerView1.Name = "graphExplorerView1";
            this.graphExplorerView1.Size = new System.Drawing.Size(628, 584);
            this.graphExplorerView1.TabIndex = 1;
            this.graphExplorerView1.UndoZoom = false;
            // 
            // cropDatesDataSet1
            // 
            this.cropDatesDataSet1.DataSetName = "CropDatesDataSet";
            this.cropDatesDataSet1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // selectAnalysisButton
            // 
            this.selectAnalysisButton.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.selectAnalysisButton.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.selectAnalysisButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.selectAnalysisButton.Location = new System.Drawing.Point(6, 3);
            this.selectAnalysisButton.Name = "selectAnalysisButton";
            this.selectAnalysisButton.Size = new System.Drawing.Size(301, 26);
            this.selectAnalysisButton.TabIndex = 2;
            this.selectAnalysisButton.Text = "Select Analysis";
            this.selectAnalysisButton.UseVisualStyleBackColor = false;
            this.selectAnalysisButton.Click += new System.EventHandler(this.DisplayAnalysisOptions);
            // 
            // selectedAnalysisTextBox
            // 
            this.selectedAnalysisTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.selectedAnalysisTextBox.Enabled = false;
            this.selectedAnalysisTextBox.Location = new System.Drawing.Point(6, 50);
            this.selectedAnalysisTextBox.Name = "selectedAnalysisTextBox";
            this.selectedAnalysisTextBox.Size = new System.Drawing.Size(301, 20);
            this.selectedAnalysisTextBox.TabIndex = 3;
            // 
            // selectedAnalysisLabel
            // 
            this.selectedAnalysisLabel.AutoSize = true;
            this.selectedAnalysisLabel.Location = new System.Drawing.Point(3, 32);
            this.selectedAnalysisLabel.Name = "selectedAnalysisLabel";
            this.selectedAnalysisLabel.Size = new System.Drawing.Size(96, 13);
            this.selectedAnalysisLabel.TabIndex = 4;
            this.selectedAnalysisLabel.Text = "Selected Analysis: ";
            // 
            // availableSeriesLabel
            // 
            this.availableSeriesLabel.AutoSize = true;
            this.availableSeriesLabel.Location = new System.Drawing.Point(3, 75);
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
            this.splitContainer1.Panel1.Controls.Add(this.selectedSeriesListBox1);
            this.splitContainer1.Panel1.Controls.Add(this.selectAnalysisButton);
            this.splitContainer1.Panel1.Controls.Add(this.selectedAnalysisLabel);
            this.splitContainer1.Panel1.Controls.Add(this.availableSeriesLabel);
            this.splitContainer1.Panel1.Controls.Add(this.selectedAnalysisTextBox);
            this.splitContainer1.Panel1.Margin = new System.Windows.Forms.Padding(1);
            this.splitContainer1.Panel1MinSize = 250;
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.graphExplorerView1);
            this.splitContainer1.Panel2.Margin = new System.Windows.Forms.Padding(1);
            this.splitContainer1.Panel2MinSize = 250;
            this.splitContainer1.Size = new System.Drawing.Size(961, 597);
            this.splitContainer1.SplitterDistance = 319;
            this.splitContainer1.TabIndex = 6;
            // 
            // selectedSeriesListBox1
            // 
            this.selectedSeriesListBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.selectedSeriesListBox1.Location = new System.Drawing.Point(6, 92);
            this.selectedSeriesListBox1.Margin = new System.Windows.Forms.Padding(0);
            this.selectedSeriesListBox1.Name = "selectedSeriesListBox1";
            this.selectedSeriesListBox1.Size = new System.Drawing.Size(342, 495);
            this.selectedSeriesListBox1.TabIndex = 0;
            // 
            // DataAnalysis
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitContainer1);
            this.Name = "DataAnalysis";
            this.Size = new System.Drawing.Size(967, 600);
            ((System.ComponentModel.ISupportInitialize)(this.cropDatesDataSet1)).EndInit();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        public SelectedSeriesListBox selectedSeriesListBox1;
        private Reclamation.TimeSeries.Graphing.GraphExplorerView graphExplorerView1;
        private Reclamation.TimeSeries.AgriMet.CropDatesDataSet cropDatesDataSet1;
        private System.Windows.Forms.Button selectAnalysisButton;
        private System.Windows.Forms.TextBox selectedAnalysisTextBox;
        private System.Windows.Forms.Label selectedAnalysisLabel;
        private System.Windows.Forms.Label availableSeriesLabel;
        private System.Windows.Forms.SplitContainer splitContainer1;
    }
}
