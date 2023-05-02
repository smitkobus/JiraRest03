using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using Telerik.Windows.Documents.Spreadsheet.Expressions.Functions;

namespace lb
{
    class clsResponseBody
    {
        //*********************************************************************************************
        //* Get List of Issues !!!! EPIC !!!!
        //*********************************************************************************************
        //DataTable dt_issues = lb.clsResponseBody.get_issue_list_dt(sbResponseBody);
        public static DataTable get_issue_list_dt(Chilkat.StringBuilder sbResponseBody)
        {
            string m_method = "clsResponseBody.get_issue_list_dt";
            DataTable dt_issues = new DataTable();
            
            int count_i = 0;
            int i = 0;
            string m_label_list = "";
            try
            {
                dt_issues = lb.clsResponseBody.get_dt_add_columns_4_issue(dt_issues);
                Chilkat.JsonObject jsonResponse = new Chilkat.JsonObject();
                jsonResponse.LoadSb(sbResponseBody);
                count_i = jsonResponse.SizeOfArray("issues");
                while (i < count_i)
                {
                    jsonResponse.I = i;
                    DataRow dr_issues = dt_issues.NewRow();
                    dr_issues["id"] = jsonResponse.StringOf("issues[i].id");
                    dr_issues["key"] = jsonResponse.StringOf("issues[i].key");
                    dr_issues["summary"] = jsonResponse.StringOf("issues[i].fields.summary");
                    dr_issues["issuetype"] = jsonResponse.StringOf("issues[i].fields.issuetype.name");
                    dr_issues["status"] = jsonResponse.StringOf("issues[i].fields.status.name");
                    dr_issues["epic_prio"] = jsonResponse.StringOf("issues[i].fields.priority.name");
                    dr_issues["parent_key"] = jsonResponse.StringOf("issues[i].fields.customfield_11201");
                    dr_issues["parent_assignee_uid"] = jsonResponse.StringOf("issues[i].fields.assignee.name");
                    dr_issues["parent_assignee_short"] = lb.clsDB_user_store_custom.get_short_from_uid(dr_issues["parent_assignee_uid"].ToString());
                    dr_issues["parent_assignee_name"] = lb.clsDB_user_store_custom.get_displayname_from_uid(dr_issues["parent_assignee_uid"].ToString());
                    dr_issues["effort_size"] = jsonResponse.StringOf("issues[i].fields.customfield_10401.value");
                    dr_issues["story_points"] = jsonResponse.StringOf("issues[i].fields.customfield_10006");
                    if(jsonResponse.StringOf("issues[i].fields.duedate").ToString()=="null")
                    {
                        dr_issues["epic_due"] = "1900-01-01";
                    }
                    else
                    {
                        dr_issues["epic_due"] = jsonResponse.StringOf("issues[i].fields.duedate");
                    }
                    

                    //*
                    //* Labels
                    //*
                    m_label_list = jsonResponse.StringOf("issues[i].fields.labels");
                    m_label_list = m_label_list.Replace("[", "");
                    m_label_list = m_label_list.Replace("]", "");
                    string m_label01 = "";
                    string m_label02 = "";
                    string m_label03 = "";
                    string m_clean_label_list = "";
                    if (m_label_list.Length>0)
                    {
                        string[] labels = m_label_list.Split(',');
                        Array.Sort(labels);
                        foreach (string label in labels)
                        {
                            // The `state` variable takes on the value of an element in `states` and updates every iteration.
                            Console.WriteLine(label);
                            string m_label = label.Replace("\"", "");
                            if (!m_clean_label_list.Contains(m_label))
                            {
                                m_clean_label_list = m_clean_label_list + m_label + ";";
                            }
                               
                            if (m_label01=="")
                            {
                                m_label01 = m_label;
                            }
                            else
                            {
                                if (m_label02 == "")
                                {
                                    m_label02 = m_label;
                                }
                                else
                                {
                                    if (m_label03 == "")
                                    {
                                        m_label03 = m_label;
                                    }
                                }
                            }
                        }
                        dr_issues["label01"] = m_label01;
                        dr_issues["label02"] = m_label02;
                        

                    }
                    dr_issues["label_list"] = m_clean_label_list;
                    
                    dt_issues.Rows.Add(dr_issues);
                    i = i + 1;
                }
                return dt_issues;

            }
            catch (Exception ex)
            {
                lb.clsLogger.WriteLog("Error in : " + m_method + " : " + ex.Message);
                return dt_issues;
            }
        }
        //*********************************************************************************************
        //* Get List of Issues
        //*********************************************************************************************
        //Int32 m_issue_total = lb.clsResponseBody.get_total_issues(sbResponseBody);
        public static Int32 get_total_issues(Chilkat.StringBuilder sbResponseBody)
        {
            string m_method = "clsResponseBody.get_total_issues";
            Int32 m_issue_total = 0;
            try
            {
                Chilkat.JsonObject jsonResponse = new Chilkat.JsonObject();
                jsonResponse.LoadSb(sbResponseBody);
                m_issue_total = jsonResponse.IntOf("total");
                return m_issue_total;
            }
            catch (Exception ex)
            {
                lb.clsLogger.WriteLog("Error in : " + m_method + " : " + ex.Message);
                return m_issue_total;
            }
        }
        //*********************************************************************************************
        //* Get List of Issues
        //*********************************************************************************************
        //string m_issue_list = lb.clsResponseBody.get_issue_list(sbResponseBody);
        public static string get_issue_list(Chilkat.StringBuilder sbResponseBody)
        {
            string m_method = "clsResponseBody.get_issue_list";
            string status = "";
            string m_issue_list = "";
            string m_key = "";
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
                    m_key = jsonResponse.StringOf("issues[i].key");
                    m_issue_list = m_issue_list + m_key + ",";

                    i = i + 1;
                }
                return m_issue_list;

            }
            catch (Exception ex)
            {
                lb.clsLogger.WriteLog("Error in : " + m_method + " : " + ex.Message);
                return m_issue_list;
            }
        }
        //*********************************************************************************************
        //1) string m_comments = lb.clsResponseBody.get_comments_responsebody(sbResponseBody);
        //1) string m_issue_list = lb.clsResponseBody.get_issue_ResponseBody(sbResponseBody);
        //2) DataTable dt_acceptance = lb.clsResponseBody.get_acceptance_for_ResponseBody(sbResponseBody);
        //3) string m_summary = lb.clsResponseBody.get_summary_for_ResponseBody(sbResponseBody);
        //*********************************************************************************************
        //*********************************************************************************************
        //* Get List of Issues ID's
        //*********************************************************************************************
        //bool m_have_selected_acceptance = lb.clsResponseBody.check_if_user_story_have_selected_acceptance(sbResponseBody,m_issue_key);
        public static bool check_if_user_story_have_selected_acceptance(Chilkat.StringBuilder sbResponseBody,string p_issue_key)
        {
            string m_method = "clsResponseBody.check_if_user_story_have_selected_acceptance";
            bool m_have_selected_acceptance = false;
            Int32 count_i = 0;
            Int32 i = 0;
            Int32 count_j = 0;
            Int32 j = 0;
            string m_attribute = "";
            try
            {
                Chilkat.JsonObject jsonResponse = new Chilkat.JsonObject();
                jsonResponse.LoadSb(sbResponseBody);
                count_i = jsonResponse.SizeOfArray("issues");
                while (i < count_i)
                {
                    jsonResponse.I = i;
                    //*
                    //* Get File for User Stories to GET the ACCEPTANCE Criteria
                    //*
                    string attribute = "issues[i].key";
                    attribute = attribute.Replace("[i]", "[" + i + "]");
                    string value_key = jsonResponse.StringOf(attribute);
                    if(value_key== "BDASINGZA-4714")
                    {

                    }
                    if (value_key == p_issue_key)
                    {
                        //*
                        //* GET the ACCEPTANCE Criteria for the Selected Story
                        //*
                        attribute = "issues[i].fields.customfield_11100";
                        attribute = attribute.Replace("[i]", "[" + i + "]");
                        count_j = jsonResponse.SizeOfArray(attribute);
                        while (j < count_j)
                        {
                            jsonResponse.J = j;
                            //m_acceptance_total_cnt = m_acceptance_total_cnt + count_j;
                            //*
                            //* ID
                            //*
                            m_attribute = "issues[i].fields.customfield_11100[j].id";
                            string m_id = lb.clsJira.get_jsonResponse_StringOf(jsonResponse, m_attribute, i, j);
                            //*
                            //* Name
                            //*
                            m_attribute = "issues[i].fields.customfield_11100[j].name";
                            string m_name = lb.clsJira.get_jsonResponse_StringOf(jsonResponse, m_attribute, i, j);

                            //*
                            //* Checked
                            //*
                            m_attribute = "issues[i].fields.customfield_11100[j].checked";
                            bool m_checked = lb.clsJira.get_jsonResponse_BoolOf(jsonResponse, m_attribute, i, j);
                            if(m_checked==true)
                            {
                                m_have_selected_acceptance = true;
                            }
                            j = j + 1;
                        }
                    }
                    i = i + 1;                    
                }
                return m_have_selected_acceptance;

            }
            catch (Exception ex)
            {
                lb.clsLogger.WriteLog("Error in : " + m_method + " : " + ex.Message);
                return m_have_selected_acceptance;
            }
        }
        //*********************************************************************************************
        //* Get List of Issues ID's
        //*********************************************************************************************
        //string m_issue_id_list = lb.clsResponseBody.get_issue_id_list(sbResponseBody);
        public static string get_issue_id_list(Chilkat.StringBuilder sbResponseBody)
        {
            string m_method = "clsResponseBody.get_issue_id_list";
            string status = "";
            string m_issue_id_list = "";
            string m_id = "";
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
                    m_id = jsonResponse.StringOf("issues[i].id");
                    m_issue_id_list = m_issue_id_list + m_id + ",";

                    i = i + 1;
                }
                return m_issue_id_list;

            }
            catch (Exception ex)
            {
                lb.clsLogger.WriteLog("Error in : " + m_method + " : " + ex.Message);
                return m_issue_id_list;
            }
        }

        //*********************************************************************************************
        //* Get Comments for the List
        //*********************************************************************************************
        //string m_comments = lb.clsResponseBody.get_comments(sbResponseBody);
        public static string get_comments(Chilkat.StringBuilder sbResponseBody)
        {
            string m_method = "clsResponseBody.get_comments";
            string status = "";
            string m_issue_list = "";
            string m_key = "";
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
                    m_key = jsonResponse.StringOf("issues[i].key");
                    m_issue_list = m_issue_list + m_key + ",";

                    i = i + 1;
                }
                return m_issue_list;

            }
            catch (Exception ex)
            {
                lb.clsLogger.WriteLog("Error in : " + m_method + " : " + ex.Message);
                return m_issue_list;
            }
        }



        //*********************************************************************************************
        //* Get Acceptance Criterias for the ISSUE
        //*********************************************************************************************
        //DataTable dt_acceptance = lb.clsResponseBody.get_acceptance(sbResponseBody);
        public static DataTable get_acceptance(Chilkat.StringBuilder sbResponseBody, DataTable dt_acceptance)
        {
            string m_method = "clsResponseBody.get_acceptance";
            //DataTable dt_acceptance = new DataTable();
            Int32 i = 0;
            Int32 count_i = 0;
            Int32 j = 0;
            Int32 count_j = 0;
            string m_id = "";
            string m_name = "";
            bool m_checked = false;
            bool m_process_user_story = false;
            string m_attribute = "";
            string m_status = "";
            bool m_isHeader = false;
            string m_rank = "0";
            bool m_mandatory = true;
            try
            {
                Chilkat.JsonObject jsonResponse = new Chilkat.JsonObject();
                jsonResponse.LoadSb(sbResponseBody);

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
                    m_process_user_story = true;
                    if (m_process_user_story == true)
                    {
                        j = 0;

                        while (j < count_j)
                        {
                            jsonResponse.J = j;
                            //m_acceptance_total_cnt = m_acceptance_total_cnt + count_j;
                            //*
                            //* ID
                            //*
                            m_attribute = "issues[i].fields.customfield_11100[j].id";
                            m_id = lb.clsJira.get_jsonResponse_StringOf(jsonResponse, m_attribute, i, j);
                            //*
                            //* Name
                            //*
                            m_attribute = "issues[i].fields.customfield_11100[j].name";
                            m_name = lb.clsJira.get_jsonResponse_StringOf(jsonResponse, m_attribute, i, j);
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
                            DataRow dr = dt_acceptance.NewRow();
                            dr["id"] = m_id;
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
                            dt_acceptance.Rows.Add(dr);
                            j = j + 1;
                        }
                       
                    }
                    i = i + 1;
                }
                //m_summary = LittleBytes.Jira.FromJson.get_summary(jsonResponse);
                return dt_acceptance;

            }
            catch (Exception ex)
            {
                lb.clsLogger.WriteLog("Error in : " + m_method + " : " + ex.Message);
                return dt_acceptance;
            }
        }
    
        //*********************************************************************************************
        //* Get SUMMARY for the ISSUE
        //*********************************************************************************************
        //string m_summary = lb.clsResponseBody.get_summary(sbResponseBody);
        public static string get_summary(Chilkat.StringBuilder sbResponseBody)
        {
            string m_method = "clsResponseBody.get_summary";
            string m_summary = "";

            try
            {
                Chilkat.JsonObject jsonResponse = new Chilkat.JsonObject();
                jsonResponse.LoadSb(sbResponseBody);
                m_summary = jsonResponse.StringOf("issues[0].fields.summary");
                //m_summary = LittleBytes.Jira.FromJson.get_summary(jsonResponse);
                return m_summary;

            }
            catch (Exception ex)
            {
                lb.clsLogger.WriteLog("Error in : " + m_method + " : " + ex.Message);
                return m_summary;
            }
        }
        //*********************************************************************************************
        //* Get DESCRIPTION for the ISSUE
        //*********************************************************************************************
        //string m_description = lb.clsResponseBody.get_description(sbResponseBody);
        public static string get_description(Chilkat.StringBuilder sbResponseBody)
        {
            string m_method = "clsResponseBody.get_description";
            string m_description = "";

            try
            {
                Chilkat.JsonObject jsonResponse = new Chilkat.JsonObject();
                jsonResponse.LoadSb(sbResponseBody);
                m_description = jsonResponse.StringOf("issues[0].fields.description");
                //m_summary = LittleBytes.Jira.FromJson.get_summary(jsonResponse);
                return m_description;

            }
            catch (Exception ex)
            {
                lb.clsLogger.WriteLog("Error in : " + m_method + " : " + ex.Message);
                return m_description;
            }
        }
        //*********************************************************************************************
        //* Get Last Date change for the ISSUE
        //*********************************************************************************************
        //string last_date_changed_on_issue = lb.clsResponseBody.get_last_change_date(sbResponseBody);
        public static string get_last_change_date(Chilkat.StringBuilder sbResponseBody)
        {
            string m_method = "clsResponseBody.get_last_change_date";
            string status = "";
            string last_date_changed_on_issue = "";
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
                last_date_changed_on_issue = "1999/01/01";
                while (i < count_i)
                {
                    jsonResponse.I = i;
                    updated_on = jsonResponse.StringOf("issues[i].fields.updated");
                    updated_on = updated_on.Substring(0, 10);
                    updated_on = updated_on.Replace('-', '/');
                    DateTime dt_updated_on = DateTime.Parse(updated_on);
                    DateTime dt_last_date_changed_on_story = DateTime.Parse(last_date_changed_on_issue);
                    if (dt_updated_on > dt_last_date_changed_on_story)
                    {
                        last_date_changed_on_issue = updated_on;
                    }
                    found_status = false;
                    irow_to_update = 0;
                    i = i + 1;
                }
                return last_date_changed_on_issue;

            }
            catch (Exception ex)
            {
                lb.clsLogger.WriteLog("Error in : " + m_method + " : " + ex.Message);
                return last_date_changed_on_issue;
            }
        }
        //*********************************************************************************************
        //* Add Columns for Issue
        //*********************************************************************************************
        public static DataTable get_dt_add_columns_4_issue(DataTable p_dt)
        {
            string m_method = "clsResponseBody.get_dt_add_columns_4_issue";
            try
            {
                p_dt.Columns.Add("id");
                p_dt.Columns.Add("key");
                p_dt.Columns.Add("summary");
                p_dt.Columns.Add("issuetype");
                p_dt.Columns.Add("status");
                p_dt.Columns.Add("parent_key");
                p_dt.Columns.Add("epic_prio");
                p_dt.Columns.Add("parent_summary");
                p_dt.Columns.Add("parent_assignee_uid");
                p_dt.Columns.Add("parent_assignee_short");
                p_dt.Columns.Add("parent_assignee_name");
                p_dt.Columns.Add("label01");
                p_dt.Columns.Add("label02");
                p_dt.Columns.Add("label03");
                p_dt.Columns.Add("label_list");
                p_dt.Columns.Add("effort_size");
                p_dt.Columns.Add("story_points");
                p_dt.Columns.Add("epic_due");

                //*
                //* Checked
                //*
                //DataColumn colChecked = new DataColumn("checked");
                //colChecked.DataType = System.Type.GetType("System.Boolean");
                //p_dt.Columns.Add(colChecked);
                return p_dt;
            }
            catch (Exception ex)
            {
                return p_dt;
            }
        }
        //*********************************************************************************************
        //* Get Issue (Storie) Priority
        //*********************************************************************************************
        //(Int32 m_PrioLow, Int32 m_PrioMedium, Int32  m_PrioHigh, Int32 m_PrioCritical) = lb.clsResponseBody.get_issue_priority(sbResponseBody_Stories);
        public static (Int32, Int32, Int32, Int32) get_issue_priority(Chilkat.StringBuilder sbResponseBody)
        {
            string m_method = "clsResponseBody.get_issue_priority";
            Int32 i = 0;
            Int32 count_i = 0;
            Int32 cntPrioLow = 0;
            Int32 cntPrioMedium = 0;
            Int32 cntPrioHigh = 0;
            Int32 cntPrioCritical = 0;
            string issue_prio = "";
            try
            {
                Chilkat.JsonObject jsonResponse = new Chilkat.JsonObject();
                jsonResponse.LoadSb(sbResponseBody);
                count_i = jsonResponse.SizeOfArray("issues");

                while (i < count_i)
                {
                    jsonResponse.I = i;
                    //********************************************************
                    //* Count Story Priority
                    //********************************************************
                    issue_prio = jsonResponse.StringOf("issues[i].fields.priority.name");
                    switch (issue_prio)
                    {
                        case "Low":
                            cntPrioLow = cntPrioLow + 1;
                            break;
                        case "Medium":
                            cntPrioMedium = cntPrioMedium + 1;
                            break;
                        case "High":
                            cntPrioHigh = cntPrioHigh + 1;
                            break;
                        case "Critical":
                            cntPrioCritical = cntPrioCritical + 1;
                            break;
                        default:
                            // code block
                            break;
                    }
                    i = i + 1;
                }
                return (cntPrioLow, cntPrioMedium, cntPrioHigh, cntPrioCritical);
            }
            catch (Exception ex)
            {
                lb.clsLogger.WriteLog("Error in : " + m_method + " : " + ex.Message);
                return (cntPrioLow, cntPrioMedium, cntPrioHigh, cntPrioCritical);
            }
        }
        //*********************************************************************************************
        //* Get Issue (Storie) Priority
        //*********************************************************************************************
        //(Int32 m_StatusNew, Int32 m_StatusOpen, Int32  m_StatusInProgress, Int32 m_StatusInSpecification,Int32 m_StatusPending, Int32 m_StatusInReview , Int32  m_StatusResolved, Int32 m_StatusClosed,String m_all_stories_resolved) = lb.clsResponseBody.get_issue_status(sbResponseBody_Stories);
        public static (Int32,Int32,Int32,Int32,Int32,Int32,Int32,Int32,string) get_issue_status(Chilkat.StringBuilder sbResponseBody)
        {
            string m_method = "clsResponseBody.get_issue_status";
            Int32 i = 0;
            Int32 count_i = 0;
            int cntStatusNew = 0;
            int cntStatusOpen = 0;
            int cntStatusInProgress = 0;
            int cntStatusInSpecification = 0;
            int cntStatusPending = 0;
            int cntStatusInReview = 0;
            int cntStatusResolved = 0;
            int cntStatusClosed = 0;

            string m_status = "";
            bool is_all_user_stories_resolved = true;
            bool have_any_resolved_user_stories = true;
            string m_is_all_user_stories_resolved = "";
            try
            {
                Chilkat.JsonObject jsonResponse = new Chilkat.JsonObject();
                jsonResponse.LoadSb(sbResponseBody);
                count_i = jsonResponse.SizeOfArray("issues");

                while (i < count_i)
                {
                    jsonResponse.I = i;
                    //********************************************************
                    //* Count Story Priority
                    //********************************************************
                    m_status = jsonResponse.StringOf("issues[i].fields.status.name");
                    switch (m_status)
                    {
                        case "New":
                            cntStatusNew = cntStatusNew + 1;
                            is_all_user_stories_resolved = false;
                            break;
                        case "Open":
                            cntStatusOpen = cntStatusOpen + 1;
                            is_all_user_stories_resolved = false;
                            break;
                        case "In Progress":
                            cntStatusInProgress = cntStatusInProgress + 1;
                            is_all_user_stories_resolved = false;
                            break;
                        case "InSpecification":
                            cntStatusInSpecification = cntStatusInSpecification + 1;
                            is_all_user_stories_resolved = false;
                            break;
                        case "Pending":
                            cntStatusPending = cntStatusPending + 1;
                            is_all_user_stories_resolved = false;
                            break;
                        case "In Review":
                            cntStatusInReview = cntStatusInReview + 1;
                            is_all_user_stories_resolved = false;
                            break;
                        case "Resolved":
                            cntStatusResolved = cntStatusResolved + 1;
                            have_any_resolved_user_stories = true;
                            break;
                        case "Closed":
                            cntStatusClosed = cntStatusClosed + 1;
                            have_any_resolved_user_stories = true;
                            break;
                        default:
                            // code block
                            break;
                    }
                    i = i + 1;
                }
                if (is_all_user_stories_resolved == true)
                {
                    if (have_any_resolved_user_stories == true)
                    {
                        m_is_all_user_stories_resolved = "X";
                    }
                    else
                    {
                        m_is_all_user_stories_resolved = "";
                    }
                }
                else
                {
                    m_is_all_user_stories_resolved = "";
                }
                return (cntStatusNew, cntStatusOpen, cntStatusInProgress, cntStatusInSpecification, cntStatusPending, cntStatusInReview, cntStatusResolved, cntStatusClosed, m_is_all_user_stories_resolved);
            }
            catch (Exception ex)
            {
                lb.clsLogger.WriteLog("Error in : " + m_method + " : " + ex.Message);
                return (cntStatusNew, cntStatusOpen, cntStatusInProgress, cntStatusInSpecification, cntStatusPending, cntStatusInReview, cntStatusResolved, cntStatusClosed, m_is_all_user_stories_resolved);
            }
        }
        //*********************************************************************************************
        //* Get SUMMARY for the ISSUE
        //*********************************************************************************************
        //string m_parent_prio = lb.clsResponseBody.get_parent_prio(sbResponseBody);
        public static string get_parent_prio(Chilkat.StringBuilder sbResponseBody,string p_key)
        {
            string m_method = "clsResponseBody.get_parent_prio";
            string m_parent_prio = "";
            Int32 count_i = 0;
            Int32 i = 0;
            string m_key = "";
            try
            {
                Chilkat.JsonObject jsonResponse = new Chilkat.JsonObject();
                jsonResponse.LoadSb(sbResponseBody);
                m_parent_prio = jsonResponse.StringOf("issues[0].fields.priority.name");


                count_i = jsonResponse.SizeOfArray("issues");

                while (i < count_i)
                {
                    jsonResponse.I = i;
                    //********************************************************
                    //* Count Story Priority
                    //********************************************************
                    m_key = jsonResponse.StringOf("issues[i].key");
                    if(m_key== p_key)
                    {
                        m_parent_prio = jsonResponse.StringOf("issues[i].fields.priority.name");
                        return m_parent_prio;
                    }
                    
                    i = i + 1;
                }
                return m_parent_prio;

            }
            catch (Exception ex)
            {
                lb.clsLogger.WriteLog("Error in : " + m_method + " : " + ex.Message);
                return m_parent_prio;
            }
        }

    }

}
