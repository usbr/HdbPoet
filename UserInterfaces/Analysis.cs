using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Reclamation.TimeSeries.Forms;
using Reclamation.TimeSeries;
using Reclamation.TimeSeries.Graphing;
using Reclamation.Core;


namespace HdbPoet
{
    public partial class DataAnalysis : UserControl
    {
        public PiscesEngine engine1;

        public DataAnalysis()
        {
            InitializeComponent();
        }

        public DataAnalysis(TimeSeriesDataSet ds)
        {
            InitializeComponent();
            this.ds = ds;
            var filename = AppDomain.CurrentDomain.BaseDirectory + @"hdb-poet-analysis.pdb";

            var g = new TimeSeriesZedGraph();
            var view = new GraphExplorerView(g); 
            view.Parent = this.splitContainer1.Panel2;
            view.BringToFront();
            view.Dock = DockStyle.Fill;

            engine1 = new PiscesEngine(view, filename);
        }
        

        DisplayOptionsDialog displayOptionsDialog1;
        private TimeSeriesDataSet ds;

        void DisplayAnalysisOptions(object sender, EventArgs e)
        {
            displayOptionsDialog1 = new DisplayOptionsDialog(engine1);

            if( displayOptionsDialog1.ShowDialog() == DialogResult.OK)
            {
                this.selectedAnalysisTextBox.Text = engine1.SelectedAnalysisType.ToString();

                var sList = new SeriesList();
                foreach (TimeSeriesDataSet.SeriesRow sr in ds.Series)
                {
                    HDBSeries s = new HDBSeries(this.ds, sr.SeriesNumber);
                    sList.Add(s);
                }
                Run(sList);
            }
            
        }

        void Run(SeriesList sList)
        {
            this.Text = engine1.Database.DataSource + " - Pisces";
            engine1.SelectedSeries = sList.ToArray();
            engine1.Run();
            engine1.View.Draw();

        }
    }
}
