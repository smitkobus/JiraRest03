using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//namespace JiraRest02.Class.Telerik
namespace JiraRest02
{
    public static class DropDownList
    {
        public static string fix_cbo_value(string combo_value)
        {
            string method = "fix_cbo_value";
            string sprintid = "";
            try
            {
                    int pos = combo_value.IndexOf('[');
                if (pos > -1)
                {
                    pos = combo_value.IndexOf(',');
                    if (pos > -1)
                    {
                        combo_value = combo_value.Substring(1, pos - 1);
                    }
                    Console.WriteLine("The Index Value of character 'F' is " + pos);
                }

            }
            catch (Exception ex)
            {
                string error = ex + "";
                //clsLog.WriteLog(method, UniqueID, "Error :" + ex);
            }
            return combo_value;
        }
    }
}
