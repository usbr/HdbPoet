using System;
using System.Data;
using System.Windows.Forms;
using System.Collections.Generic;
using HdbPoet.Properties;

namespace HdbPoet
{
    public partial class SelectObjectTypes : Form
    {
        private DataTable objectTypeTable;

        public SelectObjectTypes()
        {
            InitializeComponent();
            SetupObjectTable();
        }

        /// <summary>
        /// Fill in form table.
        /// </summary>
        private void SetupObjectTable()
        {

            Cursor = Cursors.WaitCursor;
            try
            {
                objectTypeTable = Hdb.Instance.ObjectTypesTableAll;
                List<int> selected = GlobalVariables.hdbObjectTypes;

                DataColumn c = new DataColumn("SELECTED", typeof(bool));
                c.AllowDBNull = false;
                c.DefaultValue = false;
                objectTypeTable.Columns.Add(c);

                foreach (DataRow row in objectTypeTable.Rows)
                {
                    int objID = Convert.ToInt32(row["OBJECTTYPE_ID"]);
                    if (selected.Contains(objID))
                    {
                        row["SELECTED"] = true;
                    }
                }

                this.dataGridView1.DataSource = objectTypeTable;
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }

        /// <summary>
        /// Save table selections to app settings
        /// </summary>
        private void ProcessTableEdits(object sender, EventArgs e)
        {
            List<int> objList = new List<int>();
            foreach (DataRow row in objectTypeTable.Rows)
            {
                if ((bool)row["SELECTED"] == true)
                {
                    objList.Add(Convert.ToInt32(row["OBJECTTYPE_ID"]));
                }
            }
            GlobalVariables.hdbObjectTypes = objList;
        }

    }
}
