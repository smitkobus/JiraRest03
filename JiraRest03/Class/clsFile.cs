using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace lb
{
    class clsFile
    {
        public static bool write_text_to_file(string fullpath, string text)
        {
            bool bSuccessfull = true;
            string path;
            string file;

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
        //public static string get_latest_file_for_epic(string dbo,string pi,string status)
        public static string get_latest_file_for_epic(string dbo, string dbo_key)
        {
            string data_path = "";
            string path = "";
            string dbo_filter = "";
            DateTime lastDT = new DateTime();
            string latest_file = "";
            try
            {
                path = System.IO.Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
                //data_path = path + @"\Data\"+ dbo+"_"+ pi + "_" + status + @"\";
                data_path = path + @"\Data\" + dbo + @"\";
                dbo_filter = dbo_key + "*.*";
                //jira_key_filter = "*.*";
                string[] files = Directory.GetFiles(data_path, dbo_filter, SearchOption.TopDirectoryOnly);
                //string[] files = Directory.GetFiles(data_path, jira_key_filter, SearchOption.AllDirectories);
                System.IO.FileInfo fi = null;

                foreach (string file in files)
                {
                    fi = new System.IO.FileInfo(file);
                    if (lastDT == DateTime.MinValue)
                    {
                        lastDT = fi.LastWriteTimeUtc;
                        latest_file = file;
                    }
                    if (lastDT < fi.LastWriteTimeUtc)
                    {
                        lastDT = fi.LastWriteTimeUtc;
                        latest_file = file;
                    }
                    // The `state` variable takes on the value of an element in `states` and updates every iteration.
                    Console.WriteLine(file);
                }
                return latest_file;
            }
            catch (Exception ex)
            {
                return latest_file;

            }
        }
        //public static string get_latest_file_for_jira_key(string jira_key, string dbo, string pi, string status)
        public static string get_latest_file_for_dbo_id_and_jira_key(Int32 p_dbo_id, string p_epic_key)
        {
            string data_path = "";
            string path = "";
            string jira_key_filter = "";
            DateTime lastDT = new DateTime();
            string latest_file = "";
            string m_dbo = "";
            string sFull_Path = "";
            try
            {
                //                dbo = lb.clsDB_dbo_custom.get_dbo_value_from_id(Convert.ToInt32(p_dbo_id));
                m_dbo = lb.clsDB_dbo_custom.get_dbo_value_from_id(p_dbo_id);
                path = System.IO.Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
                // data_path = path + @"\Data\" + dbo + "_" + pi + "_" + status + @"\";
                data_path = path + @"\Data\" + m_dbo + @"\";
                if (p_epic_key == "EXXD-2547")
                {

                }
                delete_files_for_jira_key_that_is_empty(data_path, p_epic_key, p_dbo_id.ToString());
                jira_key_filter = p_epic_key + "*.*";
                //jira_key_filter = "*.*";
                string[] files = Directory.GetFiles(data_path, jira_key_filter, SearchOption.TopDirectoryOnly);
                //string[] files = Directory.GetFiles(data_path, jira_key_filter, SearchOption.AllDirectories);
                System.IO.FileInfo fi = null;

                foreach (string file in files)
                {
                    fi = new System.IO.FileInfo(file);
                    if (lastDT == DateTime.MinValue)
                    {
                        lastDT = fi.LastWriteTimeUtc;
                        latest_file = file;
                    }
                    if (lastDT < fi.LastWriteTimeUtc)
                    {
                        lastDT = fi.LastWriteTimeUtc;
                        latest_file = file;
                    }
                    // The `state` variable takes on the value of an element in `states` and updates every iteration.
                    Console.WriteLine(file);
                }
                if (latest_file == "")
                {
                    //Get Now
                    DateTimeOffset now = (DateTimeOffset)DateTime.Now;
                    string ts = now.ToString("yyyyMMdd_HH_mm_ss_fff");
                    //data_path = lb.clsFile.get_data_path() + dbo + "_" + pi + "_" + status + @"\" + dbo_key;
                    data_path = lb.clsFile.get_data_path() + m_dbo.Trim() + @"\" + p_epic_key.Trim();
                    sFull_Path = lb.clsJira.save_json_epic_file(m_dbo, p_epic_key);
                    latest_file = sFull_Path;
                }

                return latest_file;
            }
            catch (Exception ex)
            {
                return latest_file;

            }
        }
        public static void delete_files_for_jira_key_that_is_empty(string s_path, string p_jira_key, string p_dbo_id)
        {
            string sFile = "";
            try
            {
                DirectoryInfo d = new DirectoryInfo(s_path); //Assuming Test is your Folder

                FileInfo[] Files = d.GetFiles(p_jira_key + "*.*"); //Getting Text files

                if (p_jira_key == "EXXD-2547")
                {

                }
                foreach (FileInfo file in Files)
                {
                    sFile = file.Name;
                    if (file.Length < 1)
                    {

                    }
                }

            }
            catch (Exception ex)
            {


            }
        }
        public static string get_data_path()
        {
            string data_path = "";
            string path = "";
            try
            {
                path = System.IO.Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
                data_path = path + @"\Data\";

                return data_path;
            }
            catch (Exception ex)
            {
                return data_path;

            }
        }
        public static (string, string) get_first_and_last_occurency_in_a_comma_delimeted_string(string comma_delimeted_string, string separators)
        {
            string first = "";
            string last = "";
            try
            {
                string[] values = comma_delimeted_string.Split(separators.ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                first = values[0];
                last = values[values.Length - 1];
                return (first, last);
            }
            catch (Exception ex)
            {
                return (first, last);
                MessageBox.Show("Error" + ex);
            }
        }
        public static (int, string) write_string_to_file(string text_to_file, string folder_prefix, string folder_suffix, string file_prefix, string folder_date_level, string file_have_timestamp_build_in)
        {
            int ret_code = 0;
            DateTime time = DateTime.Now;
            DateTime foo = DateTime.Now;
            long unixTimeSec = ((DateTimeOffset)foo).ToUnixTimeSeconds();
            long unixTimeMillSec = ((DateTimeOffset)foo).ToUnixTimeMilliseconds();
            string folder = "";
            string sYear = time.Year.ToString().PadLeft(4, '0');
            string sMonth = time.Month.ToString().PadLeft(2, '0');
            string sDay = time.Day.ToString().PadLeft(2, '0');
            string sHour = time.Hour.ToString().PadLeft(2, '0');
            string sMinute = time.Minute.ToString().PadLeft(2, '0');
            string sSeconds = time.Second.ToString().PadLeft(2, '0');
            string sMilliSeconds = time.Millisecond.ToString().PadLeft(3, '0');
            // File Path
            //  string folder_with_date = folder;
            string sPath = System.IO.Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
            string sPath_Path = sPath + @"\Data\";
            // sPath = sPath + @"\Logs";
            //
            switch (folder_date_level)
            {
                case "M": //Minute
                    folder = folder_prefix + sYear + @"\" + sMonth + @"\" + sDay + @"\" + sHour + @"\" + sMinute + @"\" + folder_suffix;
                    break;
                case "H": //Hour
                    folder = folder_prefix + sYear + @"\" + sMonth + @"\" + sDay + @"\" + sHour + @"\" + folder_suffix;
                    break;
                case "D": //Day
                    folder = folder_prefix + sYear + @"\" + sMonth + @"\" + sDay + @"\" + folder_suffix;
                    break;
                default: //None
                    folder = folder_prefix + folder_suffix;
                    break;
            }
            //
            sPath = sPath + @"\" + folder;
            if (!Directory.Exists(sPath))
            {
                Directory.CreateDirectory(sPath);
            }
            string full_file_path = "";
            if (file_have_timestamp_build_in == "Y")
            {
                full_file_path = sPath + @"\" + file_prefix + "_" + sYear + sMonth + sDay + "_" + sHour + sMinute + sSeconds + "_" + sMilliSeconds + ".txt";
            }
            if (file_have_timestamp_build_in == "N")
            {
                full_file_path = sPath + @"\" + file_prefix + ".txt";
            }
            clsFile.write_text_to_file_by_type("Log", full_file_path, text_to_file);
            string path_log = sPath_Path + @"log_path.txt";
            clsFile.write_text_to_file_by_type("Log", path_log, full_file_path);
            //(ret_code, full_file_path) = lb.clsFile.write_file(kraken_json, folder_prefix, folder_suffix, file_prefix, folder_type);
            return (ret_code, full_file_path);
        }
        public static (int, string) write_file(Chilkat.StringBuilder kraken_json, string folder_prefix, string folder_suffix, string file_prefix, string folder_date_level, string file_have_timestamp_build_in)
        {
            int ret_code = 0;
            DateTime time = DateTime.Now;
            DateTime foo = DateTime.Now;
            long unixTimeSec = ((DateTimeOffset)foo).ToUnixTimeSeconds();
            long unixTimeMillSec = ((DateTimeOffset)foo).ToUnixTimeMilliseconds();
            string folder = "";
            string sYear = time.Year.ToString().PadLeft(4, '0');
            string sMonth = time.Month.ToString().PadLeft(2, '0');
            string sDay = time.Day.ToString().PadLeft(2, '0');
            string sHour = time.Hour.ToString().PadLeft(2, '0');
            string sMinute = time.Minute.ToString().PadLeft(2, '0');
            string sSeconds = time.Second.ToString().PadLeft(2, '0');
            string sMilliSeconds = time.Millisecond.ToString().PadLeft(3, '0');
            // File Path
            //  string folder_with_date = folder;
            string sPath = System.IO.Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
            string sPath_Path = sPath + @"\Data\";
            // sPath = sPath + @"\Logs";
            //
            switch (folder_date_level)
            {
                case "M": //Minute
                    folder = folder_prefix + sYear + @"\" + sMonth + @"\" + sDay + @"\" + sHour + @"\" + sMinute + @"\" + folder_suffix;
                    break;
                case "H": //Hour
                    folder = folder_prefix + sYear + @"\" + sMonth + @"\" + sDay + @"\" + sHour + @"\" + folder_suffix;
                    break;
                case "D": //Day
                    folder = folder_prefix + sYear + @"\" + sMonth + @"\" + sDay + @"\" + folder_suffix;
                    break;
                default: //None
                    folder = folder_prefix + folder_suffix;
                    break;
            }
            //
            sPath = sPath + @"\" + folder;
            if (!Directory.Exists(sPath))
            {
                Directory.CreateDirectory(sPath);
            }
            string full_file_path = "";
            if (file_have_timestamp_build_in == "Y")
            {
                full_file_path = sPath + @"\" + file_prefix + "_" + sYear + sMonth + sDay + "_" + sHour + sMinute + sSeconds + "_" + sMilliSeconds + ".txt";
            }
            if (file_have_timestamp_build_in == "N")
            {
                full_file_path = sPath + @"\" + file_prefix + ".txt";
            }
            clsFile.write_text_to_file_by_type("Log", full_file_path, kraken_json.ToString());
            string path_log = sPath_Path + @"log_path.txt";
            clsFile.write_text_to_file_by_type("Log", path_log, full_file_path);
            //(ret_code, full_file_path) = lb.clsFile.write_file(kraken_json, folder_prefix, folder_suffix, file_prefix, folder_type);
            return (ret_code, full_file_path);
        }
        public static bool write_text_to_file_by_type(string type, string fullpath, string text, bool append = true)
        {
            bool bSuccessfull = true;
            try
            {

                string path;
                string file;
                switch (type)
                {
                    case "Exception":
                        // Console.WriteLine("Write:Exception");
                        break;
                    case "Log":
                        Console.WriteLine("Write:Log");
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
                    if (append == false)
                    {
                        File.Delete(fullpath);
                    }

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
            catch (Exception ex)
            {
                lb.clsLogger.WriteLog("Error:" + ex.Message);
                bSuccessfull = false;
                return bSuccessfull;

            }

        }
        //*
        //* Move to Parameter Path (Eg "processed")
        //*
        public static void MoveFile(string full_path_from, string subfolder_to_move_to)

        {
            string processed_path = "";
            string path = "";
            string file = "";
            string filename = "";
            string extention = "";
            string full_path_to = "";
            lb.clsFile.SplitPath(full_path_from, out path, out file);
            lb.clsFile.Splitfile(file, out filename, out extention);
            processed_path = path + @"\" + subfolder_to_move_to + @"\";
            full_path_to = processed_path + file;
            if (!Directory.Exists(processed_path))
            {
                Directory.CreateDirectory(processed_path);
            }
            File.Move(full_path_from, full_path_to);

        }
        //Splitfile(file, out filename, out extention);
        public static void Splitfile(string file, out string filename, out string extention)
        {
            filename = string.Empty;
            extention = string.Empty;
            if (!string.IsNullOrEmpty(file))
            {
                string strConstatnt = "Fixed";
                List<string> splitted = file.Split(new char[] { '.' }).ToList();
                //int indexToFixed = splitted.IndexOf(strConstatnt);
                //int indexForFile = (splitted.Count - 1);
                filename = splitted[0];
                extention = splitted[1];
                //extention = file.Replace(file, "");
            }
        }
        public static void SplitPath(string fullPath, out string path, out string file)
        {
            //string path = "";
            //string file = "";
            // lb.clsFile.SplitPath(fullPath, out path, out file);
            try
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
                    if (file.Length > 0)
                    {
                        path = fullPath.Replace(file, "");
                    }
                }
            }
            catch (Exception ex)
            {
                path = string.Empty;
                file = string.Empty;
                lb.clsLogger.WriteLog("Error:" + ex.Message);
            }
        }
        public static string read_text_file(string p_fullPath)
        {
            //string m_file_context = lb.clsFile.read_text_file(m_fullPath);
            string m_file_context = "";
            try
            {
                m_file_context = System.IO.File.ReadAllText(p_fullPath);
                return m_file_context;
            }
            catch (Exception ex)
            {
                lb.clsLogger.WriteLog("Error:" + ex.Message);
                return m_file_context;
            }
        }
    }
}

