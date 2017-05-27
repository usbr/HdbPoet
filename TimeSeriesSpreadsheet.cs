using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;
using Reclamation.Core;
using System.Collections.Generic;
using System.Drawing.Printing;

namespace HdbPoet
{
    /// <summary>
    /// TimeSeriesSpreadsheet displays a custom 
    /// DataTable (MultipleSeriesDataTable) 
    /// with custom font and coloring
    /// </summary>
    public class TimeSeriesSpreadsheet : System.Windows.Forms.UserControl
    {

        private System.Windows.Forms.DataGridView dataGrid1;
        private ContextMenuStrip contextMenuStrip1;
        private IContainer components;
        private ToolStripMenuItem copyToolStripMenuItem;
        private ToolStripMenuItem pasteToolStripMenuItem;
        private ToolStripSeparator toolStripMenuItem1;
        private ToolStripMenuItem deleteToolStripMenuItem;
        private ToolStripMenuItem toolStripMenuItemFormat;
        private ToolStripMenuItem toolStripMenuItemInterpolate;
        private ToolStripMenuItem menuDetails;
        MultipleSeriesDataTable msDataTable;


        public TimeSeriesSpreadsheet()
        {
            InitializeComponent();
            dataGrid1.CellFormatting += new DataGridViewCellFormattingEventHandler(dataGrid1_CellFormatting);
            dataGrid1.CellValueChanged += new DataGridViewCellEventHandler(dataGrid1_CellValueChanged);
            dataGrid1.SelectionChanged += new EventHandler(dataGrid1_SelectionChanged);
        }

        void dataGrid1_SelectionChanged(object sender, EventArgs e)
        {
            var sel = new DataGridSelection(dataGrid1);

            Logger.WriteLine(sel.ComputeSelectedStats(), "ui");
        }

        void dataGrid1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            //Console.WriteLine("Cell Changed "+e.RowIndex +", "+e.ColumnIndex);
        }

       

        internal DataViewRowState DataViewRowState
        {
            set
            {
                msDataTable.DefaultView.RowStateFilter = value;
            }
            get { return msDataTable.DefaultView.RowStateFilter; }
        }


        private string m_colorColumnName = "";
        internal void SetTable(MultipleSeriesDataTable table, string colorColumnName)
        {
            this.dataGrid1.DataSource = null;
            m_colorColumnName = colorColumnName;
            this.msDataTable = table;
            this.dataGrid1.DataSource = msDataTable;
            FormatDataGridView();
        }

        internal void SetColorColumnName(string columnName)
        {
            m_colorColumnName = columnName;
            this.dataGrid1.DataSource = null;
            this.dataGrid1.DataSource = msDataTable;
            FormatDataGridView();
        }


        private void FormatDataGridView()
        {
            this.dataGrid1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            if( dataGrid1.ColumnCount >0)
              this.dataGrid1.Columns[0].ReadOnly = true;

            for (int c = 1; c < dataGrid1.ColumnCount; c++)
            {
                var s = msDataTable.LookupSeries(c);
                dataGrid1.Columns[c].DefaultCellStyle.Format = msDataTable.LookupSeries(c).DisplayFormat;
                dataGrid1.Columns[c].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dataGrid1.Columns[c].ReadOnly = msDataTable.LookupSeries(c).ReadOnly;
                // adjust column widths to auto-fit
                dataGrid1.Columns[c].Width = Convert.ToInt16(System.Math.Max(System.Math.Max(
                        TextRenderer.MeasureText(msDataTable.LookupSeries(c).ParameterType, dataGrid1.Font).Width,
                        TextRenderer.MeasureText(msDataTable.LookupSeries(c).SiteName, dataGrid1.Font).Width / 2),
                        TextRenderer.MeasureText(msDataTable.LookupSeries(c).sdid_descriptor, dataGrid1.Font).Width
                        ) * 1.25);

            }

        }

        public void PasteFromClipBoard()
        {
            try
            {
                DataGridViewCell cell = this.dataGrid1.CurrentCell;

                if (cell != null)
                {
                    if (cell.ColumnIndex == 0)
                    {
                        MessageBox.Show("Pasting in the Date column is not supported");
                        return;
                    }
                }
                DataGridSelection sel = new DataGridSelection(this.dataGrid1);
                sel.Paste(ClipBoardUtility.GetDataTableFromClipboard());
                //DataGridViewUtility u = new DataGridViewUtility(this.dataGrid1);
                //u.PasteFromClipboard();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);   
            }
        }

        public void CopyToClipboard()
        {
            if (dataGrid1.SelectedCells.Count > 0)
            {
                DataObject o = dataGrid1.GetClipboardContent();
                if (o != null)
                    Clipboard.SetDataObject(o);
            }
        }

        void dataGrid1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.ColumnIndex == 0)
                return;

            object o = dataGrid1[0, e.RowIndex].Value;
            if (o != null)
            {
                
                DateTime t = (DateTime)o;
                //Console.WriteLine(t.ToString());
                Color background;
                bool bold;

                if (e.ColumnIndex - 1 >= msDataTable.Columns.Count)
                {
                    MessageBox.Show("Internal error: formatting cell error");
                    return;
                }

                msDataTable.GetColor(e.ColumnIndex - 1, t, out background, out bold,m_colorColumnName);

                DataGridViewCellStyle s = e.CellStyle;
               
               
               
                if (bold)
                {
                    Font f = s.Font;
                    Font f2 = new Font(f, FontStyle.Bold);
                    e.CellStyle.Font = f2;
                }
                s.BackColor = background;
            }
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
            this.dataGrid1 = new System.Windows.Forms.DataGridView();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.copyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pasteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemFormat = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemInterpolate = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.deleteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuDetails = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.dataGrid1)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dataGrid1
            // 
            this.dataGrid1.AllowUserToAddRows = false;
            this.dataGrid1.AllowUserToDeleteRows = false;
            this.dataGrid1.ContextMenuStrip = this.contextMenuStrip1;
            this.dataGrid1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGrid1.Location = new System.Drawing.Point(0, 0);
            this.dataGrid1.Name = "dataGrid1";
            this.dataGrid1.Size = new System.Drawing.Size(320, 368);
            this.dataGrid1.TabIndex = 33;
            this.dataGrid1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dataGrid1_KeyDown);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.copyToolStripMenuItem,
            this.pasteToolStripMenuItem,
            this.toolStripMenuItemFormat,
            this.toolStripMenuItemInterpolate,
            this.menuDetails,
            this.toolStripMenuItem1,
            this.deleteToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(157, 164);
            this.contextMenuStrip1.Opening += new System.ComponentModel.CancelEventHandler(this.contextMenuStrip1_Opening);
            // 
            // copyToolStripMenuItem
            // 
            this.copyToolStripMenuItem.Name = "copyToolStripMenuItem";
            this.copyToolStripMenuItem.Size = new System.Drawing.Size(156, 22);
            this.copyToolStripMenuItem.Text = "&Copy";
            this.copyToolStripMenuItem.Click += new System.EventHandler(this.copyToolStripMenuItem_Click);
            // 
            // pasteToolStripMenuItem
            // 
            this.pasteToolStripMenuItem.Name = "pasteToolStripMenuItem";
            this.pasteToolStripMenuItem.Size = new System.Drawing.Size(156, 22);
            this.pasteToolStripMenuItem.Text = "&Paste";
            this.pasteToolStripMenuItem.Click += new System.EventHandler(this.pasteToolStripMenuItem_Click);
            // 
            // toolStripMenuItemFormat
            // 
            this.toolStripMenuItemFormat.Name = "toolStripMenuItemFormat";
            this.toolStripMenuItemFormat.Size = new System.Drawing.Size(156, 22);
            this.toolStripMenuItemFormat.Text = "&Format";
            this.toolStripMenuItemFormat.Click += new System.EventHandler(this.toolStripMenuItemFormat_Click);
            // 
            // toolStripMenuItemInterpolate
            // 
            this.toolStripMenuItemInterpolate.Name = "toolStripMenuItemInterpolate";
            this.toolStripMenuItemInterpolate.Size = new System.Drawing.Size(156, 22);
            this.toolStripMenuItemInterpolate.Text = "Interpolated Fill";
            this.toolStripMenuItemInterpolate.Click += new System.EventHandler(this.toolStripMenuItemInterpolate_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(153, 6);
            // 
            // deleteToolStripMenuItem
            // 
            this.deleteToolStripMenuItem.Name = "deleteToolStripMenuItem";
            this.deleteToolStripMenuItem.Size = new System.Drawing.Size(156, 22);
            this.deleteToolStripMenuItem.Text = "&Delete";
            this.deleteToolStripMenuItem.Click += new System.EventHandler(this.deleteToolStripMenuItem_Click);
            // 
            // menuDetails
            // 
            this.menuDetails.Name = "menuDetails";
            this.menuDetails.Size = new System.Drawing.Size(156, 22);
            this.menuDetails.Text = "Show Details...";
            this.menuDetails.Click += new System.EventHandler(this.menuDetails_Click);
            // 
            // TimeSeriesSpreadsheet
            // 
            this.Controls.Add(this.dataGrid1);
            this.Name = "TimeSeriesSpreadsheet";
            this.Size = new System.Drawing.Size(320, 368);
            ((System.ComponentModel.ISupportInitialize)(this.dataGrid1)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);

        }
        #endregion

        private void copyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CopyToClipboard();
        }

        private void pasteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PasteFromClipBoard();
        }

        private void dataGrid1_KeyDown(object sender, KeyEventArgs e)
        {
            

            if (e.KeyCode == Keys.Delete && deleteToolStripMenuItem.Enabled )
            {
                DeleteSelectedCells();
               //e.Handled = true;      
                
            }
        }

        internal DataGridViewSelectedCellCollection  GetSelectedCells()
        {
            return this.dataGrid1.SelectedCells;
        }

        /// <summary>
        /// Delete selected cells, but not the date column
        /// </summary>
        private void DeleteSelectedCells()
        {
            for (int i = 0; i < dataGrid1.SelectedCells.Count; i++)
            {
                DataGridViewCell cell = dataGrid1.SelectedCells[i];
                if (cell.ColumnIndex != 0 && cell.Value != DBNull.Value)
                {
                    DataRow row = ((DataRowView)cell.OwningRow.DataBoundItem).Row;
                    row[cell.ColumnIndex] = DBNull.Value;
                }
            }
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DeleteSelectedCells();
        }


        internal void SelectCell(int rowIndex, int columnIndex)
        {
            DataGridViewCell cell = dataGrid1[columnIndex, rowIndex];
            dataGrid1.CurrentCell = cell;
        }

        internal void AllowEdits(bool enable)
        {
            this.pasteToolStripMenuItem.Enabled = enable;
            this.deleteToolStripMenuItem.Enabled = enable;
        }

        //private void dataGrid1_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        //{


        //    if (e.Button == MouseButtons.Left)
        //    {
        //        //toolStripMenuItemFormat.Visible = true;
        //        //  e.ColumnIndex 
        //    }
        //}



        private void toolStripMenuItemFormat_Click(object sender, EventArgs e)
        {

            var f = new ColumnFormat();

            var seriesNames = new List<string>();
            for (int i = 1; i < dataGrid1.ColumnCount; i++)
			{
			 seriesNames.Add(dataGrid1.Columns[i].HeaderText);
			}

            f.SetData(seriesNames.ToArray(), msDataTable.DisplayFormat);
            if (f.ShowDialog() == DialogResult.OK)
            {
                msDataTable.DisplayFormat = f.DisplayFormat;
                for (int i = 1; i < dataGrid1.ColumnCount; i++)
                {
                    dataGrid1.Columns[i].DefaultCellStyle.Format = f.DisplayFormat[i-1];
                }
            }
        }

        internal void Print(string title)
        {
            var e = new ExcelReportTemplate(msDataTable);
            e.PrintPreview();
        }

        private void toolStripMenuItemInterpolate_Click(object sender, EventArgs e)
        {
            var interpolate = new DataGridSelection(dataGrid1);

            interpolate.Interpolate();
        }

        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {
            var interpolate = new DataGridSelection(dataGrid1);

            this.toolStripMenuItemInterpolate.Enabled = interpolate.ValidInterpolationSelection;
            this.menuDetails.Enabled = dataGrid1.SelectedCells.Count == 1;
        }

        private void menuDetails_Click(object sender, EventArgs e)
        {
            DataGridViewCell cell = dataGrid1.SelectedCells[0];

            if (cell.ColumnIndex != 0 && cell.Value != DBNull.Value)
            {
                DataRow row = ((DataRowView)cell.OwningRow.DataBoundItem).Row;
                MultipleSeriesDataTable tbl = row.Table as MultipleSeriesDataTable;
                DateTime t = Convert.ToDateTime(row[0]);
                string interval = tbl.TableName;
                HdbPoet.TimeSeriesDataSet.SeriesRow s = tbl.LookupSeries(cell.ColumnIndex);
                var info = Hdb.Instance.BaseInfo(t, s.hdb_site_datatype_id, interval);

                info = DataTableUtility.Transpose(info);

                // GET Computation Processor Information
                try
                {
                    if (info.Columns.Count > 1 && Convert.ToInt32(info.Rows[info.Rows.Count - 1][1]) > 99)
                    {
                        var cpInfo = Hdb.Instance.CpInfo(info.Rows[info.Rows.Count - 1][1].ToString());

                        // cp comment
                        var cpRow = info.NewRow();
                        cpRow[0] = "CP_COMMENT";
                        cpRow[1] = cpInfo.Rows[0]["CMMNT"];
                        info.Rows.Add(cpRow);

                        // cp inputs
                        if (cpInfo.Rows[0]["GRP"].ToString() != "")
                        {
                            cpRow = info.NewRow();
                            cpRow[0] = "CP_INPUT";
                            cpRow[1] = "Group Comp - Input is self with a different time-step or ";
                            info.Rows.Add(cpRow);
                            cpRow = info.NewRow();
                            cpRow[0] = "";
                            cpRow[1] = "a different SDID under the same site";
                            info.Rows.Add(cpRow);
                        }
                        else
                        {
                            int inputCounter = 1;
                            foreach (DataRow item in cpInfo.Rows)
                            {
                                cpRow = info.NewRow();
                                cpRow[0] = "CP_INPUT_" + inputCounter;
                                cpRow[1] = "SDID " + item["SDID"].ToString() +
                                           " (" + item["SNAME"].ToString().ToUpper() +
                                           "-" + item["DNAME"].ToString().ToUpper() +
                                           ") from " + item["TABL"].ToString().ToUpper();
                                info.Rows.Add(cpRow);
                                inputCounter++;
                            }
                        }
                    }
                }
                catch
                {
                    var cpRow = info.NewRow();
                    cpRow[0] = "CP COMMENT";
                    cpRow[1] = "Unable to get CP Information from the DB...";
                    info.Rows.Add(cpRow);
                }

                TableViewer tv = new TableViewer(info);
                tv.Show();


                //msDataTable
                //row[cell.ColumnIndex] = DBNull.Value;
            }
        }




    }
}
