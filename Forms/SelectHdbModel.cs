﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace HdbPoet
{
    public partial class SelectHdbModel : Form
    {
        public SelectHdbModel()
        {
            InitializeComponent();

            this.dataGridView1.DataSource = Hdb.Instance.HDB_Model();

            //this.dataGridView1.Columns["Model_ID"].Visible = false;
           this.dataGridView1.Columns["Coordinated"].Visible = false;
        }


        public int ModelID
        {
            get
            {
                if (this.dataGridView1.SelectedRows.Count > 0)
                {
                    return
                        Convert.ToInt32(dataGridView1.SelectedRows[0].Cells["Model_ID"].Value);
                }
                return -1;
            }
        }

        public string ModelName
        {
            get
            {
                if (ModelID < 0)
                    return "";

                var row = dataGridView1.SelectedRows[0];
                string rval = ModelID.ToString() + " "
                    + row.Cells["Model_Name"].Value.ToString()
                +" " + row.Cells["Cmmnt"].Value.ToString();
                return rval;

            }
        }

        public string ModelTable
        {
            get{
            if( radioButtonday.Checked)
            return "m_day";
            else if( radioButtonMonth.Checked)
            return "m_month";

            return "m_hour";
            }

        }

        public DateTime OldestModelRunDate
        {
            get { return 
                DateTime.Now.AddDays(-(int)this.numericUpDown1.Value); }
        }
    }
}
