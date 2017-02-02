using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Reclamation.Core;
using System.Drawing;

namespace HdbPoet
{
    /// <summary>
    /// MultipleSeriesDataTable is used to combine
    /// multiple series into a single DataTable.  
    /// </summary>
    public class MultipleSeriesDataTable : DataTable
    {
        List<DataTable> m_tblList;
        List<TimeSeriesDataSet.SeriesRow> m_Series;
        List<string> m_columnTitle;
        static int s_instanceCounter=0;
        List<int> m_seriesIndexList;
        int m_instanceNumber;
        GraphData ds;

        /// <summary>
        /// Creates a DataTable composed from multiple time series
        /// </summary>
        /// <param name="ds">The TimeSeriesDataset passed to the constructor
        /// should contain time series data.</param>
        /// <param name="interval">one of : hour,day,year,wy </param>
        public MultipleSeriesDataTable(GraphData ds,
            string interval) 
            : base(interval)
        {
            this.ds = ds;
            s_instanceCounter++;

            Logger.WriteLine("MultipleSeriesDataTable(" + interval + ") + instanceCounter= " + s_instanceCounter);

            m_instanceNumber = s_instanceCounter;
            m_tblList = new List<DataTable>();
            m_Series = new List<TimeSeriesDataSet.SeriesRow>();
            m_seriesIndexList = new List<int>();
            m_columnTitle = new List<string>();

            for (int i = 0; i < ds.SeriesRows.Count(); i++)
            {
                TimeSeriesDataSet.SeriesRow s = ds.SeriesRows.Skip(i).First();
                
                DataTable tbl = ds.Tables[s.TableName];
                if (String.Compare(s.Interval, interval, true) == 0)
                {
                    m_tblList.Add(tbl);
                    // set the sort order so we can do fast lookups in
                    // The TimeSeriesDataSet code : DefaultView.Find(t)
                    
                    tbl.DefaultView.Sort = tbl.Columns[0].ColumnName;
                    tbl.DefaultView.ApplyDefaultSort = true;

                    m_Series.Add(s);

                    string title = s.SiteName + " " + s.ParameterType;
                    title += " " + s.Units + ", " + s.sdid_descriptor + 
                        " (SDID=" + s.hdb_site_datatype_id + ")";
                    if (s.hdb_r_table == "r_base")
                    {// this is needed because r_base could be any interval.
                        // we need to identify it as different
                        title += " - base";
                    }
                    m_columnTitle.Add(title);
                    m_seriesIndexList.Add(i);
                }
            }
            CreateMultiColumnTable();

            AcceptChanges();
            ColumnChanged += new DataColumnChangeEventHandler(m_table_ColumnChanged);
            SetupPrimaryKey();
            SetupReadOnly();
        }

        internal string[] DisplayFormat
        {
            get
            {
                var a = new List<string>();
                for (int i = 1; i < this.Columns.Count; i++)
                {
                    a.Add(LookupSeries(i).DisplayFormat);
                }
                return a.ToArray();
            }
            set
            {
                for (int i = 0; i < value.Length; i++)
                {
                    SetDisplayFormat(i + 1, value[i]);
                }
            }
        }

        internal void SetDisplayFormat(int columnIndex, string format)
        {
               if (columnIndex <= 0)
                throw new ArgumentOutOfRangeException();
               int idx = this.m_seriesIndexList[columnIndex - 1];
               ds.SeriesRows.Skip(idx).First().DisplayFormat = format;

        }
        internal HdbPoet.TimeSeriesDataSet.SeriesRow LookupSeries(int columnIndex)
        {
            if (columnIndex <= 0)
                throw new ArgumentOutOfRangeException();

            int idx = this.m_seriesIndexList[columnIndex - 1];
            return ds.SeriesRows.Skip(idx).First();
        }
        

        private void SetupReadOnly()
        {
            for (int i = 0; i < m_seriesIndexList.Count; i++)
            {
                Columns[i+1].ReadOnly = ds.SeriesRows.Skip(m_seriesIndexList[i]).First().ReadOnly;
            }
        }

        void ds_ValueChanged(object sender, TimeSeriesChangeEventArgs e)
        {
            Console.WriteLine("ds.ValueChanged");


        }


        private void SetupPrimaryKey()
        {
            this.Columns[0].Unique = true;
            this.PrimaryKey = new DataColumn[] { this.Columns[0] };
            this.DefaultView.Sort = this.Columns[0].ColumnName;
            this.DefaultView.ApplyDefaultSort = true;
        }



        public void UpdateValue(int seriesIndex, DateTime t, object value)
        {
            if (this.DefaultView.Sort == "")
            {
                this.DefaultView.Sort = this.Columns[0].ColumnName;
                this.DefaultView.ApplyDefaultSort = true;
            }

            int rowIndex = DefaultView.Find(t);
            int idxSeries = m_seriesIndexList.IndexOf(seriesIndex);
            if (idxSeries < 0)
                throw new InvalidOperationException("Internal Error: SeriesIndex :" + seriesIndex);
            DefaultView[rowIndex].Row[idxSeries + 1] = value;
        }

        internal void GetColor(int seriesIndex, System.DateTime t,
            out Color background, out bool bold, string colorColumnName)
        {
            if (seriesIndex >= m_tblList.Count)
            {
                throw new InvalidOperationException("Error: SeriesIndex = " + seriesIndex + " table count = " + m_tblList.Count);
            }
            DataView dView = m_tblList[seriesIndex].DefaultView;
            int idx = dView.Find(t);
            if (idx < 0)
            {
             //   Console.WriteLine("seriesIndex: " + seriesIndex + " idx = " + idx);
              //  Console.WriteLine(t.ToLongDateString() + " " + t.ToLongTimeString());
                background = Color.LightGray;// Black;
                bold = false;
            }
            else
            {
                DataRow row = dView[idx].Row;
                string s = row[colorColumnName].ToString();

                background = Color.FromName(s);

                if (row.RowState == DataRowState.Modified)
                {
                    bold = true;
                }
                else
                {
                    bold = false;
                }
            }
        }


        void MultipleSeriesDataTable_RowChanged(object sender, DataRowChangeEventArgs e)
        {
            Console.WriteLine("Row Changed");
        }


        /// <summary>
        ///  update TimeSeriesDataSet that holds the source DataTable
        ///  for this column
        /// </summary>
        void m_table_ColumnChanged(object sender, DataColumnChangeEventArgs e)
        {
            Console.WriteLine("Column Changed");
            
            Console.WriteLine("Column_Changed Event: name={0}; Column={1}; original name={2}",
                             e.Row[e.Column.ColumnName], 
                             e.Column.ColumnName, 
                             e.Row[e.Column, 
                             DataRowVersion.Original]);

            int idx = Columns.IndexOf(e.Column);
            if (idx == 0)
            {
                Console.WriteLine("??? ColumnChanged for "+e.Column.ColumnName);
                return;
            }
            idx -= 1;
            DateTime date = Convert.ToDateTime(e.Row[0]);
            
            ds.UpdateValue(m_seriesIndexList[idx], date, e.Row[e.Column]);

            // Debug..
//            if( this.DefaultView.DataViewManager.
            for (int i = 0; i < Rows.Count; i++)
            {
                DataRow r = this.Rows[i];
                Console.WriteLine(r.RowState);
            }

        }

        private void CreateMultiColumnTable()
        {
            if (m_Series.Count == 0)
            {
                return;
            }
            int count = m_tblList[0].Rows.Count;
            for (int i = 0; i < m_Series.Count; i++)
            {
                if (count != m_tblList[i].Rows.Count)
                {
                   throw new InvalidOperationException("Error: All tables in this interval should have the same number of Dates");
                }
            }

            // DateTime Column
            AppendColumn(m_tblList[0].Columns[0],m_tblList[0].Columns[0].ColumnName); 
            
            for (int i = 0; i < m_Series.Count; i++)
            {
                AppendColumn(m_tblList[i].Columns[1],m_columnTitle[i]); // value column
            }

        }

        private void AppendColumn(DataColumn dataColumn, string columnName)
        {
            DataTable tbl = dataColumn.Table;

            columnName = MakeUniqueColumnName(columnName);

            Columns.Add(columnName, dataColumn.DataType);

            for (int i = 0; i < tbl.Rows.Count; i++)
            {
                while (Rows.Count <= i)
                {
                    Rows.Add(NewRow());
                }
            Rows[i][columnName] = tbl.Rows[i][dataColumn.ColumnName];
            }
        }

        private string MakeUniqueColumnName(string columnName)
        {
            string rval = columnName;
            int i = 1;
            while (Columns.IndexOf(rval) >= 0)
            {
                rval = columnName + i.ToString();
                i++;
            }
            return rval;
        }

    }
}
