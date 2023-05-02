using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace lb
{
    class clsExcel
    {
        public static string DataTableToCSV(DataTable datatable, string seperator)
        {
            StringBuilder sb = new StringBuilder();
            string m_val = "";
            for (int i = 0; i < datatable.Columns.Count; i++)
            {
                sb.Append(datatable.Columns[i]);
                if (i < datatable.Columns.Count - 1)
                    sb.Append(seperator);
            }
            sb.AppendLine();
            foreach (DataRow dr in datatable.Rows)
            {
                for (int i = 0; i < datatable.Columns.Count; i++)
                {
                    m_val = dr[i].ToString();
                    if(m_val.Contains(seperator))
                    {
                        m_val = dr[i].ToString();
                        m_val =  m_val.Replace(seperator, "qqqqq");
                    }
                    //sb.Append(dr[i].ToString());
                    sb.Append(m_val);

                    if (i < datatable.Columns.Count - 1)
                        sb.Append(seperator);
                }
                sb.AppendLine();
            }
            return sb.ToString();
        }
        public static DataTable ConvertCSVtoDataTable(string strFilePath)
        {
            DataTable dt = new DataTable();
            using (StreamReader sr = new StreamReader(strFilePath))
            {
                string[] headers = sr.ReadLine().Split(',');
                foreach (string header in headers)
                {
                    dt.Columns.Add(header);
                }
                while (!sr.EndOfStream)
                {
                    string[] rows = sr.ReadLine().Split(',');
                    DataRow dr = dt.NewRow();
                    for (int i = 0; i < headers.Length; i++)
                    {
                        dr[i] = rows[i];
                    }
                    dt.Rows.Add(dr);
                }

            }


            return dt;
        }
        public static DataTable csvToDataTable(string fileName, char splitCharacter)
        {
            DataTable CsvData = new DataTable();
            try
            {
                StreamReader sr = new StreamReader(fileName);
                string myStringRow = sr.ReadLine();
                var rows = myStringRow.Split(splitCharacter);
                
                foreach (string column in rows)
                {
                    //creates the columns of new datatable based on first row of csv
                    CsvData.Columns.Add(column);
                }
                myStringRow = sr.ReadLine();
                while (myStringRow != null)
                {
                    //myStringRow = myStringRow.Replace("qqqqq", ";");
                    //runs until string reader returns null and adds rows to dt 
                    rows = myStringRow.Split(splitCharacter);
                    CsvData.Rows.Add(rows);
                    myStringRow = sr.ReadLine();
                }
                sr.Close();
                sr.Dispose();
                //*********************************************************************************************
                //* Loop In Records and fix all "99999" to ";"
                //*********************************************************************************************
                foreach (DataRow dtRow in CsvData.Rows)
                {
                    //dtRow["label_list"] = dtRow["label_list"].ToString().ToLower();
                    dtRow["label_list"] = dtRow["label_list"].ToString().Replace("qqqqq",";");
                    //foreach (DataColumn dc in CsvData.Columns)
                    //{
                    //    //var field1 = dtRow[dc].ToString();

                    //}
                    //String myValue = row["ImagePath"].ToString();
                }

                return CsvData;

            }
            catch (Exception ex)
            {

                return CsvData;
            }
        }
        public static DataTable ConvertCSVtoDataTable_02(string strFilePath)
        {
            StreamReader sr = new StreamReader(strFilePath);
            string[] headers = sr.ReadLine().Split(';');
            DataTable dt = new DataTable();
            foreach (string header in headers)
            {
                dt.Columns.Add(header);
            }
            while (!sr.EndOfStream)
            {
                string[] rows = Regex.Split(sr.ReadLine(), ",(?=(?:[^\"]*\"[^\"]*\")*[^\"]*$)");
                DataRow dr = dt.NewRow();
                for (int i = 0; i < headers.Length; i++)
                {
                    dr[i] = rows[i];
                }
                dt.Rows.Add(dr);
            }
            return dt;
        }
    }
}
