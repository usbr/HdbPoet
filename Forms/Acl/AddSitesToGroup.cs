using System;
using System.Data;
using System.Windows.Forms;
using HdbPoet.Properties;

namespace HdbPoet.Acl
{
    public partial class AddSitesToGroup : Form
    {
        public AddSitesToGroup()
        {
            InitializeComponent();
            SetupListBoxes();
        }

        /// <summary>
        /// Fill in list boxes that show different ways to filter the
        /// selection tree.
        /// </summary>
        void SetupListBoxes()
        {
            //categories
            DataTable t = Hdb.Instance.ObjectTypesTable;
            DataRow r = t.NewRow();
            this.listBoxCategory.Items.Clear();
            for (int i = 0; i < t.Rows.Count; i++)
            {
                string objecttype_name = t.Rows[i]["objecttype_name"].ToString();
                this.listBoxCategory.Items.Add(objecttype_name);

                bool selected = Settings.Default.SelectedObjectTypes.Contains(objecttype_name);
                this.listBoxCategory.SetSelected(i, selected);
            }
          //  SetupIntervalListBox();
        }

        //private void SetupIntervalListBox()
        //{
        //    this.listBoxInterval.Items.Clear();
        //    string[] intervalNames = Hdb.r_names;
        //    for (int i = 0; i < intervalNames.Length; i++)
        //    {
        //        string interval = intervalNames[i];
        //        if (interval != "base")
        //        {
        //            this.listBoxInterval.Items.Add(interval);
        //            bool selected = Settings.Default.SelectedIntervals.Contains(interval);
        //            listBoxInterval.SetSelected(i, selected);
        //        }
        //    }
        //}

        public DataTable SiteDataTable
        {
            get {
                return tblSites;

                
            }
        }



        DataTable tblSites = null;
        private void buttonRefresh_Click(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor;
            try
            {
                //string[] intervalDescriptions = new string[listBoxInterval.SelectedItems.Count];
                //listBoxInterval.SelectedItems.CopyTo(intervalDescriptions, 0);

                int[] categories = new int[listBoxCategory.SelectedIndices.Count];
                listBoxCategory.SelectedIndices.CopyTo(categories, 0);
                DataTable tbl = Hdb.Instance.ObjectTypesTable;
                for (int i = 0; i < categories.Length; i++)
                { // switch from indices to objecttype_id
                    categories[i] = Convert.ToInt32(tbl.Rows[categories[i]]["objecttype_id"]);
                }
                tblSites = Hdb.Instance.SiteList(this.textBoxKeyWords.Text, categories);
                DataColumn c = new DataColumn("Selected", typeof(bool));
                c.AllowDBNull = false;

                c.DefaultValue = false;
                tblSites.Columns.Add(c);
                tblSites.Columns.Remove("objecttype_id");
                this.dataGridView1.DataSource = tblSites;
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }

    }
}
