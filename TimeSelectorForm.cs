using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace HdbPoet
{
	/// <summary>
	/// Dialog window showing a time selector and a OK Button
	/// </summary>
	public class TimeSelectorForm : System.Windows.Forms.Form
	{
    private TimeSelector timeSelector1;
    private System.Windows.Forms.Button buttonOk;
    private System.Windows.Forms.Button buttonCancel;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

    public DateTime BeginningTime
    {
        get{ return this.timeSelector1.BeginingTime;}
        set{ this.timeSelector1.BeginingTime = value;}
    }
    public DateTime EndingTime
    {
      get{ return this.timeSelector1.EndingTime;}
      set{ this.timeSelector1.EndingTime = value;}
    }

		public TimeSelectorForm()
		{
			InitializeComponent();
			this.timeSelector1.Reset(5);
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
      this.timeSelector1 = new HdbPoet.TimeSelector();
      this.buttonOk = new System.Windows.Forms.Button();
      this.buttonCancel = new System.Windows.Forms.Button();
      this.SuspendLayout();
      // 
      // timeSelector1
      // 
      this.timeSelector1.BeginingTime = new System.DateTime(2003, 7, 10, 12, 34, 9, 921);
      this.timeSelector1.EndingTime = new System.DateTime(2003, 7, 10, 12, 34, 9, 921);
      this.timeSelector1.Location = new System.Drawing.Point(8, 8);
      this.timeSelector1.Name = "timeSelector1";
      this.timeSelector1.ShowTime = false;
      this.timeSelector1.Size = new System.Drawing.Size(192, 48);
      this.timeSelector1.TabIndex = 0;
      // 
      // buttonOk
      // 
      this.buttonOk.DialogResult = System.Windows.Forms.DialogResult.OK;
      this.buttonOk.Location = new System.Drawing.Point(208, 16);
      this.buttonOk.Name = "buttonOk";
      this.buttonOk.TabIndex = 1;
      this.buttonOk.Text = "Ok";
      this.buttonOk.Click += new System.EventHandler(this.buttonOk_Click);
      // 
      // buttonCancel
      // 
      this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
      this.buttonCancel.Location = new System.Drawing.Point(208, 48);
      this.buttonCancel.Name = "buttonCancel";
      this.buttonCancel.TabIndex = 2;
      this.buttonCancel.Text = "Cancel";
      this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
      // 
      // TimeSelectorForm
      // 
      this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
      this.ClientSize = new System.Drawing.Size(288, 78);
      this.ControlBox = false;
      this.Controls.Add(this.buttonCancel);
      this.Controls.Add(this.buttonOk);
      this.Controls.Add(this.timeSelector1);
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
      this.MinimizeBox = false;
      this.Name = "TimeSelectorForm";
      this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
      this.Text = "Dates for graph";
      this.ResumeLayout(false);

    }
		#endregion

    private void buttonOk_Click(object sender, System.EventArgs e)
    {
      Close();
    }
    private void buttonCancel_Click(object sender, System.EventArgs e)
    {
      Close();
    }

	}
}
