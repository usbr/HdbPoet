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
	public class Upgrade : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Label labelVersion;
		private System.Windows.Forms.Button buttonOk;
        private LinkLabel linkLabel1;
        private RichTextBox richTextBox1;

        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.Container components = null;

		public Upgrade()
		{
			InitializeComponent();
            this.labelVersion.Text = "Version " + Application.ProductVersion;
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Upgrade));
            this.labelVersion = new System.Windows.Forms.Label();
            this.buttonOk = new System.Windows.Forms.Button();
            this.linkLabel1 = new System.Windows.Forms.LinkLabel();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.SuspendLayout();
            // 
            // labelVersion
            // 
            this.labelVersion.Location = new System.Drawing.Point(2, 5);
            this.labelVersion.Name = "labelVersion";
            this.labelVersion.Size = new System.Drawing.Size(337, 17);
            this.labelVersion.TabIndex = 0;
            this.labelVersion.Text = "version number here..";
            // 
            // buttonOk
            // 
            this.buttonOk.Location = new System.Drawing.Point(264, 105);
            this.buttonOk.Name = "buttonOk";
            this.buttonOk.Size = new System.Drawing.Size(75, 23);
            this.buttonOk.TabIndex = 1;
            this.buttonOk.Text = "Ok";
            this.buttonOk.Click += new System.EventHandler(this.buttonOk_Click);
            // 
            // linkLabel1
            // 
            this.linkLabel1.AutoSize = true;
            this.linkLabel1.Location = new System.Drawing.Point(2, 25);
            this.linkLabel1.Name = "linkLabel1";
            this.linkLabel1.Size = new System.Drawing.Size(100, 13);
            this.linkLabel1.TabIndex = 3;
            this.linkLabel1.TabStop = true;
            this.linkLabel1.Text = "Check for upgrades";
            this.linkLabel1.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel1_LinkClicked);
            // 
            // richTextBox1
            // 
            this.richTextBox1.BackColor = System.Drawing.SystemColors.Menu;
            this.richTextBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.richTextBox1.Location = new System.Drawing.Point(5, 44);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(334, 55);
            this.richTextBox1.TabIndex = 2;
            this.richTextBox1.Text = "- Installer files may be found on GitHub via the link above.\n- You will need an i" +
    "nternet connection to view and download the files.\n- Please close HDB Poet befor" +
    "e installing a more recent version.";
            // 
            // Upgrade
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(344, 139);
            this.Controls.Add(this.linkLabel1);
            this.Controls.Add(this.richTextBox1);
            this.Controls.Add(this.buttonOk);
            this.Controls.Add(this.labelVersion);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Upgrade";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Text = "Upgrade HDB Poet";
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
    }
}
