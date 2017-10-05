using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;
using System.IO;
using System.Linq;
using System.Diagnostics;
using Reclamation.Core;
using System.Collections.Generic;
using System.Drawing.Printing;
using SpreadsheetGear.Windows.Forms;

namespace HdbPoet
{
    /// <summary>
    /// TimeSeriesSpreadsheet displays a custom 
    /// DataTable (MultipleSeriesDataTable) 
    /// with custom font and coloring
    /// </summary>
    public class TimeSeriesSpreadsheetSG : System.Windows.Forms.UserControl
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
        private WorkbookView workbookView1;
        private SpreadsheetGear.IWorksheet workSheet1;
        private StatusStrip statusStrip1;
        private ToolStripStatusLabel toolStripStatusLabel1;
        private SpreadsheetGear.IRange initialUsedRange;
        private string m_colorColumnName = "";
        MultipleSeriesDataTable msDataTable;

        public TimeSeriesSpreadsheetSG()
        {
            InitializeComponent();
            workbookView1.RangeChanged += new RangeChangedEventHandler(workSheet1_RangeChange);
            workbookView1.RangeSelectionChanged += new RangeSelectionChangedEventHandler(workSheet1_SelectionChanged);
        }

        void workSheet1_SelectionChanged(object sender, RangeSelectionChangedEventArgs e)
        {
            workbookView1.GetLock();
            var valList = new List<double>();
            var rval = "";
            if (e.RangeSelection.CellCount > 1 && e.RangeSelection.CellCount < 1000)
            {
                foreach (SpreadsheetGear.IRange item in e.RangeSelection)
                {
                    if (item.Column != 0 && item.Row != 0 && item.Value != null && item.Value != DBNull.Value)
                    {
                        valList.Add((double)item.Value);
                    }
                }
                if (valList.Count > 1)
                {
                    rval = "Selected Cells Statistics | Count: " + valList.Count +
                        " | Average: " + valList.ToArray().Average().ToString("F2") +
                        " | Min: " + valList.ToArray().Min().ToString("F2") +
                        " | Max: " + valList.ToArray().Max().ToString("F2") +
                        " | Sum: " + valList.ToArray().Sum().ToString("F2");
                }
            }
            toolStripStatusLabel1.Text = rval;
            workbookView1.ReleaseLock();
        }

        internal DataViewRowState DataViewRowState
        {
            set
            {
                msDataTable.DefaultView.RowStateFilter = value;
            }
            get
            {
                return msDataTable.DefaultView.RowStateFilter;
            }
        }

        void workSheet1_DataChanged(object sender, DataRowChangeEventArgs e)
        {
            workbookView1.GetLock();
            workbookView1.BeginUpdate();

            int sgRow = msDataTable.Rows.IndexOf(e.Row) + 1;//SG has header row

            for (int sgCol = 1; sgCol < e.Row.ItemArray.Count(); sgCol++)
            {
                SpreadsheetGear.IRange sgCell = workSheet1.Cells[sgRow, sgCol];

                if (sgCell.Value != null)
                {
                    double currentVal = Convert.ToDouble(sgCell.Value);
                    double changedVal;
                    try
                    {
                        changedVal = Convert.ToDouble(e.Row.ItemArray[sgCol]);
                        if (currentVal != changedVal)
                        {
                            sgCell.Value = changedVal;
                            // format cell
                            FormatEditedCell(sgRow, sgCol);
                        }
                    }
                    catch
                    {

                    }
                }
            }
            workbookView1.EndUpdate();
            workbookView1.ReleaseLock();

            //SetTable(msDataTable, m_colorColumnName);
        }

        internal void SetTable(MultipleSeriesDataTable table, string colorColumnName)
        {
            this.dataGrid1.DataSource = null;
            m_colorColumnName = colorColumnName;
            this.msDataTable = table;
            this.dataGrid1.DataSource = msDataTable;

            /////////////////////////////////////////////////
            // Create a new workbook and worksheet.
            SpreadsheetGear.IWorkbook workbook = SpreadsheetGear.Factory.GetWorkbook();
            workSheet1 = workbook.Worksheets["Sheet1"];
            workSheet1.Name = "DATA";
            // Get the top left cell for the DataTable.
            SpreadsheetGear.IRange range = workSheet1.Cells["A1"];
            // Copy the DataTable to the worksheet range.
            range.CopyFromDataTable(msDataTable, SpreadsheetGear.Data.SetDataFlags.None);
            // Format SG worksheet
            FormatSpreadsheetView();
            workbookView1.ActiveWorkbook = workbook;

            /////////////////////////////////////////////////

            msDataTable.RowChanged += new DataRowChangeEventHandler(workSheet1_DataChanged);

            //FormatDataGridView();
        }

        internal void SetColorColumnName(string columnName)
        {
            m_colorColumnName = columnName;
            this.dataGrid1.DataSource = null;
            this.dataGrid1.DataSource = msDataTable;

            /////////////////////////////////////////////////
            // Create a new workbook and worksheet.
            SpreadsheetGear.IWorkbook workbook = SpreadsheetGear.Factory.GetWorkbook();
            workSheet1 = workbook.Worksheets["Sheet1"];
            workSheet1.Name = "DATA";
            // Get the top left cell for the DataTable.
            SpreadsheetGear.IRange range = workSheet1.Cells["A1"];
            // Copy the DataTable to the worksheet range.
            range.CopyFromDataTable(msDataTable, SpreadsheetGear.Data.SetDataFlags.None);
            // Format SG worksheet
            FormatSpreadsheetView();
        }


        private void FormatSpreadsheetView()
        {
            workbookView1.GetLock();
            workbookView1.BeginUpdate();

            if (workSheet1.UsedRange.Columns.ColumnCount > 0)
            {
                for (int c = 1; c < workSheet1.UsedRange.Columns.ColumnCount; c++)
                {
                    var s = msDataTable.LookupSeries(c);
                    //dataGrid1.Columns[c].DefaultCellStyle.Format = msDataTable.LookupSeries(c).DisplayFormat;
                    var dispFormat = msDataTable.LookupSeries(c).DisplayFormat;
                    workSheet1.UsedRange.Columns[0, c].Locked = msDataTable.LookupSeries(c).ReadOnly;
                    // adjust column width to auto-fit longest header entry
                    var cName = workSheet1.Range[0,c].Value.ToString().Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
                    var sortedNames = cName.OrderBy(n => n.Length);
                    var longestName = sortedNames.LastOrDefault();
                    var colWidth = System.Math.Max(Convert.ToInt16(10),
                        System.Math.Min(Convert.ToInt16(TextRenderer.MeasureText(longestName.ToUpper(), this.Font).Width),
                        Convert.ToInt16(20)));
                    workSheet1.Range.Columns[0, c].EntireColumn.ColumnWidth = colWidth;

                    // color cells
                    DataTable sTab = msDataTable.DataSet.Tables[msDataTable.LookupSeries(c).TableName];
                    for (int r = 1; r <= sTab.Rows.Count; r++)
                    {
                        DataRow row = sTab.Rows[r - 1];
                        //get and set cell color
                        workSheet1.Cells[r, c].Interior.Color = SpreadsheetGear.Drawing.Color.GetSpreadsheetGearColor(
                            System.Drawing.ColorTranslator.FromHtml(row[m_colorColumnName].ToString()));
                        // check if there is a data_flags column
                        if (row.Table.Columns.Contains("DATA_FLAGS"))
                        {
                            //get and set cell comment
                            if (row["DATA_FLAGS"] != null && row["DATA_FLAGS"] != DBNull.Value)
                            {
                                workSheet1.Cells[r, c].AddComment("HDB Data Flag: " + row["DATA_FLAGS"].ToString());
                            }
                        }
                    }

                }
            }

            // Set global spreadsheet formatting
            workSheet1.UsedRange.WrapText = true;
            workSheet1.UsedRange.HorizontalAlignment = SpreadsheetGear.HAlign.Right;
            //workSheet1.Range.Style.Interior.Color = SpreadsheetGear.Drawing.Color.GetSpreadsheetGearColor(Color.AntiqueWhite);
            // Format row & col headers
            workSheet1.Range["A1"].EntireColumn.AutoFit();
            workSheet1.Range["A1"].EntireColumn.Font.Bold = true;
            workSheet1.Range["A1"].EntireColumn.Locked = true;
            workSheet1.Range["A1"].EntireRow.AutoFit();
            workSheet1.Range["A1"].EntireRow.Font.Bold = true;
            workSheet1.Range["A1"].EntireRow.Locked = true;
            if (workSheet1.Range["A1"].EntireColumn.ColumnWidth < 12.0)
            { workSheet1.Range["A1"].EntireColumn.ColumnWidth = 12.0; }
            // Split col header 
            workSheet1.WindowInfo.ScrollColumn = 0;
            workSheet1.WindowInfo.SplitColumns = 1;
            // Split row header 
            workSheet1.WindowInfo.ScrollRow = 0;
            workSheet1.WindowInfo.SplitRows = 1;
            // Freeze headers 
            workSheet1.WindowInfo.FreezePanes = true;

            initialUsedRange = workSheet1.UsedRange;
            workbookView1.ReleaseLock();
            workbookView1.EndUpdate();
        }

        public void CopyToClipboard()
        {
            workbookView1.GetLock();
            workbookView1.Copy();
            workbookView1.ReleaseLock();
        }

        void workSheet1_RangeChange(object sender, SpreadsheetGear.Windows.Forms.RangeChangedEventArgs e)
        {
            workbookView1.GetLock();
            workbookView1.BeginUpdate();

            //iterate through cells in edited range
            foreach (SpreadsheetGear.IRange ithCell in e.Range.Cells)
            {
                //iterate through SG used range to see if cell is a used data cell
                ithCell.Cells.Activate();
                bool activeCellUsed = false;
                foreach (SpreadsheetGear.IRange item in initialUsedRange)
                {
                    if (workbookView1.ActiveCell.Address == item.Cells.Address)
                    {
                        activeCellUsed = true;
                        break;
                    }
                }
                //process cell
                if (activeCellUsed)
                {
                    // check if row has a date
                    object o = workSheet1.Range[workbookView1.ActiveCell.Row, 0].Value;
                    if (o != null)
                    {
                        DateTime t = workbookView1.ActiveWorkbook.NumberToDateTime((double)o);

                        if (workbookView1.ActiveCell.Column - 1 >= msDataTable.Columns.Count)
                        { MessageBox.Show("Internal error: formatting cell error"); }
                        //process cell
                        else
                        {
                            // check if cell value was changed
                            int dGridRow = workbookView1.ActiveCell.Row - 1;//SG rows are 1-based since it has the header in row-0
                            int sgRow = workbookView1.ActiveCell.Row;
                            int dGridCol = workbookView1.ActiveCell.Column;
                            int sgCol = dGridCol;
                            var sgVal = workbookView1.ActiveCell.Value;//workSheet1.Cells[sgRow, sgCol].Value;
                            double origVal, editVal;
                            if (!Double.TryParse(msDataTable.Rows[dGridRow][dGridCol].ToString(), out origVal))
                            { origVal = double.NaN; }
                            if (sgVal == null)
                            { editVal = double.NaN; }
                            else
                            { editVal = Convert.ToDouble(sgVal.ToString()); }
                            //origVal = Convert.ToDouble(msDataTable.Rows[dGridRow][dGridCol].ToString());
                            //editVal = Convert.ToDouble(workbookView1.ActiveCell.Value);
                            if ((origVal != editVal && !double.IsNaN(origVal)) || (double.IsNaN(origVal) && !double.IsNaN(editVal)))
                            {
                                // format cell
                                FormatEditedCell(sgRow, sgCol);
                                // mirror change to datagrid
                                dataGrid1.CurrentCell = dataGrid1.Rows[dGridRow].Cells[dGridCol];
                                DataGridViewCell cell = dataGrid1.CurrentCell;
                                if (cell.ColumnIndex != 0)
                                {
                                    DataRow row = ((DataRowView)cell.OwningRow.DataBoundItem).Row;
                                    if (double.IsNaN(editVal))
                                    { row[cell.ColumnIndex] = DBNull.Value; }
                                    else
                                    { row[cell.ColumnIndex] = editVal; }
                                }
                            }
                        }
                    }
                }
            }
            workbookView1.EndUpdate();
            workbookView1.ReleaseLock();
        }

        private void FormatEditedCell(int sgRow, int sgCol)
        {
            workSheet1.Cells[sgRow, sgCol].Select();
            workbookView1.ActiveCell.Font.Bold = true;// workSheet1.Cells[sgRow, sgCol].Font.Bold = true;
            workbookView1.ActiveCell.Font.Italic = true;
            workbookView1.ActiveCell.HorizontalAlignment = SpreadsheetGear.HAlign.Left;
            workbookView1.ActiveCell.Font.Color = SpreadsheetGear.Drawing.Color.GetSpreadsheetGearColor(Color.Yellow);
            workbookView1.ActiveCell.Interior.Color = SpreadsheetGear.Drawing.Color.GetSpreadsheetGearColor(Color.Black);
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

        private void copyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CopyToClipboard();
        }

        private void pasteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            workbookView1.GetLock();
            workbookView1.BeginUpdate();
            workbookView1.PasteSpecial(SpreadsheetGear.PasteType.Values, SpreadsheetGear.PasteOperation.None, false, false);
            workbookView1.EndUpdate();
            workbookView1.ReleaseLock();
        }

        private void dataGrid1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete && deleteToolStripMenuItem.Enabled )
            {
                DeleteSelectedCells();
            }
        }

        internal DataGridViewSelectedCellCollection GetSelectedCells()
        {
            workbookView1.GetLock();
            SpreadsheetGear.IRange selectedCells = workbookView1.RangeSelection;
            foreach (SpreadsheetGear.IRange cell in selectedCells)
            {
                int dGridRow = cell.Row - 1;
                int dGrdCol = cell.Column;
                this.dataGrid1[dGrdCol, dGridRow].Selected = true;
            }
            workbookView1.ReleaseLock();
            return this.dataGrid1.SelectedCells;
        }

        /// <summary>
        /// Delete selected cells, but not the date column
        /// </summary>
        private void DeleteSelectedCells()
        {
            workbookView1.GetLock();
            workbookView1.BeginUpdate();

            SpreadsheetGear.IRange selectedCells = workbookView1.RangeSelection;
            foreach (SpreadsheetGear.IRange sgCell in selectedCells)
            {                              
                int dGridRow = sgCell.Row - 1;
                int dGrdCol = sgCell.Column;
                this.dataGrid1.CurrentCell = dataGrid1.Rows[dGridRow].Cells[dGrdCol];
                DataGridViewCell dgCell = dataGrid1.CurrentCell;
                if (dgCell.ColumnIndex != 0 && dgCell.Value != DBNull.Value)
                {
                    DataRow row = ((DataRowView)dgCell.OwningRow.DataBoundItem).Row;
                    row[dgCell.ColumnIndex] = DBNull.Value;
                    workSheet1.Cells[sgCell.Row, sgCell.Column].Value = DBNull.Value;
                }
            }
            workbookView1.EndUpdate();
            workbookView1.ReleaseLock();
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DeleteSelectedCells();
        }
        
        internal void SelectCell(int rowIndex, int columnIndex)
        {
            workbookView1.GetLock();
            workbookView1.RangeSelection[rowIndex + 1, columnIndex].Select();
            DataGridViewCell cell = dataGrid1[columnIndex, rowIndex];
            dataGrid1.CurrentCell = cell;
            workbookView1.ReleaseLock();
        }

        internal void AllowEdits(bool enable)
        {
            this.pasteToolStripMenuItem.Enabled = enable;
            this.deleteToolStripMenuItem.Enabled = enable;
        }

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
            workbookView1.GetLock();
            var interpolate = new DataGridSelection(dataGrid1);

            this.toolStripMenuItemInterpolate.Enabled = interpolate.ValidInterpolationSelection;
            //check if cell can have ts-point details
            this.menuDetails.Enabled = workbookView1.RangeSelection.CellCount == 1 &&
                workbookView1.RangeSelection.Column != 0 && 
                workbookView1.RangeSelection.Row != 0 &&
                workbookView1.RangeSelection.Value != null &&
                workbookView1.RangeSelection.Value != DBNull.Value;

            workbookView1.ReleaseLock();
        }

        private void menuDetails_Click(object sender, EventArgs e)
        {
            workbookView1.GetLock();
            toolStripStatusLabel1.Text = "Fetching selected point details...";
            var sCell = workbookView1.RangeSelection.Cells;
            var s = msDataTable.LookupSeries(sCell.Column);
            DateTime t = workbookView1.ActiveWorkbook.NumberToDateTime(
                (double)workbookView1.ActiveCell.Offset(0, sCell.Column * -1).Value);
            var info = Hdb.Instance.BaseInfo(t, s.hdb_site_datatype_id, s.Interval);

            info = DataTableUtility.Transpose(info);

            if (s.hdb_r_table.ToString().ToLower()[0] == 'm')
            {
                info = new DataTable();
                info.Columns.Add("Query Failure");
                var infoRow = info.NewRow();
                infoRow[0] = "HDB does not store metadata for data in the Modeled Tables...";
                info.Rows.Add(infoRow);
            }
            else
            {
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
                            cpRow[1] = "a different SDI under the same site";
                            info.Rows.Add(cpRow);
                        }
                        else
                        {
                            int inputCounter = 1;
                            foreach (DataRow item in cpInfo.Rows)
                            {
                                cpRow = info.NewRow();
                                cpRow[0] = "CP_INPUT_" + inputCounter;
                                cpRow[1] = "SDI " + item["SDID"].ToString() +
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
            }
            TableViewer tv = new TableViewer(info);
            tv.Icon = HdbPoet.Properties.Resources.PoetIcon;
            tv.Text = "Details";
            tv.Show();

            workbookView1.ReleaseLock();
            toolStripStatusLabel1.Text = "";
        }

        #region Component Designer generated code
        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TimeSeriesSpreadsheetSG));
            this.dataGrid1 = new System.Windows.Forms.DataGridView();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.copyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pasteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemFormat = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemInterpolate = new System.Windows.Forms.ToolStripMenuItem();
            this.menuDetails = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.deleteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.workbookView1 = new SpreadsheetGear.Windows.Forms.WorkbookView();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            ((System.ComponentModel.ISupportInitialize)(this.dataGrid1)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
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
            this.dataGrid1.Visible = false;
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
            this.contextMenuStrip1.Size = new System.Drawing.Size(157, 142);
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
            // menuDetails
            // 
            this.menuDetails.Name = "menuDetails";
            this.menuDetails.Size = new System.Drawing.Size(156, 22);
            this.menuDetails.Text = "Show Details...";
            this.menuDetails.Click += new System.EventHandler(this.menuDetails_Click);
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
            // workbookView1
            // 
            this.workbookView1.AllowChartExplorer = false;
            this.workbookView1.AllowRangeExplorer = false;
            this.workbookView1.AllowShapeExplorer = false;
            this.workbookView1.AllowWorkbookDesigner = false;
            this.workbookView1.AllowWorkbookExplorer = false;
            this.workbookView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.workbookView1.ContextMenuStrip = this.contextMenuStrip1;
            this.workbookView1.FormulaBar = null;
            this.workbookView1.Location = new System.Drawing.Point(0, 0);
            this.workbookView1.Name = "workbookView1";
            this.workbookView1.Size = new System.Drawing.Size(320, 348);
            this.workbookView1.TabIndex = 34;
            this.workbookView1.WorkbookSetState = resources.GetString("workbookView1.WorkbookSetState");
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1});
            this.statusStrip1.Location = new System.Drawing.Point(0, 346);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(320, 22);
            this.statusStrip1.TabIndex = 35;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(77, 17);
            this.toolStripStatusLabel1.Text = "toolStripStats";
            // 
            // TimeSeriesSpreadsheetSG
            // 
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.workbookView1);
            this.Controls.Add(this.dataGrid1);
            this.Name = "TimeSeriesSpreadsheetSG";
            this.Size = new System.Drawing.Size(320, 368);
            ((System.ComponentModel.ISupportInitialize)(this.dataGrid1)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        #endregion

    }
}