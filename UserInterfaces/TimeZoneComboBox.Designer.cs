namespace HdbPoet
{
    partial class TimeZoneComboBox
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
            this.comboBoxTimeZone = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // comboBoxTimeZone
            // 
            this.comboBoxTimeZone.Dock = System.Windows.Forms.DockStyle.Fill;
            this.comboBoxTimeZone.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxTimeZone.FormattingEnabled = true;
            this.comboBoxTimeZone.Items.AddRange(new object[] {
            "MST: Mountain Standard Time",
            "MDT: Mountain Daylight Time",
            "PST: Pacific Standard Time",
            "PDT: Pacific Daylight Time",
            "EST: Eastern Standard Time",
            "EDT: Eastern Daylight Time",
            "GMT: Greenwich Mean Time"});
            this.comboBoxTimeZone.Location = new System.Drawing.Point(0, 0);
            this.comboBoxTimeZone.Name = "comboBoxTimeZone";
            this.comboBoxTimeZone.Size = new System.Drawing.Size(251, 21);
            this.comboBoxTimeZone.TabIndex = 28;
            // 
            // TimeZoneComboBox
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.comboBoxTimeZone);
            this.Name = "TimeZoneComboBox";
            this.Size = new System.Drawing.Size(251, 21);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox comboBoxTimeZone;
    }
}
