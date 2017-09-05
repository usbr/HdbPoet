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

        // create Pisces DB
        private static string filename = AppDomain.CurrentDomain.BaseDirectory + @"hdb-poet-analysis.pdb";
        private static SQLiteServer server = new SQLiteServer(filename); // Create the Pisces database
        private static TimeSeriesDatabase db = new TimeSeriesDatabase(server); // Set a variable to invoke for working with the database

        public DataAnalysis()
        {
            InitializeComponent();
        }
        
        private static GraphExplorerView graphView1;
        private static PiscesEngine engine1 = new PiscesEngine(graphView1, filename);
        DisplayOptionsDialog displayOptionsDialog1;

        void DisplayAnalysisOptions(object sender, EventArgs e)
        {
            displayOptionsDialog1 = new DisplayOptionsDialog(engine1);
            displayOptionsDialog1.ShowDialog();
            
        }








    }
}
