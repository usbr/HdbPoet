using Reclamation.Core;
using System.IO;

namespace HdbPoet
{
    class ExcelReportTemplate: IExcelReportTool
    {
        private MultipleSeriesDataTable msDataTable;
        string xlsFilename;

        public ExcelReportTemplate(MultipleSeriesDataTable msDataTable)
        {
            this.msDataTable = msDataTable;
        }

        public void PrintPreview()
        {
            if (msDataTable != null)
            {
                // remove new line characters in header for proper csv export
                for (int i = 0; i < msDataTable.Columns.Count; i++)
                {
                    msDataTable.Columns[i].ColumnName = msDataTable.Columns[i].ColumnName.Replace("\r\n", " ");
                }
                string filename = Path.ChangeExtension(Path.GetTempFileName(), ".csv");
                CsvFile.WriteToCSV(msDataTable, filename, false);
                System.Diagnostics.Process.Start(filename);
            }
        }

    }
}
