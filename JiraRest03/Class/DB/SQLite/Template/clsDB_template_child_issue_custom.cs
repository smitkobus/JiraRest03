using System;
using System.Data;
using System.Data.SQLite;
namespace lb
{
    class clsDB_template_child_issue_custom
    {
        public static string get_child_id_4_hdr_id_and_summary(string p_template_hdr_id, string p_summary)
        {
            string m_method = "clsDB_template_child_issue_custom.get_child_id_4_hdr_id_and_summary";
            string template_child_issue_id = "";
            try
            {
                //dt = lb.clsDB_template_hdr_gen.get_dt_template_hdr_columns(dt);
                clsSQLiteDB databaseObject = new clsSQLiteDB();
                string sql = "Select * from template_child_issue";
                sql = sql + " Where template_hdr_id = '" + p_template_hdr_id + "'";
                sql = sql + " and summary = '" + p_summary + "'";
                SQLiteCommand myCommand = new SQLiteCommand(sql, databaseObject.myConfigConnection);
                databaseObject.OpenConnection();
                SQLiteDataReader result = myCommand.ExecuteReader();
                if (result.HasRows)
                {
                    while (result.Read())
                    {
                        template_child_issue_id = result["template_child_issue_id"].ToString();
                    }
                }
                databaseObject.CloseConnection();
                return template_child_issue_id;
            }
            catch (Exception ex)
            {
                lb.clsLogger.WriteLog("Error in : " + m_method + " : " + ex.Message);
                return template_child_issue_id;
            }
        }
        public static bool copy_child_template(string p_from_template_hdr_id, string p_to_template_hdr_id)
        {
            string m_method = "clsDB_template_child_issue_custom.copy_child_template";
            DataTable dt_child_template = new DataTable();
            bool child_templeate_copied = false;
            
            string m_from_template_child_issue_id = "";
            string m_to_template_child_issue_id = "";
            string m_template_hdr_id = "";
            string m_issue_type_id = "";
            string m_summary = "";
            string m_description = "";
            string m_reporter_id = "";
            string m_assignee_id = "";
            string m_fix_version = "";
            string m_epic_link = "";
            string m_story_points = "";
            string m_label = "";

            try
            {
                dt_child_template = lb.clsDB_template_child_issue_custom.get_dt_for_all_template_child_issue_4_one_hrd(p_from_template_hdr_id);
                foreach (DataRow row in dt_child_template.Rows)
                {
                    m_from_template_child_issue_id = row["template_child_issue_id"].ToString();
                    m_template_hdr_id = p_to_template_hdr_id;
                    m_issue_type_id = row["issue_type_id"].ToString();
                    m_summary = row["summary"].ToString();
                    m_description = row["description"].ToString();
                    m_reporter_id = row["reporter_id"].ToString();
                    m_assignee_id = row["assignee_id"].ToString();
                    m_fix_version = row["fix_version"].ToString();
                    m_epic_link = row["epic_link"].ToString();
                    m_story_points = row["story_points"].ToString();
                    m_label = row["label"].ToString();
                    //*
                    //* Insert Child Template
                    //*
                    lb.clsDB_template_child_issue_gen.insert(m_template_hdr_id, m_issue_type_id,m_summary,
                    m_description, m_reporter_id, m_assignee_id, m_fix_version, m_epic_link, m_story_points, m_label);
                    m_to_template_child_issue_id = lb.clsDB_template_child_issue_custom.get_child_id_4_hdr_id_and_summary(m_template_hdr_id, m_summary);
                    //*
                    //* Copy Child Acceptance
                    //*
                    lb.clsDB_template_child_acceptance_custom.copy_child_acceptance(m_from_template_child_issue_id, m_to_template_child_issue_id);
                }
                child_templeate_copied = true;
                //Insert Template HDR
                return child_templeate_copied;
            }
            catch (Exception ex)
            {
                lb.clsLogger.WriteLog("Error in : " + m_method + " : " + ex.Message);
                return child_templeate_copied;
            }
        }
        //string description = lb.clsDB_template_child_issue_custom.get_description(m_template_child_issue_id);
        public static string get_description(string p_template_child_issue_id)
        {
            string m_method = "clsDB_template_child_issue_custom.get_description";
            string description = "";
            DataTable p_dt;
            try
            {
                clsSQLiteDB databaseObject = new clsSQLiteDB();
                string sql = "Select issue_type_id from template_child_issue";
                sql = sql + " where template_child_issue_id = '" + p_template_child_issue_id + "'";

                SQLiteCommand myCommand = new SQLiteCommand(sql, databaseObject.myConfigConnection);
                databaseObject.OpenConnection();
                SQLiteDataReader result = myCommand.ExecuteReader();
                if (result.HasRows)
                {
                    while (result.Read())
                    {
                        description = (result["description"]).ToString();
                    }
                }
                return description;
            }
            catch (Exception ex)
            {
                lb.clsLogger.WriteLog("Error in : " + m_method + " : " + ex.Message);
                return description;
            }
        }
        public static DataTable get_dt_for_all_template_child_issue_4_one_hrd(string p_template_hdr_id)
        {
            //DataTable dt = lb.clsDB_template_child_issue_custom.get_dt_for_all_template_child_issue_4_one_hrd(p_template_hdr_id);
            string m_method = "clsDB_template_child_issue_custom.get_dt_for_all_template_child_issue_4_one_hrd";
            DataTable dt = new DataTable();
            string uid = "";
            try
            {
                dt = lb.clsDB_template_child_issue_gen.get_dt_template_child_issue_columns(dt);
                clsSQLiteDB databaseObject = new clsSQLiteDB();
                string sql = "Select * from template_child_issue";
                sql = sql + " where template_hdr_id = '" + p_template_hdr_id + "'";
                sql = sql + " order by summary";
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
                        //dr["soundex"] = lb.clsUtils.Soundex(result["summary"].ToString());
                        //dr["soundex_passed"] = "No";
                        dr["description"] = result["description"].ToString();
                        //*
                        //* Reporter
                        //*
                        dr["reporter_id"] = result["reporter"].ToString();
                        uid = result["reporter"].ToString();
                        dr["reporter"] = lb.clsDB_user_store_custom.get_displayname_from_uid(uid);
                        //*
                        //* Assignee
                        //*
                        dr["assignee_id"] = result["assignee"].ToString();
                        uid = result["assignee"].ToString();
                        dr["assignee"] = lb.clsDB_user_store_custom.get_displayname_from_uid(uid);
                        // Fix Version
                        dr["fix_version"] = result["fix_version"].ToString();
                        dr["epic_link"] = result["epic_link"].ToString();
                        dr["story_points"] = result["story_points"].ToString();
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
    }
}
