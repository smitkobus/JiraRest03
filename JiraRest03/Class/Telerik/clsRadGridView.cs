using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telerik.WinControls.UI;

namespace lb
{
    class clsRadGridView
    {
        //*********************************************************************************************
        //* Convert RadGrid to DATA TABLE
        //*********************************************************************************************
        //public static DataTable get_dt_from_grd(RadGridView p_grd)
        //{
        //    DataTable dt = new DataTable();
        //    Int32 i = 0;
        //    try
        //    {

        //        //  dt.Columns.Add("component");//col.HeaderText
        //        foreach (GridViewColumn column in p_grd.Columns)
        //        {
        //            if (column is GridViewDataColumn)
        //            {
        //                GridViewDataColumn col = column as GridViewDataColumn;
        //                if (col != null)
        //                {
        //                    col.Width = 90;
        //                    dt.Columns.Add(col.HeaderText);
        //                    col.HeaderText = "Column count : " + i.ToString();
                            
        //                    i++;
        //                }
        //            }
        //        }
        //        for (int r = 0; r < p_grd.RowCount; r++)
        //        {
        //            //s_checked = p_grd.Rows[r].Cells["checked"].Value.ToString();
        //            //if (s_checked == "True")
        //            //{
        //            //    DataRow dr = dt.NewRow();
        //            //    dr["component"] = p_grd.Rows[r].Cells["component"].Value.ToString();
        //            //    dt.Rows.Add(dr);
        //            //}

        //        }
        //        return dt;
        //    }
        //    catch (Exception ex)
        //    {
        //        return dt;
        //    }
        //}
    }
}
