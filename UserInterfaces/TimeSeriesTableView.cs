using System;
using System.Data;
using System.Windows.Forms;
using Reclamation.Core;
using System.IO;
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;

namespace HdbPoet
{
    /// <summary>
    /// TimeSeriesTableView is used to Edit TimeSeries 
    /// Data grouped by intervals: hour,day,year,wy
    /// </summary>
    public partial class TimeSeriesTableView : UserControl
    {

        private GraphData dataSet;


        public TimeSeriesTableView()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Recompute selected points
        /// Required: selected point must have calculation
        /// </summary>
        public void Undo()
        {
            DataGridViewSelectedCellCollection x = timeSeriesSpreadsheet1.GetSelectedCells();

            try
            {

                foreach (DataGridViewCell cell in x)
                {
                    if (cell.ColumnIndex != 0) // not date column
                    {
                        DataRow row = ((DataRowView)cell.OwningRow.DataBoundItem).Row;
                        MultipleSeriesDataTable tbl = row.Table as MultipleSeriesDataTable;
                        DateTime t = Convert.ToDateTime(row[0]);
                        string interval = tbl.TableName;
                        OracleHdb.TimeSeriesDataSet.SeriesRow s = tbl.LookupSeries(cell.ColumnIndex);
                        if (s.IsComputed)
                        {
                            Hdb.Instance.Calculate_Series(s.hdb_site_datatype_id, interval, t,dataSet.GraphRow.TimeZone);
                        }
                        else
                        {
                            Logger.WriteLine("Warning series is not computed " + s.SiteName + " " + s.hdb_site_datatype_id);
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                
            }

        }



        public void CopyToClipboard()
        {
            this.timeSeriesSpreadsheet1.CopyToClipboard();
        }

        public void OpenWithExcel()
        {
            if (GetIntervalDataTable() != null)
            {
                string filename = Path.ChangeExtension(Path.GetTempFileName(), ".csv");

                // remove new line characters in header for proper csv export
                var dTab = GetIntervalDataTable();
                for (int i = 0; i < dTab.Columns.Count; i++)
                {
                    dTab.Columns[i].ColumnName = dTab.Columns[i].ColumnName.Replace("\r\n", " ");
                }

                CsvFile.WriteToCSV(GetIntervalDataTable(), filename, false);

                Process.Start(filename);
                
            }
        }

        public void SaveToHdb(bool isModeledData, int mrid)
        {
            TimeSeriesCommitChanges f = new TimeSeriesCommitChanges(GetIntervalDataTable(), Hdb.Instance.ValidationList(), m_colorColumnName);
            if (f.ShowDialog() == DialogResult.OK)
            {
                string RickClayton = "You are about to save an edit which will violate the intergrity of a computation.  The data you have edited is based on a computation that has inputs and this data is automatically updated when the input data changes.  If you edit the output of the computation rather than the input you will violate the computation integrity which is generally not recommended.  It is recommended that you edit the input source data and allow the computation to recompute an acceptable value.  DO YOU WISH TO CONTINUE    <YES/NO>";

                bool isComputed = Hdb.AnyComputedValues(dataSet,m_interval);
                if (isComputed)
                {
                    if (MessageBox.Show(RickClayton, "Overwrite computed value?", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        Hdb.Instance.SaveChanges(m_interval, dataSet, f.OverwriteChecked, f.ValidationFlag, isModeledData, mrid);
                    }
                }
                else
                {
                    Hdb.Instance.SaveChanges(m_interval, dataSet, f.OverwriteChecked, f.ValidationFlag, isModeledData, mrid);
                }
             
            }
            this.timeSeriesSpreadsheet1.SetTable(GetIntervalDataTable(),m_colorColumnName);
            this.timeSeriesSpreadsheet1.DataViewRowState = DataViewRowState.CurrentRows;
        }


        string m_interval;

        public void SetInterval(string interval) 
        {
            if (dataSet == null)
                return;
            m_interval = interval;
            this.timeSeriesSpreadsheet1.SetTable(GetIntervalDataTable(),m_colorColumnName);
        }

        public void SetColorColumnName(string columnName)
        {
            m_colorColumnName = columnName;
            timeSeriesSpreadsheet1.SetColorColumnName(m_colorColumnName);
        }

        string m_colorColumnName = "";
        public void SetDataSource(GraphData ds, string interval, string colorColumnName)
        {
            m_colorColumnName = colorColumnName;
            m_interval = interval;
                dataSet = ds;

                bool InvalidDataSet = dataSet == null || dataSet.SeriesRows.Count() == 0;

                if (InvalidDataSet)
                {
                    this.Visible = false;
                    return;
                }
                else
                {
                    this.Visible = true;
                }

                this.timeSeriesSpreadsheet1.SetTable(GetIntervalDataTable(),m_colorColumnName);
                this.timeSeriesSpreadsheet1.AllowEdits(!ds.ReadOnly);
        }

        MultipleSeriesDataTable GetIntervalDataTable()
        {
          return dataSet.GetTable(m_interval) as MultipleSeriesDataTable;
        }

        internal void SelectCell(DateTime dragDateTime, int dragSeriesIndex)
        {
            DataTable tbl = GetIntervalDataTable();
            int rowIndex = tbl.DefaultView.Find(dragDateTime);


            timeSeriesSpreadsheet1.SelectCell(rowIndex, dragSeriesIndex + 1);

        }

        internal void SetValidationFlagForSelectedCells(string flag)
        {


            string msg = "Validation flag will be set to '"+flag +"'  for all selected cells";
            if( flag.Trim() == "")
                msg = "Validation flag will be set to 'null' (cleared)  for all selected cells";

            if (MessageBox.Show(msg, "Confirm Setting of Validation Flag", MessageBoxButtons.OKCancel) != DialogResult.OK)
                return;


            DataGridViewSelectedCellCollection x = timeSeriesSpreadsheet1.GetSelectedCells();
            try
            {
                foreach (DataGridViewCell cell in x)
                {
                    if (cell.ColumnIndex != 0) // no date column
                    {
                        DataRow row = ((DataRowView)cell.OwningRow.DataBoundItem).Row;
                        MultipleSeriesDataTable tbl = row.Table as MultipleSeriesDataTable;
                        DateTime t = Convert.ToDateTime(row[0]);
                        string interval = tbl.TableName;
                        OracleHdb.TimeSeriesDataSet.SeriesRow s = tbl.LookupSeries(cell.ColumnIndex);
                        Hdb.Instance.SetValidationFlag(s.hdb_site_datatype_id, interval, t, flag,dataSet.GraphRow.TimeZone);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
   
        }

        public void SetFlagForSelectedCells(bool overwrite)
        {
        //    List<string> msgList = new List<string>();
            string msg = "Overwrite Flag will be set for all selected cells";
            if( ! overwrite)
                msg = "Overwrite Flag will be cleared for all selected cells";


            if (MessageBox.Show(msg, "Confirm Setting of Overwrite Flag", MessageBoxButtons.OKCancel) != DialogResult.OK)
                return;

            DataGridViewSelectedCellCollection x = timeSeriesSpreadsheet1.GetSelectedCells();
            try
            {
                foreach (DataGridViewCell cell in x)
                {
                    if (cell.ColumnIndex != 0) // no date column
                    {
                        DataRow row = ((DataRowView)cell.OwningRow.DataBoundItem).Row;
                        MultipleSeriesDataTable tbl = row.Table as MultipleSeriesDataTable;
                        DateTime t = Convert.ToDateTime(row[0]);
                        string interval = tbl.TableName;
                        OracleHdb.TimeSeriesDataSet.SeriesRow s = tbl.LookupSeries(cell.ColumnIndex);

                        Hdb.Instance.SetOverwriteFlag(s.hdb_site_datatype_id, interval, t, overwrite, dataSet.GraphRow.TimeZone);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        internal void Print(string title)
        {
            timeSeriesSpreadsheet1.Print(title);
        }

        
        public bool ValidState { get; set; }

        internal void Cleanup()
        {
            this.SetDataSource(new GraphData(new OracleHdb.TimeSeriesDataSet(), 0), "", "");
        }
    }
}
