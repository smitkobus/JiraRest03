using System;
using System.Data;
using System.Data.SQLite;
namespace lb
{
    class clsDB_cbo_hdr_custom
    {
        public static string get_value_01_for_cbo_hdr(string p_cbo_hdr_id)
        {
            string method = "get_value_01_for_cbo_hdr";
            string value_01 = "";
            try
            {
                clsSQLiteDB databaseObject = new clsSQLiteDB();
                string sql = "Select * from cbo_hdr";
                sql = sql + " where cbo_hdr_id = '" + p_cbo_hdr_id + "'";
                SQLiteCommand myCommand = new SQLiteCommand(sql, databaseObject.myConfigConnection);
                databaseObject.OpenConnection();
                SQLiteDataReader result = myCommand.ExecuteReader();
                if (result.HasRows)
                {
                    while (result.Read())
                    {
                        value_01 = result["cbo_det_value_desc01"].ToString();
                    }
                }
                databaseObject.CloseConnection();
                return value_01;
            }
            catch (Exception ex)
            {
                return value_01;
            }
        }
        public static string get_value_02_for_cbo_hdr(string p_cbo_hdr_id)
        {
            string method = "get_value_02_for_cbo_hdr";
            string value_02 = "";
            try
            {
                clsSQLiteDB databaseObject = new clsSQLiteDB();
                string sql = "Select * from cbo_hdr";
                sql = sql + " where cbo_hdr_id = '" + p_cbo_hdr_id + "'";
                SQLiteCommand myCommand = new SQLiteCommand(sql, databaseObject.myConfigConnection);
                databaseObject.OpenConnection();
                SQLiteDataReader result = myCommand.ExecuteReader();
                if (result.HasRows)
                {
                    while (result.Read())
                    {
                        value_02 = result["cbo_det_value_desc02"].ToString();
                    }
                }
                databaseObject.CloseConnection();
                return value_02;
            }
            catch (Exception ex)
            {
                return value_02;
            }
        }
    }
}
