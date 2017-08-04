using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace HdbPoet
{
    public partial class SelectedSeriesListBox : UserControl
    {
        public SelectedSeriesListBox()
        {
            InitializeComponent();

            this.listBox.MouseDoubleClick += new MouseEventHandler(selectedSeriesListBox_MouseDoubleClick);
        }

        GraphData ds;
        public void SetDataSource(GraphData value)
        {
                ds = value;
                FillListBox();
        }

        private void FillListBox()
        {
            this.listBox.Items.Clear();
            
            foreach (var item in ds.SeriesRows)
            {
                listBox.Items.Add(item.DisplayName);
            }
        }

        public void RemoveSelected()
        {
            var rows = new List<TimeSeriesDataSet.SeriesRow>();
            TimeSeriesDataSet.SeriesRow[] x =  ds.SeriesRows.ToArray();

            for (int i = 0; i < listBox.SelectedIndices.Count; i++)
            {
                rows.Add(x[listBox.SelectedIndices[i]]);
            }

            for (int i = 0; i < rows.Count; i++)
            {
                var r = rows[i];
                r.Delete();
                r.Table.AcceptChanges();
                //r.AcceptChanges();

                //rows[i].Table.AcceptChanges();
                //ds.SeriesRows.RemoveSeriesRow(rows[i]);
            }
            FillListBox();
        }

        void selectedSeriesListBox_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            RemoveSelected();
        }

        public void ClearListBox()
        {
            for (int i = 0; i < listBox.Items.Count; i++)
            {
                this.listBox.SetSelected(i, true);
            }
            this.RemoveSelected();            
        }

        /// <summary>
        /// We are relying on order in DataTable.
        /// find index(SeriesNumber) for selected and above
        /// so they can be swaped.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonMoveGraphUp_Click(object sender, EventArgs e)
        {
            int idx = this.listBox.SelectedIndex;
            if (idx < 0 || idx == 0) // cant move up if you are at the top.
                return;
            MoveSeries(idx, idx-1);
        }

        private void buttonMoveGraphDown_Click(object sender, EventArgs e)
        {
            int idx = this.listBox.SelectedIndex;
            if (idx < 0 || idx == (this.listBox.Items.Count - 1))
                return;
            MoveSeries(idx, idx + 1);
        }

        private void MoveSeries(int idx, int idxNew )
        {
            var Series = ds.SeriesRows.ToArray();
            var selected = Series[idx];
            var destination = Series[idxNew];

            TimeSeriesDataSet.SeriesDataTable table = (TimeSeriesDataSet.SeriesDataTable)selected.Table;
            int insertIndex = table.FindRowIndex(destination.SeriesNumber);

            object[] array = selected.ItemArray;
            var newRow = table.NewRow();
            newRow.ItemArray = array;

            // Fix to allow the down button on the RHS of the UI
            int insertOffset = 0;
            if (idxNew > idx)
            { insertOffset = 1; }

            selected.Delete();
            table.Rows.InsertAt(newRow, insertIndex + insertOffset);

            table.AcceptChanges();
            FillListBox();
            listBox.SelectedIndex = idxNew;
        }      
        
    }
}
