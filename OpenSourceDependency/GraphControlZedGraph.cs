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
    public partial class GraphControl : UserControl
    {
        GraphData graphDef;
        ZedGraphControl chart;
        private GraphPane pane; 

        public GraphControl()
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

        internal void ChangeSeriesValue(TimeSeriesChangeEventArgs e)
        {
        }

        
        internal void DrawGraph(GraphData graphDef)
        {
            this.graphDef = graphDef;
            this.toolStripButtonDragPoints.Enabled = !graphDef.ReadOnly;

            HdbPoet.Graphs.StandardTChart(graphDef, chart);
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




        internal void Cleanup()
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
