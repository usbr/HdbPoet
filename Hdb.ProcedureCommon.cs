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

#if HDB_OPEN
        static OracleCommand GetCommandProvider()
        {
            return new OracleCommand();
        }
        static OracleParameter GetCommandOutputParameter()
        {
            var par = new OracleParameter();
            par.OracleDbType = GetNumberType();
            par.Direction = ParameterDirection.Output;
            return par;
        }
        static OracleParameter GetCommandReturnParameter()
        {
            var par = new OracleParameter();
            par.OracleDbType = GetVarCharType();
            par.Direction = ParameterDirection.ReturnValue;
            return par;
        }
        static OracleDbType GetVarCharType()
        {
            return OracleDbType.Varchar2;
        }
        static OracleDbType GetIntegerType()
        {
            return OracleDbType.Int32;
        }

        static OracleDbType GetNumberType()
        {
            return OracleDbType.Decimal;
        }

        static OracleDbType GetDateTimeType()
        {
            return OracleDbType.Date;
        }


#else
        static UniCommand GetCommandProvider()
        {
            return new UniCommand();
        }
        static UniParameter GetCommandOutputParameter()
        {
            var par = new UniParameter();
            par.UniDbType = GetNumberType();
            par.Direction = ParameterDirection.Output;
            return par;
        }
        static UniParameter GetCommandReturnParameter()
        {
            var par = new UniParameter();
            par.UniDbType = GetVarCharType();
            par.Direction = ParameterDirection.ReturnValue;
            return par;
        }
        static UniDbType GetVarCharType()
        {
            return UniDbType.VarChar;
        }
        static UniDbType GetIntegerType()
        {
            return UniDbType.BigInt;
        }
        static UniDbType GetNumberType()
        {
            return UniDbType.Double;
        }
        static UniDbType GetDateTimeType()
        {
            return UniDbType.DateTime;
        }
#endif


        public int delete_from_mtable(int mrid, int sdi, DateTime t1, DateTime t2, string interval)
        {
            var cmd = GetCommandProvider();
            cmd.CommandText = "DELETE_M_TABLE";
            //UniCommand cmd = new UniCommand("DELETE_M_TABLE");
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("MODEL_RUN_ID", GetIntegerType());
            cmd.Parameters["MODEL_RUN_ID"].Value = mrid;

            cmd.Parameters.Add("SITE_DATATYPE_ID", GetIntegerType());
            cmd.Parameters["SITE_DATATYPE_ID"].Value = sdi;

            cmd.Parameters.Add("START_DATE_TIME", GetDateTimeType());
            cmd.Parameters["START_DATE_TIME"].Value = t1;

            cmd.Parameters.Add("END_DATE_TIME", GetDateTimeType());
            cmd.Parameters["END_DATE_TIME"].Value = t2;

            cmd.Parameters.Add("INTERVAL", GetVarCharType());
            cmd.Parameters["INTERVAL"].Value = interval;


            int rval = 0;
            rval = m_server.RunStoredProc(cmd);
            return rval;
        }


        public int modify_m_table(int mrid, int sdi, DateTime t1, DateTime t2, double value, string interval, bool isNewEntry)
        {
            var cmd = GetCommandProvider();
            cmd.CommandText = "MODIFY_M_TABLE";
            //UniCommand cmd = new UniCommand("MODIFY_M_TABLE");
            cmd.CommandType = CommandType.StoredProcedure;

            string doUpdateYorN;
            if (isNewEntry)
            { doUpdateYorN = "Y"; }
            else
            { doUpdateYorN = "N"; }

            cmd.Parameters.Add("MODEL_RUN_ID", GetIntegerType());
            cmd.Parameters["MODEL_RUN_ID"].Value = mrid;

            cmd.Parameters.Add("SITE_DATATYPE_ID", GetIntegerType());
            cmd.Parameters["SITE_DATATYPE_ID"].Value = sdi;

            cmd.Parameters.Add("START_DATE_TIME", GetDateTimeType());
            cmd.Parameters["START_DATE_TIME"].Value = t1;

            cmd.Parameters.Add("END_DATE_TIME", GetDateTimeType());
            cmd.Parameters["END_DATE_TIME"].Value = t2;

            cmd.Parameters.Add("VALUE", GetNumberType());
            cmd.Parameters["VALUE"].Value = value;

            cmd.Parameters.Add("INTERVAL", GetVarCharType());
            cmd.Parameters["INTERVAL"].Value = interval;

            cmd.Parameters.Add("DO_UPDATE_Y_OR_N", GetVarCharType());
            cmd.Parameters["DO_UPDATE_Y_OR_N"].Value = doUpdateYorN;


            int rval = 0;
            rval = m_server.RunStoredProc(cmd);
            return rval;
        }

        internal void delete_from_hdb(decimal sdi, DateTime t, string interval, string timeZone)
        {
            var cmd = GetCommandProvider();
            cmd.CommandText = "DELETE_FROM_HDB";
            //UniCommand cmd = new UniCommand("DELETE_FROM_HDB");
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("SAMPLE_SDI", GetIntegerType());
            cmd.Parameters["SAMPLE_SDI"].Value = sdi;

            cmd.Parameters.Add("SAMPLE_DATE_TIME", GetDateTimeType());
            cmd.Parameters["SAMPLE_DATE_TIME"].Value = t;

            cmd.Parameters.Add("SAMPLE_END_TIME", GetDateTimeType());
            cmd.Parameters["SAMPLE_END_TIME"].Value = DBNull.Value;

            cmd.Parameters.Add("SAMPLE_INTERVAL", GetVarCharType());
            cmd.Parameters["SAMPLE_INTERVAL"].Value = interval;

            cmd.Parameters.Add("LOADING_APP_ID", GetVarCharType());
            cmd.Parameters["LOADING_APP_ID"].Value = s_LOADING_APPLICATION_ID;

            int modelrun_id = 0;
            cmd.Parameters.Add("MODELRUN_ID", GetIntegerType());
            cmd.Parameters["MODELRUN_ID"].Value = modelrun_id;

            cmd.Parameters.Add("AGENCY_ID", GetVarCharType());
            cmd.Parameters["AGENCY_ID"].Value = s_AGEN_ID;

            cmd.Parameters.Add("time_zone", GetVarCharType());
            cmd.Parameters["time_zone"].Value = timeZone;

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

            var cmd = GetCommandProvider();
            cmd.CommandText = "LOOKUP_APPLICATION";
            //UniCommand cmd = new UniCommand("LOOKUP_APPLICATION");
            cmd.CommandType = CommandType.StoredProcedure;

            string agen_id_name = System.Configuration.ConfigurationManager.AppSettings["AGEN_ID_NAME"];

            //inputs
            cmd.Parameters.Add("AGEN_ID_NAME", GetVarCharType());
            cmd.Parameters["AGEN_ID_NAME"].Value = agen_id_name;

            cmd.Parameters.Add("COLLECTION_SYSTEM_NAME", GetVarCharType());
            cmd.Parameters["COLLECTION_SYSTEM_NAME"].Value = "(see agency)";

            cmd.Parameters.Add("LOADING_APPLICATION_NAME", GetVarCharType());
            cmd.Parameters["LOADING_APPLICATION_NAME"].Value = "HDB-POET";

            cmd.Parameters.Add("METHOD_NAME", GetVarCharType());
            cmd.Parameters["METHOD_NAME"].Value = "unknown";

            cmd.Parameters.Add("COMPUTATION_NAME", GetVarCharType());
            cmd.Parameters["COMPUTATION_NAME"].Value = "unknown";

            //output parameters.
            
            var par = GetCommandOutputParameter();
            par.ParameterName = "AGEN_ID";
            cmd.Parameters.Add(par);
            
            par = GetCommandOutputParameter();
            par.ParameterName = "COLLECTION_SYSTEM_ID";
            cmd.Parameters.Add(par);
            
            par = GetCommandOutputParameter();
            par.ParameterName = "LOADING_APPLICATION_ID";
            cmd.Parameters.Add(par);
            
            par = GetCommandOutputParameter();
            par.ParameterName = "METHOD_ID";
            cmd.Parameters.Add(par);
            
            par = GetCommandOutputParameter();
            par.ParameterName = "COMPUTATION_ID";
            cmd.Parameters.Add(par);


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
            var cmd = GetCommandProvider();
            cmd.CommandText = "hdb_utilities.set_validation";
            //UniCommand cmd = new UniCommand("hdb_utilities.set_validation");
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("SITE_DATATYPE_ID", GetIntegerType());
            cmd.Parameters["SITE_DATATYPE_ID"].Value = sdi;

            cmd.Parameters.Add("INTERVAL", GetVarCharType());
            cmd.Parameters["INTERVAL"].Value = interval;

            cmd.Parameters.Add("START_DATE_TIME", GetDateTimeType());
            cmd.Parameters["START_DATE_TIME"].Value = t;

            cmd.Parameters.Add("END_DATE_TIME", GetDateTimeType());
            cmd.Parameters["END_DATE_TIME"].Value = DBNull.Value;

            cmd.Parameters.Add("VALIDATION_FLAG", GetVarCharType());
            if (validationFlag.Trim() == "")
            { cmd.Parameters["VALIDATION_FLAG"].Value = DBNull.Value; }
            else
            { cmd.Parameters["VALIDATION_FLAG"].Value = validationFlag; }

            cmd.Parameters.Add("time_zone", GetVarCharType());
            cmd.Parameters["time_zone"].Value = timeZone;

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
            var cmd = GetCommandProvider();
            cmd.CommandText = "hdb_utilities.set_overwrite_flag";
            //UniCommand cmd = new UniCommand("hdb_utilities.set_overwrite_flag");
            cmd.CommandType = CommandType.StoredProcedure;
            
            cmd.Parameters.Add("SITE_DATATYPE_ID", GetIntegerType());
            cmd.Parameters["SITE_DATATYPE_ID"].Value = sdi;

            cmd.Parameters.Add("INTERVAL", GetVarCharType());
            cmd.Parameters["INTERVAL"].Value = interval;

            cmd.Parameters.Add("START_DATE_TIME", GetDateTimeType());
            cmd.Parameters["START_DATE_TIME"].Value = t;

            cmd.Parameters.Add("END_DATE_TIME", GetDateTimeType());
            cmd.Parameters["END_DATE_TIME"].Value = DBNull.Value;

            cmd.Parameters.Add("OVERWRITE_FLAG", GetVarCharType());
            if (overwrite)
            { cmd.Parameters["OVERWRITE_FLAG"].Value = "O"; }
            else
            { cmd.Parameters["OVERWRITE_FLAG"].Value = DBNull.Value; }

            cmd.Parameters.Add("time_zone", GetVarCharType());
            cmd.Parameters["time_zone"].Value = timeZone;

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
        internal int ModifyRBase(
          decimal sdi,
          string interval, // 'instant', 'other', 'hour', 'day', 'month', 'year', 'wy', 'table interval'
          DateTime t,
          double value,
          bool overwrite, //   'O'  'null' 
          char s_validation,
           string timeZone) // 'V' '-' 'Z'
        {
            var cmd = GetCommandProvider();
            cmd.CommandText = "MODIFY_R_BASE_RAW";
            //UniCommand cmd = new UniCommand("MODIFY_R_BASE_RAW");
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("SITE_DATATYPE_ID", GetIntegerType());
            cmd.Parameters["SITE_DATATYPE_ID"].Value = sdi;

            cmd.Parameters.Add("INTERVAL", GetVarCharType());
            cmd.Parameters["INTERVAL"].Value = interval;

            cmd.Parameters.Add("START_DATE_TIME", GetDateTimeType());
            cmd.Parameters["START_DATE_TIME"].Value = t;

            cmd.Parameters.Add("END_DATE_TIME", GetDateTimeType());
            cmd.Parameters["END_DATE_TIME"].Value = DBNull.Value;

            cmd.Parameters.Add("VALUE", GetNumberType());
            cmd.Parameters["VALUE"].Value = value;

            cmd.Parameters.Add("AGEN_ID", GetIntegerType());
            cmd.Parameters["AGEN_ID"].Value = s_AGEN_ID;

            cmd.Parameters.Add("OVERWRITE_FLAG", GetVarCharType());
            if (overwrite || GlobalVariables.overwriteOnWrite)
            { cmd.Parameters["OVERWRITE_FLAG"].Value = "O"; }
            else
            { cmd.Parameters["OVERWRITE_FLAG"].Value = DBNull.Value; }

            if (GlobalVariables.writeValidationFlag != 'Z')
            { s_validation = GlobalVariables.writeValidationFlag; }
            cmd.Parameters.Add("VALIDATION", GetVarCharType());
            cmd.Parameters["VALIDATION"].Value = s_validation;

            cmd.Parameters.Add("COLLECTION_SYSTEM_ID", GetVarCharType());
            cmd.Parameters["COLLECTION_SYSTEM_ID"].Value = s_COLLECTION_SYSTEM_ID;

            cmd.Parameters.Add("LOADING_APPLICATION_ID", GetVarCharType());
            cmd.Parameters["LOADING_APPLICATION_ID"].Value = s_LOADING_APPLICATION_ID;

            cmd.Parameters.Add("METHOD_ID", GetVarCharType());
            cmd.Parameters["METHOD_ID"].Value = s_METHOD_ID;

            cmd.Parameters.Add("COMPUTATION_ID", GetVarCharType());
            cmd.Parameters["COMPUTATION_ID"].Value = s_COMPUTATION_ID;

            cmd.Parameters.Add("DO_UPDATE_Y_OR_N", GetVarCharType());
            if (GlobalVariables.insertOnWrite)
            { cmd.Parameters["DO_UPDATE_Y_OR_N"].Value = "N"; }
            else
            { cmd.Parameters["DO_UPDATE_Y_OR_N"].Value = "Y"; }

            cmd.Parameters.Add("DATA_FLAGS", GetVarCharType());
            cmd.Parameters["DATA_FLAGS"].Value = DBNull.Value;

            cmd.Parameters.Add("time_zone", GetVarCharType());
            cmd.Parameters["time_zone"].Value = timeZone;

            int rval = 0;
            rval = m_server.RunStoredProc(cmd);
            return rval;
        }


        internal int Calculate_Series(decimal sdi, string interval, DateTime t, string timeZone)
        {
            var cmd = GetCommandProvider();
            cmd.CommandText = "HDB_POET.CALCULATE_SERIES";
            //UniCommand cmd = new UniCommand("HDB_POET.CALCULATE_SERIES");
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("SITE_DATATYPE_ID", GetVarCharType());
            cmd.Parameters["SITE_DATATYPE_ID"].Value = sdi;

            cmd.Parameters.Add("INTERVAL", GetVarCharType());
            cmd.Parameters["INTERVAL"].Value = interval;

            cmd.Parameters.Add("START_TIME", GetDateTimeType());
            cmd.Parameters["START_TIME"].Value = t;

            cmd.Parameters.Add("time_zone", GetVarCharType());
            cmd.Parameters["time_zone"].Value = timeZone;

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
            var cmd = GetCommandProvider();
            cmd.CommandText = "hdb_utilities.modify_acl";
            //UniCommand cmd = new UniCommand("hdb_utilities.modify_acl");
            cmd.CommandType = CommandType.StoredProcedure;

            string p_active_flag = active ? "Y" : "N";
            string p_delete_flag = delete ? "Y" : "N";

            cmd.Parameters.Add("p_user_name", GetVarCharType());
            cmd.Parameters["p_user_name"].Value = user.ToUpper();

            cmd.Parameters.Add("p_group_name", GetVarCharType());
            cmd.Parameters["p_group_name"].Value = group.ToUpper();

            cmd.Parameters.Add("p_active_flag", GetVarCharType());
            cmd.Parameters["p_active_flag"].Value = p_active_flag;

            cmd.Parameters.Add("p_delete_flag", GetVarCharType());
            cmd.Parameters["p_delete_flag"].Value = p_delete_flag;

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
            var cmd = GetCommandProvider();
            cmd.CommandText = "hdb_utilities.modify_site_group_name";
            //UniCommand cmd = new UniCommand("hdb_utilities.modify_site_group_name");
            cmd.CommandType = CommandType.StoredProcedure;

            string p_delete_flag = delete ? "Y" : "N";

            cmd.Parameters.Add("p_site_id", GetVarCharType());
            cmd.Parameters["p_site_id"].Value = site_id;

            cmd.Parameters.Add("p_group_name", GetVarCharType());
            cmd.Parameters["p_group_name"].Value = group.ToUpper();

            cmd.Parameters.Add("p_delete_flag", GetVarCharType());
            cmd.Parameters["p_delete_flag"].Value = p_delete_flag;
            
            int rval = m_server.RunStoredProc(cmd);

            return rval;

        }

        internal bool AclReadonly(int sdi)
        {
            var cmd = GetCommandProvider();
            cmd.CommandText = "hdb_utilities.is_sdi_in_acl";
            //UniCommand cmd = new UniCommand("hdb_utilities.is_sdi_in_acl");
            cmd.CommandType = CommandType.StoredProcedure;

            //cmd.Parameters.Add("RS", GetVarCharType(), 10, null, ParameterDirection.ReturnValue);
            //var par = GetParame
            var par = GetCommandReturnParameter();
            par.ParameterName = "RS";
            par.Size = 10;
            //var parameter = new OracleParameter("RS", GetVarCharType(), 10);
            par.Value = null;
            //parameter.Direction = ParameterDirection.ReturnValue;
            cmd.Parameters.Add(par);

            cmd.Parameters.Add("P_SITE_DATATYPE_ID", GetIntegerType());
            cmd.Parameters["P_SITE_DATATYPE_ID"].Value = sdi;

            int rval = m_server.RunStoredProc(cmd);

            object o = cmd.Parameters["RS"].Value;

            if (o.ToString() == "Y")
                return false;

            return true;
        }
        
    }
}
