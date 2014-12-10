using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using HdbPoet.Properties;

namespace HdbPoet
{
    public partial class TimeZoneOptions : Form
    {
        public TimeZoneOptions()
        {
            InitializeComponent();
            InitializeTimeZone();
        }

        private void InitializeTimeZone()
        {
            listBoxTimeZones.SelectedIndex = -1;

            for (int i = 0; i < listBoxTimeZones.Items.Count; i++)
			{
                string s = listBoxTimeZones.Items[i].ToString();
                //if (s.Length > 1 && String.Compare(s.Substring(0, 3), Settings.Default.DisplayTimeZone, true) == 0)
                //{
                //    listBoxTimeZones.SelectedIndex = i;
                //}
                if (s.Length > 1 && String.Compare(s.Substring(0, 3), Hdb.Instance.Server.TimeZone, true) == 0)
                {
                    listBoxTimeZones.SelectedIndex = i;
                    listBoxTimeZones.Items[i] += " (Default)";
                }
			}
        }

        public string TimeZone
        {
            get
            {
                if (listBoxTimeZones.SelectedIndex >= 0)
                {
                    return listBoxTimeZones.SelectedItem.ToString().Substring(0, 3);
                }
                else
                    return "";
            }
        }

        
    }
}
