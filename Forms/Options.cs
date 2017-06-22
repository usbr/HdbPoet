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
        public CheckBox checkBoxShowEmptySdids;
        public CheckBox checkBoxInsertIntoRbase;
        private Label label1;
        private Button buttonLogIn;
        private Label label2;
        public CheckBox checkBoxShowBase;
        private GroupBox validationGroupBox;
        private RadioButton eValidationRadioButton;
        private RadioButton tValidationRadioButton;
        private RadioButton zValidationRadioButton;
        private RadioButton pValidationRadioButton;
        private RadioButton vValidationRadioButton;
        public CheckBox checkBoxSendOverwrite;

        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.Container components = null;

		public Options()
		{
			InitializeComponent();
            readOptionSettings();            
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
            this.checkBoxShowEmptySdids = new System.Windows.Forms.CheckBox();
            this.checkBoxInsertIntoRbase = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.buttonLogIn = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.checkBoxShowBase = new System.Windows.Forms.CheckBox();
            this.validationGroupBox = new System.Windows.Forms.GroupBox();
            this.eValidationRadioButton = new System.Windows.Forms.RadioButton();
            this.tValidationRadioButton = new System.Windows.Forms.RadioButton();
            this.zValidationRadioButton = new System.Windows.Forms.RadioButton();
            this.pValidationRadioButton = new System.Windows.Forms.RadioButton();
            this.vValidationRadioButton = new System.Windows.Forms.RadioButton();
            this.checkBoxSendOverwrite = new System.Windows.Forms.CheckBox();
            this.validationGroupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // buttonOk
            // 
            this.buttonOk.Location = new System.Drawing.Point(260, 261);
            this.buttonOk.Name = "buttonOk";
            this.buttonOk.Size = new System.Drawing.Size(75, 23);
            this.buttonOk.TabIndex = 1;
            this.buttonOk.Text = "Ok";
            this.buttonOk.Click += new System.EventHandler(this.buttonOk_Click);
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
            this.buttonLogIn.Location = new System.Drawing.Point(12, 231);
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
            this.label2.Location = new System.Drawing.Point(104, 236);
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
            this.checkBoxShowBase.CheckedChanged += new System.EventHandler(this.showBaseData_CheckedChanged);
            // 
            // validationGroupBox
            // 
            this.validationGroupBox.Controls.Add(this.eValidationRadioButton);
            this.validationGroupBox.Controls.Add(this.tValidationRadioButton);
            this.validationGroupBox.Controls.Add(this.zValidationRadioButton);
            this.validationGroupBox.Controls.Add(this.pValidationRadioButton);
            this.validationGroupBox.Controls.Add(this.vValidationRadioButton);
            this.validationGroupBox.Location = new System.Drawing.Point(11, 119);
            this.validationGroupBox.Name = "validationGroupBox";
            this.validationGroupBox.Size = new System.Drawing.Size(324, 73);
            this.validationGroupBox.TabIndex = 9;
            this.validationGroupBox.TabStop = false;
            this.validationGroupBox.Text = "Set Validation Flag on write";
            // 
            // eValidationRadioButton
            // 
            this.eValidationRadioButton.AutoSize = true;
            this.eValidationRadioButton.Location = new System.Drawing.Point(111, 42);
            this.eValidationRadioButton.Name = "eValidationRadioButton";
            this.eValidationRadioButton.Size = new System.Drawing.Size(70, 17);
            this.eValidationRadioButton.TabIndex = 4;
            this.eValidationRadioButton.Text = "Edited (e)";
            this.eValidationRadioButton.UseVisualStyleBackColor = true;
            this.eValidationRadioButton.CheckedChanged += new System.EventHandler(this.validationGroupBox_CheckedChanged);
            // 
            // tValidationRadioButton
            // 
            this.tValidationRadioButton.AutoSize = true;
            this.tValidationRadioButton.Location = new System.Drawing.Point(20, 42);
            this.tValidationRadioButton.Name = "tValidationRadioButton";
            this.tValidationRadioButton.Size = new System.Drawing.Size(68, 17);
            this.tValidationRadioButton.TabIndex = 3;
            this.tValidationRadioButton.Text = "Temp (T)";
            this.tValidationRadioButton.UseVisualStyleBackColor = true;
            this.tValidationRadioButton.CheckedChanged += new System.EventHandler(this.validationGroupBox_CheckedChanged);
            // 
            // zValidationRadioButton
            // 
            this.zValidationRadioButton.AutoSize = true;
            this.zValidationRadioButton.Checked = true;
            this.zValidationRadioButton.Location = new System.Drawing.Point(209, 19);
            this.zValidationRadioButton.Name = "zValidationRadioButton";
            this.zValidationRadioButton.Size = new System.Drawing.Size(59, 17);
            this.zValidationRadioButton.TabIndex = 2;
            this.zValidationRadioButton.TabStop = true;
            this.zValidationRadioButton.Text = "Default";
            this.zValidationRadioButton.UseVisualStyleBackColor = true;
            this.zValidationRadioButton.CheckedChanged += new System.EventHandler(this.validationGroupBox_CheckedChanged);
            // 
            // pValidationRadioButton
            // 
            this.pValidationRadioButton.AutoSize = true;
            this.pValidationRadioButton.Location = new System.Drawing.Point(111, 19);
            this.pValidationRadioButton.Name = "pValidationRadioButton";
            this.pValidationRadioButton.Size = new System.Drawing.Size(92, 17);
            this.pValidationRadioButton.TabIndex = 1;
            this.pValidationRadioButton.Text = "Provisional (P)";
            this.pValidationRadioButton.UseVisualStyleBackColor = true;
            this.pValidationRadioButton.CheckedChanged += new System.EventHandler(this.validationGroupBox_CheckedChanged);
            // 
            // vValidationRadioButton
            // 
            this.vValidationRadioButton.AutoSize = true;
            this.vValidationRadioButton.Location = new System.Drawing.Point(20, 19);
            this.vValidationRadioButton.Name = "vValidationRadioButton";
            this.vValidationRadioButton.Size = new System.Drawing.Size(85, 17);
            this.vValidationRadioButton.TabIndex = 0;
            this.vValidationRadioButton.Text = "Validated (V)";
            this.vValidationRadioButton.UseVisualStyleBackColor = true;
            this.vValidationRadioButton.CheckedChanged += new System.EventHandler(this.validationGroupBox_CheckedChanged);
            // 
            // checkBoxSendOverwrite
            // 
            this.checkBoxSendOverwrite.AutoSize = true;
            this.checkBoxSendOverwrite.Location = new System.Drawing.Point(13, 97);
            this.checkBoxSendOverwrite.Name = "checkBoxSendOverwrite";
            this.checkBoxSendOverwrite.Size = new System.Drawing.Size(162, 17);
            this.checkBoxSendOverwrite.TabIndex = 10;
            this.checkBoxSendOverwrite.Text = "Send Overwrite Flag on write";
            this.checkBoxSendOverwrite.UseVisualStyleBackColor = true;
            this.checkBoxSendOverwrite.CheckedChanged += new System.EventHandler(this.checkBoxSendOverwrite_CheckedChanged);
            // 
            // Options
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(348, 296);
            this.Controls.Add(this.checkBoxSendOverwrite);
            this.Controls.Add(this.validationGroupBox);
            this.Controls.Add(this.checkBoxShowBase);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.buttonLogIn);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.checkBoxInsertIntoRbase);
            this.Controls.Add(this.checkBoxShowEmptySdids);
            this.Controls.Add(this.buttonOk);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Options";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Text = "HDB POET Advanced Options";
            this.validationGroupBox.ResumeLayout(false);
            this.validationGroupBox.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

    }
		#endregion

        private void readOptionSettings()
        {
            this.checkBoxShowEmptySdids.Checked = GlobalVariables.showEmptySdids;
            this.checkBoxShowBase.Checked = GlobalVariables.showBaseData;
            this.checkBoxInsertIntoRbase.Checked = GlobalVariables.insertOnWrite;
            this.checkBoxSendOverwrite.Checked = GlobalVariables.overwriteOnWrite;
            switch (GlobalVariables.writeValidationFlag)
            {
                case 'V':
                    this.vValidationRadioButton.Checked = true;
                    break;
                case 'P':
                    this.pValidationRadioButton.Checked = true;
                    break;
                case 'T':
                    this.tValidationRadioButton.Checked = true;
                    break;
                case 'e':
                    this.eValidationRadioButton.Checked = true;
                    break;
                default:
                    this.zValidationRadioButton.Checked = true;
                    break;
            }
        }

		private void buttonOk_Click(object sender, System.EventArgs e)
		{
			Close();
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

        private void checkBoxSendOverwrite_CheckedChanged(object sender, EventArgs e)
        {
            GlobalVariables.overwriteOnWrite = this.checkBoxSendOverwrite.Checked;
        }

        private void openNewPoetInstance(object sender, EventArgs e)
        {
            Application.Restart();
        }

        private void validationGroupBox_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton radioButton = sender as RadioButton;

            if (vValidationRadioButton.Checked)
            {
                GlobalVariables.writeValidationFlag = 'V';
            }
            else if (pValidationRadioButton.Checked)
            {
                GlobalVariables.writeValidationFlag = 'P';
            }
            else if (eValidationRadioButton.Checked)
            {
                GlobalVariables.writeValidationFlag = 'e';
            }
            else if (tValidationRadioButton.Checked)
            {
                GlobalVariables.writeValidationFlag = 'T';
            }
            else
            {
                GlobalVariables.writeValidationFlag = 'Z';
            }
        }

    }
}
