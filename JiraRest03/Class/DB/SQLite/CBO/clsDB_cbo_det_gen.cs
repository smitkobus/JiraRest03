using System;
using System.Data;
using System.Data.SQLite;
namespace lb
{
    class clsDB_cbo_det_gen
    {
        public static Boolean insert(string p_cbo_hdr_id, string p_cbo_value, string p_cbo_value_02, string p_active, string p_sort_order)
        {
            string method = "insert_cbo_det";
            try
            {
                clsSQLiteDB databaseObject = new clsSQLiteDB();
                string sql = "Insert into cbo_det ('cbo_det_id','cbo_hdr_id','cbo_value','active','sort_order')";
                sql = sql + "values (null,'" + p_cbo_hdr_id + "','" + p_cbo_value + "','" + p_cbo_value_02 + "','" + p_active + "','" + p_sort_order + "')";
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
        public static Boolean update(string p_cbo_det_id, string p_cbo_hdr_id, string p_cbo_value, string p_cbo_value_02, string p_active, string p_sort_order)
        {
            string method = "update_cbo_det";
            try
            {
                clsSQLiteDB databaseObject = new clsSQLiteDB();
                string sql = "Update cbo_det set";
                sql = sql + " cbo_hdr_id = '" + p_cbo_hdr_id + "',";
                sql = sql + " cbo_value = '" + p_cbo_value + "',";
                sql = sql + " cbo_value_02 = '" + p_cbo_value_02 + "',";
                sql = sql + " active = '" + p_active + "',";
                sql = sql + " sort_order = '" + p_sort_order + "'";
                sql = sql + " where cbo_det_id = '" + p_cbo_det_id + "'";
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
        public static DataTable get_dt_for_all_cbo_det()
        {
            string method = "get_all_cbo_det";
            DataTable dt = new DataTable();
            try
            {
                dt = get_dt_cbo_det_columns(dt);
                clsSQLiteDB databaseObject = new clsSQLiteDB();
                string sql = "Select * from cbo_det";
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
                        dr["active"] = result["active"].ToString();
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
        public static DataTable get_dt_cbo_det_columns(DataTable p_dt)
        {
            try
            {
                

                p_dt.Columns.Add("cbo_det_id");
                p_dt.Columns.Add("cbo_hdr_id");
                p_dt.Columns.Add("cbo_value");
                p_dt.Columns.Add("cbo_value_02");
                //*
                //* Active
                //*
                DataColumn colActive = new DataColumn("active");
                colActive.DataType = System.Type.GetType("System.Boolean");
                p_dt.Columns.Add(colActive);

                p_dt.Columns.Add("sort_order");
                return p_dt;
            }
            catch (Exception ex)
            {
                return p_dt;
            }
        }
    }
}
