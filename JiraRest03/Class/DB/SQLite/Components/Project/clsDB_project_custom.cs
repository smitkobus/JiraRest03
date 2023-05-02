using System;
using System.Data;
using System.Data.SQLite;
namespace lb
{
    class clsDB_project_custom
    {
        //*********************************************************************************************
        //* SGet PROJECT KEY from PROJECT ID
        //*********************************************************************************************
        //string m_project_key = lb.clsDB_project_custom.get_project_key_for_project_id(m_project_id);
        public static string get_project_key_for_project_id(string p_project_id)
        {
            string m_method = "clsDB_project_custom.get_project_key_for_project_id";
            string m_project_key = "";
            try
            {
                clsSQLiteDB databaseObject = new clsSQLiteDB();
                string sql = "Select * from project";
                sql = sql + " where project_id = '" + p_project_id + "'";

                SQLiteCommand myCommand = new SQLiteCommand(sql, databaseObject.myConfigConnection);
                databaseObject.OpenConnection();
                SQLiteDataReader result = myCommand.ExecuteReader();
                if (result.HasRows)
                {
                    while (result.Read())
                    {
                        m_project_key = result["jira_project_key"].ToString();
                    }
                }
                databaseObject.CloseConnection();
                return m_project_key;
            }
            catch (Exception ex)
            {
                lb.clsLogger.WriteLog("Error in : " + m_method + " : " + ex.Message);
                return m_project_key;
            }
        }
        public static string get_project_id_for_dbo_and_key(string p_dbo_id, string p_jira_project_key)
        {
            string m_method = "clsDB_project_custom.get_project_id_for_dbo_and_key";
            string m_project_id = "0";
            try
            {
                clsSQLiteDB databaseObject = new clsSQLiteDB();
                string sql = "Select * from project";
                sql = sql + " where dbo_id = '" + p_dbo_id + "'";
                sql = sql + " and  jira_project_key = '" + p_jira_project_key + "'";

                SQLiteCommand myCommand = new SQLiteCommand(sql, databaseObject.myConfigConnection);
                databaseObject.OpenConnection();
                SQLiteDataReader result = myCommand.ExecuteReader();
                if (result.HasRows)
                {
                    while (result.Read())
                    {
                        m_project_id = result["project_id"].ToString();
                    }
                }
                databaseObject.CloseConnection();
                return m_project_id;
            }
            catch (Exception ex)
            {
                lb.clsLogger.WriteLog("Error in : " + m_method + " : " + ex.Message);
                return m_project_id;
            }
        }
        public static DataTable get_project_per_dbo_all(string p_dbo_id)
        {
            string m_method = "clsDB_project_custom.get_project_per_dbo";
            DataTable dt = new DataTable();
            try
            {
                dt = lb.clsDB_project_gen.get_dt_project_columns(dt);
                clsSQLiteDB databaseObject = new clsSQLiteDB();
                string sql = "Select * from project";
                sql = sql + " where dbo_id = '" + p_dbo_id + "'";
                sql = sql + " order by sort_order";

                SQLiteCommand myCommand = new SQLiteCommand(sql, databaseObject.myConfigConnection);
                databaseObject.OpenConnection();
                SQLiteDataReader result = myCommand.ExecuteReader();
                if (result.HasRows)
                {
                    while (result.Read())
                    {
                        DataRow dr = dt.NewRow();
                        dr["project_id"] = result["project_id"].ToString();
                        dr["dbo_id"] = result["dbo_id"].ToString();
                        dr["jira_project_key"] = result["jira_project_key"].ToString();
                        dr["project_name"] = result["project_name"].ToString();
                        dr["label"] = result["label"].ToString();
                        dr["sort_order"] = result["sort_order"].ToString();
                        if (result["active"].ToString() == "0") { dr["active"] = false; }
                        if (result["active"].ToString() == "1") { dr["active"] = true; }
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
        public static DataTable get_project_per_dbo_only_story(string p_dbo_id)
        {
            string m_method = "clsDB_project_custom.get_project_per_dbo";
            DataTable dt = new DataTable();
            try
            {
                dt = lb.clsDB_project_gen.get_dt_project_columns(dt);
                clsSQLiteDB databaseObject = new clsSQLiteDB();
                string sql = "Select * from project";
                sql = sql + " where dbo_id = '" + p_dbo_id + "'";
                sql = sql + " and is_story_project = '1'";
                sql = sql + " order by sort_order";

                SQLiteCommand myCommand = new SQLiteCommand(sql, databaseObject.myConfigConnection);
                databaseObject.OpenConnection();
                SQLiteDataReader result = myCommand.ExecuteReader();
                if (result.HasRows)
                {
                    while (result.Read())
                    {
                        DataRow dr = dt.NewRow();
                        dr["project_id"] = result["project_id"].ToString();
                        dr["dbo_id"] = result["dbo_id"].ToString();
                        dr["jira_project_key"] = result["jira_project_key"].ToString();
                        dr["project_name"] = result["project_name"].ToString();
                        dr["label"] = result["label"].ToString();
                        dr["sort_order"] = result["sort_order"].ToString();
                        if (result["active"].ToString() == "0") { dr["active"] = false; }
                        if (result["active"].ToString() == "1") { dr["active"] = true; }
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
