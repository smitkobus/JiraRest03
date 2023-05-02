using System;
using System.Data;
using System.Data.SQLite;
namespace lb
{
    class clsDB_user_store_custom
    {
        public static string get_displayname_from_uid(string p_uid)
        {
            string method = "clsDB_user_store_custom.get_displayname_from_uid";
            string m_display_name = "";
            try
            {
                clsSQLiteDB databaseObject = new clsSQLiteDB();
                string sql = "Select * from user_store";
                sql = sql + " where uid = '" + p_uid + "'";
                SQLiteCommand myCommand = new SQLiteCommand(sql, databaseObject.myConfigConnection);
                databaseObject.OpenConnection();
                SQLiteDataReader result = myCommand.ExecuteReader();
                if (result.HasRows)
                {
                    while (result.Read())
                    {
                        m_display_name = result["displayName"].ToString();
                    }
                }
                databaseObject.CloseConnection();
                return m_display_name;
            }
            catch (Exception ex)
            {
                return m_display_name;
            }
        }
        public static string get_pod_from_uid(string p_uid)
        {
            string method = "clsDB_user_store_custom.get_pod_from_uid";
            string m_display_name = "";
            try
            {
                clsSQLiteDB databaseObject = new clsSQLiteDB();
                string sql = "Select * from user_store";
                sql = sql + " where uid = '" + p_uid + "'";
                SQLiteCommand myCommand = new SQLiteCommand(sql, databaseObject.myConfigConnection);
                databaseObject.OpenConnection();
                SQLiteDataReader result = myCommand.ExecuteReader();
                if (result.HasRows)
                {
                    while (result.Read())
                    {
                        m_display_name = result["pod"].ToString();
                    }
                }
                databaseObject.CloseConnection();
                return m_display_name;
            }
            catch (Exception ex)
            {
                return m_display_name;
            }
        }
        public static string get_short_from_uid(string p_uid)
        {
            string method = "clsDB_user_store_custom.get_short_from_uid";
            string m_display_name = "";
            try
            {
                clsSQLiteDB databaseObject = new clsSQLiteDB();
                string sql = "Select * from user_store";
                sql = sql + " where uid = '" + p_uid + "'";
                SQLiteCommand myCommand = new SQLiteCommand(sql, databaseObject.myConfigConnection);
                databaseObject.OpenConnection();
                SQLiteDataReader result = myCommand.ExecuteReader();
                if (result.HasRows)
                {
                    while (result.Read())
                    {
                        m_display_name = result["short"].ToString();
                    }
                }
                databaseObject.CloseConnection();
                return m_display_name;
            }
            catch (Exception ex)
            {
                return m_display_name;
            }
        }
        public static bool store_user(string p_uid,string p_displayName, string p_emailAddress, string p_timeZone)
        {

            string method = "store_user";
            bool uid_exist = false;
            try
            {
                uid_exist = lb.clsDB_user_store_custom.uid_exist(p_uid);
                if(uid_exist==false)
                {
                    lb.clsDB_user_store_gen.insert(p_uid, p_displayName, p_emailAddress, p_timeZone);
                }

                return uid_exist;
            }
            catch (Exception ex)
            {
                return uid_exist;
            }
        }
        public static bool uid_exist(string p_uid)
        {
            string method = "uid_exist";
            bool uid_exist = false;
            try
            {
                clsSQLiteDB databaseObject = new clsSQLiteDB();
                string sql = "Select * from user_store";
                sql = sql + " where uid = '" + p_uid + "'";
                SQLiteCommand myCommand = new SQLiteCommand(sql, databaseObject.myConfigConnection);
                databaseObject.OpenConnection();
                SQLiteDataReader result = myCommand.ExecuteReader();
                if (result.HasRows)
                {
                    while (result.Read())
                    {
                        uid_exist = true;
                    }
                }
                databaseObject.CloseConnection();
                return uid_exist;
            }
            catch (Exception ex)
            {
                return uid_exist;
            }
        }
    }
}
