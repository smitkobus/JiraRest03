using System;
using System.Data;
using System.Data.SQLite;
namespace lb
{
    class clsDB_task_type_custom
    {
        public static DataTable get_dt_for_all_active_task_types_per_dbo(string p_dbo_id)
        {
            string m_method = "clsDB_task_type_custom.get_dt_for_all_active_task_types_per_dbo";
            DataTable dt = new DataTable();
            string p_dbo = "";
            Int32 i_dbo_id = 0;
            string p_interface_type = "";
            try
            {
                i_dbo_id = Convert.ToInt32(p_dbo_id);
                p_dbo = lb.clsDB_dbo_custom.get_dbo_value_from_id(Convert.ToInt32(p_dbo_id));
                p_interface_type = "Ingest";
                dt = lb.clsDB_task_type_gen.get_dt_task_type_columns(dt);
                clsSQLiteDB databaseObject = new clsSQLiteDB();
                string sql = "Select * from task_type";
                sql = sql + " where dbo_id = '" + p_dbo_id + "'";
                sql = sql + " and active = '1'";
                sql = sql + " order by sort_order";

                SQLiteCommand myCommand = new SQLiteCommand(sql, databaseObject.myConfigConnection);
                databaseObject.OpenConnection();
                SQLiteDataReader result = myCommand.ExecuteReader();
                if (result.HasRows)
                {
                    while (result.Read())
                    {
                        DataRow dr = dt.NewRow();
                        dr["task_type_id"] = result["task_type_id"].ToString();
                        dr["dbo_id"] = result["dbo_id"].ToString();
                        dr["task_type"] = result["task_type"].ToString();
                        dr["label"] = result["label"].ToString();
                        if (result["active"].ToString() == "0") { dr["active"] = false; }
                        if (result["active"].ToString() == "1") { dr["active"] = true; }
                        dr["sort_order"] = result["sort_order"].ToString();
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
        public static DataTable get_dt_for_all_task_type()
        {
            string method = "get_all_task_type";
            DataTable dt = new DataTable();
            try
            {
                dt = lb.clsDB_task_type_gen.get_dt_task_type_columns(dt);
                clsSQLiteDB databaseObject = new clsSQLiteDB();
                string sql = "Select * from task_type";
                SQLiteCommand myCommand = new SQLiteCommand(sql, databaseObject.myConfigConnection);
                databaseObject.OpenConnection();
                SQLiteDataReader result = myCommand.ExecuteReader();
                if (result.HasRows)
                {
                    while (result.Read())
                    {
                        DataRow dr = dt.NewRow();
                        dr["task_type_id"] = result["task_type_id"].ToString();
                        dr["dbo_id"] = result["dbo_id"].ToString();
                        dr["task_type"] = result["task_type"].ToString();
                        dr["label"] = result["label"].ToString();
                        dr["active"] = result["active"].ToString();
                        dr["sort_order"] = result["sort_order"].ToString();
                        dt.Rows.Add(dr);
                    }
                }
                databaseObject.CloseConnection();
                return dt;
            }
            catch (Exception ex)
            {
                return dt;
            }
        }
    
    }
}
