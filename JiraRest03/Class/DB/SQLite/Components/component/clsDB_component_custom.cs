using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lb
{
    class clsDB_component_custom
    {
        //public static DataTable get_dt_for_all_component_for_dbo_and_type(string p_dbo_id,string p_project_id,string p_task_type_id)
        public static DataTable get_dt_for_all_component_for_dbo_and_type(string p_dbo_id, string p_task_type_id)
        {
            string m_method = "clsDB_component_custom.get_dt_for_all_component_for_dbo_and_type";
            DataTable dt = new DataTable();
            bool process_sql = true;
            string m_project_id = "";
            try
            {
                if(p_task_type_id=="System.Data.DataRowView")
                {
                    process_sql = false;
                }
                if (process_sql == true)
                {
                    dt = lb.clsDB_component_gen.get_dt_component_columns(dt);
                    clsSQLiteDB databaseObject = new clsSQLiteDB();
                    string sql = "Select * from component";
                    sql = sql + " where dbo_id = '" + p_dbo_id + "'";
                    sql = sql + " and task_type_id = '" + p_task_type_id + "'";
                    sql = sql + " order by project_id,component";
                    SQLiteCommand myCommand = new SQLiteCommand(sql, databaseObject.myConfigConnection);
                    databaseObject.OpenConnection();
                    SQLiteDataReader result = myCommand.ExecuteReader();
                    if (result.HasRows)
                    {
                        while (result.Read())
                        {
                            DataRow dr = dt.NewRow();
                            dr["checked"] = true;
                            dr["component_id"] = result["component_id"].ToString();
                            dr["dbo_id"] = result["dbo_id"].ToString();
                            dr["project_id"] = result["project_id"].ToString();
                            m_project_id = dr["project_id"].ToString();
                            dr["project"] = lb.clsDB_project_custom.get_project_key_for_project_id(m_project_id);
                            dr["task_type_id"] = result["task_type_id"].ToString();
                            dr["component"] = result["component"].ToString();
                            dr["label"] = result["label"].ToString();
                            dt.Rows.Add(dr);
                        }
                    }
                    databaseObject.CloseConnection();

                }
                return dt;
            }
            catch (Exception ex)
            {
                lb.clsLogger.WriteLog("Error in : " + m_method + " : " + ex.Message);
                return dt;
            }
        }
        public static DataTable get_dt_for_all_component_for_dbo_and_type_and_project(string p_dbo_id, string p_task_type_id, string p_project_id)
        {
            string m_method = "clsDB_component_custom.get_dt_for_all_component_for_dbo_and_type_and_project";
            DataTable dt = new DataTable();
            try
            {
                dt = lb.clsDB_component_gen.get_dt_component_columns(dt);
                clsSQLiteDB databaseObject = new clsSQLiteDB();
                string sql = "Select * from component";
                sql = sql + " where dbo_id = '" + p_dbo_id + "'";
                sql = sql + " and task_type_id = '" + p_task_type_id + "'";
                sql = sql + " and project_id = '" + p_project_id + "'";
                SQLiteCommand myCommand = new SQLiteCommand(sql, databaseObject.myConfigConnection);
                databaseObject.OpenConnection();
                SQLiteDataReader result = myCommand.ExecuteReader();
                if (result.HasRows)
                {
                    while (result.Read())
                    {
                        DataRow dr = dt.NewRow();
                        dr["checked"] = true;
                        dr["component_id"] = result["component_id"].ToString();
                        dr["dbo_id"] = result["dbo_id"].ToString();
                        dr["project_id"] = result["project_id"].ToString();
                        dr["task_type_id"] = result["task_type_id"].ToString();
                        dr["component"] = result["component"].ToString();
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
    }
}
