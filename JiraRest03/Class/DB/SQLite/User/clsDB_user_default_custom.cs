using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lb
{
    class clsDB_user_default_custom
    {
        //Int32 d_cbocbo_id = lb.clsDB_user_default_custom.get_default_for_user_and_key(txtUID.Text, "frmTemplateCreator", "cboDBO");
        //lb.clsDB_user_default_custom.store_user_default(txtUID.Text,"frmTemplateCreator", "cboDBO", dbo_id);
        public static bool store_user_default(string p_uid, string p_application, string p_default_key, string p_value)
        {

            string m_method = "clsDB_user_default_custom.store_user_default";
            bool user_default_exist = false;
            bool user_default_saved = false;
            try
            {
                user_default_exist = lb.clsDB_user_default_custom.user_default_exist(p_uid, p_default_key);
                if (user_default_exist == false)
                {
                    lb.clsDB_user_default_gen.insert(p_uid, p_application, p_default_key, p_value);
                }
                if (user_default_exist == true)
                {
                    lb.clsDB_user_default_gen.update(p_uid, p_application, p_default_key, p_value);
                }
                user_default_saved = true;
                return user_default_saved;
            }
            catch (Exception ex)
            {
                lb.clsLogger.WriteLog("Error in : " + m_method + " : " + ex.Message);
                return user_default_saved;
            }
        }
        public static bool user_default_exist(string p_uid,string p_default_key)
        {
            string m_method = "clsDB_user_default_custom.user_default_exist";
            bool user_default_exist = false;
            try
            {
                clsSQLiteDB databaseObject = new clsSQLiteDB();
                string sql = "Select * from user_default";
                sql = sql + " where uid = '" + p_uid + "'";
                sql = sql + " and default_key = '" + p_default_key + "'";
                SQLiteCommand myCommand = new SQLiteCommand(sql, databaseObject.myConfigConnection);
                databaseObject.OpenConnection();
                SQLiteDataReader result = myCommand.ExecuteReader();
                if (result.HasRows)
                {
                    while (result.Read())
                    {
                        user_default_exist = true;
                    }
                }
                databaseObject.CloseConnection();
                return user_default_exist;
            }
            catch (Exception ex)
            {
                lb.clsLogger.WriteLog("Error in : " + m_method + " : " + ex.Message);
                return user_default_exist;
            }
        }
        public static string get_default_value_for_user_and_key(string p_uid, string p_application, string p_default_key)
        {
            string m_method = "clsDB_user_default_custom.get_default_value_for_user_and_key";
            string default_val = "";
            try
            {
                clsSQLiteDB databaseObject = new clsSQLiteDB();
                string sql = "Select * from user_default";
                sql = sql + " where uid = '" + p_uid + "'";
                sql = sql + " and application = '" + p_application + "'";
                sql = sql + " and default_key = '" + p_default_key + "'";
                SQLiteCommand myCommand = new SQLiteCommand(sql, databaseObject.myConfigConnection);
                databaseObject.OpenConnection();
                SQLiteDataReader result = myCommand.ExecuteReader();
                if (result.HasRows)
                {
                    while (result.Read())
                    {
                        default_val = result["value"].ToString();
                    }
                }
                databaseObject.CloseConnection();
                return default_val;
            }
            catch (Exception ex)
            {
                lb.clsLogger.WriteLog("Error in : " + m_method + " : " + ex.Message);
                return default_val;
            }
        }
    }
}
