using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace HdbPoet
{
    class ExcelReportTemplate: IExcelReportTool
    {
        private DataTable msDataTable;

        public ExcelReportTemplate(DataTable msDataTable)
        {
            // TODO: Complete member initialization
            this.msDataTable = msDataTable;
        }
        public void PrintPreview()
        {
        }
    }
}
