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
