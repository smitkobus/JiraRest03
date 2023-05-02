using System;
using System.Data;
using System.Data.SQLite;
namespace lb
{
    class clsDB_template_hdr_gen
    {
        //public static Boolean insert(string p_dbo_id, string p_project_id, string p_type_id, string p_issue_type_id, string p_template, string p_summary, string p_description, string p_auth_type, string p_auth_value, string p_parent_issue_type, string p_label)
        public static Boolean insert(string p_dbo_id, string p_type_id, string p_issue_type_id, string p_template, string p_summary, string p_description, string p_auth_type, string p_auth_value, string p_parent_issue_type, string p_label)
        {
            string method = "insert_template_hdr";
            try
            {
                clsSQLiteDB databaseObject = new clsSQLiteDB();
                //string sql = "Insert into template_hdr ('template_hdr_id','dbo_id','project_id','type_id','issue_type_id','template','summary','description','auth_type','auth_value','parent_issue_type','label')";
                //sql = sql + "values (null,'" + p_dbo_id + "','" + p_project_id + "','" + p_type_id + "','" + p_issue_type_id + "','" + p_template + "','" + p_summary + "','" + p_description + "','" + p_auth_type + "','" + p_auth_value + "','" + p_parent_issue_type + "','" + p_label + "')";
                string sql = "Insert into template_hdr ('template_hdr_id','dbo_id','type_id','issue_type_id','template','summary','description','auth_type','auth_value','parent_issue_type','label')";
                sql = sql + "values (null,'" + p_dbo_id + "','" + p_type_id + "','" + p_issue_type_id + "','" + p_template + "','" + p_summary + "','" + p_description + "','" + p_auth_type + "','" + p_auth_value + "','" + p_parent_issue_type + "','" + p_label + "')";
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
        //public static Boolean update(string p_template_hdr_id, string p_dbo_id, string p_project_id, string p_type_id, string p_issue_type_id, string p_template, string p_summary, string p_description, string p_auth_type, string p_auth_value, string p_parent_issue_type, string p_label)
        public static Boolean update(string p_template_hdr_id, string p_dbo_id, string p_type_id, string p_issue_type_id, string p_template, string p_summary, string p_description, string p_auth_type, string p_auth_value, string p_parent_issue_type, string p_label)
        {
            string method = "update_template_hdr";
            try
            {
                clsSQLiteDB databaseObject = new clsSQLiteDB();
                string sql = "Update template_hdr set";
                sql = sql + " dbo_id = '" + p_dbo_id + "',";
                //sql = sql + " project_id = '" + p_project_id + "',";
                sql = sql + " type_id = '" + p_type_id + "',";
                sql = sql + " issue_type_id = '" + p_issue_type_id + "',";
                sql = sql + " template = '" + p_template + "',";
                sql = sql + " summary = '" + p_summary + "',";
                sql = sql + " description = '" + p_description + "',";
                sql = sql + " auth_type = '" + p_auth_type + "',";
                sql = sql + " auth_value = '" + p_auth_value + "',";
                sql = sql + " parent_issue_type = '" + p_parent_issue_type + "',";
                sql = sql + " label = '" + p_label + "'";
                sql = sql + " where template_hdr_id = '" + p_template_hdr_id + "'";
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
        public static DataTable get_dt_for_all_template_hdr()
        {
            string method = "get_all_template_hdr";
            DataTable dt = new DataTable();
            try
            {
                dt = get_dt_template_hdr_columns(dt);
                clsSQLiteDB databaseObject = new clsSQLiteDB();
                string sql = "Select * from template_hdr";
                SQLiteCommand myCommand = new SQLiteCommand(sql, databaseObject.myConfigConnection);
                databaseObject.OpenConnection();
                SQLiteDataReader result = myCommand.ExecuteReader();
                if (result.HasRows)
                {
                    while (result.Read())
                    {
                        DataRow dr = dt.NewRow();
                        dr["template_hdr_id"] = result["template_hdr_id"].ToString();
                        dr["dbo_id"] = result["dbo_id"].ToString();
                        //dr["project_id"] = result["project_id"].ToString();
                        dr["type_id"] = result["type_id"].ToString();
                        dr["issue_type_id"] = result["issue_type_id"].ToString();
                        dr["template"] = result["template"].ToString();
                        dr["summary"] = result["summary"].ToString();
                        dr["description"] = result["description"].ToString();
                        dr["auth_type"] = result["auth_type"].ToString();
                        dr["auth_value"] = result["auth_value"].ToString();
                        dr["parent_issue_type"] = result["parent_issue_type"].ToString();
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
        public static DataTable get_dt_template_hdr_columns(DataTable p_dt)
        {
            try
            {
                p_dt.Columns.Add("btn01");
                p_dt.Columns.Add("template_hdr_id");
                p_dt.Columns.Add("dbo_id");
                //p_dt.Columns.Add("project_id");
                p_dt.Columns.Add("type_id");
                p_dt.Columns.Add("type");
                p_dt.Columns.Add("issue_type_id");
                p_dt.Columns.Add("issue_type");
                p_dt.Columns.Add("template");
                p_dt.Columns.Add("summary");
                p_dt.Columns.Add("description");
                p_dt.Columns.Add("auth_type");
                p_dt.Columns.Add("auth_value");
                p_dt.Columns.Add("parent_issue_type");
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
