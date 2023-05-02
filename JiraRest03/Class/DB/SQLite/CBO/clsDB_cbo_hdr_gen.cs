using System;
using System.Data;
using System.Data.SQLite;
namespace lb
{
    class clsDB_cbo_hdr_gen
    {
        public static Boolean insert(string p_cbo_value, string p_cbo_det_value_desc01, string p_cbo_det_value_desc02)
        {
            string method = "insert_cbo_hdr";
            try
            {
                clsSQLiteDB databaseObject = new clsSQLiteDB();
                string sql = "Insert into cbo_hdr ('cbo_hdr_id','cbo_value','cbo_det_value_desc01','cbo_det_value_desc02')";
                sql = sql + "values (null,'" + p_cbo_value + "','" + p_cbo_det_value_desc01 + "','" + p_cbo_det_value_desc02 + "')";
                SQLiteCommand myCommand = new SQLiteCommand(sql, databaseObject.myConfigConnection);
                databaseObject.OpenConnection();
                myCommand.ExecuteNonQuery();
                databaseObject.CloseConnection();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public static Boolean update(string p_cbo_hdr_id, string p_cbo_value, string p_cbo_det_value_desc01, string p_cbo_det_value_desc02)
        {
            string method = "update_cbo_hdr";
            try
            {
                clsSQLiteDB databaseObject = new clsSQLiteDB();
                string sql = "Update cbo_hdr set";
                sql = sql + " cbo_value = '" + p_cbo_value + "',";
                sql = sql + " cbo_det_value_desc01 = '" + p_cbo_det_value_desc01 + "',";
                sql = sql + " cbo_det_value_desc02 = '" + p_cbo_det_value_desc02 + "'";
                sql = sql + " where cbo_hdr_id = '" + p_cbo_hdr_id + "'";
                
                SQLiteCommand myCommand = new SQLiteCommand(sql, databaseObject.myConfigConnection);
                databaseObject.OpenConnection();
                myCommand.ExecuteNonQuery();
                databaseObject.CloseConnection();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public static DataTable get_dt_for_all_cbo_hdr()
        {
            string method = "get_all_cbo_hdr";
            DataTable dt = new DataTable();
            try
            {
                dt = get_dt_cbo_hdr_columns(dt);
                clsSQLiteDB databaseObject = new clsSQLiteDB();
                string sql = "Select * from cbo_hdr";
                SQLiteCommand myCommand = new SQLiteCommand(sql, databaseObject.myConfigConnection);
                databaseObject.OpenConnection();
                SQLiteDataReader result = myCommand.ExecuteReader();
                if (result.HasRows)
                {
                    while (result.Read())
                    {
                        DataRow dr = dt.NewRow();
                        dr["btn01"] = "Detail";
                        dr["cbo_hdr_id"] = result["cbo_hdr_id"].ToString();
                        dr["cbo_value"] = result["cbo_value"].ToString();
                        dr["cbo_det_value_desc01"] = result["cbo_det_value_desc01"].ToString();
                        dr["cbo_det_value_desc02"] = result["cbo_det_value_desc02"].ToString();
                        
                        dt.Rows.Add(dr);
                    }
                }
                databaseObject.CloseConnection();
                return dt;
            }
            catch (Exception ex)
            {
                return dt;
            }
        }
        public static DataTable get_dt_cbo_hdr_columns(DataTable p_dt)
        {
            try
            {
                p_dt.Columns.Add("btn01");
                p_dt.Columns.Add("cbo_hdr_id");
                p_dt.Columns.Add("cbo_value");
                p_dt.Columns.Add("cbo_det_value_desc01");
                p_dt.Columns.Add("cbo_det_value_desc02");
                
                return p_dt;
            }
            catch (Exception ex)
            {
                return p_dt;
            }
        }
    }
}
