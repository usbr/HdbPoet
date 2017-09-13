using Reclamation.Core;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace HdbPoet
{
    class ExcelReportTemplate: IExcelReportTool
    {
        private MultipleSeriesDataTable msDataTable;
        string xlsFilename;

        public ExcelReportTemplate(MultipleSeriesDataTable msDataTable)
        {
            this.msDataTable = msDataTable;
            xlsFilename = CreateTemporaryExcelFile();
        }
        public void PrintPreview()
        {

            NpoiExcel xls = new NpoiExcel(xlsFilename);

            xls.SetCellText(0, "D7", Hdb.Instance.Server.ServiceName);
            xls.SetCellText(0, "D8", DateTime.Now.ToString());

            xls.InsertDataTable(0, "A9", msDataTable);
            // format excel according to POET format.

            for (int c = 1; c < msDataTable.DisplayFormat.Length; c++)
            {
                string fmt = ExcelFormat(msDataTable.DisplayFormat[c]);
                xls.FormatRange(0,10,c,msDataTable.Rows.Count+10,c, fmt);
            }
         
            xls.Save(xlsFilename);
            System.Diagnostics.Process.Start(xlsFilename);
        }


        private string ExcelFormat(string f)
        {

            string[] fmts = { "F0", "F1", "F2", "F3", "F4", "N0", "N1", "N2", "N3", "N2", "N3", "N4" };
            string[] xls = { "0", "0.00", "0.00", "0.00", "0.00",
                             "#,###", "#,###0.00", "#,###.00", "#,###.00", "#,###.00", };
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

            File.Copy(filename, rval, true);

            return rval;
        }
    }
}
