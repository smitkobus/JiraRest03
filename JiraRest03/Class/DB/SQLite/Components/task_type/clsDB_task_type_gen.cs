using System;
using System.Data;
using System.Data.SQLite;
namespace lb
{
    class clsDB_task_type_gen
    {
        public static Boolean insert( string p_dbo_id, string p_task_type, string p_label, string p_active, string p_sort_order)
        {
            string m_method = "clsDB_task_type_gen.insert";
            try
            {
                clsSQLiteDB databaseObject = new clsSQLiteDB();
                string sql = "Insert into task_type ('task_type_id','dbo_id','task_type','label','active','sort_order')";
                sql = sql + "values (null,'" + p_dbo_id + "','" + p_task_type + "','" + p_label + "','" + p_active + "','" + p_sort_order + "')";
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
        public static Boolean update(string p_task_type_id, string p_dbo_id, string p_task_type, string p_label, string p_active, string p_sort_order)
        {
            string m_method = "clsDB_task_type_gen.update";
            try
            {
                clsSQLiteDB databaseObject = new clsSQLiteDB();
                string sql = "Update task_type set";
                sql = sql + " dbo_id = '" + p_dbo_id + "',";
                sql = sql + " task_type = '" + p_task_type + "',";
                sql = sql + " label = '" + p_label + "',";
                sql = sql + " active = '" + p_active + "',";
                sql = sql + " sort_order = '" + p_sort_order + "'";
                sql = sql + " where task_type_id = '" + p_task_type_id + "'";
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
        public static DataTable get_dt_for_all_task_type()
        {
            string m_method = "clsDB_task_type_gen.get_dt_for_all_task_type";
            DataTable dt = new DataTable();
            try
            {
                dt = get_dt_task_type_columns(dt);
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
                lb.clsLogger.WriteLog("Error in : " + m_method + " : " + ex.Message);
                return dt;
            }
        }
        public static DataTable get_dt_task_type_columns(DataTable p_dt)
        {
            string m_method = "clsDB_task_type_gen.get_dt_task_type_columns";
            try
            {
                //*
                //* Checked
                //*
                DataColumn colActive = new DataColumn("active");
                colActive.DataType = System.Type.GetType("System.Boolean");
                //*
                //* Rest
                //*                
                p_dt.Columns.Add("task_type_id");
                p_dt.Columns.Add("dbo_id");
                p_dt.Columns.Add("task_type");
                p_dt.Columns.Add("label");
                p_dt.Columns.Add("sort_order");
                p_dt.Columns.Add(colActive);
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
