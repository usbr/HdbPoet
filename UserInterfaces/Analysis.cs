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


namespace HdbPoet
{
    public partial class DataAnalysis : UserControl
    {

        private PiscesEngine engine1;
        PiscesTree tree1;
        GraphExplorerView graphView1;
        private DisplayOptionsDialog displayOptionsDialog1;
        
        public DataAnalysis()
        {
            InitializeComponent();
        }

        void DatabaseChanged(object sender, EventArgs e)
        {
            tree1.SetModel(new TimeSeriesTreeModel(engine1.Database));

            this.Text = engine1.Database.DataSource + " - Pisces";

            //ReadSettingsFromDatabase();
            this.engine1.View = graphView1;
            displayOptionsDialog1 = new DisplayOptionsDialog(engine1);
            //SetupScenarioSelector();

            engine1.SelectedSeries = new Series[] { };

            engine1.View.SeriesList.Clear();
            engine1.View.Clear();
            engine1.Run();
        }




    }
}
