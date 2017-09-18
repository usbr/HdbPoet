using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HdbPoet
{
    public partial class WaitToCompleteForm : System.Windows.Forms.Form
    {
        public WaitToCompleteForm()
        {
            InitializeComponent();
            StartTimer();
        }
        
        System.Windows.Forms.Timer timer = new System.Windows.Forms.Timer();
        private DateTime startTime;
        public TimeSpan tElapsed;

        private void StartTimer()
        {
            startTime = DateTime.Now;
            timer.Interval = 1000;
            timer.Tick -= new EventHandler(timer_Tick);
            timer.Tick += new EventHandler(timer_Tick);
            timer.Start();
            UpdateText();
        }

        void timer_Tick(object sender, EventArgs e)
        {
            UpdateText();
        }

        void UpdateText()
        {
            tElapsed = DateTime.Now.Subtract(startTime);
            richTextBox1.Text = string.Format("{0:D2}:{1:D2}", tElapsed.Minutes, tElapsed.Seconds);
            if (tElapsed.Hours <= 0)
            {
                int nthMinute = tElapsed.Minutes % 10;
                switch (nthMinute)
                {
                    case 1:
                        richTextBox1.Text = richTextBox1.Text + " - HDB is taking a while. Hang in there...";
                        break;
                    case 2:
                        richTextBox1.Text = richTextBox1.Text + " - Still waiting on HDB. I promise I'm still working...";
                        break;
                    case 3:
                    case 4:
                        richTextBox1.Text = richTextBox1.Text + " - It's HDB I swear... No hard feelings if you press Cancel...";
                        break;
                    case 5:
                    case 6:
                        richTextBox1.Text = richTextBox1.Text + " - Maybe your connection and/or HDB is slow?";
                        break;
                    case 7:
                    case 8:
                        richTextBox1.Text = richTextBox1.Text + " - Help HDB out; maybe limit your date range?";
                        break;
                    default:
                        richTextBox1.Text = richTextBox1.Text + " - Querying HDB data. Please wait...";
                        break;
                }
            }
            else
            {
                //label1.Text = "Timer: " + string.Format("{0:D2}:{1:D2}",
                //                           diff.Hours, diff.Minutes);
                richTextBox1.Text = string.Format("{0:D2}:{1:D2}", tElapsed.Hours, tElapsed.Minutes);
                richTextBox1.Text = richTextBox1.Text + " - Why are you querying so much data?";
            }
        }

        public event EventHandler<EventArgs> Canceled;

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            // Create a copy of the event to work with
            EventHandler<EventArgs> ea = Canceled;
            /* If there are no subscribers, eh will be null so we need to check
             * to avoid a NullReferenceException. */
            if (ea != null)
                ea(this, e);
        }
    }
}
