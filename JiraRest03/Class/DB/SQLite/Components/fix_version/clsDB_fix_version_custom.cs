using System;
using System.Data;
using System.Data.SQLite;
namespace lb
{
    class clsDB_fix_version_custom
    {
        public static DataTable get_dt_for_all_fix_version_for_dbo(string p_dbo_id)
        {
            string m_method = "clsDB_fix_version_gen.get_dt_for_all_fix_version_for_dbo";
            DataTable dt = new DataTable();
            try
            {
                dt = lb.clsDB_fix_version_gen.get_dt_fix_version_columns(dt);
                clsSQLiteDB databaseObject = new clsSQLiteDB();
                string sql = "Select * from fix_version";
                sql = sql + " where dbo_id = '" + p_dbo_id + "'";
                SQLiteCommand myCommand = new SQLiteCommand(sql, databaseObject.myConfigConnection);
                databaseObject.OpenConnection();
                SQLiteDataReader result = myCommand.ExecuteReader();
                if (result.HasRows)
                {
                    while (result.Read())
                    {
                        DataRow dr = dt.NewRow();
                        dr["fix_version_id"] = result["fix_version_id"].ToString();
                        dr["dbo_id"] = result["dbo_id"].ToString();
                        dr["fix_version"] = result["fix_version"].ToString();
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
