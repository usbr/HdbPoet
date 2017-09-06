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
        Hdb hdb;
        GraphData gData;

        public DataAnalysis(OracleServer oracle, GraphData graphData)
        {
            InitializeComponent();
            hdb = new Hdb(oracle);
            gData = graphData;
        }
        
        private static GraphExplorerView graphView1;
        private static PiscesEngine engine1 = new PiscesEngine(graphView1, filename);
        DisplayOptionsDialog displayOptionsDialog1;

        void DisplayAnalysisOptions(object sender, EventArgs e)
        {
            displayOptionsDialog1 = new DisplayOptionsDialog(engine1);
            displayOptionsDialog1.ShowDialog();

            if (displayOptionsDialog1.DialogResult == System.Windows.Forms.DialogResult.OK)
            {
                var a = 1;
                this.selectedAnalysisTextBox.Text = engine1.SelectedAnalysisType.ToString();
                hdb.Fill(gData);

                //rval.Columns["SourceColor"].DefaultValue = "LightGray";
                //rval.Columns["ValidationColor"].DefaultValue = "LightGray";

                var sList = new SeriesList();
                foreach (var s in gData.SeriesRows)
                {
                    var sNew = new Series();
                    var tempTable = gData.GetTable(s.TableName);
                    tempTable.Columns.Remove("SourceColor");
                    tempTable.Columns.Remove("ValidationColor");
                    tempTable.Columns.Add("flag");
                    sNew.Table = tempTable;
                    //[JR] build method to get Series TimeInterval from POET Interval
                    sNew.TimeInterval = TimeInterval.Monthly;

                    sList.Add(sNew);
                }

                DatabaseChanged(sList);
            }
            
        }

        void DatabaseChanged(SeriesList sList)
        {
            //tree1.SetModel(new TimeSeriesTreeModel(engine1.Database));

            this.Text = engine1.Database.DataSource + " - Pisces";

            ReadSettingsFromDatabase();
            engine1.View = graphExplorerView1;// graphView1;
            //displayOptionsDialog1 = new DisplayOptionsDialog(engine1);
            //SetupScenarioSelector();

            engine1.SelectedSeries = sList.ToArray();

            //engine1.View.SeriesList.Clear();
            //engine1.View.Clear();
            engine1.Run();
        }

        private void ReadSettingsFromDatabase()
        {
            var m_settings = db.Settings;
            //HydrometInfoUtility.WebCaching = m_settings.ReadBoolean("HydrometWebCaching", false);
            //HydrometInfoUtility.AutoUpdate = m_settings.ReadBoolean("HydrometAutoUpdate", false);
            //HydrometInstantSeries.KeepFlaggedData = m_settings.ReadBoolean("HydrometIncludeFlaggedData", false);
            //HydrometInfoUtility.WebOnly = m_settings.ReadBoolean("HydrometWebOnly", false);
            //Reclamation.TimeSeries.Usgs.Utility.AutoUpdate = m_settings.ReadBoolean("UsgsAutoUpdate", false);
            ////  db.se

            //Reclamation.TimeSeries.Modsim.ModsimSeries.DisplayFlowInCfs = m_settings.ReadBoolean("ModsimDisplayFlowInCfs", false);
            //SpreadsheetGearSeries.AutoUpdate = m_settings.ReadBoolean("ExcelAutoUpdate", true);

            var w = engine1.TimeWindow;
            w.FromToDatesT1 = m_settings.ReadDateTime("FromToDatesT1", w.FromToDatesT1);
            w.FromToDatesT2 = m_settings.ReadDateTime("FromToDatesT2", w.FromToDatesT2);
            w.FromDateToTodayT1 = m_settings.ReadDateTime("FromDateToTodayT1", w.FromDateToTodayT1);
            w.NumDaysFromToday = m_settings.ReadDecimal("NumDaysFromToday", w.NumDaysFromToday);

            string s = m_settings.ReadString("TimeWindowType", "FromToDates");
            w.WindowType = (TimeWindowType)System.Enum.Parse(typeof(TimeWindowType), s);
            db.AutoRefresh = m_settings.ReadBoolean("AutoRefresh", true);
        }






    }
}
