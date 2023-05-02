using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace lb
{
    public static class clsLogger
    {
        //try
        //{
        //}
        //catch (Exception ex)
        //{
        //    lb.clsLogger.WriteLog("FUNC:btnARBCurrencyPairToFix_Click with error:" + ex.Message);
        //}
        public static void WriteLog(string Message)
        {
            string path = "";
            string file = "";
            string sPath = System.IO.Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
            string logPath = "\\Log\\log.txt";
            string fullPath = sPath + logPath;
            lb.clsFile.SplitPath(fullPath, out path, out file);
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            using (StreamWriter writer = new StreamWriter(fullPath, true))
            {
                writer.WriteLine($"{DateTime.Now.Year}/{DateTime.Now.Month.ToString().PadLeft(2, '0')}/{DateTime.Now.Day.ToString().PadLeft(2, '0')} {DateTime.Now.Hour.ToString().PadLeft(2, '0')}:{DateTime.Now.Minute.ToString().PadLeft(2, '0')}:{DateTime.Now.Second.ToString().PadLeft(2, '0')}  : {Message}");
            }


        }
    }
}

