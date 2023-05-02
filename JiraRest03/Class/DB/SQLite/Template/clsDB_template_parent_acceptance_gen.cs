﻿using System;
using System.Data;
using System.Data.SQLite;
namespace lb
{
    class clsDB_template_parent_acceptance_gen
    {
        public static Boolean insert(string p_template_parent_acceptance, string p_template_hdr_id, string p_isHeader)
        {
            string m_method = "clsDB_template_parent_acceptance_gen.insert";
            try
            {
                clsSQLiteDB databaseObject = new clsSQLiteDB();
                string sql = "Insert into template_parent_acceptance ('template_parent_acceptance_id','template_parent_acceptance','template_hdr_id','isHeader')";
                sql = sql + "values (null,'" + p_template_parent_acceptance + "','" + p_template_hdr_id + "','" + p_isHeader + "')";
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
        public static Boolean update(string p_template_parent_acceptance_id, string p_template_parent_acceptance, string p_template_hdr_id, string p_isHeader)
        {
            string m_method = "clsDB_template_parent_acceptance_gen.update";
            try
            {
                clsSQLiteDB databaseObject = new clsSQLiteDB();
                string sql = "Update template_parent_acceptance set";
                sql = sql + " template_parent_acceptance = '" + p_template_parent_acceptance + "',";
                sql = sql + " template_hdr_id = '" + p_template_hdr_id + "',";
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
        public static DataTable get_dt_for_all_template_parent_acceptance()
        {
            string m_method = "clsDB_template_parent_acceptance_gen.get_dt_for_all_template_parent_acceptance";
            DataTable dt = new DataTable();
            try
            {
                dt = get_dt_template_parent_acceptance_columns(dt);
                clsSQLiteDB databaseObject = new clsSQLiteDB();
                string sql = "Select * from template_parent_acceptance";
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
        public static DataTable get_dt_template_parent_acceptance_columns(DataTable p_dt)
        {
            string m_method = "clsDB_template_parent_acceptance_gen.get_dt_template_parent_acceptance_columns";
            try
            {
                //*
                //* Is Header
                //*
                DataColumn colIsHeader = new DataColumn("isHeader");
                colIsHeader.DataType = System.Type.GetType("System.Boolean");
                //*
                //* Rest
                //*
                p_dt.Columns.Add(colIsHeader);
                p_dt.Columns.Add("template_parent_acceptance_id");
                p_dt.Columns.Add("template_parent_acceptance");
                p_dt.Columns.Add("template_hdr_id");
               
                return p_dt;
            }
            catch (Exception ex)
            {
                lb.clsLogger.WriteLog("Error in : " + m_method + " : " + ex.Message);
                return p_dt;
            }
        }
    }
}
