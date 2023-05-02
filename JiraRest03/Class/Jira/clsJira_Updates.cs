using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lb
{
    class clsJira_Updates
    {
        //*********************************************************************************************
        //* POST JSON
        //*********************************************************************************************
        //Int32 rc = lb.clsJira_Updates.json_put_prio_2_jira(p_issue_key,p_json);
        public static bool json_put_prio_2_jira(string p_issue_key, Chilkat.JsonObject json, string p_url_path = "")
        {
            string m_method = "clsJira_Updates.json_put_prio_2_jira";
            bool success = true;
            string m_hostname = "";
            string m_url = "";
            try
            {
                m_hostname = lb.clsParameters.get_hostname();
                Chilkat.Global glob = new Chilkat.Global();
                success = glob.UnlockBundle("KBSDTS.CB1092022_zTbLSG7a86mb");

                Chilkat.Rest rest = new Chilkat.Rest();

                //  URL: https://atc-int.bmwgroup.net/rest/api/2/issue
                bool bTls = true;
                int port = 443;
                bool bAutoReconnect = true;
                success = rest.Connect(m_hostname, port, bTls, bAutoReconnect);
                if (success != true)
                {
                    return success;
                }
                //
                string m_uid = lb.clsConfigParameters.get_uid();
                string m_pw = lb.clsConfigParameters.get_pw();
                rest.SetAuthBasic(m_uid, m_pw);
                //
                int x;
                x = 0;

                rest.AddHeader("Content-Type", "application/json");
                rest.AddHeader("Accept", "application/json");
                //var bodyData = 1"{\"statusColor\": \"#123456\",\"name\": \"My updated priority\",\"description\": \"My updated priority description\",\"iconUrl\": \"images/icons/priorities/minor.png\"}`";
                Chilkat.StringBuilder sbRequestBody = new Chilkat.StringBuilder();
                json.EmitSb(sbRequestBody);
                Chilkat.StringBuilder sbResponseBody = new Chilkat.StringBuilder();
                m_url = "/jira/rest/api/2/priority/{p_issue_key}";
                m_url = m_url.Replace("{p_issue_key}", p_issue_key);
                //if (p_url_path.Length > 0)
                //{
                //    m_url = m_url + "/" + p_url_path.Trim();
                //}
                success = rest.FullRequestSb("PUT", m_url, sbRequestBody, sbResponseBody);
                if (success != true)
                {
                    return success;
                }

                int respStatusCode = rest.ResponseStatusCode;
                if (respStatusCode >= 400)
                {
                    string txtResult = sbResponseBody.GetAsString();
                    return success;
                }

                Chilkat.JsonObject jsonResponse = new Chilkat.JsonObject();
                jsonResponse.LoadSb(sbResponseBody);
                return success;

            }
            catch (Exception ex)
            {
                lb.clsLogger.WriteLog("Error in : " + m_method + " : " + ex.Message);
                return success;
            }
        }
        //*********************************************************************************************
        //* Update Priority on a Story
        //*********************************************************************************************
        //Int32 rc = lb.clsJira_Updates.update_prio(p_issue_key,p_prio);
        public static Int32 update_prio(string p_issue_key, string p_prio)
        {

            string m_method = "clsJira_Updates.update_prio";
            Int32 rc = 0;
            try
            {
                Chilkat.JsonObject json = new Chilkat.JsonObject();
                json.UpdateString("body.fields.priority.name", p_prio);
                json_put_prio_2_jira(p_issue_key, json, "priority");
                return rc;
            }
            catch (Exception ex)
            {
                lb.clsLogger.WriteLog("Error in : " + m_method + " : " + ex.Message);
                return rc;
            }
        }
        //*********************************************************************************************
        //* PUT JSON
        //*********************************************************************************************
        //Int32 rc = lb.clsJira_Updates.json_put_prio(p_issue_key,p_json,p_url_path);
        public static bool json_put_prio(string p_issue_key, Chilkat.JsonObject json, string p_url_path = "")
        {
            string m_method = "clsJira_Updates.json_put_prio";
            bool success = true;
            string m_hostname = "";
            string m_url = "";
            try
            {
                m_hostname = lb.clsParameters.get_hostname();
                Chilkat.Global glob = new Chilkat.Global();
                success = glob.UnlockBundle("KBSDTS.CB1092022_zTbLSG7a86mb");

                Chilkat.Rest rest = new Chilkat.Rest();

                //  URL: https://atc-int.bmwgroup.net/rest/api/2/issue
                bool bTls = true;
                int port = 443;
                bool bAutoReconnect = true;
                success = rest.Connect(m_hostname, port, bTls, bAutoReconnect);
                if (success != true)
                {
                    return success;
                }
                //
                string m_uid = lb.clsConfigParameters.get_uid();
                string m_pw = lb.clsConfigParameters.get_pw();
                rest.SetAuthBasic(m_uid, m_pw);
                //
                int x;
                x = 0;

                rest.AddHeader("Content-Type", "application/json");
                rest.AddHeader("Accept", "application/json");
                Chilkat.StringBuilder sbRequestBody = new Chilkat.StringBuilder();
                json.EmitSb(sbRequestBody);
                Chilkat.StringBuilder sbResponseBody = new Chilkat.StringBuilder();
                m_url = "/jira/rest/api/2/priority/{p_issue_key}";
                m_url = m_url.Replace("{p_issue_key}", p_issue_key);
                if (p_url_path.Length > 0)
                {
                    m_url = m_url + "/" + p_url_path.Trim();
                }
                success = rest.FullRequestSb("PUT", m_url, sbRequestBody, sbResponseBody);
                if (success != true)
                {
                    return success;
                }

                int respStatusCode = rest.ResponseStatusCode;
                if (respStatusCode >= 400)
                {
                    string txtResult = sbResponseBody.GetAsString();
                    return success;
                }

                Chilkat.JsonObject jsonResponse = new Chilkat.JsonObject();
                jsonResponse.LoadSb(sbResponseBody);
                return success;

            }
            catch (Exception ex)
            {
                lb.clsLogger.WriteLog("Error in : " + m_method + " : " + ex.Message);
                return success;
            }
        }
        //*********************************************************************************************
        //* PUT JSON
        //*********************************************************************************************
        //Int32 rc = lb.clsJira_Updates.json_put_2_jira(p_issue_key,p_json,p_url_path);
        public static bool json_put_2_jira(string p_issue_key,Chilkat.JsonObject json, string p_url_path = "")
        {
            string m_method = "clsJira_Updates.json_put_2_jira";
            bool success = true;
            string m_hostname = "";
            string m_url = "";
            try
            {
                m_hostname = lb.clsParameters.get_hostname();
                Chilkat.Global glob = new Chilkat.Global();
                success = glob.UnlockBundle("KBSDTS.CB1092022_zTbLSG7a86mb");

                Chilkat.Rest rest = new Chilkat.Rest();

                //  URL: https://atc-int.bmwgroup.net/rest/api/2/issue
                bool bTls = true;
                int port = 443;
                bool bAutoReconnect = true;
                success = rest.Connect(m_hostname, port, bTls, bAutoReconnect);
                if (success != true)
                {
                    return success;
                }
                //
                string m_uid = lb.clsConfigParameters.get_uid();
                string m_pw = lb.clsConfigParameters.get_pw();
                rest.SetAuthBasic(m_uid, m_pw);
                //
                int x;
                x = 0;
      
                rest.AddHeader("Content-Type", "application/json");
                rest.AddHeader("Accept", "application/json");
                Chilkat.StringBuilder sbRequestBody = new Chilkat.StringBuilder();
                json.EmitSb(sbRequestBody);
                Chilkat.StringBuilder sbResponseBody = new Chilkat.StringBuilder();
                m_url = "/jira/rest/api/2/issue/{p_issue_key}";
                m_url = m_url.Replace("{p_issue_key}", p_issue_key);
                if (p_url_path.Length > 0)
                {
                    m_url = m_url + "/" + p_url_path.Trim();
                }
                success = rest.FullRequestSb("PUT", m_url, sbRequestBody, sbResponseBody);
                if (success != true)
                {
                    return success;
                }

                int respStatusCode = rest.ResponseStatusCode;
                if (respStatusCode >= 400)
                {
                    string txtResult = sbResponseBody.GetAsString();
                    return success;
                }

                Chilkat.JsonObject jsonResponse = new Chilkat.JsonObject();
                jsonResponse.LoadSb(sbResponseBody);
                return success;

            }
            catch (Exception ex)
            {
                lb.clsLogger.WriteLog("Error in : " + m_method + " : " + ex.Message);
                return success;
            }
        }
        //*********************************************************************************************
        //* POST JSON
        //*********************************************************************************************
        //Int32 rc = lb.clsJira_Updates.post_json_2_jira(p_issue_key,p_json);
        public static bool json_post_2_jira(string p_issue_key, Chilkat.JsonObject json,string p_url_path = "")
        {
            string m_method = "clsJira_Updates.post_json_2_jira";
            bool success = true;
            string m_hostname = "";
            string m_url = "";
            try
            {
                m_hostname = lb.clsParameters.get_hostname();
                Chilkat.Global glob = new Chilkat.Global();
                success = glob.UnlockBundle("KBSDTS.CB1092022_zTbLSG7a86mb");

                Chilkat.Rest rest = new Chilkat.Rest();

                //  URL: https://atc-int.bmwgroup.net/rest/api/2/issue
                bool bTls = true;
                int port = 443;
                bool bAutoReconnect = true;
                success = rest.Connect(m_hostname, port, bTls, bAutoReconnect);
                if (success != true)
                {
                    return success;
                }
                //
                string m_uid = lb.clsConfigParameters.get_uid();
                string m_pw = lb.clsConfigParameters.get_pw();
                rest.SetAuthBasic(m_uid, m_pw);
                //
                int x;
                x = 0;

                rest.AddHeader("Content-Type", "application/json");
                rest.AddHeader("Accept", "application/json");
                Chilkat.StringBuilder sbRequestBody = new Chilkat.StringBuilder();
                json.EmitSb(sbRequestBody);
                Chilkat.StringBuilder sbResponseBody = new Chilkat.StringBuilder();
                m_url = "/jira/rest/api/2/issue/{p_issue_key}";
                m_url = m_url.Replace("{p_issue_key}", p_issue_key);
                if(p_url_path.Length>0)
                {
                    m_url = m_url + "/" + p_url_path.Trim();
                }
                success = rest.FullRequestSb("POST", m_url, sbRequestBody, sbResponseBody);
                if (success != true)
                {
                    return success;
                }

                int respStatusCode = rest.ResponseStatusCode;
                if (respStatusCode >= 400)
                {
                    string txtResult = sbResponseBody.GetAsString();
                    return success;
                }

                Chilkat.JsonObject jsonResponse = new Chilkat.JsonObject();
                jsonResponse.LoadSb(sbResponseBody);
                return success;

            }
            catch (Exception ex)
            {
                lb.clsLogger.WriteLog("Error in : " + m_method + " : " + ex.Message);
                return success;
            }
        }
        //*********************************************************************************************
        //* Update Accptance Criteria
        //*********************************************************************************************
        //Int32 rc = lb.clsJira_Updates.update_issue_acceptance(p_issue_key,dtCurrentAcceptance p_new_acceptance);
        public static Int32 update_issue_acceptance(string p_issue_key, DataTable dtCurrentAcceptance,string p_new_acceptance)
        {
            string m_method = "clsJira_Updates.update_issue_acceptance";
            Int32 rc = 0;
            try
            {
                Chilkat.JsonObject json = new Chilkat.JsonObject();
                json = lb.clsJiraField.add_new_acceptance_to_current_acceptance(json, dtCurrentAcceptance, p_new_acceptance);
                json_put_2_jira(p_issue_key, json, "acceptance");
                //
                return rc;
            }
            catch (Exception ex)
            {
                lb.clsLogger.WriteLog("Error in : " + m_method + " : " + ex.Message);
                return rc;
            }
        }
        //*********************************************************************************************
        //* Update Summary
        //*********************************************************************************************
        //Int32 rc = lb.clsJira_Updates.update_issue_summary(p_issue_key,p_summary);
        public static Int32 update_issue_summary(string p_issue_key, string p_summary)
        {

            string m_method = "clsJira_Updates.update_issue_summary";
            Int32 rc = 0;

            try
            {
                Chilkat.JsonObject json = new Chilkat.JsonObject();
                json.UpdateString("fields.summary", p_summary);
                json_put_2_jira(p_issue_key, json);
                return rc;
            }
            catch (Exception ex)
            {
                lb.clsLogger.WriteLog("Error in : " + m_method + " : " + ex.Message);
                return rc;
            }
        }


        //*********************************************************************************************
        //* Update Summary
        //*********************************************************************************************
        //Int32 rc = lb.clsJira_Updates.update_issue_description(p_issue_key,p_description);
        public static Int32 update_issue_description(string p_issue_key, string p_description)
        {

            string m_method = "clsJira_Updates.update_issue_description";
            Int32 rc = 0;
            try
            {
                Chilkat.JsonObject json = new Chilkat.JsonObject();
                json.UpdateString("fields.description", p_description);
                json_put_2_jira(p_issue_key, json);
                return rc;
            }
            catch (Exception ex)
            {
                lb.clsLogger.WriteLog("Error in : " + m_method + " : " + ex.Message);
                return rc;
            }
        }
        //*********************************************************************************************
        //* Add TAG to Story
        //*********************************************************************************************
        //Int32 rc = lb.clsJira_Updates.update_issue_summary(p_issue_key,p_summary);
        public static Int32 add_tag(string p_issue_key, string p_tag)
        {

            string m_method = "clsJira_Updates.add_tag";
            Int32 rc = 0;
            try
            {
                Chilkat.JsonObject json = new Chilkat.JsonObject();
                json.UpdateString("update.labels[0].add", p_tag);
                json_put_2_jira(p_issue_key, json);
                return rc;
            }
            catch (Exception ex)
            {
                lb.clsLogger.WriteLog("Error in : " + m_method + " : " + ex.Message);
                return rc;
            }
        }
        //*********************************************************************************************
        //* Add COMMENT to Story
        //*********************************************************************************************
        //Int32 rc = lb.clsJira_Updates.add_comment(p_issue_key,p_comment);
        public static Int32 add_comment(string p_issue_key, string p_comment)
        {

            string m_method = "clsJira_Updates.add_comment";
            Int32 rc = 0;
            try
            {
                Chilkat.JsonObject json = new Chilkat.JsonObject();
                //json.UpdateString("update.labels[0].add", p_comment);
                json.UpdateString("body", p_comment);
                //json.UpdateString("visibility.type", "role");
                //json.UpdateString("visibility.value", "Administrators");
                json_post_2_jira(p_issue_key, json,"comment");
                return rc;
            }
            catch (Exception ex)
            {
                lb.clsLogger.WriteLog("Error in : " + m_method + " : " + ex.Message);
                return rc;
            }
        }

        //*********************************************************************************************
        //* Update Summary
        //*********************************************************************************************
        //Int32 rc = lb.clsJira_Updates.update_issue_priotity(p_issue_key,p_summary);
        public static Int32 update_issue_priotity(string p_issue_key, string p_summary)
        {

            string m_method = "clsJira_Updates.update_issue_priotity";
            Int32 rc = 0;

            try
            {
                Chilkat.JsonObject json = new Chilkat.JsonObject();
                json.UpdateString("fields.priority", p_summary);
                json_put_2_jira(p_issue_key, json);
                return rc;
            }
            catch (Exception ex)
            {
                lb.clsLogger.WriteLog("Error in : " + m_method + " : " + ex.Message);
                return rc;
            }
        }
    }
}
