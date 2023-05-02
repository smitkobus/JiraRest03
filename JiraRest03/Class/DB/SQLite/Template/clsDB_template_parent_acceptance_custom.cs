using System;
using System.Data;
using System.Data.SQLite;

namespace lb
{
    class clsDB_template_parent_acceptance_custom
    {
        //*
        //* Delete HEADER - Acceptance criteria
        //*
        public static Boolean delete_all_acceptance_for_an_parent_issue(string p_template_hdr_id)
        {
            string m_method = "clsDB_template_parent_acceptance_custom.delete_all_acceptance_for_an_parent_issue";
            try
            {
                clsSQLiteDB databaseObject = new clsSQLiteDB();
                string sql = "delete from template_parent_acceptance";
                sql = sql + " where template_hdr_id = '" + p_template_hdr_id + "'";
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
        //*
        //* Copy all acceptance from one HDR to the next
        //*
        public static bool copy_parent_acceptance(string p_from_template_hdr_id, string p_to_template_hdr_id)
        {
            string m_method = "clsDB_template_parent_acceptance_custom.copy_parent_acceptance";
            //DataTable dtc = new DataTable();
            bool acceptancee_copied = false;
            string m_template_parent_acceptance_id = "";
            string m_template_parent_acceptance = "";
            string m_template_hdr_id = "";
            string m_is_Header = "0";
            try
            {
                //dtc = lb.clsDB_template_parent_acceptance_gen.get_dt_template_parent_acceptance_columns(dt);
                DataTable dt_template = get_dt_all_acceptance_for_one_template_hdr_id(p_from_template_hdr_id);
                foreach (DataRow row in dt_template.Rows)
                {
                    m_template_parent_acceptance_id = row["template_parent_acceptance_id"].ToString();
                    m_template_parent_acceptance = row["template_parent_acceptance"].ToString();
                    m_template_hdr_id = row["template_hdr_id"].ToString();  
                    m_is_Header = row["isHeader"].ToString();
                    lb.clsDB_template_parent_acceptance_gen.insert(m_template_parent_acceptance, p_to_template_hdr_id, m_is_Header);
                }
                //Insert Template HDR
                return acceptancee_copied;
            }
            catch (Exception ex)
            {
                lb.clsLogger.WriteLog("Error in : " + m_method + " : " + ex.Message);
                return acceptancee_copied;
            }
        }
        public static DataTable get_dt_all_acceptance_for_one_template_hdr_id(string p_template_hdr_id)
        {
            string m_method = "clsDB_template_parent_acceptance_custom.get_all_template_parent_acceptance_for_one_parent_hdr_id";
            DataTable dt = new DataTable();
            try
            {
                dt = lb.clsDB_template_parent_acceptance_gen.get_dt_template_parent_acceptance_columns(dt);
                clsSQLiteDB databaseObject = new clsSQLiteDB();
                string sql = "Select * from template_parent_acceptance";
                sql = sql + " where template_hdr_id = '" + p_template_hdr_id + "'";
                SQLiteCommand myCommand = new SQLiteCommand(sql, databaseObject.myConfigConnection);
                databaseObject.OpenConnection();
                SQLiteDataReader result = myCommand.ExecuteReader();
                if (result.HasRows)
                {
                    while (result.Read())
                    {
                        DataRow dr = dt.NewRow();
                        dr["template_parent_acceptance_id"] = result["template_parent_acceptance_id"].ToString();
                        dr["template_parent_acceptance"] = result["template_parent_acceptance"].ToString();
                        dr["template_hdr_id"] = result["template_hdr_id"].ToString();
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

                lb.clsLogger.WriteLog("Error in : " + m_method + " : " + ex.Message);
                return dt;
            }
        }
        public static Boolean update_is_header(string p_template_parent_acceptance_id, string p_isHeader)
        {
            string m_method = "clsDB_template_parent_acceptance_custom.update_is_header";
            try
            {
                clsSQLiteDB databaseObject = new clsSQLiteDB();
                string sql = "Update template_parent_acceptance set";
                sql = sql + " isHeader = '" + p_isHeader + "'";
                sql = sql + " where template_parent_acceptance_id = '" + p_template_parent_acceptance_id + "'";
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
