using System.Data;
using Reclamation.Core;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace HdbPoet.OracleHdb
{
    /// <summary>
    /// TimeSeriesDataSet Contains information to create a graph and table.
    /// Contains in-memory copy of time series data, and
    /// MultipleSeriesDataTable that contains composite of all
    /// time series for a each interval in the graph
    /// </summary>
    public partial class TimeSeriesDataSet
    {

        public void SetGraphsRowDefaults(TimeSeriesDataSet.GraphRow graphsRow)
        {
            graphsRow.TimeWindowType = "FromXToY";
            graphsRow.BeginningDate = DateTime.Now.AddDays(-30);
            graphsRow.BeginningDate2 = DateTime.Now.AddDays(-30);
            graphsRow.EndingDate = DateTime.Now;
            graphsRow.PreviousDays = 90;
            graphsRow.ShowTime = false;
            graphsRow.TimeZone = Hdb.Instance.Server.TimeZone;
            int next = this.Graph.GetMaxGraphNumber() + 1;
            graphsRow.Name = "Config " + next;
            graphsRow.GraphNumber = next;
        }

        partial class GraphDataTable
        {

            /// <summary>
            /// returns the maximum GraphNumber form the GraphDataTable
            /// returns -1 if no rows exist.
            /// </summary>
            /// <returns></returns>
            internal int GetMaxGraphNumber()
            {

                if (this.Rows.Count == 0)
                    return -1;

                int max = this[0].GraphNumber;
                for (int i = 1; i < this.Count; i++)
                {
                    if (this[i].GraphNumber > max)
                        max = this[i].GraphNumber;
                }

                return max;
            }
        }

        partial class SeriesDataTable
        {
            /// <summary>
            /// returns the maximum SeriesNumber form the GraphDataTable
            /// returns -1 if no rows exist.
            /// </summary>
            /// <returns></returns>
            internal int GetMaxSeriesNumber()
            {

                if (this.Rows.Count == 0)
                    return -1;

                int max = this[0].SeriesNumber;
                for (int i = 1; i < this.Count; i++)
                {
                    if (this[i].SeriesNumber > max)
                        max = this[i].SeriesNumber;
                }

                return max;
            }

            internal int FindRowIndex(int seriesNumber)
            {
                for (int i = 0; i < Count; i++)
                {
                    if (this[i].SeriesNumber == seriesNumber)
                        return i;
                }

                return -1;
            }
        }

        public partial class SeriesRow : global::System.Data.DataRow
        {
            public string DisplayName
            {
                get
                {
                    return this.SiteName + " " + this.Interval + ": " + this.ParameterType +
                        ", " + this.sdid_descriptor +
                        " (SDI=" + this.hdb_site_datatype_id + ")";
                    //return "";
                }
                set
                {
                }
            }
        }




        /// <summary>
        /// Saves Time Series Data Set to xml file.
        /// Set SaveEveryting to true to save everything
        /// inculding the time series data loaded from the database.
        /// Otherwise the time series data tables are not saved.
        /// </summary>
        public void Save(string filename, bool SaveEverything)
        {
            this.FileName = filename;
            if (this.Graph.Rows.Count > 0)
            {
                //this.Graph[0].FileName = filename;
            }

            if (SaveEverything)
            {
                WriteXml(filename);
            }
            else
            {// 

                TimeSeriesDataSet ds = new TimeSeriesDataSet();
                for (int i = 0; i < Series.Count; i++)
                {
                    SeriesRow r = ds.Series.NewSeriesRow();
                    ds.Series.AddSeriesRow(r);
                    for (int c = 0; c < Series.Columns.Count; c++)
                    {
                        r[c] = Series[i][c];
                    }
                }
                for (int i = 0; i < Graph.Count; i++)
                {
                    GraphRow r = ds.Graph.NewGraphRow();
                    ds.Graph.AddGraphRow(r);
                    for (int c = 0; c < Graph.Columns.Count; c++)
                    {
                        r[c] = Graph[i][c];
                    }
                }

                ds.WriteXml(filename);

            }
        }


        ///// <summary>
        ///// Removes all tables containing hdb series data.
        ///// The tables are named 'table1', 'table2', ...
        ///// </summary>
        //internal void RemoveTimeSeriesTables()
        //{
        //    //get the table Names.
        //    string[] tableNames = (from t in Tables.Cast<DataTable>() 
        //                           where Regex.IsMatch(t.TableName, @"table\d+", RegexOptions.IgnoreCase) 
        //                           select t.TableName).ToArray();

        //    foreach (var tn in tableNames)
        //    {
        //      Tables.Remove(tn);
        //    }
        //}

        /// <summary>
        /// Reads data set from XML file.  Creates a 
        /// GraphNumber and SeriesNumber for older files where the default value is -1.
        /// </summary>
        /// <param name="filename"></param>
        public void ReadXmlFile(string filename)
        {
            this.EnforceConstraints = false;
            Graph.Clear();
            Series.Clear();
            base.ReadXml(filename);

            int index = Graph.GetMaxGraphNumber() + 1;

            if (Graph.Count == 1 && Graph[0].Name.Trim() == "")
            {
                Graph[0].Name = Path.GetFileNameWithoutExtension(filename);
            }


            foreach (var g in this.Graph)
            {
                if (g.Name.Trim() == "")
                    g.Name = "Config" + index;

                if (g.GraphNumber == -1)
                {
                    g.GraphNumber = index;
                }
                index++;
            }

            index = Series.GetMaxSeriesNumber() + 1;
            foreach (var s in this.Series)
            {
                if (s.GraphNumber == -1)
                    s.GraphNumber = 0;

                if (s.SeriesNumber == -1)
                    s.SeriesNumber = index;
                index++;
            }

            this.EnforceConstraints = true;
        }

        /// <summary>
        /// Append another dataset this DataSet
        /// </summary>
        /// <param name="fileName"></param>
        public void AppendXmlFile(string fileName)
        {
            var ds = new OracleHdb.TimeSeriesDataSet();
            ds.ReadXmlFile(fileName);

            int graphNum = this.Graph.GetMaxGraphNumber() + 1;

            foreach (var gr in ds.Graph)
            {
                if (gr.Name.Trim() == "")
                    gr.Name = "Config" + graphNum;

                var row = Graph.NewGraphRow();
                row.ItemArray = gr.ItemArray;
                row.GraphNumber = graphNum;
                Graph.AddGraphRow(row);

                // use GraphData class to filter out Series Rows.
                GraphData gd = new GraphData(ds, gr.GraphNumber);

                int sn = this.Series.GetMaxSeriesNumber() + 1;
                foreach (var s in gd.SeriesRows)
                {
                    var row1 = Series.NewSeriesRow();
                    row1.ItemArray = s.ItemArray;
                    row1.SeriesNumber = sn++;
                    row1.GraphNumber = graphNum;
                    Series.AddSeriesRow(row1);
                }

                graphNum++;
            }
        }


        public string FileName { get; set; }

        internal int AddNewGraph(string graphName)
        {
            CleanupEmptyGraph();

            var row = Graph.NewGraphRow();
            SetGraphsRowDefaults(row);
            row.Name = graphName;
            this.Graph.AddGraphRow(row);

            return row.GraphNumber;
        }

        private void CleanupEmptyGraph()
        {
            // if there is only one graph and it is empty remove it.
            if (Graph.Count == 1 && this.Series.Count == 0
                && Graph[0].Name.IndexOf("Config") == 0)
            {
                Graph[0].Delete();
            }
        }

        internal void DeleteGraph(GraphData gd)
        {
            gd.GraphRow.Delete();

            foreach (var r in gd.SeriesRows.ToList())
            {
                r.Delete();
            }

        }

        internal string[] GetGraphNames()
        {
            var q = (from r in Graph select r.Name).ToArray();

            return q;
            //return Graph.Select(r => r.Name).ToArray();
        }

        internal int IndexOfGraphNumber(int graphNumber)
        {
            int idx = 0;
            foreach (var r in Graph)
            {
                if (r.GraphNumber == graphNumber)
                    return idx;

                idx++;
            }


            return -1;
        }

        internal int GraphNumberFromIndex(int index)
        {

            return Graph[index].GraphNumber;
        }

    }
}