using System;
using System.Data;
using System.Data.SQLite;
namespace lb
{
    class clsDB_component_gen
    {
        public static Boolean insert(string p_dbo_id, string p_project_id, string p_task_type_id, string p_component, string p_label)
        {
            string m_method = "clsDB_component_gen.insert";
            try
            {
                clsSQLiteDB databaseObject = new clsSQLiteDB();
                string sql = "Insert into component ('component_id','dbo_id','project_id','task_type_id','component','label')";
                sql = sql + "values (null,'" + p_dbo_id + "','" + p_project_id + "','" + p_task_type_id + "','" + p_component + "','" + p_label + "')";
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
        public static Boolean update(string p_component_id, string p_dbo_id, string p_project_id, string p_task_type_id, string p_component, string p_label)
        {
            string m_method = "clsDB_component_gen.update";
            try
            {
                clsSQLiteDB databaseObject = new clsSQLiteDB();
                string sql = "Update component set";
                sql = sql + " dbo_id = '" + p_dbo_id + "',";
                sql = sql + " project_id = '" + p_project_id + "',";
                sql = sql + " task_type_id = '" + p_task_type_id + "',";
                sql = sql + " component = '" + p_component + "',";
                sql = sql + " label = '" + p_label + "'";
                sql = sql + " where component_id = '" + p_component_id + "'";

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
        public static DataTable get_dt_for_all_component()
        {
            string m_method = "clsDB_component_gen.get_dt_for_all_component";
            DataTable dt = new DataTable();
            string m_project_id = "";
            try
            {
                dt = get_dt_component_columns(dt);
                clsSQLiteDB databaseObject = new clsSQLiteDB();
                string sql = "Select * from component";
                SQLiteCommand myCommand = new SQLiteCommand(sql, databaseObject.myConfigConnection);
                databaseObject.OpenConnection();
                SQLiteDataReader result = myCommand.ExecuteReader();
                if (result.HasRows)
                {
                    while (result.Read())
                    {
                        DataRow dr = dt.NewRow();
                        dr["component_id"] = result["component_id"].ToString();
                        dr["dbo_id"] = result["dbo_id"].ToString();
                        dr["project_id"] = result["project_id"].ToString();
                        m_project_id = dr["project_id"].ToString();
                        dr["project"] = lb.clsDB_project_custom.get_project_key_for_project_id(m_project_id);
                        dr["cbo_det_id"] = result["cbo_det_id"].ToString();
                        dr["component"] = result["component"].ToString();
                        dr["label"] = result["label"].ToString();
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
        public static DataTable get_dt_component_columns(DataTable p_dt)
        {
            string m_method = "clsDB_component_gen.get_dt_component_columns";
            try
            {
                //*
                //* Checked
                //*
                DataColumn colChecked = new DataColumn("checked");
                colChecked.DataType = System.Type.GetType("System.Boolean");
                p_dt.Columns.Add(colChecked);
                //*
                //* Rest
                //*
                p_dt.Columns.Add("component_id");
                p_dt.Columns.Add("dbo_id");
                p_dt.Columns.Add("project_id");
                p_dt.Columns.Add("project");
                p_dt.Columns.Add("task_type_id");
                p_dt.Columns.Add("component");
                p_dt.Columns.Add("label");
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
