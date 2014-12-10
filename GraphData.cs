using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace HdbPoet
{

    /// <summary>
    /// GraphData manages data to a Graph/Table.
    /// This is a wrapper for a specific graph within a TimeSeriesDataSet
    /// </summary>
    public class GraphData
    {
        private DataSet dsTables;
        public DataTableCollection Tables // contains time series data, and interval MultipleSeriesDataTables
        {
            get
            {
                return dsTables.Tables;
            }
        }


        TimeSeriesDataSet ds;
        int graphNumber = 0;
        public GraphData(TimeSeriesDataSet ds, int graphNumber)
        {
            dsTables = new DataSet();
            this.ds = ds;
            SetDefaults();
            this.graphNumber = graphNumber;
            SetGraphTitle();
            SetSeriesTitles();

        }


        public event EventHandler<TimeSeriesChangeEventArgs> ValueChanged;

        public DataTable SeriesTable(int index)
        {
            string tableName = SeriesRows.Skip(index).First().TableName;
            return this.Tables[tableName];
        }


        /// <summary>
        /// Updates specified value in MultipleSeriesDataTable
        /// used when you drag a point on the Chart
        /// </summary>
        public void UpdateIntervalTable(int seriesIndex, DateTime t, object value)
        {
            MultipleSeriesDataTable tbl = IntervalTable(seriesIndex);
            tbl.UpdateValue(seriesIndex, t, value);
        }

        private MultipleSeriesDataTable IntervalTable(int seriesIndex)
        {
            var sr = SeriesRows.Skip(seriesIndex).First();
            MultipleSeriesDataTable tbl = Tables[sr.Interval] as MultipleSeriesDataTable;

            return tbl;
        }

        /// <summary>
        /// Updates a specified value in the underlying DataTable for a Series
        /// </summary>
        public void UpdateValue(int seriesIndex, DateTime t, object value)
        {

            DataTable tbl = SeriesTable(seriesIndex);
            int rowIndex = tbl.DefaultView.Find(t);
            tbl.DefaultView[rowIndex].Row[1] = value;


            double? val = 0;
            object o = value;
            if (o == DBNull.Value)
                val = null;
            else
                val = Convert.ToDouble(o);

            if (ValueChanged != null)
            {
                ValueChanged(this,
                 new TimeSeriesChangeEventArgs(seriesIndex, rowIndex, val));
            }
        }

        internal void BuildIntervalTables()
        {
            string[] intervals = IntervalList();
            foreach (string interval in intervals)
            {
                NormalizeDates(interval);
            }
            // build table for each interval.
            // using interval name for table name
            for (int j = 0; j < intervals.Length; j++)
            {
                if (Tables.IndexOf(intervals[j]) >= 0)
                    Tables.Remove(intervals[j]);

                MultipleSeriesDataTable msTable = new MultipleSeriesDataTable(this, intervals[j]);
                Tables.Add(msTable);
            }
        }

        public TimeSeriesDataSet.GraphRow GraphRow
        {
            get { return ds.Graph.FindByGraphNumber(graphNumber); }
        }


        public IEnumerable<TimeSeriesDataSet.SeriesRow> SeriesRows
        {
            get { return ds.Series.Where(r => r.GraphNumber == graphNumber); }
        }

        private void SetSeriesTitles()
        {
            foreach (var s in SeriesRows)
            {
                string units = "";
                if (HasMultipleUnits())
                {
                    units = s.Units;
                }
                s.Title = s.ParameterType + " " + units;
                // If there are multiple sites, append site name to legend.
                if (HasMultipleSites())
                {
                    s.Title = s.SiteName + " " + s.ParameterType + " " + units;
                }
                if (s.hdb_r_table == "r_base")
                { // this is needed because r_base could be any interval.
                    // we need to identify it as different
                    s.Title += " - base";
                }
            }
        }
        private void SetGraphTitle()
        {
            GraphRow.Title = "";

            if (SeriesRows.Count() == 0)
                return;

            // If there is just one site .. use the site name in the title.
            if (!HasMultipleSites())
            {
                GraphRow.Title = SeriesRows.First().SiteName;
            }

            // if all data types are the same append it to the title.
            if (!HasMultipleDataTypes())
            {
                GraphRow.Title += " " + SeriesRows.First().ParameterType;
            }
        }


         bool HasMultipleSites()
        {
            var rval = from x in SeriesRows select x.SiteName;
            return rval.Distinct().ToArray().Length > 1;
        }


         bool HasMultipleDataTypes()
         {
             var rval = from x in SeriesRows select x.ParameterType;
             return rval.Distinct().ToArray().Length > 1;
         }


          bool HasMultipleUnits()
         {
             var rval = from x in SeriesRows select x.Units;
             return rval.Distinct().ToArray().Length > 1;
         }

          internal bool ReadOnly
         {
             get
             {
                 return SeriesRows.Any(s => s.ReadOnly);
             }
         }

          public DataTable GetTable(string tableName)
          {
              return this.dsTables.Tables[tableName];
          }


          /// <summary>
          /// Lookup the beginning time for the data set.
          /// this will be used to define a database query 
          /// </summary>
          /// <param name="dataSet"></param>
          /// <returns></returns>
          internal DateTime BeginingTime()
          {
           //   TimeSeriesDataSet dataSet = this;
              SetDefaults();

              string timewindow = GraphRow.TimeWindowType;

              if (timewindow == "FromXToY")
              {
                  return GraphRow.BeginningDate;
              }
              else
                  if (timewindow == "previousXDays")
                  {
                      return DateTime.Now.AddDays(-GraphRow.PreviousDays);
                  }
                  else
                      if (timewindow == "FromXToToday")
                      {
                          return GraphRow.BeginningDate2;
                      }
                      else // should not happen...
                      {
                          System.Windows.Forms.MessageBox.Show("Error: graph type " + timewindow + " us unknown");
                          return DateTime.Now.AddDays(-30);
                      }
          }
          /// <summary>
          /// Lookup the ending time for the data set.
          /// this will be used to define a database query 
          /// </summary>
          /// <param name="dataSet"></param>
          /// <returns></returns>
          internal DateTime EndingTime()
          {
              SetDefaults();

              string timewindow = GraphRow.TimeWindowType;

              if (timewindow == "FromXToY")
              {
                  return GraphRow.EndingDate;
              }
              else if (timewindow == "previousXDays")
              {
                  return DateTime.Now;
              }
              else if (timewindow == "FromXToToday")
              {
                  return DateTime.Now;
              }
              else // should not happen...
              {
                  System.Windows.Forms.MessageBox.Show("Error: graph type " + timewindow + " us unknown");
                  return DateTime.Now;
              }
          }



          /// <summary>
          /// Sets some default values for the Data Set.
          /// </summary>
          public void SetDefaults()
          {
              if (ds.Graph.Count == 0)
              {
                  TimeSeriesDataSet.GraphRow graphsRow;
                  ds.Graph.AddGraphRow(ds.Graph.NewGraphRow());
                  graphsRow = ds.Graph[0];

                  ds.SetGraphsRowDefaults(graphsRow);
              }

          }

          

           internal string[] IntervalList()
          {
              var rval = from row1 in SeriesRows
                         where row1.GraphNumber == graphNumber
                         select row1.Interval;
              return rval.Distinct().ToArray();
          }

          /// <summary>
          /// Insert Rows as needed so all Tables, in the interval,
          /// have the same list of DateTimes.  There may be
          /// gaps in the interval if this is instant data
          /// </summary>
           void NormalizeDates(string interval)
          {
              SortedList<DateTime, DateTime> dateList = new SortedList<DateTime, DateTime>();
              BuildDateList(interval, dateList);
              //for (int i = 0; i < SeriesRows.Count(); i++)
              foreach (var s in SeriesRows)
              {
                  DataTable tbl = Tables[s.TableName];
                  tbl.DefaultView.Sort = tbl.Columns[0].ColumnName;
                  tbl.DefaultView.ApplyDefaultSort = true;

                  if (String.Compare(s.Interval, interval, true) == 0)
                  {
                      InsertMissingDates(tbl, dateList);
                  }
              }
          }

          private void BuildDateList(string interval, SortedList<DateTime, DateTime> dateList)
          {
              foreach (var s in SeriesRows)
              {
                  DataTable tbl = Tables[s.TableName];
                  if (String.Compare(s.Interval, interval, true) == 0)
                  {
                      for (int j = 0; j < tbl.Rows.Count; j++)
                      {
                          DateTime t = Convert.ToDateTime(tbl.Rows[j][0]);
                          if (!dateList.ContainsKey(t))
                          {
                              dateList.Add(t, t);
                          }
                      }
                  }
              }
          }

          private void InsertMissingDates(DataTable tbl, SortedList<DateTime, DateTime> dateList)
          {
              foreach (KeyValuePair<DateTime, DateTime> kvp in dateList)
              {
                  int idx = tbl.DefaultView.Find(kvp.Key);

                  if (idx < 0)
                  { // need new row
                      DateTime t = kvp.Key;
                      DataRow row = tbl.NewRow();
                      row[0] = t;
                      if (tbl.Rows.Count == 0 || t > Convert.ToDateTime(tbl.Rows[tbl.Rows.Count - 1][0]))
                      {   // append
                          tbl.Rows.Add(row);
                      }
                      else// find spot to insert 
                      {
                          int j = BinaryLookupIndex(tbl, t, false);
                          tbl.Rows.InsertAt(row, j);
                      }
                  }
              }
              tbl.AcceptChanges();
          }

          /// <summary>
          /// Find index in this series close to DateTime t
          /// </summary>
          /// <param name="t"></param>
          /// <param name="findNearest">if true search for nearest index. If false and no exact index to t exists, will return index with DateTime greater than t</param>
          /// <returns>index to t or first index greater than t. returns -1 on failure</returns>
          private int BinaryLookupIndex(DataTable tbl, DateTime t, bool findNearest)
          {
              int index = -1;
              int firstIndex = 0;
              int lastIndex = tbl.Rows.Count - 1;
              while (1 != 0)
              {
                  if (firstIndex > lastIndex)
                  {
                      index = -1;
                      break;
                  }
                  int key = (int)System.Math.Round((double)(firstIndex + (((double)(lastIndex - firstIndex)) / 2)));
                  //Console.WriteLine("new key = "+key);
                  if ((DateTime)tbl.DefaultView[key][0] == t)
                  {
                      index = key;
                      break;
                  }
                  if ((DateTime)tbl.DefaultView[key][0] > t)
                  {
                      lastIndex = key - 1;
                  }
                  else
                  {
                      firstIndex = key + 1;
                  }
              }

              //Console.WriteLine("lastIndex = "+lastIndex);
              //Console.WriteLine("firstIndex = "+firstIndex);
              int first = System.Math.Max(System.Math.Min(firstIndex, lastIndex), 0);
              int last = System.Math.Max(System.Math.Max(firstIndex, lastIndex), 0);
              for (int i = first; i <= last; i++)
              {
                  DateTime date = (DateTime)tbl.DefaultView[i][0];
                  if (date == t)
                  {
                      return i;
                  }
                  if (date > t && findNearest && i > 0)
                  {
                      DateTime prev = (DateTime)tbl.DefaultView[i - 1][0];
                      if (System.Math.Abs(prev.Ticks - t.Ticks)
                           < System.Math.Abs(date.Ticks - t.Ticks))
                      {
                          return i - 1;
                      }
                      return i;
                  }
                  else
                      if (date > t)
                      {

                          return i;
                      }
              }
              return -1;// not found
          }




          internal TimeSeriesDataSet.SeriesRow NewSeriesRow()
          {
              var rval = ds.Series.NewSeriesRow();
              rval.SeriesNumber = ds.Series.GetMaxSeriesNumber() + 1;
              return rval;
          }

          internal void AddSeriesRow(TimeSeriesDataSet.SeriesRow row)
          {
              ds.Series.AddSeriesRow(row);
          }
    }
}
