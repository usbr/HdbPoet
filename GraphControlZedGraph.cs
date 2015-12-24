using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using ZedGraph;
using System.IO;

namespace HdbPoet
{
    public partial class GraphControlZedGraph : UserControl, IGraphControl
    {
        GraphData graphDef;
        ZedGraphControl chart;
        private GraphPane pane; 

        public GraphControlZedGraph()
        {
            InitializeComponent();
            InitChart();
        }

        private void InitChart()
        {
            chart = new ZedGraphControl();
            pane = chart.GraphPane;
            chart.Parent = this;
            chart.Dock = DockStyle.Fill;
        }

         
        internal void Print()
        {
        }

        public void ChangeSeriesValue(TimeSeriesChangeEventArgs e)
        {
        }

        
        public void DrawGraph(GraphData graphDef)
        {
            this.graphDef = graphDef;
            this.toolStripButtonDragPoints.Enabled = !graphDef.ReadOnly;

            StandardTChart(graphDef, chart);
        }
        /// <summary>
        /// This overloaded StandardTChart is used with WindowsForms.
        /// </summary>
        /// <returns></returns>
         static void StandardTChart(GraphData ds, ZedGraphControl tChart1)
        {
            if (tChart1 == null)
            {
                return;
            }

            ZedGraphDataLoader loader = new ZedGraphDataLoader(tChart1);
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

            if (PointChanged != null)
                Console.WriteLine();
        }


        private void menuItemChartProperties_Click(object sender, EventArgs e)
        {
        }

        private void toolStripButtonDragPoints_Click(object sender, EventArgs e)
        {
            DragPoints = !DragPoints;
            toolStripButtonDragPoints.Checked = DragPoints;
        }

        private void marksTip1_GetText(Steema.TeeChart.Tools.MarksTip sender, Steema.TeeChart.Tools.MarksTipGetTextEventArgs e)
        {
            e.Text = m_markTipText;
        }

        string m_markTipText = "";

        private void chart_MouseMove(object sender, MouseEventArgs e)
        {
        }


        private void toolStripButtonUndoZoom_Click(object sender, EventArgs e)
        {
           // UndoZoom();
        }

        private void toolStripButtonProperties_Click(object sender, EventArgs e)
        {
        }


        private void undoZoomToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //UndoZoom();
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
