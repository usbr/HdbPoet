using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using OxyPlot;
using OxyPlot.Series;
using OxyPlot.WindowsForms;

namespace HdbPoet.MetaData
{

    public partial class RatingTableGraphOxyPlot : UserControl
    {
        OxyPlot.WindowsForms.PlotView graph1;

        public RatingTableGraphOxyPlot()
        {
            InitializeComponent();
            InitChart();
        }

        public PlotController customController { get; private set; }

        private void InitChart()
        {
            graph1 = new OxyPlot.WindowsForms.PlotView();
            graph1.Parent = this;
            graph1.Dock = DockStyle.Fill;

            // Sets the controller to enable show tracker on mouse hover
            customController = new PlotController();
            customController.UnbindMouseDown(OxyMouseButton.Left);
            customController.BindMouseEnter(PlotCommands.HoverSnapTrack);
            customController.BindMouseDown(OxyMouseButton.Left, PlotCommands.ZoomRectangle);
            graph1.Controller = customController;
        }


        /// <summary>
        /// Loads data from a DataTable into a X-Y graph.
        /// </summary>
        /// <param name="tbl"></param>
        /// <param name="xColumnName"></param>
        /// <param name="yColumnName"></param>
        public void AddSeriesFromTable(DataTable tbl, string xColumnName, string yColumnName, string yText, string xText, string title)
        {
            // Initialize OxyPlot PlotModel
            var pm = new PlotModel
            {
                Title = title,
                Subtitle = "Rating Table",
                PlotType = PlotType.XY,
                Background = OxyColors.WhiteSmoke,
                LegendPlacement = LegendPlacement.Outside,
                LegendPosition = LegendPosition.RightTop
            };

            // Add individual Series to PlotModel

            LineSeries series = new LineSeries();
            foreach (DataRow sRow in tbl.Rows)
            {
                double x = Convert.ToDouble(sRow[xColumnName]);
                double y = Convert.ToDouble(sRow[yColumnName]);
                series.Points.Add(new DataPoint(x, y));
            }
            series.Selectable = true;
            series.LineStyle = LineStyle.Solid;
            series.Smooth = false;
            series.StrokeThickness = 2;
            series.MarkerType = MarkerType.Circle;
            series.MarkerSize = 3.0;
            series.CanTrackerInterpolatePoints = false;
            //series.Title = s.SiteName + "-" + s.ParameterType;
            //series.YAxisKey = s.Units;
            pm.Series.Add(series);

            // Add X-Axis 
            pm.Axes.Add(new OxyPlot.Axes.LinearAxis
            {
                Position = OxyPlot.Axes.AxisPosition.Bottom,
                MajorGridlineStyle = LineStyle.Dot,
                MajorGridlineThickness = 0.25,
                MajorGridlineColor = OxyColors.LightSlateGray,
                Title = xText
            });

            // Add Y-Axis 
            pm.Axes.Add(new OxyPlot.Axes.LinearAxis
            {
                Position = OxyPlot.Axes.AxisPosition.Left,
                MajorGridlineStyle = LineStyle.Dot,
                MajorGridlineThickness = 0.25,
                MajorGridlineColor = OxyColors.LightSlateGray,
                Title = yText
            });

            // Set OxyChart contents
            graph1.Model = pm;
            graph1.Update();
        }

    }
}
