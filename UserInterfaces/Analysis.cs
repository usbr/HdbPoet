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
        private WaitToCompleteForm waitForm;

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
            this.statusStrip1.Update();
            var sList = getSeriesList();
            if (sList.Count == 0)
            {
                PrintStatus("No Series selected. Select at least 1 Series...");
            }
            else
            {
                PrintStatus("Reading HDB data...");
                engine1.SelectedSeries = sList.ToArray();

                // Background Worker code adapted from http://perschluter.com/show-progress-dialog-during-long-process-c-sharp/
                if (backgroundWorker1.IsBusy != true)
                {
                    // create a new instance of the alert form
                    waitForm = new WaitToCompleteForm();
                    // event handler for the Cancel button in AlertForm
                    waitForm.Canceled += new EventHandler<EventArgs>(buttonCancel_Click);
                    // Start the asynchronous operation.
                    backgroundWorker1.RunWorkerAsync();
                    waitForm.ShowDialog();
                }
            }
        }

        // This event handler cancels the backgroundworker, fired from Cancel button in AlertForm.
        private void buttonCancel_Click(object sender, EventArgs e)
        {
            if (backgroundWorker1.WorkerSupportsCancellation == true)
            {
                // Cancel the asynchronous operation.
                backgroundWorker1.CancelAsync();
                // Close the AlertForm
                waitForm.Close();

                PrintStatus("Canceled!");
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

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            // Perform a time consuming operation and report progress.
            engine1.Run();
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (this.toolStripStatusLabel1.Text == "Canceled!")
            {
                PrintStatus("Canceled!");
            }
            else if (e.Error != null)
            {
                PrintStatus("Error: " + e.Error.Message);
            }
            else
            {
                PrintStatus("Drawing graphs...");
                engine1.View.Draw();
                var tElapsed = waitForm.tElapsed;
                string formattedTimeSpan = string.Format("{0:D2} mins, {1:D2} secs", tElapsed.Minutes, tElapsed.Seconds);
                PrintStatus("Done! Data queried in " + formattedTimeSpan);
            }
            // Close the AlertForm
            waitForm.Close();
        }
    }
}
