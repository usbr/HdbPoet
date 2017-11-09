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
        private static bool showEmptySdidsCheck = false;
        private static bool showBaseDataCheck = false;
        private static bool insertOnWriteCheck = false;
        private static bool overwriteFlagOnWriteCheck = false;
        private static char validationFlag = 'Z';

        public static bool showEmptySdids
        {
            get { return showEmptySdidsCheck; }
            set { showEmptySdidsCheck = value; }
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
                ConfigurationManager.AppSettings["objectTypes"] = s;
            }
        }
    }
}
