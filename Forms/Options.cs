using System;
using System.Drawing;
using System.Diagnostics;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace HdbPoet
{
	/// <summary>
	/// Dialog window that shows version of HDB-Poet.
	/// </summary>
	public class Options : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Button buttonOk;
        private Button buttonCancel;
        public CheckBox checkBoxShowEmptySdids;
        public CheckBox checkBoxInsertIntoRbase;
        private Label label1;
        private Button buttonLogIn;
        private Label label2;
        public CheckBox checkBoxShowBase;

        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.Container components = null;

		public Options()
		{
			InitializeComponent();
            
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if(components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            this.buttonOk = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.checkBoxShowEmptySdids = new System.Windows.Forms.CheckBox();
            this.checkBoxInsertIntoRbase = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.buttonLogIn = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.checkBoxShowBase = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // buttonOk
            // 
            this.buttonOk.Location = new System.Drawing.Point(261, 167);
            this.buttonOk.Name = "buttonOk";
            this.buttonOk.Size = new System.Drawing.Size(75, 23);
            this.buttonOk.TabIndex = 1;
            this.buttonOk.Text = "Ok";
            this.buttonOk.Click += new System.EventHandler(this.buttonOk_Click);
            // 
            // buttonCancel
            // 
            this.buttonCancel.Location = new System.Drawing.Point(180, 167);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(75, 23);
            this.buttonCancel.TabIndex = 2;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // checkBoxShowEmptySdids
            // 
            this.checkBoxShowEmptySdids.AutoSize = true;
            this.checkBoxShowEmptySdids.Location = new System.Drawing.Point(13, 13);
            this.checkBoxShowEmptySdids.Name = "checkBoxShowEmptySdids";
            this.checkBoxShowEmptySdids.Size = new System.Drawing.Size(164, 17);
            this.checkBoxShowEmptySdids.TabIndex = 3;
            this.checkBoxShowEmptySdids.Text = "Show empty SDIDs in search";
            this.checkBoxShowEmptySdids.UseVisualStyleBackColor = true; 
            this.checkBoxShowEmptySdids.Checked = GlobalVariables.showEmptySdids;
            this.checkBoxShowEmptySdids.CheckedChanged += new System.EventHandler(this.showEmptySdid_CheckedChanged);
            // 
            // checkBoxInsertIntoRbase
            // 
            this.checkBoxInsertIntoRbase.AutoSize = true;
            this.checkBoxInsertIntoRbase.Location = new System.Drawing.Point(13, 59);
            this.checkBoxInsertIntoRbase.Name = "checkBoxInsertIntoRbase";
            this.checkBoxInsertIntoRbase.Size = new System.Drawing.Size(150, 17);
            this.checkBoxInsertIntoRbase.TabIndex = 4;
            this.checkBoxInsertIntoRbase.Text = "Insert into R-Base on write";
            this.checkBoxInsertIntoRbase.UseVisualStyleBackColor = true;
            this.checkBoxInsertIntoRbase.Checked = GlobalVariables.insertOnWrite;
            this.checkBoxInsertIntoRbase.CheckedChanged += new System.EventHandler(this.insertOnWrite_CheckedChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(28, 79);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(267, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "This allows historical changes to be archived in R-Base";
            // 
            // buttonLogIn
            // 
            this.buttonLogIn.Location = new System.Drawing.Point(13, 100);
            this.buttonLogIn.Name = "buttonLogIn";
            this.buttonLogIn.Size = new System.Drawing.Size(86, 23);
            this.buttonLogIn.TabIndex = 6;
            this.buttonLogIn.Text = "HDB Log-In";
            this.buttonLogIn.UseVisualStyleBackColor = true;
            this.buttonLogIn.Click += new System.EventHandler(this.openNewPoetInstance);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(105, 105);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(125, 13);
            this.label2.TabIndex = 7;
            this.label2.Text = "Log-In to a different HDB";
            // 
            // checkBoxShowBase
            // 
            this.checkBoxShowBase.AutoSize = true;
            this.checkBoxShowBase.Location = new System.Drawing.Point(13, 36);
            this.checkBoxShowBase.Name = "checkBoxShowBase";
            this.checkBoxShowBase.Size = new System.Drawing.Size(103, 17);
            this.checkBoxShowBase.TabIndex = 8;
            this.checkBoxShowBase.Text = "Show base data";
            this.checkBoxShowBase.UseVisualStyleBackColor = true;
            this.checkBoxShowBase.Checked = GlobalVariables.showBaseData;
            this.checkBoxShowBase.CheckedChanged += new System.EventHandler(this.showBaseData_CheckedChanged);
            // 
            // Options
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(348, 202);
            this.Controls.Add(this.checkBoxShowBase);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.buttonLogIn);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.checkBoxInsertIntoRbase);
            this.Controls.Add(this.checkBoxShowEmptySdids);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonOk);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Options";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Text = "HDB POET Advanced Options";
            this.ResumeLayout(false);
            this.PerformLayout();

    }
		#endregion

		private void buttonOk_Click(object sender, System.EventArgs e)
		{
			Close();
		}

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            // Send the URL to the operating system.
            Process.Start(@"https://github.com/usbr/HdbPoet/releases" as string);
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void showEmptySdid_CheckedChanged(object sender, EventArgs e)
        {
            GlobalVariables.showEmptySdids = this.checkBoxShowEmptySdids.Checked;
        }

        private void showBaseData_CheckedChanged(object sender, EventArgs e)
        {
            GlobalVariables.showBaseData = this.checkBoxShowBase.Checked;
        }

        private void insertOnWrite_CheckedChanged(object sender, EventArgs e)
        {
            GlobalVariables.insertOnWrite = this.checkBoxInsertIntoRbase.Checked;
        }

        private void openNewPoetInstance(object sender, EventArgs e)
        {
            Application.Restart();
        }

    }
}
