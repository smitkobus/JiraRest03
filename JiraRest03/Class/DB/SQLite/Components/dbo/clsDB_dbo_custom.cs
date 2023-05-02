using System;
using System.Data;
using System.Data.SQLite;
namespace lb
{
    class clsDB_dbo_custom
    {
        public static string get_dbo_value_from_id(Int32 p_dbo_id)
        {
            string method = "get_dbo_value_from_id";
            string dbo_value = "";
            try
            {
                clsSQLiteDB databaseObject = new clsSQLiteDB();
                string sql = "Select * from dbo";
                sql = sql + " where dbo_id = '" + p_dbo_id+ "'";
                SQLiteCommand myCommand = new SQLiteCommand(sql, databaseObject.myConfigConnection);
                databaseObject.OpenConnection();
                SQLiteDataReader result = myCommand.ExecuteReader();
                if (result.HasRows)
                {
                    while (result.Read())
                    {
                        dbo_value = result["dbo"].ToString();
                    }
                }
                databaseObject.CloseConnection();
                return dbo_value;
            }
            catch (Exception ex)
            {
                return dbo_value;
            }
        }
        public static string get_epic_project_id_from_id(Int32 p_dbo_id)
        {
            string method = "get_dbo_value_from_id";
            string epic_project_id = "";
            try
            {
                clsSQLiteDB databaseObject = new clsSQLiteDB();
                string sql = "Select * from dbo";
                sql = sql + " where dbo_id = '" + p_dbo_id + "'";
                SQLiteCommand myCommand = new SQLiteCommand(sql, databaseObject.myConfigConnection);
                databaseObject.OpenConnection();
                SQLiteDataReader result = myCommand.ExecuteReader();
                if (result.HasRows)
                {
                    while (result.Read())
                    {
                        epic_project_id = result["epic_project_id"].ToString();
                    }
                }
                databaseObject.CloseConnection();
                return epic_project_id;
            }
            catch (Exception ex)
            {
                return epic_project_id;
            }
        }
        public static string get_issue_project_id_from_id(Int32 p_dbo_id)
        {
            string method = "get_issue_project_id_from_id";
            string issue_project_id = "";
            try
            {
                clsSQLiteDB databaseObject = new clsSQLiteDB();
                string sql = "Select * from dbo";
                sql = sql + " where dbo_id = '" + p_dbo_id + "'";
                SQLiteCommand myCommand = new SQLiteCommand(sql, databaseObject.myConfigConnection);
                databaseObject.OpenConnection();
                SQLiteDataReader result = myCommand.ExecuteReader();
                if (result.HasRows)
                {
                    while (result.Read())
                    {
                        issue_project_id = result["issue_project_id"].ToString();
                    }
                }
                databaseObject.CloseConnection();
                return issue_project_id;
            }
            catch (Exception ex)
            {
                return issue_project_id;
            }
        }
    }
}
