using System;
using System.Data;
using System.Data.SQLite;
namespace lb
{
    class clsDB_template_hdr_custom
    {
        public static DataTable get_dt_for_key_template_hdr(string p_template_hdr_id)
        {
            string m_method = "clsDB_template_hdr_custom.get_dt_for_key_template_hdr";
            DataTable dt = new DataTable();
            try
            {
                dt = lb.clsDB_template_hdr_gen.get_dt_template_hdr_columns(dt);
                clsSQLiteDB databaseObject = new clsSQLiteDB();
                string sql = "Select * from template_hdr";
                sql = sql + " where  template_hdr_id = '" + p_template_hdr_id + "'";
                SQLiteCommand myCommand = new SQLiteCommand(sql, databaseObject.myConfigConnection);
                databaseObject.OpenConnection();
                SQLiteDataReader result = myCommand.ExecuteReader();
                if (result.HasRows)
                {
                    while (result.Read())
                    {
                        DataRow dr = dt.NewRow();
                        dr["btn01"] = "Copy";
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
                lb.clsLogger.WriteLog("Error in : " + m_method + " : " + ex.Message);
                return dt;
            }
        }
        //Int32 issue_type_id = lb.clsDB_template_hdr_custom.get_template_issue_type(m_template_hdr_id);
        public static Int32 get_template_issue_type(string p_template_hdr_id)
        {
            string m_method = "clsDB_template_hdr_custom.get_template_issue_type";
            Int32 issue_type_id = 0;
            DataTable p_dt;
            try
            {
                clsSQLiteDB databaseObject = new clsSQLiteDB();
                string sql = "Select issue_type_id from template_hdr";
                sql = sql + " where template_hdr_id = '" + p_template_hdr_id + "'";

                SQLiteCommand myCommand = new SQLiteCommand(sql, databaseObject.myConfigConnection);
                databaseObject.OpenConnection();
                SQLiteDataReader result = myCommand.ExecuteReader();
                if (result.HasRows)
                {
                    while (result.Read())
                    {
                        issue_type_id = Convert.ToInt32(result["issue_type_id"]);
                    }
                }
                return issue_type_id;
            }
            catch (Exception ex)
            {
                lb.clsLogger.WriteLog("Error in : " + m_method + " : " + ex.Message);
                return issue_type_id;
            }
        }
        //Int32 template_count_per_dbo = lb.clsDB_template_hdr_custom.get_template_count_per_dbo(m_dbo_id);
        public static Int32 get_template_count_per_dbo(string p_dbo_id)
        {
            string m_method = "clsDB_template_hdr_custom.get_template_count_per_dbo";
            Int32 template_count_per_dbo = 0;
            DataTable p_dt;
            try
            {
                clsSQLiteDB databaseObject = new clsSQLiteDB();
                string sql = "Select count(*) as cnt_template from template_hdr";
                sql = sql + " where dbo_id = '" + p_dbo_id + "'";

                SQLiteCommand myCommand = new SQLiteCommand(sql, databaseObject.myConfigConnection);
                databaseObject.OpenConnection();
                SQLiteDataReader result = myCommand.ExecuteReader();
                if (result.HasRows)
                {
                    while (result.Read())
                    {
                        template_count_per_dbo = Convert.ToInt32(result["cnt_template"]);
                    }
                }
                return template_count_per_dbo;
            }
            catch (Exception ex)
            {
                lb.clsLogger.WriteLog("Error in : " + m_method + " : " + ex.Message);
                return template_count_per_dbo;
            }
        }
        //m_to_dbo = cboToDBO.Text;
        //            m_to_type = cboToType.Text;
        //            m_to_template = txtToTemplate.Text;
        public static string get_hdr_id_4_dbo_type_and_template(string p_dbo_id, string p_task_type_id, string p_template)
        {
            string m_method = "clsDB_template_hdr_custom.get_hdr_id_4_dbo_type_and_template";
            string template_hdr_id = "";
            try
            {
                //dt = lb.clsDB_template_hdr_gen.get_dt_template_hdr_columns(dt);
                clsSQLiteDB databaseObject = new clsSQLiteDB();
                string sql = "Select * from template_hdr";
                sql = sql + " Where dbo_id = '" + p_dbo_id + "'";
                sql = sql + " and type_id = '" + p_task_type_id + "'";
                sql = sql + " and template = '" + p_template + "'";
                SQLiteCommand myCommand = new SQLiteCommand(sql, databaseObject.myConfigConnection);
                databaseObject.OpenConnection();
                SQLiteDataReader result = myCommand.ExecuteReader();
                if (result.HasRows)
                {
                    while (result.Read())
                    {
                        template_hdr_id = result["template_hdr_id"].ToString();
                    }
                }
                databaseObject.CloseConnection();
                return template_hdr_id;
            }
            catch (Exception ex)
            {
                lb.clsLogger.WriteLog("Error in : " + m_method + " : " + ex.Message);
                return template_hdr_id;
            }
        }

        public static bool copy_template_hdr(string m_template_hdr_id, string p_dbo_id, string p_to_task_type_id, string p_to_issue_type_id, string p_to_template)
        {
            string m_method = "clsDB_template_hdr_custom.copy_template_hdr";
            bool template_copied = false;
            string m_summary = "";
            string m_description = "";
            string m_auth_type = "";
            string m_auth_value = "";
            string m_parent_issue_type = "";
            string m_label = "";
            //string m_project_id = "";
            try
            {
                DataTable dt_template = lb.clsDB_template_hdr_custom.get_dt_for_key_template_hdr(m_template_hdr_id);
                foreach (DataRow row in dt_template.Rows)
                {
                    m_summary = row["summary"].ToString();
                    m_description = row["description"].ToString();
                    m_parent_issue_type = row["parent_issue_type"].ToString();
                    //m_label = row["label"].ToString();
                    m_label = "";  //!!!!!!!!!!!!!!!!!!!!!!!  CLEAR ALL LABELS, otherwise it will confuse the FRM PROJECT
                }
                m_auth_type = "User";
                m_auth_value = "All";
                //m_project_id = "";
                //Insert Template HDR
                //lb.clsDB_template_hdr_gen.insert(p_dbo_id, m_project_id, p_to_task_type_id, p_to_issue_type_id, p_to_template, m_summary, m_description, m_auth_type, m_auth_value, m_parent_issue_type, m_label);
                lb.clsDB_template_hdr_gen.insert(p_dbo_id,  p_to_task_type_id, p_to_issue_type_id, p_to_template, m_summary, m_description, m_auth_type, m_auth_value, m_parent_issue_type, m_label);
                return template_copied;
            }
            catch (Exception ex)
            {
                lb.clsLogger.WriteLog("Error in : " + m_method + " : " + ex.Message);
                return template_copied;
            }
        }
        public static bool template_exist_on_hdr_4_dbo_type_and_template(string p_dbo_id, string p_type_id, string p_template)
        {
            string m_method = "clsDB_template_hdr_custom.template_exist_on_hdr_4_dbo_type_and_template";
            bool template_exist = false;
            try
            {
                //dt = lb.clsDB_template_hdr_gen.get_dt_template_hdr_columns(dt);
                clsSQLiteDB databaseObject = new clsSQLiteDB();
                string sql = "Select * from template_hdr";
                sql = sql + " Where dbo_id = '" + p_dbo_id + "'";
                sql = sql + " and type_id = '" + p_type_id + "'";
                sql = sql + " and template = '" + p_template + "'";
                SQLiteCommand myCommand = new SQLiteCommand(sql, databaseObject.myConfigConnection);
                databaseObject.OpenConnection();
                SQLiteDataReader result = myCommand.ExecuteReader();
                if (result.HasRows)
                {
                    while (result.Read())
                    {
                        template_exist = true;
                    }
                }
                databaseObject.CloseConnection();
                return template_exist;
            }
            catch (Exception ex)
            {
                lb.clsLogger.WriteLog("Error in : " + m_method + " : " + ex.Message);
                return template_exist;
            }
        }
        //  public static DataTable get_dt_for_all_template_hdr_per_dbo_and_interface_type(string p_dbo_id, string p_project_id, string p_type_id)
        public static DataTable get_dt_for_all_template_hdr_per_dbo_and_interface_type(string p_dbo_id, string p_type_id)
        {
            string m_method = "clsDB_template_hdr_custom.get_dt_for_all_template_hdr_per_dbo_and_interface_type";
            string template_hdr_id = "";
            DataTable dt = new DataTable();
            try
            {
                dt = lb.clsDB_template_hdr_gen.get_dt_template_hdr_columns(dt);
                clsSQLiteDB databaseObject = new clsSQLiteDB();
                string sql = "Select * from template_hdr";
                sql = sql + " Where dbo_id = '" + p_dbo_id + "'";
                //sql = sql + " and project_id = '" + p_project_id + "'";
                sql = sql + " and type_id = '" + p_type_id + "'";
                
                SQLiteCommand myCommand = new SQLiteCommand(sql, databaseObject.myConfigConnection);
                databaseObject.OpenConnection();
                SQLiteDataReader result = myCommand.ExecuteReader();
                if (result.HasRows)
                {
                    while (result.Read())
                    {
                        DataRow dr = dt.NewRow();
                        dr["btn01"] = "Copy";
                        dr["template_hdr_id"] = result["template_hdr_id"].ToString();
                        template_hdr_id = dr["template_hdr_id"].ToString(); ;
                        dr["dbo_id"] = result["dbo_id"].ToString();
                      //  dr["project_id"] = result["project_id"].ToString();
                        //dr["dbo"] = result["dbo"].ToString();
                        dr["type_id"] = result["type_id"].ToString();
                        dr["type"] = lb.clsDB_cbo_det_custom.get_value_01_for_cbo_det(dr["type_id"].ToString());
                        dr["issue_type_id"] = result["issue_type_id"].ToString();
                        //          dr["issue_type"] = result["issue_type"].ToString();
                        dr["issue_type"] = lb.clsDB_cbo_det_custom.get_value_01_for_cbo_det(dr["issue_type_id"].ToString());
                        //dr["issue_type"] = lb.clsDB_cbo_det_custom.
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
                lb.clsLogger.WriteLog("Error in : " + m_method + " : " + ex.Message);
                return dt;
            }
        }

    }
}
