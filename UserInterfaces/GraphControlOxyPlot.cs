using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using OxyPlot;
using OxyPlot.WindowsForms;
using System.IO;

namespace HdbPoet
{
    public partial class GraphControlOxyPlot : UserControl, IGraphControl
    {
        GraphData graphDef;
        OxyPlot.WindowsForms.PlotView chart;
        //private GraphPane pane; 

        public GraphControlOxyPlot()
        {
            InitializeComponent();
            InitChart();
        }

        public PlotController customController { get; private set; }

        private void InitChart()
        {
            chart = new OxyPlot.WindowsForms.PlotView();
            //pane = chart.GraphPane;
            chart.Parent = this;
            chart.Dock = DockStyle.Fill;

            //Sets the controller to enable show tracker on mouse hover
            customController = new PlotController();
            customController.UnbindMouseDown(OxyMouseButton.Left);
            customController.BindMouseEnter(PlotCommands.HoverSnapTrack);
            customController.BindMouseDown(OxyMouseButton.Left, PlotCommands.ZoomRectangle);
            chart.Controller = customController;
        }

         
        internal void Print()
        {
            //string fileName = System.IO.Path.GetTempPath() + Guid.NewGuid().ToString() + ".png";
            //var pngExporter = new PngExporter { Width = 600, Height = 400, Background = OxyColors.White };
            //pngExporter.ExportToFile(chart.ActualModel, fileName);
            //System.Diagnostics.Process.Start(fileName);

            var fileName = System.IO.Path.GetTempPath() + Guid.NewGuid().ToString() + ".pdf";
            using (var stream = File.Create(fileName))
            {
                var pdfExporter = new PdfExporter { Width = 600, Height = 400, Background = OxyColors.White };
                pdfExporter.Export(chart.ActualModel, stream);
            }
            System.Diagnostics.Process.Start(fileName);
        }

        public void ChangeSeriesValue(TimeSeriesChangeEventArgs e)
        {
        }

        
        public void DrawGraph(GraphData graphDef)
        {
            this.graphDef = graphDef;

            StandardOxyChart(graphDef, chart);
        }

        /// <summary>
        /// This overloaded method is used with WindowsForms.
        /// </summary>
        /// <returns></returns>
         static void StandardOxyChart(GraphData ds, PlotView tChart1)
        {
            if (tChart1 == null)
            {
                return;
            }

            OxyPlotDataLoader loader = new OxyPlotDataLoader(tChart1);
            loader.DrawTimeSeries(ds, ds.GraphRow.Title, "", true, false);
        }

        private bool m_dragPoints = false;

        public bool DragPoints
        {
            get { return m_dragPoints; }
            set
            {
                m_dragPoints = value;
            }
        }

        public event EventHandler<PointChangeEventArgs> PointChanged;

        private void chart_MouseUp(object sender, MouseEventArgs e)
        {
        }

        private void PointChangedRaised(DateTime dragDateTime, int dragSeriesIndex)
        {
        }


        private void menuItemChartProperties_Click(object sender, EventArgs e)
        {
        }

        private void toolStripButtonDragPoints_Click(object sender, EventArgs e)
        {
            DragPoints = !DragPoints;
            toolStripButtonDragPoints.Checked = DragPoints;
        }

        private void toolStripButtonUndoZoom_Click(object sender, EventArgs e)
        {
            chart.Model.ResetAllAxes();
            chart.Refresh();
        }

        private void toolStripButtonProperties_Click(object sender, EventArgs e)
        {
        }


        private void undoZoomToolStripMenuItem_Click(object sender, EventArgs e)
        {
            chart.Model.ResetAllAxes();
            chart.Refresh();
        }

        private void propertieschartToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }


        public event EventHandler<EventArgs> EditSeriesClick;

        private void editSeriesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EventHandler<EventArgs> handler = EditSeriesClick;
            if (handler != null)
            {
                handler(this, EventArgs.Empty);
            }   
        }

        public event EventHandler<EventArgs> DatesClick;

        private void toolStripButtonDates_Click(object sender, EventArgs e)
        {
            EventHandler<EventArgs> handler = DatesClick;
            if (handler != null)
            {
                handler(this, EventArgs.Empty);
            }
        }

        private void toolStripMenuItemDates_Click(object sender, EventArgs e)
        {
            toolStripButtonDates_Click(sender, e);
        }

        private void toolStripButtonPrint_Click(object sender, EventArgs e)
        {
            Print();
        }

        public void Cleanup()
        {

            if (chart != null)
            {
                chart.Visible = false;
                chart = null;
            }
            InitChart();
        }
    }
}
