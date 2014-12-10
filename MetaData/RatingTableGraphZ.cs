using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ZedGraph;

namespace HdbPoet.MetaData
{
    /// <summary>
    /// Simple X-Y Graph used for rating tables
    /// using ZedGraph compoentn
    /// http://www.codeproject.com/Articles/5431/A-flexible-charting-library-for-NET
    /// </summary>
    public partial class RatingTableGraphZ : UserControl
    {
        public RatingTableGraphZ()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Loads data from a DataTable into a X-Y graph.
        /// </summary>
        /// <param name="tbl"></param>
        /// <param name="xColumnName"></param>
        /// <param name="yColumnName"></param>
        public void AddSeriesFromTable(DataTable tbl, string xColumnName, string yColumnName, string label= "")
        {
            PointPairList list1 = new PointPairList();
            for (int i = 0; i < tbl.Rows.Count; i++)
            {
                double x = Convert.ToDouble(tbl.Rows[i][xColumnName]);
                double y = Convert.ToDouble(tbl.Rows[i][yColumnName]);
                list1.Add(x, y);
            }
            zed1.GraphPane.AddCurve(label, list1, Color.Black);
            zed1.AxisChange();

            
        }

        public string XAxisText
        {
            set
            {
                zed1.GraphPane.XAxis.Title.Text = value;
                zed1.AxisChange();
            }
        }
        public string YAxisText
        {
            set
            {
                zed1.GraphPane.YAxis.Title.Text = value;
                zed1.AxisChange();
            }
        }
        public string Title
        {
            set
            {
                zed1.GraphPane.Title.Text = value;
                zed1.AxisChange();
            }
        }


        internal void ClearSeries()
        {
            zed1.GraphPane.CurveList.Clear();
        }
    }
}
