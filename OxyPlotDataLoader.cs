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
        

        /// <summary>
        /// Populates the OxyChart with all the components that it needs
        /// </summary>
        /// <param name="list"></param>
        public void DrawTimeSeries(GraphData list)
        {
            // Initialize OxyPlot PlotModel
            var pm = new PlotModel
            {
                Title = " ",
                Subtitle = " ",
                PlotType = PlotType.XY,
                Background = OxyColors.WhiteSmoke,
                LegendPlacement = LegendPlacement.Outside,
                LegendPosition = LegendPosition.RightTop
            };

            // Add individual Series to PlotModel
            var yAxes = new List<string>();
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
                series.MarkerSize = 3.0;
                series.CanTrackerInterpolatePoints = false;
                series.Title = s.SiteName + "-" + s.ParameterType;
                series.YAxisKey = s.Units;
                if (!yAxes.Contains(s.Units))
                { yAxes.Add(s.Units); }
                pm.Series.Add(series);
            }

            // Add X-Axis formatted as a DateTime
            pm.Axes.Add(new OxyPlot.Axes.DateTimeAxis
            {
                Position = OxyPlot.Axes.AxisPosition.Bottom,
                StringFormat = "M/d/yyyy",
                MajorGridlineStyle = LineStyle.Dot,
                MajorGridlineThickness = 0.25,
                MajorGridlineColor = OxyColors.LightSlateGray
            });

            // Add Y-Axes formatted as doubles
            var yAxisCounter = 0;
            var multiAxisDistanceOffset = 50;
            foreach (var unit in yAxes)
            {
                var newYAxis = new OxyPlot.Axes.LinearAxis();
                if (yAxisCounter == 0)
                {
                    newYAxis.Position = OxyPlot.Axes.AxisPosition.Left;
                    newYAxis.AxisTickToLabelDistance = 0;
                }
                else
                {
                    newYAxis.Position = OxyPlot.Axes.AxisPosition.Right;
                    newYAxis.AxisDistance = (yAxisCounter - 1) * multiAxisDistanceOffset;
                    newYAxis.AxisTickToLabelDistance = 0;
                }
                newYAxis.MajorGridlineStyle = LineStyle.Dot;
                newYAxis.MajorGridlineThickness = 0.25;
                newYAxis.MajorGridlineColor = OxyColors.LightSlateGray;
                newYAxis.Key = unit;
                newYAxis.Title = unit;
                pm.Axes.Add(newYAxis);
                yAxisCounter++;
            }

            // Set OxyChart contents
            chart1.Model = pm;
        }
        


        
        

        
    }
}
