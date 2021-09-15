using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace HdbPoet.MetaData
{
    public partial class RatingTableMain : UserControl
    {
        public RatingTableMain()
        {
            InitializeComponent();
            sqlViewEditor1.CellClick += sqlViewEditor1_CellClick;
            // CHECK TSDB VERSION FOR ERD RATING TABLES ENHANCEMENT 9/15/2021 GITHUB TICKET
            string sql = "SELECT COUNT(*) FROM tsdb_property where prop_name = '4.1.5'";
            DataTable tsdbFound = Hdb.Instance.Server.Table("ref_rating", sql);            
            if (Convert.ToInt16(tsdbFound.Rows[0][0]) > 0)
            {
                RatingTableData.RatingSQL = RatingTableData.RatingSQL.Replace("r.rating_id", "r.site_rating_id as rating_id");
                RatingTableData.RatingViewSQL = RatingTableData.RatingViewSQL.Replace("r.rating_id", "r.site_rating_id as rating_id");
                RatingTableData.RatingtableListSQL = RatingTableData.RatingtableListSQL.Replace("rating_id", "site_rating_id");
            }
        }


        internal void LoadTableList()
        {
            sqlViewEditor1.LinkLabelPrimaryKey = true;
            this.sqlViewEditor1.SetQueries("ref_site_rating", RatingTableData.RatingSQL, RatingTableData.RatingViewSQL, false);

        }

        void sqlViewEditor1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex != 0 || e.RowIndex == -1)
                return;

            DataGridViewCell cell = (DataGridViewCell)sqlViewEditor1.DataGridView.Rows[e.RowIndex].Cells[e.ColumnIndex];
            var rating_id = Convert.ToInt32(cell.Value.ToString());
            OpenTable(rating_id);
        }

        private void OpenTable(int rating_id)
        {
            var e = new RatingTableEditor();
            e.SetRatingID(rating_id);
            e.Parent = this;
            e.BringToFront();
            e.Dock = DockStyle.Fill;
            e.Visible = true;
            e.OnClose += e_OnClose;
        }

        void e_OnClose(object sender, EventArgs e)
        {
            
        }
    }
}
