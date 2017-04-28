using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

#if HDB_OPEN
using Oracle.ManagedDataAccess.Client;
#else
using Devart.Data.Universal;
#endif

namespace HdbPoet
{
    public partial class Hdb
    {
        static int HDB_INVALID_ID = -1;
        static decimal s_AGEN_ID = HDB_INVALID_ID;
        static decimal s_COLLECTION_SYSTEM_ID = HDB_INVALID_ID;
        static decimal s_LOADING_APPLICATION_ID = HDB_INVALID_ID;
        static decimal s_METHOD_ID = HDB_INVALID_ID;
        static decimal s_COMPUTATION_ID = HDB_INVALID_ID;

        static  UniDbType GetVarCharType()
        {
#if HDB_OPEN
            return OracleDbType.Varchar2;
#else
            return UniDbType.VarChar;
#endif
        }
static UniDbType GetNumberType()
        {
#if HDB_OPEN
            return OracleDbType.Decimal;
#else
            return UniDbType.BigInt;
#endif
        }

        public int delete_from_mtable(int mrid, int sdi, DateTime t1, DateTime t2, string interval)
        {
            UniCommand cmd = new UniCommand("DELETE_M_TABLE");
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("MODEL_RUN_ID", mrid);
            cmd.Parameters.Add("SITE_DATATYPE_ID", sdi);
            cmd.Parameters.Add("START_DATE_TIME", t1);
            cmd.Parameters.Add("END_DATE_TIME", t2);
            cmd.Parameters.Add("INTERVAL", interval);

            int rval = 0;
            rval = m_server.RunStoredProc(cmd);
            return rval;
        }


        public int modify_m_table(int mrid, int sdi, DateTime t1, DateTime t2, double value, string interval, bool isNewEntry)
        {
            UniCommand cmd = new UniCommand("MODIFY_M_TABLE");
            cmd.CommandType = CommandType.StoredProcedure;

            string doUpdateYorN;
            if (isNewEntry)
            { doUpdateYorN = "Y"; }
            else
            { doUpdateYorN = "N"; }

            cmd.Parameters.Add("MODEL_RUN_ID", mrid);
            cmd.Parameters.Add("SITE_DATATYPE_ID", sdi);
            cmd.Parameters.Add("START_DATE_TIME", t1);
            cmd.Parameters.Add("END_DATE_TIME", t2);
            cmd.Parameters.Add("VALUE", value);
            cmd.Parameters.Add("INTERVAL", interval);
            cmd.Parameters.Add("DO_UPDATE_Y_OR_N", doUpdateYorN);

            int rval = 0;
            rval = m_server.RunStoredProc(cmd);
            return rval;
        }

        internal void delete_from_hdb(decimal sdi, DateTime t, string interval, string timeZone)
        {
            UniCommand cmd = new UniCommand("DELETE_FROM_HDB");
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("SAMPLE_SDI", sdi);
            cmd.Parameters.Add("SAMPLE_DATE_TIME", t);
            cmd.Parameters.Add("SAMPLE_END_TIME", DBNull.Value);
            cmd.Parameters.Add("SAMPLE_INTERVAL", interval);
            cmd.Parameters.Add("LOADING_APP_ID", s_LOADING_APPLICATION_ID);
            int modelrun_id = 0;
            cmd.Parameters.Add("MODELRUN_ID", modelrun_id);
            cmd.Parameters.Add("AGENCY_ID", s_AGEN_ID);

            cmd.Parameters.Add("time_zone", timeZone);
            m_server.RunStoredProc(cmd);

        }


        /// <summary>
        /// LookupApplication calls a stored procedure to 
        /// determine what id the hdb-poet application is using for the 
        /// specific HDB you are logged into.
        /// 
        /// Some other id's are also returned from the stored procedure:
        /// 
        /// the values returned are:
        /// 
        /// static_AGEN_ID 
        /// static_COLLECTION_SYSTEM_ID
        /// static_LOADING_APPLICATION_ID
        /// static_METHOD_ID 
        /// static_COMPUTATION_ID 
        /// </summary>
        void LookupApplication()
        {

            //if application is not in HDB2 give good error message:
            DataTable table = m_server.Table("HDB_LOADING_APPLICATION", "select * from HDB_LOADING_APPLICATION where Loading_Application_Name ='HDB-POET'");
            if (table.Rows.Count != 1)
            {
                throw new Exception("Error:  HDB-POET is not listed as an loading application");
            }


            UniCommand cmd = new UniCommand("LOOKUP_APPLICATION");
            cmd.CommandType = CommandType.StoredProcedure;

            string agen_id_name = System.Configuration.ConfigurationManager.AppSettings["AGEN_ID_NAME"];
            //inputs
            cmd.Parameters.Add("AGEN_ID_NAME", agen_id_name);
            cmd.Parameters.Add("COLLECTION_SYSTEM_NAME", "(see agency)");
            cmd.Parameters.Add("LOADING_APPLICATION_NAME", "HDB-POET");
            cmd.Parameters.Add("METHOD_NAME", "unknown");
            cmd.Parameters.Add("COMPUTATION_NAME", "unknown");

            //output parameters.
            var n = GetNumberType();
            //cmd.Parameters.Add("AGEN_ID",n , ParameterDirection.Output);
            UniParameter parameter = new UniParameter("AGEN_ID", n);
            parameter.Direction = ParameterDirection.Output;
            cmd.Parameters.Add(parameter);

            //cmd.Parameters.Add("COLLECTION_SYSTEM_ID", n, ParameterDirection.Output);
            parameter = new UniParameter("COLLECTION_SYSTEM_ID", n);
            parameter.Direction = ParameterDirection.Output;
            cmd.Parameters.Add(parameter);

            //cmd.Parameters.Add("LOADING_APPLICATION_ID", n, ParameterDirection.Output);
            parameter = new UniParameter("LOADING_APPLICATION_ID", n);
            parameter.Direction = ParameterDirection.Output;
            cmd.Parameters.Add(parameter);

            //cmd.Parameters.Add("METHOD_ID", n, ParameterDirection.Output);
            parameter = new UniParameter("METHOD_ID", n);
            parameter.Direction = ParameterDirection.Output;
            cmd.Parameters.Add(parameter);

            //cmd.Parameters.Add("COMPUTATION_ID", n, ParameterDirection.Output);
            parameter = new UniParameter("COMPUTATION_ID", n);
            parameter.Direction = ParameterDirection.Output;
            cmd.Parameters.Add(parameter);


            m_server.RunStoredProc(cmd);

            s_AGEN_ID = ToDecimal(cmd.Parameters["AGEN_ID"].Value);
            s_COLLECTION_SYSTEM_ID = ToDecimal(cmd.Parameters["COLLECTION_SYSTEM_ID"].Value);
            s_LOADING_APPLICATION_ID = ToDecimal(cmd.Parameters["LOADING_APPLICATION_ID"].Value);
            s_METHOD_ID = ToDecimal(cmd.Parameters["METHOD_ID"].Value);
            s_COMPUTATION_ID = ToDecimal(cmd.Parameters["COMPUTATION_ID"].Value);

        }

        private static decimal ToDecimal(object o)
        {
            return Convert.ToDecimal(o.ToString());
        }
        /*
         SET_VALIDATION(SITE_DATATYPE_ID NUMBER, INTERVAL VARCHAR2, START_DATE_TIME DATE, END_DATE_TIME DATE DEFAULT NULL, VALIDATION_FLAG VARCHAR2, STATUS OUT VARCHAR2);
        */

        internal void SetValidationFlag(decimal sdi,
          string interval, // 'instant', 'other', 'hour', 'day', 'month', 'year', 'wy', 'table interval'
          DateTime t,
          string validationFlag, string timeZone)
        {
            //status = "";
            UniCommand cmd = new UniCommand("hdb_utilities.set_validation");
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("SITE_DATATYPE_ID", sdi);
            cmd.Parameters.Add("INTERVAL", interval);
            cmd.Parameters.Add("START_DATE_TIME", t);
            cmd.Parameters.Add("END_DATE_TIME", DBNull.Value);
            cmd.Parameters.Add("VALIDATION_FLAG", GetVarCharType(), 1);
            if (validationFlag.Trim() == "")
                cmd.Parameters["VALIDATION_FLAG"].Value = DBNull.Value;
            else
                cmd.Parameters["VALIDATION_FLAG"].Value = validationFlag;

            cmd.Parameters.Add("time_zone", timeZone);

            int rval = 0;
            rval = m_server.RunStoredProc(cmd);
        }



        /*

  PROCEDURE SET_OVERWRITE_FLAG(SITE_DATATYPE_ID NUMBER, INTERVAL VARCHAR2, START_DATE_TIME DATE,
    END_DATE_TIME DATE DEFAULT NULL, OVERWRITE_FLAG VARCHAR2, STATUS OUT VARCHAR2);

         */


        internal void SetOverwriteFlag(decimal sdi,
          string interval, // 'instant', 'other', 'hour', 'day', 'month', 'year', 'wy', 'table interval'
          DateTime t,
          bool overwrite, string timeZone)
        //out string status) //   'O'  'null' 
        {
            //            status = "";
            UniCommand cmd = new UniCommand("hdb_utilities.set_overwrite_flag");
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("SITE_DATATYPE_ID", sdi);
            cmd.Parameters.Add("INTERVAL", interval);
            cmd.Parameters.Add("START_DATE_TIME", t);
            cmd.Parameters.Add("END_DATE_TIME", DBNull.Value);
            cmd.Parameters.Add("OVERWRITE_FLAG", GetVarCharType(), 1);
            //cmd.Parameters.Add("Status", OracleDbType.Varchar2,1024, ParameterDirection.Output);
            if (overwrite)
            {
                cmd.Parameters["OVERWRITE_FLAG"].Value = "O";
            }
            else
            {
                cmd.Parameters["OVERWRITE_FLAG"].Value = DBNull.Value;
            }

            cmd.Parameters.Add("time_zone", timeZone);

            int rval = 0;
            rval = m_server.RunStoredProc(cmd);

            // status = cmd.Parameters["Status"].Value.ToString();
        }


        /// <summary>
        /// Wrapper function to call the stored procedure named modify_r_base.
        /// This function is called for each time series value that is modified or 
        /// inserted.
        /// </summary>
        /// <returns></returns>
        internal int ModifyRase(
          decimal sdi,
          string interval, // 'instant', 'other', 'hour', 'day', 'month', 'year', 'wy', 'table interval'
          DateTime t,
          double value,
          bool overwrite, //   'O'  'null' 
          char VALIDATION,
           string timeZone) // 'V' '-' 'Z'
        {

            UniCommand cmd = new UniCommand("MODIFY_R_BASE_RAW");
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("SITE_DATATYPE_ID", sdi);
            cmd.Parameters.Add("INTERVAL", interval);
            cmd.Parameters.Add("START_DATE_TIME", t);
            cmd.Parameters.Add("END_DATE_TIME", DBNull.Value);
            cmd.Parameters.Add("VALUE", value);
            cmd.Parameters.Add("AGEN_ID", s_AGEN_ID);
            cmd.Parameters.Add("OVERWRITE_FLAG", GetVarCharType(), 1);

            if (overwrite)
            {
                cmd.Parameters["OVERWRITE_FLAG"].Value = "O";
            }
            else
            {
                cmd.Parameters["OVERWRITE_FLAG"].Value = DBNull.Value;
            }

            cmd.Parameters.Add("VALIDATION", VALIDATION);
            cmd.Parameters.Add("COLLECTION_SYSTEM_ID", s_COLLECTION_SYSTEM_ID);
            cmd.Parameters.Add("LOADING_APPLICATION_ID", s_LOADING_APPLICATION_ID);
            cmd.Parameters.Add("METHOD_ID", s_METHOD_ID);
            cmd.Parameters.Add("COMPUTATION_ID", s_COMPUTATION_ID);

            cmd.Parameters.Add("DO_UPDATE_Y_OR_N", "Y");
            cmd.Parameters.Add("DATA_FLAGS", DBNull.Value);
            cmd.Parameters.Add("time_zone", timeZone);

            int rval = 0;
            rval = m_server.RunStoredProc(cmd);
            return rval;
        }


        internal int Calculate_Series(decimal sdi, string interval, DateTime t, string timeZone)
        {
            UniCommand cmd = new UniCommand("HDB_POET.CALCULATE_SERIES");
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("SITE_DATATYPE_ID", sdi);
            cmd.Parameters.Add("INTERVAL", interval);
            cmd.Parameters.Add("START_TIME", t);
            cmd.Parameters.Add("time_zone", timeZone);

            int rval = m_server.RunStoredProc(cmd);

            return rval;

        }
        internal void AclAddUserAndGroup(string user, string group)
        {
            Modify_Acl(user, group, true, false);
        }
        internal void AclDeleteGroup(string group)
        {
            var tbl = m_server.Table("a", "select user_name from ref_user_groups where group_name ='" + group + "'");

            foreach (DataRow row in tbl.Rows)
            {
                Modify_Acl(row["user_name"].ToString(), group, true, true);
            }
            //  Modify_Acl(user, group,true, true);
        }

        internal void AclDeleteUser(string user)
        {
            var tbl = m_server.Table("a", "select group_name from ref_user_groups where user_name ='" + user + "'");

            foreach (DataRow row in tbl.Rows)
            {
                Modify_Acl(user, row["group_name"].ToString(), true, true);
            }
        }



        private int Modify_Acl(string user, string group, bool active, bool delete)
        {
            UniCommand cmd = new UniCommand("hdb_utilities.modify_acl");
            cmd.CommandType = CommandType.StoredProcedure;

            string p_active_flag = active ? "Y" : "N";
            string p_delete_flag = delete ? "Y" : "N";

            cmd.Parameters.Add("p_user_name", user.ToUpper());
            cmd.Parameters.Add("p_group_name", group.ToUpper());
            cmd.Parameters.Add("p_active_flag", p_active_flag);
            cmd.Parameters.Add("p_delete_flag", p_delete_flag);

            int rval = m_server.RunStoredProc(cmd);

            return rval;
        }


        internal void AclAddSite(decimal site_id, string group)
        {
            Modify_Site_Group_Name(site_id, group, false);
        }
        internal void AclDeleteSite(decimal site_id, string group)
        {
            Modify_Site_Group_Name(site_id, group, true);
        }

        private int Modify_Site_Group_Name(decimal site_id, string group,
             bool delete)
        {
            UniCommand cmd = new UniCommand("hdb_utilities.modify_site_group_name");
            cmd.CommandType = CommandType.StoredProcedure;

            string p_delete_flag = delete ? "Y" : "N";

            cmd.Parameters.Add("p_site_id", site_id);
            cmd.Parameters.Add("p_group_name", group.ToUpper());
            cmd.Parameters.Add("p_delete_flag", p_delete_flag);

            int rval = m_server.RunStoredProc(cmd);

            return rval;

        }

        internal bool AclReadonly(int sdi)
        {
            UniCommand cmd = new UniCommand("hdb_utilities.is_sdi_in_acl");
            cmd.CommandType = CommandType.StoredProcedure;

            //cmd.Parameters.Add("RS", GetVarCharType(), 10, null, ParameterDirection.ReturnValue);
            UniParameter parameter = new UniParameter("RS", GetVarCharType(), 10);
            parameter.Value = null;
            parameter.Direction = ParameterDirection.ReturnValue;
            cmd.Parameters.Add(parameter);

            cmd.Parameters.Add("P_SITE_DATATYPE_ID", (decimal)sdi);

            int rval = m_server.RunStoredProc(cmd);

            object o = cmd.Parameters["RS"].Value;

            if (o.ToString() == "Y")
                return false;

            return true;
        }
        
    }
}
