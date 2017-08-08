using System;
using System.Collections.Generic;
using System.Text;
using Reclamation.Core;
using System.Configuration;
using OxyPlot;
using OxyPlot.Series;
using OxyPlot.WindowsForms;
using System.Drawing;
using System.Data;

namespace HdbPoet
{
    /// <summary>
    /// Loads TimeSeries data into a ZedGraph Graph
    /// </summary>
    public class OxyPlotDataLoader
    {
        private PlotView chart1;

        public OxyPlotDataLoader(PlotView chart)
        {
            chart1 = chart;
        }
        

        public void DrawTimeSeries(GraphData list, string title, string subTitle,
            bool undoZoom, bool multiLeftAxis = false)
        {
            var pm = new PlotModel
            {
                Title = " ",
                Subtitle = " ",
                PlotType = PlotType.XY,
                Background = OxyColors.White,
                LegendPlacement = LegendPlacement.Inside,
                LegendPosition = LegendPosition.TopRight
            };

            foreach (var s in list.SeriesRows)
            {
                LineSeries series = new LineSeries();
                var sTable = list.Tables[s.TableName];
                foreach (DataRow sRow in sTable.Rows)
                {
                    DateTime t = DateTime.Parse(sRow["DATE_TIME"].ToString());
                    double val;
                    try
                    { val = Convert.ToDouble(sRow["VALUE"].ToString()); }
                    catch
                    { val = double.NaN; }
                    series.Points.Add(new DataPoint(OxyPlot.Axes.DateTimeAxis.ToDouble(t), val));
                }
                series.Selectable = true;
                series.Smooth = false;
                series.StrokeThickness = 2;
                series.MarkerType = MarkerType.Circle;
                //series.MarkerFill = OxyColors.Transparent;
                //series.MarkerStroke = series.Color;
                series.MarkerSize = 3.0;
                series.CanTrackerInterpolatePoints = false;
                series.Title = s.SiteName + "-" + s.ParameterType;
                pm.Series.Add(series);
            }
            pm.Axes.Add(new OxyPlot.Axes.DateTimeAxis {
                Position = OxyPlot.Axes.AxisPosition.Bottom,
                StringFormat = "M/d/yyyy"
            });
            chart1.Model = pm;
        }
        


        
        

        
    }
}
