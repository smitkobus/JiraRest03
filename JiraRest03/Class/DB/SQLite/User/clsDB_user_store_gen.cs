using System;
using System.Data;
using System.Data.SQLite;
namespace lb
{
    class clsDB_user_store_gen
    {
        public static Boolean insert(string p_uid, string p_displayName, string p_emailAddress, string p_timeZone)
        {
            string m_method = "clsDB_user_store_gen.insert";
            try
            {
                clsSQLiteDB databaseObject = new clsSQLiteDB();
                string sql = "Insert into user_store ('uid','displayName','emailAddress','timeZone')";
                sql = sql + "values ('"+ p_uid + "','" + p_displayName + "','" + p_emailAddress + "','" + p_timeZone + "')";
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
        public static Boolean update(string p_uid, string p_displayName, string p_emailAddress, string p_timeZone)
        {
            string m_method = "clsDB_user_store_gen.update";
            try
            {
                clsSQLiteDB databaseObject = new clsSQLiteDB();
                string sql = "Update user_store set";
                sql = sql + " displayName = '" + p_displayName + "',";
                sql = sql + " emailAddress = '" + p_emailAddress + "',";
                sql = sql + " timeZone = '" + p_timeZone + "'";
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
        public static DataTable get_dt_for_all_user_store()
        {
            string m_method = "clsDB_user_store_gen.get_dt_for_all_user_store";
            DataTable dt = new DataTable();
            try
            {
                dt = get_dt_user_store_columns(dt);
                clsSQLiteDB databaseObject = new clsSQLiteDB();
                string sql = "Select * from user_store";
                sql = sql + " order by displayName";
                SQLiteCommand myCommand = new SQLiteCommand(sql, databaseObject.myConfigConnection);
                databaseObject.OpenConnection();
                SQLiteDataReader result = myCommand.ExecuteReader();
                //*
                //* Select a UID
                //*
                DataRow dr = dt.NewRow();
                dr["uid"] = "Q123456";
                dr["displayName"] = "Please select a UID";
                dr["emailAddress"] = "kobusdotsmit@gmail.com";
                dr["timeZone"] = "";
                dt.Rows.Add(dr);
                //*
                //* Select UID from Interface
                //*
                dr = dt.NewRow();
                dr["uid"] = "Q999999";
                dr["displayName"] = "Select a UID from Interface";
                dr["emailAddress"] = "kobusdotsmit@gmail.com";
                dr["timeZone"] = "";
                dt.Rows.Add(dr);

                if (result.HasRows)
                {
                    while (result.Read())
                    {
                        dr = dt.NewRow();
                        dr["uid"] = result["uid"].ToString();
                        dr["displayName"] = result["displayName"].ToString();
                        dr["emailAddress"] = result["emailAddress"].ToString();
                        dr["timeZone"] = result["timeZone"].ToString();
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
        public static DataTable get_dt_user_store_columns(DataTable p_dt)
        {
            string m_method = "clsDB_user_store_gen.get_dt_user_store_columns";
            try
            {
                p_dt.Columns.Add("uid");
                p_dt.Columns.Add("displayName");
                p_dt.Columns.Add("emailAddress");
                p_dt.Columns.Add("timeZone");
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
