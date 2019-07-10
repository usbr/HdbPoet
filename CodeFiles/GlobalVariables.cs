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

                settings["objectTypes"].Value = s;
                SaveConfigFile();
            }
        }

        public static bool tableGraphHide
        {
            get { return Convert.ToBoolean(ConfigurationManager.AppSettings["hideGraph"]); }
            set
            {
                settings["hideGraph"].Value = value.ToString().ToLower();
                SaveConfigFile();
            }
        }

        public static string connectedUser
        {
            get { return userName; }
            set { userName = value.ToString().ToLower(); }
        }

        private static void SaveConfigFile()
        {
            try
            {
                configFile.Save(ConfigurationSaveMode.Modified);
                ConfigurationManager.RefreshSection(configFile.AppSettings.SectionInformation.Name);
            }
            catch
            {
                System.Windows.Forms.MessageBox.Show("Error editing application settings...", "Error", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }

        }
    }
}
