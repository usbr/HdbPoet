using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace HdbPoet.MetaData
{
    public class RatingTableData
    {

        internal const string RatingSQL = " select RATING_ID, INDEP_SITE_DATATYPE_ID, RATING_TYPE_COMMON_NAME, "
            +" EFFECTIVE_START_DATE_TIME, EFFECTIVE_END_DATE_TIME, DATE_TIME_LOADED, AGEN_ID, "
            +" DESCRIPTION from ref_site_rating";


        internal const string RatingViewSQL = "select r.rating_id, rating_type_common_name, r.agen_id, s.site_name ,indep_site_datatype_id, "
           + " d.datatype_name, u.unit_common_name, EFFECTIVE_START_DATE_TIME, EFFECTIVE_END_DATE_TIME, DATE_TIME_LOADED , r.Description "
+ " FROM "
+ "   ref_site_rating r, hdb_site_datatype sdi,  hdb_datatype d, hdb_site s, hdb_unit u "
+ " where  sdi.site_datatype_id = r.indep_site_datatype_id "
+ "      and  d.datatype_id = sdi.datatype_id "
+ "      and s.site_id = sdi.site_id"
+ "      and d.unit_id = u.unit_id ";


        internal static DataTable RatingTableList(int rating_id = -1)
        {

            string sql = RatingViewSQL;
            if (rating_id != -1)
                sql += " AND rating_id = "+rating_id;

           return Hdb.Instance.Server.Table("rating_list", sql);

        }

        internal static DataTable RatingTable(int rating_id)
        {
            string sql = "select * from  ref_rating where rating_id = "+rating_id;
            sql += " order by independent_value";
            return Hdb.Instance.Server.Table("ref_rating", sql);
        }


    }
}
