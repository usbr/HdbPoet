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
        private Button buttonObjectList;
        private Label label3;
        private CheckBox checkBoxHideGraph;
        public CheckBox checkBoxShowSimpleSdiInfo;
        private ComboBox comboBoxDbSiteCode;
        private Label label4;
        private Label label5;
        private ComboBox comboBoxAgencyCodes;

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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Options));
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
            this.buttonObjectList = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.checkBoxHideGraph = new System.Windows.Forms.CheckBox();
            this.checkBoxShowSimpleSdiInfo = new System.Windows.Forms.CheckBox();
            this.comboBoxDbSiteCode = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.comboBoxAgencyCodes = new System.Windows.Forms.ComboBox();
            this.validationGroupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // buttonOk
            // 
            this.buttonOk.Location = new System.Drawing.Point(322, 497);
            this.buttonOk.Name = "buttonOk";
            this.buttonOk.Size = new System.Drawing.Size(90, 27);
            this.buttonOk.TabIndex = 1;
            this.buttonOk.Text = "OK";
            this.buttonOk.Click += new System.EventHandler(this.buttonOk_Click);
            // 
            // checkBoxShowEmptySdids
            // 
            this.checkBoxShowEmptySdids.AutoSize = true;
            this.checkBoxShowEmptySdids.Checked = true;
            this.checkBoxShowEmptySdids.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxShowEmptySdids.Location = new System.Drawing.Point(16, 15);
            this.checkBoxShowEmptySdids.Name = "checkBoxShowEmptySdids";
            this.checkBoxShowEmptySdids.Size = new System.Drawing.Size(201, 21);
            this.checkBoxShowEmptySdids.TabIndex = 3;
            this.checkBoxShowEmptySdids.Text = "Show empty SDIs in search";
            this.checkBoxShowEmptySdids.UseVisualStyleBackColor = true;
            this.checkBoxShowEmptySdids.CheckedChanged += new System.EventHandler(this.showEmptySdid_CheckedChanged);
            // 
            // checkBoxInsertIntoRbase
            // 
            this.checkBoxInsertIntoRbase.AutoSize = true;
            this.checkBoxInsertIntoRbase.Location = new System.Drawing.Point(16, 95);
            this.checkBoxInsertIntoRbase.Name = "checkBoxInsertIntoRbase";
            this.checkBoxInsertIntoRbase.Size = new System.Drawing.Size(196, 21);
            this.checkBoxInsertIntoRbase.TabIndex = 4;
            this.checkBoxInsertIntoRbase.Text = "Insert into R-Base on write";
            this.checkBoxInsertIntoRbase.UseVisualStyleBackColor = true;
            this.checkBoxInsertIntoRbase.CheckedChanged += new System.EventHandler(this.insertOnWrite_CheckedChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(34, 118);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(355, 17);
            this.label1.TabIndex = 5;
            this.label1.Text = "This allows historical changes to be archived in R-Base";
            // 
            // buttonLogIn
            // 
            this.buttonLogIn.Location = new System.Drawing.Point(16, 497);
            this.buttonLogIn.Name = "buttonLogIn";
            this.buttonLogIn.Size = new System.Drawing.Size(103, 27);
            this.buttonLogIn.TabIndex = 6;
            this.buttonLogIn.Text = "HDB Log-In";
            this.buttonLogIn.UseVisualStyleBackColor = true;
            this.buttonLogIn.Click += new System.EventHandler(this.openNewPoetInstance);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(120, 503);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(165, 17);
            this.label2.TabIndex = 7;
            this.label2.Text = "Log-In to a different HDB";
            // 
            // checkBoxShowBase
            // 
            this.checkBoxShowBase.AutoSize = true;
            this.checkBoxShowBase.Location = new System.Drawing.Point(16, 69);
            this.checkBoxShowBase.Name = "checkBoxShowBase";
            this.checkBoxShowBase.Size = new System.Drawing.Size(131, 21);
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
            this.validationGroupBox.Location = new System.Drawing.Point(16, 194);
            this.validationGroupBox.Name = "validationGroupBox";
            this.validationGroupBox.Size = new System.Drawing.Size(389, 84);
            this.validationGroupBox.TabIndex = 9;
            this.validationGroupBox.TabStop = false;
            this.validationGroupBox.Text = "Set Validation Flag on write";
            // 
            // eValidationRadioButton
            // 
            this.eValidationRadioButton.AutoSize = true;
            this.eValidationRadioButton.Location = new System.Drawing.Point(133, 48);
            this.eValidationRadioButton.Name = "eValidationRadioButton";
            this.eValidationRadioButton.Size = new System.Drawing.Size(91, 21);
            this.eValidationRadioButton.TabIndex = 4;
            this.eValidationRadioButton.Text = "Edited (e)";
            this.eValidationRadioButton.UseVisualStyleBackColor = true;
            this.eValidationRadioButton.CheckedChanged += new System.EventHandler(this.validationGroupBox_CheckedChanged);
            // 
            // tValidationRadioButton
            // 
            this.tValidationRadioButton.AutoSize = true;
            this.tValidationRadioButton.Location = new System.Drawing.Point(24, 48);
            this.tValidationRadioButton.Name = "tValidationRadioButton";
            this.tValidationRadioButton.Size = new System.Drawing.Size(88, 21);
            this.tValidationRadioButton.TabIndex = 3;
            this.tValidationRadioButton.Text = "Temp (T)";
            this.tValidationRadioButton.UseVisualStyleBackColor = true;
            this.tValidationRadioButton.CheckedChanged += new System.EventHandler(this.validationGroupBox_CheckedChanged);
            // 
            // zValidationRadioButton
            // 
            this.zValidationRadioButton.AutoSize = true;
            this.zValidationRadioButton.Checked = true;
            this.zValidationRadioButton.Location = new System.Drawing.Point(251, 22);
            this.zValidationRadioButton.Name = "zValidationRadioButton";
            this.zValidationRadioButton.Size = new System.Drawing.Size(97, 21);
            this.zValidationRadioButton.TabIndex = 2;
            this.zValidationRadioButton.TabStop = true;
            this.zValidationRadioButton.Text = "Default (Z)";
            this.zValidationRadioButton.UseVisualStyleBackColor = true;
            this.zValidationRadioButton.CheckedChanged += new System.EventHandler(this.validationGroupBox_CheckedChanged);
            // 
            // pValidationRadioButton
            // 
            this.pValidationRadioButton.AutoSize = true;
            this.pValidationRadioButton.Location = new System.Drawing.Point(133, 22);
            this.pValidationRadioButton.Name = "pValidationRadioButton";
            this.pValidationRadioButton.Size = new System.Drawing.Size(121, 21);
            this.pValidationRadioButton.TabIndex = 1;
            this.pValidationRadioButton.Text = "Provisional (P)";
            this.pValidationRadioButton.UseVisualStyleBackColor = true;
            this.pValidationRadioButton.CheckedChanged += new System.EventHandler(this.validationGroupBox_CheckedChanged);
            // 
            // vValidationRadioButton
            // 
            this.vValidationRadioButton.AutoSize = true;
            this.vValidationRadioButton.Location = new System.Drawing.Point(24, 22);
            this.vValidationRadioButton.Name = "vValidationRadioButton";
            this.vValidationRadioButton.Size = new System.Drawing.Size(111, 21);
            this.vValidationRadioButton.TabIndex = 0;
            this.vValidationRadioButton.Text = "Validated (V)";
            this.vValidationRadioButton.UseVisualStyleBackColor = true;
            this.vValidationRadioButton.CheckedChanged += new System.EventHandler(this.validationGroupBox_CheckedChanged);
            // 
            // checkBoxSendOverwrite
            // 
            this.checkBoxSendOverwrite.AutoSize = true;
            this.checkBoxSendOverwrite.Location = new System.Drawing.Point(16, 139);
            this.checkBoxSendOverwrite.Name = "checkBoxSendOverwrite";
            this.checkBoxSendOverwrite.Size = new System.Drawing.Size(211, 21);
            this.checkBoxSendOverwrite.TabIndex = 10;
            this.checkBoxSendOverwrite.Text = "Send Overwrite Flag on write";
            this.checkBoxSendOverwrite.UseVisualStyleBackColor = true;
            this.checkBoxSendOverwrite.CheckedChanged += new System.EventHandler(this.checkBoxSendOverwrite_CheckedChanged);
            // 
            // buttonObjectList
            // 
            this.buttonObjectList.Location = new System.Drawing.Point(16, 464);
            this.buttonObjectList.Name = "buttonObjectList";
            this.buttonObjectList.Size = new System.Drawing.Size(103, 26);
            this.buttonObjectList.TabIndex = 11;
            this.buttonObjectList.Text = "Object List";
            this.buttonObjectList.UseVisualStyleBackColor = true;
            this.buttonObjectList.Click += new System.EventHandler(this.openObjectTypeUI);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(120, 470);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(177, 17);
            this.label3.TabIndex = 12;
            this.label3.Text = "Edit available HDB Objects";
            // 
            // checkBoxHideGraph
            // 
            this.checkBoxHideGraph.AutoSize = true;
            this.checkBoxHideGraph.Location = new System.Drawing.Point(16, 167);
            this.checkBoxHideGraph.Name = "checkBoxHideGraph";
            this.checkBoxHideGraph.Size = new System.Drawing.Size(250, 21);
            this.checkBoxHideGraph.TabIndex = 13;
            this.checkBoxHideGraph.Text = "Hide graph on Table tab by default";
            this.checkBoxHideGraph.UseVisualStyleBackColor = true;
            this.checkBoxHideGraph.CheckedChanged += new System.EventHandler(this.checkBoxHideGraph_CheckedChanged);
            // 
            // checkBoxShowSimpleSdiInfo
            // 
            this.checkBoxShowSimpleSdiInfo.AutoSize = true;
            this.checkBoxShowSimpleSdiInfo.Checked = true;
            this.checkBoxShowSimpleSdiInfo.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxShowSimpleSdiInfo.Location = new System.Drawing.Point(16, 42);
            this.checkBoxShowSimpleSdiInfo.Name = "checkBoxShowSimpleSdiInfo";
            this.checkBoxShowSimpleSdiInfo.Size = new System.Drawing.Size(154, 21);
            this.checkBoxShowSimpleSdiInfo.TabIndex = 14;
            this.checkBoxShowSimpleSdiInfo.Text = "Show basic SDI Info";
            this.checkBoxShowSimpleSdiInfo.UseVisualStyleBackColor = true;
            this.checkBoxShowSimpleSdiInfo.CheckedChanged += new System.EventHandler(this.checkBoxShowSimpleSdiInfo_CheckedChanged);
            // 
            // comboBoxDbSiteCode
            // 
            this.comboBoxDbSiteCode.FormattingEnabled = true;
            this.comboBoxDbSiteCode.Location = new System.Drawing.Point(154, 283);
            this.comboBoxDbSiteCode.Name = "comboBoxDbSiteCode";
            this.comboBoxDbSiteCode.Size = new System.Drawing.Size(95, 24);
            this.comboBoxDbSiteCode.TabIndex = 15;
            this.comboBoxDbSiteCode.SelectedIndexChanged += new System.EventHandler(this.comboBoxDbSiteCode_SelectedIndexChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(13, 286);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(127, 17);
            this.label4.TabIndex = 16;
            this.label4.Text = "DB Site Code Filter";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(13, 318);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(147, 17);
            this.label5.TabIndex = 18;
            this.label5.Text = "HDB Agency Override";
            // 
            // comboBoxAgencyCodes
            // 
            this.comboBoxAgencyCodes.FormattingEnabled = true;
            this.comboBoxAgencyCodes.Location = new System.Drawing.Point(175, 315);
            this.comboBoxAgencyCodes.Name = "comboBoxAgencyCodes";
            this.comboBoxAgencyCodes.Size = new System.Drawing.Size(230, 24);
            this.comboBoxAgencyCodes.TabIndex = 17;
            // 
            // Options
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(6, 15);
            this.ClientSize = new System.Drawing.Size(423, 535);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.comboBoxAgencyCodes);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.comboBoxDbSiteCode);
            this.Controls.Add(this.checkBoxShowSimpleSdiInfo);
            this.Controls.Add(this.checkBoxHideGraph);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.buttonObjectList);
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
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
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
            this.checkBoxHideGraph.Checked = GlobalVariables.tableGraphHide;
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
            this.comboBoxDbSiteCode.Items.Clear();
            this.comboBoxDbSiteCode.Items.AddRange(GlobalVariables.dbSiteCodeOptions.ToArray());
            this.comboBoxDbSiteCode.SelectedIndex = this.comboBoxDbSiteCode.FindStringExact(GlobalVariables.dbSiteCode);
            this.comboBoxAgencyCodes.Items.AddRange(GlobalVariables.dbAgencyCodeOptions.ToArray());
            this.comboBoxAgencyCodes.SelectedIndex = this.comboBoxAgencyCodes.FindStringExact(GlobalVariables.dbAgencyCode);
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

        private void checkBoxShowSimpleSdiInfo_CheckedChanged(object sender, EventArgs e)
        {
            GlobalVariables.showSimpleSdiInfo = this.checkBoxShowSimpleSdiInfo.Checked;
        }

        private void comboBoxDbSiteCode_SelectedIndexChanged(object sender, EventArgs e)
        {
            GlobalVariables.dbSiteCode = this.comboBoxDbSiteCode.SelectedItem.ToString();
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

        private void checkBoxHideGraph_CheckedChanged(object sender, EventArgs e)
        {
            GlobalVariables.tableGraphHide = this.checkBoxHideGraph.Checked;
        }

        private void openNewPoetInstance(object sender, EventArgs e)
        {
            Application.Restart();
        }

        private void openObjectTypeUI(object sender, EventArgs e)
        {
            var dlg = new SelectObjectTypes();
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                MessageBox.Show("Restart HDB POET for changes to take effect...", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
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
