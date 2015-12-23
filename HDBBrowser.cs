using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Diagnostics;
using System.Windows.Forms;
using System.IO;
using Reclamation.Core;
using Steema.TeeChart.Styles;
using HdbPoet.Properties;

namespace HdbPoet
{
    /// <summary>
    /// HDBBrowser contains a Chart and table used to edit timeseries data 
    /// </summary>
    public class HdbBrowser : System.Windows.Forms.UserControl
    {

        OracleServer oracle = null;
        TimeSeriesDataSet ds;
        GraphData graphData;
        
        bool graphListReady = false;
        SeriesSelection m_seriesSelection;
        private System.Windows.Forms.StatusBar statusBar1;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Panel panelGraphTable;
        private System.Windows.Forms.StatusBarPanel statusBarPanel1;

        private System.Windows.Forms.ContextMenu contextMenuChart;
        private System.Windows.Forms.MenuItem menuItemDates;
        private System.Windows.Forms.MenuItem menuItemRefresh;
        private Timer timer1;
        private TimeSeriesTableView timeSeriesTableView1;
        
        private System.ComponentModel.IContainer components;
        private TextBox textBoxSQL;
        private ToolStrip toolStrip1;
        private ToolStripButton toolStripButtonCopyToClipboard;
        private ToolStripButton toolStripButtonSQL;
        private ToolStripButton buttonRefresh;
        private ToolStripButton toolStripButtonDates;
        private ToolStripButton toolStripButtonExcel;
        private ToolStripButton toolStripButtonSave;
        private ToolStripButton toolStripButtonUndo;
        private ToolStripButton toolStripButtonGraph;
        private ToolStripComboBox toolStripComboBoxInterval;
        private StatusBarPanel statusBarPanel2;
        private ToolStripButton toolStripButtonClearOverwriteFlag;
        private ToolStripButton toolStripButtonAddOverwriteFlag;
        private ToolStripButton toolStripButtonValidationFlagNull;
        private ToolStripSeparator toolStripSeparator1;
        private ToolStripSeparator toolStripSeparator2;
        private ToolStripButton toolStripButtonValidation;
        private ToolStripButton toolStripButtonValidationV;
        private ToolStripButton toolStripButtonValidationProvisional;
        private ToolStripButton toolStripButtonPrint;
        private TabControl tabControl1;
        private TabPage tabPageHome;
        private TabPage tabPageTable;
        Form graphForm1;
        private Splitter splitter1;
        private Button buttonHideGraph;
        private Button buttonShowGraph;
        private OpenFileDialog openFileDialog1;
        private ToolStripComboBox comboBoxGraphList;
        private ToolStripButton addGraph;
        private ToolStripButton removeGraph;
        private ToolStripButton addConfigFile;
        private ToolStripButton previous;
        private ToolStripButton next;
        private ToolStripButton reorder;
        private Panel panelGraph;
        IGraphControl graphControlPopup;
        IGraphControl graphControlRight;
        

        public HdbBrowser()
        {
            
            InitializeComponent();

            graphControlRight = GetGraph();
            graphControlRight.Parent = panelGraph;
            graphControlRight.Dock = DockStyle.Fill;

            oracle = Hdb.Instance.Server;
            if (oracle == null)
            {
                throw new Exception("error:  oracle not connected");
            }
            CreateNewDataSet();
            LoadMultiGraphComboBox();

            graphControlPopup = GetGraph();
            graphForm1 = new GraphForm();
            graphControlPopup.Parent = graphForm1;
            graphControlPopup.Dock = DockStyle.Fill;

            
            graphControlPopup.PointChanged += new EventHandler<PointChangeEventArgs>(graphForm1_PointChanged);
            graphControlRight.PointChanged += new EventHandler<PointChangeEventArgs>(graphForm1_PointChanged);

            graphControlPopup.DatesClick += new EventHandler<EventArgs>(graphForm1_DatesClick);
            graphControlRight.DatesClick += new EventHandler<EventArgs>(graphForm1_DatesClick);
            toolStripComboBoxInterval.SelectedIndexChanged += new EventHandler(toolStripComboBoxInterval_SelectedIndexChanged);

            ValidationButtonEnabling();
            Logger.OnLogEvent += new StatusEventHandler(Logger_OnLogEvent);
        }

        static IGraphControl GetGraph()
        {
            #if HDB_OPEN
                return  new GraphControlZedGraph();
            #else
                return new GraphControl();
            #endif
        }
        private void CreateNewDataSet()
        {
            ds = new TimeSeriesDataSet();
            InitGraphData(0);
        }

        private void InitGraphData(int graphNumber)
        {
            graphData = new GraphData(ds, graphNumber);
            graphData.ValueChanged += new EventHandler<TimeSeriesChangeEventArgs>(graphDef_ValueChanged);
        }

        void Logger_OnLogEvent(object sender, StatusEventArgs e)
        {
            if( e.Tag == "ui" && statusBar1.Panels.Count >1)
            statusBar1.Panels[1].Text = e.Message;
        }


        void toolStripComboBoxInterval_SelectedIndexChanged(object sender, EventArgs e)
        {
            if( timeSeriesTableView1.ValidState)
            timeSeriesTableView1.SetInterval(SelectedInterval);
        }


        void graphForm1_DatesClick(object sender, EventArgs e)
        {
            menuItemDates_Click(sender, e);
        }

        void graphForm1_PointChanged(object sender, PointChangeEventArgs e)
        {
            timeSeriesTableView1.SelectCell(e.DateTime, e.SeriesIndex);
        }

        public void Print()
        {
            timeSeriesTableView1.Print(GetTitle());
        }


        private string GetTitle()
        {
            int idx = this.comboBoxGraphList.SelectedIndex;
            if( ds != null && ds.Graph.Rows.Count >0 && idx >=0)
                return ds.Graph[idx].Title;

            return "";
        }
        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (components != null)
                {
                    components.Dispose();
                }
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code
        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(HdbBrowser));
            this.statusBar1 = new System.Windows.Forms.StatusBar();
            this.statusBarPanel2 = new System.Windows.Forms.StatusBarPanel();
            this.statusBarPanel1 = new System.Windows.Forms.StatusBarPanel();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripButtonSQL = new System.Windows.Forms.ToolStripButton();
            this.buttonRefresh = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonDates = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonValidation = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonCopyToClipboard = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonPrint = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonExcel = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonSave = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonUndo = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonGraph = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButtonClearOverwriteFlag = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonAddOverwriteFlag = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButtonValidationFlagNull = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonValidationV = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonValidationProvisional = new System.Windows.Forms.ToolStripButton();
            this.toolStripComboBoxInterval = new System.Windows.Forms.ToolStripComboBox();
            this.comboBoxGraphList = new System.Windows.Forms.ToolStripComboBox();
            this.previous = new System.Windows.Forms.ToolStripButton();
            this.next = new System.Windows.Forms.ToolStripButton();
            this.reorder = new System.Windows.Forms.ToolStripButton();
            this.addGraph = new System.Windows.Forms.ToolStripButton();
            this.removeGraph = new System.Windows.Forms.ToolStripButton();
            this.addConfigFile = new System.Windows.Forms.ToolStripButton();
            this.panelGraphTable = new System.Windows.Forms.Panel();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPageHome = new System.Windows.Forms.TabPage();
            this.tabPageTable = new System.Windows.Forms.TabPage();
            this.buttonShowGraph = new System.Windows.Forms.Button();
            this.buttonHideGraph = new System.Windows.Forms.Button();
            this.splitter1 = new System.Windows.Forms.Splitter();
            this.timeSeriesTableView1 = new HdbPoet.TimeSeriesTableView();
            this.textBoxSQL = new System.Windows.Forms.TextBox();
            this.contextMenuChart = new System.Windows.Forms.ContextMenu();
            this.menuItemRefresh = new System.Windows.Forms.MenuItem();
            this.menuItemDates = new System.Windows.Forms.MenuItem();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.panelGraph = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.statusBarPanel2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.statusBarPanel1)).BeginInit();
            this.toolStrip1.SuspendLayout();
            this.panelGraphTable.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPageTable.SuspendLayout();
            this.SuspendLayout();
            // 
            // statusBar1
            // 
            this.statusBar1.Location = new System.Drawing.Point(0, 519);
            this.statusBar1.Name = "statusBar1";
            this.statusBar1.Panels.AddRange(new System.Windows.Forms.StatusBarPanel[] {
            this.statusBarPanel2,
            this.statusBarPanel1});
            this.statusBar1.ShowPanels = true;
            this.statusBar1.Size = new System.Drawing.Size(919, 24);
            this.statusBar1.TabIndex = 26;
            this.statusBar1.DrawItem += new System.Windows.Forms.StatusBarDrawItemEventHandler(this.statusBar1_DrawItem);
            this.statusBar1.DoubleClick += new System.EventHandler(this.statusBar1_DoubleClick);
            // 
            // statusBarPanel2
            // 
            this.statusBarPanel2.Name = "statusBarPanel2";
            this.statusBarPanel2.Style = System.Windows.Forms.StatusBarPanelStyle.OwnerDraw;
            this.statusBarPanel2.Text = "Display: MST";
            // 
            // statusBarPanel1
            // 
            this.statusBarPanel1.AutoSize = System.Windows.Forms.StatusBarPanelAutoSize.Contents;
            this.statusBarPanel1.Name = "statusBarPanel1";
            this.statusBarPanel1.Width = 10;
            // 
            // toolStrip1
            // 
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(25, 25);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButtonSQL,
            this.buttonRefresh,
            this.toolStripButtonDates,
            this.toolStripButtonValidation,
            this.toolStripButtonCopyToClipboard,
            this.toolStripButtonPrint,
            this.toolStripButtonExcel,
            this.toolStripButtonSave,
            this.toolStripButtonUndo,
            this.toolStripButtonGraph,
            this.toolStripSeparator2,
            this.toolStripButtonClearOverwriteFlag,
            this.toolStripButtonAddOverwriteFlag,
            this.toolStripSeparator1,
            this.toolStripButtonValidationFlagNull,
            this.toolStripButtonValidationV,
            this.toolStripButtonValidationProvisional,
            this.toolStripComboBoxInterval,
            this.comboBoxGraphList,
            this.previous,
            this.next,
            this.reorder,
            this.addGraph,
            this.removeGraph,
            this.addConfigFile});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(919, 32);
            this.toolStrip1.TabIndex = 2;
            // 
            // toolStripButtonSQL
            // 
            this.toolStripButtonSQL.CheckOnClick = true;
            this.toolStripButtonSQL.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripButtonSQL.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonSQL.Image")));
            this.toolStripButtonSQL.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonSQL.Name = "toolStripButtonSQL";
            this.toolStripButtonSQL.Size = new System.Drawing.Size(32, 29);
            this.toolStripButtonSQL.Text = "SQL";
            this.toolStripButtonSQL.ToolTipText = "display the latest sql command";
            this.toolStripButtonSQL.Click += new System.EventHandler(this.toolStripButtonSQL_Click);
            // 
            // buttonRefresh
            // 
            this.buttonRefresh.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.buttonRefresh.Image = ((System.Drawing.Image)(resources.GetObject("buttonRefresh.Image")));
            this.buttonRefresh.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.buttonRefresh.Name = "buttonRefresh";
            this.buttonRefresh.Size = new System.Drawing.Size(50, 29);
            this.buttonRefresh.Text = "Refresh";
            this.buttonRefresh.ToolTipText = "refresh data and graph.  This will undo any changes";
            this.buttonRefresh.Click += new System.EventHandler(this.buttonRefresh_Click);
            // 
            // toolStripButtonDates
            // 
            this.toolStripButtonDates.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripButtonDates.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonDates.Image")));
            this.toolStripButtonDates.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonDates.Name = "toolStripButtonDates";
            this.toolStripButtonDates.Size = new System.Drawing.Size(40, 29);
            this.toolStripButtonDates.Text = "Dates";
            this.toolStripButtonDates.ToolTipText = "change the date range for the graph";
            this.toolStripButtonDates.Click += new System.EventHandler(this.toolStripButtonDates_Click);
            // 
            // toolStripButtonValidation
            // 
            this.toolStripButtonValidation.CheckOnClick = true;
            this.toolStripButtonValidation.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripButtonValidation.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonValidation.Image")));
            this.toolStripButtonValidation.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonValidation.Name = "toolStripButtonValidation";
            this.toolStripButtonValidation.Size = new System.Drawing.Size(64, 29);
            this.toolStripButtonValidation.Text = "Validation";
            this.toolStripButtonValidation.Click += new System.EventHandler(this.toolStripButtonValidation_Click);
            // 
            // toolStripButtonCopyToClipboard
            // 
            this.toolStripButtonCopyToClipboard.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonCopyToClipboard.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonCopyToClipboard.Image")));
            this.toolStripButtonCopyToClipboard.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonCopyToClipboard.Name = "toolStripButtonCopyToClipboard";
            this.toolStripButtonCopyToClipboard.Size = new System.Drawing.Size(29, 29);
            this.toolStripButtonCopyToClipboard.Text = "toolStripButton1";
            this.toolStripButtonCopyToClipboard.ToolTipText = "copy to clipboard";
            this.toolStripButtonCopyToClipboard.Click += new System.EventHandler(this.toolStripButtonCopyToClipboard_Click);
            // 
            // toolStripButtonPrint
            // 
            this.toolStripButtonPrint.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonPrint.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonPrint.Image")));
            this.toolStripButtonPrint.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonPrint.Name = "toolStripButtonPrint";
            this.toolStripButtonPrint.Size = new System.Drawing.Size(29, 29);
            this.toolStripButtonPrint.Click += new System.EventHandler(this.toolStripButtonPrint_Click);
            // 
            // toolStripButtonExcel
            // 
            this.toolStripButtonExcel.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonExcel.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonExcel.Image")));
            this.toolStripButtonExcel.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonExcel.Name = "toolStripButtonExcel";
            this.toolStripButtonExcel.Size = new System.Drawing.Size(29, 29);
            this.toolStripButtonExcel.ToolTipText = "open with excel";
            this.toolStripButtonExcel.Click += new System.EventHandler(this.toolStripButtonExcel_Click);
            // 
            // toolStripButtonSave
            // 
            this.toolStripButtonSave.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonSave.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonSave.Image")));
            this.toolStripButtonSave.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonSave.Name = "toolStripButtonSave";
            this.toolStripButtonSave.Size = new System.Drawing.Size(29, 29);
            this.toolStripButtonSave.Text = "toolStripButton1";
            this.toolStripButtonSave.ToolTipText = "save changes to HDB";
            this.toolStripButtonSave.Click += new System.EventHandler(this.toolStripButtonSave_Click);
            // 
            // toolStripButtonUndo
            // 
            this.toolStripButtonUndo.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonUndo.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonUndo.Image")));
            this.toolStripButtonUndo.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonUndo.Name = "toolStripButtonUndo";
            this.toolStripButtonUndo.Size = new System.Drawing.Size(29, 29);
            this.toolStripButtonUndo.Text = "Revert";
            this.toolStripButtonUndo.ToolTipText = "revert changes (computation processor will overwrite values with information curr" +
    "ently in HDB)";
            this.toolStripButtonUndo.Click += new System.EventHandler(this.toolStripButtonUndo_Click);
            // 
            // toolStripButtonGraph
            // 
            this.toolStripButtonGraph.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonGraph.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonGraph.Image")));
            this.toolStripButtonGraph.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonGraph.Name = "toolStripButtonGraph";
            this.toolStripButtonGraph.Size = new System.Drawing.Size(29, 29);
            this.toolStripButtonGraph.Text = "toolStripButton1";
            this.toolStripButtonGraph.ToolTipText = "show graph";
            this.toolStripButtonGraph.Click += new System.EventHandler(this.toolStripButtonGraph_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 32);
            // 
            // toolStripButtonClearOverwriteFlag
            // 
            this.toolStripButtonClearOverwriteFlag.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonClearOverwriteFlag.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonClearOverwriteFlag.Image")));
            this.toolStripButtonClearOverwriteFlag.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonClearOverwriteFlag.Name = "toolStripButtonClearOverwriteFlag";
            this.toolStripButtonClearOverwriteFlag.Size = new System.Drawing.Size(29, 29);
            this.toolStripButtonClearOverwriteFlag.ToolTipText = "clear overwrite flags";
            this.toolStripButtonClearOverwriteFlag.Click += new System.EventHandler(this.toolStripButtonClearOverwriteFlag_Click);
            // 
            // toolStripButtonAddOverwriteFlag
            // 
            this.toolStripButtonAddOverwriteFlag.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonAddOverwriteFlag.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonAddOverwriteFlag.Image")));
            this.toolStripButtonAddOverwriteFlag.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonAddOverwriteFlag.Name = "toolStripButtonAddOverwriteFlag";
            this.toolStripButtonAddOverwriteFlag.Size = new System.Drawing.Size(29, 29);
            this.toolStripButtonAddOverwriteFlag.ToolTipText = "add overwrite flag";
            this.toolStripButtonAddOverwriteFlag.Click += new System.EventHandler(this.toolStripButtonAddOverwriteFlag_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 32);
            // 
            // toolStripButtonValidationFlagNull
            // 
            this.toolStripButtonValidationFlagNull.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonValidationFlagNull.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonValidationFlagNull.Image")));
            this.toolStripButtonValidationFlagNull.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonValidationFlagNull.Name = "toolStripButtonValidationFlagNull";
            this.toolStripButtonValidationFlagNull.Size = new System.Drawing.Size(29, 29);
            this.toolStripButtonValidationFlagNull.ToolTipText = "clear validation flag";
            this.toolStripButtonValidationFlagNull.Click += new System.EventHandler(this.toolStripButtonValidationNullClick);
            // 
            // toolStripButtonValidationV
            // 
            this.toolStripButtonValidationV.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonValidationV.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonValidationV.Image")));
            this.toolStripButtonValidationV.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonValidationV.Name = "toolStripButtonValidationV";
            this.toolStripButtonValidationV.Size = new System.Drawing.Size(29, 29);
            this.toolStripButtonValidationV.ToolTipText = "mark as validated \'V\'";
            this.toolStripButtonValidationV.Click += new System.EventHandler(this.toolStripButtonValidationV_Click);
            // 
            // toolStripButtonValidationProvisional
            // 
            this.toolStripButtonValidationProvisional.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonValidationProvisional.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonValidationProvisional.Image")));
            this.toolStripButtonValidationProvisional.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonValidationProvisional.Name = "toolStripButtonValidationProvisional";
            this.toolStripButtonValidationProvisional.Size = new System.Drawing.Size(29, 29);
            this.toolStripButtonValidationProvisional.ToolTipText = "mark as provisional \'P\'";
            this.toolStripButtonValidationProvisional.Click += new System.EventHandler(this.toolStripButtonValidationProvisional_Click);
            // 
            // toolStripComboBoxInterval
            // 
            this.toolStripComboBoxInterval.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.toolStripComboBoxInterval.Name = "toolStripComboBoxInterval";
            this.toolStripComboBoxInterval.Size = new System.Drawing.Size(121, 32);
            // 
            // comboBoxGraphList
            // 
            this.comboBoxGraphList.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxGraphList.Name = "comboBoxGraphList";
            this.comboBoxGraphList.Size = new System.Drawing.Size(121, 32);
            this.comboBoxGraphList.ToolTipText = "multiple graphs";
            this.comboBoxGraphList.SelectedIndexChanged += new System.EventHandler(this.comboBoxGraphList_SelectedIndexChanged);
            // 
            // previous
            // 
            this.previous.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.previous.Image = ((System.Drawing.Image)(resources.GetObject("previous.Image")));
            this.previous.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.previous.Name = "previous";
            this.previous.Size = new System.Drawing.Size(23, 29);
            this.previous.Text = "<";
            this.previous.ToolTipText = "previous graph";
            this.previous.Click += new System.EventHandler(this.previous_Click);
            // 
            // next
            // 
            this.next.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.next.Image = ((System.Drawing.Image)(resources.GetObject("next.Image")));
            this.next.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.next.Name = "next";
            this.next.Size = new System.Drawing.Size(23, 29);
            this.next.Text = ">";
            this.next.ToolTipText = "next graph";
            this.next.Click += new System.EventHandler(this.next_Click);
            // 
            // reorder
            // 
            this.reorder.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.reorder.Image = ((System.Drawing.Image)(resources.GetObject("reorder.Image")));
            this.reorder.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.reorder.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.reorder.Name = "reorder";
            this.reorder.Size = new System.Drawing.Size(23, 29);
            this.reorder.Text = "...";
            this.reorder.ToolTipText = "change order of graphs...";
            this.reorder.Click += new System.EventHandler(this.reorder_Click);
            // 
            // addGraph
            // 
            this.addGraph.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.addGraph.Image = ((System.Drawing.Image)(resources.GetObject("addGraph.Image")));
            this.addGraph.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.addGraph.Name = "addGraph";
            this.addGraph.Size = new System.Drawing.Size(23, 29);
            this.addGraph.Text = "+";
            this.addGraph.ToolTipText = "add another graph";
            this.addGraph.Click += new System.EventHandler(this.addToolStripMenuItem_Click);
            // 
            // removeGraph
            // 
            this.removeGraph.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.removeGraph.Image = ((System.Drawing.Image)(resources.GetObject("removeGraph.Image")));
            this.removeGraph.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.removeGraph.Name = "removeGraph";
            this.removeGraph.Size = new System.Drawing.Size(23, 29);
            this.removeGraph.Text = "-";
            this.removeGraph.ToolTipText = "remove current graph";
            this.removeGraph.Click += new System.EventHandler(this.removeToolStripMenuItem_Click);
            // 
            // addConfigFile
            // 
            this.addConfigFile.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.addConfigFile.Image = ((System.Drawing.Image)(resources.GetObject("addConfigFile.Image")));
            this.addConfigFile.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.addConfigFile.Name = "addConfigFile";
            this.addConfigFile.Size = new System.Drawing.Size(27, 29);
            this.addConfigFile.Text = "++";
            this.addConfigFile.ToolTipText = "add graphs from config file";
            this.addConfigFile.Click += new System.EventHandler(this.addFromFileToolStripMenuItem_Click);
            // 
            // panelGraphTable
            // 
            this.panelGraphTable.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelGraphTable.Controls.Add(this.tabControl1);
            this.panelGraphTable.Controls.Add(this.textBoxSQL);
            this.panelGraphTable.Controls.Add(this.toolStrip1);
            this.panelGraphTable.Location = new System.Drawing.Point(0, 0);
            this.panelGraphTable.Name = "panelGraphTable";
            this.panelGraphTable.Size = new System.Drawing.Size(919, 526);
            this.panelGraphTable.TabIndex = 27;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPageHome);
            this.tabControl1.Controls.Add(this.tabPageTable);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 32);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(919, 453);
            this.tabControl1.TabIndex = 3;
            this.tabControl1.SelectedIndexChanged += new System.EventHandler(this.tabControl1_SelectedIndexChanged);
            // 
            // tabPageHome
            // 
            this.tabPageHome.Location = new System.Drawing.Point(4, 22);
            this.tabPageHome.Name = "tabPageHome";
            this.tabPageHome.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageHome.Size = new System.Drawing.Size(911, 427);
            this.tabPageHome.TabIndex = 0;
            this.tabPageHome.Text = "Home";
            this.tabPageHome.UseVisualStyleBackColor = true;
            // 
            // tabPageTable
            // 
            this.tabPageTable.Controls.Add(this.panelGraph);
            this.tabPageTable.Controls.Add(this.buttonShowGraph);
            this.tabPageTable.Controls.Add(this.buttonHideGraph);
            this.tabPageTable.Controls.Add(this.splitter1);
            this.tabPageTable.Controls.Add(this.timeSeriesTableView1);
            this.tabPageTable.Location = new System.Drawing.Point(4, 22);
            this.tabPageTable.Name = "tabPageTable";
            this.tabPageTable.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageTable.Size = new System.Drawing.Size(911, 427);
            this.tabPageTable.TabIndex = 1;
            this.tabPageTable.Text = "Table";
            this.tabPageTable.UseVisualStyleBackColor = true;
            // 
            // buttonShowGraph
            // 
            this.buttonShowGraph.Dock = System.Windows.Forms.DockStyle.Right;
            this.buttonShowGraph.Location = new System.Drawing.Point(882, 3);
            this.buttonShowGraph.Name = "buttonShowGraph";
            this.buttonShowGraph.Size = new System.Drawing.Size(26, 398);
            this.buttonShowGraph.TabIndex = 4;
            this.buttonShowGraph.Text = "<";
            this.buttonShowGraph.UseVisualStyleBackColor = true;
            this.buttonShowGraph.Visible = false;
            this.buttonShowGraph.Click += new System.EventHandler(this.buttonShowGraph_Click);
            // 
            // buttonHideGraph
            // 
            this.buttonHideGraph.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.buttonHideGraph.Location = new System.Drawing.Point(460, 401);
            this.buttonHideGraph.Name = "buttonHideGraph";
            this.buttonHideGraph.Size = new System.Drawing.Size(448, 23);
            this.buttonHideGraph.TabIndex = 3;
            this.buttonHideGraph.Text = "hide >>";
            this.buttonHideGraph.UseVisualStyleBackColor = true;
            this.buttonHideGraph.Click += new System.EventHandler(this.buttonHideGraph_Click);
            // 
            // splitter1
            // 
            this.splitter1.Location = new System.Drawing.Point(453, 3);
            this.splitter1.Name = "splitter1";
            this.splitter1.Size = new System.Drawing.Size(7, 421);
            this.splitter1.TabIndex = 1;
            this.splitter1.TabStop = false;
            // 
            // timeSeriesTableView1
            // 
            this.timeSeriesTableView1.Dock = System.Windows.Forms.DockStyle.Left;
            this.timeSeriesTableView1.Location = new System.Drawing.Point(3, 3);
            this.timeSeriesTableView1.Name = "timeSeriesTableView1";
            this.timeSeriesTableView1.Size = new System.Drawing.Size(450, 421);
            this.timeSeriesTableView1.TabIndex = 0;
            this.timeSeriesTableView1.ValidState = false;
            // 
            // textBoxSQL
            // 
            this.textBoxSQL.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.textBoxSQL.Location = new System.Drawing.Point(0, 485);
            this.textBoxSQL.Multiline = true;
            this.textBoxSQL.Name = "textBoxSQL";
            this.textBoxSQL.ReadOnly = true;
            this.textBoxSQL.Size = new System.Drawing.Size(919, 41);
            this.textBoxSQL.TabIndex = 1;
            // 
            // contextMenuChart
            // 
            this.contextMenuChart.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItemRefresh,
            this.menuItemDates});
            // 
            // menuItemRefresh
            // 
            this.menuItemRefresh.Index = 0;
            this.menuItemRefresh.Text = "&Refresh";
            this.menuItemRefresh.Click += new System.EventHandler(this.menuItemRefresh_Click);
            // 
            // menuItemDates
            // 
            this.menuItemDates.Index = 1;
            this.menuItemDates.Text = "&Dates";
            this.menuItemDates.Click += new System.EventHandler(this.menuItemDates_Click);
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 1000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.Filter = "HDB Files |*.hdb|AllFiles|*.*";
            // 
            // panelGraph
            // 
            this.panelGraph.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelGraph.Location = new System.Drawing.Point(460, 3);
            this.panelGraph.Name = "panelGraph";
            this.panelGraph.Size = new System.Drawing.Size(422, 398);
            this.panelGraph.TabIndex = 5;
            // 
            // HdbBrowser
            // 
            this.AllowDrop = true;
            this.Controls.Add(this.statusBar1);
            this.Controls.Add(this.panelGraphTable);
            this.Name = "HdbBrowser";
            this.Size = new System.Drawing.Size(919, 543);
            this.DragDrop += new System.Windows.Forms.DragEventHandler(this.HDBBrowser_DragDrop);
            this.DragEnter += new System.Windows.Forms.DragEventHandler(this.HDBBrowser_DragEnter);
            ((System.ComponentModel.ISupportInitialize)(this.statusBarPanel2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.statusBarPanel1)).EndInit();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.panelGraphTable.ResumeLayout(false);
            this.panelGraphTable.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.tabPageTable.ResumeLayout(false);
            this.ResumeLayout(false);

        }
        #endregion

        public void OpenFile(string filename)
        {
            try
            {
                if (this.oracle == null)
                    return;
                Cursor = Cursors.WaitCursor;
                CreateNewDataSet();
                tabControl1.SelectedTab = tabPageTable;

                ds.ReadXmlFile(filename);
                ds.FileName = filename;
                
                RefreshSeriesSelection();
                LoadMultiGraphComboBox();

                UpdateViews(true);
                
            }
            finally
            {
                Cursor = Cursors.Default;
            }
            if (FilenameChanged != null)
            {
                HDBPoetEventArgs ea = new HDBPoetEventArgs(filename, this.ds);
                FilenameChanged(null, ea);
            }
        }

        /// <summary>
        /// Reload list of Graphs, and select using graphNumber
        /// </summary>
        /// <param name="graphNumber"></param>
        private void LoadMultiGraphComboBox(int graphNumber=-1)
        {
            graphListReady = false;
            comboBoxGraphList.Items.Clear();
            comboBoxGraphList.Items.AddRange(ds.GetGraphNames());

            if (graphNumber == -1)
            {
                if (comboBoxGraphList.Items.Count > 0)
                    comboBoxGraphList.SelectedIndex = 0;
            }
            else
            {
                int idx = 0;
                idx = ds.IndexOfGraphNumber(graphNumber);
                comboBoxGraphList.SelectedIndex = idx;
            }
            graphListReady = true;

        }

        /// <summary>
        /// update chart with user input value
        /// hide points that are deleted
        /// </summary>
        void graphDef_ValueChanged(object sender, TimeSeriesChangeEventArgs e)
        {
            graphControlPopup.ChangeSeriesValue(e);
            graphControlRight.ChangeSeriesValue(e);
        }

        public delegate void OnFileChanged(object sender, HDBPoetEventArgs fe);
        public event OnFileChanged FilenameChanged;

        public class HDBPoetEventArgs : EventArgs
        {
            public string filename;
            public TimeSeriesDataSet graphDef;

            public HDBPoetEventArgs(string filename, TimeSeriesDataSet graphDef)
            {
                this.filename = filename;
                this.graphDef = graphDef;
            }
        }

        public bool ValidationEnabled
        {
            get { return this.toolStripButtonValidation.Checked; }
        }

        private string GetColorColumnName()
        {
            if (this.toolStripButtonValidation.Checked)
                return "ValidationColor";

             return "SourceColor";
        }



        int GraphNumber
        {
            get
            {
                if (comboBoxGraphList.SelectedIndex < 0)
                    throw new Exception("error no graph selected");

              return  ds.GraphNumberFromIndex(comboBoxGraphList.SelectedIndex);
                
            }
        }

        /// <summary>
        /// Redraw graph. 
        /// </summary>
        /// <param name="reloadFromOracle">set to true to get fresh data from database</param>
        void UpdateViews(bool reloadFromOracle)
        {
            try
            {
                if (graphData == null || graphData.SeriesRows.Count() == 0)
                {
                    timeSeriesTableView1.Cleanup();
                    graphControlRight.Cleanup();
                    return;
                }

                Cursor = Cursors.WaitCursor;
                                
                if (reloadFromOracle) // reload will lose any edits
                {
                    timeSeriesTableView1.ValidState = false;
                   // ds.RemoveTimeSeriesTables();

                    graphData = new GraphData(ds, GraphNumber);
                    Hdb.Instance.Fill(graphData);
                    IntervalList = graphData.IntervalList();
                    this.timeSeriesTableView1.SetDataSource(graphData, SelectedInterval, GetColorColumnName());
                    timeSeriesTableView1.ValidState = true;
                }
                // [JR] commented out to test writing capability
                //this.toolStripButtonSave.Enabled = !graphData.ReadOnly;
                //this.toolStripButtonUndo.Enabled = !graphData.ReadOnly;



                this.textBoxSQL.Text = Hdb.Instance.Server.LastSqlCommand;
                DrawGraphs();

                if (graphData.ReadOnly)
                {
                    this.statusBarPanel1.Text = "Read only: Please contact the system administrator for edit privileges.";
                }
                else
                {
                    this.statusBarPanel1.Text = "";
                }
                if (OnGraph != null)
                {
                    OnGraph(this, EventArgs.Empty);
                }
            }
            finally
            {
                Cursor = Cursors.Default;
            }

        }

        private void DrawGraphs()
        {
            graphControlPopup.DrawGraph(graphData);
            graphControlRight.DrawGraph(graphData);

        }

        public string[] IntervalList
        {
            set
            {
                string selectedInterval = "";
                if (toolStripComboBoxInterval.SelectedItem != null)
                    selectedInterval = toolStripComboBoxInterval.SelectedItem.ToString();

                this.toolStripComboBoxInterval.Items.Clear();
                this.toolStripComboBoxInterval.Items.AddRange(value);

                int idx = this.toolStripComboBoxInterval.Items.IndexOf(selectedInterval);
                if (idx >= 0)
                    this.toolStripComboBoxInterval.SelectedIndex = idx;
                else if( value.Length >0)
                    this.toolStripComboBoxInterval.SelectedIndex = 0;
            }
        }

         string SelectedInterval
        {
            get
            {
                return toolStripComboBoxInterval.SelectedItem.ToString();
            }
        }

        //public TimeSeriesDataSet TimeSeriesDataSet
        //{
        //    get { return this.ds; }
        //}

        public event EventHandler OnGraph;

       

       
        public void SaveFullGraph(string filename)
        {
            this.ds.WriteXml(filename, XmlWriteMode.WriteSchema);
        }

        private void statusBar1_DoubleClick(object sender, System.EventArgs e)
        {
            SqlView sqlView = new SqlView(this.oracle);
            sqlView.ShowDialog();
        }


        /// <summary>
        /// Creates a new blank list of graphs.
        /// </summary>
        /// <returns>returns true if a new dataset was created</returns>
        public bool NewDataSet()
        {
            if (oracle == null)
            {
                return false;
            }

            CreateNewDataSet();

            SyncSeriesSelection();

            tabControl1.SelectedTab = tabPageHome;
            
            timeSeriesTableView1.SetDataSource(graphData, "", "");

            return true;
        }

       

        /// <summary>
        /// Update the selected Series user interface.
        /// </summary>
        private void RefreshSeriesSelection()
        {
            if (m_seriesSelection != null)
            {
                m_seriesSelection.Visible = false;
            }

            m_seriesSelection = new SeriesSelection(graphData);
            m_seriesSelection.Parent = tabPageHome;
            m_seriesSelection.Dock = DockStyle.Fill;

        }

        public void SaveGraph(string filename)
        {
            if (ds == null)
                return;
            ds.Save(filename, false);
        }

        
        public void EditDates()
        {
            menuItemDates_Click(this, EventArgs.Empty);
        }


        private void menuItemDates_Click(object sender, System.EventArgs e)
        {
            HdbPoet.TimeSelectorForm f = new TimeSelectorForm();

            graphData.SetDefaults();

            TimeSeriesDataSet.GraphRow r;
            r = this.ds.Graph[0];

            f.BeginningTime = graphData.BeginingTime();
            f.EndingTime = graphData.EndingTime();

            if (f.ShowDialog() == DialogResult.OK)
            {
                r.TimeWindowType = "FromXToY";
                r.BeginningDate = f.BeginningTime;
                r.EndingDate = f.EndingTime;
                this.UpdateViews(true);
            }
        }

        private void menuItemRefresh_Click(object sender, System.EventArgs e)
        {
            UpdateViews(true);
        }


        private void HDBBrowser_DragEnter(object sender, System.Windows.Forms.DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
                e.Effect = DragDropEffects.All;
        }

        /// <summary>
        /// Handle dragging file onto UI
        /// </summary>
        private void HDBBrowser_DragDrop(object sender, System.Windows.Forms.DragEventArgs e)
        {
            try
            {
                if (e.Data.GetDataPresent(DataFormats.FileDrop))
                {
                    String[] MyFiles;
                    MyFiles = (string[])e.Data.GetData(DataFormats.FileDrop);

                    if (MyFiles != null && MyFiles.Length > 0)
                    { // take first hdb file dropped in case of multiple
                        this.OpenFile(MyFiles[0].ToString());
                    }
                }
            }
            catch (Exception exc)
            {
                MessageBox.Show("Error dragging file", exc.ToString());
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {

            if (toolStripButtonSQL.Checked)
            {
                this.textBoxSQL.Text = Hdb.Instance.Server.LastSqlCommand;
                this.textBoxSQL.Height = 41;
            }
            else
            {
                this.textBoxSQL.Height = 8;
            }
            if (graphData.GraphRow == null)
                return;

            statusBarPanel2.Text = "Display in " + graphData.GraphRow.TimeZone;
        }

        private void toolStripButtonSave_Click(object sender, EventArgs e)
        {
            var isModeledDataVars = m_seriesSelection.GetModeledDataVars();
            bool isModeledData = isModeledDataVars.Item1;
            int mrid = isModeledDataVars.Item2;

            if (timeSeriesTableView1.ValidState)
            { timeSeriesTableView1.SaveToHdb(isModeledData, mrid); }
        }

        private void toolStripButtonExcel_Click(object sender, EventArgs e)
        {
            if( this.timeSeriesTableView1.ValidState)
              timeSeriesTableView1.OpenWithExcel();
        }

        private void toolStripButtonCopyToClipboard_Click(object sender, EventArgs e)
        {
            timeSeriesTableView1.CopyToClipboard();
        }

        private void toolStripButtonUndo_Click(object sender, EventArgs e)
        {
            timeSeriesTableView1.Undo();
        }

        private void toolStripButtonGraph_Click(object sender, EventArgs e)
        {

            graphForm1.Visible = true;
            graphForm1.BringToFront();
        }

        private void toolStripButtonSQL_Click(object sender, EventArgs e)
        {
         //   this.ShowSql = !this.ShowSql;
          //  toolStripButtonSQL.Checked = this.ShowSql;
        }

        private void toolStripButtonValidation_Click(object sender, EventArgs e)
        {
            ValidationButtonEnabling();
            this.timeSeriesTableView1.SetColorColumnName(GetColorColumnName());
        }

        private void ValidationButtonEnabling()
        {
            bool validationMode = this.toolStripButtonValidation.Checked;

            this.toolStripButtonValidationFlagNull.Enabled = validationMode;
            this.toolStripButtonValidationProvisional.Enabled = validationMode;
            this.toolStripButtonValidationV.Enabled = validationMode;

            this.toolStripButtonAddOverwriteFlag.Enabled = !validationMode;
            this.toolStripButtonClearOverwriteFlag.Enabled = !validationMode;

        }

        private void buttonRefresh_Click(object sender, EventArgs e)
        {
            UpdateViews(true);
        }

        private void toolStripButtonDates_Click(object sender, EventArgs e)
        {
            EditDates();
        }

        Pen p = new Pen(Color.Black);
        SolidBrush yellowBrush = new SolidBrush(Color.Yellow);
        SolidBrush redBrush = new SolidBrush(Color.Red);
        SolidBrush normalBrush = new SolidBrush(Color.LightGray);
        SolidBrush blackBrush = new SolidBrush(Color.Black);

        private void statusBar1_DrawItem(object sender, StatusBarDrawItemEventArgs sbdevent)
        {
            if( sbdevent.Index != 0) // just one owner draw statusbarpanel.
                return;

            Graphics g = sbdevent.Graphics;
            RectangleF rectf = new RectangleF(sbdevent.Bounds.X, sbdevent.Bounds.Y,
                sbdevent.Bounds.Width, sbdevent.Bounds.Height);

            g.DrawRectangle(p, sbdevent.Bounds);
        

            string text = statusBarPanel2.Text;
            if (Hdb.Instance.Server.TimeZone != graphData.GraphRow.TimeZone)
            {
                g.FillRectangle(redBrush, sbdevent.Bounds);
                g.DrawString(text, statusBar1.Font, yellowBrush, rectf);
            }
            else
            {
                g.FillRectangle(normalBrush, sbdevent.Bounds);
                g.DrawString(text, statusBar1.Font, blackBrush, rectf);
            }

        }

        private void toolStripButtonClearOverwriteFlag_Click(object sender, EventArgs e)
        {
            timeSeriesTableView1.SetFlagForSelectedCells(false);
        }

        private void toolStripButtonAddOverwriteFlag_Click(object sender, EventArgs e)
        {
            timeSeriesTableView1.SetFlagForSelectedCells(true);
        }

        private void toolStripButtonValidationNullClick(object sender, EventArgs e)
        {
            timeSeriesTableView1.SetValidationFlagForSelectedCells(" ");
        }

        private void toolStripButtonValidationV_Click(object sender, EventArgs e)
        {
            timeSeriesTableView1.SetValidationFlagForSelectedCells("V");
        }

        private void toolStripButtonValidationProvisional_Click(object sender, EventArgs e)
        {
            timeSeriesTableView1.SetValidationFlagForSelectedCells("P");
        }

        private void toolStripButtonPrint_Click(object sender, EventArgs e)
        {
            Print();
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControl1.SelectedTab == tabPageTable )
            {
            UpdateViews(true);
            }
            else if (tabControl1.SelectedTab == tabPageHome)
            {
                // dates may have changed from table view--- update UI
                m_seriesSelection.DataSet = graphData;
            }
        }

       
        private void buttonHideGraph_Click(object sender, EventArgs e)
        {
            // make table full size
            timeSeriesTableView1.Width = tabPageTable.Width - buttonShowGraph.Width;
            buttonHideGraph.Visible = false;
            buttonShowGraph.Visible = true;
        }

        private void buttonShowGraph_Click(object sender, EventArgs e)
        {
            // let graph take about half of the screen.
            timeSeriesTableView1.Width = tabPageTable.Width / 2; 
            buttonHideGraph.Visible = true;
            buttonShowGraph.Visible = false;
        }

        /// <summary>
        /// update user interface
        /// usually called when use changes to a new graph
        /// </summary>
        private void SyncSeriesSelection(int graphNumber=-1)
        {
            timeSeriesTableView1.Cleanup();
            graphControlRight.Cleanup();

            LoadMultiGraphComboBox(graphNumber);
            if (graphNumber == -1)
                graphNumber = GraphNumber;
            
            InitGraphData(graphNumber);
            RefreshSeriesSelection();
            IntervalList = graphData.IntervalList();
            
        }


        // create new empty graph
        private void addToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormNewGraphName f = new FormNewGraphName();
            if (f.ShowDialog() == DialogResult.OK)
            {
                int gn = this.ds.AddNewGraph(f.Value);
                SyncSeriesSelection(gn);
                tabControl1.SelectedTab = tabPageHome;
            }
        }

        private void removeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // remove currently selected Graph
            ds.DeleteGraph(graphData);
            if (ds.Graph.Count == 0)
                CreateNewDataSet();

            SyncSeriesSelection();

            if (tabControl1.SelectedTab == tabPageTable)
            {// update graph also
                UpdateViews(true);
            }

        }

        // Add graph(s) from xml config file
        private void addFromFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                ds.AppendXmlFile(openFileDialog1.FileName);
                SyncSeriesSelection();
            }
        }

       

        private void comboBoxGraphList_SelectedIndexChanged(object sender, EventArgs e)
        {
            Console.WriteLine("comboBoxGraphList_SelectedIndexChanged");
            // delete selected Graph
            if (graphListReady && comboBoxGraphList.SelectedIndex >= 0)
            {
                Console.WriteLine("graphListREady");
                InitGraphData(GraphNumber);
                RefreshSeriesSelection();

                if (tabControl1.SelectedTab == tabPageTable)
                {// update graph also
                    UpdateViews(true);   
                }
            }
        }


        private void previous_Click(object sender, EventArgs e)
        {
            int idx = comboBoxGraphList.SelectedIndex;
            if (idx > 0)
                comboBoxGraphList.SelectedIndex = --idx;
        }

        private void next_Click(object sender, EventArgs e)
        {
            int idx = comboBoxGraphList.SelectedIndex;
            if (idx < comboBoxGraphList.Items.Count - 1)
                comboBoxGraphList.SelectedIndex = ++idx;
        }

        private void reorder_Click(object sender, EventArgs e)
        {
            GraphListEditor gle = new GraphListEditor(ds);
            gle.ShowDialog();

            LoadMultiGraphComboBox();

        }

    }
}
