using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Windows.Forms;
using System.IO;
using Reclamation.Core;
using SpreadsheetGear;
using SpreadsheetGearExplorer;

namespace HdbPoet
{
    /// <summary>
    /// uses template 'report.xls' to print a custom
    /// HDB report
    /// </summary>
    internal class ExcelReportTool : HdbPoet.IExcelReportTool
    {
        MultipleSeriesDataTable tbl;
        string xlsFilename;
        public ExcelReportTool(MultipleSeriesDataTable tbl)
        {
            this.tbl = tbl;
            xlsFilename = CreateTemporaryExcelFile();
        }


        public void PrintPreview()
        {
            if (tbl == null)
                return;

           var wb =  SpreadsheetGear.Factory.GetWorkbook(xlsFilename);
           var ws = wb.Worksheets[0];
            
           ws.Cells["A9"].CopyFromDataTable(tbl, SpreadsheetGear.Data.SetDataFlags.None);
           ws.Cells["D7"].Value = Hdb.Instance.Server.ServiceName;
           ws.Cells["D8"].Value = DateTime.Now.ToString();

           int lastRow = ws.UsedRange.RowCount;
           string strRange = "A10:A" + (lastRow+5);
           string[] formats = tbl.DisplayFormat;
           IRange rng= ws.Cells[strRange];
           rng = rng.Offset(0, 1);
           for (int i = 0; i < formats.Length; i++)
           {
               rng.NumberFormat = ExcelFormat(formats[i]);
               rng = rng.Offset(0, 1);
           }
           wb.Save(); // save for debugging..



           var v = new SpreadsheetGear.Windows.Forms.WorkbookView(wb);
           CustomPrintPreview(v,ws);
           
            
           //v.GetLock();

           //try
           //{
           //    ws.PageSetup.PrintArea = ws.UsedRange.Address;
           //    v.PrintPreview();
           //}
           //finally
           //{
           //    v.ReleaseLock();
           //}
        }

        private void CustomPrintPreview(
           SpreadsheetGear.Windows.Forms.WorkbookView workbookView, IWorksheet ws)
        {
            // NOTE: Must acquire a workbook set lock.
            workbookView.GetLock();
            try
            {
                // Create a workbook print document.
                var document = new SpreadsheetGear.Drawing.Printing.WorkbookPrintDocument(ws, SpreadsheetGear.Drawing.Printing.PrintWhat.Sheet);
                //    CreatePrintDocument(workbookView.ActiveWorkbook);

                using (document)
                {
                    // Create a custom print preview dialog.
                    CustomPrintPreviewDialog dialog = new CustomPrintPreviewDialog();

                    // Set the print preview dialogs print document.
                    dialog.Document = document;
                    
                    // Show the print preview dialog.
                    dialog.ShowDialog();
                }
            }
            catch (System.Exception exc)
            {
                System.Windows.Forms.MessageBox.Show( exc.Message, "Error Printing",
                    System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
            finally
            {
                // NOTE: Must release the workbook set lock.
                workbookView.ReleaseLock();
            }
        }

        //private SpreadsheetGear.Drawing.Printing.WorkbookPrintDocument CreatePrintDocument(
        //    SpreadsheetGear.IWorkbook workbook)
        //{
        //    // Create an empty printable list.
        //    System.Collections.Generic.List<SpreadsheetGear.IPrintable> printables =
        //        new System.Collections.Generic.List<SpreadsheetGear.IPrintable>();

        //    // If the range option is selected...
        //    if (checkBoxRange.Checked)
        //    {
        //        // Get a reference to a range from an existing defined name.
        //        SpreadsheetGear.IRange range = workbook.Worksheets[0].Cells["West"];

        //        // Add the range to the printable list.
        //        printables.Add(range);
        //    }

        //    // If the chart option is selected...
        //    if (checkBoxChart.Checked)
        //    {
        //        // Get a reference to a chart.
        //        SpreadsheetGear.Charts.IChart chart =
        //            workbook.Worksheets[0].Shapes["Chart 1"].Chart;

        //        // Add the chart to the printable list.
        //        printables.Add(chart);
        //    }

        //    // Create a workbook print document.
        //    SpreadsheetGear.Drawing.Printing.WorkbookPrintDocument document =
        //        new SpreadsheetGear.Drawing.Printing.WorkbookPrintDocument(printables);

        //    // Return the workbook print document.
        //    return document;
        //}

        

        private string ExcelFormat(string f)
        {

            string[] fmts ={ "F0","F1","F2","F3","F4","N0","N1","N2","N3","N2","N3","N4"};
            string[] xls = { "#", "#.0", "#.00", "#.000", "#.0000",
                             "#,###", "#,###.0", "#,###.00", "#,###.000", "#,###.0000", };
            int idx = Array.IndexOf(fmts, f);
            if (idx >= 0)
                return xls[idx];
            return "General"; 
        }


        private string CreateTemporaryExcelFile()
        {
           string filename = Path.Combine(Application.StartupPath, "report.xls");
            if (!File.Exists(filename))
            {
              throw new FileNotFoundException("Error: report.xls not found in hdb install directory");

            }

            string rval = FileUtility.GetTempFileNameInDirectory(FileUtility.GetTempPath(), ".xls");

            File.Copy(filename, rval,true);

            return rval;
        }
    }

}
