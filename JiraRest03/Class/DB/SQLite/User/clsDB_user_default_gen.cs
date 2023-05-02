using System;
using System.Data;
using System.Data.SQLite;
namespace lb
{
    class clsDB_user_default_gen
    {
        public static Boolean insert(string p_uid, string p_application, string p_default_key, string p_value)
        {
            string m_method = "clsDB_user_default_gen.insert";
            try
            {
                clsSQLiteDB databaseObject = new clsSQLiteDB();
                string sql = "Insert into user_default ('uid','application','default_key','value')";
                sql = sql + "values ('" + p_uid + "','" + p_application + "','" + p_default_key + "','" + p_value + "')";
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
        public static Boolean update(string p_uid, string p_application, string p_default_key, string p_value)
        {
            string m_method = "clsDB_user_default_gen.update";
            try
            {
                clsSQLiteDB databaseObject = new clsSQLiteDB();
                string sql = "Update user_default set";
                sql = sql + " application = '" + p_application + "',";
                sql = sql + " default_key = '" + p_default_key + "',";
                sql = sql + " value = '" + p_value + "'";
                sql = sql + " where uid = '" + p_uid + "'";
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
        public static DataTable get_dt_for_all_user_default()
        {
            string m_method = "clsDB_user_default_gen.get_dt_for_all_user_default";
            DataTable dt = new DataTable();
            try
            {
                dt = get_dt_user_default_columns(dt);
                clsSQLiteDB databaseObject = new clsSQLiteDB();
                string sql = "Select * from user_default";
                SQLiteCommand myCommand = new SQLiteCommand(sql, databaseObject.myConfigConnection);
                databaseObject.OpenConnection();
                SQLiteDataReader result = myCommand.ExecuteReader();
                if (result.HasRows)
                {
                    while (result.Read())
                    {
                        DataRow dr = dt.NewRow();
                        dr["uid"] = result["uid"].ToString();
                        dr["application"] = result["application"].ToString();
                        dr["default_key"] = result["default_key"].ToString();
                        dr["value"] = result["value"].ToString();
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
        public static DataTable get_dt_user_default_columns(DataTable p_dt)
        {
            string m_method = "clsDB_user_default_gen.get_dt_user_default_columns";
            try
            {
                p_dt.Columns.Add("uid");
                p_dt.Columns.Add("application");
                p_dt.Columns.Add("default_key");
                p_dt.Columns.Add("value");
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
