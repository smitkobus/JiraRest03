using lb.Conn;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lb
{
    class DBO
    {
        public static Boolean insert(string dbo_id,string epic_project_id, string issue_project_id, string po_uid, string dmgf_uid, string use_case_dmgf_uid, string lde_uid, string dbo_display)
        {
            string method = "insert_dbo";
            try
            {
                string connstring = clsConn.get_conn("dbzdtest09");
                NpgsqlConnection conn = new NpgsqlConnection(connstring);
                // First Clean the table
                conn.Open();
                string sqlInsert = @"INSERT INTO jira.dbo";
                //sqlInsert = sqlInsert + "(dbo_id, lde, dmgf, dmgf_use_case, epic_project, issue_project) VALUES ";
                sqlInsert = sqlInsert + "(dbo_id, epic_project_id, issue_project_id, po_uid, dmgf_uid, use_case_dmgf_uid, lde_uid,dbo_display) VALUES ";
                //dbo_id, epic_project_id, issue_project_id, po_uid, dmgf_uid, use_case_dmgf_uid, lde_uid)
                //dbo_id, epic_project_id, issue_project_id, po_uid, dmgf_uid, use_case_dmgf_uid, lde_uid
                sqlInsert = sqlInsert + "('" + dbo_id + "',";
                sqlInsert = sqlInsert + "'" + epic_project_id + "',";
                sqlInsert = sqlInsert + "'" + issue_project_id + "',";
                sqlInsert = sqlInsert + "'" + po_uid + "',";
                sqlInsert = sqlInsert + "'" + dmgf_uid + "',";
                sqlInsert = sqlInsert + "'" + use_case_dmgf_uid + "',";
                sqlInsert = sqlInsert + "'" + lde_uid + "',";
                sqlInsert = sqlInsert + "'" + dbo_display + "')";
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
                //clsLog.WriteLog(method, UniqueID, "Error :" + ex);
            }

            return false;
        }
        public static Boolean change_id(string original_dbo_id,string dbo_id)
        {
            string method = "update_dbo";
            try
            {
                string connstring = clsConn.get_conn("dbzdtest09");
                NpgsqlConnection conn = new NpgsqlConnection(connstring);
                // First Clean the table
                conn.Open();
                //dbo_id, epic_project_id, issue_project_id, po_uid, dmgf_uid, use_case_dmgf_uid, lde_uid
                string sqlUpdate = @"UPDATE jira.dbo SET ";
                sqlUpdate = sqlUpdate + "dbo_id = '" + dbo_id.ToUpper() + "'";
                sqlUpdate = sqlUpdate + " WHERE dbo_id ='" + original_dbo_id + "' ";
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
                //clsLog.WriteLog(method, UniqueID, "Error :" + ex);
            }

            return false;
        }
        public static Boolean update( string dbo_id, string epic_project_id, string issue_project_id, string po_uid, string dmgf_uid, string use_case_dmgf_uid, string lde_uid,string dbo_display)
        {
            string method = "update_dbo";
            try
            {
                string connstring = clsConn.get_conn("dbzdtest09");
                NpgsqlConnection conn = new NpgsqlConnection(connstring);
                // First Clean the table
                conn.Open();
                //dbo_id, epic_project_id, issue_project_id, po_uid, dmgf_uid, use_case_dmgf_uid, lde_uid
                string sqlUpdate = @"UPDATE jira.dbo SET ";
                sqlUpdate = sqlUpdate + "epic_project_id = '" + epic_project_id + "',";
                sqlUpdate = sqlUpdate + "issue_project_id = '" + issue_project_id + "',";
                sqlUpdate = sqlUpdate + "po_uid = '" + po_uid + "',";
                sqlUpdate = sqlUpdate + "dmgf_uid = '" + dmgf_uid + "',";
                sqlUpdate = sqlUpdate + "use_case_dmgf_uid = '" + use_case_dmgf_uid + "',";
                sqlUpdate = sqlUpdate + "lde_uid = '" + lde_uid + "',";
                sqlUpdate = sqlUpdate + "dbo_display = '" + dbo_display + "'";
                sqlUpdate = sqlUpdate + " WHERE dbo_id ='" + dbo_id + "' ";
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
                //clsLog.WriteLog(method, UniqueID, "Error :" + ex);
            }

            return false;
        }
    }
}
