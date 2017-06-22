using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;

namespace HdbPoet
{
  /// <summary>
  /// Summary description for TimeSelector.
  /// </summary>
  public class TimeSelector : System.Windows.Forms.UserControl
  {
    private System.Windows.Forms.Label label2;
    private System.Windows.Forms.DateTimePicker dateTimePicker2;
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.DateTimePicker dateTimePicker1;
    private System.Windows.Forms.MenuItem menuItemPeroidOfRecord;

    /// <summary> 
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.Container components = null;

    bool showTime;

    public TimeSelector()
    {
      InitializeComponent();
      ShowTime = false;
    }

    static string TimeFormat = "MM/dd/yyyy HH:mm";
    static string DateFormat = "MM/dd/yyyy";
    public bool ShowTime
    {
      get
      {
        return (showTime);
      }
      set
      {
        showTime = value;

        if( value)
        {
          this.dateTimePicker1.CustomFormat =TimeFormat;
          this.dateTimePicker2.CustomFormat =TimeFormat;
        }
        else
        {
          this.dateTimePicker1.CustomFormat =DateFormat;
          this.dateTimePicker2.CustomFormat = DateFormat;
        }
      }
    }

    public DateTime BeginingTime
    {
      get { return dateTimePicker1.Value;}
      set { dateTimePicker1.Value = value;}
    }
    public DateTime EndingTime
    {
      get { return dateTimePicker2.Value;}
      set { dateTimePicker2.Value = value;}
    }
		


    /// <summary>
    /// rounds dates set in time pickers 
    /// so the beginning date starts at midnight
    /// and the ending date ends at 1 second before midnight.
    /// </summary>
    public void SetInclusiveDates()
    {
      // set the date to the beginning of the day (midnight)
      DateTime time = dateTimePicker1.Value;
      time = new DateTime(time.Year,time.Month,time.Day,0,0,0);
      dateTimePicker1.Value = time;

      //set the date to the end of the day. (just before midnight)
      time = dateTimePicker2.Value;
      time = new DateTime(time.Year,time.Month,time.Day,23,59,59);
      dateTimePicker2.Value = time;
		

    }

    public void Reset(int days) // number of days difference between time selectors
    {
      DateTime time = DateTime.Now.AddMinutes(5); //DateTime.Now;
      DateTime t2 = time; //sybase.ServerTime; //Now;
      t2 = t2.AddDays(-days).AddMinutes(-5);;
      //t2 = new DateTime(t2.Year,t2.Month,t2.Day,time.Hour,time.Minute,time.Second);
      this.dateTimePicker1.Value = t2; 
      this.dateTimePicker2.Value = time; 
      //MessageBox.Show(" begin:   "+t2.ToString()+"\n   end: "+time.ToString());
    }
    public void Reset()// 48 hours default
    {
      Reset(2);		
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

    #region Component Designer generated code
    /// <summary> 
    /// Required method for Designer support - do not modify 
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
      this.label2 = new System.Windows.Forms.Label();
      this.dateTimePicker2 = new System.Windows.Forms.DateTimePicker();
      this.label1 = new System.Windows.Forms.Label();
      this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
      this.menuItemPeroidOfRecord = new System.Windows.Forms.MenuItem();
      this.SuspendLayout();
      // 
      // label2
      // 
      this.label2.Location = new System.Drawing.Point(8, 24);
      this.label2.Name = "label2";
      this.label2.Size = new System.Drawing.Size(24, 16);
      this.label2.TabIndex = 24;
      this.label2.Text = "End";
      // 
      // dateTimePicker2
      // 
      this.dateTimePicker2.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
      this.dateTimePicker2.Location = new System.Drawing.Point(56, 24);
      this.dateTimePicker2.Name = "dateTimePicker2";
      this.dateTimePicker2.Size = new System.Drawing.Size(120, 20);
      this.dateTimePicker2.TabIndex = 23;
      // 
      // label1
      // 
      this.label1.Location = new System.Drawing.Point(8, 8);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(40, 16);
      this.label1.TabIndex = 22;
      this.label1.Text = "Start";
      // 
      // dateTimePicker1
      // 
      this.dateTimePicker1.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
      this.dateTimePicker1.Location = new System.Drawing.Point(56, 2);
      this.dateTimePicker1.Name = "dateTimePicker1";
      this.dateTimePicker1.Size = new System.Drawing.Size(120, 20);
      this.dateTimePicker1.TabIndex = 21;
      // 
      // menuItemPeroidOfRecord
      // 
      this.menuItemPeroidOfRecord.Index = -1;
      this.menuItemPeroidOfRecord.Text = "&Lookup period of record";
      this.menuItemPeroidOfRecord.Visible = false;
      this.menuItemPeroidOfRecord.Click += new System.EventHandler(this.menuItem1_Click_1);
      // 
      // TimeSelector
      // 
      this.Controls.Add(this.label2);
      this.Controls.Add(this.dateTimePicker2);
      this.Controls.Add(this.label1);
      this.Controls.Add(this.dateTimePicker1);
      this.Name = "TimeSelector";
      this.Size = new System.Drawing.Size(184, 40);
      this.ResumeLayout(false);

    }
    #endregion


    private void menuItem1_Click_1(object sender, System.EventArgs e)
    {
      /*
        string siteName = Soi.SoiMain.main1.SiteName;
        string sql = "Select max(tmstp),min(tmstp) from cbp."+siteName;

        DataTable tbl = sybase.Table("sitedata",sql);

        DateTime t1 = (DateTime) tbl.Rows[0][0];
        DateTime t2 = (DateTime) tbl.Rows[0][1];
        this.BeginingTime = t2;
        this.EndingTime = t1;
        this.SetInclusiveDates();
        */
    }

    private void menuItemLastTwoWeeks_Click(object sender, System.EventArgs e)
    {
      Reset(14);
    }


  }
}
