using System;
using System.Data;
using System.Configuration;
using NUnit.Framework;

namespace HdbPoet.Tests
{
    /// <summary>
    /// Test Create-Replace-Update-Delete related functionality
    /// </summary>
    [TestFixture]
    public class HdbPoetTestIO
    {
        public void Connect()
        {
            OracleLogin login = new OracleLogin();
            var logIn = new HdbPoet.OracleConnectionInfo();
            logIn.Host = "ibr3river.bor.doi.net";
            logIn.Service = "lchdb2.usbr.gov";
            logIn.Username = ConfigurationManager.AppSettings["admUser"];
            logIn.Password = ConfigurationManager.AppSettings["admPass"];
            var rval = new OracleServer(logIn);
            Hdb.Instance = new Hdb(rval);
        }

        private static GraphData CreateDataSet()
        {
            OracleHdb.TimeSeriesDataSet ds = new OracleHdb.TimeSeriesDataSet();
            ds.Series.AddSeriesRow(0, 0, "BEAVER DIVIDE", "min air temp", "HDB", "table0", "", "min air temp",
                "F", "month", false, 1187, false, "r_month", "", false, false, "N2", 0, "");
            /*
                Beaver Divide Min Air Temp SDI=1187
                1/1/1995	22
                2/1/1995	20
                3/1/1995	24
                4/1/1995	28
                5/1/1995	33
                6/1/1995	37
                7/1/1995	43
                8/1/1995	43
                9/1/1995	39
                10/1/1995	33
                11/1/1995	30
                12/1/1995	25
                1/1/1996	
                2/1/1996	
                3/1/1996	
                4/1/1996	
                5/1/1996	
                6/1/1996	
                7/1/1996	
                8/1/1996	
                9/1/1996	
                10/1/1996	
                11/1/1996	
                12/1/1996	
            */
            ds.Graph.AddGraphRow(ds.Graph.NewGraphRow());
            ds.Graph[0].GraphType = "TimeSeries";
            ds.Graph[0].BeginningDate = new DateTime(1995, 1, 1);
            ds.Graph[0].EndingDate = new DateTime(1996, 12, 31);
            ds.Graph[0].TimeWindowType = "FromXToY";
            ds.Graph[0].InstantInterval = 60;
            ds.Graph[0].GraphNumber = 0;
            GraphData gd = new GraphData(ds, 0);
            Hdb.Instance.Fill(gd);
            return gd;
        }

        [Test, Order(1)]
        public void TestRead()
        {
            Connect();
            var gd = CreateDataSet();
            var tbl = gd.Tables["table0"];            
            Assert.AreEqual(Convert.ToDouble(tbl.Rows[0]["VALUE"]), 22.0);
        }

        [Test, Order(2)]
        public void TestWrite()
        {
            Connect();
            var gd = CreateDataSet();
            //write 
            gd.Tables["table0"].Rows[14]["VALUE"] = 10;
            Hdb.Instance.SaveChanges("month", gd, false, 'Z');
            //verify success
            gd = CreateDataSet();
            Assert.AreEqual(Convert.ToDouble(gd.Tables["table0"].Rows[14]["VALUE"]), 10.0);
        }

        [Test, Order(3)]
        public void TestOverwriteFlagAdd()
        {
            Connect();
            Hdb.Instance.SetOverwriteFlag(1187, "month", new DateTime(1996, 3, 1), true, "");
            var gd = CreateDataSet();
            Assert.IsTrue(gd.Tables["table0"].Rows[14]["SOURCECOLOR"].ToString().ToLower() == "royalblue");
        }

        [Test, Order(4)]
        public void TestWriteOverwrite()
        {
            Connect();
            var gd = CreateDataSet();
            //write 
            gd.Tables["table0"].Rows[14]["VALUE"] = 15;
            Hdb.Instance.SaveChanges("month", gd, false, 'Z');
            //verify success
            gd = CreateDataSet();
            Assert.AreNotEqual(Convert.ToDouble(gd.Tables["table0"].Rows[14]["VALUE"]), 15.0);
        }

        [Test, Order(5)]
        public void TestOverwriteFlagRemove()
        {
            Connect();
            Hdb.Instance.SetOverwriteFlag(1187, "month", new DateTime(1996, 3, 1), false, "");
            var gd = CreateDataSet();
            Assert.IsTrue(gd.Tables["table0"].Rows[14]["SOURCECOLOR"].ToString().ToLower() == "skyblue");            
        }

        [Test, Order(6)]
        public void TestDelete()
        {
            Connect();
            var gd = CreateDataSet();
            //write
            gd.Tables["table0"].Rows[14]["VALUE"] = DBNull.Value;
            Hdb.Instance.SaveChanges("month", gd, false, 'Z');
            //verify
            gd = CreateDataSet();
            var a = gd.Tables["table0"].Rows[14]["VALUE"];
            Assert.AreEqual(gd.Tables["table0"].Rows[14]["VALUE"], DBNull.Value);
        }

    }
}
