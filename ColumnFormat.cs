using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace HdbPoet
{
    public partial class ColumnFormat : Form
    {
        public ColumnFormat()
        {
            InitializeComponent();
        }


        internal void SetData(string[] seriesNames, string[] formats)
        {
            this.dataGridView1.Rows.Clear();
            for (int i = 0; i < seriesNames.Length; i++)
            {
                dataGridView1.Rows.Add(seriesNames[i], formats[i]);
            }
        }

        public string[] DisplayFormat
        {
            get
            {
                var a = new List<string>();
                for (int r = 0; r < dataGridView1.Rows.Count; r++)
                {
                    a.Add(dataGridView1[1, r].FormattedValue.ToString());
                }

                return a.ToArray();
            }
        }
        
    }
}
