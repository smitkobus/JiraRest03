using System;
using System.Data;
using System.Data.SQLite;
namespace lb
{
    class clsDB_interface_custom
    {
        //Int32 interface_count_per_dbo = lb.clsDB_interface_custom.get_interface_count_per_dbo(m_dbo_id);
        public static Int32 get_interface_count_per_dbo(string p_dbo_id)
        {
            string m_method = "clsDB_interface_custom.get_interface_count_per_dbo";
            Int32 interface_count_per_dbo = 0;
            DataTable p_dt;
            try
            {
                clsSQLiteDB databaseObject = new clsSQLiteDB();
                string sql = "Select count(*) as cnt_interface from interface";
                sql = sql + " where dbo_id = '" + p_dbo_id + "'";

                SQLiteCommand myCommand = new SQLiteCommand(sql, databaseObject.myConfigConnection);
                databaseObject.OpenConnection();
                SQLiteDataReader result = myCommand.ExecuteReader();
                if (result.HasRows)
                {
                    while (result.Read())
                    {
                        interface_count_per_dbo = Convert.ToInt32(result["cnt_interface"]);
                    }
                }
                return interface_count_per_dbo;
            }
            catch (Exception ex)
            {
                lb.clsLogger.WriteLog("Error in : " + m_method + " : " + ex.Message);
                return interface_count_per_dbo;
            }
        }
        public static string get_responsible_uid(string p_interface_id)
        {
            string m_method = "clsDB_interface_custom.get_responsible_uid";
            string responsible_uid = "";
            DataTable p_dt;
            try
            {
                p_dt = lb.clsDB_interface_custom.get_dt_for_key_interface(p_interface_id);
                foreach (DataRow row in p_dt.Rows)
                {
                    responsible_uid = row["responsible_uid"].ToString();
                }
                return responsible_uid;
            }
            catch (Exception ex)
            {
                lb.clsLogger.WriteLog("Error in : " + m_method + " : " + ex.Message);
                return responsible_uid;
            }
        }
        public static string get_interface_key(string p_interface_id)
        {
            string m_method = "clsDB_interface_custom.get_interface_key";
            string interface_key = "";
            DataTable p_dt;
            try
            {
                p_dt = lb.clsDB_interface_custom.get_dt_for_key_interface(p_interface_id);
                foreach (DataRow row in p_dt.Rows)
                {
                    interface_key = row["interface_key"].ToString();
                }
                return interface_key;
            }
            catch (Exception ex)
            {
                lb.clsLogger.WriteLog("Error in : " + m_method + " : " + ex.Message);
                return interface_key;
            }
        }
        public static string get_interface(string p_interface_id)
        {
            string m_method = "clsDB_interface_custom.p_interface_id";
            string m_interface = "";
            DataTable p_dt;
            try
            {
                p_dt = lb.clsDB_interface_custom.get_dt_for_key_interface(p_interface_id);
                foreach (DataRow row in p_dt.Rows)
                {
                    m_interface = row["interface"].ToString();
                }
                return m_interface;
            }
            catch (Exception ex)
            {

                lb.clsLogger.WriteLog("Error in : " + m_method + " : " + ex.Message);
                return m_interface;
            }
        }
        //*********************************************************************************************
        public static DataTable get_dt_for_all_interface_per_dbo_and_task_type(string p_dbo_id, string p_type_id)
        {
            string m_method = "clsDB_interface_custom.get_dt_for_all_interface_per_dbo_and_task_type";
            DataTable dt = new DataTable();
            string p_dbo = "";
            Int32 i_dbo_id = 0;
            //string p_interface_type = "";
            try
            {
                i_dbo_id = Convert.ToInt32(p_dbo_id);
                p_dbo = lb.clsDB_dbo_custom.get_dbo_value_from_id(Convert.ToInt32(p_dbo_id));
                //p_interface_type = "Ingest";
                dt = lb.clsDB_interface_gen.get_dt_interface_columns(dt);
                clsSQLiteDB databaseObject = new clsSQLiteDB();
                string sql = "Select * from interface";
                sql = sql + " where dbo_id = '" + p_dbo_id + "'";
                sql = sql + " and interface_type_id = '" + p_type_id + "'";
                sql = sql + " order by interface_key";

                SQLiteCommand myCommand = new SQLiteCommand(sql, databaseObject.myConfigConnection);
                databaseObject.OpenConnection();
                SQLiteDataReader result = myCommand.ExecuteReader();
                if (result.HasRows)
                {
                    while (result.Read())
                    {
                        DataRow dr = dt.NewRow();
                        dr["interface_id"] = result["interface_id"].ToString();
                        dr["dbo_id"] = result["dbo_id"].ToString();
                        dr["interface_type_id"] = result["interface_type_id"].ToString();
                        dr["interface_key"] = result["interface_key"].ToString();
                        dr["interface"] = result["interface"].ToString();
                        dr["label"] = result["label"].ToString();
                        dr["responsible_uid"] = result["responsible_uid"].ToString();
                        dr["responsible_name"] = lb.clsDB_user_store_custom.get_displayname_from_uid(dr["responsible_uid"].ToString());
                        dr["pod"] = lb.clsDB_user_store_custom.get_pod_from_uid(dr["responsible_uid"].ToString());
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
        public static DataTable get_dt_for_all_interface_per_dbo_with_key_and_description(string p_dbo_id, string p_type_id)
        {
            string m_method = "clsDB_interface_custom.get_dt_for_all_interface_per_dbo_with_key_and_description";
            DataTable dt = new DataTable();
            string p_dbo = "";
            Int32 i_dbo_id = 0;
            string p_interface_type = "";
            bool get_interface = true;
            try
            {
                if(p_type_id=="System.Data.DataRowView")
                {
                    get_interface = false;
                }
                if (get_interface == true)
                {
                    i_dbo_id = Convert.ToInt32(p_dbo_id);
                    p_dbo = lb.clsDB_dbo_custom.get_dbo_value_from_id(i_dbo_id);
                    p_interface_type = "Ingest";
                    dt = lb.clsDB_interface_gen.get_dt_interface_columns(dt);
                    clsSQLiteDB databaseObject = new clsSQLiteDB();
                    string sql = "Select * from interface";
                    sql = sql + " where dbo_id = '" + p_dbo_id + "'";
                    sql = sql + " and interface_type_id = '" + p_type_id + "'";
                    sql = sql + " order by interface";

                    SQLiteCommand myCommand = new SQLiteCommand(sql, databaseObject.myConfigConnection);
                    databaseObject.OpenConnection();
                    SQLiteDataReader result = myCommand.ExecuteReader();
                    //**************************************************
                    //* Add NO Interface
                    //**************************************************
                    DataRow dr = dt.NewRow();
                    dr["interface_id"] = 99999999;
                    dr["dbo_id"] = p_dbo_id;
                    dr["interface_type_id"] = 1;
                    dr["interface_key_and_interface_name"] = "No Interface";
                    dr["interface_key"] = "No Interface";
                    dr["interface"] = "No Interface";
                    dr["label"] = "";
                    dr["responsible_uid"] = "";
                    dt.Rows.Add(dr);
                    //**************************************************
                    //* Add All Interface
                    //**************************************************
                    dr = dt.NewRow();
                    dr["interface_id"] = 88888888;
                    dr["dbo_id"] = p_dbo_id;
                    dr["interface_type_id"] = 1;
                    dr["interface_key_and_interface_name"] = "All Interfaces";
                    dr["interface_key"] = "All Interfaces";
                    dr["interface"] = "All Interfaces";
                    dr["label"] = "";
                    dr["responsible_uid"] = "";
                    dt.Rows.Add(dr);
                    //**************************************************
                    //* Now add All interfaces
                    //**************************************************
                    if (result.HasRows)
                    {
                        while (result.Read())
                        {
                            dr = dt.NewRow();
                            dr["interface_id"] = result["interface_id"].ToString();
                            dr["dbo_id"] = result["dbo_id"].ToString();
                            dr["interface_type_id"] = result["interface_type_id"].ToString();
                            dr["interface_key_and_interface_name"] = result["interface_key"].ToString() + " - " + result["interface"].ToString();
                            dr["interface_key"] = result["interface_key"].ToString();
                            dr["interface"] = result["interface"].ToString();
                            dr["label"] = result["label"].ToString();
                            dr["responsible_uid"] = result["responsible_uid"].ToString();
                            dt.Rows.Add(dr);
                        }
                    }
                    databaseObject.CloseConnection();
                }


                return dt;
            }
            catch (Exception ex)
            {
                lb.clsLogger.WriteLog("Error in : " + m_method + " : " + ex.Message);
                return dt;
            }
        }
        public static DataTable get_dt_for_key_interface(string p_interface_id)
        {
            string m_method = "clsDB_interface_custom.get_dt_for_key_interface";
            DataTable dt = new DataTable();
            try
            {
                dt = lb.clsDB_interface_gen.get_dt_interface_columns(dt);
                clsSQLiteDB databaseObject = new clsSQLiteDB();
                string sql = "Select * from interface";
                sql = sql + " where interface_id = '" + p_interface_id + "'";
                SQLiteCommand myCommand = new SQLiteCommand(sql, databaseObject.myConfigConnection);
                databaseObject.OpenConnection();
                SQLiteDataReader result = myCommand.ExecuteReader();
                if (result.HasRows)
                {
                    while (result.Read())
                    {
                        DataRow dr = dt.NewRow();
                        dr["interface_id"] = result["interface_id"].ToString();
                        dr["dbo_id"] = result["dbo_id"].ToString();
                        dr["interface_type_id"] = result["interface_type_id"].ToString();
                        dr["interface_key"] = result["interface_key"].ToString();
                        dr["interface"] = result["interface"].ToString();
                        dr["label"] = result["label"].ToString();
                        dr["responsible_uid"] = result["responsible_uid"].ToString();
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
