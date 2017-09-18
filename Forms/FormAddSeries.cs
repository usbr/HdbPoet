using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace HdbPoet
{
    /// <summary>
    /// Used by Pisces to select HDB Series
    /// </summary>
    public partial class FormAddSeries : Form
    {

        public GraphData DataSet
        {
            get { return ss.DataSet; }
        }

        SeriesSelection ss;

        public FormAddSeries()
        {
            InitializeComponent();

            ss = new SeriesSelection(new GraphData(new OracleHdb.TimeSeriesDataSet(),0));
            ss.Parent = this;
            ss.Dock = DockStyle.Fill;
        }

    }
}
