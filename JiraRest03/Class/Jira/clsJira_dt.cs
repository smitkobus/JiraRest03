using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;


namespace lb
{
    //*********************************************************************************************
    //* Jira - Data Table
    //*********************************************************************************************
    class clsJira_dt
    {
        //*********************************************************************************************
        //* Get JSON object for the Query
        //*********************************************************************************************
        //DataTable dt = lb.clsJira.get_dt_sum_story_status(sbResponseBody);
        public static DataTable get_dt_sum_story_status(Chilkat.StringBuilder sbResponseBody)
        {
            string url_for_epic = "";
            DataTable dt = new DataTable();
            try
            {

                return dt;                

            }
            catch (Exception ex)
            {
                return dt;
            }
        }
        //*********************************************************************************************
        //* Get JSON object for the Query
        //*********************************************************************************************
        //Chilkat.StringBuilder sbResponseBody = lb.clsJira.get_json_obj_for_url(jira_url);
        public static Chilkat.StringBuilder get_json_obj_for_url(string hostname, string jira_url)
        {
            Chilkat.StringBuilder sbResponseBody = new Chilkat.StringBuilder();
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
                lb.clsLogger.WriteLog("Exception Error : " + ex.Message);
                return sbResponseBody;
            }
        }
    }
}
