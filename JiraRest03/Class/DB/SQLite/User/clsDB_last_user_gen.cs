using System;
using System.Data;
using System.Data.SQLite;
namespace lb
{
    class clsDB_last_user_gen
    {
        public static Boolean insert(string p_uid, string p_date_time)
        {
            string m_method = "clsDB_last_user_gen.insert";
            try
            {
                //clsSQLiteDB databaseObject = new clsSQLiteDB();
                //string sql = "Insert into last_user ('last_user_id','uid','date_time')";
                //sql = sql + "values (null,'" + p_uid + "','" + p_date_time + "')";
                //SQLiteCommand myCommand = new SQLiteCommand(sql, databaseObject.myConfigConnection);
                //databaseObject.OpenConnection();
                //myCommand.ExecuteNonQuery();
                //databaseObject.CloseConnection();
                return true;
            }
            catch (Exception ex)
            {
                lb.clsLogger.WriteLog("Error in : " + m_method + " : " + ex.Message);
                return false;
            }
        }
        public static Boolean update(string p_last_user_id, string p_uid, string p_date_time)
        {
            string m_method = "clsDB_last_user_gen.update";
            try
            {
                clsSQLiteDB databaseObject = new clsSQLiteDB();
                string sql = "Update last_user set";
                sql = sql + " uid = '" + p_uid + "',";
                sql = sql + " date_time = '" + p_date_time + "'";
                sql = sql + " where last_user_id = '" + p_last_user_id + "'";
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
        public static DataTable get_dt_for_all_last_user()
        {
            string m_method = "clsDB_last_user_gen.get_dt_for_all_last_user";
            DataTable dt = new DataTable();
            try
            {
                dt = get_dt_last_user_columns(dt);
                clsSQLiteDB databaseObject = new clsSQLiteDB();
                string sql = "Select * from last_user";
                SQLiteCommand myCommand = new SQLiteCommand(sql, databaseObject.myConfigConnection);
                databaseObject.OpenConnection();
                SQLiteDataReader result = myCommand.ExecuteReader();
                if (result.HasRows)
                {
                    while (result.Read())
                    {
                        DataRow dr = dt.NewRow();
                        dr["last_user_id"] = result["last_user_id"].ToString();
                        dr["uid"] = result["uid"].ToString();
                        dr["date_time"] = result["date_time"].ToString();
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
        public static DataTable get_dt_last_user_columns(DataTable p_dt)
        {
            string m_method = "clsDB_last_user_gen.get_dt_last_user_columns";
            try
            {
                p_dt.Columns.Add("last_user_id");
                p_dt.Columns.Add("uid");
                p_dt.Columns.Add("date_time");
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
