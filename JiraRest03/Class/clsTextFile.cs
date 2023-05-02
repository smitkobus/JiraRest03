using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lb
{
    class TextFile
    {
        public static bool write_text_to_file_by_type(string type, string fullpath, string text)
        {
            bool bSuccessfull = true;
            string path;
            string file;
            switch (type)
            {
                case "Exception":
                    // Console.WriteLine("Write:Exception");
                    break;
                case "Log":
                    // Console.WriteLine("Write:Log");
                    break;
                case "Executed":
                    // Console.WriteLine("Write:Executed");
                    break;
                default:
                    // Console.WriteLine("Write:Not Handled");
                    break;

            }

            SplitPath(fullpath, out path, out file);
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            if (File.Exists(fullpath))
            {
                // File.AppendText(fullpath, text);
                using (StreamWriter swfile = File.AppendText(fullpath))
                {
                    swfile.WriteLine(text);
                }
            }
            else
            {
                using (System.IO.StreamWriter swfile = new System.IO.StreamWriter(fullpath))
                {
                    swfile.WriteLine(text);
                }
            }
            return bSuccessfull;
        }
        public static void SplitPath(string fullPath, out string path, out string file)
        {
            path = string.Empty;
            file = string.Empty;
            if (!string.IsNullOrEmpty(fullPath))
            {
                string strConstatnt = "Fixed";
                List<string> splitted = fullPath.Split(new char[] { '\\' }).ToList();
                int indexToFixed = splitted.IndexOf(strConstatnt);
                int indexForFile = (splitted.Count - 1);
                file = splitted[indexForFile];
                path = fullPath.Replace(file, "");
            }
        }
    }
}
