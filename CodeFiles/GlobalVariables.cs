using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace HdbPoet
{
    class GlobalVariables
    {
        private static Configuration configFile = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
        private static KeyValueConfigurationCollection settings = configFile.AppSettings.Settings;

        /// <summary>
        /// Method to modify the Configuration File
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public static void AddOrUpdateAppSettings(string key, string value)
        {
            try
            {
                if (settings[key] == null)
                {
                    settings.Add(key, value);
                }
                else
                {
                    settings[key].Value = value;
                }
                configFile.Save(ConfigurationSaveMode.Modified);
                ConfigurationManager.RefreshSection(configFile.AppSettings.SectionInformation.Name);
            }
            catch (ConfigurationErrorsException)
            {
                Console.WriteLine("Error writing app settings");
            }
        }

        private static bool showEmptySdidsCheck = true;
        private static bool showSimpleSdidInfoCheck = true;
        private static bool showBaseDataCheck = false;
        private static bool insertOnWriteCheck = false;
        private static bool overwriteFlagOnWriteCheck = false;
        private static char validationFlag = 'Z';
        private static bool qaqcValidationCheck = false;
        private static bool userDba = false;
        private static string userName = "";
        private static string dbSiteCodeFilter = "none";
        private static List<string> dbSiteCodeValues = new List<string>() { "none" };
        private static string dbAgencyCodeValue = System.Configuration.ConfigurationManager.AppSettings["AGEN_ID_NAME"];
        private static List<string> dbAgencyCodeValues = new List<string>() {};

        public static bool userIsDba
        {
            get { return userDba; }
            set { userDba = value; }
        }

        public static string dbSiteCode
        {
            get { return dbSiteCodeFilter; }
            set { dbSiteCodeFilter = value; }
        }

        public static List<string> dbSiteCodeOptions
        {
            get { return dbSiteCodeValues; }
            set { dbSiteCodeValues = value; }
        }

        public static string dbAgencyCode
        {
            get { return dbAgencyCodeValue; }
            set { dbAgencyCodeValue = value; }
        }

        public static List<string> dbAgencyCodeOptions
        {
            get { return dbAgencyCodeValues; }
            set { dbAgencyCodeValues = value; }
        }

        public static decimal agencyId
        {
            get
            {
                return Convert.ToDecimal(dbAgencyCodeValue.Split('|')[0].Trim());
            }
        }

        public static bool showEmptySdids
        {
            get { return showEmptySdidsCheck; }
            set { showEmptySdidsCheck = value; }
        }

        public static bool showSimpleSdiInfo
        {
            get { return showSimpleSdidInfoCheck; }
            set { showSimpleSdidInfoCheck = value; }
        }

        public static bool showBaseData
        {
            get { return showBaseDataCheck; }
            set { showBaseDataCheck = value; }
        }

        public static bool insertOnWrite
        {
            get { return insertOnWriteCheck; }
            set { insertOnWriteCheck = value; }
        }

        public static bool overwriteOnWrite
        {
            get { return overwriteFlagOnWriteCheck; }
            set { overwriteFlagOnWriteCheck = value; }
        }

        public static char writeValidationFlag
        {
            get { return validationFlag; }
            set { validationFlag = value; }
        }

        public static bool qaqcValidationToggled
        {
            get { return qaqcValidationCheck; }
            set { qaqcValidationCheck = value; }
        }

        public static List<int> hdbObjectTypes
        {
            get
            {
                string s = ConfigurationManager.AppSettings["objectTypes"];
                return s.Split(',').Select(x=>int.Parse(x)).ToList();
            }
            set
            {
                string s = "";
                foreach (int item in value)
                {
                    s += item.ToString() + ",";
                }
                s = s.TrimEnd(',');

                AddOrUpdateAppSettings("objectTypes", s);
                //settings["objectTypes"].Value = s;
                //SaveConfigFile();
            }
        }

        public static bool tableGraphHide
        {
            get
            {
                return Convert.ToBoolean(ConfigurationManager.AppSettings["hideGraph"]);
            }
            set
            {
                AddOrUpdateAppSettings("hideGraph", value.ToString().ToLower());
            }
        }

        public static string connectedUser
        {
            get { return userName; }
            set { userName = value.ToString().ToLower(); }
        }
    }
}
