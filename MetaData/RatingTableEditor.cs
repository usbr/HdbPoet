using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Reclamation.Core;

namespace HdbPoet.MetaData
{
    public partial class RatingTableEditor : UserControl
    {
        public RatingTableEditor()
        {
            InitializeComponent();
        }

        int m_rating_id;
        DataTable ratingTable = new DataTable();
        DataTable ratingTableDetails = new DataTable();
        internal void SetRatingID(int rating_id)
        {
            m_rating_id = rating_id;
            ratingTable = RatingTableData.RatingTable(rating_id);
            this.dataGridView1.DataSource = ratingTable;
        
            // get the cosmetic details
            ratingTableDetails = RatingTableData.RatingTableList(rating_id);
            labelSiteName.Text =  ratingTableDetails.Rows[0]["site_name"].ToString();
            labelRatingTableName.Text = "rating_id " + rating_id.ToString() + " - " + ratingTableDetails.Rows[0]["rating_type_common_name"].ToString();
         
            DrawGraph();
        }

        private void DrawGraph()
        {
            // draw the graph.
            graph1.AddSeriesFromTable(ratingTable, "independent_value", "dependent_value",
                ratingTableDetails.Rows[0]["rating_type_common_name"].ToString(),
                ratingTableDetails.Rows[0]["unit_common_name"].ToString(),
                ratingTableDetails.Rows[0]["site_name"].ToString());
        }

        public event EventHandler<EventArgs> OnClose;

        private void linkLabelBack_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (OnClose != null)
                OnClose(this, EventArgs.Empty);

            this.Visible = false;
            this.Parent = null;
        }

        private void linkLabelSave_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            int x = Hdb.Instance.Server.SaveTable(ratingTable);
            labelSaveStatus.Text = x + " rows modified";

        }

        private void copyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedCells.Count > 0)
            {
                DataObject o = dataGridView1.GetClipboardContent();
                if (o != null)
                    Clipboard.SetDataObject(o);
            }
        }


        private void pasteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                //DataGridViewCell cell = this.dataGridView1.CurrentCell;

                DataGridSelection sel = new DataGridSelection(this.dataGridView1);

                sel.PasteWithPrimaryKey(ClipBoardUtility.GetDataTableFromClipboard(),m_rating_id);
                //DataGridViewUtility u = new DataGridViewUtility(this.dataGrid1);
                //u.PasteFromClipboard();
              //  DrawGraph();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void toolStripMenuItemInterpolate_Click(object sender, EventArgs e)
        {
            var interpolate = new DataGridSelection(dataGridView1);
            interpolate.Interpolate();
            //DrawGraph();
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < dataGridView1.SelectedCells.Count; i++)
            {
                DataGridViewCell cell = dataGridView1.SelectedCells[i];
                if (cell.ColumnIndex != 0 && cell.Value != DBNull.Value)
                {
                    DataRow row = ((DataRowView)cell.OwningRow.DataBoundItem).Row;
                    row[cell.ColumnIndex] = DBNull.Value;
                }
            }
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControl1.SelectedTab == tabPageGraph)
                DrawGraph();
        }

        private void linkLabelRefresh_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            SetRatingID(m_rating_id);
        }

        private void linkLabelClear_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {

            DataRow[] drr = ratingTable.Select();
            for (int i = 0; i < drr.Length; i++)
                drr[i].Delete();


            //ratingTable.AcceptChanges();

        }

    }
}
