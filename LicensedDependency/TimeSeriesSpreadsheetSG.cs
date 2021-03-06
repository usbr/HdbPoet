using System;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using System.Linq;
using Reclamation.Core;
using System.Collections.Generic;
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
        private SpreadsheetGear.IWorksheet workSheet1;
        private StatusStrip statusStrip1;
        private ToolStripStatusLabel toolStripStatusLabel1;
        private SpreadsheetGear.IRange initialUsedRange;
        private string m_colorColumnName = "";
        MultipleSeriesDataTable msDataTable;
        private Regex IsValidNumber = new Regex(@"^-?[0-9]*(?:\.[0-9]*)?$");
        private List<decimal> editableSdis;
        private WorkbookView workbookView1;
        private Panel panel1;
        private List<int[]> editedRange;

        public TimeSeriesSpreadsheetSG()
        {
            InitializeComponent();
        }

        /******************************************************************
         * EVENT HANDLERS
         ******************************************************************/
        #region
        /// <summary>
        /// Handler for events that occur before edits are processed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void workSheet1_CellBeginEdit(object sender, CellBeginEditEventArgs e)
        {            
            // Diasble edits to header row, header column, and read only cells in used range
            if (ActiveCellIsHeader() || CellReadOnly())
            { e.Cancel = true; }
        }

        /// <summary>
        /// Handler for events that occur after edits are processed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void workSheet1_CellEndEdit(object sender, CellEndEditEventArgs e)
        {
            
        }

        /// <summary>
        /// Handler for button presses while control is in focus
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void workSheet1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyValue == 86 && (CellReadOnly() || ActiveCellIsHeader())) //override header edits or ctrl-v for read only columns
            {
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
            if (e.Control && e.KeyValue == 86) //override ctrl-v
            {
                e.Handled = true;
                e.SuppressKeyPress = true;
                workbookView1.PasteSpecial(SpreadsheetGear.PasteType.Values, SpreadsheetGear.PasteOperation.None, false, false);//.Paste();
            }
            CheckSecretCode(e);
        }

        /// <summary>
        /// Handler for user-prompted changes in selected ranges
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void workSheet1_SelectionChanged(object sender, RangeSelectionChangedEventArgs e)
        {
            workbookView1.GetLock();
            try
            {
                var valList = new List<double>();
                int missingCount = 0;
                var rval = "";
                bool headerRowSelected = (e.RangeSelection.Cells.Row == 0 && e.RangeSelection.Cells.Column != 0);
                bool headerColSelected = (e.RangeSelection.Cells.Column == 0 && e.RangeSelection.Cells.Row != 0);
                bool rowInUsedRange = (e.RangeSelection.Cells.Row < initialUsedRange.RowCount);
                bool colInUsedRange = (e.RangeSelection.Cells.Column < initialUsedRange.ColumnCount);

                //crunch array stats
                if (e.RangeSelection.CellCount > 1 && e.RangeSelection.CellCount < 1000)
                {
                    foreach (SpreadsheetGear.IRange item in e.RangeSelection)
                    {
                        if (item.Column != 0 && item.Row != 0 && item.Value != null && item.Value != DBNull.Value)
                        { valList.Add((double)item.Value); }
                        else
                        { missingCount++; }
                    }
                    rval = "Selected Cells Statistics";
                }
                //crunch column stats
                if (e.RangeSelection.CellCount == 1 && colInUsedRange && headerRowSelected && !headerColSelected)
                {
                    int processCol = e.RangeSelection.Cells.Column;
                    for (int i = 1; i < initialUsedRange.RowCount; i++)
                    {
                        var ithVal = workSheet1.Cells[i, processCol].Value;
                        if (ithVal != null && ithVal != DBNull.Value)
                        { valList.Add((double)ithVal); }
                        else
                        { missingCount++; }
                    }
                    rval = "Selected Column Statistics";
                }
                //crunch row stats
                if (e.RangeSelection.CellCount == 1 && rowInUsedRange && headerColSelected && !headerRowSelected)
                {
                    int processRow = e.RangeSelection.Cells.Row;
                    for (int i = 1; i < initialUsedRange.ColumnCount; i++)
                    {
                        var ithVal = workSheet1.Cells[processRow, i].Value;
                        if (ithVal != null && ithVal != DBNull.Value)
                        { valList.Add((double)ithVal); }
                        else
                        { missingCount++; }
                    }
                    rval = "Selected Row Statistics";
                }
                if (valList.Count > 1)
                {
                    rval = rval + " | Count: " + valList.Count + " | Missing: " + missingCount +
                        " | Average: " + valList.ToArray().Average().ToString("F2") +
                        " | Min: " + valList.ToArray().Min().ToString("F2") +
                        " | Max: " + valList.ToArray().Max().ToString("F2") +
                        " | Sum: " + valList.ToArray().Sum().ToString("F2");
                    toolStripStatusLabel1.Text = rval;
                }
                else if (valList.Count != 0 || missingCount != 0)
                { toolStripStatusLabel1.Text = rval + " | Count: " + valList.Count + " | Missing: " + missingCount; }
                else
                { toolStripStatusLabel1.Text = ""; }
                toolStripStatusLabel1.ForeColor = Color.Blue;
            }
            finally
            {
                workbookView1.ReleaseLock();
            }
        }

        private bool bypassDataChangeEvent = false;

        /// <summary>
        /// Handler for when data in the msDataTable object is updated
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void workSheet1_DataChanged(object sender, DataRowChangeEventArgs e)
        {
            if (!bypassDataChangeEvent)
            {
                workbookView1.GetLock();
                workbookView1.BeginUpdate();
                try
                {
                    editedRange = new List<int[]>();

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
                                    sgCell.Activate();
                                    workbookView1.BeginEdit();
                                    sgCell.Value = changedVal;
                                    workbookView1.EndEdit();
                                    AddEditedCell(sgRow, sgCol);
                                }
                            }
                            catch
                            {

                            }
                        }
                    }
                    FormatEditedCells();
                }
                finally
                {
                    workbookView1.EndUpdate();
                    workbookView1.ReleaseLock();
                }
            }

            // [JR] NEED TO REDRAW GRAPH WHEN TABLE DATA IS CHANGED...
        }

        /// <summary>
        /// Handler for when data and formatting in a SG range is modified
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void workSheet1_RangeChange(object sender, SpreadsheetGear.Windows.Forms.RangeChangedEventArgs e)
        {
            workbookView1.GetLock();
            workbookView1.BeginUpdate();
            bypassDataChangeEvent = true;
            editedRange = new List<int[]>();

            try
            {
                //iterate through columns in edited range
                foreach (SpreadsheetGear.IRange col in e.Range.Columns)
                {
                    col.Rows[0, 0].Activate();
                    bool columnReadOnly = CellReadOnly(false);

                    //iterate through rows in edited range
                    foreach (SpreadsheetGear.IRange ithCell in col.Rows)// e.Range.Cells)
                    {
                        //iterate through SG used range to see if cell is a used data cell
                        ithCell.Cells.Activate();
                        int dGridRow = workbookView1.ActiveCell.Row - 1;//SG rows are 1-based since it has the header in row-0
                        int sgRow = workbookView1.ActiveCell.Row;
                        int dGridCol = workbookView1.ActiveCell.Column;
                        int sgCol = dGridCol;

                        bool activeCellUsed = CellInUsedRange(ithCell);
                        bool headerCell = ActiveCellIsHeader();// (workbookView1.ActiveCell.Row == 0 || workbookView1.ActiveCell.Column == 0);

                        //process cell
                        if (activeCellUsed && !headerCell && !columnReadOnly)
                        {
                            if (workbookView1.ActiveCell.Column - 1 >= msDataTable.Columns.Count)
                            { MessageBox.Show("Internal error: formatting cell error"); }
                            //process cell
                            else
                            {
                                // check if cell value was changed
                                var sgVal = workbookView1.ActiveCell.Value;//workSheet1.Cells[sgRow, sgCol].Value;
                                double origVal = 0, editVal = 0;
                                if (!Double.TryParse(msDataTable.Rows[dGridRow][dGridCol].ToString(), out origVal))
                                { origVal = double.NaN; }
                                if (sgVal == null)
                                { editVal = double.NaN; }
                                else if (!IsValidNumber.IsMatch(sgVal.ToString())) //disallow not numbers in used data range
                                {
                                    workSheet1.Range[sgRow, sgCol].Value = msDataTable.Rows[dGridRow][dGridCol].ToString();// origVal;
                                    break;
                                }
                                else
                                { editVal = Convert.ToDouble(sgVal); }
                                if ((origVal != editVal && !double.IsNaN(origVal)) || (double.IsNaN(origVal) && !double.IsNaN(editVal)))
                                {
                                    // add cell to format list
                                    AddEditedCell(sgRow, sgCol);
                                    // mirror change to datagrid
                                    dataGrid1.CurrentCell = dataGrid1.Rows[dGridRow].Cells[dGridCol];
                                    DataGridViewCell cell = dataGrid1.CurrentCell;
                                    if (dGridCol != 0)
                                    {
                                        DataRow row = ((DataRowView)cell.OwningRow.DataBoundItem).Row;
                                        if (double.IsNaN(editVal))
                                        {
                                            row[cell.ColumnIndex] = DBNull.Value;
                                        }
                                        else
                                        {
                                            row[cell.ColumnIndex] = editVal;
                                        }
                                    }
                                }
                            }
                            //}
                        }
                        else if (!activeCellUsed && !headerCell) // cells outside of used range and not a header
                        {
                            ClearCellFormat(sgRow, sgCol);//clear cell formatting
                        }
                        else
                        {

                        }
                    }
                }
            }
            finally
            {
                bypassDataChangeEvent = false;
                FormatEditedCells();
                workbookView1.EndUpdate();
                workbookView1.ReleaseLock();
            }
        }

        #endregion
        
        /******************************************************************
         * CUSTOM SG METHODS
         ******************************************************************/
        
        /// <summary>
        /// Sets the msDataTable into SG
        /// </summary>
        /// <param name="table"></param>
        /// <param name="colorColumnName"></param>
        internal void SetTable(MultipleSeriesDataTable table, string colorColumnName)
        {
            this.dataGrid1.DataSource = null;
            m_colorColumnName = colorColumnName;
            this.msDataTable = table;
            this.dataGrid1.DataSource = msDataTable;
            /////////////////////////////////////////////////
            // Create a new workbook and worksheet
            SpreadsheetGear.IWorkbook workbook = SpreadsheetGear.Factory.GetWorkbook();
            workbook.WindowInfo.DisplayWorkbookTabs = false;
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
            // Attach handlers for range-selection and data change events
            msDataTable.RowChanged += new DataRowChangeEventHandler(workSheet1_DataChanged);
            workbookView1.RangeChanged += new RangeChangedEventHandler(workSheet1_RangeChange);
            workbookView1.RangeSelectionChanged += new RangeSelectionChangedEventHandler(workSheet1_SelectionChanged);
            workbookView1.KeyDown += new KeyEventHandler(workSheet1_KeyDown);
            workbookView1.CellBeginEdit += new CellBeginEditEventHandler(workSheet1_CellBeginEdit);
            workbookView1.CellEndEdit += new CellEndEditEventHandler(workSheet1_CellEndEdit);
        }

        /// <summary>
        /// Sets the msDataTable into SG - Overload for the TimeSeriesCommitChanges UI
        /// </summary>
        /// <param name="table"></param>
        /// <param name="colorColumnName"></param>
        internal void SetTable(MultipleSeriesDataTable table, string colorColumnName, bool onlyShowModified)
        {
            SetTable(table, colorColumnName);
            if (onlyShowModified)
            {
                workbookView1.GetLock();
                workbookView1.BeginUpdate();

                try
                {
                    foreach (DataRow dr in table.Rows)
                    {
                        if (dr.RowState == DataRowState.Modified)
                        {
                            foreach (DataColumn dc in table.Columns)
                            {
                                if (!dr[dc, DataRowVersion.Original].Equals(dr[dc, DataRowVersion.Current]) && table.Columns.IndexOf(dc) != 0)
                                {
                                    //workSheet1.Cells.Range[table.Rows.IndexOf(dr) + 1, table.Columns.IndexOf(dc)].EntireColumn.Hidden = true;
                                    var editedCell = workSheet1.Cells.Range[table.Rows.IndexOf(dr) + 1, table.Columns.IndexOf(dc)];
                                    editedCell.Font.Bold = true;
                                    editedCell.HorizontalAlignment = SpreadsheetGear.HAlign.Center;
                                    editedCell.Font.Color = SpreadsheetGear.Drawing.Color.GetSpreadsheetGearColor(Color.White);
                                    editedCell.Interior.Color = SpreadsheetGear.Drawing.Color.GetSpreadsheetGearColor(Color.Red);
                                }
                            }
                        }
                        else
                        {
                            workSheet1.Cells.Range[table.Rows.IndexOf(dr) + 1, 0].EntireRow.Hidden = true;
                        }
                    }
                }
                finally
                {
                    workbookView1.ReleaseLock();
                    workbookView1.EndUpdate();
                }
            }

        }

        /// <summary>
        /// Formats SG Table
        /// </summary>
        private void FormatSpreadsheetView()
        {
            workbookView1.GetLock();
            workbookView1.BeginUpdate();

            try
            {
                if (workSheet1.UsedRange.Columns.ColumnCount > 0)
                {
                    for (int c = 1; c < workSheet1.UsedRange.Columns.ColumnCount; c++)
                    {
                        var s = msDataTable.LookupSeries(c);
                        //dataGrid1.Columns[c].DefaultCellStyle.Format = msDataTable.LookupSeries(c).DisplayFormat;
                        var dispFormat = msDataTable.LookupSeries(c).DisplayFormat;
                        workSheet1.Range.Columns[0, c].EntireColumn.NumberFormat = ResolveNumberFormat(dispFormat);
                        workSheet1.UsedRange.Columns[0, c].Locked = msDataTable.LookupSeries(c).ReadOnly;
                        // adjust column width to auto-fit longest header entry
                        var cName = workSheet1.Range[0, c].Value.ToString().Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
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

                // Set global spreadsheet formatting and variables
                workSheet1.UsedRange.WrapText = true;
                workSheet1.UsedRange.HorizontalAlignment = SpreadsheetGear.HAlign.Right;
                workSheet1.UsedRange.Borders.LineStyle = SpreadsheetGear.LineStyle.Dot;
                workSheet1.UsedRange.Borders.Color = SpreadsheetGear.Drawing.Color.GetSpreadsheetGearColor(Color.Gray);
                initialUsedRange = workSheet1.UsedRange;
                // Format row & col headers
                var header = workSheet1.Range["A1"];
                header.EntireColumn.AutoFit();
                header.EntireColumn.Font.Bold = true;
                header.EntireColumn.Locked = true;
                header.EntireRow.AutoFit();
                header.EntireRow.Font.Bold = true;
                header.EntireRow.Locked = true;
                if (header.EntireColumn.ColumnWidth < 12.0) { header.EntireColumn.ColumnWidth = 12.0; }
                header.EntireColumn.Interior.Color = SpreadsheetGear.Drawing.Color.GetSpreadsheetGearColor(Color.Gray);
                header.EntireRow.Interior.Color = SpreadsheetGear.Drawing.Color.GetSpreadsheetGearColor(Color.Gray);
                header.EntireColumn.Font.Color = SpreadsheetGear.Drawing.Color.GetSpreadsheetGearColor(Color.White);
                header.EntireRow.Font.Color = SpreadsheetGear.Drawing.Color.GetSpreadsheetGearColor(Color.White);
                header.EntireColumn.Font.Italic = true;
                header.EntireRow.Font.Italic = true;
                //workSheet1.Range.Font.Name = "Calibri";
                // Split col header 
                workSheet1.WindowInfo.ScrollColumn = 0;
                workSheet1.WindowInfo.SplitColumns = 1;
                // Split row header 
                workSheet1.WindowInfo.ScrollRow = 0;
                workSheet1.WindowInfo.SplitRows = 1;
                // Freeze headers 
                workSheet1.WindowInfo.FreezePanes = true;
                // Lock row and column headers, unlock everything else
                workSheet1.Range.Locked = false;
                header.EntireRow.Locked = true;
                header.EntireColumn.Locked = true;
                workSheet1.ProtectContents = false;
            }
            finally
            {
                workbookView1.ReleaseLock();
                workbookView1.EndUpdate();
                // Get SDIs in ACL
                GetEditableColumns();
            }
        }


        private void AddEditedCell(int sgRow, int sgCol)
        {
            editedRange.Add(new int[] { sgRow, sgCol });
        }

        /// <summary>
        /// Format SG cell as edited
        /// </summary>
        /// <param name="sgRow"></param>
        /// <param name="sgCol"></param>
        private void FormatEditedCells()//int sgRow, int sgCol)
        {
            workbookView1.BeginUpdate();
            foreach (var item in editedRange)
            {
                int sgRow = item[0];
                int sgCol = item[1];
                //workSheet1.Cells[sgRow, sgCol].Select();
                if (CellInUsedRange(workSheet1.Cells[sgRow, sgCol]) && !CellReadOnly(false))
                {
                    workSheet1.Cells[sgRow, sgCol].Font.Bold = true;
                    workSheet1.Cells[sgRow, sgCol].Font.Italic = true;
                    workSheet1.Cells[sgRow, sgCol].HorizontalAlignment = SpreadsheetGear.HAlign.Left;
                    workSheet1.Cells[sgRow, sgCol].Font.Color = SpreadsheetGear.Drawing.Color.GetSpreadsheetGearColor(Color.White);
                    workSheet1.Cells[sgRow, sgCol].Interior.Color = SpreadsheetGear.Drawing.Color.GetSpreadsheetGearColor(Color.Black);
                }
            }
            workbookView1.EndUpdate();
        }

        /// <summary>
        /// Clear SG cell format
        /// </summary>
        /// <param name="sgRow"></param>
        /// <param name="sgCol"></param>
        private void ClearCellFormat(int sgRow, int sgCol)
        {
            workSheet1.Cells[sgRow, sgCol].Select();
            workbookView1.ActiveCell.Font.Bold = false;
            workbookView1.ActiveCell.Font.Italic = false;
            workbookView1.ActiveCell.HorizontalAlignment = SpreadsheetGear.HAlign.Center;
            workbookView1.ActiveCell.Font.Color = SpreadsheetGear.Drawing.Color.GetSpreadsheetGearColor(Color.SlateGray);
            workbookView1.ActiveCell.Interior.Color = SpreadsheetGear.Drawing.Color.GetSpreadsheetGearColor(Color.Transparent);
        }

        /// <summary>
        /// Converts the C# string number format (F0,F1..., N4) into an Excel custom number format
        /// </summary>
        /// <param name="numFormat"></param>
        /// <returns></returns>
        private string ResolveNumberFormat(string numFormat)
        {
            string numStr = "General";//default
            if (numFormat != "")
            {
                if (numFormat.Contains("N"))
                {
                    numStr = "#,##0";
                }
                else
                {
                    numStr = "0";
                }
                int decimalCount = Convert.ToInt16(numFormat[numFormat.Length - 1].ToString());
                for (int i = 0; i < decimalCount; i++)
                {
                    if (i == 0) { numStr += ".0"; }
                    else { numStr += "0"; }
                }
            }
            return numStr;
        }

        private bool CellReadOnly(bool popup = true)
        {
            bool readOnly = false;
            if (CellInUsedRange(workbookView1.ActiveCell))
            {
                // check if Series is editable by user
                var s = msDataTable.LookupSeries(workbookView1.ActiveCell.Column);
                readOnly = !editableSdis.Contains(s.hdb_site_datatype_id);// Hdb.Instance.ReadOnly(Convert.ToInt32(s.hdb_site_datatype_id));
                if (readOnly && popup)
                {
                    MessageBox.Show(GlobalVariables.connectedUser + " is not authorizerd to edit the data for " + s.SiteName + ". Contact your DBA to request authorization...", "Unauthorized Edit", MessageBoxButtons.OK);
                }
            }
            return readOnly;
        }

        private bool CellInUsedRange(SpreadsheetGear.IRange activeCell)
        {
            bool rowInUsedRange = (activeCell.Row < initialUsedRange.RowCount);
            bool colInUsedRange = (activeCell.Column < initialUsedRange.ColumnCount);
            return rowInUsedRange && colInUsedRange;
        }

        private bool ActiveCellIsHeader()
        {
            bool rowHeader = (workbookView1.ActiveCell.Row == 0);
            bool colHeader = (workbookView1.ActiveCell.Column == 0);
            return rowHeader || colHeader;
        }

        private void GetEditableColumns()
        {
            editableSdis = new List<decimal>();
            for (int i = 1; i < initialUsedRange.ColumnCount; i++)//SG has 1 extra column (date) than the msDataTable
            {
                var s = msDataTable.LookupSeries(i); 
                bool readOnly = Hdb.Instance.ReadOnly(Convert.ToInt32(s.hdb_site_datatype_id));
                if (!readOnly)
                {
                    editableSdis.Add(s.hdb_site_datatype_id);
                }
                else
                {
                    workSheet1.Cells[0, i].AddComment(GlobalVariables.connectedUser + " does not have access to modify this SDI");
                }
            }
        }

        /******************************************************************
         * BASE TIMESERIESSPREADSHEET METHODS
         ******************************************************************/
        #region
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

        public void CopyToClipboard()
        {
            workbookView1.GetLock();
            try
            {
                workbookView1.Copy();
            }
            finally
            {
                workbookView1.ReleaseLock();
            }
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
            workbookView1.PasteSpecial(SpreadsheetGear.PasteType.Values, SpreadsheetGear.PasteOperation.None, false, false);//.Paste();            
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
            try
            {
                SpreadsheetGear.IRange selectedCells = workbookView1.RangeSelection;
                foreach (SpreadsheetGear.IRange cell in selectedCells)
                {
                    int dGridRow = cell.Row - 1;
                    int dGrdCol = cell.Column;
                    this.dataGrid1[dGrdCol, dGridRow].Selected = true;
                }
            }
            finally
            {
                workbookView1.ReleaseLock();
            }
            return this.dataGrid1.SelectedCells;
        }

        /// <summary>
        /// Delete selected cells, but not the date column
        /// </summary>
        private void DeleteSelectedCells()
        {
            workbookView1.GetLock();
            workbookView1.BeginUpdate();
            try
            {
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
            }
            finally
            {
                workbookView1.EndUpdate();
                workbookView1.ReleaseLock();
            }
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DeleteSelectedCells();
        }
        
        internal void SelectCell(int rowIndex, int columnIndex)
        {
            workbookView1.GetLock();
            try
            {
                workbookView1.RangeSelection[rowIndex + 1, columnIndex].Select();
                DataGridViewCell cell = dataGrid1[columnIndex, rowIndex];
                dataGrid1.CurrentCell = cell;
            }
            finally
            {
                workbookView1.ReleaseLock();
            }
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
                SetTable(msDataTable, m_colorColumnName);
            }
        }

        internal void Print(string title)
        {
            var e = new ExcelReportTemplate(msDataTable);
            e.PrintPreview();
        }

        private void toolStripMenuItemInterpolate_Click(object sender, EventArgs e)
        {
            workbookView1.GetLock();
            try
            {
                if (!CellReadOnly())
                { Interpolate(); }
            }
            finally
            {
                workbookView1.ReleaseLock();
            }
        }

        /// <summary>
        /// Determine which options should be available when user right-clicks
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {
            workbookView1.GetLock();
            try
            {
                // check if selected range can be interpolated
                this.toolStripMenuItemInterpolate.Enabled = CheckInterpolateAvailable();

                //check if cell can have ts-point details
                this.menuDetails.Enabled = workbookView1.RangeSelection.CellCount == 1 &&
                    workbookView1.RangeSelection.Column != 0 &&
                    workbookView1.RangeSelection.Row != 0 &&
                    workbookView1.RangeSelection.Value != null &&
                    workbookView1.RangeSelection.Value != DBNull.Value;
            }
            finally
            {
                workbookView1.ReleaseLock();
            }
        }

        /// <summary>
        /// Build and show data point details window
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void menuDetails_Click(object sender, EventArgs e)
        {
            workbookView1.GetLock();
            try
            {
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
            }
            finally
            {
                workbookView1.ReleaseLock();
                toolStripStatusLabel1.Text = "";
            }
        }

        /// <summary>
        /// Check if interpolation is available for the selected range
        /// </summary>
        /// <returns></returns>
        private bool CheckInterpolateAvailable()
        {
            var selRange = workbookView1.RangeSelection;
            bool interpolateAvailable = (
                // check selection in used range
                selRange.Column > 0 
                && selRange.Column < initialUsedRange.ColumnCount
                && selRange.Rows[0, 0].Row > 0
                && selRange.Rows[selRange.RowCount, 0].Row < initialUsedRange.RowCount
                // check selection is single col
                && selRange.ColumnCount == 1
                //check selection has at least 3 rows
                && selRange.CellCount >= 3
                // check end points values are not missing
                && selRange.Rows[0, 0].Value != DBNull.Value
                && selRange.Rows[0, 0].Value != null
                && selRange.Rows[selRange.RowCount - 1, 0].Value != DBNull.Value
                && selRange.Rows[selRange.RowCount - 1, 0].Value != null
            );
            return interpolateAvailable;
        }

        /// <summary>
        /// Interpolate data points between selected range
        /// </summary>
        public void Interpolate()
        {
            var selRange = workbookView1.RangeSelection;
            double d1 = Convert.ToDouble(selRange.Rows[0, 0].Value);
            double d2 = Convert.ToDouble(selRange.Rows[selRange.RowCount - 1, 0].Value);

            double increment = (d2 - d1) / (selRange.Rows[selRange.RowCount, 0].Row - selRange.Rows[0, 0].Row - 1);

            for (int rowIndex = 0; rowIndex < selRange.Rows.RowCount; rowIndex++)
            {
                selRange.Rows[0, 0].Offset(rowIndex, 0).Value = d1 + (increment * rowIndex);
            }
        }

        #endregion

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
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.workbookView1 = new SpreadsheetGear.Windows.Forms.WorkbookView();
            this.panel1 = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.dataGrid1)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.panel1.SuspendLayout();
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
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1});
            this.statusStrip1.Location = new System.Drawing.Point(0, 346);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(320, 22);
            this.statusStrip1.SizingGrip = false;
            this.statusStrip1.TabIndex = 35;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.ForeColor = System.Drawing.Color.Blue;
            this.toolStripStatusLabel1.LinkColor = System.Drawing.Color.Blue;
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(104, 17);
            this.toolStripStatusLabel1.Text = "Selection Statistics";
            // 
            // workbookView1
            // 
            this.workbookView1.AllowChartExplorer = false;
            this.workbookView1.AllowRangeExplorer = false;
            this.workbookView1.AllowShapeExplorer = false;
            this.workbookView1.AllowWorkbookDesigner = false;
            this.workbookView1.AllowWorkbookExplorer = false;
            this.workbookView1.ContextMenuStrip = this.contextMenuStrip1;
            this.workbookView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.workbookView1.FormulaBar = null;
            this.workbookView1.Location = new System.Drawing.Point(0, 0);
            this.workbookView1.Name = "workbookView1";
            this.workbookView1.Size = new System.Drawing.Size(320, 346);
            this.workbookView1.TabIndex = 34;
            this.workbookView1.WorkbookSetState = resources.GetString("workbookView1.WorkbookSetState");
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.workbookView1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(320, 346);
            this.panel1.TabIndex = 36;
            // 
            // TimeSeriesSpreadsheetSG
            // 
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.dataGrid1);
            this.Name = "TimeSeriesSpreadsheetSG";
            this.Size = new System.Drawing.Size(320, 368);
            ((System.ComponentModel.ISupportInitialize)(this.dataGrid1)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        

        private List<Keys> secretKeyLog = new List<Keys>();
        private List<Keys> secretKeys = new List<Keys> { Keys.Up, Keys.Up, Keys.Down, Keys.Down, Keys.Left, Keys.Right, Keys.Left, Keys.Right, Keys.B, Keys.A, Keys.Enter };
        private void CheckSecretCode(KeyEventArgs e)
        {
            if (secretKeyLog.Count > 20)
            { secretKeyLog = new List<Keys>(); }
            secretKeyLog.Add(e.KeyCode);

            bool contra = secretKeyLog
                .Where((item, index) => index <= secretKeyLog.Count - secretKeys.Count)
                .Select((item, index) => secretKeyLog.Skip(index).Take(secretKeys.Count))
                .Any(part => part.SequenceEqual(secretKeys));

            if (contra)
            {
                var aboutForm = new HdbPoet.About();
                aboutForm.labelVersion.Text = "Congratulations! You're a nerd...";
                aboutForm.linkLabel1.Text = @"https://en.wikipedia.org/wiki/Konami_Code";
                aboutForm.richTextBox1.Visible = false;
                aboutForm.Text = "OMGWTFCONTRA";
                aboutForm.pictureBox1.Visible = true;
                byte[] bytes = Convert.FromBase64String("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");
                Image image;
                using (System.IO.MemoryStream ms = new System.IO.MemoryStream(bytes))
                {
                    image = Image.FromStream(ms);
                }
                aboutForm.pictureBox1.Image = image;
                aboutForm.Show();

                secretKeyLog = new List<Keys>();
            }
        }
        #endregion

    }
}
