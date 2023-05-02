using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using lb;
namespace lb
{
    class Log
    {
        public static bool WriteLog(string method, string UniqueID, string LogText)
        {
            bool bSuccessfull = true;
            DateTime time = DateTime.Now;
            string sYear = time.Year.ToString().PadLeft(4, '0');
            string sMonth = time.Month.ToString().PadLeft(2, '0');
            string sDay = time.Day.ToString().PadLeft(2, '0');
            string sHour = time.Hour.ToString().PadLeft(2, '0');
            string sMinute = time.Minute.ToString().PadLeft(2, '0');
            // Check for log Break 
            if (Globals.must_print_break_in_log_file == true)
            {
                Globals.must_print_break_in_log_file = false;
                WriteLogBreak();
            }
            // Path
            string sPath = System.IO.Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
            sPath = sPath + @"\Logs";
            if (!Directory.Exists(sPath))
            {
                Directory.CreateDirectory(sPath);
            }
            string sFull_Log_Path = sPath + @"\lb_log_" + sYear + sMonth + sDay + ".txt";
            LogText = DateTime.Now.ToString() + ":" + method + " : ID (" + UniqueID + ") : " + LogText;
            TextFile.write_text_to_file_by_type("Log", sFull_Log_Path, LogText);
            return bSuccessfull;
        }
        public static bool WriteLogBreak()
        {
            bool bSuccessfull = true;
            DateTime time = DateTime.Now;
            string sYear = time.Year.ToString().PadLeft(4, '0');
            string sMonth = time.Month.ToString().PadLeft(2, '0');
            string sDay = time.Day.ToString().PadLeft(2, '0');
            string sHour = time.Hour.ToString().PadLeft(2, '0');
            string sMinute = time.Minute.ToString().PadLeft(2, '0');
            // Path
            string sPath = System.IO.Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
            sPath = sPath + @"\Logs";
            if (!Directory.Exists(sPath))
            {
                Directory.CreateDirectory(sPath);
            }
            string sFull_Log_Path = sPath + @"\lb_log_" + sYear + sMonth + sDay + ".txt";
            string LogText = DateTime.Now.ToString() + ": ****************************************************************************************************************************";
            TextFile.write_text_to_file_by_type("Log", sFull_Log_Path, LogText);
            return bSuccessfull;
        }
    }
}
