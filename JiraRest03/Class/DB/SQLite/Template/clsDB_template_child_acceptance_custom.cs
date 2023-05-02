using System;
using System.Data;
using System.Data.SQLite;
namespace lb
{
    class clsDB_template_child_acceptance_custom
    {
        //*
        //* Copy all acceptance from one HDR to the next
        //*
        public static bool copy_child_acceptance(string p_from_template_child_issue_id, string p_to_template_child_issue_id)
        {
            string method = "copy_child_acceptance";
            //DataTable dtc = new DataTable();
            bool acceptancee_copied = false;
            string m_template_child_acceptance_id = "";
            string m_template_child_acceptance = "";
            string m_template_child_issue_id = "";
            string m_isHeader = "";
            try
            {
                DataTable dt_child_acceptance = lb.clsDB_template_child_acceptance_custom.get_dt_all_acceptance_for_one_child_issue_id(p_from_template_child_issue_id);
                foreach (DataRow row in dt_child_acceptance.Rows)
                {
                    m_template_child_acceptance_id = row["template_child_acceptance_id"].ToString();
                    m_template_child_acceptance = row["template_child_acceptance"].ToString();
                    m_template_child_issue_id = row["template_child_issue_id"].ToString();
                    m_isHeader = row["isHeader"].ToString();
                    lb.clsDB_template_child_acceptance_gen.insert(m_template_child_acceptance, p_to_template_child_issue_id, m_isHeader);
                }
                return acceptancee_copied;
            }
            catch (Exception ex)
            {
                lb.clsLogger.WriteLog("Error : " + ex.Message);
                return acceptancee_copied;
            }
        }
        //*
        //* Delete All acceptance from one Child ISSUE
        //*
        public static Boolean delete_all_acceptance_for_an_child_issue(string p_template_child_issue_id)
        {
            string method = "delete_all_acceptance_for_an_child_issue";
            try
            {
                clsSQLiteDB databaseObject = new clsSQLiteDB();
                string sql = "delete from template_child_acceptance";
                sql = sql + " where template_child_issue_id = '" + p_template_child_issue_id + "'";
                SQLiteCommand myCommand = new SQLiteCommand(sql, databaseObject.myConfigConnection);
                databaseObject.OpenConnection();
                myCommand.ExecuteNonQuery();
                databaseObject.CloseConnection();
                return true;
            }
            catch (Exception ex)
            {
                lb.clsLogger.WriteLog("Error : " + ex.Message);
                return false;
            }
        }
        public static bool child_acceptance_exist(string p_template_child_issue_id, string p_template_child_acceptance)
        {
            string method = "child_acceptance_exist";
            bool child_acceptance_exist = false;
            try
            {
                clsSQLiteDB databaseObject = new clsSQLiteDB();
                string sql = "Select * from template_child_acceptance";
                sql = sql + " where template_child_issue_id = '" + p_template_child_issue_id + "'";
                sql = sql + " and template_child_acceptance = '" + p_template_child_acceptance + "'";
                SQLiteCommand myCommand = new SQLiteCommand(sql, databaseObject.myConfigConnection);
                databaseObject.OpenConnection();
                SQLiteDataReader result = myCommand.ExecuteReader();
                if (result.HasRows)
                {
                    while (result.Read())
                    {
                        child_acceptance_exist = true;
                    }
                }
                databaseObject.CloseConnection();
                return child_acceptance_exist;
            }
            catch (Exception ex)
            {
                lb.clsLogger.WriteLog("Error : " + ex.Message);
                return child_acceptance_exist;
            }
        }
        public static DataTable get_dt_all_acceptance_for_one_child_issue_id(string p_child_issue_id)
        {
            string method = "get_dt_all_acceptance_for_one_child_issue_id";
            DataTable dt = new DataTable();
            try
            {
                dt = lb.clsDB_template_child_acceptance_gen.get_dt_template_child_acceptance_columns(dt);
                clsSQLiteDB databaseObject = new clsSQLiteDB();
                string sql = "Select * from template_child_acceptance";
                sql = sql + " where template_child_issue_id = '" + p_child_issue_id + "'";
                SQLiteCommand myCommand = new SQLiteCommand(sql, databaseObject.myConfigConnection);
                databaseObject.OpenConnection();
                SQLiteDataReader result = myCommand.ExecuteReader();
                if (result.HasRows)
                {
                    while (result.Read())
                    {
                        DataRow dr = dt.NewRow();
                        dr["template_child_acceptance_id"] = result["template_child_acceptance_id"].ToString();
                        dr["template_child_acceptance"] = result["template_child_acceptance"].ToString();
                        dr["template_child_issue_id"] = result["template_child_issue_id"].ToString();
                        if (result["isHeader"].ToString() == "0") { dr["isHeader"] = false; }
                        if (result["isHeader"].ToString() == "1") { dr["isHeader"] = true; }
                        dt.Rows.Add(dr);
                    }
                }
                databaseObject.CloseConnection();
                return dt;
            }
            catch (Exception ex)
            {
                lb.clsLogger.WriteLog("Error : " + ex.Message);
                return dt;
            }
        }
        public static Boolean update_is_header(string p_template_child_acceptance_id, string p_isHeader)
        {
            string m_method = "clsDB_template_child_acceptance_gen.update";
            try
            {
                clsSQLiteDB databaseObject = new clsSQLiteDB();
                string sql = "Update template_child_acceptance set";
                sql = sql + " isHeader = '" + p_isHeader + "'";
                sql = sql + " where template_child_acceptance_id = '" + p_template_child_acceptance_id + "'";
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
    }
}
