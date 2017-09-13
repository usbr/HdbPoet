namespace HdbPoet.MetaData
{
    partial class RatingTableMain
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
            this.sqlViewEditor1 = new HdbPoet.MetaData.SqlViewEditor();
            this.SuspendLayout();
            // 
            // sqlViewEditor1
            // 
            this.sqlViewEditor1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.sqlViewEditor1.Location = new System.Drawing.Point(0, 0);
            this.sqlViewEditor1.Name = "sqlViewEditor1";
            this.sqlViewEditor1.Size = new System.Drawing.Size(651, 600);
            this.sqlViewEditor1.TabIndex = 0;
            // 
            // RatingTableMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.sqlViewEditor1);
            this.Name = "RatingTableMain";
            this.Size = new System.Drawing.Size(651, 600);
            this.ResumeLayout(false);

        }

        #endregion

        private SqlViewEditor sqlViewEditor1;

    }
}
