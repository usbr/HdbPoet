using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace HdbPoet
{
	/// <summary>
	/// Summary description for StringInputForm.
	/// </summary>
	public class StringInputForm : System.Windows.Forms.Form
	{
		private System.Windows.Forms.TextBox textBoxInput;
		private System.Windows.Forms.Button buttonCancel;
		private System.Windows.Forms.Button buttonOk;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public StringInputForm()
		{
			InitializeComponent();
		}

		/// <summary>
		/// removes invalid filename characters.
		/// </summary>
		public void CleanTextForFilename()
		{
			string txt = this.textBoxInput.Text;
			txt = txt.Replace("\\","_");
			txt = txt.Replace("/","_");
			txt = txt.Replace(":","_");
			txt = txt.Replace("*","_");
			txt = txt.Replace("?","_");
			txt = txt.Replace("\"","_");
			txt = txt.Replace("<","_");
			txt = txt.Replace(">","_");
			txt = txt.Replace("|","_");
		 this.textBoxInput.Text = txt;
		}

		public string InputText
		{
			get { return this.textBoxInput.Text;}
			set { this.textBoxInput.Text = value;}
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
			this.textBoxInput = new System.Windows.Forms.TextBox();
			this.buttonCancel = new System.Windows.Forms.Button();
			this.buttonOk = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// textBoxInput
			// 
			this.textBoxInput.Location = new System.Drawing.Point(8, 48);
			this.textBoxInput.Name = "textBoxInput";
			this.textBoxInput.Size = new System.Drawing.Size(280, 20);
			this.textBoxInput.TabIndex = 0;
			this.textBoxInput.Text = "";
			// 
			// buttonCancel
			// 
			this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.buttonCancel.Location = new System.Drawing.Point(224, 136);
			this.buttonCancel.Name = "buttonCancel";
			this.buttonCancel.TabIndex = 1;
			this.buttonCancel.Text = "Cancel";
			this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
			// 
			// buttonOk
			// 
			this.buttonOk.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.buttonOk.Location = new System.Drawing.Point(224, 104);
			this.buttonOk.Name = "buttonOk";
			this.buttonOk.TabIndex = 2;
			this.buttonOk.Text = "Ok";
			this.buttonOk.Click += new System.EventHandler(this.buttonOk_Click);
			// 
			// StringInputForm
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(304, 166);
			this.Controls.AddRange(new System.Windows.Forms.Control[] {
																		  this.buttonOk,
																		  this.buttonCancel,
																		  this.textBoxInput});
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.Name = "StringInputForm";
			this.ResumeLayout(false);

		}
		#endregion

		private void buttonCancel_Click(object sender, System.EventArgs e)
		{
			Close();
		}

		private void buttonOk_Click(object sender, System.EventArgs e)
		{
			Close();
		}
	}
}
