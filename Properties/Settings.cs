using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Linq.Expressions;
using System.Linq;

namespace HdbPoet.Properties {
    
    
    // This class allows you to handle specific events on the settings class:
    //  The SettingChanging event is raised before a setting's value is changed.
    //  The PropertyChanged event is raised after a setting's value is changed.
    //  The SettingsLoaded event is raised after the setting values are loaded.
    //  The SettingsSaving event is raised before the setting values are saved.
    public sealed partial class Settings {
        
        public Settings() {
            // // To add event handlers for saving and changing settings, uncomment the lines below:
            //
            // this.SettingChanging += this.SettingChangingEventHandler;
            //
            // this.SettingsSaving += this.SettingsSavingEventHandler;
            //
        }
        
        private void SettingChangingEventHandler(object sender, System.Configuration.SettingChangingEventArgs e) {
            // Add code to handle the SettingChangingEvent event here.
        }
        
        private void SettingsSavingEventHandler(object sender, System.ComponentModel.CancelEventArgs e) {
            // Add code to handle the SettingsSaving event here.
        }


        internal static List<OracleConnectionInfo> GetConnectionInfoList()
        {
            var rval = new List<OracleConnectionInfo>();
            //host:uchdbdev.uc.usbr.gov service:uchdbdev.uc.usbr.gov
            //string pattern = "host:(?<host>[^\\s]+)\\s+service:(?<service>[^\\s]+)\\s+timezone:(?<timezone>[^\\s]{3,3})\\s+sid:(?<sid>[^\\s]+)";
            string pattern = "host:(?<host>[^\\s]+)\\s+service:(?<service>[^\\s]+)\\s+timezone:(?<timezone>[^\\s]{3,3})";
            Regex re = new Regex(pattern);
            foreach (string line in Settings.Default.HdbServers)
            {
                if (re.IsMatch(line))
                {
                    OracleConnectionInfo c = new OracleConnectionInfo();
                    Match m = re.Match(line);
                    c.Host = re.Match(line).Groups["host"].Value;
                    c.Service = re.Match(line).Groups["service"].Value;
                    c.ServicePrefix = c.Service;
                    int idx = c.ServicePrefix.IndexOf(".");
                    if (idx > 0)
                        c.ServicePrefix = c.ServicePrefix.Substring(0, idx);
                    c.Timezone = re.Match(line).Groups["timezone"].Value;

                    rval.Add(c);
                }
            }
            return rval;

        }

        internal static OracleConnectionInfo GetConnectionInfo(string host)
        {
           var list = GetConnectionInfoList();

           var query = (from c in list where c.Host == host select c).First();  
            return query;
        }

        internal static string[] GetServiceNames()
        {
           return(  from c in GetConnectionInfoList() select c.Service ).ToArray();
        }
    }
}
