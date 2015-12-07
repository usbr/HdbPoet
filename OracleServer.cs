using System;
using System.Data;
using System.Linq;
using System.Diagnostics;
#if HDB_OPEN
using Oracle.ManagedDataAccess.Client;
#else
using Devart.Data.Oracle;
#endif

using System.Collections;
using System.Windows.Forms;
using Reclamation.Core;
namespace HdbPoet
{
    /// <summary>
    /// Oracle Database specific
    /// </summary>
    public class OracleServer: BasicDBServer
    {
       // string m_connectionString = null;
        string lastSqlCommand;
        public ArrayList sqlCommands = new ArrayList();
        string lastMessage;
        string username, service = "";

        public string Username
        {
            get { return username; }
            set { username = value; }
        }
        string m_host = "";

        public string Host
        {
            get { return m_host; }
            set { m_host = value; }
        }

        string m_port = "";
        public string Port
        {
            get { return m_port; }
            set { m_port = value; }
        }

        bool loginCanceled = false;
        string m_timeZone = "";

        public string TimeZone
        {
            get { return m_timeZone; }
            //set { m_timeZone = value; }
        }

        public override string DataSource
        {
            get { return Host + ":" + service; }
        }

        public OracleServer(OracleConnectionInfo ci)
        {
            this.username = ci.Username;
            this.service = ci.Service;
            this.m_host = ci.Host;
            this.m_port = ci.Port;
            this.m_timeZone = ci.Timezone;
            sqlCommands.Clear();
            MakeConnectionString(username, ci.Password);
        }
        /// <summary>
        /// Creates instance of Oracle class with inputs.
        /// </summary>
        public OracleServer(string username, string password, string host, string service, string timeZone, string port)
        {
            this.username = username;
            this.service = service;
            this.m_host = host;
            this.m_port = port;
            this.m_timeZone = timeZone;
            sqlCommands.Clear();
            MakeConnectionString(username, password);
        }


        public string[] OracleUsers
        {
            get
            {

                var tbl = Table("a","select username from all_users order by username");

                return (from row in tbl.AsEnumerable()
                                select row.Field<string>("username")).ToArray();
            }
        }


        public bool LoginCanceled
        {
            get { return loginCanceled; }
        }

        public string ServiceName
        {
            get { return this.service; }
        }

        //public string ConnectionString
        //{
        //    get { return this.m_connectionString; }
        //}


        /// <summary>
        /// returns true if connection is working.
        /// </summary>
        /// <returns></returns>
        public bool ConnectionWorking()
        {
            string sql = "select count(*) from hdb_site";
            DataTable tbl = this.Table("test", sql, true);
            return true;
        }

#if HDB_OPEN
        void MakeConnectionString(string username, string password)
        {
            //strAccessConn = "Provider="+provider+";User ID="+username+";"
            //    +"Password="+password+"; Data Source="+service+";";
            ConnectionString = "Data Source=(DESCRIPTION="
             + "(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST=" + m_host + ")(PORT=1521)))"
             + "(CONNECT_DATA=(SERVER=DEDICATED)(SERVICE_NAME=" + service + ")));"
             + "User Id=" + username + ";Password=" + password + ";";
        }

#else

        void MakeConnectionString(string username, string password)
        {
            //strAccessConn = "Data Source=(DESCRIPTION="
            // + "(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST=" + host + ")(PORT=1521)))"
            // + "(CONNECT_DATA=(SERVER=DEDICATED)(SERVICE_NAME=" + service + ")));"
            // + "User Id=" + username + ";Password=" + password + ";";

            OracleConnectionStringBuilder sb = new OracleConnectionStringBuilder();
            sb.Direct = true;
            sb.Server = m_host;
            sb.Port = 1521;
            if(!string.IsNullOrEmpty(m_port))
                {
                    try
                    {
                        sb.Port = Convert.ToInt32(m_port); 
                    }
                    catch (FormatException ex)
                    {
                        MessageBox.Show("You must enter a numeric value for the Port Number:  " + ex.Message, "HDB Poet");
                    }
                    
                }
            //sb.Sid = ;
            sb.ServiceName = service;
            sb.UserId = username;
            sb.Password = password;

            ConnectionString = sb.ConnectionString;
        }
#endif


        public override DataTable Table(string tableName)
        {
            return Table(tableName, "select * from " + tableName);
        }
        public override DataTable Table(string tableName, string sql)
        {
            return Table(tableName, sql, true);
        }
        public DataTable Table(string tableName, string sql, bool throwErrors)
        {
            string strAccessSelect = sql;
            OracleConnection myAccessConn = new OracleConnection(ConnectionString);
            OracleCommand myAccessCommand = new OracleCommand(strAccessSelect, myAccessConn);
            OracleDataAdapter myDataAdapter = new OracleDataAdapter(myAccessCommand);

            //Console.WriteLine(sql);
            this.lastSqlCommand = sql;
            this.sqlCommands.Add(sql);
            DataSet myDataSet = new DataSet();
            try
            {
                myAccessConn.Open();

                myDataAdapter.Fill(myDataSet, tableName);
            }
            catch (Exception e)
            {
                string msg = "Error reading from database \n" + sql + "\n Exception " + e.ToString();
                Console.WriteLine(msg);

                if (throwErrors)
                {
                    throw e;
                }
                
                System.Windows.Forms.MessageBox.Show(msg, "Error",
                  System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                //throw e; 
            }
            finally
            {
                myAccessConn.Close();
            }
            DataTable tbl = myDataSet.Tables[tableName];
            return tbl;
        }


        public override int RunSqlCommand(string sql)
        {
            return this.RunSqlCommand(sql, this.ConnectionString);
        }


        public int RunStoredProc(OracleCommand cmd)
        {
            int rval = 0;
            OracleConnection conn = new OracleConnection(this.ConnectionString);
            Debug.Assert(cmd.CommandType == CommandType.StoredProcedure);
            cmd.Connection = conn;

            rval = -1;
            try
            {
                conn.Open();
                rval = cmd.ExecuteNonQuery();
            }
            catch (Exception exc)
            {
                Console.WriteLine(exc.ToString());
                throw exc;
                //rval = -1;
            }

            conn.Close();
            

            string msg = cmd.CommandText + " ";
            for (int i = 0; i < cmd.Parameters.Count; i++)
            {
                msg += "," + cmd.Parameters[i].Value.ToString();
            
            }
            this.lastSqlCommand = msg;
            this.sqlCommands.Add(msg);
            return rval;
        }
        /// <summary>
        /// runs sql command.
        /// returns number of rows affected.
        /// </summary>
        /// <returns></returns>
        public int RunSqlCommand(string sql, string strAccessConn)
        {
            int rval = 0;
            this.lastMessage = "";
            OracleConnection myConnection = new OracleConnection(strAccessConn);
            myConnection.Open();
            OracleCommand myCommand = new OracleCommand();
            OracleTransaction myTrans;

            // Start a local transaction
            myTrans = myConnection.BeginTransaction(IsolationLevel.ReadCommitted);
            // Assign transaction object for a pending local transaction
            myCommand.Connection = myConnection;
            myCommand.Transaction = myTrans;

            try
            {
                myCommand.CommandText = sql;
                rval = myCommand.ExecuteNonQuery();
                myTrans.Commit();
                this.lastSqlCommand = sql;
                this.sqlCommands.Add(sql);
            }
            catch (Exception e)
            {
                myTrans.Rollback();
                Console.WriteLine(e.ToString());
                System.Windows.Forms.MessageBox.Show("Error running command :" + sql + " exception: " + e.ToString());
                Console.WriteLine("Error running " + sql);
                this.lastMessage = e.ToString();
                rval = -1;
                //throw e;
            }
            finally
            {
                myConnection.Close();
            }
            return rval;
        }

        public string[] SqlHistory
        {
            get
            {
                string[] rval = new String[sqlCommands.Count];
                this.sqlCommands.CopyTo(rval);
                return rval;
            }
            set
            {
                this.sqlCommands.Clear();
                for (int i = 0; i < value.Length; i++)
                {
                    this.sqlCommands.Add(value[i]);
                }
            }

        }
        public string LastSqlCommand
        {
            get { return this.lastSqlCommand; }
        }


        public static OracleServer ConnectToOracle(string hostname="")
        {
            DialogResult dr;
            bool loginSucess = false;
            OracleLogin login = new OracleLogin(hostname);
            OracleServer rval = null;
            do
            {
                dr = login.ShowDialog();
                if (dr == DialogResult.Cancel)
                    return null;


                rval = new OracleServer(login.ConnectionInfo);
                try
                {
                    loginSucess = rval.ConnectionWorking();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message+"\n"+ex.StackTrace+"\n"+ex.Source);
                    if( ex.InnerException != null)
                        MessageBox.Show(ex.InnerException.Message);
                }

            } while (!loginSucess);

            if (rval.LoginCanceled)
            {
                return null;
            }
            return rval;
        }

        //Saves DataTable in database
        public override int SaveTable(DataTable dataTable)
        {
            string sql = "select * from " + dataTable.TableName+" where 2 = 1";
            return SaveTable(dataTable, sql);

        }

        public override int SaveTable(DataTable dataTable, string sql)
        {
            Console.WriteLine("Saving " + dataTable.TableName);
            DataSet myDataSet = new DataSet();
            myDataSet.Tables.Add(dataTable.TableName);

            OracleConnection myAccessConn = new OracleConnection(ConnectionString);
            OracleCommand myAccessCommand = new OracleCommand(sql, myAccessConn);
            OracleDataAdapter myDataAdapter = new OracleDataAdapter(myAccessCommand);
            OracleCommandBuilder cb = new OracleCommandBuilder(myDataAdapter);
//            myDataAdapter.InsertCommand =  cb.GetInsertCommand();
            this.lastSqlCommand = sql;
            SqlCommands.Add(sql);

            myAccessConn.Open();
            int recordCount = 0;
            try
            {   // call Fill method only to make things work. (we ignore myDataSet)
                myDataAdapter.Fill(myDataSet, dataTable.TableName);
                recordCount = myDataAdapter.Update(dataTable);

            }
            finally
            {
                myAccessConn.Close();
            }
            return recordCount;
        }


    }
}
