using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lb
{
    class clsJira_From_File
    {
        // string text = File.ReadAllText(sFull_Path);
        //*********************************************************************************************
        //* Get LATEST FILE and Return RESPONSE BODY
        //*********************************************************************************************
        //*Chilkat.StringBuilder sbResponseBody = lb.clsJira_From_File.get_responsebody_from_dbo_id_and_epic(m_dbo_id,m_epic_key);
        public static Chilkat.StringBuilder get_responsebody_from_dbo_id_and_epic(string p_dbo_id, string p_epic_key)
        {
            string m_method = "clsJira_From_File.get_responsebody_from_dbo_id_and_epic";
            Chilkat.StringBuilder sbResponseBody;
            string sFull_Path = "";
            try
            {
                sFull_Path = lb.clsFile.get_latest_file_for_dbo_id_and_jira_key(Convert.ToInt32(p_dbo_id), p_epic_key);
                sbResponseBody = lb.clsChilKat.get_stringbuilder_from_file_path(sFull_Path);
                return sbResponseBody;
            }
            catch (Exception ex)
            {
                lb.clsLogger.WriteLog("Error in : " + m_method + " : " + ex.Message);
                sbResponseBody = null;
                return sbResponseBody;
            }
        }
        //*********************************************************************************************
        //* Get Response BODY from PATH
        //*********************************************************************************************
        //*string m_summary = lb.clsJire_From_File.get_responsebody_from_file(m_full_path);
        public static Chilkat.StringBuilder get_responsebody_from_file(string p_full_path)
        {
            string m_method = "clsJira_From_File.get_responsebody_from_file";
            //Chilkat.StringBuilder sbResponseBody = lb.clsJira.get_user_stories_for_epic(epic);
            Chilkat.StringBuilder sbResponseBody;
            try
            {
                sbResponseBody = lb.clsChilKat.get_stringbuilder_from_file_path(p_full_path);
                return sbResponseBody;
            }
            catch (Exception ex)
            {
                lb.clsLogger.WriteLog("Error in : " + m_method + " : " + ex.Message);
                sbResponseBody = null;
                return sbResponseBody;
            }
        }
        //*********************************************************************************************
        //* Get URL for EPIC
        //*********************************************************************************************
        //*string m_summary = lb.clsJire_From_File.get_summary_for_a_issue(m_full_path,m_issue_key);
        public static string get_summary_for_a_issue(string p_full_path,string p_issue_key)
        {
            string m_method = "clsJira_From_File.get_summary_for_a_issue";
            string m_summary = "";
            string text = "";
            Chilkat.StringBuilder sbResponseBody_epic;
            Chilkat.JsonObject jsonResponse = new Chilkat.JsonObject();
            Int32 count_i = 0;
            Int32 i = 0;
            string attribute = "";
            string issue_key = "";
            string issue_summary = "";
            try
            {
                sbResponseBody_epic = lb.clsChilKat.get_stringbuilder_from_file_path(p_full_path);
                jsonResponse.LoadSb(sbResponseBody_epic);
                count_i = jsonResponse.SizeOfArray("issues");
                while (i < count_i)
                {
                    //* KEY
                    attribute = "issues[i].key";
                    attribute = attribute.Replace("[i]", "[" + i + "]");
                    issue_key = jsonResponse.StringOf(attribute);
                    //* IF KEY = PARM KEY, the get SUMMARY and Return it
                    if(issue_key== p_issue_key)
                    {
                        attribute = "issues[i].fields.summary";
                        attribute = attribute.Replace("[i]", "[" + i + "]");
                        issue_summary = jsonResponse.StringOf(attribute);
                        return issue_summary;
                    }

                    i = i + 1;
                }
                return m_summary;
            }
            catch (Exception ex)
            {
                lb.clsLogger.WriteLog("Error in : " + m_method + " : " + ex.Message);
                return m_summary;
            }
        }
        public static (string, string) get_story_that_contain(string sFull_Path, string p_stories_contains)
        {
            string m_method = "clsJira_From_File.get_story_that_contain";
            string m_issue_key = "";
            string m_issue_summary = "";
            Chilkat.StringBuilder sbResponseBody_epic;
            Chilkat.JsonObject jsonResponse = new Chilkat.JsonObject();
            Int32 count_i = 0;
            Int32 i = 0;
            bool load_epic_row = true;
            bool summary_is_valid = false;
            try
            {
                sbResponseBody_epic = lb.clsChilKat.get_stringbuilder_from_file_path(sFull_Path);
                jsonResponse.LoadSb(sbResponseBody_epic);
                count_i = jsonResponse.SizeOfArray("issues");
                while (i < count_i)
                {
                    //* KEY
                    string attribute = "issues[i].key";
                    attribute = attribute.Replace("[i]", "[" + i + "]");
                    string issue_key = jsonResponse.StringOf(attribute);
                    //* SUMMARY
                    attribute = "issues[i].fields.summary";
                    attribute = attribute.Replace("[i]", "[" + i + "]");
                    string issue_summary = jsonResponse.StringOf(attribute);
                    summary_is_valid = issue_summary.ToLower().Contains(p_stories_contains.ToLower());
                    if (summary_is_valid == true)
                    {
                        load_epic_row = true;
                    }
                    if (summary_is_valid == true)
                    {
                        m_issue_key = issue_key;
                        m_issue_summary = issue_summary;
                        return (m_issue_key, m_issue_summary);
                    }
                    i = i + 1;
                }
                return (m_issue_key, m_issue_summary);
            }
            catch (Exception ex)
            {
                lb.clsLogger.WriteLog("Error in : " + m_method + " : " + ex.Message);
                return (m_issue_key, m_issue_summary);
            }
        }
    }
}
