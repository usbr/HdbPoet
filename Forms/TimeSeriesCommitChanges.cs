using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace HdbPoet
{
    public partial class TimeSeriesCommitChanges : Form
    {
        public TimeSeriesCommitChanges(MultipleSeriesDataTable table,
            string[] validationList, string colorColumnName)
        {
            InitializeComponent();
            this.timeSeriesSpreadsheet1.SetTable(table,colorColumnName);
            this.timeSeriesSpreadsheet1.DataViewRowState = DataViewRowState.ModifiedCurrent;
            SetupValidationList(validationList);
        }

        private void SetupValidationList(string[] validationList)
        {
            this.comboBoxValidation.Items.Clear();
            this.comboBoxValidation.Items.AddRange(validationList);

            for (int i = 0; i < comboBoxValidation.Items.Count; i++)
            {
                object o = comboBoxValidation.Items[i];

                if (o.ToString().IndexOf("Z") == 0)
                {
                    comboBoxValidation.SelectedIndex = i;
                    break;
                }
            }
        }

        internal bool OverwriteChecked
        {
            get { return this.checkBox1.Checked; }
        }

        internal char ValidationFlag
        {
            get {
                return this.comboBoxValidation.SelectedItem.ToString()[0];
            }
        }


    }
}
