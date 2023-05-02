using System;
using System.Data;
using System.Data.SQLite;
namespace lb
{
    class clsDB_project_gen
    {
        public static Boolean insert(string p_dbo_id, string p_jira_project_key, string p_project_name, string p_label, string p_sort_order, string p_active)
        {
            string method = "insert_project";
            try
            {
                clsSQLiteDB databaseObject = new clsSQLiteDB();
                string sql = "Insert into project ('project_id','dbo_id','jira_project_key','project_name','label','sort_order','active')";
                sql = sql + "values (null,'" + p_dbo_id + "','" + p_jira_project_key + "','" + p_project_name + "','" + p_label + "','" + p_sort_order + "','" + p_active + "')";
                SQLiteCommand myCommand = new SQLiteCommand(sql, databaseObject.myConfigConnection);
                databaseObject.OpenConnection();
                myCommand.ExecuteNonQuery();
                databaseObject.CloseConnection();
                return true;
            }
            catch (Exception ex)
            {
                lb.clsLogger.WriteLog("Error: " + ex.Message);
                return false;
            }
        }
        public static Boolean update(string p_project_id, string p_dbo_id, string p_jira_project_key, string p_project_name, string p_label, string p_sort_order, string p_active)
        {
            string method = "update_project";
            try
            {
                clsSQLiteDB databaseObject = new clsSQLiteDB();
                string sql = "Update project set";
                sql = sql + " dbo_id = '" + p_dbo_id + "',";
                sql = sql + " jira_project_key = '" + p_jira_project_key + "',";
                sql = sql + " project_name = '" + p_project_name + "',";
                sql = sql + " label = '" + p_label + "',";
                sql = sql + " sort_order = '" + p_sort_order + "',";
                sql = sql + " active = '" + p_active + "'";
                sql = sql + " where project_id = '" + p_project_id + "'";
                SQLiteCommand myCommand = new SQLiteCommand(sql, databaseObject.myConfigConnection);
                databaseObject.OpenConnection();
                myCommand.ExecuteNonQuery();
                databaseObject.CloseConnection();
                return true;
            }
            catch (Exception ex)
            {
                lb.clsLogger.WriteLog("Error: " + ex.Message);
                return false;
            }
        }
        public static DataTable get_dt_for_all_project()
        {
            string method = "get_all_project";
            DataTable dt = new DataTable();
            try
            {
                dt = get_dt_project_columns(dt);
                clsSQLiteDB databaseObject = new clsSQLiteDB();
                string sql = "Select * from project";
                SQLiteCommand myCommand = new SQLiteCommand(sql, databaseObject.myConfigConnection);
                databaseObject.OpenConnection();
                SQLiteDataReader result = myCommand.ExecuteReader();
                if (result.HasRows)
                {
                    while (result.Read())
                    {
                        DataRow dr = dt.NewRow();
                        dr["project_id"] = result["project_id"].ToString();
                        dr["dbo_id"] = result["dbo_id"].ToString();
                        dr["jira_project_key"] = result["jira_project_key"].ToString();
                        dr["project_name"] = result["project_name"].ToString();
                        dr["label"] = result["label"].ToString();
                        dr["sort_order"] = result["sort_order"].ToString();
                        if (result["active"].ToString() == "0") { dr["active"] = false; }
                        if (result["active"].ToString() == "1") { dr["active"] = true; }
                        dt.Rows.Add(dr);
                    }
                }
                databaseObject.CloseConnection();
                return dt;
            }
            catch (Exception ex)
            {
                lb.clsLogger.WriteLog("Error: " + ex.Message);
                return dt;
            }
        }
        public static DataTable get_dt_project_columns(DataTable p_dt)
        {
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
                p_dt.Columns.Add("project_id");
                p_dt.Columns.Add("dbo_id");
                p_dt.Columns.Add("jira_project_key");
                p_dt.Columns.Add("project_name");
                p_dt.Columns.Add("label");
                p_dt.Columns.Add("sort_order");
                p_dt.Columns.Add(colActive);
                return p_dt;
            }
            catch (Exception ex)
            {
                lb.clsLogger.WriteLog("Error: " + ex.Message);
                return p_dt;
            }
        }
    }
}
