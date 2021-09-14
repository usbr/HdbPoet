using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace HdbPoet.MetaData
{
    public class RatingTableData
    {

        internal const string RatingSQL = " SELECT rating_id, indep_site_datatype_id, rating_type_common_name, "
            + "effective_start_date_time, effective_end_date_time, date_time_loaded, agen_id"//, description "
            + "FROM ref_site_rating";


        internal const string RatingViewSQL = "SELECT r.rating_id, rating_type_common_name, r.agen_id, s.site_name, indep_site_datatype_id, "
            + "d.datatype_name, u.unit_common_name, r.effective_start_date_time, r.effective_end_date_time, r.date_time_loaded "//, r.Description "
            + "FROM ref_site_rating r, hdb_site_datatype sdi, hdb_datatype d, hdb_site s, hdb_unit u "
            + "WHERE sdi.site_datatype_id = r.indep_site_datatype_id AND d.datatype_id = sdi.datatype_id AND s.site_id = sdi.site_id " 
            + "AND d.unit_id = u.unit_id ORDER BY r.rating_id";        


        internal static DataTable RatingTableList(int rating_id = -1)
        {
            string sql = RatingViewSQL.Replace("ORDER BY r.rating_id", "");
            if (rating_id != -1)
                sql += " AND rating_id = "+rating_id;

           return Hdb.Instance.Server.Table("rating_list", sql);
        }

        internal static DataTable RatingTable(int rating_id)
        {
            string sql = "SELECT * FROM  ref_rating WHERE rating_id = "+rating_id;
            sql += " ORDER BY independent_value";
            return Hdb.Instance.Server.Table("ref_rating", sql);
        }


    }
}
