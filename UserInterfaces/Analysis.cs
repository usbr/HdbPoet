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

        public DataAnalysis(TimeSeriesDataSet ds)
        {
            InitializeComponent();
            this.ds = ds;
            var filename = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\hdb-poet-analysis.pdb";
            CreatePiscesDB(filename);


            //var g = new TimeSeriesZedGraph();
            var g = new TimeSeriesTeeChartGraph();
            var view = new GraphExplorerView(g);
            view.Parent = this.splitContainer1.Panel2;
            view.BringToFront();
            view.Dock = DockStyle.Fill;
            engine1 = new PiscesEngine(view, filename);

        }
        

        public void CreatePiscesDB(string fName)
        {
            var server = new SQLiteServer(fName); // Create the Pisces database
        }

        DisplayOptionsDialog displayOptionsDialog1;
        private TimeSeriesDataSet ds;

        void DisplayAnalysisOptions(object sender, EventArgs e)
        {
            displayOptionsDialog1 = new DisplayOptionsDialog(engine1);

            if( displayOptionsDialog1.ShowDialog() == DialogResult.OK)
            {
                this.selectedAnalysisTextBox.Text = engine1.SelectedAnalysisType.ToString();
                Run(sender, e);
            }            
        }

        void Run(object sender, EventArgs e)
        {
            ReRun(sender, e);
        }

        void ReRun(object sender, EventArgs e)
        {
            PrintStatus("Reading HDB data...");
            this.statusStrip1.Update();
            var sList = getSeriesList();
            if (sList.Count == 0)
            {
                PrintStatus("No Series selected. Select at least 1 Series...");
            }
            else
            {
                engine1.SelectedSeries = sList.ToArray();
                engine1.Run();
                PrintStatus("Drawing graphs...");
                engine1.View.Draw();
                PrintStatus("OK!");
            }
        }

        private SeriesList getSeriesList()
        {
            var sList = new SeriesList();
            for (int i = 0; i < selectedSeriesListBox1.SelectedIndicies.Length; i++)
            {
                int idx = selectedSeriesListBox1.SelectedIndicies[i];
                HDBSeries s = new HDBSeries(this.ds, ds.Series[idx].SeriesNumber);
                sList.Add(s);
            }
            return sList;
        }

        void PrintStatus(string text)
        {
            this.toolStripStatusLabel1.Text = text;
            this.statusStrip1.Update();
        }
    }
}
