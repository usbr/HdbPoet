using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using Reclamation.Core;
using System.Diagnostics;
using DgvFilterPopup;

namespace HdbPoet.MetaData
{

    /// <summary>
    /// SqlViewEditor manages editing a single table in a SQL based database. 
    /// The DataTable used in the interface can be composed as a SQL join to include additional read-only columns.
    /// However, only the single base table is persisted in the database.
    /// The first column must be the primary key
    /// Includes Features for filtering from http://www.codeproject.com/Articles/33786/DataGridView-Filter-Popup
    /// </summary>
    public partial class SqlViewEditor : UserControl
    {

        DataTable m_table;
        DataTable m_viewTable; 
        public SqlViewEditor()
        {
            InitializeComponent();
        }

        public static BasicDBServer Server { get; set; }

        private string baseTableName;

        private List<string> comboBoxTableNames = new List<string>();
        private List<string> valueMembers = new List<string>();
        private List<string> displayMembers = new List<string>();
        private List<int> columnIndexes = new List<int>();
        private List<bool> hideMemberValues = new List<bool>();

        private string sqlBase;
        private string sqlView;
        private bool insertPrimaryKey;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="baseTableName">name of table in database to be edited</param>
        /// <param name="sqlBase">sql to retrieve columns from baseTable</param>
        /// <param name="sqlView">sql command to retrieve  baseTable and additional columns</param>
        public void SetQueries(string baseTableName, string sqlBase, string sqlView, bool insertPrimaryKey=true)
        {
            this.insertPrimaryKey = insertPrimaryKey;
            this.baseTableName = baseTableName;
            this.sqlBase = sqlBase;
            this.sqlView = sqlView;
        }


        public bool LinkLabelPrimaryKey = false;

        public void AddDropDownColumn(string tableName, string valueMember, 
            string displayMember, int columnIndex, bool hideValueMember=false)
        {

            comboBoxTableNames.Add(tableName);
            valueMembers.Add(valueMember);
            displayMembers.Add(displayMember);
            columnIndexes.Add(columnIndex);
            hideMemberValues.Add(hideValueMember);
        }

        private void toolStripButtonExcel_Click(object sender, EventArgs e)
        {
            if (m_table == null || m_table.Rows.Count == 0)
                return;

            string filename = Path.ChangeExtension(Path.GetTempFileName(), ".csv");
            CsvFile.WriteToCSV(m_table, filename, false);
            Process.Start(filename);
        }

        private void toolStripButtonSave_Click(object sender, EventArgs e)
        {

            if (m_viewTable == null)
                return;
           
            m_table.Merge(m_viewTable, false, MissingSchemaAction.Ignore);
            try
            {
                Server.SaveTable(m_table);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            toolStripButtonRefresh_Click(this, EventArgs.Empty);

        }

        DgvFilterManager filterManager = new DgvFilterManager();

        private void toolStripButtonRefresh_Click(object sender, EventArgs e)
        {

            try
            {
                Cursor = Cursors.WaitCursor;
                Application.DoEvents();
                filterManager = null;
                filterManager = new DgvFilterManager();

                m_viewTable = Server.Table(baseTableName + "x", this.sqlView);
                m_table = Server.Table(baseTableName, "select * from " + baseTableName + " where 2 = 1");

                m_viewTable.TableNewRow += m_viewTable_TableNewRow;
                //var col = m_viewTable.Columns[0];
                //col.AllowDBNull = false;
                //col.AutoIncrement = true;
                //col.AutoIncrementSeed = -999;
                //col.AutoIncrementStep = 1;

                this.dataGridView1.DataSource = null;
                this.dataGridView1.Columns.Clear();
                if (LinkLabelPrimaryKey)
                {
                    var lc = new DataGridViewLinkColumn();
                    lc.DataPropertyName = m_table.Columns[0].ColumnName;
                    this.dataGridView1.Columns.Add(lc);
                }
                this.dataGridView1.DataSource = m_viewTable;
                this.dataGridView1.DataError += dataGridView1_DataError;

                // Make referenced columns readonly
                dataGridView1.Columns[0].ReadOnly = true; // primary key
                for (int i = 1; i < m_viewTable.Columns.Count; i++)
                {
                    string colName = m_viewTable.Columns[i].ColumnName;
                    if (m_table.Columns.IndexOf(colName) < 0)
                    {
                        dataGridView1.Columns[colName].ReadOnly = true;
                    }
                    else
                    {
                        dataGridView1.Columns[colName].ReadOnly = false;
                    }
                }


                for (int i = 0; i < comboBoxTableNames.Count; i++)
                {
                    var tbl = Server.Table(baseTableName, "select " + valueMembers[i]
                        + ", " + displayMembers[i] + " from " + comboBoxTableNames[i]);

                    DataGridViewComboBoxColumn c = new DataGridViewComboBoxColumn();

                    c.DataSource = tbl;
                    c.Name = displayMembers[i];
                    c.DataPropertyName = SourceValueName(i);
                    c.ValueMember = SourceValueName(i);
                    c.DisplayMember = displayMembers[i];

                    //DataGridViewTextBoxColumn tc = new DataGridViewTextBoxColumn();


                    if (hideMemberValues[i])
                    {
                        dataGridView1.Columns[SourceValueName(i)].Visible = false;
                    }

                    this.dataGridView1.Columns.Insert(columnIndexes[i], c);
                    //m_table.Columns.Add("s" + displayMembers[i]);

                }
                filterManager.DataGridView = dataGridView1;
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }

        void m_viewTable_TableNewRow(object sender, DataTableNewRowEventArgs e)
        {
            if (insertPrimaryKey)
            {
                e.Row[0] = -999;
            }
            //Console.WriteLine("hi");
        }

        void dataGridView1_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            
        }

        private string SourceValueName(int i)
        {
            var asName = valueMembers[i];
            var tokens = asName.Split(' ');

            if (tokens.Length == 2)
                asName = tokens[1];
            return asName;
        }

        public DataGridView DataGridView
        {
            get { return this.dataGridView1; }
        }

        public event EventHandler<DataGridViewCellEventArgs> CellClick;

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {

            if (CellClick != null)
                CellClick(this, e);
        }
    }
}
