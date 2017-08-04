using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Steema.TeeChart.Styles;
using System.IO;

namespace HdbPoet
{
    public partial class GraphControl : UserControl, IGraphControl
    {
        GraphData graphDef;
        public GraphControl()
        {
            InitializeComponent();
            //graphDef = new GraphData(0);
            //graphDef.GraphRow.AddGraphRow(graphDef.Graph.NewGraphRow());
         
        }

        /// <summary>
        ///  clear out all Series
        /// </summary>
        public void Cleanup()
        {
            chart.Series.Clear();
        }
        internal void Print()
        {
            this.chart.Printer.Preview();
        }

        public void ChangeSeriesValue(TimeSeriesChangeEventArgs e)
        {
            Series s = chart.Series[e.SeriesIndex];
            if (e.Value.HasValue)
            {
                s[e.RowIndex].Y = e.Value.Value;
                s[e.RowIndex].Color = s.Color;
            }
            else
            {
                s[e.RowIndex].Color = Color.Transparent;
            }
        }

        

        public void DrawGraph(GraphData graphDef)
        {
            this.graphDef = graphDef;
            this.toolStripButtonDragPoints.Enabled = !graphDef.ReadOnly;

            UndoZoom();
            chart.Axes.Left.Automatic = true;
            chart.Axes.Right.Automatic = true;

            bool haveTemplate = !graphDef.GraphRow.IsChartSettingsNull();
            if (haveTemplate)
            {
                MemoryStream m = new MemoryStream(graphDef.GraphRow.ChartSettings);
                chart.Import.Template.Load(m);
                 m.Close();
            }
            HdbPoet.Graphs.StandardTChart(graphDef, chart,haveTemplate);

            SetupTChartTools();
        }

        private bool m_dragPoints = false;

        public bool DragPoints
        {
            get { return m_dragPoints; }
            set
            {

                m_dragPoints = value;
                SetupTChartTools();
            }
        }


        void SetupTChartTools()
        {
            chart.Tools.Clear();

            if (graphDef == null)
                return;
            if (m_dragPoints && !graphDef.ReadOnly)
            {
                for (int i = 0; i < graphDef.SeriesRows.Count(); i++)
                {
                    Steema.TeeChart.Tools.DragPoint dragPoint1 = new Steema.TeeChart.Tools.DragPoint();
                    dragPoint1.Series = chart[i];
                    dragPoint1.Active = true;
                    dragPoint1.Style = Steema.TeeChart.Tools.DragPointStyles.Y;

                    dragPoint1.Drag += new Steema.TeeChart.Tools.DragPointEventHandler(this.dragPoint1_Drag);
                    dragPoint1.Cursor = System.Windows.Forms.Cursors.Hand;
                    chart.Tools.Add(dragPoint1);
                }
            }
            //// format nearest point
            //for (int i = 0; i < graphDef.SeriesRows.Count(); i++)
            //{
            //    Steema.TeeChart.Tools.NearestPoint nearestPoint1 = new Steema.TeeChart.Tools.NearestPoint(chart[i]);
            //    nearestPoint1.Pen.Color = Color.Blue;
            //    nearestPoint1.Size = 25;
            //    nearestPoint1.Style = Steema.TeeChart.Tools.NearestPointStyles.Circle;
            //    chart.Tools.Add(nearestPoint1);
            //}

            // Add point tooltips
            Steema.TeeChart.Tools.MarksTip marksTip1 = new Steema.TeeChart.Tools.MarksTip(chart.Chart);
            marksTip1.Style = Steema.TeeChart.Styles.MarksStyles.XY;
            marksTip1.Active = true;
            marksTip1.MouseDelay = 250;
            marksTip1.HideDelay = 9999;
            marksTip1.MouseAction = Steema.TeeChart.Tools.MarksTipMouseAction.Move;
            marksTip1.BackColor = Color.LightSteelBlue;
            marksTip1.ForeColor = Color.Black;
            chart.Tools.Add(marksTip1);
        }


        bool m_SetActiveCellNeeded = false;
        DateTime dragDateTime = DateTime.MinValue;
        int dragSeriesIndex = -1;

        private void dragPoint1_Drag(Steema.TeeChart.Tools.DragPoint sender, int Index)
        {

            int seriesIndex = chart.Series.IndexOf(sender.Series);

            if (seriesIndex >= 0)
            {
                double val = chart[seriesIndex].YValues[Index];
                val = Math.Round(val, 2);
                chart[seriesIndex].YValues[Index] = val;

                DateTime t = Steema.TeeChart.Utils.DateTime(chart[seriesIndex].XValues[Index]);
                graphDef.UpdateIntervalTable(seriesIndex, t, val);
                dragDateTime = t;
                dragSeriesIndex = seriesIndex;
                m_SetActiveCellNeeded = true;
            }
        }

        public event EventHandler<PointChangeEventArgs> PointChanged;

        private void chart_MouseUp(object sender, MouseEventArgs e)
        {
            if (m_SetActiveCellNeeded)
            {
                PointChangedRaised(dragDateTime,dragSeriesIndex);
                //timeSeriesTableView1.SelectCell(dragDateTime, dragSeriesIndex);
                m_SetActiveCellNeeded = false;
            }


        }

        private void PointChangedRaised(DateTime dragDateTime, int dragSeriesIndex)
        {
             EventHandler<PointChangeEventArgs> handler= PointChanged;
             if (handler != null)
             {
                 handler(this, new PointChangeEventArgs(dragSeriesIndex, dragDateTime));
             }
        }

         void UndoZoom()
        {
            chart.Zoom.Undo();
            chart.Refresh();
        }

        private void menuItemChartProperties_Click(object sender, EventArgs e)
        {
            chart.ShowEditor();
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
            m_markTipText = "";
            int index = -1;
            for (int i = 0; i < chart.Series.Count; i++)
            {
                Series s = chart[i];
                index = s.Clicked(e.X, e.Y);

                if (index != -1 && i < graphDef.SeriesRows.Count())
                {
                    DateTime t = Steema.TeeChart.Utils.DateTime(s[index].X);
                    double val = s[index].Y;

                    switch (graphDef.SeriesRows.Skip(i).First().Interval)
                    {
                        case "day": m_markTipText = t.ToString("MM/dd/yyyy") + ", " + val.ToString();
                            break;
                        case "hour": m_markTipText = t.ToString("MM/dd/yyyy HH:mm") + ", " + val.ToString();
                            break;
                        case "instant": m_markTipText = t.ToString("MM/dd/yyyy HH:mm") + ", " + val.ToString();
                            break;
                        default: m_markTipText = t.ToString("MM/dd/yyyy") + ", " + val.ToString();
                            break;
                    }
                    break;
                }
            }
        }

        //private void GraphForm_FormClosing(object sender, FormClosingEventArgs e)
        //{
        //    if (e.CloseReason == CloseReason.UserClosing)
        //    {
        //        e.Cancel = true;
        //        this.Visible = false;
        //    }
        //}

        private void toolStripButtonUndoZoom_Click(object sender, EventArgs e)
        {
            UndoZoom();
        }

        private void toolStripButtonProperties_Click(object sender, EventArgs e)
        {
            

            //chart.ShowEditor();
            chart.Export.Template.IncludeData = false;
            // to do.. write to stream of bytes.. then save in ChartSettings
            
            Steema.TeeChart.Editor.Show(chart);
            
            MemoryStream mem = new MemoryStream();
            chart.Export.Template.Save(mem);
            graphDef.GraphRow.ChartSettings = mem.ToArray();
            mem.Close();
        }

       

        private void undoZoomToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UndoZoom();
        }

        private void propertieschartToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Steema.TeeChart.Editor.Show(chart);
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



    }
}
