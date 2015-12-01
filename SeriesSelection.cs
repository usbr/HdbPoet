using System;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;
using Reclamation.Core;
using System.Collections.Generic;
using HdbPoet.Properties;
using System.Configuration;
using Aga.Controls.Tree;
using Aga.Controls.Tree.NodeControls;
namespace HdbPoet
{
    /// <summary>
    /// SeriesSelection is used to query and select available time series data from HDB
    /// </summary>
    public class SeriesSelection : System.Windows.Forms.UserControl
    {
        GraphData graphDef;
        DataTable siteTableFiltered;
        private TreeModel model;

        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBoxKeyWords;
        private System.Windows.Forms.Button buttonRefresh;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ListBox listBoxCategory;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ListBox listBoxInterval;
        private HdbPoet.DateSelector dateSelector1;
        private ToolTip toolTip1;
        private CheckBox checkBoxShowBase;
        private Label label4;
        private ComboBox comboBoxInstantIncrement;
        private Aga.Controls.Tree.TreeViewAdv treeView1;
        private ImageList imageList1;
        private Label label5;
        private TimeZoneComboBox timeZoneComboBox1;
        private Label label6;
        private ComboBox comboBoxBasin;
        private Button buttonAddSelected;
        private SelectedSeriesListBox selectedSeriesListBox1;
        private Button buttonRemove;
        private SplitContainer splitContainer1;
        private Panel panel1;
        private TextBox selectedMRID;
        private CheckBox checkBoxGetMRID;
        private IContainer components;


        public SeriesSelection(GraphData dataSet)
        {
            Init(dataSet);
        }

        private void Init(GraphData dataSet)
        {
            InitializeComponent();
            SetupListBoxes();
            SetupInstantInterval();
            SetupBasinList();
            DataSet = dataSet;
            timeZoneComboBox1.SelectedIndexChanged += new EventHandler<EventArgs>(timeZoneComboBox1_SelectedIndexChanged);
            dateSelector1.ValueChanged += new EventHandler<EventArgs>(dateSelector1_ValueChanged);
            comboBoxInstantIncrement.SelectedIndexChanged += new EventHandler(comboBoxInstantIncrement_SelectedIndexChanged);

            treeView1.NodeControls.Clear();

            NodeStateIcon ni = new NodeStateIcon();
            ni.DataPropertyName = "Icon";
            treeView1.NodeControls.Add(ni);
            NodeTextBox tb = new NodeTextBox();
            //tb.EditEnabled = true;
            //tb.DrawText += new EventHandler<DrawEventArgs>(tb_DrawText);
            tb.DataPropertyName = "Text";
            treeView1.NodeControls.Add(tb);
            treeView1.SelectionMode = TreeSelectionMode.Multi;
            treeView1.Expanding += new EventHandler<TreeViewAdvEventArgs>(treeView1_Expanding);
            model = new TreeModel();

            selectedSeriesListBox1.SetDataSource( dataSet);
        }

        //void tb_DrawText(object sender, DrawEventArgs e)
        //{
        //    e.Font = GetFont();
        //}

        //private Font m_font=null;
        //private System.Drawing.Font GetFont()
        //{
        //    if (m_font == null)
        //    {
        //        string fontName = ConfigurationManager.AppSettings["TreeNodeFont"];
        //        float fontSize = 8;
        //        float.TryParse(ConfigurationManager.AppSettings["TreeNodeFontSize"], out fontSize);
        //        m_font = new Font(fontName, fontSize);

        //    }

        //    return m_font;
        //}

        void timeZoneComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            SaveSelections();
        }

        void dateSelector1_ValueChanged(object sender, EventArgs e)
        {
            SaveSelections();
        }

        

        private void SetupInstantInterval()
        {
           this.comboBoxInstantIncrement.Items.Clear();
           string s = ConfigurationManager.AppSettings["instantIntervals"];
           string[] intervals = s.Split(',');
           comboBoxInstantIncrement.Items.AddRange(intervals);
           if (Array.IndexOf(intervals,"15")>=0)
               comboBoxInstantIncrement.SelectedItem = "15";
           else
               comboBoxInstantIncrement.SelectedIndex = 0;

        }

        internal GraphData DataSet
        {
            set
            {
               this.graphDef = value;
               this.dateSelector1.ReadFromDataSet(this.graphDef);

               this.timeZoneComboBox1.TimeZone = value.GraphRow.TimeZone;

            }
            get {

                graphDef.GraphRow.TimeZone = timeZoneComboBox1.TimeZone;
                return this.graphDef;
            }
        }

        //private void LoadTreeFromDataSet()
        //{
        //    model = new TreeModel();
        //    this.treeView1.Model = model;
            
        //      HdbNode root = new HdbNode(graphDef.Graph[0].FileName);
        //      model.Nodes.Add(root);
        //       HdbNode node;
        //       if (graphDef.Series.Count == 0)
        //           return;

        //       DataTable SiteInfo = Hdb.SiteInfo(-1,new string[] {"day" }, false);
        //       for (int i = 0; i < graphDef.Series.Count; i++)
        //       {
        //           DataRow row = SiteInfo.NewRow();
        //           var s = graphDef.Series[i];
        //           string text = s.Interval + " " + s.SiteName + " " + s.ParameterType + "("
        //               + s.hdb_site_datatype_id.ToString() + ")";

        //           node = new HdbNode(text);

        //           row["site_common_name"] = s.SiteName;
        //           row["interval"] = s.Interval;
        //           row["unit_common_name"] = s.Units;
        //           row["datatype_common_name"] = s.ParameterType;
        //           row["site_datatype_id"] = s.hdb_site_datatype_id;
        //           row["rtable"] = s.hdb_r_table;
                  
        //           node.Tag = row;
        //           root.Nodes.Add(node);
        //       }
        //    //HdbNode root = new HdbNode(rootName);   
        //}


        /// <summary>
        /// Fill in list boxes that show different ways to filter the
        /// selection tree.
        /// </summary>
        void SetupListBoxes()
        {
            //categories
            DataTable t = Hdb.Instance.ObjectTypesTable;
            DataRow r = t.NewRow();
            this.listBoxCategory.Items.Clear();
            for (int i = 0; i < t.Rows.Count; i++)
            {
                string objecttype_name = t.Rows[i]["objecttype_name"].ToString();
                this.listBoxCategory.Items.Add(objecttype_name);

                bool selected = Settings.Default.SelectedObjectTypes.Contains(objecttype_name);
                this.listBoxCategory.SetSelected(i, selected);
            }
            SetupIntervalListBox();
        }

        private void SetupBasinList()
        {
            this.comboBoxBasin.Items.Clear();
            this.comboBoxBasin.DataSource = Hdb.Instance.BasinNames();
            this.comboBoxBasin.ValueMember = "site_id";
            this.comboBoxBasin.DisplayMember = "site_name";
            comboBoxBasin.SelectedIndex = 0;
        }

        private void SetupIntervalListBox()
        {
            this.listBoxInterval.Items.Clear();
            string[] intervalNames = Hdb.r_names;
            for (int i = 0; i < intervalNames.Length; i++)
            {
                string interval = intervalNames[i];
                if (interval != "base")
                {
                    this.listBoxInterval.Items.Add(interval);
                    bool selected = Settings.Default.SelectedIntervals.Contains(interval);
                    listBoxInterval.SetSelected(i, selected);
                }
            }
        }

        /// <summary>
        /// using the filtered site list build a tree that
        /// has datatype (such as 'flow')as the sub-branches.
        /// The Tag property of each tree node contains the important HDB site_datatype_id
        /// </summary>
        void LoadTree()
        {
            model = new TreeModel();
            
            this.treeView1.Model = model;
            HdbNode node;
            string rootName = Hdb.Instance.Server.ServiceName + " Sites";
            HdbNode root = new HdbNode(rootName);
            
            model.Nodes.Add(root);
            int sz = this.siteTableFiltered.Rows.Count;
            for (int i = 0; i < sz; i++) // Each Site
            {
                node = new HdbNode((string)siteTableFiltered.Rows[i]["SITE_Common_NAME"]);
                node.Text = node.Text + " ( " + siteTableFiltered.Rows[i]["SITE_ID"].ToString() + ")";
                node.Tag = this.siteTableFiltered.Rows[i];
                node.Nodes.Add( new HdbNode("expand_this_site"));
                root.Nodes.Add(node);
            }
            treeView1.FindNode( model.GetPath(root)).Expand();
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SeriesSelection));
            this.label2 = new System.Windows.Forms.Label();
            this.textBoxKeyWords = new System.Windows.Forms.TextBox();
            this.buttonRefresh = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.listBoxCategory = new System.Windows.Forms.ListBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.selectedMRID = new System.Windows.Forms.TextBox();
            this.checkBoxGetMRID = new System.Windows.Forms.CheckBox();
            this.label6 = new System.Windows.Forms.Label();
            this.comboBoxBasin = new System.Windows.Forms.ComboBox();
            this.timeZoneComboBox1 = new HdbPoet.TimeZoneComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.comboBoxInstantIncrement = new System.Windows.Forms.ComboBox();
            this.checkBoxShowBase = new System.Windows.Forms.CheckBox();
            this.dateSelector1 = new HdbPoet.DateSelector();
            this.listBoxInterval = new System.Windows.Forms.ListBox();
            this.label1 = new System.Windows.Forms.Label();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.treeView1 = new Aga.Controls.Tree.TreeViewAdv();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.buttonAddSelected = new System.Windows.Forms.Button();
            this.buttonRemove = new System.Windows.Forms.Button();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.selectedSeriesListBox1 = new HdbPoet.SelectedSeriesListBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(8, 16);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(160, 16);
            this.label2.TabIndex = 11;
            this.label2.Text = "site name search string";
            // 
            // textBoxKeyWords
            // 
            this.textBoxKeyWords.Location = new System.Drawing.Point(8, 32);
            this.textBoxKeyWords.Name = "textBoxKeyWords";
            this.textBoxKeyWords.Size = new System.Drawing.Size(216, 20);
            this.textBoxKeyWords.TabIndex = 10;
            // 
            // buttonRefresh
            // 
            this.buttonRefresh.Location = new System.Drawing.Point(248, 188);
            this.buttonRefresh.Name = "buttonRefresh";
            this.buttonRefresh.Size = new System.Drawing.Size(112, 23);
            this.buttonRefresh.TabIndex = 9;
            this.buttonRefresh.Text = "create list of sites";
            this.buttonRefresh.Click += new System.EventHandler(this.buttonRefresh_Click);
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(6, 55);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(104, 16);
            this.label3.TabIndex = 7;
            this.label3.Text = "object type";
            // 
            // listBoxCategory
            // 
            this.listBoxCategory.Location = new System.Drawing.Point(8, 74);
            this.listBoxCategory.Name = "listBoxCategory";
            this.listBoxCategory.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.listBoxCategory.Size = new System.Drawing.Size(216, 134);
            this.listBoxCategory.TabIndex = 17;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.selectedMRID);
            this.groupBox1.Controls.Add(this.checkBoxGetMRID);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.comboBoxBasin);
            this.groupBox1.Controls.Add(this.timeZoneComboBox1);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.comboBoxInstantIncrement);
            this.groupBox1.Controls.Add(this.checkBoxShowBase);
            this.groupBox1.Controls.Add(this.dateSelector1);
            this.groupBox1.Controls.Add(this.listBoxInterval);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.textBoxKeyWords);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.listBoxCategory);
            this.groupBox1.Controls.Add(this.buttonRefresh);
            this.groupBox1.Location = new System.Drawing.Point(8, 8);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(792, 220);
            this.groupBox1.TabIndex = 30;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Site List Criteria";
            // 
            // selectedMRID
            // 
            this.selectedMRID.Location = new System.Drawing.Point(630, 136);
            this.selectedMRID.Name = "selectedMRID";
            this.selectedMRID.Size = new System.Drawing.Size(77, 20);
            this.selectedMRID.TabIndex = 31;
            this.selectedMRID.Text = "4";
            this.selectedMRID.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // checkBoxGetMRID
            // 
            this.checkBoxGetMRID.AutoSize = true;
            this.checkBoxGetMRID.Location = new System.Drawing.Point(531, 139);
            this.checkBoxGetMRID.Name = "checkBoxGetMRID";
            this.checkBoxGetMRID.Size = new System.Drawing.Size(93, 17);
            this.checkBoxGetMRID.TabIndex = 30;
            this.checkBoxGetMRID.Text = "Modeled Data";
            this.checkBoxGetMRID.UseVisualStyleBackColor = true;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(251, 121);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(32, 13);
            this.label6.TabIndex = 29;
            this.label6.Text = "basin";
            // 
            // comboBoxBasin
            // 
            this.comboBoxBasin.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxBasin.FormattingEnabled = true;
            this.comboBoxBasin.Location = new System.Drawing.Point(250, 139);
            this.comboBoxBasin.Name = "comboBoxBasin";
            this.comboBoxBasin.Size = new System.Drawing.Size(255, 21);
            this.comboBoxBasin.TabIndex = 28;
            // 
            // timeZoneComboBox1
            // 
            this.timeZoneComboBox1.Location = new System.Drawing.Point(531, 188);
            this.timeZoneComboBox1.Name = "timeZoneComboBox1";
            this.timeZoneComboBox1.Size = new System.Drawing.Size(251, 21);
            this.timeZoneComboBox1.TabIndex = 27;
            this.timeZoneComboBox1.TimeZone = "";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(538, 169);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(52, 13);
            this.label5.TabIndex = 26;
            this.label5.Text = "time zone";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(386, 169);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(98, 13);
            this.label4.TabIndex = 25;
            this.label4.Text = "increment (minutes)";
            // 
            // comboBoxInstantIncrement
            // 
            this.comboBoxInstantIncrement.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxInstantIncrement.FormattingEnabled = true;
            this.comboBoxInstantIncrement.Location = new System.Drawing.Point(380, 188);
            this.comboBoxInstantIncrement.Name = "comboBoxInstantIncrement";
            this.comboBoxInstantIncrement.Size = new System.Drawing.Size(125, 21);
            this.comboBoxInstantIncrement.TabIndex = 24;
            // 
            // checkBoxShowBase
            // 
            this.checkBoxShowBase.AutoSize = true;
            this.checkBoxShowBase.Location = new System.Drawing.Point(250, 165);
            this.checkBoxShowBase.Name = "checkBoxShowBase";
            this.checkBoxShowBase.Size = new System.Drawing.Size(101, 17);
            this.checkBoxShowBase.TabIndex = 23;
            this.checkBoxShowBase.Text = "show base data";
            this.checkBoxShowBase.UseVisualStyleBackColor = true;
            // 
            // dateSelector1
            // 
            this.dateSelector1.Location = new System.Drawing.Point(424, 8);
            this.dateSelector1.Name = "dateSelector1";
            this.dateSelector1.ShowTime = false;
            this.dateSelector1.Size = new System.Drawing.Size(360, 126);
            this.dateSelector1.TabIndex = 22;
            this.dateSelector1.Validating += new System.ComponentModel.CancelEventHandler(this.dateSelector1_Validating);
            // 
            // listBoxInterval
            // 
            this.listBoxInterval.Location = new System.Drawing.Point(248, 24);
            this.listBoxInterval.Name = "listBoxInterval";
            this.listBoxInterval.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.listBoxInterval.Size = new System.Drawing.Size(168, 95);
            this.listBoxInterval.TabIndex = 21;
            this.listBoxInterval.SelectedIndexChanged += new System.EventHandler(this.listBoxInterval_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(248, 8);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(104, 16);
            this.label1.TabIndex = 20;
            this.label1.Text = "time series interval";
            // 
            // treeView1
            // 
            this.treeView1.BackColor = System.Drawing.SystemColors.Window;
            this.treeView1.DefaultToolTipProvider = null;
            this.treeView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeView1.DragDropMarkColor = System.Drawing.Color.Black;
            this.treeView1.LineColor = System.Drawing.SystemColors.ControlDark;
            this.treeView1.Location = new System.Drawing.Point(0, 0);
            this.treeView1.Model = null;
            this.treeView1.Name = "treeView1";
            this.treeView1.RowHeight = 20;
            this.treeView1.SelectedNode = null;
            this.treeView1.Size = new System.Drawing.Size(378, 328);
            this.treeView1.TabIndex = 35;
            this.treeView1.Text = "treeView1";
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "");
            this.imageList1.Images.SetKeyName(1, "");
            this.imageList1.Images.SetKeyName(2, "");
            this.imageList1.Images.SetKeyName(3, "");
            this.imageList1.Images.SetKeyName(4, "");
            this.imageList1.Images.SetKeyName(5, "");
            this.imageList1.Images.SetKeyName(6, "");
            this.imageList1.Images.SetKeyName(7, "performance.ico");
            this.imageList1.Images.SetKeyName(8, "selectedSeries.bmp");
            // 
            // buttonAddSelected
            // 
            this.buttonAddSelected.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonAddSelected.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonAddSelected.Location = new System.Drawing.Point(5, 111);
            this.buttonAddSelected.Name = "buttonAddSelected";
            this.buttonAddSelected.Size = new System.Drawing.Size(43, 39);
            this.buttonAddSelected.TabIndex = 40;
            this.buttonAddSelected.Text = "->";
            this.buttonAddSelected.Click += new System.EventHandler(this.buttonAddSelected_Click);
            // 
            // buttonRemove
            // 
            this.buttonRemove.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonRemove.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonRemove.Location = new System.Drawing.Point(5, 156);
            this.buttonRemove.Name = "buttonRemove";
            this.buttonRemove.Size = new System.Drawing.Size(43, 39);
            this.buttonRemove.TabIndex = 42;
            this.buttonRemove.Text = "<-";
            this.buttonRemove.Click += new System.EventHandler(this.buttonRemove_Click);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.splitContainer1.Location = new System.Drawing.Point(8, 247);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.treeView1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.selectedSeriesListBox1);
            this.splitContainer1.Panel2.Controls.Add(this.panel1);
            this.splitContainer1.Size = new System.Drawing.Size(815, 330);
            this.splitContainer1.SplitterDistance = 380;
            this.splitContainer1.SplitterWidth = 8;
            this.splitContainer1.TabIndex = 43;
            // 
            // selectedSeriesListBox1
            // 
            this.selectedSeriesListBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.selectedSeriesListBox1.Location = new System.Drawing.Point(55, 0);
            this.selectedSeriesListBox1.Margin = new System.Windows.Forms.Padding(0);
            this.selectedSeriesListBox1.Name = "selectedSeriesListBox1";
            this.selectedSeriesListBox1.Size = new System.Drawing.Size(370, 328);
            this.selectedSeriesListBox1.TabIndex = 41;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.buttonAddSelected);
            this.panel1.Controls.Add(this.buttonRemove);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(55, 328);
            this.panel1.TabIndex = 43;
            // 
            // SeriesSelection
            // 
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.groupBox1);
            this.MinimumSize = new System.Drawing.Size(826, 500);
            this.Name = "SeriesSelection";
            this.Size = new System.Drawing.Size(826, 582);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }
        #endregion

        bool m_showBase = false;
        bool m_getModeledData = false;
        int mrid;
        private void buttonRefresh_Click(object sender, System.EventArgs e)
        {
            Cursor = Cursors.WaitCursor;
            try
            {
                m_showBase = this.checkBoxShowBase.Checked;
                m_getModeledData = this.checkBoxGetMRID.Checked;
                if (m_getModeledData)
                { mrid = Convert.ToInt32(this.selectedMRID.Text); }

                string[] intervalDescriptions = new string[listBoxInterval.SelectedItems.Count];
                listBoxInterval.SelectedItems.CopyTo(intervalDescriptions, 0);

                int[] categories = new int[listBoxCategory.SelectedIndices.Count];
                listBoxCategory.SelectedIndices.CopyTo(categories, 0);
                DataTable tbl = Hdb.Instance.ObjectTypesTable;
                for (int i = 0; i < categories.Length; i++)
                { // switch from indices to objecttype_id
                    categories[i] = Convert.ToInt32(tbl.Rows[categories[i]]["objecttype_id"]);
                }
                this.dateSelector1.SaveToDataSet(this.graphDef);
                if (m_getModeledData)
                {
                    siteTableFiltered = Hdb.Instance.FilteredSiteList(this.textBoxKeyWords.Text,
                                       intervalDescriptions,
                                       categories, comboBoxBasin.SelectedValue.ToString(),m_getModeledData,mrid);
                }
                else
                {
                    siteTableFiltered = Hdb.Instance.FilteredSiteList(this.textBoxKeyWords.Text,
                                       intervalDescriptions,
                                       categories, comboBoxBasin.SelectedValue.ToString());
                }
              //  CsvFile.Write( siteTableFiltered,@"c:\temp\site.csv");
                LoadTree();
            }
            finally
            {
                Cursor = Cursors.Default;
            }

        }

       
        void treeView1_Expanding(object sender, TreeViewAdvEventArgs e)
        {
            try
            {
                HdbNode n = e.Node.Tag as HdbNode;
                Cursor = Cursors.WaitCursor;
                List<string> intervalDescriptions = new List<string>();
                for (int i = 0; i < listBoxInterval.SelectedItems.Count; i++)
                {
                    intervalDescriptions.Add(listBoxInterval.SelectedItems[i].ToString());
                }

                if (e.Node.Tag == null)
                    return;

                HdbNode selectedNode = e.Node.Tag as HdbNode;
                if (selectedNode.Nodes.Count == 1 && selectedNode.Tag != null
                    && selectedNode.Nodes[0].Text == "expand_this_site")
                {

                    AddSiteInventoryToTree(intervalDescriptions.ToArray(), selectedNode, m_showBase);
                }
                if (n.Text != "HDB Sites")
                {
                    //UpdateSelectedSeries();
                }
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }

        private void AddSiteInventoryToTree(string[] intervalDescriptions, HdbNode selectedNode, bool showBase)
        {
            DataRow row = (DataRow)selectedNode.Tag;
            int site_id = Convert.ToInt32(row["site_id"]);
            
            m_getModeledData = this.checkBoxGetMRID.Checked;
            if (m_getModeledData)
            { mrid = Convert.ToInt32(this.selectedMRID.Text); }

            DataTable tblSiteInfo = Hdb.Instance.SiteInfo(site_id, intervalDescriptions, showBase, m_getModeledData, mrid);
            //  CsvFile.Write( tblSiteInfo,@"c:\temp\siteInfo.csv");

            DataTable tblUnique = DataTableUtility.SelectDistinct(tblSiteInfo, "interval_Text");
            bool multipleDataTypes = (tblUnique.Rows.Count > 1);
            selectedNode.Nodes.Clear();

            if (multipleDataTypes)
            {// add tree structure to organize data into daily,hourly, monthly,etc...
                foreach (DataRow r in tblUnique.Rows)
                {
                    string s = r[0].ToString();
                    HdbNode n = new HdbNode(s);
                    selectedNode.Nodes.Add(n);
                }
            }

            for (int interval = 0; interval < tblUnique.Rows.Count; interval++)
            {
                DataTable tbl;
                if (multipleDataTypes)
                {
                    tbl = DataTableUtility.Select(tblSiteInfo, "interval_Text = '" + tblUnique.Rows[interval][0].ToString() + "'", "");
                }
                else
                {
                    tbl = tblSiteInfo;
                }
                for (int i = 0; i < tbl.Rows.Count; i++)
                {
                    string text = "";
                    if (!multipleDataTypes)
                    {
                        text = tblUnique.Rows[0][0] + " : ";
                    }
                    text += (string)tbl.Rows[i]["DATATYPE_COMMON_NAME"];
                    text += " (" + tbl.Rows[i]["DATATYPE_ID"] + ")";
                    text += " " + tbl.Rows[i]["COUNT(A.VALUE)"].ToString() + "";
                    text += " records";
                    text += " " + ((DateTime)tbl.Rows[i]["min(start_date_time)"]).ToString("MMM-dd-yyyy")
                         + " --> "
                         + ((DateTime)tbl.Rows[i]["max(start_date_time)"]).ToString("MMM-dd-yyyy");
                    text += " (" + tbl.Rows[i]["site_datatype_id"] + ") ";

                    HdbNode node = new HdbNode(text);
                    node.Tag = tbl.Rows[i];
                    node.Icon = GetIcon();
                    if (multipleDataTypes)
                    {
                        selectedNode.Nodes[interval].Nodes.Add(node);
                    }
                    else
                    {
                        selectedNode.Nodes.Add(node);
                    }
                }
            }
            //root.ExpandAll();
        }

        private Image GetIcon()
        {
            return imageList1.Images[8];
        }

        /// <summary>
        /// Adds Selected series to the graph graph.
        /// </summary>
        void AddSelectedSeries()
        {
            try
            {
                int count = treeView1.SelectedNodes.Count;
                
                 for (int i = 0; i < treeView1.SelectedNodes.Count; i++)
                {

                    HdbNode node = treeView1.SelectedNodes[i].Tag as HdbNode;
                    if (node.Nodes.Count == 0 && node.Parent != null && node.Parent.Parent != null)
                    {
                        DataRow r_tableRow = (DataRow)node.Tag;
                        TimeSeriesDataSet.SeriesRow row = graphDef.NewSeriesRow();

                        row.SiteName = (string)r_tableRow["site_common_name"];
                        row.SiteName = row.SiteName.Replace(","," ");
                        row.Interval = (string)r_tableRow["interval"];
                        row.Source = "HDB";
                        row.TagInfo = "";
                        row.Units = (string)r_tableRow["unit_common_name"];
                        row.ParameterType = (string)r_tableRow["datatype_common_name"];
                        row.hdb_site_datatype_id = (decimal)r_tableRow["site_datatype_id"];
                        row.hdb_r_table = (string)r_tableRow["rtable"];
                        row.ReadOnly = true;
                        row.GraphNumber = graphDef.GraphRow.GraphNumber;
                        graphDef.AddSeriesRow(row);
                    }
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.ToString());
            }

            this.selectedSeriesListBox1.SetDataSource(graphDef);
        //    this.statusBar1.Text = graphDef.Series.Rows.Count + " selected";
        }
       

        private void SaveSelections()
        {
           
            this.dateSelector1.SaveToDataSet(graphDef);
            SaveInstantInterval();

            Settings.Default.SelectedObjectTypes.Clear();
            foreach (var item in listBoxCategory.SelectedItems)
            {
                Settings.Default.SelectedObjectTypes.Add(item.ToString());
            }

            Settings.Default.SelectedIntervals.Clear();
            foreach (var item in listBoxInterval.SelectedItems)
            {
                Settings.Default.SelectedIntervals.Add(item.ToString());
            }
            graphDef.GraphRow.TimeZone = timeZoneComboBox1.TimeZone;
        }

        private void SaveInstantInterval()
        {
                graphDef.GraphRow.InstantInterval = Convert.ToInt32(comboBoxInstantIncrement.SelectedItem);
            
        }

        private void dateSelector1_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (graphDef.BeginingTime() >
              graphDef.EndingTime())
            {
                //e.Cancel=true;
                //MessageBox.Show("Error: Ending date must be larger than start date");
            }
        }

        


        private void listBoxInterval_SelectedIndexChanged(object sender, EventArgs e)
        {
            string[] intervalDescriptions = new string[listBoxInterval.SelectedItems.Count];
            listBoxInterval.SelectedItems.CopyTo(intervalDescriptions, 0);

            if (Array.IndexOf(intervalDescriptions, "hour") >= 0
                || Array.IndexOf(intervalDescriptions, "instant") >= 0)
            {
                this.dateSelector1.ShowTime = true;
            }
            else
            {
                this.dateSelector1.ShowTime = false;
            }

            if( intervalDescriptions.Length == 1)
            {

                string interval = intervalDescriptions[0];
                SetDefaultDates(interval);

            }

            if (Array.IndexOf(intervalDescriptions, "instant") >= 0)
            {
                this.comboBoxInstantIncrement.Enabled = true;
            }
            else
            {
                this.comboBoxInstantIncrement.Enabled = false;
            }


        }

        private void SetDefaultDates(string interval)
        {

            //instant  (most recent 7 days)
            //hour (most recent 7 days)
            //day (most recent month)
            //month (most recent 12 months)
            //year (most recent 5 years)
            //wyear (most recent 5 years)

            // Set default range of dates.
            DateTime t1 = DateTime.Now;
            DateTime t2 = DateTime.Now;
            switch (interval)
            {
                case "instant":
                    t1 = t1.AddDays(-7);
                    break;
                case "hour":
                    t1 = t1.AddDays(-7);
                    break;
                case "day":
                    t1 = t1.AddMonths(-1);
                    break;
                case "month":
                    t1 = t1.AddMonths(-12);
                    break;

                case "year":
                    t1 = t1.AddYears(-5);
                    break;
                case "wy":
                    t1 = t1.AddYears(-5);
                    break;

                default:
                    break;
            }
            this.dateSelector1.dateTimePickerFrom.Value = t1;
            this.dateSelector1.dateTimePickerFrom2.Value = t1;
            this.dateSelector1.dateTimePickerTo.Value = t2;
            TimeSpan ts = new TimeSpan(t2.Ticks-t1.Ticks);
            this.dateSelector1.numericUpDownDays.Value = ts.Days;

        }

       

        private void buttonAddSelected_Click(object sender, EventArgs e)
        {
            AddSelectedSeries();
        }

        private void buttonRemove_Click(object sender, EventArgs e)
        {
            this.selectedSeriesListBox1.RemoveSelected();
        }

        private void comboBoxInstantIncrement_SelectedIndexChanged(object sender, EventArgs e)
        {
            SaveInstantInterval();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }



    }
}
