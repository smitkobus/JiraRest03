using System;
using System.Data;
using System.Data.SQLite;

namespace lb
{
    class clsDB_cbo_det_custom
    {
        //string value_01 = lb.clsDB_cbo_det_custom.get_value_01_for_cbo_det(m_cbo_det_id);
        public static string get_value_01_for_cbo_det(string p_cbo_det_id)
        {
            string method = "get_value_01_for_cbo_det";
            string value_01 = "";
            try
            {
                clsSQLiteDB databaseObject = new clsSQLiteDB();
                string sql = "Select * from cbo_det";
                sql = sql + " where cbo_det_id = '" + p_cbo_det_id + "'";
                SQLiteCommand myCommand = new SQLiteCommand(sql, databaseObject.myConfigConnection);
                databaseObject.OpenConnection();
                SQLiteDataReader result = myCommand.ExecuteReader();
                if (result.HasRows)
                {
                    while (result.Read())
                    {
                        value_01 = result["cbo_value"].ToString();
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
        //string value_02 = lb.clsDB_cbo_det_custom.get_value_02_for_cbo_det(m_cbo_det_id);
        public static string get_value_02_for_cbo_det(string p_cbo_det_id)
        {
            string method = "get_value_02_for_cbo_det";
            string value_02 = "";
            try
            {
                clsSQLiteDB databaseObject = new clsSQLiteDB();
                string sql = "Select * from cbo_det";
                sql = sql + " where cbo_det_id = '" + p_cbo_det_id + "'";
                SQLiteCommand myCommand = new SQLiteCommand(sql, databaseObject.myConfigConnection);
                databaseObject.OpenConnection();
                SQLiteDataReader result = myCommand.ExecuteReader();
                if (result.HasRows)
                {
                    while (result.Read())
                    {
                        value_02 = result["cbo_value_02"].ToString();
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
        public static string get_cbo_detail_value_from_id(Int32 p_dbo_id)
        {
            string method = "get_cbo_detail_value_from_id";
            string dbo_value = "";
            try
            {
                clsSQLiteDB databaseObject = new clsSQLiteDB();
                string sql = "Select * from cbo_det";
                sql = sql + " where cbo_det_id = '" + p_dbo_id + "'";
                SQLiteCommand myCommand = new SQLiteCommand(sql, databaseObject.myConfigConnection);
                databaseObject.OpenConnection();
                SQLiteDataReader result = myCommand.ExecuteReader();
                if (result.HasRows)
                {
                    while (result.Read())
                    {
                        dbo_value = result["cbo_value"].ToString();
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
        public static DataTable get_dt_for_all_cbo_det_for_a_hdr(string p_cbo_hdr_id)
        {
            string method = "get_all_cbo_det";
            DataTable dt = new DataTable();
            try
            {
                dt = lb.clsDB_cbo_det_gen.get_dt_cbo_det_columns(dt);
                clsSQLiteDB databaseObject = new clsSQLiteDB();
                string sql = "Select * from cbo_det";
                sql = sql + " where cbo_hdr_id = '" + p_cbo_hdr_id + "'";
                SQLiteCommand myCommand = new SQLiteCommand(sql, databaseObject.myConfigConnection);
                databaseObject.OpenConnection();
                SQLiteDataReader result = myCommand.ExecuteReader();
                if (result.HasRows)
                {
                    while (result.Read())
                    {
                        DataRow dr = dt.NewRow();
                        dr["cbo_det_id"] = result["cbo_det_id"].ToString();
                        dr["cbo_hdr_id"] = result["cbo_hdr_id"].ToString();
                        dr["cbo_value"] = result["cbo_value"].ToString();
                        dr["cbo_value_02"] = result["cbo_value_02"].ToString();
                        if (result["active"].ToString() == "0") { dr["active"] = false; }
                        if (result["active"].ToString() == "1") { dr["active"] = true; }
                        //                        dr["active"] = result["active"].ToString();
                        dr["sort_order"] = result["sort_order"].ToString();
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
    }
}
