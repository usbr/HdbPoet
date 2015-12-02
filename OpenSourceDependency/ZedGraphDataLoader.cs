using System;
using System.Collections.Generic;
using System.Text;
using Reclamation.Core;
using System.Configuration;
using ZedGraph;
using System.Drawing;
using System.Data;

namespace HdbPoet
{
    /// <summary>
    /// Loads TimeSeries data into a ZedGraph Graph
    /// </summary>
    public class ZedGraphDataLoader
    {
        private ZedGraph.ZedGraphControl chart1;
        private GraphPane pane; 
        public ZedGraphDataLoader(ZedGraph.ZedGraphControl chart)
        {
            chart1 = chart;
            pane = chart1.GraphPane;
        }

        private void LabelYaxis(GraphData list)
        {
            pane.YAxis.Title.Text = "";// String.Join(", ", list.Text.UniqueUnits);
        }

        public void DrawTimeSeries(GraphData list, string title, string subTitle,
            bool undoZoom,bool multiLeftAxis=false)
        {
            CreateSeries(list, title, subTitle, undoZoom, multiLeftAxis);

            int i = 0;
            foreach (DataTable s in list.Tables)
            {
                if (s.TableName.Length > 5 && s.TableName.Remove(5) == "table")
                {
                    FillTimeSeries(s, chart1.GraphPane.CurveList[i]);                
                }
                               
                i++;
            }
            
            FormatBottomAxisStandard();
            chart1.RestoreScale(chart1.GraphPane);
            pane.YAxis.Scale.Mag = 0;
            pane.YAxis.Scale.Format = "#,#";
            LabelYaxis(list);
           chart1.Refresh();
        }


        private void FormatBottomAxisStandard()
        {
            var myPane = chart1.GraphPane;
            myPane.XAxis.Title.Text = "Date";
            myPane.XAxis.Type = AxisType.Date;
            myPane.XAxis.Scale.Format = "dd-MMM-yy";
            myPane.XAxis.Scale.MajorUnit = DateUnit.Day;
            myPane.XAxis.Scale.MajorStep = 1;
            //myPane.XAxis.Scale.Min = new XDate(DateTime.Now.AddDays(-NumberOfBars));
            //myPane.XAxis.Scale.Max = new XDate(DateTime.Now);
            myPane.XAxis.MajorTic.IsBetweenLabels = true;
            myPane.XAxis.MinorTic.Size = 0;
            myPane.XAxis.MajorTic.IsInside = false;
            myPane.XAxis.MajorTic.IsOutside = true;


        }

      
        /// <summary>
        /// Creates basic graph with empty series
        /// </summary>
        private void CreateSeries(GraphData list, string title, string subTitle, 
                        bool undoZoom, bool multiLeftAxis)
        {
            Clear(undoZoom);
            var pane = chart1.GraphPane;

            chart1.Text = title + "\n" + subTitle;
            LineItem series = new LineItem("");
            foreach (var s in list.SeriesRows)
            {
               series = CreateSeries(s.Title);
                //string units = list[i].Units;
               pane.CurveList.Add(series);    
            }
            
        }



        internal void Clear( )
        {
            Clear(true);
        }

        internal void Clear(bool undoZoom )
        {
            pane.Title.Text = "";
            pane.XAxis.Title.Text = "";
            pane.Y2Axis.Title.Text = "";
            pane.CurveList.Clear();
            chart1.AxisChange();
            chart1.Refresh();
        }


        private HdbPoet.Properties.Settings Default
        {
            get { return HdbPoet.Properties.Settings.Default; }
        }

        LineItem CreateSeries(string legendText)
        {
            var pane = this.chart1.GraphPane;
            var series1 = new LineItem(legendText);
            series1.Symbol.Size = 2;
            series1.Color =  Default.GetSeriesColor(pane.CurveList.Count);
           // series1.Line.Width = Default.GetSeriesWidth(pane.CurveList.Count);
            return series1;
        }

        
        /// <summary>
        /// copy data from TimeSeries.Series into ZedGraph CurveItem
        /// </summary>
        /// <param name="s"></param>
        /// <param name="tSeries"></param>
         void FillTimeSeries(DataTable table,CurveItem tSeries)
        {
            
            int sz = table.Rows.Count;
            if (sz == 0)
            {
                return;
            }

            var pane = this.chart1.GraphPane;
            pane.XAxis.Type = AxisType.Date;
            var columnName = table.Columns[1].ColumnName;
            double avg = AverageOfColumn(table, columnName);

            for (int i = 0; i < sz; i++)
            {
                DateTime date = (DateTime)table.Rows[i][0];

                if (table.Rows[i][columnName] != System.DBNull.Value)
                {
                    double val = Convert.ToDouble(table.Rows[i][columnName]);
                    tSeries.AddPoint(date.ToOADate(),val);
                }
                else
                {
                    //list.Add(x, avg, System.Drawing.Color.Transparent);
                }
            }
        }



         static double AverageOfColumn(DataTable table, string columnName)
         {
             int sz = table.Rows.Count;
             int counter = 0;
             double rval = 0;
             for (int i = 0; i < sz; i++)
             {
                 if (table.Rows[i][columnName] != System.DBNull.Value)
                 {
                     double x = Convert.ToDouble(table.Rows[i][columnName]);
                     rval += x;
                     counter++;
                 }
             }
             if (counter > 0)
                 return rval / counter;
             else return 0;
         }

        
    }
}
