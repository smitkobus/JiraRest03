using System;
using System.Data;
using System.Data.SQLite;
namespace lb
{
    class clsDB_interface_gen
    {
        public static Boolean insert(string p_dbo_id, string p_interface_type_id, string p_interface_key, string p_interface, string p_label, string p_responsible_uid)
        {
            string m_method = "clsDB_interface_gen.insert";
            try
            {
                clsSQLiteDB databaseObject = new clsSQLiteDB();
                string sql = "Insert into interface ('interface_id','dbo_id','interface_type_id','interface_key','interface','label','responsible_uid')";
                sql = sql + "values (null,'" + p_dbo_id + "','" + p_interface_type_id + "','" + p_interface_key + "','" + p_interface + "','" + p_label.ToUpper() + "','" + p_responsible_uid + "')";
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
        public static Boolean update(string p_interface_id, string p_dbo_id, string p_interface_type_id, string p_interface_key, string p_interface, string p_label, string p_responsible_uid)
        {
            string m_method = "clsDB_interface_gen.update";
            try
            {
                clsSQLiteDB databaseObject = new clsSQLiteDB();
                string sql = "Update interface set";
                sql = sql + " dbo_id = '" + p_dbo_id + "',";
                sql = sql + " interface_type_id = '" + p_interface_type_id + "',";
                sql = sql + " interface_key = '" + p_interface_key + "',";
                sql = sql + " interface = '" + p_interface + "',";
                sql = sql + " label = '" + p_label + "',";
                sql = sql + " responsible_uid = '" + p_responsible_uid + "'";
                sql = sql + " where interface_id = '" + p_interface_id + "'";
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
        public static DataTable get_dt_for_all_interface()
        {
            string m_method = "clsDB_interface_gen.get_dt_for_all_interface";
            DataTable dt = new DataTable();
            try
            {
                dt = get_dt_interface_columns(dt);
                clsSQLiteDB databaseObject = new clsSQLiteDB();
                string sql = "Select * from interface";
                SQLiteCommand myCommand = new SQLiteCommand(sql, databaseObject.myConfigConnection);
                databaseObject.OpenConnection();
                SQLiteDataReader result = myCommand.ExecuteReader();
                if (result.HasRows)
                {
                    while (result.Read())
                    {
                        DataRow dr = dt.NewRow();
                        dr["interface_key"] = result["interface_key"].ToString();
                        dr["interface"] = result["interface"].ToString();
                        dr["interface_id"] = result["interface_id"].ToString();
                        dr["dbo_id"] = result["dbo_id"].ToString();
                        dr["interface_type_id"] = result["interface_type_id"].ToString();
                        dr["label"] = result["label"].ToString();
                        dr["responsible_uid"] = result["responsible_uid"].ToString();
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
        public static DataTable get_dt_interface_columns(DataTable p_dt)
        {
            string m_method = "clsDB_interface_gen.get_dt_interface_columns";
            try
            {
                DataColumn col_sel = new DataColumn("col_sel");
                col_sel.DataType = System.Type.GetType("System.Boolean");
                p_dt.Columns.Add(col_sel);
                p_dt.Columns.Add("interface_id");
                p_dt.Columns.Add("dbo_id");
                p_dt.Columns.Add("interface_type_id");
                p_dt.Columns.Add("interface_key");
                p_dt.Columns.Add("interface_key_and_interface_name");
                p_dt.Columns.Add("interface");
                p_dt.Columns.Add("label");
                p_dt.Columns.Add("responsible_uid");
                p_dt.Columns.Add("responsible_name");
                p_dt.Columns.Add("pod");


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
