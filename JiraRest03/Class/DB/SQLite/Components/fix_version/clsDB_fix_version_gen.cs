using System;
using System.Data;
using System.Data.SQLite;
namespace lb
{
    class clsDB_fix_version_gen
    {
        public static Boolean insert(string p_fix_version_id, string p_dbo_id, string p_fix_version)
        {
            string m_method = "clsDB_fix_version_gen.insert";
            try
            {
                clsSQLiteDB databaseObject = new clsSQLiteDB();
                string sql = "Insert into fix_version ('fix_version_id','dbo_id','fix_version')";
                sql = sql + "values (null,'" + p_dbo_id + "','" + p_fix_version + "')";
                SQLiteCommand myCommand = new SQLiteCommand(sql, databaseObject.myConfigConnection);
                databaseObject.OpenConnection();
                myCommand.ExecuteNonQuery();
                databaseObject.CloseConnection();
                return true;
            }
            catch (Exception ex)
            {
                lb.clsLogger.WriteLog("Error in : " + m_method + " : " + ex.Message);
                return false;
            }
        }
        public static Boolean update(string p_fix_version_id, string p_dbo_id, string p_fix_version)
        {
            string m_method = "clsDB_fix_version_gen.update";
            try
            {
                clsSQLiteDB databaseObject = new clsSQLiteDB();
                string sql = "Update fix_version set";
                sql = sql + " dbo_id = '" + p_dbo_id + "',";
                sql = sql + " fix_version = '" + p_fix_version + "'";
                sql = sql + " where fix_version_id = '" + p_fix_version_id + "'";
                SQLiteCommand myCommand = new SQLiteCommand(sql, databaseObject.myConfigConnection);
                databaseObject.OpenConnection();
                myCommand.ExecuteNonQuery();
                databaseObject.CloseConnection();
                return true;
            }
            catch (Exception ex)
            {
                lb.clsLogger.WriteLog("Error in : " + m_method + " : " + ex.Message);
                return false;
            }
        }
        public static DataTable get_dt_for_all_fix_version()
        {
            string m_method = "clsDB_fix_version_gen.get_dt_for_all_fix_version";
            DataTable dt = new DataTable();
            try
            {
                dt = get_dt_fix_version_columns(dt);
                clsSQLiteDB databaseObject = new clsSQLiteDB();
                string sql = "Select * from fix_version";
                SQLiteCommand myCommand = new SQLiteCommand(sql, databaseObject.myConfigConnection);
                databaseObject.OpenConnection();
                SQLiteDataReader result = myCommand.ExecuteReader();
                if (result.HasRows)
                {
                    while (result.Read())
                    {
                        DataRow dr = dt.NewRow();
                        dr["fix_version_id"] = result["fix_version_id"].ToString();
                        dr["dbo_id"] = result["dbo_id"].ToString();
                        dr["fix_version"] = result["fix_version"].ToString();
                        dt.Rows.Add(dr);
                    }
                }
                databaseObject.CloseConnection();
                return dt;
            }
            catch (Exception ex)
            {
                lb.clsLogger.WriteLog("Error in : " + m_method + " : " + ex.Message);
                return dt;
            }
        }
        public static DataTable get_dt_fix_version_columns(DataTable p_dt)
        {
            string m_method = "clsDB_fix_version_gen.get_dt_fix_version_columns";
            try
            {
                p_dt.Columns.Add("fix_version_id");
                p_dt.Columns.Add("dbo_id");
                p_dt.Columns.Add("fix_version");
                return p_dt;
            }
            catch (Exception ex)
            {
                lb.clsLogger.WriteLog("Error in : " + m_method + " : " + ex.Message);
                return p_dt;
            }
        }
    }
}
