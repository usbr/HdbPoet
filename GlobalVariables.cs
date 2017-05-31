using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HdbPoet
{
    class GlobalVariables
    {

        
            private static bool showEmptySdidsCheck = false;
            private static bool showBaseDataCheck = false;
            private static bool insertOnWriteCheck = false;

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
        
    }
}
