using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using HdbPoet.Properties;
using System.Collections.Specialized;
using System.Data;

namespace HdbPoet
{
    public class OracleConnectionInfo
    {

        public OracleConnectionInfo()
        {

        }
        public string Host { get; set; }
        public string Port { get; set; }
        public string ServicePrefix { get; set; }
        public string Service { get; set; }
        public string Timezone { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }

    }
}
