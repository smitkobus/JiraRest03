using System;
using System.Data;
using System.Data.SQLite;
namespace lb
{
    class clsDB_last_user_custom
    {
        public static string get_max_last_id_for_last_user()
        {
            string method = "get_max_last_id_for_last_user";
            DataTable dt = new DataTable();
            string max_id = "";
            try
            {
                
                clsSQLiteDB databaseObject = new clsSQLiteDB();
                string sql = "Select max(last_user_id) as max_id from last_user";
                SQLiteCommand myCommand = new SQLiteCommand(sql, databaseObject.myConfigConnection);
                databaseObject.OpenConnection();
                SQLiteDataReader result = myCommand.ExecuteReader();
                if (result.HasRows)
                {
                    while (result.Read())
                    {
                        max_id = result["max_id"].ToString();
                    }
                }
                databaseObject.CloseConnection();
                return max_id;
            }
            catch (Exception ex)
            {
                return max_id;
            }
        }
        // txtUID.Text = lb.clsDB_last_user_custom.log_uid();
        public static string log_uid()
        {
            string uid = "";
            try
            {
                string userName = System.Security.Principal.WindowsIdentity.GetCurrent().Name;
                uid = userName;
                string date_time = DateTime.Now.ToString("yyyy/MM/dd HH:mm");
                lb.clsDB_last_user_gen.insert(userName, date_time);
                uid = uid.Replace(@"W9\","");
                return uid;
            }
            catch (Exception ex)
            {
                return uid;
            }
        }
    }
}
