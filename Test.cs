using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace HdbPoet
{
    public class Test
    {

        public static void Main()
        {
           OracleServer oracle = new OracleServer("", "","uchdbdev.uc.usbr.gov","","MST", "1521");
           Hdb.Instance.Server = oracle;

          ModifyValue();
           DeleteValue();
          // InsertValue();

        }

        private static void InsertValue()
        {
            
        }

        private static void DeleteValue()
        {
            TimeSeriesDataSet ds = CreateDataSet();

            DataTable tbl = ds.Tables[ds.Series[0].TableName];
            int rowCount = tbl.Rows.Count;

            Console.WriteLine(tbl.Rows[0][1]);
            tbl.Rows[0][1] = DBNull.Value;

            GraphData gd = new GraphData(ds, 0);
            Hdb.Instance.SaveChanges("day", gd, false, 'Z');

            ds = CreateDataSet();

            tbl = ds.Tables[ds.Series[0].TableName];
            if( rowCount != tbl.Rows.Count)
                Console.WriteLine("Error: Delete Test Failed");
            else
                Console.WriteLine("Delete Test ok");
        }

        private static void ModifyValue()
        {
            TimeSeriesDataSet ds = CreateDataSet();

            DataTable tbl = ds.Tables[ds.Series[0].TableName];
            double val = Convert.ToDouble(tbl.Rows[0][1]);
            Console.WriteLine("original value = "+val);
            val +=1;
            tbl.Rows[0][1] = val;

            GraphData gd = new GraphData(ds, 0);
            Hdb.Instance.SaveChanges("day", gd, true, 'Z');

            ds = CreateDataSet();
            
            tbl = ds.Tables[ds.Series[0].TableName];

            double val2 = Convert.ToDouble(tbl.Rows[0][1]);

            if (val2 != val)
            {
                Console.WriteLine("Error: Test failed");
            }
            else
                Console.WriteLine("Modify test ok");
        }

        private static TimeSeriesDataSet CreateDataSet()
        {
            TimeSeriesDataSet ds = new TimeSeriesDataSet();
            ds.Series.AddSeriesRow(0,1,"CABALLO", "storage",
                "HDB", "table0", "", "storage", "acre-feet", "day", false,
                2678, false, "r_day", "",false,false,"N2",0,"");
            ds.Graph.AddGraphRow(ds.Graph.NewGraphRow());
            ds.Graph[0].GraphType = "TimeSeries";
            ds.Graph[0].BeginningDate = new DateTime(2008, 4, 16);
            ds.Graph[0].EndingDate = new DateTime(2008, 5, 16);
            ds.Graph[0].TimeWindowType = "FromXToY";
            ds.Graph[0].InstantInterval = 15;
            GraphData gd = new GraphData(ds, 0);
            Hdb.Instance.Fill(gd);
            return ds;
        }
    }
}
