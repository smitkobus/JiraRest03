using System;
using System.Data;
using System.Data.SQLite;
namespace lb
{
    class clsDB_dbo_gen
    {
        public static Boolean insert(string p_dbo, string p_dbo_jira_tag, string p_epic_project_id, string p_issue_project_id, string p_sort_order, string p_label)
        {
            string method = "insert_dbo";
            try
            {
                clsSQLiteDB databaseObject = new clsSQLiteDB();
                string sql = "Insert into dbo ('dbo_id','dbo','dbo_jira_tag','epic_project_id','issue_project_id','sort_order','label')";
                sql = sql + "values (null,'" + p_dbo + "','" + p_dbo_jira_tag + "','" + p_epic_project_id + "','" + p_issue_project_id + "','" + p_sort_order + "','" + p_label + "')";
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
        public static Boolean update(string p_dbo_id, string p_dbo, string p_dbo_jira_tag, string p_epic_project_id, string p_issue_project_id, string p_sort_order, string p_label)
        {
            string method = "update_dbo";
            try
            {
                clsSQLiteDB databaseObject = new clsSQLiteDB();
                string sql = "Update dbo set";
                sql = sql + " dbo = '" + p_dbo + "',";
                sql = sql + " dbo_jira_tag = '" + p_dbo_jira_tag + "',";
                sql = sql + " epic_project_id = '" + p_epic_project_id + "',";
                sql = sql + " issue_project_id = '" + p_issue_project_id + "',";
                sql = sql + " sort_order = '" + p_sort_order + "',";
                sql = sql + " label = '" + p_label + "'";
                sql = sql + " where dbo_id = '" + p_dbo_id + "'";
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
        public static DataTable get_dt_for_all_dbo()
        {
            string method = "get_all_dbo";
            DataTable dt = new DataTable();
            try
            {
                dt = get_dt_dbo_columns(dt);
                clsSQLiteDB databaseObject = new clsSQLiteDB();
                string sql = "Select * from dbo";
                SQLiteCommand myCommand = new SQLiteCommand(sql, databaseObject.myConfigConnection);
                databaseObject.OpenConnection();
                SQLiteDataReader result = myCommand.ExecuteReader();
                if (result.HasRows)
                {
                    while (result.Read())
                    {
                        DataRow dr = dt.NewRow();
                        dr["dbo_id"] = result["dbo_id"].ToString();
                        dr["dbo"] = result["dbo"].ToString();
                        dr["dbo_jira_tag"] = result["dbo_jira_tag"].ToString();
                        dr["epic_project_id"] = result["epic_project_id"].ToString();
                        dr["issue_project_id"] = result["issue_project_id"].ToString();
                        dr["sort_order"] = result["sort_order"].ToString();
                        dr["label"] = result["label"].ToString();
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
        public static DataTable get_dt_dbo_columns(DataTable p_dt)
        {
            try
            {
                p_dt.Columns.Add("dbo_id");
                p_dt.Columns.Add("dbo");
                p_dt.Columns.Add("dbo_jira_tag");
                p_dt.Columns.Add("epic_project_id");
                p_dt.Columns.Add("issue_project_id");
                p_dt.Columns.Add("sort_order");
                p_dt.Columns.Add("label");
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
