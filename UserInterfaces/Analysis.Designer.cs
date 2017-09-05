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
            this.selectedSeriesListBox1 = new HdbPoet.SelectedSeriesListBox();
            ((System.ComponentModel.ISupportInitialize)(this.cropDatesDataSet1)).BeginInit();
            this.SuspendLayout();
            // 
            // graphExplorerView1
            // 
            this.graphExplorerView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.graphExplorerView1.DataTable = null;
            this.graphExplorerView1.Location = new System.Drawing.Point(246, 3);
            this.graphExplorerView1.Messages = ((System.Collections.Generic.List<string>)(resources.GetObject("graphExplorerView1.Messages")));
            this.graphExplorerView1.Name = "graphExplorerView1";
            this.graphExplorerView1.Size = new System.Drawing.Size(718, 597);
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
            this.selectAnalysisButton.Location = new System.Drawing.Point(5, 5);
            this.selectAnalysisButton.Name = "selectAnalysisButton";
            this.selectAnalysisButton.Size = new System.Drawing.Size(235, 23);
            this.selectAnalysisButton.TabIndex = 2;
            this.selectAnalysisButton.Text = "Select Analysis";
            this.selectAnalysisButton.UseVisualStyleBackColor = true;
            this.selectAnalysisButton.Click += new System.EventHandler(this.DatabaseChanged);
            // 
            // selectedAnalysisTextBox
            // 
            this.selectedAnalysisTextBox.Location = new System.Drawing.Point(6, 57);
            this.selectedAnalysisTextBox.Name = "selectedAnalysisTextBox";
            this.selectedAnalysisTextBox.Size = new System.Drawing.Size(234, 20);
            this.selectedAnalysisTextBox.TabIndex = 3;
            // 
            // selectedAnalysisLabel
            // 
            this.selectedAnalysisLabel.AutoSize = true;
            this.selectedAnalysisLabel.Location = new System.Drawing.Point(3, 36);
            this.selectedAnalysisLabel.Name = "selectedAnalysisLabel";
            this.selectedAnalysisLabel.Size = new System.Drawing.Size(96, 13);
            this.selectedAnalysisLabel.TabIndex = 4;
            this.selectedAnalysisLabel.Text = "Selected Analysis: ";
            // 
            // selectedSeriesListBox1
            // 
            this.selectedSeriesListBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.selectedSeriesListBox1.Location = new System.Drawing.Point(5, 80);
            this.selectedSeriesListBox1.Margin = new System.Windows.Forms.Padding(0);
            this.selectedSeriesListBox1.Name = "selectedSeriesListBox1";
            this.selectedSeriesListBox1.Size = new System.Drawing.Size(273, 520);
            this.selectedSeriesListBox1.TabIndex = 0;
            // 
            // DataAnalysis
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.selectedAnalysisLabel);
            this.Controls.Add(this.selectedAnalysisTextBox);
            this.Controls.Add(this.selectAnalysisButton);
            this.Controls.Add(this.graphExplorerView1);
            this.Controls.Add(this.selectedSeriesListBox1);
            this.Name = "DataAnalysis";
            this.Size = new System.Drawing.Size(967, 600);
            ((System.ComponentModel.ISupportInitialize)(this.cropDatesDataSet1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private SelectedSeriesListBox selectedSeriesListBox1;
        private Reclamation.TimeSeries.Graphing.GraphExplorerView graphExplorerView1;
        private Reclamation.TimeSeries.AgriMet.CropDatesDataSet cropDatesDataSet1;
        private System.Windows.Forms.Button selectAnalysisButton;
        private System.Windows.Forms.TextBox selectedAnalysisTextBox;
        private System.Windows.Forms.Label selectedAnalysisLabel;
    }
}
