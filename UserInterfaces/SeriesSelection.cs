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
        private Label label4;
        private ComboBox comboBoxInstantIncrement;
        private Aga.Controls.Tree.TreeViewAdv treeView1;
        private ImageList imageList1;
        private Label label5;
        private Label label6;
        private ComboBox comboBoxBasin;
        private Button buttonAddSelected;
        public SelectedSeriesListBox selectedSeriesListBox1;
        private Button buttonRemove;
        private SplitContainer splitContainer1;
        private Panel panel1;
        private TextBox selectedMRID;
        private Label label8;
        private RadioButton radioGetMRID;
        private RadioButton radioGetRData;
        private TimeZoneComboBox timeZoneComboBox2;
        private ComboBox comboBoxModelId;
        private ComboBox comboBoxMrid;
        private Label label10;
        private Label label9;
        private Button buttonRemoveAll;
        private Button buttonAddAll;
        private CheckBox checkBoxSelectAll;
        private GroupBox groupBoxDataType;
        private CheckBox sdidSearchCheckBox;
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
            SetupModelIdList();
            SetupDataTypeSelection(dataSet);
            DataSet = dataSet;
            timeZoneComboBox2.SelectedIndexChanged += new EventHandler<EventArgs>(timeZoneComboBox2_SelectedIndexChanged);
            dateSelector1.ValueChanged += new EventHandler<EventArgs>(dateSelector1_ValueChanged);
            comboBoxInstantIncrement.SelectedIndexChanged += new EventHandler(comboBoxInstantIncrement_SelectedIndexChanged);
            radioGetRData.CheckedChanged += new System.EventHandler(this.radioGetRData_CheckedChanged);
            radioGetMRID.CheckedChanged += new System.EventHandler(this.radioGetMRID_CheckedChanged);
            
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
            treeView1.NodeMouseDoubleClick += new EventHandler<TreeNodeAdvMouseEventArgs>(treeView1_NodeMouseDoubleClick);
            treeView1.NodeMouseClick += new EventHandler<TreeNodeAdvMouseEventArgs>(treeView1_NodeMouseClick);

            model = new TreeModel();

            selectedSeriesListBox1.SetDataSource( dataSet);
        }

        private void SetupDataTypeSelection(GraphData dataSet)
        {
            try
            {
                var mrid = dataSet.SeriesRows.CopyToDataTable().Rows[0]["model_run_id"].ToString();
                if (mrid != "0" && mrid != "")
                {
                    this.radioGetMRID.Checked = true;
                    this.radioGetRData.Checked = false;
                    this.selectedMRID.Enabled = true;
                    this.selectedMRID.Text = mrid;
                }
                else
                {
                    this.radioGetMRID.Checked = false;
                    this.radioGetRData.Checked = true;
                    this.selectedMRID.Enabled = false;
                }
            }
            catch
            { }
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

        void timeZoneComboBox2_SelectedIndexChanged(object sender, EventArgs e)
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
            if (Array.IndexOf(intervals, "15") >= 0)
            {
                comboBoxInstantIncrement.SelectedItem = "15";
            }
            else
            {
                comboBoxInstantIncrement.SelectedIndex = 0;
            }
        }

        internal GraphData DataSet
        {
            get
            {
                graphDef.GraphRow.TimeZone = timeZoneComboBox2.TimeZone;
                return this.graphDef;
            }
            set
            {
               this.graphDef = value;
               this.dateSelector1.ReadFromDataSet(this.graphDef);

               this.timeZoneComboBox2.TimeZone = value.GraphRow.TimeZone;
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

        //           row["site_name"] = s.SiteName;
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

        private void SetupModelIdList()
        {
            this.comboBoxModelId.Items.Clear();
            this.comboBoxModelId.DataSource = Hdb.Instance.getModelIds();
            this.comboBoxModelId.ValueMember = "ModelId";
            this.comboBoxModelId.DisplayMember = "comboBoxCaption";

            ComboBox senderComboBox = this.comboBoxModelId;
            int width = senderComboBox.DropDownWidth;
            Graphics g = senderComboBox.CreateGraphics();
            Font font = senderComboBox.Font;
            int vertScrollBarWidth =
                (senderComboBox.Items.Count > senderComboBox.MaxDropDownItems)
                ? SystemInformation.VerticalScrollBarWidth : 0;

            int newWidth;
            var dTab = Hdb.Instance.getModelIds();
            for (int i = 0; i < dTab.Rows.Count; i++)
            {
                string s = dTab.Rows[i][dTab.Columns.Count - 1].ToString();
                newWidth = (int)g.MeasureString(s, font).Width
                    + vertScrollBarWidth;
                if (width < newWidth)
                { width = newWidth; }
            }
            senderComboBox.DropDownWidth = width;
        }

        private void SetupModelRunIdList()
        {
            this.comboBoxMrid.DataSource = null;
            this.comboBoxMrid.Items.Clear();
            this.comboBoxMrid.DataSource = Hdb.Instance.getModelRunIds(Convert.ToInt32(this.comboBoxModelId.SelectedValue));
            this.comboBoxMrid.ValueMember = "Mrid";
            this.comboBoxMrid.DisplayMember = "comboBoxCaption";

            ComboBox senderComboBox = this.comboBoxMrid;
            int width = senderComboBox.DropDownWidth;
            Graphics g = senderComboBox.CreateGraphics();
            Font font = senderComboBox.Font;
            int vertScrollBarWidth =
                (senderComboBox.Items.Count > senderComboBox.MaxDropDownItems)
                ? SystemInformation.VerticalScrollBarWidth : 0;

            int newWidth, maxWidth = 156;
            var dTab = Hdb.Instance.getModelRunIds(Convert.ToInt32(this.comboBoxModelId.SelectedValue));
            for (int i = 0; i < dTab.Rows.Count; i++)
            {
                string s = dTab.Rows[i][dTab.Columns.Count - 1].ToString();
                newWidth = (int)g.MeasureString(s, font).Width
                    + vertScrollBarWidth;
                if (maxWidth < newWidth)
                { maxWidth = newWidth; }
            }
            senderComboBox.DropDownWidth = maxWidth;
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
                node = new HdbNode((string)siteTableFiltered.Rows[i]["site_name"]);
                node.Text = node.Text + " (SiteID=" + siteTableFiltered.Rows[i]["SITE_ID"].ToString() + ")";
                node.Tag = this.siteTableFiltered.Rows[i];
                node.Nodes.Add( new HdbNode("expand_this_site"));
                node.Icon = GetIcon(siteTableFiltered.Rows[i]["OBJECTTYPE_ID"].ToString());
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
            this.sdidSearchCheckBox = new System.Windows.Forms.CheckBox();
            this.groupBoxDataType = new System.Windows.Forms.GroupBox();
            this.radioGetRData = new System.Windows.Forms.RadioButton();
            this.selectedMRID = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.radioGetMRID = new System.Windows.Forms.RadioButton();
            this.comboBoxMrid = new System.Windows.Forms.ComboBox();
            this.comboBoxModelId = new System.Windows.Forms.ComboBox();
            this.checkBoxSelectAll = new System.Windows.Forms.CheckBox();
            this.timeZoneComboBox2 = new HdbPoet.TimeZoneComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.comboBoxBasin = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.comboBoxInstantIncrement = new System.Windows.Forms.ComboBox();
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
            this.buttonRemoveAll = new System.Windows.Forms.Button();
            this.buttonAddAll = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBoxDataType.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Location = new System.Drawing.Point(8, 14);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(237, 19);
            this.label2.TabIndex = 11;
            this.label2.Text = "Site Name Search";
            // 
            // textBoxKeyWords
            // 
            this.textBoxKeyWords.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxKeyWords.Location = new System.Drawing.Point(8, 34);
            this.textBoxKeyWords.Name = "textBoxKeyWords";
            this.textBoxKeyWords.Size = new System.Drawing.Size(235, 20);
            this.textBoxKeyWords.TabIndex = 1;
            this.textBoxKeyWords.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBoxKeyWords_KeyDown);
            // 
            // buttonRefresh
            // 
            this.buttonRefresh.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonRefresh.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.buttonRefresh.Cursor = System.Windows.Forms.Cursors.Hand;
            this.buttonRefresh.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.buttonRefresh.FlatAppearance.BorderSize = 2;
            this.buttonRefresh.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonRefresh.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonRefresh.ForeColor = System.Drawing.SystemColors.Desktop;
            this.buttonRefresh.Location = new System.Drawing.Point(252, 182);
            this.buttonRefresh.Name = "buttonRefresh";
            this.buttonRefresh.Size = new System.Drawing.Size(169, 38);
            this.buttonRefresh.TabIndex = 11;
            this.buttonRefresh.Text = "Create Site List";
            this.buttonRefresh.UseVisualStyleBackColor = false;
            this.buttonRefresh.Click += new System.EventHandler(this.buttonRefresh_Click);
            // 
            // label3
            // 
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Location = new System.Drawing.Point(6, 57);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(237, 16);
            this.label3.TabIndex = 7;
            this.label3.Text = "Object Type";
            // 
            // listBoxCategory
            // 
            this.listBoxCategory.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listBoxCategory.Location = new System.Drawing.Point(8, 74);
            this.listBoxCategory.Name = "listBoxCategory";
            this.listBoxCategory.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.listBoxCategory.Size = new System.Drawing.Size(235, 147);
            this.listBoxCategory.TabIndex = 2;
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.sdidSearchCheckBox);
            this.groupBox1.Controls.Add(this.groupBoxDataType);
            this.groupBox1.Controls.Add(this.checkBoxSelectAll);
            this.groupBox1.Controls.Add(this.timeZoneComboBox2);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.comboBoxBasin);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.comboBoxInstantIncrement);
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
            this.groupBox1.Size = new System.Drawing.Size(1088, 233);
            this.groupBox1.TabIndex = 30;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Site List Criteria";
            // 
            // sdidSearchCheckBox
            // 
            this.sdidSearchCheckBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.sdidSearchCheckBox.BackColor = System.Drawing.Color.Transparent;
            this.sdidSearchCheckBox.Location = new System.Drawing.Point(162, 14);
            this.sdidSearchCheckBox.Name = "sdidSearchCheckBox";
            this.sdidSearchCheckBox.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.sdidSearchCheckBox.Size = new System.Drawing.Size(81, 19);
            this.sdidSearchCheckBox.TabIndex = 42;
            this.sdidSearchCheckBox.Text = "Use SDI";
            this.sdidSearchCheckBox.UseVisualStyleBackColor = false;
            // 
            // groupBoxDataType
            // 
            this.groupBoxDataType.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBoxDataType.Controls.Add(this.radioGetRData);
            this.groupBoxDataType.Controls.Add(this.selectedMRID);
            this.groupBoxDataType.Controls.Add(this.label10);
            this.groupBoxDataType.Controls.Add(this.label8);
            this.groupBoxDataType.Controls.Add(this.label9);
            this.groupBoxDataType.Controls.Add(this.radioGetMRID);
            this.groupBoxDataType.Controls.Add(this.comboBoxMrid);
            this.groupBoxDataType.Controls.Add(this.comboBoxModelId);
            this.groupBoxDataType.Location = new System.Drawing.Point(868, 8);
            this.groupBoxDataType.Name = "groupBoxDataType";
            this.groupBoxDataType.Size = new System.Drawing.Size(214, 219);
            this.groupBoxDataType.TabIndex = 41;
            this.groupBoxDataType.TabStop = false;
            this.groupBoxDataType.Text = "Data Type Selection";
            // 
            // radioGetRData
            // 
            this.radioGetRData.BackColor = System.Drawing.Color.Transparent;
            this.radioGetRData.Checked = true;
            this.radioGetRData.Location = new System.Drawing.Point(6, 19);
            this.radioGetRData.Name = "radioGetRData";
            this.radioGetRData.Size = new System.Drawing.Size(202, 19);
            this.radioGetRData.TabIndex = 33;
            this.radioGetRData.TabStop = true;
            this.radioGetRData.Text = "Real Data";
            this.radioGetRData.UseVisualStyleBackColor = false;
            // 
            // selectedMRID
            // 
            this.selectedMRID.Enabled = false;
            this.selectedMRID.Location = new System.Drawing.Point(114, 64);
            this.selectedMRID.Name = "selectedMRID";
            this.selectedMRID.Size = new System.Drawing.Size(94, 20);
            this.selectedMRID.TabIndex = 7;
            this.selectedMRID.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.selectedMRID.TextChanged += new System.EventHandler(this.selectedMRID_TextChanged);
            // 
            // label10
            // 
            this.label10.BackColor = System.Drawing.Color.Transparent;
            this.label10.Location = new System.Drawing.Point(5, 145);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(157, 18);
            this.label10.TabIndex = 39;
            this.label10.Text = "Model Run ID:";
            // 
            // label8
            // 
            this.label8.BackColor = System.Drawing.Color.Transparent;
            this.label8.Location = new System.Drawing.Point(21, 67);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(77, 18);
            this.label8.TabIndex = 32;
            this.label8.Text = "Model Run ID:";
            // 
            // label9
            // 
            this.label9.BackColor = System.Drawing.Color.Transparent;
            this.label9.Location = new System.Drawing.Point(5, 99);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(157, 18);
            this.label9.TabIndex = 38;
            this.label9.Text = "Model ID:";
            // 
            // radioGetMRID
            // 
            this.radioGetMRID.BackColor = System.Drawing.Color.Transparent;
            this.radioGetMRID.Location = new System.Drawing.Point(6, 39);
            this.radioGetMRID.Name = "radioGetMRID";
            this.radioGetMRID.Size = new System.Drawing.Size(202, 19);
            this.radioGetMRID.TabIndex = 34;
            this.radioGetMRID.Text = "Modeled Data";
            this.radioGetMRID.UseVisualStyleBackColor = false;
            // 
            // comboBoxMrid
            // 
            this.comboBoxMrid.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxMrid.Enabled = false;
            this.comboBoxMrid.FormattingEnabled = true;
            this.comboBoxMrid.Location = new System.Drawing.Point(6, 165);
            this.comboBoxMrid.MaxDropDownItems = 10;
            this.comboBoxMrid.Name = "comboBoxMrid";
            this.comboBoxMrid.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.comboBoxMrid.Size = new System.Drawing.Size(202, 21);
            this.comboBoxMrid.TabIndex = 37;
            this.comboBoxMrid.DropDown += new System.EventHandler(this.comboBoxMrid_OnDropDown);
            this.comboBoxMrid.SelectedIndexChanged += new System.EventHandler(this.comboBoxMrid_SelectedIndexChanged);
            // 
            // comboBoxModelId
            // 
            this.comboBoxModelId.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxModelId.Enabled = false;
            this.comboBoxModelId.FormattingEnabled = true;
            this.comboBoxModelId.Location = new System.Drawing.Point(5, 119);
            this.comboBoxModelId.MaxDropDownItems = 10;
            this.comboBoxModelId.Name = "comboBoxModelId";
            this.comboBoxModelId.Size = new System.Drawing.Size(203, 21);
            this.comboBoxModelId.TabIndex = 36;
            this.comboBoxModelId.SelectedIndexChanged += new System.EventHandler(this.comboBoxModelId_SelectedIndexChanged);
            // 
            // checkBoxSelectAll
            // 
            this.checkBoxSelectAll.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.checkBoxSelectAll.BackColor = System.Drawing.Color.Transparent;
            this.checkBoxSelectAll.Location = new System.Drawing.Point(162, 57);
            this.checkBoxSelectAll.Name = "checkBoxSelectAll";
            this.checkBoxSelectAll.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.checkBoxSelectAll.Size = new System.Drawing.Size(81, 16);
            this.checkBoxSelectAll.TabIndex = 40;
            this.checkBoxSelectAll.Text = "Select All";
            this.checkBoxSelectAll.UseVisualStyleBackColor = false;
            this.checkBoxSelectAll.CheckedChanged += new System.EventHandler(this.checkBoxSelectAll_CheckedChanged);
            // 
            // timeZoneComboBox2
            // 
            this.timeZoneComboBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.timeZoneComboBox2.Location = new System.Drawing.Point(577, 199);
            this.timeZoneComboBox2.Name = "timeZoneComboBox2";
            this.timeZoneComboBox2.Size = new System.Drawing.Size(286, 21);
            this.timeZoneComboBox2.TabIndex = 35;
            this.timeZoneComboBox2.TimeZone = "";
            // 
            // label6
            // 
            this.label6.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label6.BackColor = System.Drawing.Color.Transparent;
            this.label6.Location = new System.Drawing.Point(249, 136);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(184, 14);
            this.label6.TabIndex = 29;
            this.label6.Text = "Basin";
            // 
            // comboBoxBasin
            // 
            this.comboBoxBasin.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBoxBasin.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxBasin.FormattingEnabled = true;
            this.comboBoxBasin.Location = new System.Drawing.Point(252, 153);
            this.comboBoxBasin.Name = "comboBoxBasin";
            this.comboBoxBasin.Size = new System.Drawing.Size(181, 21);
            this.comboBoxBasin.TabIndex = 4;
            // 
            // label5
            // 
            this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label5.BackColor = System.Drawing.Color.Transparent;
            this.label5.Location = new System.Drawing.Point(574, 182);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(292, 14);
            this.label5.TabIndex = 26;
            this.label5.Text = "Time Zone";
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Location = new System.Drawing.Point(424, 182);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(144, 14);
            this.label4.TabIndex = 25;
            this.label4.Text = "Increment (min)";
            // 
            // comboBoxInstantIncrement
            // 
            this.comboBoxInstantIncrement.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBoxInstantIncrement.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxInstantIncrement.FormattingEnabled = true;
            this.comboBoxInstantIncrement.Location = new System.Drawing.Point(427, 199);
            this.comboBoxInstantIncrement.Name = "comboBoxInstantIncrement";
            this.comboBoxInstantIncrement.Size = new System.Drawing.Size(143, 21);
            this.comboBoxInstantIncrement.TabIndex = 9;
            // 
            // dateSelector1
            // 
            this.dateSelector1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.dateSelector1.Location = new System.Drawing.Point(424, 8);
            this.dateSelector1.Name = "dateSelector1";
            this.dateSelector1.ShowTime = false;
            this.dateSelector1.Size = new System.Drawing.Size(439, 126);
            this.dateSelector1.TabIndex = 5;
            this.dateSelector1.Validating += new System.ComponentModel.CancelEventHandler(this.dateSelector1_Validating);
            // 
            // listBoxInterval
            // 
            this.listBoxInterval.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.listBoxInterval.Location = new System.Drawing.Point(251, 25);
            this.listBoxInterval.Name = "listBoxInterval";
            this.listBoxInterval.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.listBoxInterval.Size = new System.Drawing.Size(170, 108);
            this.listBoxInterval.TabIndex = 3;
            this.listBoxInterval.SelectedIndexChanged += new System.EventHandler(this.listBoxInterval_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Location = new System.Drawing.Point(249, 8);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(145, 14);
            this.label1.TabIndex = 20;
            this.label1.Text = "Time Series Interval";
            // 
            // treeView1
            // 
            this.treeView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.treeView1.BackColor = System.Drawing.SystemColors.Window;
            this.treeView1.DefaultToolTipProvider = null;
            this.treeView1.DragDropMarkColor = System.Drawing.Color.Black;
            this.treeView1.LineColor = System.Drawing.SystemColors.ControlDark;
            this.treeView1.Location = new System.Drawing.Point(0, 0);
            this.treeView1.Model = null;
            this.treeView1.Name = "treeView1";
            this.treeView1.RowHeight = 20;
            this.treeView1.SelectedNode = null;
            this.treeView1.Size = new System.Drawing.Size(502, 447);
            this.treeView1.TabIndex = 12;
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
            this.imageList1.Images.SetKeyName(9, "elec.bmp");
            this.imageList1.Images.SetKeyName(10, "flow.bmp");
            this.imageList1.Images.SetKeyName(11, "length.bmp");
            this.imageList1.Images.SetKeyName(12, "pct.bmp");
            this.imageList1.Images.SetKeyName(13, "volume.bmp");
            this.imageList1.Images.SetKeyName(14, "user.bmp");
            this.imageList1.Images.SetKeyName(15, "dam.bmp");
            this.imageList1.Images.SetKeyName(16, "river.bmp");
            // 
            // buttonAddSelected
            // 
            this.buttonAddSelected.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonAddSelected.Location = new System.Drawing.Point(5, 71);
            this.buttonAddSelected.Name = "buttonAddSelected";
            this.buttonAddSelected.Size = new System.Drawing.Size(43, 39);
            this.buttonAddSelected.TabIndex = 13;
            this.buttonAddSelected.Text = "->";
            this.buttonAddSelected.Click += new System.EventHandler(this.buttonAddSelected_Click);
            // 
            // buttonRemove
            // 
            this.buttonRemove.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonRemove.Location = new System.Drawing.Point(5, 116);
            this.buttonRemove.Name = "buttonRemove";
            this.buttonRemove.Size = new System.Drawing.Size(43, 39);
            this.buttonRemove.TabIndex = 14;
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
            this.splitContainer1.Size = new System.Drawing.Size(1089, 448);
            this.splitContainer1.SplitterDistance = 504;
            this.splitContainer1.SplitterWidth = 8;
            this.splitContainer1.TabIndex = 43;
            // 
            // selectedSeriesListBox1
            // 
            this.selectedSeriesListBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.selectedSeriesListBox1.Location = new System.Drawing.Point(55, 0);
            this.selectedSeriesListBox1.Margin = new System.Windows.Forms.Padding(0);
            this.selectedSeriesListBox1.Name = "selectedSeriesListBox1";
            this.selectedSeriesListBox1.Size = new System.Drawing.Size(508, 447);
            this.selectedSeriesListBox1.TabIndex = 15;
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.panel1.Controls.Add(this.buttonRemoveAll);
            this.panel1.Controls.Add(this.buttonAddAll);
            this.panel1.Controls.Add(this.buttonAddSelected);
            this.panel1.Controls.Add(this.buttonRemove);
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(52, 447);
            this.panel1.TabIndex = 43;
            // 
            // buttonRemoveAll
            // 
            this.buttonRemoveAll.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonRemoveAll.Location = new System.Drawing.Point(5, 161);
            this.buttonRemoveAll.Name = "buttonRemoveAll";
            this.buttonRemoveAll.Size = new System.Drawing.Size(43, 59);
            this.buttonRemoveAll.TabIndex = 16;
            this.buttonRemoveAll.Text = "All <-";
            this.buttonRemoveAll.Click += new System.EventHandler(this.buttonRemoveAll_Click);
            // 
            // buttonAddAll
            // 
            this.buttonAddAll.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonAddAll.Location = new System.Drawing.Point(5, 6);
            this.buttonAddAll.Name = "buttonAddAll";
            this.buttonAddAll.Size = new System.Drawing.Size(43, 59);
            this.buttonAddAll.TabIndex = 15;
            this.buttonAddAll.Text = "All ->";
            this.buttonAddAll.Click += new System.EventHandler(this.buttonAddAll_Click);
            // 
            // SeriesSelection
            // 
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.groupBox1);
            this.MinimumSize = new System.Drawing.Size(1000, 500);
            this.Name = "SeriesSelection";
            this.Size = new System.Drawing.Size(1100, 700);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBoxDataType.ResumeLayout(false);
            this.groupBoxDataType.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }
        #endregion

        
        // If a node is double-clicked, open the file indicated by the TreeNode.
        void treeView1_NodeMouseDoubleClick(object sender, TreeNodeAdvMouseEventArgs e)
        {
            if (e.Node.Level == 0)
            {
                e.Node.Expand();
            }
            else
            {
                AddSelectedSeries();
            }
        }

        // If a node is clicked do something.
        void treeView1_NodeMouseClick(object sender, TreeNodeAdvMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {

            }
            // Get SDID detailed info for the node
            if (e.Button == MouseButtons.Right && e.Node.Children.Count == 0)
            {
                var hdbNode = e.Node.Tag as HdbNode;
                DataRow row = (DataRow)hdbNode.Tag;

                var isModeledData = GetModeledDataVars();
                var sdidInfo = Hdb.Instance.SiteInfo(Convert.ToInt32(row["site_id"]), new string[] { row["interval"].ToString() },
                    GlobalVariables.showBaseData, isModeledData.Item1, isModeledData.Item2, Convert.ToInt32(row["site_datatype_id"]));
                
                string text = "";
                text += (string)sdidInfo.Rows[0]["INTERVAL"] + " : ";
                text += (string)sdidInfo.Rows[0]["DATATYPE_COMMON_NAME"];
                text += " (DatatypeID=" + sdidInfo.Rows[0]["DATATYPE_ID"] + ")";
                text += " " + sdidInfo.Rows[0]["SDID_DESCRIPTOR"];
                if (sdidInfo.Rows[0]["min(start_date_time)"] != DBNull.Value || sdidInfo.Rows[0]["max(start_date_time)"] != DBNull.Value)
                {
                    text += " " + sdidInfo.Rows[0]["COUNT(A.VALUE)"].ToString() + " records";
                    text += " from ";
                    text += ((DateTime)sdidInfo.Rows[0]["min(start_date_time)"]).ToString("MMM-dd-yyyy")
                         + " to "
                         + ((DateTime)sdidInfo.Rows[0]["max(start_date_time)"]).ToString("MMM-dd-yyyy");
                }
                else
                {
                    text += " No data for selected interval";
                }
                text += " (SDI=" + sdidInfo.Rows[0]["site_datatype_id"] + ")";
                               
                hdbNode.Text = text;
            }
        }


        public Tuple<bool,int> GetModeledDataVars()
        {
            bool rVal = false;
            int mrid = 0;

            bool m_getModeledData = this.radioGetMRID.Checked;
            if (m_getModeledData)
            {
                rVal = true;
                if (this.selectedMRID.Text == "")
                { MessageBox.Show("Model Run ID cannot be blank if Modeled Data is selected..."); }
                else
                { mrid = Convert.ToInt32(this.selectedMRID.Text); }
            }
            return Tuple.Create(rVal, mrid);
        }


        private void buttonRefresh_Click(object sender, System.EventArgs e)
        {
            Cursor = Cursors.WaitCursor;
            try
            {
                var isModeledData = GetModeledDataVars();
                bool m_getModeledData = isModeledData.Item1;

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
                siteTableFiltered = Hdb.Instance.FilteredSiteList(this.textBoxKeyWords.Text,
                                   intervalDescriptions, categories, comboBoxBasin.SelectedValue.ToString(),
                                   this.sdidSearchCheckBox.Checked, m_getModeledData, isModeledData.Item2);
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
                    AddSiteInventoryToTree(intervalDescriptions.ToArray(), selectedNode, GlobalVariables.showBaseData);
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

            var isModeledData = GetModeledDataVars();

            DataTable tblSiteInfo;
            if (GlobalVariables.showSimpleSdiInfo && !isModeledData.Item1)
            {
                tblSiteInfo = Hdb.Instance.SiteInfoSimple(site_id, intervalDescriptions, showBase, isModeledData.Item1, isModeledData.Item2);
            }
            else
            {
                tblSiteInfo = Hdb.Instance.SiteInfo(site_id, intervalDescriptions, showBase, isModeledData.Item1, isModeledData.Item2);
            }
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
                    text += " (DatatypeID=" + tbl.Rows[i]["DATATYPE_ID"] + ")";
                    text += " " + tbl.Rows[i]["SDID_DESCRIPTOR"];
                    if (GlobalVariables.showSimpleSdiInfo)
                    {
                        text += "";
                    }
                    else if (tbl.Rows[i]["min(start_date_time)"] != DBNull.Value || tbl.Rows[i]["max(start_date_time)"] != DBNull.Value)
                    {
                        text += " " + tbl.Rows[i]["COUNT(A.VALUE)"].ToString() + " records";
                        text += " from ";
                        text += ((DateTime)tbl.Rows[i]["min(start_date_time)"]).ToString("MMM-dd-yyyy")
                             + " to "
                             + ((DateTime)tbl.Rows[i]["max(start_date_time)"]).ToString("MMM-dd-yyyy");
                    }
                    else
                    {
                        text += " No data for selected interval";
                    }
                    text += " (SDI=" + tbl.Rows[i]["site_datatype_id"] + ")";

                    HdbNode node = new HdbNode(text);
                    node.Tag = tbl.Rows[i];
                    node.Icon = GetIcon(tbl.Rows[i]["unit_common_name"].ToString());
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

        private Image GetIcon(string unitType = "")
        {
            int n;
            bool isNumeric = int.TryParse(unitType, out n);

            if (!isNumeric)
            {
                switch (unitType.ToLower())
                {
                    case "kw":
                    case "kwh":
                    case "mw":
                    case "mwh":
                    case "gw":
                    case "gwh":
                        return imageList1.Images[9];
                    case "cfs":
                        return imageList1.Images[10];
                    case "feet":
                        return imageList1.Images[11];
                    case "decimal":
                        return imageList1.Images[12];
                    case "acre-feet":
                        return imageList1.Images[13];
                    default:
                        return imageList1.Images[8];
                }
            }
            else
            {
                switch (unitType.ToLower())
                {
                    case "4":
                    case "301":
                    case "302":
                    case "303":
                    case "304":
                    case "305":
                    case "306":
                    case "307":
                    case "308":
                    case "309":
                    case "310":
                    case "330":
                        return imageList1.Images[14];
                    case "5":
                    case "7":
                    case "10":
                        return imageList1.Images[15];
                    case "1":
                    case "3":
                    case "6":
                    case "9":
                    case "11":
                    case "13":
                        return imageList1.Images[16];
                    default:
                        return imageList1.Images[0];
                }

            }
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
                        OracleHdb.TimeSeriesDataSet.SeriesRow row = graphDef.NewSeriesRow();

                        row.SiteName = (string)r_tableRow["site_name"];
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
                        if (this.selectedMRID.Text == "")
                        { row.model_run_id = 0; }
                        else
                        { row.model_run_id = Convert.ToDecimal(this.selectedMRID.Text); }
                        if (r_tableRow["sdid_descriptor"] == DBNull.Value)
                        { row.sdid_descriptor = ""; }
                        else
                        { row.sdid_descriptor = (string)r_tableRow["sdid_descriptor"]; }
                        graphDef.AddSeriesRow(row);
                    }
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.ToString());
            }

            this.selectedSeriesListBox1.SetDataSource(graphDef);
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
            graphDef.GraphRow.TimeZone = timeZoneComboBox2.TimeZone;
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
            bool isModeledData = this.radioGetMRID.Checked;

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
                SetDefaultDates(interval, isModeledData);
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

        private void SetDefaultDates(string interval, bool isModeledData)
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
            if (!isModeledData)
            {
                switch (interval)
                {
                    case "instant": t1 = t1.AddDays(-7);
                        break;
                    case "hour": t1 = t1.AddDays(-7);
                        break;
                    case "day": t1 = t1.AddMonths(-1);
                        break;
                    case "month": t1 = t1.AddMonths(-12);
                        break;
                    case "year": t1 = t1.AddYears(-5);
                        break;
                    case "wy": t1 = t1.AddYears(-5);
                        break;
                    default:
                        break;
                }
                TimeSpan ts = new TimeSpan(t2.Ticks - t1.Ticks); 
                this.dateSelector1.numericUpDownDays.Value = ts.Days;
                this.dateSelector1.dateTimePickerFrom2.Value = t1;
            }
            else
            {
                switch (interval)
                {
                    case "instant": t2 = t1.AddDays(7);
                        break;
                    case "hour": t2 = t1.AddDays(7);
                        break;
                    case "day": t2 = t1.AddMonths(1);
                        break;
                    case "month": t2 = t1.AddMonths(12);
                        break;
                    case "year": t2 = t1.AddYears(5);
                        break;
                    case "wy": t2 = t1.AddYears(5);
                        break;
                    default:
                        break;
                }
            }
            this.dateSelector1.dateTimePickerFrom.Value = t1;
            this.dateSelector1.dateTimePickerTo.Value = t2;
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

        private void selectedMRID_TextChanged(object sender, EventArgs e)
        {
            // Clear ListBoxes on change
            this.selectedSeriesListBox1.ClearListBox();
            model = new TreeModel();
            this.treeView1.Model = model;
        }

        private void radioGetMRID_CheckedChanged(object sender, EventArgs e)
        {
            // Clear ListBoxes on change
            this.selectedSeriesListBox1.ClearListBox();
            model = new TreeModel();
            this.treeView1.Model = model;
            this.comboBoxModelId.Enabled = true;
            this.comboBoxMrid.Enabled = true;
            this.selectedMRID.Enabled = true;
            this.dateSelector1.radioButtonPreviousXDays.Enabled = false;
            this.dateSelector1.radioButtonXToToday.Enabled = false;
            this.dateSelector1.numericUpDownDays.Enabled = false;
            this.dateSelector1.dateTimePickerFrom2.Enabled = false;
            GlobalVariables.showEmptySdids = false;
            SetDefaultDates(this.listBoxInterval.SelectedItem.ToString(), this.radioGetMRID.Checked);
        }

        private void radioGetRData_CheckedChanged(object sender, EventArgs e)
        {
            // Clear ListBoxes on change
            this.selectedSeriesListBox1.ClearListBox();
            model = new TreeModel();
            this.treeView1.Model = model;
            this.comboBoxModelId.Enabled = false;
            this.comboBoxMrid.Enabled = false;
            this.selectedMRID.Enabled = false;
            this.dateSelector1.radioButtonPreviousXDays.Enabled = true;
            this.dateSelector1.radioButtonXToToday.Enabled = true;
            this.dateSelector1.numericUpDownDays.Enabled = true;
            this.dateSelector1.dateTimePickerFrom2.Enabled = true;
            this.selectedMRID.Clear();
            SetDefaultDates(this.listBoxInterval.SelectedItem.ToString(), this.radioGetMRID.Checked);
        }              

        private void comboBoxModelId_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.comboBoxMrid.DataSource = null;            
        }

        private void comboBoxMrid_OnDropDown(object sender, EventArgs e)
        {
            if (comboBoxMrid.SelectedValue == null)
            { SetupModelRunIdList(); }
        }

        private void comboBoxMrid_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.selectedMRID.Text = null;
            if (comboBoxMrid.SelectedValue != null)
            { this.selectedMRID.Text = this.comboBoxMrid.SelectedValue.ToString(); }
        }

        private void buttonRemoveAll_Click(object sender, EventArgs e)
        {
            this.selectedSeriesListBox1.ClearListBox();
        }

        private void buttonAddAll_Click(object sender, EventArgs e)
        {
            treeView1.ExpandAll();
            treeView1.SelectAllNodes();
            AddSelectedSeries();
        }

        private void checkBoxSelectAll_CheckedChanged(object sender, EventArgs e)
        {
            if (this.checkBoxSelectAll.Checked)
            {
                for (int i = 0; i < listBoxCategory.Items.Count; i++)
                { listBoxCategory.SelectedIndices.Add(i); }
            }
            else
            {
                listBoxCategory.SelectedIndices.Clear();
            }
        }

        /// <summary>
        /// Handle ENTER key on the search box
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void textBoxKeyWords_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                buttonRefresh_Click(this, new EventArgs());
            }
        }
    }
}