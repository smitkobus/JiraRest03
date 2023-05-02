using lb.Conn;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lb
{
    class dbo_epic
    {
        public static Boolean insert(string dbo_id, string epic_id)
        {
            string m_method = "dbo_epic.insert";
            try
            {
                string connstring = clsConn.get_conn("dbzdtest09");
                NpgsqlConnection conn = new NpgsqlConnection(connstring);
                // First Clean the table
                conn.Open();
                string sqlInsert = @"INSERT INTO jira.dbo_epic";
                sqlInsert = sqlInsert + "(dbo_id, epic_id) VALUES ";
                //dbo_id, epic_project_id, issue_project_id, po_uid, dmgf_uid, use_case_dmgf_uid, lde_uid)
                //dbo_id, epic_project_id, issue_project_id, po_uid, dmgf_uid, use_case_dmgf_uid, lde_uid
                sqlInsert = sqlInsert + "('" + dbo_id + "',";
                sqlInsert = sqlInsert + "'" + epic_id + "')";
                sqlInsert.Replace("'", "''");
                try
                {
                    NpgsqlCommand cmd = new NpgsqlCommand(sqlInsert, conn);
                    cmd.ExecuteNonQuery();
                }
                catch (NpgsqlException e)
                {
                    //if (dt.Rows[j]["tmadmin"] is null)
                    //{
                    //    cnt_errors_in_xml = cnt_errors_in_xml + 1;
                    //}
                    NpgsqlException aa = e;
                    //clsLog.WriteLog(method, UniqueID, "SQLError :" + aa);
                }
                conn.Close();
            }
            catch (Exception ex)
            {
                lb.clsLogger.WriteLog("Error in : " + m_method + " : " + ex.Message);
            }

            return false;
        }
        public static Boolean update(string dbo_id, string epic_id)
        {
            string m_method = "dbo_epic.update";
            try
            {
                string connstring = clsConn.get_conn("dbzdtest09");
                NpgsqlConnection conn = new NpgsqlConnection(connstring);
                // First Clean the table
                conn.Open();
                string sqlUpdate = @"UPDATE jira.dbo_epic SET ";
                sqlUpdate = sqlUpdate + "dbo_id = '" + dbo_id + "'";
                sqlUpdate = sqlUpdate + " WHERE epic_id ='" + epic_id + "' ";
                sqlUpdate.Replace("'", "''");
                try
                {
                    NpgsqlCommand cmd = new NpgsqlCommand(sqlUpdate, conn);
                    cmd.ExecuteNonQuery();
                }
                catch (NpgsqlException e)
                {
                    //if (dt.Rows[j]["tmadmin"] is null)
                    //{
                    //    cnt_errors_in_xml = cnt_errors_in_xml + 1;
                    //}
                    NpgsqlException aa = e;
                    //clsLog.WriteLog(method, UniqueID, "SQLError :" + aa);
                }
                conn.Close();
            }
            catch (Exception ex)
            {
                lb.clsLogger.WriteLog("Error in : " + m_method + " : " + ex.Message);
            }

            return false;
        }
    }
}
