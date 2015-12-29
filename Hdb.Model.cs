using System;
using System.Data;

namespace HdbPoet
{
    public partial class Hdb
    {

        public DataTable HDB_Model()
        {
            string sql = "Select * from HDB_Model order by model_id";
            return m_server.Table("Hdb_Model", sql);
        }

        /// <summary>
        /// Returns first of potential list of model_runs.
        /// (becomes baseline scenario)
        /// </summary>
        /// <returns></returns>
        public void FirstModelRunInfo(DateTime t, int model_id, 
            out int model_run_id, out string model_run_name, out string run_date)
        {
            string sql = "select model_run_id, model_run_name, run_date "
+ " from ref_model_run r where r.model_id=" + model_id + " and  run_date > "
            + ToHdbDate(t) + " and rownum =1";
            var tbl = m_server.Table("ref_model_run", sql);
            if (tbl.Rows.Count == 0)
            {
                model_run_id = -1;
                model_run_name = "";
                run_date = "";
            }
            else
            {
              model_run_id = Convert.ToInt32(tbl.Rows[0][0]);
              model_run_name = tbl.Rows[0]["model_run_name"].ToString();
              run_date = tbl.Rows[0]["run_date"].ToString();
            }
        }

        public DataTable ModelParameterList(DateTime t, int model_id, string m_table)
        {
            string sql = " select a.site_id, s.site_name, a.datatype_id, site_datatype_id, b.datatype_common_name, u.unit_common_name   "
+ " from hdb_site_datatype a , hdb_datatype b, hdb_unit u , hdb_site s "
+ " where "
+ " a.datatype_id = b.datatype_id and b.unit_id = u.unit_id "
+ " and s.site_id = a.site_id " 
+ " and site_datatype_id in ( "
+ " select distinct site_datatype_id from "+m_table+" where model_run_id in ( "
+ " select model_run_id "
+ " from ref_model_run r where r.model_id="+model_id+" and  run_date > "
            + ToHdbDate( t) 
+ " ) "
+ " ) ";
            DataTable SdiList = m_server.Table("sdi_list", sql);
            return SdiList;
        }



        public DataTable ref_model_run(int model_id, DateTime t)
        {
          //  DateTime t = DateTime.Now.Date.AddDays(-previousDaysRuns);

            string sql = "select model_run_id, model_run_name, user_name, run_date "
            + " from ref_model_run where model_id= " + model_id + " and run_date > "
            + ToHdbDate(t) + " order by run_date desc";

            return m_server.Table("ref_model_run", sql);
        }

    }
}
