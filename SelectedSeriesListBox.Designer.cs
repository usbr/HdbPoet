namespace HdbPoet
{
    partial class SelectedSeriesListBox
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SelectedSeriesListBox));
            this.listBox = new System.Windows.Forms.ListBox();
            this.buttonMoveGraphDown = new System.Windows.Forms.Button();
            this.buttonMoveGraphUp = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // listBox
            // 
            this.listBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listBox.HorizontalScrollbar = true;
            this.listBox.IntegralHeight = false;
            this.listBox.Location = new System.Drawing.Point(0, 0);
            this.listBox.Margin = new System.Windows.Forms.Padding(0);
            this.listBox.Name = "listBox";
            this.listBox.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.listBox.Size = new System.Drawing.Size(246, 234);
            this.listBox.TabIndex = 27;
            // 
            // buttonMoveGraphDown
            // 
            this.buttonMoveGraphDown.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonMoveGraphDown.BackColor = System.Drawing.Color.Silver;
            this.buttonMoveGraphDown.Image = ((System.Drawing.Image)(resources.GetObject("buttonMoveGraphDown.Image")));
            this.buttonMoveGraphDown.Location = new System.Drawing.Point(9, 159);
            this.buttonMoveGraphDown.Name = "buttonMoveGraphDown";
            this.buttonMoveGraphDown.Size = new System.Drawing.Size(24, 32);
            this.buttonMoveGraphDown.TabIndex = 32;
            this.buttonMoveGraphDown.UseVisualStyleBackColor = false;
            this.buttonMoveGraphDown.Click += new System.EventHandler(this.buttonMoveGraphDown_Click);
            // 
            // buttonMoveGraphUp
            // 
            this.buttonMoveGraphUp.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonMoveGraphUp.BackColor = System.Drawing.Color.Silver;
            this.buttonMoveGraphUp.Image = ((System.Drawing.Image)(resources.GetObject("buttonMoveGraphUp.Image")));
            this.buttonMoveGraphUp.Location = new System.Drawing.Point(9, 127);
            this.buttonMoveGraphUp.Name = "buttonMoveGraphUp";
            this.buttonMoveGraphUp.Size = new System.Drawing.Size(24, 32);
            this.buttonMoveGraphUp.TabIndex = 31;
            this.buttonMoveGraphUp.UseVisualStyleBackColor = false;
            this.buttonMoveGraphUp.Click += new System.EventHandler(this.buttonMoveGraphUp_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.buttonMoveGraphUp);
            this.panel1.Controls.Add(this.buttonMoveGraphDown);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel1.Location = new System.Drawing.Point(246, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(38, 234);
            this.panel1.TabIndex = 33;
            // 
            // SelectedSeriesListBox
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.listBox);
            this.Controls.Add(this.panel1);
            this.Margin = new System.Windows.Forms.Padding(0);
            this.Name = "SelectedSeriesListBox";
            this.Size = new System.Drawing.Size(284, 234);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button buttonMoveGraphDown;
        private System.Windows.Forms.Button buttonMoveGraphUp;
        private System.Windows.Forms.ListBox listBox;
        private System.Windows.Forms.Panel panel1;
    }
}
