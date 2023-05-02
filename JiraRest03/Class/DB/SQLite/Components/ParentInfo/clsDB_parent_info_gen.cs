using System;
using System.Data;
using System.Data.SQLite;
namespace lb
{
    class clsDB_parent_info_gen
    {
        public static Int32 modify(string p_parent_key, string p_parent_type, string p_parent_desc)
        {
            string m_method = "clsDB_parent_info_gen.modify";
            Int32 rc = 0;
            try
            {
                bool record_exist = lb.clsDB_parent_info_gen.exist(p_parent_key);
                if(record_exist == true)
                {
                    update(p_parent_key, p_parent_type, p_parent_desc);
                }
                else
                {
                    
                    insert(p_parent_key, p_parent_type, p_parent_desc);
                }
                return rc;
            }
            catch (Exception ex)
            {
                lb.clsLogger.WriteLog("Error in : " + m_method + " : " + ex.Message);
                return rc;
            }
        }
        public static Boolean exist(string p_parent_key)
        {
            // bool record_exist = lb.clsDB_parent_info_gen.exist(p_parent_key);
            string m_method = "clsDB_parent_info_gen.exist";
            bool record_exist = false;
            try
            {
                clsSQLiteDB databaseObject = new clsSQLiteDB();
                string sql = "Select * from parent_info";
                sql = sql + " where parent_key = '" + p_parent_key + "'";
                SQLiteCommand myCommand = new SQLiteCommand(sql, databaseObject.myConfigConnection);
                databaseObject.OpenConnection();
                SQLiteDataReader result = myCommand.ExecuteReader();
                if (result.HasRows)
                {
                    record_exist = true;
                    //while (result.Read())
                    //{

                    //}
                }
                databaseObject.CloseConnection();
                return record_exist;
            }
            catch (Exception ex)
            {
                lb.clsLogger.WriteLog("Error in : " + m_method + " : " + ex.Message);
                return record_exist;
            }
        }
        public static Boolean insert(string p_parent_key, string p_parent_type, string p_parent_desc)
        {
            string m_method = "clsDB_parent_info_gen.insert";
            try
            {
                DateTime foo = DateTime.Now;
                long unixTime = ((DateTimeOffset)foo).ToUnixTimeSeconds();
                clsSQLiteDB databaseObject = new clsSQLiteDB();
                string sql = "Insert into parent_info ('parent_key','parent_type','parent_desc','last_updated')";
                sql = sql + "values ('" + p_parent_key + "','" + p_parent_type + "','" + p_parent_desc + "','" + unixTime + "')";
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
        public static Boolean update(string p_parent_key, string p_parent_type, string p_parent_desc)
        {
            string m_method = "clsDB_parent_info_gen.update";
            try
            {
                DateTime foo = DateTime.Now;
                long unixTime = ((DateTimeOffset)foo).ToUnixTimeSeconds();
                //
                clsSQLiteDB databaseObject = new clsSQLiteDB();
                string sql = "Update parent_info set";
                sql = sql + " parent_type = '" + p_parent_type + "',";
                sql = sql + " parent_desc = '" + p_parent_desc + "',";
                sql = sql + " last_updated = '" + unixTime + "'";
                sql = sql + " where parent_key = '" + p_parent_key + "'";
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
        public static DataTable get_dt_for_all_parent_info()
        {
            string m_method = "clsDB_parent_info_gen.get_dt_for_all_parent_info";
            DataTable dt = new DataTable();
            try
            {
                dt = get_dt_parent_info_columns(dt);
                clsSQLiteDB databaseObject = new clsSQLiteDB();
                string sql = "Select * from parent_info order by parent_desc";
                SQLiteCommand myCommand = new SQLiteCommand(sql, databaseObject.myConfigConnection);
                databaseObject.OpenConnection();
                SQLiteDataReader result = myCommand.ExecuteReader();
                if (result.HasRows)
                {
                    while (result.Read())
                    {
                        DataRow dr = dt.NewRow();
                        dr["parent_key"] = result["parent_key"].ToString();
                        dr["parent_type"] = result["parent_type"].ToString();
                        dr["parent_desc"] = result["parent_desc"].ToString();
                        dr["last_updated"] = result["last_updated"].ToString();
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
        public static DataTable get_dt_parent_info_columns(DataTable p_dt)
        {
            string m_method = "clsDB_parent_info_gen.get_dt_parent_info_columns";
            try
            {
                p_dt.Columns.Add("parent_key");
                p_dt.Columns.Add("parent_type");
                p_dt.Columns.Add("parent_desc");
                p_dt.Columns.Add("last_updated");
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
