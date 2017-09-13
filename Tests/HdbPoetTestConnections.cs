using System.Configuration;
using NUnit.Framework;

namespace HdbPoet.Tests
{
    /// <summary>
    /// Test HDB connection and user related funtionality
    /// </summary>
    [TestFixture]
    public class HdbPoetTestConnectivity
    {
        private Configuration GetConfig()
        {
            ExeConfigurationFileMap custmConfg = new ExeConfigurationFileMap();
            custmConfg.ExeConfigFilename = @"d:\test\test.XML";
            Configuration config = ConfigurationManager.OpenMappedExeConfiguration(custmConfg, ConfigurationUserLevel.None);
            return config;
        }

        [Test]
        public void TestConnectionLC()
        {
            OracleLogin login = new OracleLogin();
            var logIn= new HdbPoet.OracleConnectionInfo();
            logIn.Host = "ibr3river.bor.doi.net";
            logIn.Service = "lchdb2.usbr.gov";
            logIn.Username = ConfigurationManager.AppSettings["lcUser"];
            logIn.Password = ConfigurationManager.AppSettings["lcPass"];
            var rval = new OracleServer(logIn);
            Assert.IsTrue(rval.ConnectionWorking());
        }

        [Test]
        public void TestConnectionUC()
        {
            OracleLogin login = new OracleLogin();
            var logIn = new HdbPoet.OracleConnectionInfo();
            logIn.Host = "uchdb2.uc.usbr.gov";
            logIn.Service = "uchdb2.uc.usbr.gov";
            logIn.Username = ConfigurationManager.AppSettings["ucUser"];
            logIn.Password = ConfigurationManager.AppSettings["ucPass"];
            var rval = new OracleServer(logIn);
            Assert.IsTrue(rval.ConnectionWorking());
        }

        [Test]
        public void TestConnectionYAO()
        {
            OracleLogin login = new OracleLogin();
            var logIn = new HdbPoet.OracleConnectionInfo();
            logIn.Host = "ibr3yaohdb.bor.doi.net";
            logIn.Service = "yaohdb";
            logIn.Username = ConfigurationManager.AppSettings["yaoUser"];
            logIn.Password = ConfigurationManager.AppSettings["yaoPass"];
            var rval = new OracleServer(logIn);
            Assert.IsTrue(rval.ConnectionWorking());
        }

        [Test]
        public void TestConnectionECAO()
        {
            OracleLogin login = new OracleLogin();
            var logIn = new HdbPoet.OracleConnectionInfo();
            logIn.Host = "ecohdb.bor.doi.net";
            logIn.Service = "ecohdb.bor.doi.net";
            logIn.Username = ConfigurationManager.AppSettings["ecoUser"];
            logIn.Password = ConfigurationManager.AppSettings["ecoPass"];
            var rval = new OracleServer(logIn);
            Assert.IsTrue(rval.ConnectionWorking());
        }

        [Test]
        public void TestAdminOptionsVisible()
        {
            OracleLogin login = new OracleLogin();
            var logIn = new HdbPoet.OracleConnectionInfo();
            logIn.Host = "ibr3river.bor.doi.net";
            logIn.Service = "lchdb2.usbr.gov";
            logIn.Username = ConfigurationManager.AppSettings["lcUser"];
            logIn.Password = ConfigurationManager.AppSettings["lcPass"];
            var rval = new OracleServer(logIn);
            Hdb.Instance = new Hdb(rval);
            Assert.IsFalse(Hdb.Instance.IsAclAdministrator);
        }

        [Test]
        public void TestAdminOptionsHidden()
        {
            OracleLogin login = new OracleLogin();
            var logIn = new HdbPoet.OracleConnectionInfo();
            logIn.Host = "ibr3river.bor.doi.net";
            logIn.Service = "lchdb2.usbr.gov";
            logIn.Username = ConfigurationManager.AppSettings["admUser"];
            logIn.Password = ConfigurationManager.AppSettings["admPass"];
            var rval = new OracleServer(logIn);
            Hdb.Instance = new Hdb(rval);
            Assert.IsTrue(Hdb.Instance.IsAclAdministrator);
        }


    }
}
