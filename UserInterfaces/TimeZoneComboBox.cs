using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace HdbPoet
{
    public partial class TimeZoneComboBox : UserControl
    {
        public TimeZoneComboBox()
        {
            InitializeComponent();
            if (!DesignMode)
            {
                if (Hdb.Instance != null && Hdb.Instance.Server != null)
                    TagDefaultTimeZone(Hdb.Instance.Server.TimeZone);
                comboBoxTimeZone.SelectedIndexChanged += new EventHandler(comboBoxTimeZone_SelectedIndexChanged);
            }
        }

        void comboBoxTimeZone_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (SelectedIndexChanged != null)
                SelectedIndexChanged(this, EventArgs.Empty);
        }


        private void TagDefaultTimeZone(string timeZone)
        {
            comboBoxTimeZone.SelectedIndex = -1;

            for (int i = 0; i < comboBoxTimeZone.Items.Count; i++)
            {
                string s = comboBoxTimeZone.Items[i].ToString();
                if (String.Compare(s.Substring(0, 3), timeZone, true) == 0)
                {
                    comboBoxTimeZone.SelectedIndex = i;
                    comboBoxTimeZone.Items[i] += " (Default)";
                }
            }
        }

        public event EventHandler<EventArgs> SelectedIndexChanged;

        public string TimeZone
        {
            set
            {
                // set time zone
                for (int i = 0; i < this.comboBoxTimeZone.Items.Count; i++)
                {
                    string s = this.comboBoxTimeZone.Items[i].ToString();

                    if (String.Compare(s.Substring(0, 3), value, true) == 0)
                    {
                        this.comboBoxTimeZone.SelectedIndex = i;
                    }
                } 
            }
            get
            {
                if (comboBoxTimeZone.SelectedIndex >= 0)
                {
                    return comboBoxTimeZone.SelectedItem.ToString().Substring(0, 3);
                }
                else
                    return "";
            }
        }

    }
}
