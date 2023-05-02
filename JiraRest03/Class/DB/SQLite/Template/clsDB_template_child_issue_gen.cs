using System;
using System.Data;
using System.Data.SQLite;
namespace lb
{
    class clsDB_template_child_issue_gen
    {
        public static Boolean insert(string p_template_hdr_id, string p_issue_type_id, string p_summary, string p_description, string p_reporter, string p_assignee, string p_fix_version, string p_epic_link, string p_story_points, string p_label)
        {
            string m_method = "clsDB_template_child_issue_gen.insert";
            try
            {
                clsSQLiteDB databaseObject = new clsSQLiteDB();
                string sql = "Insert into template_child_issue ('template_child_issue_id','template_hdr_id','issue_type_id','summary','description','reporter','assignee','fix_version','epic_link','story_points','label')";
                sql = sql + "values (null,'" + p_template_hdr_id + "','" + p_issue_type_id + "','" + p_summary + "','" + p_description + "','" + p_reporter + "','" + p_assignee + "','" + p_fix_version + "','" + p_epic_link + "','" + p_story_points + "','" + p_label + "')";
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
        public static Boolean update(string p_template_child_issue_id, string p_template_hdr_id, string p_issue_type_id, string p_summary, string p_description, string p_reporter, string p_assignee, string p_fix_version, string p_epic_link, string p_story_points, string p_label)
        {
            string m_method = "clsDB_template_child_issue_gen.update";
            try
            {
                clsSQLiteDB databaseObject = new clsSQLiteDB();
                string sql = "Update template_child_issue set";
                sql = sql + " template_hdr_id = '" + p_template_hdr_id + "',";
                sql = sql + " issue_type_id = '" + p_issue_type_id + "',";
                sql = sql + " summary = '" + p_summary + "',";
                sql = sql + " description = '" + p_description + "',";
                sql = sql + " reporter = '" + p_reporter + "',";
                sql = sql + " assignee = '" + p_assignee + "',";
                sql = sql + " fix_version = '" + p_fix_version + "',";
                sql = sql + " epic_link = '" + p_epic_link + "',";
                sql = sql + " story_points = '" + p_story_points + "',";
                sql = sql + " label = '" + p_label + "'";
                sql = sql + " where template_child_issue_id = '" + p_template_child_issue_id + "'";
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
        public static DataTable get_dt_for_all_template_child_issue()
        {
            string m_method = "clsDB_template_child_issue_gen.get_dt_for_all_template_child_issue";
            DataTable dt = new DataTable();
            try
            {
                dt = get_dt_template_child_issue_columns(dt);
                clsSQLiteDB databaseObject = new clsSQLiteDB();
                string sql = "Select * from template_child_issue";
                SQLiteCommand myCommand = new SQLiteCommand(sql, databaseObject.myConfigConnection);
                databaseObject.OpenConnection();
                SQLiteDataReader result = myCommand.ExecuteReader();
                if (result.HasRows)
                {
                    while (result.Read())
                    {
                        DataRow dr = dt.NewRow();
                        dr["template_child_issue_id"] = result["template_child_issue_id"].ToString();
                        dr["template_hdr_id"] = result["template_hdr_id"].ToString();
                        dr["issue_type_id"] = result["issue_type_id"].ToString();
                        dr["issue_type"] = lb.clsDB_cbo_det_custom.get_value_01_for_cbo_det(dr["issue_type_id"].ToString());
                        dr["summary"] = result["summary"].ToString();
                      //  dr["soundex"] = lb.clsUtils.Soundex(result["summary"].ToString());
                        dr["description"] = result["description"].ToString();
                        dr["reporter"] = result["reporter"].ToString();
                        dr["assignee"] = result["assignee"].ToString();
                        dr["fix_version"] = result["fix_version"].ToString();
                        dr["epic_link"] = result["epic_link"].ToString();
                        dr["story_points"] = result["story_points"].ToString();
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
        public static DataTable get_dt_template_child_issue_columns(DataTable p_dt)
        {
            string m_method = "clsDB_template_child_issue_gen.get_dt_template_child_issue_columns";
            try
            {
                p_dt.Columns.Add("template_child_issue_id");
                p_dt.Columns.Add("template_hdr_id");
                p_dt.Columns.Add("issue_type_id");
                p_dt.Columns.Add("issue_type");
                p_dt.Columns.Add("label");
                p_dt.Columns.Add("summary");
                p_dt.Columns.Add("description");
                p_dt.Columns.Add("fix_version");
                p_dt.Columns.Add("story_points");
                p_dt.Columns.Add("epic_link");
                p_dt.Columns.Add("reporter_id");
                p_dt.Columns.Add("reporter");
                p_dt.Columns.Add("assignee_id");
                p_dt.Columns.Add("assignee");
                p_dt.Columns.Add("soundex");
                p_dt.Columns.Add("soundex_passedee");
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
