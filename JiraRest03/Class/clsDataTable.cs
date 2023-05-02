using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Data;

namespace lb
{
    class clsDataTable
    {
        public static DataTable ConvertCSVtoDataTable(string strFilePath)
        {
            StreamReader sr = new StreamReader(strFilePath);
            string[] headers = sr.ReadLine().Split(',');
            DataTable dt = new DataTable();
            //foreach (string header in headers)
            //{
            //    dt.Columns.Add(header);
            //}
            //while (!sr.EndOfStream)
            //{
            //    string[] rows = Regex.Split(sr.ReadLine(), ",(?=(?:[^\"]*\"[^\"]*\")*[^\"]*$)");
            //    DataRow dr = dt.NewRow();
            //    for (int i = 0; i < headers.Length; i++)
            //    {
            //        dr[i] = rows[i];
            //    }
            //    dt.Rows.Add(dr);
            //}
            return dt;
        }
        public static DataTable dt_filter(DataTable dt,string p_filter)
        {
            string m_method = "clsDataTable.filter_dt";
            String m_parent_summary = "";
            String m_summary = "";
            DataTable dt_clone;
            dt_clone = dt.Clone();
            bool m_load_record = false;
            try
            {
                p_filter = p_filter.ToLower();
                foreach (DataRow row in dt.Rows)
                {
                    //*
                    //**********************************************************************************
                    //*
                    //* MAKE SURE to CAST to LOWER.. 
                    //*
                    //**********************************************************************************
                    //*
                    m_load_record = false;
                    DataRow dr = dt_clone.NewRow();
                    m_summary = row["Summary"].ToString().ToLower();
                    m_parent_summary = row["parent_summary"].ToString().ToLower();
                    //*
                    //* Parent Summary
                    //*
                    if(m_parent_summary.Contains(p_filter))
                    {
                        m_load_record = true;
                    }
                    //*
                    //* Summary
                    //*
                    if (m_summary.Contains(p_filter))
                    {
                        m_load_record = true;
                    }

                    //
                    //  Load record if CONTAINS
                    //
                    if (m_load_record == true)
                    {
                        dt_clone.Rows.Add(row.ItemArray);
                    }
                    
                    //dr = row;
                    //dt_clone.Rows.Add(dr);
                }
                return dt_clone;
            }
            catch (Exception ex)
            {
                lb.clsLogger.WriteLog("Error in : " + m_method + " : " + ex.Message);
                return dt_clone;

            }
        }
    }
}
