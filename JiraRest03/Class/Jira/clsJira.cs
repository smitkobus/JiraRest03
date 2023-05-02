using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Threading.Tasks;
using System.IO;
using System.Reflection;

namespace lb
{
    class clsJira
    {

        // string jira_url = lb.clsJira.get_url_for_issue_list(issue_list);  !!!!!!!!!!!!  Get a Issue from Jira
        //*********************************************************************************************
        //* See if UserStory have ANY Selected Acceptance
        //*********************************************************************************************
        //DataTable dt_issues = lb.clsJira.get_parent_info_dt(dt_issues);
        public static DataTable get_parent_info_dt(string p_dbo_id, DataTable dt_issues)
        {
            string m_method = "clsJira.get_parent_info_dt";
            string m_issue_list = "";
            string m_parent_key = "";
            string m_parent_type = "";
            string m_parent_desc = "";
            try
            {
                //dt_issues.DefaultView.Sort = "parent_key ASC";
                //DataView dv = new DataView(dt_issues);
                //dv.Sort = "parent_key";
                //foreach (DataRowView row in dv)
                //{
                //    string m_parent_key = row["parent_key"].ToString();
                //    if(m_issue_list.Contains(m_parent_key) == false)
                //    {
                //        m_issue_list = m_issue_list + m_parent_key + ";";
                //    }

                //    // Console.WriteLine(" {0} \t {1}", row["ItemIndex"], row["ItemValue"]);
                //}
                foreach (DataRow row in dt_issues.Rows)
                {
                    m_parent_key = row["parent_key"].ToString();
                    m_parent_type = row["issuetype"].ToString();
                    // Get Parent Summary
                    Chilkat.StringBuilder sbResponseBody_parent = lb.clsJira_From_File.get_responsebody_from_dbo_id_and_epic(p_dbo_id, m_parent_key);
                    m_parent_desc = lb.clsResponseBody.get_summary(sbResponseBody_parent);
                    //  lb.clsDB_parent_info_gen.modify(m_parent_key, m_parent_type, m_parent_desc);
                    //string status = row["Status"].ToString();
                    // row["parent_summary"] = "bla bla bla";
                    row["parent_summary"] = m_parent_desc;

                }
                return dt_issues;
            }
            catch (Exception ex)
            {
                lb.clsLogger.WriteLog("Error in : " + m_method + " : " + ex.Message);
                return dt_issues;
            }

        }
        // string jira_url = lb.clsJira.get_url_for_issue_list(issue_list);  !!!!!!!!!!!!  Get a Issue from Jira
        //*********************************************************************************************
        //* See if UserStory have ANY Selected Acceptance
        //*********************************************************************************************
        //bool m_does_user_story_have_selected_acceptance = lb.clsJira.does_user_story_have_selected_acceptance(m_dbo_id,m_epic_key,m_issue_key);
        public static bool does_user_story_have_selected_acceptance(string p_dbo_id, string p_epic_key, string p_issue_key)
        {
            string m_method = "clsJira.does_user_story_have_selected_acceptance";
            bool m_does_user_story_have_selected_acceptance = false;
            try
            {
                Chilkat.StringBuilder sbResponseBody = lb.clsJira_From_File.get_responsebody_from_dbo_id_and_epic(p_dbo_id, p_epic_key);
                bool m_have_selected_acceptance = lb.clsResponseBody.check_if_user_story_have_selected_acceptance(sbResponseBody, p_issue_key);
                return m_does_user_story_have_selected_acceptance;
            }
            catch (Exception ex)
            {
                lb.clsLogger.WriteLog("Error in : " + m_method + " : " + ex.Message);
                return m_does_user_story_have_selected_acceptance;
            }

        }
        //*********************************************************************************************
        //* Get Last Update Date for EPICS
        //*********************************************************************************************
        //string m_last_update_date = lb.clsJira.get_last_update_date_for_epic(p_dbo_id,  p_key);
        public static (DateTime, Int32, Int32, Int32, Int32, Int32, Int32, Int32, Int32, Int32) get_last_update_date_for_epic(string p_dbo_id, string p_epic_key)
        {
            string m_method = "clsJira.get_last_update_date_for_epic";
            //Chilkat.StringBuilder sbResponseBody_Epic
            string sFull_Path = "";
            Chilkat.StringBuilder sbResponseBody_Stories;
            // string m_last_update_date = "2000-06-17T10:27:33.000+0000";
            string m_last_update_date = "1900-01-01";
            DateTime mdt_last_update_date = Convert.ToDateTime(m_last_update_date);
            Int32 count_i = 0;
            Int32 i = 0;
            Int32 u00 = 0;
            Int32 u01 = 0;
            Int32 u02 = 0;
            Int32 u03 = 0;
            Int32 u04 = 0;
            Int32 u05 = 0;
            Int32 u06 = 0;
            Int32 u07 = 0;
            Int32 u07p = 0;
            try
            {
                if (p_epic_key == "EDAD-2672")
                {

                }
                //*
                //* Get EPIC FILE
                //*
                sFull_Path = lb.clsFile.get_latest_file_for_dbo_id_and_jira_key(Convert.ToInt32(p_dbo_id), p_epic_key);
                Chilkat.JsonObject jsonResponse = new Chilkat.JsonObject();
                sbResponseBody_Stories = lb.clsChilKat.get_stringbuilder_from_file_path(sFull_Path);
                jsonResponse.LoadSb(sbResponseBody_Stories);
                // m_last_update_date = jsonResponse.StringOf("issues[0].fields.updated");
                //*
                //* Get All UpDate Dates for user Stories
                //*
                count_i = jsonResponse.SizeOfArray("issues");
                string m_str = "";
                while (i < count_i)
                {

                    //*
                    //* Get Story KEY
                    //*
                    string attribute = "issues[i].key";
                    attribute = attribute.Replace("[i]", "[" + i + "]");
                    string m_issue_key = jsonResponse.StringOf(attribute);
                    //*
                    //* Get File for User Stories to GET the ACCEPTANCE Criteria
                    //*
                    attribute = "issues[i].fields.updated";
                    attribute = attribute.Replace("[i]", "[" + i + "]");
                    string m_updated = jsonResponse.StringOf(attribute);
                    m_updated = m_updated.Replace("T", " ");
                    DateTime mdt_updated = Convert.ToDateTime(m_updated);
                    //  m_str = m_str + m_updated + "\r\n";
                    //Int32 m_days = Convert.ToInt32((DateTime.Now - mdt_updated).TotalDays);
                    //double m_working_days = lb.clsUtils.WorkDays(DateTime.Now, mdt_updated);
                    Int32 m_working_days = lb.clsUtils.GetNumberOfWorkingDays(mdt_updated, DateTime.Now);
                    //if (m_days!= m_working_days)
                    //{

                    //}
                    switch (m_working_days)
                    {
                        case 0:
                            u00 = u00 + 1;
                            break;
                        case 1:
                            u01 = u01 + 1;
                            break;
                        case 2:
                            u02 = u02 + 1;
                            break;
                        case 3:
                            u03 = u03 + 1;
                            break;
                        case 4:
                            u04 = u04 + 1;
                            break;
                        case 5:
                            u05 = u05 + 1;
                            break;
                        case 6:
                            u06 = u06 + 1;
                            break;
                        case 7:
                            u07 = u07 + 1;
                            break;
                        case int n when n >= 7:
                            u07p = u07p + 1;
                            break;

                        default:
                            u00 = u00 + 1;
                            break;
                    }
                    if (mdt_updated > mdt_last_update_date)
                    {
                        mdt_last_update_date = mdt_updated;
                    }
                    i = i + 1;
                }
                return (mdt_last_update_date, u00, u01, u02, u03, u04, u05, u06, u07, u07p);
            }
            catch (Exception ex)
            {
                lb.clsLogger.WriteLog("Error in : " + m_method + " : " + ex.Message);
                return (mdt_last_update_date, u00, u01, u02, u03, u04, u05, u06, u07, u07p);
            }
        }
        //*********************************************************************************************
        //* Get Last Update Date for User Stories
        //*********************************************************************************************
        //string m_last_update_date = lb.clsJira.get_last_update_date_for_epic_for_child_issue(p_dbo_id,  p_key);
        public static (DateTime, Int32, Int32, Int32, Int32, Int32, Int32, Int32, Int32, Int32) get_last_update_date_for_epic_for_child_issue(string p_dbo_id, string p_epic_key, string p_issue_key)
        {
            string m_method = "clsJira.get_last_update_date_for_epic_for_child_issue";
            //Chilkat.StringBuilder sbResponseBody_Epic
            string sFull_Path = "";
            Chilkat.StringBuilder sbResponseBody_Stories;
            // string m_last_update_date = "2000-06-17T10:27:33.000+0000";
            string m_last_update_date = "1900-01-01";
            DateTime mdt_last_update_date = Convert.ToDateTime(m_last_update_date);
            Int32 count_i = 0;
            Int32 i = 0;
            Int32 u00 = 0;
            Int32 u01 = 0;
            Int32 u02 = 0;
            Int32 u03 = 0;
            Int32 u04 = 0;
            Int32 u05 = 0;
            Int32 u06 = 0;
            Int32 u07 = 0;
            Int32 u07p = 0;
            try
            {
                if (p_epic_key == "EDAD-2672")
                {

                }
                //*
                //* Get EPIC FILE
                //*
                sFull_Path = lb.clsFile.get_latest_file_for_dbo_id_and_jira_key(Convert.ToInt32(p_dbo_id), p_epic_key);
                Chilkat.JsonObject jsonResponse = new Chilkat.JsonObject();
                sbResponseBody_Stories = lb.clsChilKat.get_stringbuilder_from_file_path(sFull_Path);
                jsonResponse.LoadSb(sbResponseBody_Stories);
                // m_last_update_date = jsonResponse.StringOf("issues[0].fields.updated");
                //*
                //* Get All UpDate Dates for user Stories
                //*
                count_i = jsonResponse.SizeOfArray("issues");
                string m_str = "";
                while (i < count_i)
                {

                    //*
                    //* Get Story KEY
                    //*
                    string attribute = "issues[i].key";
                    attribute = attribute.Replace("[i]", "[" + i + "]");
                    string m_issue_key = jsonResponse.StringOf(attribute);
                    if (m_issue_key == p_issue_key)
                    {
                        //*
                        //* Get File for User Stories to GET the ACCEPTANCE Criteria
                        //*
                        attribute = "issues[i].fields.updated";
                        attribute = attribute.Replace("[i]", "[" + i + "]");
                        string m_updated = jsonResponse.StringOf(attribute);
                        m_updated = m_updated.Replace("T", " ");
                        DateTime mdt_updated = Convert.ToDateTime(m_updated);
                        //  m_str = m_str + m_updated + "\r\n";
                        //Int32 m_days = Convert.ToInt32((DateTime.Now - mdt_updated).TotalDays);
                        Int32 m_working_days = lb.clsUtils.GetNumberOfWorkingDays(mdt_updated, DateTime.Now);
                        switch (m_working_days)
                        {
                            case 0:
                                u00 = u00 + 1;
                                break;
                            case 1:
                                u01 = u01 + 1;
                                break;
                            case 2:
                                u02 = u02 + 1;
                                break;
                            case 3:
                                u03 = u03 + 1;
                                break;
                            case 4:
                                u04 = u04 + 1;
                                break;
                            case 5:
                                u05 = u05 + 1;
                                break;
                            case 6:
                                u06 = u06 + 1;
                                break;
                            case 7:
                                u07 = u07 + 1;
                                break;
                            case int n when n >= 7:
                                u07p = u07p + 1;
                                break;

                            default:
                                u00 = u00 + 1;
                                break;
                        }
                        if (mdt_updated > mdt_last_update_date)
                        {
                            mdt_last_update_date = mdt_updated;
                        }
                        return (mdt_last_update_date, u00, u01, u02, u03, u04, u05, u06, u07, u07p);
                    }
                    i = i + 1;
                }
                return (mdt_last_update_date, u00, u01, u02, u03, u04, u05, u06, u07, u07p);
            }
            catch (Exception ex)
            {
                lb.clsLogger.WriteLog("Error in : " + m_method + " : " + ex.Message);
                return (mdt_last_update_date, u00, u01, u02, u03, u04, u05, u06, u07, u07p);
            }
        }




        //*********************************************************************************************
        //* Get User Strories for EPIC
        //* p_dbo_id       : To get the folder
        //* p_epic_key     : Get this File
        //* p_story_key    : The Story KEY to get the Acceptance for
        //*********************************************************************************************
        //DataTable dtAcceptance = lb.clsJira.get_user_story_acceptance_cnt(sbResponseBody);
        public static DataTable get_user_story_acceptance_dt(string p_dbo_id, string p_epic_key, string p_story_key)
        {
            string m_method = "clsJira.get_user_story_acceptance_dt";
            DataTable dt = new DataTable();
            string sFull_Path = "";
            Chilkat.StringBuilder sbResponseBody_Stories;
            if (p_epic_key == "EXXD-1809")
            {

            }
            int count_i = 0;
            int count_j = 0;
            string m_name = "";
            int i = 0;
            int j = 0;
            bool m_checked = false;
            bool m_process_user_story = false;
            string m_attribute = "";
            string m_status = "";
            bool m_isHeader = false;
            string m_rank = "0";
            bool m_mandatory = true;
            try
            {
                dt = get_dt_user_story_acceptance_columns(dt);
                if (p_epic_key == "EXXD-1685")
                {

                }

                sFull_Path = lb.clsFile.get_latest_file_for_dbo_id_and_jira_key(Convert.ToInt32(p_dbo_id), p_epic_key);
                Chilkat.JsonObject jsonResponse = new Chilkat.JsonObject();
                sbResponseBody_Stories = lb.clsChilKat.get_stringbuilder_from_file_path(sFull_Path);
                jsonResponse.LoadSb(sbResponseBody_Stories);

                //*
                //* Get File for User Stories to GET the ACCEPTANCE Criteria
                //*

                count_i = jsonResponse.SizeOfArray("issues");
                while (i < count_i)
                {
                    m_process_user_story = false;
                    //*
                    //* Get File for User Stories to GET the ACCEPTANCE Criteria
                    //*
                    string attribute = "issues[i].key";
                    attribute = attribute.Replace("[i]", "[" + i + "]");
                    string value_key = jsonResponse.StringOf(attribute);
                    if (value_key == "BDASINGZA-3530")
                    {

                    }
                    attribute = "issues[i].fields.customfield_11100";
                    attribute = attribute.Replace("[i]", "[" + i + "]");
                    count_j = jsonResponse.SizeOfArray(attribute);
                    if (value_key == p_story_key)
                    {
                        m_process_user_story = true;
                    }
                    if (m_process_user_story == true)
                    {
                        j = 0;

                        while (j < count_j)
                        {
                            jsonResponse.J = j;
                            //m_acceptance_total_cnt = m_acceptance_total_cnt + count_j;
                            //*
                            //* Name
                            //*
                            m_attribute = "issues[i].fields.customfield_11100[j].name";
                            m_name = lb.clsJira.get_jsonResponse_StringOf(jsonResponse, m_attribute, i, j);
                            //*
                            //* Checked
                            //*
                            m_attribute = "issues[i].fields.customfield_11100[j].checked";
                            m_checked = lb.clsJira.get_jsonResponse_BoolOf(jsonResponse, m_attribute, i, j);
                            //*
                            //* Mandatory
                            //*
                            m_attribute = "issues[i].fields.customfield_11100[j].mandatory";
                            m_mandatory = lb.clsJira.get_jsonResponse_BoolOf(jsonResponse, m_attribute, i, j);
                            //*
                            //* Name
                            //*
                            m_attribute = "issues[i].fields.customfield_11100[j].rank";
                            m_rank = lb.clsJira.get_jsonResponse_StringOf(jsonResponse, m_attribute, i, j);
                            //*
                            //* isHeader
                            //*
                            m_attribute = "issues[i].fields.customfield_11100[j].mandatory";
                            m_isHeader = lb.clsJira.get_jsonResponse_BoolOf(jsonResponse, m_attribute, i, j);
                            //*
                            //* Name
                            //*
                            m_attribute = "issues[i].fields.customfield_11100[j].status";
                            m_status = lb.clsJira.get_jsonResponse_StringOf(jsonResponse, m_attribute, i, j);


                            // ADD DataROW
                            DataRow dr = dt.NewRow();
                            dr["p_issue_key"] = value_key.ToString();
                            dr["name"] = m_name;
                            dr["checked"] = m_checked;
                            dr["mandatory"] = m_mandatory;
                            dr["rank"] = m_rank;
                            dr["isHeader"] = m_isHeader;
                            dr["status"] = m_status;
                            //p_dt.Columns.Add("p_issue_key");
                            //p_dt.Columns.Add("name");
                            //p_dt.Columns.Add("checked");
                            //p_dt.Columns.Add("mandatory");
                            //p_dt.Columns.Add("rank");
                            //p_dt.Columns.Add("isHeader");
                            //p_dt.Columns.Add("status");
                            dt.Rows.Add(dr);
                            j = j + 1;
                        }

                    }
                    if (value_key == "BDASINGZA-3532")
                    {

                    }
                    i = i + 1;
                }
                return dt;
            }
            catch (Exception ex)
            {
                lb.clsLogger.WriteLog("Error in : " + m_method + " : " + ex.Message);

                return dt;
            }
        }
        //*********************************************************************************************
        //* Get jsonResponse - StringOf
        //* jsonResponse   : jsonResponse
        //* p_attribute    : Get this File
        //* i              : Index for every Jira Issue
        //* j              : Index for every Acceptance Criteria
        //*********************************************************************************************
        //string val = lb.clsJira.get_jsonResponse_StringOf(jsonResponse,m_attribute,i,j);
        public static string get_jsonResponse_StringOf(Chilkat.JsonObject jsonResponse, string p_attribute, Int32 i, Int32 j)
        {
            string m_method = "clsJira.get_jsonResponse_StringOf";
            string jsonStringOf = "";
            try
            {
                p_attribute = p_attribute.Replace("[i]", "[" + i + "]");
                p_attribute = p_attribute.Replace("[j]", "[" + j + "]");
                jsonStringOf = jsonResponse.StringOf(p_attribute);
                return jsonStringOf;
            }
            catch (Exception ex)
            {
                lb.clsLogger.WriteLog("Error in : " + m_method + " : " + ex.Message);
                return jsonStringOf;
            }

        }
        //*********************************************************************************************
        //* Get jsonResponse - StringOf
        //* jsonResponse   : jsonResponse
        //* p_attribute    : Get this File
        //* i              : Index for every Jira Issue
        //* j              : Index for every Acceptance Criteria
        //*********************************************************************************************
        //bool val = lb.clsJira.get_jsonResponse_BoolOf(jsonResponse,m_attribute,i,j);
        public static bool get_jsonResponse_BoolOf(Chilkat.JsonObject jsonResponse, string p_attribute, Int32 i, Int32 j)
        {
            string m_method = "clsJira.get_jsonResponse_BoolOf";
            bool jsonBoolOf = false;
            try
            {
                p_attribute = p_attribute.Replace("[i]", "[" + i + "]");
                p_attribute = p_attribute.Replace("[j]", "[" + j + "]");
                jsonBoolOf = jsonResponse.BoolOf(p_attribute);
                return jsonBoolOf;
            }
            catch (Exception ex)
            {
                lb.clsLogger.WriteLog("Error in : " + m_method + " : " + ex.Message);
                return jsonBoolOf;
            }

        }
        public static DataTable get_dt_user_story_acceptance_columns(DataTable p_dt)
        {
            string m_method = "clsJira.get_dt_user_story_acceptance_columns";
            try
            {//get_user_story_acceptance
                p_dt.Columns.Add("p_issue_key");
                p_dt.Columns.Add("name");
                p_dt.Columns.Add("checked");
                p_dt.Columns.Add("mandatory");
                p_dt.Columns.Add("rank");
                p_dt.Columns.Add("isHeader");
                p_dt.Columns.Add("status");
                return p_dt;
            }
            catch (Exception ex)
            {
                lb.clsLogger.WriteLog("Error in : " + m_method + " : " + ex.Message);
                return p_dt;
            }
        }
        //*********************************************************************************************
        //* Get User Strories for EPIC
        //*********************************************************************************************
        //status = lb.clsJira.get_user_story_acceptance_cnt(sbResponseBody);
        public static async Task create_files_per_epics_from_an_epic_list_ParalleAsync(Chilkat.StringBuilder sbResponseBody, string p_dbo)
        {
            string m_method = "clsJira.create_files_per_epics_from_an_epic_list_ParalleAsync";
            Int32 rc = 0;
            Int32 count_i = 0;
            Int32 i = 0;
            Chilkat.JsonObject jsonResponse = new Chilkat.JsonObject();
            string keys = "";
            string sFull_Path;
            List<Task<string>> tasks = new List<Task<string>>();
            try
            {
                jsonResponse.LoadSb(sbResponseBody);
                count_i = jsonResponse.SizeOfArray("issues");
                while (i < count_i)
                {
                    string attribute = "issues[i].key";
                    attribute = attribute.Replace("[i]", "[" + i + "]");
                    string value_key = jsonResponse.StringOf(attribute);
                    tasks.Add(Task.Run(() => save_json_epic_file(p_dbo, value_key)));
                    keys = keys + value_key + ";";
                    i = i + 1;
                }
                var results = await Task.WhenAll(tasks);
            }
            catch (Exception ex)
            {
                lb.clsLogger.WriteLog("Error in : " + m_method + " : " + ex.Message);
            }
        }
        public static async Task create_files_per_epics_from_an_epic_list_Async(Chilkat.StringBuilder sbResponseBody, string p_dbo)
        {
            string m_method = "clsJira.create_files_per_epics_from_an_epic_list_Async";
            Int32 rc = 0;
            Int32 count_i = 0;
            Int32 i = 0;
            Chilkat.JsonObject jsonResponse = new Chilkat.JsonObject();
            string keys = "";
            string sFull_Path;
            try
            {
                jsonResponse.LoadSb(sbResponseBody);
                count_i = jsonResponse.SizeOfArray("issues");
                while (i < count_i)
                {
                    string attribute = "issues[i].key";
                    attribute = attribute.Replace("[i]", "[" + i + "]");
                    string value_key = jsonResponse.StringOf(attribute);
                    sFull_Path = await Task.Run(() => save_json_epic_file(p_dbo, value_key));
                    keys = keys + value_key + ";";
                    i = i + 1;
                }
            }
            catch (Exception ex)
            {
                lb.clsLogger.WriteLog("Error in : " + m_method + " : " + ex.Message);
            }
        }

        public static Int32 create_files_per_epics_from_an_epic_list(Chilkat.StringBuilder sbResponseBody, string p_dbo)
        {
            string m_method = "clsJira.create_files_per_epics_from_an_epic_list";
            Int32 rc = 0;
            Int32 count_i = 0;
            Int32 i = 0;
            Chilkat.JsonObject jsonResponse = new Chilkat.JsonObject();
            string keys = "";
            string sFull_Path;
            try
            {
                jsonResponse.LoadSb(sbResponseBody);
                count_i = jsonResponse.SizeOfArray("issues");
                while (i < count_i)
                {
                    string attribute = "issues[i].key";
                    attribute = attribute.Replace("[i]", "[" + i + "]");
                    string value_key = jsonResponse.StringOf(attribute);
                    //sFull_Path = save_json_for_all_stories_linked_to_epic(value_key, p_dbo);
                    //sFull_Path = await Task.Run(() => save_json_for_all_stories_linked_to_epic(value_key, p_dbo));
                    keys = keys + value_key + ";";
                    i = i + 1;
                }
                return rc;
            }
            catch (Exception ex)
            {
                rc = 400;
                return rc;
                lb.clsLogger.WriteLog("Error in : " + m_method + " : " + ex.Message);
            }
        }
        public static string save_json_epic_file(string parm_dbo, string epic)
        {
            string m_method = "clsJira.save_json_epic_file";
            string sFull_Path = "";
            try
            {
                if (epic == "EDAD-2180")
                {

                }
                DateTimeOffset now = (DateTimeOffset)DateTime.Now;
                string ts = now.ToString("yyyyMMdd_HH_mm_ss_fff");
                string sPath = System.IO.Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
                //string sData_Path = sPath + @"\Data\" + parm_dbo + "_" + parm_pi + "_" + parm_status + @"\";
                string sData_Path = sPath + @"\Data\" + parm_dbo + @"\";
                sFull_Path = sData_Path + @"\" + epic + @"_" + ts + @".json";

                Chilkat.StringBuilder sbResponseBody = lb.clsJira.get_user_stories_for_epic(epic);
                if (sbResponseBody.GetAsString().Trim().Length > 10)
                {
                    lb.clsFile.write_text_to_file_by_type("JSON", sFull_Path, sbResponseBody.GetAsString(), false);
                }
                return sFull_Path;
            }
            catch (Exception ex)
            {
                lb.clsLogger.WriteLog("Error in : " + m_method + " : " + ex.Message);
                return sFull_Path;
            }
        }
        //*********************************************************************************************
        //* Get User Strories for EPIC
        //*********************************************************************************************
        //status = lb.clsJira.get_user_story_acceptance_cnt(sbResponseBody);
        public static (Int32, Int32, Int32) get_user_story_acceptance_cnt(string p_dbo_id, string p_key, Int32 p_acceptance_done, Int32 p_acceptance_outstanding, Int32 p_total_acceptance)
        {
            string m_method = "clsJira.get_user_story_acceptance_cnt";
            //Chilkat.StringBuilder sbResponseBody_Epic
            string sFull_Path = "";
            Chilkat.StringBuilder sbResponseBody_Stories;
            if (p_key == "EXXD-1809")
            {

            }
            int count_i = 0;
            int count_j = 0;
            Int32 m_acceptance_done_tot = 0;
            Int32 m_acceptance_outstanding_tot = 0;
            Int32 m_total_acceptance_tot = 0;
            Int32 m_acceptance_done_cnt = 0;
            Int32 m_acceptance_total_cnt = 0;



            string m_name = "";
            ////string ret_status = "";
            ////string a_stat = "";

            int i = 0;
            int j = 0;
            bool m_checked = false;
            //int new_irow = 0;
            //int irow_to_update = 0;
            //string sval = "";
            //bool found_status = false;
            //string[,] a_status = new string[10, 2];
            try
            {
                if (p_key == "EXXD-1685")
                {

                }
                sFull_Path = lb.clsFile.get_latest_file_for_dbo_id_and_jira_key(Convert.ToInt32(p_dbo_id), p_key);
                Chilkat.JsonObject jsonResponse = new Chilkat.JsonObject();
                sbResponseBody_Stories = lb.clsChilKat.get_stringbuilder_from_file_path(sFull_Path);
                jsonResponse.LoadSb(sbResponseBody_Stories);

                //*
                //* Get File for User Stories to GET the ACCEPTANCE Criteria
                //*

                count_i = jsonResponse.SizeOfArray("issues");
                m_acceptance_done_tot = 0;
                m_acceptance_outstanding_tot = 0;
                m_total_acceptance_tot = 0;
                while (i < count_i)
                {
                    //*
                    //* Get File for User Stories to GET the ACCEPTANCE Criteria
                    //*
                    string attribute = "issues[i].key";
                    attribute = attribute.Replace("[i]", "[" + i + "]");
                    string value_key = jsonResponse.StringOf(attribute);
                    if (value_key == "BDASINGZA-3530")
                    {

                    }
                    attribute = "issues[i].fields.customfield_11100";
                    attribute = attribute.Replace("[i]", "[" + i + "]");
                    count_j = jsonResponse.SizeOfArray(attribute);
                    //if (count_j == 0)
                    //{
                    //    m_total_acceptance = m_total_acceptance + 1;
                    //}
                    j = 0;

                    m_acceptance_done_cnt = 0;
                    while (j < count_j)
                    {
                        jsonResponse.J = j;
                        //m_acceptance_total_cnt = m_acceptance_total_cnt + count_j;
                        //*
                        //* Name
                        //*
                        attribute = "issues[i].fields.customfield_11100[j].name";
                        attribute = attribute.Replace("[i]", "[" + i + "]");
                        attribute = attribute.Replace("[j]", "[" + j + "]");
                        m_name = jsonResponse.StringOf(attribute);
                        //*
                        //* Checked
                        //*

                        attribute = "issues[i].fields.customfield_11100[j].checked";
                        attribute = attribute.Replace("[i]", "[" + i + "]");
                        attribute = attribute.Replace("[j]", "[" + j + "]");
                        m_checked = jsonResponse.BoolOf(attribute);
                        if (m_checked == true)
                        {
                            m_acceptance_done_cnt = m_acceptance_done_cnt + 1;
                        }
                        j = j + 1;
                    }
                    if (value_key == "BDASINGZA-3532")
                    {

                    }
                    m_acceptance_done_tot = m_acceptance_done_tot + m_acceptance_done_cnt;
                    m_acceptance_outstanding_tot = m_acceptance_outstanding_tot + count_j - m_acceptance_done_cnt;
                    m_total_acceptance_tot = m_total_acceptance_tot + count_j;
                    i = i + 1;
                }
                return (m_acceptance_done_tot, m_acceptance_outstanding_tot, m_total_acceptance_tot);
            }
            catch (Exception ex)
            {
                lb.clsLogger.WriteLog("Error in : " + m_method + " : " + ex.Message);
                return (m_acceptance_done_tot, m_acceptance_outstanding_tot, m_total_acceptance_tot);
            }
        }
        //*********************************************************************************************
        //* Get Acceptance Counts
        //*********************************************************************************************
        //(Int32 m_acceptance_done,Int32 m_acceptance_outstanding) = lb.clsJira.get_acceptance_counts(m_issue_acceptance);
        public static (Int32, Int32) get_acceptance_counts(string p_key, string p_issue_acceptance)
        {
            string m_method = "clsJira.get_acceptance_counts";
            Int32 m_acceptance_done = 0;
            Int32 m_acceptance_outstanding = 0;
            Int32 cnt_acceptance = 0;
            Int32 i = 0;
            Chilkat.StringBuilder sbResponseBodyAcceptance;
            try
            {
                p_issue_acceptance = "{\"acceptance_list\":" + p_issue_acceptance + "}";
                sbResponseBodyAcceptance = lb.clsChilKat.get_stringbuilder_from_string(p_issue_acceptance);
                Chilkat.JsonObject jsonResponse = new Chilkat.JsonObject();
                jsonResponse.LoadSb(sbResponseBodyAcceptance);
                cnt_acceptance = jsonResponse.SizeOfArray("acceptance_list");
                if (cnt_acceptance > -1)
                {
                    while (i < cnt_acceptance)
                    {
                        string m_name = jsonResponse.StringOf("acceptance_list[i].name");
                        bool m_checked = jsonResponse.BoolOf("acceptance_list[i].checked");
                        if (m_checked == true)
                        {
                            m_acceptance_done = m_acceptance_done + 1;
                        }
                        i = i + 1;
                    }
                    m_acceptance_outstanding = cnt_acceptance - m_acceptance_done;
                }
                return (m_acceptance_done, m_acceptance_outstanding);
            }
            catch (Exception ex)
            {
                lb.clsLogger.WriteLog("Error in : " + m_method + " : " + ex.Message);
                return (m_acceptance_done, m_acceptance_outstanding);
            }
        }
        //*********************************************************************************************
        //* Add Columns for Issue
        //*********************************************************************************************
        public static DataTable get_dt_add_columns_4_issue(DataTable p_dt)
        {
            string m_method = "clsJira.get_dt_add_columns_4_issue";
            try
            {

                p_dt.Columns.Add("Key");
                p_dt.Columns.Add("Summary");
                p_dt.Columns.Add("Story Points");
                p_dt.Columns.Add("Status");
                return p_dt;
            }
            catch (Exception ex)
            {
                lb.clsLogger.WriteLog("Error in : " + m_method + " : " + ex.Message);
                return p_dt;
            }
        }
        //*********************************************************************************************
        //* Add Columns for Issue - ACCEPTANCE
        //*********************************************************************************************
        public static DataTable get_dt_add_columns_4_issue_acceptance(DataTable p_dt)
        {
            string m_method = "clsJira.get_dt_add_columns_4_issue_acceptance";
            try
            {
                //*
                //* Name
                //*
                p_dt.Columns.Add("Name");
                //*
                //* Checked
                //*
                DataColumn colChecked = new DataColumn("checked");
                colChecked.DataType = System.Type.GetType("System.Boolean");
                p_dt.Columns.Add(colChecked);
                //*
                //* Mandatory
                //*
                DataColumn colMandatory = new DataColumn("mandatory");
                colMandatory.DataType = System.Type.GetType("System.Boolean");
                p_dt.Columns.Add(colMandatory);
                return p_dt;
            }
            catch (Exception ex)
            {
                return p_dt;
            }
        }

        //*********************************************************************************************
        //* Get DT for an Issue
        //*********************************************************************************************
        public static (DataTable, DataTable) get_dt_4_issue(string p_issue_key)
        {
            string m_method = "clsJira.get_dt_4_issue";
            string value_key = "";
            string summary = "";
            string story_points = "";
            DataTable dt;
            DataTable dt_acceptance;
            dt = new DataTable();
            dt_acceptance = new DataTable();
            Int32 count_i = 0;
            Int32 count_j = 0;
            Int32 count_acceptance_criteria = 0;
            Int32 i = 0;
            Int32 j = 0;
            Int32 issue_status_sort_id = 0;
            Int32 cntNew = 0;
            Int32 cntInSpecification = 0;
            Int32 cntOpen = 0;
            Int32 cntInProgress = 0;
            Int32 cntInReview = 0;
            try
            {
                // Add the Columns
                dt = get_dt_add_columns_4_issue(dt);
                dt_acceptance = get_dt_add_columns_4_issue_acceptance(dt_acceptance);
                // Get the JSON for the stories
                Chilkat.StringBuilder sbResponseBody = new Chilkat.StringBuilder();
                sbResponseBody = get_string_builder_for_issue(p_issue_key);
                Chilkat.JsonObject jsonResponse = new Chilkat.JsonObject();
                Chilkat.JsonObject jsonResponse02 = new Chilkat.JsonObject();
                jsonResponse.LoadSb(sbResponseBody);
                jsonResponse02.LoadSb(sbResponseBody);
                count_i = jsonResponse.SizeOfArray("issues");

                i = 0;
                while (i < count_i)
                {
                    // Add new row in DT
                    DataRow dr = dt.NewRow();
                    // Get next row in JIRA
                    jsonResponse.I = i;
                    //*
                    //* Acceptace Criteria
                    //*
                    count_acceptance_criteria = jsonResponse.SizeOfArray("issues[i].fields.customfield_11100");
                    j = 0;
                    while (j < count_acceptance_criteria)
                    {
                        jsonResponse02.I = j;
                        DataRow dr_acceptance = dt_acceptance.NewRow();
                        //Name
                        string name_field_name = "issues[i].fields.customfield_11100[j].name";
                        name_field_name = name_field_name.Replace("[i]", "[" + i + "]");
                        name_field_name = name_field_name.Replace("[j]", "[" + j + "]");
                        string m_name = jsonResponse02.StringOf(name_field_name);
                        // Checked
                        string checked_field_name = "issues[i].fields.customfield_11100[j].checked";
                        checked_field_name = checked_field_name.Replace("[i]", "[" + i + "]");
                        checked_field_name = checked_field_name.Replace("[j]", "[" + j + "]");
                        string m_checked = jsonResponse02.StringOf(checked_field_name);
                        //
                        string mandatory_field_name = "issues[i].fields.customfield_11100[j].mandatory";
                        mandatory_field_name = mandatory_field_name.Replace("[i]", "[" + i + "]");
                        mandatory_field_name = mandatory_field_name.Replace("[j]", "[" + j + "]");
                        string m_mandatory = jsonResponse02.StringOf(mandatory_field_name);
                        //
                        m_checked = "1";
                        dr_acceptance["Name"] = m_name;
                        dr_acceptance["checked"] = 1;
                        dr_acceptance["mandatory"] = 1;
                        dt_acceptance.Rows.Add(dr_acceptance);
                        j = j + 1;
                    }
                    value_key = jsonResponse.StringOf("issues[i].key");
                    summary = jsonResponse.StringOf("issues[i].fields.summary");
                    story_points = jsonResponse.StringOf("issues[i].fields.customfield_10006");
                    string issue_status = jsonResponse.StringOf("issues[i].fields.status.name");
                    dr["Key"] = value_key;
                    dr["Summary"] = summary;
                    dr["Story Points"] = story_points;
                    dr["Status"] = issue_status;
                    dt.Rows.Add(dr);

                    i = i + 1;
                }


                return (dt, dt_acceptance);
            }
            catch (Exception ex)
            {
                lb.clsLogger.WriteLog("Error in : " + m_method + " : " + ex.Message);
                return (dt, dt_acceptance);
            }
        }
        //*********************************************************************************************
        //* Get INTERFACE ID for all stories in a DataTable
        //*********************************************************************************************
        //string interface_id = lb.clsJira.get_interface_id_for_all_stories_in_a_dt(m_epic_key);
        public static string get_interface_id_for_all_stories_in_a_dt(string p_dbo_id, string p_task_type_id, string p_epic_key)
        {
            string m_method = "clsJira.get_interface_id_for_all_stories_in_a_dt";
            string m_interface_id = "";
            string m_interface_key = "";
            string m_picked_interface_id = "";
            string m_picked_interface_key = "";
            Int32 m_previous_max_cnt = 0;
            string m_summary = "";
            DataTable dtCurrentStories = new DataTable();
            DataTable dtInterfaces = new DataTable();
            Int32 cntStoriesInInterface = 0;
            try
            {
                dtInterfaces = lb.clsDB_interface_custom.get_dt_for_all_interface_per_dbo_with_key_and_description(p_dbo_id, p_task_type_id);
                dtCurrentStories = get_dt_stories_4_epic(p_dbo_id, p_epic_key);
                //*
                //* Loop in all interfaces
                //*
                foreach (DataRow drInterface in dtInterfaces.Rows)
                {
                    cntStoriesInInterface = 0;
                    m_interface_id = drInterface["interface_id"].ToString().ToUpper();
                    m_interface_key = drInterface["interface_key"].ToString().ToUpper();
                    foreach (DataRow drCurrentStorie in dtCurrentStories.Rows)
                    {
                        m_summary = drCurrentStorie["summary"].ToString().ToUpper();
                        if (m_summary.Contains(m_interface_key) == true)
                        {
                            cntStoriesInInterface = cntStoriesInInterface + 1;
                        }
                    }
                    if (cntStoriesInInterface > 0)
                    {
                        if (m_previous_max_cnt == 0)
                        {
                            m_previous_max_cnt = cntStoriesInInterface;
                            m_picked_interface_id = m_interface_id;
                            m_picked_interface_key = m_interface_key;
                        }
                        else
                        {
                            if (cntStoriesInInterface > m_previous_max_cnt)
                            {
                                m_previous_max_cnt = cntStoriesInInterface;
                                m_picked_interface_id = m_interface_id;
                                m_picked_interface_key = m_interface_key;
                            }
                        }
                    }
                }
                //* Loop in all stories for every interface ad count the records
                return m_picked_interface_id;
            }
            catch (Exception ex)
            {
                lb.clsLogger.WriteLog("Error in : " + m_method + " : " + ex.Message);
                return m_picked_interface_id;
            }
        }
        //*********************************************************************************************
        //* Get all user stories for an EPIC
        //*********************************************************************************************
        public static DataTable get_dt_stories_4_epic(string p_dbo_id, string p_epic_key)
        {
            string m_method = "clsJira.get_dt_stories_4_epic";
            string m_issue_key = "";
            string summary = "";
            string story_points = "";
            DataTable dt;
            dt = new DataTable();
            Int32 count_i = 0;
            Int32 count_j = 0;
            Int32 i = 0;
            Int32 j = 0;
            Int32 issue_status_sort_id = 0;
            Int32 cntNew = 0;
            Int32 cntInSpecification = 0;
            Int32 cntOpen = 0;
            Int32 cntInProgress = 0;
            Int32 cntInReview = 0;
            string sFull_Path = "";
            Chilkat.StringBuilder sbResponseBody_Stories;
            Int32 m_acceptance_done = 0;
            Int32 m_acceptance_outstanding = 0;
            Int32 m_total_acceptance = 0;
            Int32 m_acceptance_total_cnt = 0;
            string m_name = "";
            bool m_checked = false;
            string attribute = "";
            string issue_status = "";
            DateTime m_last_update_date;
            Int32 u00 = 0;
            Int32 u01 = 0;
            Int32 u02 = 0;
            Int32 u03 = 0;
            Int32 u04 = 0;
            Int32 u05 = 0;
            Int32 u06 = 0;
            Int32 u07 = 0;
            Int32 u07p = 0;
            try
            {
                // Add the Columns
                dt = get_dt_stories_4_epic_add_columns(dt);
                // Get the JSON for the stories
                Chilkat.StringBuilder sbResponseBody = new Chilkat.StringBuilder();
                sbResponseBody = get_user_stories_for_epic(p_epic_key);
                Chilkat.JsonObject jsonResponse = new Chilkat.JsonObject();
                jsonResponse.LoadSb(sbResponseBody);
                count_i = jsonResponse.SizeOfArray("issues");
                i = 0;
                while (i < count_i)
                {
                    // Add new row in DT
                    DataRow dr = dt.NewRow();
                    // Get next row in JIRA
                    jsonResponse.I = i;
                    //****************  value_key
                    attribute = "issues[i].key";
                    attribute = attribute.Replace("[i]", "[" + i + "]");
                    m_issue_key = jsonResponse.StringOf(attribute);
                    //****************  
                    attribute = "issues[i].fields.summary";
                    attribute = attribute.Replace("[i]", "[" + i + "]");
                    summary = jsonResponse.StringOf(attribute);
                    //****************
                    attribute = "issues[i].fields.customfield_10006";
                    attribute = attribute.Replace("[i]", "[" + i + "]");
                    story_points = jsonResponse.StringOf(attribute);

                    //****************  
                    attribute = "issues[i].fields.status.name";
                    attribute = attribute.Replace("[i]", "[" + i + "]");
                    issue_status = jsonResponse.StringOf(attribute);
                    //****************  
                    //****************  
                    //****************  
                    dr["Key"] = m_issue_key;
                    if (m_issue_key == "BDASINGZA-4527")
                    {

                    }
                    dr["URL"] = "https://atc.bmwgroup.net/jira/browse/" + m_issue_key;
                    dr["Summary"] = summary;
                    dr["Story Points"] = story_points;
                    dr["Status"] = issue_status;
                    //*
                    //* Get File for User Stories to GET the ACCEPTANCE Criteria
                    //*
                    //****************  
                    attribute = "issues[i].fields.customfield_11100";
                    attribute = attribute.Replace("[i]", "[" + i + "]");
                    count_j = jsonResponse.SizeOfArray(attribute);
                    if (count_j == 0)
                    {
                        m_total_acceptance = m_total_acceptance + 1;
                    }
                    j = 0;
                    m_total_acceptance = 0;
                    m_acceptance_done = 0;
                    m_acceptance_outstanding = 0;
                    while (j < count_j)
                    {
                        jsonResponse.J = j;
                        m_acceptance_total_cnt = m_acceptance_total_cnt + count_j;
                        m_name = jsonResponse.StringOf("issues[i].fields.customfield_11100[j].name");
                        //****************  
                        attribute = "issues[i].fields.customfield_11100[j].name";
                        attribute = attribute.Replace("[i]", "[" + i + "]");
                        attribute = attribute.Replace("[j]", "[" + j + "]");
                        m_checked = jsonResponse.BoolOf(attribute);
                        if (m_checked == true)
                        {
                            m_acceptance_done = m_acceptance_done + 1;
                        }

                        j = j + 1;
                    }
                    if (count_j == -1)
                    {
                        dr["acceptance_outstanding"] = 0;
                        dr["acceptance_done"] = 0;
                        dr["total_acceptance"] = 0;

                    }
                    else
                    {
                        dr["acceptance_outstanding"] = count_j - m_acceptance_done;
                        dr["acceptance_done"] = m_acceptance_done;
                        dr["total_acceptance"] = count_j;

                    }
                    //sFull_Path = lb.clsFile.get_latest_file_for_jira_key(p_epic_key, cboDBO_ID);
                    //string text = File.ReadAllText(sFull_Path);
                    //sbResponseBody_Stories = lb.clsChilKat.get_stringbuilder_from_file_path(sFull_Path);
                    //(m_total_acceptance, m_acceptance_done, m_acceptance_outstanding) = lb.clsJira.get_user_story_acceptance_cnt(value_key, sbResponseBody_Stories, m_total_acceptance, m_acceptance_done, m_acceptance_outstanding);
                    //dr["total_acceptance"] = m_total_acceptance;
                    //dr["acceptance_done"] = m_acceptance_done;
                    //dr["acceptance_outstanding"] = m_acceptance_outstanding;
                    // Updated Dates
                    (m_last_update_date, u00, u01, u02, u03, u04, u05, u06, u07, u07p) = lb.clsJira.get_last_update_date_for_epic_for_child_issue(p_dbo_id, p_epic_key, m_issue_key);
                    dr["storie_last_updated"] = (m_last_update_date.ToString()).Substring(0, 10);
                    dr["u00"] = u00;
                    dr["u01"] = u01;
                    dr["u02"] = u02;
                    dr["u03"] = u03;
                    dr["u04"] = u04;
                    dr["u05"] = u05;
                    dr["u06"] = u06;
                    dr["u07"] = u07;
                    dr["u07p"] = u07p;
                    dt.Rows.Add(dr);
                    i = i + 1;
                }


                return dt;
            }
            catch (Exception ex)
            {
                lb.clsLogger.WriteLog("Error in : " + m_method + " : " + ex.Message);
                return dt;
            }
        }
        //*********************************************************************************************
        //* Get all user stories for an EPIC
        //*********************************************************************************************
        public static DataTable get_dt_stories_4_epic_add_columns(DataTable p_dt)
        {
            string m_method = "clsJira.get_dt_stories_4_epic_add_columns";
            try
            {
                p_dt.Columns.Add("Key");
                p_dt.Columns.Add("Summary");
                p_dt.Columns.Add("Story Points");
                p_dt.Columns.Add("Status");
                p_dt.Columns.Add("acceptance_done");
                p_dt.Columns.Add("acceptance_outstanding");
                p_dt.Columns.Add("total_acceptance");
                p_dt.Columns.Add("URL");
                p_dt.Columns.Add("storie_last_updated");
                p_dt.Columns.Add("u00");
                p_dt.Columns.Add("u01");
                p_dt.Columns.Add("u02");
                p_dt.Columns.Add("u03");
                p_dt.Columns.Add("u04");
                p_dt.Columns.Add("u05");
                p_dt.Columns.Add("u06");
                p_dt.Columns.Add("u07");
                p_dt.Columns.Add("u07p");
                return p_dt;
            }
            catch (Exception ex)
            {
                lb.clsLogger.WriteLog("Error in : " + m_method + " : " + ex.Message);
                return p_dt;
            }
        }
        //*********************************************************************************************
        //* Get Last Date change for the stories
        //*********************************************************************************************
        //status = lb.clsJira.get_last_change_date_for_ResponseBody(sbResponseBody);
        public static (string, string,string ) create_jira_issue(string p_hostname, string p_project, string p_parent_link, string p_summary, string p_description, string p_issue_is_parent_or_child, string p_issue_type, string p_issue_type_desc, string p_fix_version, string p_assignee, string p_reporter, DataTable dtComponents, DataTable dtAcceptance, string m_insert_sql)
        {
            string m_method = "clsJira.create_jira_issue";
            string return_epic = "";
            try
            {

                // Fix any posible problem
                // 1) If DESCRIPTION is EMPTY, move SUMMARY in DESCRIPTION
                if (p_description.Trim().Length == 0)
                {
                    p_description = p_summary;
                }

                Chilkat.Rest rest = new Chilkat.Rest();
                bool success;

                //  URL: https://atc-int.bmwgroup.net/rest/api/2/issue
                bool bTls = true;
                int port = 443;
                bool bAutoReconnect = true;
                //success = rest.Connect("atc-int.bmwgroup.net", port, bTls, bAutoReconnect);
                //success = rest.Connect("atc.bmwgroup.net", port, bTls, bAutoReconnect);
                success = rest.Connect(p_hostname, port, bTls, bAutoReconnect);
                if (success != true)
                {
                    //Debug.WriteLine("ConnectFailReason: " + Convert.ToString(rest.ConnectFailReason));
                    //Debug.WriteLine(rest.LastErrorText);

                }
                //rest.SetAuthBasic("jira@example.com", "JIRA_API_TOKEN");
                string m_uid = lb.clsConfigParameters.get_uid();
                string m_pw = lb.clsConfigParameters.get_pw();
                rest.SetAuthBasic(m_uid, m_pw);

                //* Fields
                string p_name = "QX46522";
                //string p_parent_link = "EXXD-2082";
                string p_status = "Open";
                string p_affected_version = "PI IV/2022";
                string p_priority = "3"; // 3 = Medium
                //*
                //* Now setup the JIRA JSON
                //
                Chilkat.JsonObject m_json = new Chilkat.JsonObject();
                m_json = lb.clsJiraField.add_project_key(m_json, p_project); // Project Key
                m_json = lb.clsJiraField.add_summary(m_json, p_summary); // Summary
                m_json = lb.clsJiraField.add_issuetype_id(m_json, p_issue_type); // Issue Type
                m_json = lb.clsJiraField.add_description(m_json, p_description); // Description
                m_json = lb.clsJiraField.add_component(m_json, dtComponents); //Components
                m_json = lb.clsJiraField.add_fix_version(m_json, p_fix_version); //Fix Version
                m_json = lb.clsJiraField.add_assignee_by_name(m_json, p_assignee);
                m_json = lb.clsJiraField.add_reporter_by_name(m_json, p_reporter);
                if (p_issue_is_parent_or_child.ToLower() == "child")
                {
                    m_json = lb.clsJiraField.add_acceptance_criteria_customfield_11100_child(m_json, dtAcceptance);
                }
                if (p_issue_is_parent_or_child.ToLower() == "parent")
                {
                    Chilkat.JsonObject m_json_parent = lb.clsJiraField.add_acceptance_criteria_customfield_11100_parent(m_json, dtAcceptance);
                    Chilkat.StringBuilder sbRequestBody_parent = new Chilkat.StringBuilder();
                    m_json_parent.EmitSb(sbRequestBody_parent);
                    m_json = lb.clsJiraField.add_acceptance_criteria_customfield_11100_parent(m_json, dtAcceptance);
                }
                if (p_issue_type == "10001") // 10001 : Userstory have an EPIC
                {
                    m_json = lb.clsJiraField.add_epic_link(m_json, p_parent_link);
                }
                else
                {
                    m_json = lb.clsJiraField.add_parent_link(m_json, p_parent_link);
                }
                //

                m_json = lb.clsJiraField.add_priority(m_json, p_priority);
                //m_json = lb.clsJiraField.add_affected_version(m_json, p_affected_version);
                //m_json = lb.clsJiraField.add_status(m_json, p_status);
                //json.UpdateString("fields.project.key", p_project);
                //json.UpdateString("fields.project.id", "10000");
                //m_json.UpdateString("fields.summary", p_summary);
                //m_json.UpdateString("fields.issuetype.id", p_issue_type);
                //json.UpdateString("fields.assignee.name", "matt");
                // m_json.UpdateString("fields.priority.id", "3");
                //json.UpdateString("fields.labels[0]", "bugfix");
                //json.UpdateString("fields.labels[1]", "blitz_test");
                //m_json.UpdateString("fields.description", p_description);
                //json.UpdateString("fields.fixVersions[0].id", "10001");
                //json.UpdateString("fields.customfield_10001", "blah blah");
                if (p_issue_type == "10000")
                {
                    m_json.UpdateString("fields.customfield_10003", p_summary);
                }
                //m_json.UpdateString("fields.customfield_11100[0].name", "aa");
                //m_json.UpdateString("fields.customfield_11100[1].name", "bb");
                rest.AddHeader("Content-Type", "application/json");
                rest.AddHeader("Accept", "application/json");

                //*
                //* Prepare and make the Actual Create for the User Story
                //

                Chilkat.StringBuilder sbRequestBody = new Chilkat.StringBuilder();
                m_json.EmitSb(sbRequestBody);
                Chilkat.StringBuilder sbResponseBody = new Chilkat.StringBuilder();
                //sbRequestBody = @"{{'id':'11210144','key':'BDASINGZA-4169','self':'https://atc.bmwgroup.net/jira/rest/api/2/issue/11210144'}}";
                //sbResponseBody = @{{"id":"11524554","key":"EXXD-2540","self":"https://atc.bmwgroup.net/jira/rest/api/2/issue/11524554"}}
                //{{"id":"11536553","key":"BDASINGZA-4535","self":"https://atc.bmwgroup.net/jira/rest/api/2/issue/11536553"}}
                //sbRequestBody.Append(@"{{'id':'11536553','key':'BDASINGZA-4535','self':'https://atc.bmwgroup.net/jira/rest/api/2/issue/11536553'}}");
                //{{"id":"11557168","key":"BDASINGZA-4560","self":"https://atc.bmwgroup.net/jira/rest/api/2/issue/11557168"}}
                success = rest.FullRequestSb("POST", "/jira/rest/api/2/issue", sbRequestBody, sbResponseBody);
                //  sbResponseBody.Append(@"{{'id':'11536553','key':'BDASINGZA-4535','self':'https://atc.bmwgroup.net/jira/rest/api/2/issue/11536553'}}");
                string m_issue_key = lb.clsJira.get_issue_key_for_ResponseBody(sbResponseBody.ToString());
                //*
                //* Add ISSUE LOG
                //*

                string m_issue_type_id = p_issue_type;
                string m_issue_type = p_issue_type_desc;
                string m_description = p_issue_type_desc + " added";
                m_insert_sql = lb.clsDB_issue_log_custom.build_insert_sql(m_insert_sql, m_issue_key, m_issue_type_id, m_issue_type, m_description);
                return_epic = sbResponseBody.ToString();
                return (m_issue_key, m_insert_sql, sbResponseBody.ToString());
            }
            catch (Exception ex)
            {
                lb.clsLogger.WriteLog("Error in : " + m_method + " : " + ex.Message);
                return (return_epic, m_insert_sql,"No Response");
            }
        }
        //*********************************************************************************************
        //* Get ISSUE KEY in the JSON
        //*********************************************************************************************
        //string issue_key = lb.clsJira.get_issue_key_for_ResponseBody(sbResponseBody);
        public static string get_issue_key_for_ResponseBody(string p_ResponseBody)
        {
            string m_method = "clsJira.get_issue_key_for_ResponseBody";
            string issue_key = "";
            try
            {
                p_ResponseBody = p_ResponseBody.Replace("{", "");
                p_ResponseBody = p_ResponseBody.Replace("}", "");
                p_ResponseBody = p_ResponseBody.Replace("'", "");
                string[] attributes = p_ResponseBody.ToString().Split(',');
                foreach (var attribute in attributes)
                {
                    int index1 = attribute.IndexOf("key");
                    if (index1 > -1)
                    {
                        string s_attribute = attribute.Replace("key:", "");
                        issue_key = s_attribute;
                    }
                }
                return issue_key;

            }
            catch (Exception ex)
            {
                lb.clsLogger.WriteLog("Error in : " + m_method + " : " + ex.Message);
                return issue_key;
            }

        }
        //*********************************************************************************************
        //* Get Last Date change for the stories
        //*********************************************************************************************
        //status = lb.clsJira.get_last_change_date_for_ResponseBody(sbResponseBody);
        public static string get_last_change_date_for_ResponseBody(Chilkat.StringBuilder sbResponseBody)
        {
            string m_method = "clsJira.get_last_change_date_for_ResponseBody";
            string status = "";
            string last_date_changed_on_story = "";
            string updated_on = "";
            string a_stat = "";
            int count_i = 0;
            int i = 0;
            int new_irow = 0;
            int irow_to_update = 0;
            string sval = "";
            bool found_status = false;
            string[,] a_status = new string[10, 2];
            try
            {
                Chilkat.JsonObject jsonResponse = new Chilkat.JsonObject();
                jsonResponse.LoadSb(sbResponseBody);
                count_i = jsonResponse.SizeOfArray("issues");
                last_date_changed_on_story = "2019/0/01";
                while (i < count_i)
                {
                    jsonResponse.I = i;
                    updated_on = jsonResponse.StringOf("issues[i].fields.updated");
                    updated_on = updated_on.Substring(0, 10);
                    updated_on = updated_on.Replace('-', '/');
                    DateTime dt_updated_on = DateTime.Parse(updated_on);
                    DateTime dt_last_date_changed_on_story = DateTime.Parse(last_date_changed_on_story);
                    if (dt_updated_on > dt_last_date_changed_on_story)
                    {
                        last_date_changed_on_story = updated_on;
                    }
                    found_status = false;
                    irow_to_update = 0;
                    i = i + 1;
                }
                return last_date_changed_on_story;

            }
            catch (Exception ex)
            {
                lb.clsLogger.WriteLog("Error in : " + m_method + " : " + ex.Message);
                return last_date_changed_on_story;
            }

        }
        //*********************************************************************************************
        //* Get User Strorie Point for EPIC
        //*********************************************************************************************
        //story_points = lb.clsJira.get_user_story_points_from_Epic(sbResponseBody);
        public static string[,] get_user_story_points_from_Epic(Chilkat.StringBuilder sbResponseBody)
        {
            string m_method = "clsJira.get_user_story_points_from_Epic";
            string status = "";
            string ret_status = "";
            string a_stat = "";
            int count_i = 0;
            int i = 0;
            int new_irow = 0;
            int irow_to_update = 0;
            string sval = "";
            bool found_status = false;
            string[,] a_status = new string[10, 2];
            try
            {
                Chilkat.JsonObject jsonResponse = new Chilkat.JsonObject();
                jsonResponse.LoadSb(sbResponseBody);
                count_i = jsonResponse.SizeOfArray("issues");

                while (i < count_i)
                {
                    jsonResponse.I = i;
                    status = jsonResponse.StringOf("issues[i].fields.status.name");
                    Int32 i_story_points = jsonResponse.IntOf("issues[i].fields.customfield_10006");
                    found_status = false;
                    irow_to_update = 0;
                    for (int k = 0; k < a_status.GetLength(0); k++)
                    {
                        a_stat = a_status[k, 0];
                        if (a_stat == status)
                        {
                            found_status = true;
                            irow_to_update = k;
                            sval = a_status[k, 1];
                        }
                        //for (int l = 0; l < 2; l++)
                        //{
                        //    Console.Write(a_status[k, l] + " ");
                        //}
                        //fieldskey = jsonResponse.StringOf("fields.issues[i].key");
                        //fieldsurl = jsonResponse.StringOf("fields.issues.self");
                        //      
                    }
                    if (found_status == true)
                    // ADD 1 to previous count
                    {
                        a_status[irow_to_update, 0] = status;
                        int ival = Int32.Parse(a_status[irow_to_update, 1]) + i_story_points;
                        a_status[irow_to_update, 1] = ival.ToString();
                    }
                    else
                    // Create New row
                    {
                        a_status[new_irow, 0] = status;
                        a_status[new_irow, 1] = i_story_points.ToString();
                        new_irow = new_irow + 1;
                    }
                    i = i + 1;
                }
                return a_status;
            }
            catch (Exception ex)
            {
                lb.clsLogger.WriteLog("Error in : " + m_method + " : " + ex.Message);
                return a_status;
            }
        }
        //*********************************************************************************************
        //* Get User Strories for EPIC
        //*********************************************************************************************
        //status = lb.clsJira.get_status_count_from_Epic(sbResponseBody);
        public static string[,] get_status_count_from_Epic(Chilkat.StringBuilder sbResponseBody)
        {
            string m_method = "clsJira.get_status_count_from_Epic";
            string status = "";
            string ret_status = "";
            string a_stat = "";
            int count_i = 0;
            int i = 0;
            int new_irow = 0;
            int irow_to_update = 0;
            string sval = "";
            bool found_status = false;
            string[,] a_status = new string[10, 2];
            try
            {
                Chilkat.JsonObject jsonResponse = new Chilkat.JsonObject();
                jsonResponse.LoadSb(sbResponseBody);
                count_i = jsonResponse.SizeOfArray("issues");

                while (i < count_i)
                {
                    jsonResponse.I = i;
                    status = jsonResponse.StringOf("issues[i].fields.status.name");
                    Int32 i_story_points = jsonResponse.IntOf("issues[i].fields.customfield_10006");
                    found_status = false;
                    irow_to_update = 0;
                    for (int k = 0; k < a_status.GetLength(0); k++)
                    {
                        a_stat = a_status[k, 0];
                        if (a_stat == status)
                        {
                            found_status = true;
                            irow_to_update = k;
                            sval = a_status[k, 1];
                        }
                        //for (int l = 0; l < 2; l++)
                        //{
                        //    Console.Write(a_status[k, l] + " ");
                        //}
                        //fieldskey = jsonResponse.StringOf("fields.issues[i].key");
                        //fieldsurl = jsonResponse.StringOf("fields.issues.self");
                        //      
                    }
                    if (found_status == true)
                    // ADD 1 to previous count
                    {
                        a_status[irow_to_update, 0] = status;
                        int ival = Int32.Parse(a_status[irow_to_update, 1]) + 1;
                        a_status[irow_to_update, 1] = ival.ToString();
                    }
                    else
                    // Create New row
                    {
                        a_status[new_irow, 0] = status;
                        a_status[new_irow, 1] = "1";
                        new_irow = new_irow + 1;
                    }
                    i = i + 1;
                }
                return a_status;
            }
            catch (Exception ex)
            {
                lb.clsLogger.WriteLog("Error in : " + m_method + " : " + ex.Message);
                return a_status;
            }
        }
        //*********************************************************************************************
        //* Get User Strories for EPIC
        //*********************************************************************************************
        //sbResponseBody = lb.clsJira.get_string_builder_for_issue(epic);
        public static Chilkat.StringBuilder get_string_builder_for_issue(string p_issue)
        {
            string m_method = "clsJira.get_string_builder_for_issue";
            Chilkat.StringBuilder sbResponseBody = new Chilkat.StringBuilder();
            string hostname = lb.clsParameters.get_hostname();
            try
            {
                string jira_url = "";
                //jira_url = "/jira/rest/api/2/search?jql=%27Epic%20Link%27=%27EXXD-1661%27";
                //EXXD-1801
                //jira_url = "/jira/rest/api/2/search?jql=%27Epic%20Link%27=%27EXXD-1801%27";
                // jira_url = "/jira/rest/api/2/search?jql=%27Epic%20Link%27=%27" + epic + "%27";
                //jira_url = "/jira/rest/api/2/search?jql=%27Epic%20Link%27=%27EXXD-2055%27";
                jira_url = "/jira/rest/api/2/search?jql='key' = '" + p_issue + "'";

                string url = make_url_to_rest_api_friendly(jira_url);
                lb.clsLogger.WriteLog("Start Log *************************************");
                Chilkat.Global glob = new Chilkat.Global();
                bool success = glob.UnlockBundle("KBSDTS.CB1092022_zTbLSG7a86mb");
                //Log -- Unlock
                //
                if (success != true)
                {
                    lb.clsLogger.WriteLog("Error : UnlockBundle");
                    return sbResponseBody;
                }

                int status = glob.UnlockStatus;
                if (status == 2)
                {
                    //Debug.WriteLine("Unlocked using purchased unlock code.");
                }
                else
                {
                    //Debug.WriteLine("Unlocked in trial mode.");
                }
                Chilkat.Rest rest = new Chilkat.Rest();
                //bool success;

                bool bTls = true;
                int port = 443;
                bool bAutoReconnect = true;
                success = rest.Connect(hostname, port, bTls, bAutoReconnect);
                if (success != true)
                {
                    lb.clsLogger.WriteLog("Error : With Connection : hostname (" + hostname + "), port (" + port + ")");
                    return sbResponseBody;
                }
                string m_uid = lb.clsConfigParameters.get_uid();
                string m_pw = lb.clsConfigParameters.get_pw();
                rest.SetAuthBasic(m_uid, m_pw);

                success = rest.FullRequestNoBodySb("GET", url, sbResponseBody);
                Chilkat.JsonObject jsonResponse = new Chilkat.JsonObject();
                string txtResult = sbResponseBody.GetAsString();
                jsonResponse.LoadSb(sbResponseBody);
                return sbResponseBody;
            }
            catch (Exception ex)
            {
                lb.clsLogger.WriteLog("Error in : " + m_method + " : " + ex.Message);
                return sbResponseBody;
            }
        }
        //*********************************************************************************************
        //* Get User Strories for EPIC
        //*********************************************************************************************
        //sbResponseBody = lb.clsJira.get_user_stories_for_epic(epic);
        public static Chilkat.StringBuilder get_user_stories_for_epic(string epic)
        {
            string m_method = "clsJira.get_user_stories_for_epic";
            Chilkat.StringBuilder sbResponseBody = new Chilkat.StringBuilder();
            string hostname = lb.clsParameters.get_hostname();
            try
            {
                string jira_url = "";
                //jira_url = "/jira/rest/api/2/search?jql=%27Epic%20Link%27=%27EXXD-1661%27";
                //EXXD-1801
                //jira_url = "/jira/rest/api/2/search?jql=%27Epic%20Link%27=%27EXXD-1801%27";
                //*
                //* !!!!!!!!!!!!!!!!!!! this line was last
                //*
                jira_url = "/jira/rest/api/2/search?jql=%27Epic%20Link%27=%27" + epic + "%27";
                //*
                //* !!!!!!!!!!!!!!!!!!! this line was last
                //*

                //jira_url = "/jira/rest/api/2/search?jql=%27Epic%20Link%27=%27EXXD-2055%27";
                //                jira_url = "/jira/rest/api/2/search?jql='key' = %27" + epic + "%27";

                string url = make_url_to_rest_api_friendly(jira_url);
                // lb.clsLogger.WriteLog("Start Log *************************************");
                Chilkat.Global glob = new Chilkat.Global();
                bool success = glob.UnlockBundle("KBSDTS.CB1092022_zTbLSG7a86mb");
                //Log -- Unlock
                //
                if (success != true)
                {
                    lb.clsLogger.WriteLog("Error : UnlockBundle");
                    return sbResponseBody;
                }

                int status = glob.UnlockStatus;
                if (status == 2)
                {
                    //Debug.WriteLine("Unlocked using purchased unlock code.");
                }
                else
                {
                    //Debug.WriteLine("Unlocked in trial mode.");
                }
                Chilkat.Rest rest = new Chilkat.Rest();
                //bool success;

                bool bTls = true;
                int port = 443;
                bool bAutoReconnect = true;
                success = rest.Connect(hostname, port, bTls, bAutoReconnect);
                if (success != true)
                {
                    //lb.clsLogger.WriteLog("Error : With Connection : hostname (" + hostname + "), port (" + port + ")");
                    return sbResponseBody;
                }
                string m_uid = lb.clsConfigParameters.get_uid();
                string m_pw = lb.clsConfigParameters.get_pw();
                rest.SetAuthBasic(m_uid, m_pw);
                success = rest.FullRequestNoBodySb("GET", url, sbResponseBody);
                Chilkat.JsonObject jsonResponse = new Chilkat.JsonObject();
                string txtResult = sbResponseBody.GetAsString();
                jsonResponse.LoadSb(sbResponseBody);
                return sbResponseBody;
            }
            catch (Exception ex)
            {
                //        lb.clsLogger.WriteLog("Error in : " + m_method + " : " + ex.Message);
                return sbResponseBody;
            }
        }

        //*********************************************************************************************
        //* Get JSON object for the Query
        //*********************************************************************************************
        //sbResponseBody = lb.clsJira.get_json_obj_for_url(jira_url);
        public static Chilkat.StringBuilder get_json_obj_for_url(string jira_url)
        {
            string m_method = "clsJira.get_json_obj_for_url";
            Chilkat.StringBuilder sbResponseBody = new Chilkat.StringBuilder();
            string hostname = lb.clsParameters.get_hostname();
            try
            {
                lb.clsLogger.WriteLog("Start Log *************************************");
                Chilkat.Global glob = new Chilkat.Global();
                bool success = glob.UnlockBundle("KBSDTS.CB1092022_zTbLSG7a86mb");
                //Log -- Unlock
                //
                if (success != true)
                {
                    lb.clsLogger.WriteLog("Error : UnlockBundle");
                    return sbResponseBody;
                }

                int status = glob.UnlockStatus;
                if (status == 2)
                {
                    //Debug.WriteLine("Unlocked using purchased unlock code.");
                }
                else
                {
                    //Debug.WriteLine("Unlocked in trial mode.");
                }
                Chilkat.Rest rest = new Chilkat.Rest();
                //bool success;

                bool bTls = true;
                int port = 443;
                bool bAutoReconnect = true;
                success = rest.Connect(hostname, port, bTls, bAutoReconnect);
                if (success != true)
                {
                    lb.clsLogger.WriteLog("Error : With Connection : hostname (" + hostname + "), port (" + port + ")");
                    return sbResponseBody;
                }
                //
                string m_uid = lb.clsConfigParameters.get_uid();
                string m_pw = lb.clsConfigParameters.get_pw();
                rest.SetAuthBasic(m_uid, m_pw);
                //
                success = rest.FullRequestNoBodySb("GET", jira_url, sbResponseBody);
                Chilkat.JsonObject jsonResponse = new Chilkat.JsonObject();
                jsonResponse.LoadSb(sbResponseBody);
                return sbResponseBody;
            }
            catch (Exception ex)
            {
                lb.clsLogger.WriteLog("Error in : " + m_method + " : " + ex.Message);
                return sbResponseBody;
            }
        }

        //*********************************************************************************************
        //* Get URL for EPIC
        //*********************************************************************************************
        public static string get_url_for_epic(string project_id, string epic_id)
        {
            string m_method = "clsJira.get_url_for_epic";
            string jira_url = "";
            try
            {
                //string jira_url = "/jira/rest/agile/1.0/board/{board_id}/sprint";
                jira_url = "/jira/rest/api/2/search?jql=project = {project_id} AND issuetype = Epic AND \"Epic Link\" = {epic_id}";
                //
                jira_url = jira_url.Replace("{project_id}", project_id);
                jira_url = jira_url.Replace("{epic_id}", epic_id);
                // jira_url = fixJira_url(jira_url);
            }
            catch (Exception ex)
            {
                lb.clsLogger.WriteLog("Error in : " + m_method + " : " + ex.Message);
            }

            return jira_url;
        }
        //*********************************************************************************************
        //* Get URL for EPIC from Epic List
        //*********************************************************************************************
        //string jira_url = get_url_from_url( project_id,  url);
        public static string get_url_from_url(string project_id, string url)
        {
            string m_method = "clsJira.get_url_from_url";
            string jira_url = "";
            try
            {
                //string jira_url = "/jira/rest/agile/1.0/board/{board_id}/sprint";
                jira_url = "/jira/rest/api/2/search?jql=project={project_id} AND ({url})";
                //string jira_url = "/jira/rest/api/2/search?jql=({url})";
                //
                jira_url = jira_url.Replace("{project_id}", project_id);
                jira_url = jira_url.Replace("{url}", url);
                jira_url = make_url_to_rest_api_friendly(jira_url);
            }
            catch (Exception ex)
            {
                lb.clsLogger.WriteLog("Error in : " + m_method + " : " + ex.Message);
            }

            return jira_url;
        }
        //*********************************************************************************************
        //* Get URL for EPIC from Epic List
        //*********************************************************************************************
        //string jira_url = get_url_for_epic_from_epic_id_list( p_issue_id_list);
        public static string get_url_for_epic_from_epic_id_list(string p_issue_id_list)
        {
            string m_method = "clsJira.get_url_for_epic_from_epic_id_list";
            string jira_url = "";
            try
            {
                //string jira_url = "/jira/rest/agile/1.0/board/{board_id}/sprint";
                jira_url = "/jira/rest/api/2/search?jql=id in({p_issue_id_list})";
                //
                jira_url = jira_url.Replace("{p_issue_id_list}", p_issue_id_list);
                jira_url = make_url_to_rest_api_friendly(jira_url);
            }
            catch (Exception ex)
            {
                lb.clsLogger.WriteLog("Error in : " + m_method + " : " + ex.Message);
            }

            return jira_url;
        }
        //*********************************************************************************************
        //* Get URL for EPIC from Epic List
        //*********************************************************************************************
        //string jira_url = get_url_for_epic_from_epic_list( p_issue_list);
        public static string get_url_for_epic_from_epic_list(string p_issue_list)
        {
            string m_method = "clsJira.get_url_for_epic_from_epic_list";
            string jira_url = "";
            try
            {
                //string jira_url = "/jira/rest/agile/1.0/board/{board_id}/sprint";
                jira_url = "/jira/rest/api/2/search?jql=key in({p_issue_list})";
                //
                jira_url = jira_url.Replace("{p_issue_list}", p_issue_list);
                jira_url = make_url_to_rest_api_friendly(jira_url);
            }
            catch (Exception ex)
            {
                lb.clsLogger.WriteLog("Error in : " + m_method + " : " + ex.Message);
            }

            return jira_url;
        }
        //*********************************************************************************************
        //* Get URL for EPIC from Epic List
        //*********************************************************************************************
        //string jira_url = get_url_for_epic_from_epic_list_and_project( project_id,  epic_list);
        public static string get_url_for_epic_from_epic_list_and_project(string project_id, string epic_list)
        {
            string m_method = "clsJira.get_url_for_epic_from_epic_list_and_project";
            string jira_url = "";
            try
            {
                //string jira_url = "/jira/rest/agile/1.0/board/{board_id}/sprint";
                jira_url = "/jira/rest/api/2/search?jql=project={project_id} AND key in({epic_list})";
                //
                jira_url = jira_url.Replace("{project_id}", project_id);
                jira_url = jira_url.Replace("{epic_list}", epic_list);
                jira_url = make_url_to_rest_api_friendly(jira_url);
            }
            catch (Exception ex)
            {
                lb.clsLogger.WriteLog("Error in : " + m_method + " : " + ex.Message);
            }
            return jira_url;

        }

        //*
        //**************************************************************
        //*
        //*    Add Acceptance to a User Story
        //*
        //**************************************************************
        //* string url = fixJira_url(jira_url);
        public static Int32 add_an_acceptance(string p_issue_key, DataTable dtAcceptance, string p_acceptance)
        {
            string m_method = "clsJira.add_an_acceptance";
            Int32 rc = 0;
            try
            {
                Chilkat.JsonObject m_json = new Chilkat.JsonObject();
                m_json = prepare_acceptance_json(m_json, p_issue_key, dtAcceptance, p_acceptance);
                add_acceptance_criteria(p_issue_key, m_json);
                return rc;
            }
            catch (Exception ex)
            {
                return rc;
                lb.clsLogger.WriteLog("Error in : " + m_method + " : " + ex.Message);
            }
        }
        //*
        //**************************************************************
        //*
        //*    Add Acceptance to a User Story
        //*
        //**************************************************************
        //* string m_acceptance_json = prepare_acceptance_json(p_issue_key,dtAcceptance,p_acceptance);
        public static Chilkat.JsonObject prepare_acceptance_json(Chilkat.JsonObject p_json, string p_issue_key, DataTable dtAcceptance, string p_acceptance)
        {
            string m_method = "clsJira.prepare_acceptance_json";
            string m_name = "";
            string m_acceptance = "";
            bool m_checked = false;
            string field_name = "";
            string field_checked = "";
            Int32 row_number = 0;
            bool success;
            try
            {
                p_json = lb.clsJiraField.add_project_key(p_json, "BDINGZA"); // Project Key
                //*
                //* First get all the Current Acceptance
                //*
                foreach (DataRow row in dtAcceptance.Rows)
                {
                    row_number = row_number + 1;
                    // Add the array for Price
                    //success = json.AddArrayAt(-1, "Price");
                    //Chilkat.JsonArray aPrice = json.ArrayAt(json.Size - 1);
                    success = p_json.AddArrayAt(-1, "customfield_11100");
                    Chilkat.JsonArray aAcceptance = p_json.ArrayAt(p_json.Size - 1);

                    //// Entry entry in aPrice will be a JSON object.

                    //// Append a new/empty ojbect to the end of the aPrice array.
                    //success = aPrice.AddObjectAt(-1);
                    success = aAcceptance.AddObjectAt(-1);
                    //// Get the object that was just appended.
                    //Chilkat.JsonObject priceObj = aPrice.ObjectAt(aPrice.Size - 1);
                    //success = priceObj.AddStringAt(-1, "type", "Hardcover");
                    //success = priceObj.AddNumberAt(-1, "price", "16.65");
                    Chilkat.JsonObject AcceptanceObj = aAcceptance.ObjectAt(aAcceptance.Size - 1);
                    success = AcceptanceObj.AddStringAt(-1, "type", "Hardcover");
                    success = AcceptanceObj.AddNumberAt(-1, "price", "16.65");
                    ////*
                    ////* Acceptance (NAME)
                    ////*

                    //m_name = row["name"].ToString();
                    ////field_name = "fields.customfield_11100[xxx].name";
                    //field_name = "customfield_11100[xxx].name";
                    //field_name = field_name.Replace("xxx", row_number.ToString());
                    //p_json.AddStringAt(-1,field_name, m_name);
                    ////*
                    ////* Acceptance (CHECKED) ( ONLY add when TRUE )
                    ////*
                    //m_checked = Convert.ToBoolean(row["checked"]);
                    //if(m_checked==true)
                    //{
                    //    //field_checked = "fields.customfield_11100[xxx].checked";
                    //    field_checked = "customfield_11100[xxx].checked";
                    //    field_checked = field_checked.Replace("xxx", row_number.ToString());
                    //    //p_json.UpdateBool(field_checked, m_checked);
                    //    p_json.AddBoolAt(-1, field_checked, m_checked);
                    //}


                }
                //*
                //* Now Add the NEW Acceptance
                //*
                row_number = row_number + 1;
                ////field_name = "fields.customfield_11100[xxx].name";
                //field_name = "customfield_11100[xxx].name";
                //field_name = field_name.Replace("xxx", row_number.ToString());
                ////p_json.UpdateString(field_name, p_acceptance);
                //p_json.AddStringAt(-1, field_name, p_acceptance);

                return p_json;
            }
            catch (Exception ex)
            {
                return p_json;
                lb.clsLogger.WriteLog("Error in : " + m_method + " : " + ex.Message);
            }
        }
        //*********************************************************************************************
        //* Get Last Date change for the stories
        //*********************************************************************************************
        //status = lb.clsJira.get_last_change_date_for_ResponseBody(sbResponseBody);
        public static void add_acceptance_criteria(string p_project, Chilkat.JsonObject p_json)
        {
            string m_method = "clsJira.add_acceptance_criteria";
            string return_epic = "";
            Chilkat.JsonObject m_json = p_json;
            try
            {
                string hostname = lb.clsParameters.get_hostname();
                Chilkat.Rest rest = new Chilkat.Rest();
                bool success;

                //  URL: https://atc-int.bmwgroup.net/rest/api/2/issue
                bool bTls = true;
                int port = 443;
                bool bAutoReconnect = true;
                success = rest.Connect(hostname, port, bTls, bAutoReconnect);
                if (success != true)
                {

                }
                //rest.SetAuthBasic("jira@example.com", "JIRA_API_TOKEN");
                //
                string m_uid = lb.clsConfigParameters.get_uid();
                string m_pw = lb.clsConfigParameters.get_pw();
                rest.SetAuthBasic(m_uid, m_pw);
                //
                //m_json = lb.clsJiraField.add_acceptance_criteria_customfield_11100_child(m_json, dtAcceptance);
                Chilkat.StringBuilder sbRequestBody = new Chilkat.StringBuilder();
                Chilkat.StringBuilder sbResponseBody = new Chilkat.StringBuilder();
                p_json.EmitSb(sbRequestBody);
                rest.AddHeader("Content-Type", "application/json");
                rest.AddHeader("Accept", "application/json");
                //success = rest.FullRequestSb("POST", "/jira/rest/api/2/AOS", sbRequestBody, sbResponseBody);
                //success = rest.FullRequestSb("POST", "/jira/rest/api/2/BDASINGZA", sbRequestBody, sbResponseBody);
                string url = "/jira/rest/api/2/issue/{project}";
                url = url.Replace("{project}", p_project);
                success = rest.FullRequestSb("PUT", url, sbRequestBody, sbResponseBody);
                if (success != true)
                {
                    //Debug.WriteLine(rest.LastErrorText);
                    return;
                }
            }
            catch (Exception ex)
            {
                lb.clsLogger.WriteLog("Error in : " + m_method + " : " + ex.Message);
            }
        }
        //*********************************************************************************************
        //* Get URL for ISSUE from ISSUE List (EXXD-1684,EXXD-1683)
        //*********************************************************************************************
        //string jira_url = lb.clsJira.get_url_for_issue_list(issue_list);
        public static string get_url_for_issue_list(string p_issue_list)
        {
            string m_method = "clsJira.get_url_for_issue_list";
            string jira_url = "";
            try
            {
                //string jira_url = "/jira/rest/agile/1.0/board/{board_id}/sprint";
                jira_url = "/jira/rest/api/2/search?jql=key in({p_issue_list})";
                //
                jira_url = jira_url.Replace("{p_issue_list}", p_issue_list);
                jira_url = make_url_to_rest_api_friendly(jira_url);
            }
            catch (Exception ex)
            {
                lb.clsLogger.WriteLog("Error in : " + m_method + " : " + ex.Message);
            }
            return jira_url;
        }
        //*********************************************************************************************
        //* Get URL for ISSUE from ISSUE List (EXXD-1684,EXXD-1683)
        //*********************************************************************************************
        //Chilkat.StringBuilder sbResponseBody = lb.clsJira.get_issue_json(issue_list);
        public static Chilkat.StringBuilder get_issue_json(string p_m_issue)
        {
            string m_method = "clsJira.get_issue_json";
            Chilkat.StringBuilder sbResponseBody;
            try
            {
                string jira_url = lb.clsJira.get_url_for_issue_list(p_m_issue);
                sbResponseBody = lb.clsJira.get_json_obj_for_url(jira_url);
                return sbResponseBody;

            }
            catch (Exception ex)
            {
                lb.clsLogger.WriteLog("Error in : " + m_method + " : " + ex.Message);
                sbResponseBody = null;
                return sbResponseBody;
            }
        }
        //*
        //**************************************************************
        //*
        //*    FIX JIRA SPACES
        //*
        //**************************************************************
        //* string url = make_url_to_rest_api_friendly(jira_url);
        public static string make_url_to_rest_api_friendly(string jira_url)
        {
            string m_method = "clsJira.make_url_to_rest_api_friendly";
            try
            {
                //https://www.w3schools.com/tags/ref_urlencode.ASP
                jira_url = jira_url.Replace(" ", "%20"); // Space
                jira_url = jira_url.Replace("=", "%3D"); // Equal;"="
                jira_url = jira_url.Replace("jql%3D", "jql="); // fix the JQL =
            }
            catch (Exception ex)
            {
                lb.clsLogger.WriteLog("Error in : " + m_method + " : " + ex.Message);
            }

            return jira_url;
        }
        //*
        //**************************************************************
        //*
        //*    FIX JIRA SPACES
        //*
        //**************************************************************
        //* string url = make_url_to_jira_friendly(jira_url);
        public static string make_url_to_jira_friendly(string jira_url)
        {
            string m_method = "clsJira.make_url_to_jira_friendly";
            try
            {
                //https://www.w3schools.com/tags/ref_urlencode.ASP
                jira_url = jira_url.Replace("%20", " "); // Space
                jira_url = jira_url.Replace("%3D", "="); // Equal;"="
                jira_url = jira_url.Replace("jql%3D", "jql="); // fix the JQL =
            }
            catch (Exception ex)
            {
                lb.clsLogger.WriteLog("Error in : " + m_method + " : " + ex.Message);
            }

            return jira_url;
        }
        //*
        //*******************************************************************************
        //*
        //*    Save sbResponseBody JSON
        //*
        //*******************************************************************************
        //*string sFull_Path = save_json_for_all_stories_linked_to_epic(hostname,epic);
        public static string save_sbResponseBody_json(string sFull_Path, Chilkat.StringBuilder sbResponseBody)
        {
            string m_method = "frmEpic_to_Story_Creator.save_sbResponseBody_json";
            try
            {
                if (sbResponseBody.ToString().Trim().Length > -1)
                {
                    lb.clsFile.write_text_to_file_by_type("JSON", sFull_Path, sbResponseBody.GetAsString(), false);
                    if (sFull_Path.Contains(@"\7\"))
                    {

                    }
                    if (sFull_Path.Contains(@"\ItO\"))
                    {

                    }
                }
                else
                {

                }
                return sFull_Path;
            }
            catch (Exception ex)
            {
                lb.clsLogger.WriteLog("Error in : " + m_method + " : " + ex.Message);
                return sFull_Path;
            }
        }
        //*********************************************************************************************
        //* Get JSON object for the Query
        //*********************************************************************************************
        //sbResponseBody = lb.clsJira.get_json_full_obj_for_url(jira_url);
        public static Chilkat.StringBuilder get_json_full_obj_for_url(string jira_url, string sFull_Path)
        {
            string m_method = "clsJira.get_json_full_obj_for_url";
            Chilkat.StringBuilder sbResponseBody = new Chilkat.StringBuilder();
            Chilkat.StringBuilder sbResponseBody_temp = new Chilkat.StringBuilder();
            string hostname = lb.clsParameters.get_hostname();
            Int32 m_startat = 0;
            string sb_responsebody_clean = "";
            string str_responsebody = "";
            try
            {
                sbResponseBody_temp = lb.clsJira.get_json_obj_for_url(jira_url);
                Int32 m_issue_total = lb.clsResponseBody.get_total_issues(sbResponseBody_temp);
                if (m_issue_total < 51)
                {
                    sbResponseBody = sbResponseBody_temp;
                    sFull_Path = save_sbResponseBody_json(sFull_Path, sbResponseBody);
                }
                else
                {
                    int m_loops = m_issue_total / 50;
                    m_loops = m_loops + 1;
                    //sb_responsebody_clean = sbResponseBody_temp.GetAsString();
                    sb_responsebody_clean = lb.clsJira.get_clean_jira_json(sbResponseBody_temp);
                    str_responsebody = sb_responsebody_clean;
                    // Only start at 50 ( First read was done on line ONE of THIS CODE
                    for (int i = 1; i < m_loops; i++)
                    {
                        //50,100 (START)
                        m_startat = i * 50;
                        string jira_url_temp = jira_url + "&startAt=" + m_startat;
                        sbResponseBody_temp = lb.clsJira.get_json_obj_for_url(jira_url_temp);
                        sb_responsebody_clean = lb.clsJira.get_clean_jira_json(sbResponseBody_temp);
                        str_responsebody = str_responsebody + "," + sb_responsebody_clean;
                    }
                    string str_result_qty = "''expand'':''schema,names'',''startAt'':0,''maxResults'':50,''total'':30,''issues''";
                    str_result_qty = str_result_qty.Replace("''","\"");
                    str_responsebody = "{" + str_result_qty+ ":[" + str_responsebody + "]}";
                    sbResponseBody.Append(str_responsebody);
                    sFull_Path = save_sbResponseBody_json(sFull_Path, sbResponseBody);

                }
                return sbResponseBody;
            }
            catch (Exception ex)
            {
                lb.clsLogger.WriteLog("Error in : " + m_method + " : " + ex.Message);
                return sbResponseBody;
            }
        }
        //*********************************************************************************************
        //* Get JSON object for the Query
        //*********************************************************************************************
        //sb_responsebody_clean = lb.clsJira.get_clean_jira_json(sbResponseBody);
        public static string get_clean_jira_json(Chilkat.StringBuilder sbResponseBody)
        {
            string m_method = "clsJira.get_clean_jira_json";
            Int32 m_startat = 0;
            string sb_responsebody_clean = "";
            try
            {
                sb_responsebody_clean = sbResponseBody.GetAsString();
                // Remove string In Front
                int index1 = sb_responsebody_clean.IndexOf(":[");
                sb_responsebody_clean = sb_responsebody_clean.Substring(index1+2);
                // Remove string at End
                index1 = sb_responsebody_clean.LastIndexOf("]");
                sb_responsebody_clean = sb_responsebody_clean.Substring(0, index1);
                return sb_responsebody_clean;
            }
            catch (Exception ex)
            {
                lb.clsLogger.WriteLog("Error in : " + m_method + " : " + ex.Message);
                return sb_responsebody_clean;
            }
        }
        //*********************************************************************************************
        //* Check EPIC Priority vs Story Priority
        //*********************************************************************************************
        //(string,string) (m_have_error,m_error) = lb.clsJira.check_epic_prio_vs_child_prio(sbResponseBody);
        public static (string,string) check_epic_prio_vs_child_prio(string p_epic_prio, Int32 p_PrioLow, Int32 p_PrioMedium, Int32 p_PrioHigh, Int32 p_PrioCritical)
        {
            string m_method = "clsJira.check_epic_prio_vs_child_prio";
            string m_have_error = "";
            string m_error = "";
            try
            {
                if(p_PrioLow==0 && p_PrioMedium==0 && p_PrioHigh==0 && p_PrioCritical==0)
                {
                    m_have_error = "X";
                    m_error = "There is no STORY linked to EPIC";
                }
                if(m_have_error != "X")
                {
                    switch (p_epic_prio)
                    {
                        case "Critical":
                            break;
                        case "High":
                            break;
                        case "Medium":
                            break;
                        case "Low":
                            break;
                        default:
                            // code block
                            break;
                    }
                }
                return (m_have_error, m_error);
            }
            catch (Exception ex)
            {
                lb.clsLogger.WriteLog("Error in : " + m_method + " : " + ex.Message);
                return (m_have_error, m_have_error);
            }
        }

    }
}
