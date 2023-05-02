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
    class clsUID
    {
        //*********************************************************************************************
        //* Get JSON object for UID
        //* json.Emit()
        //*********************************************************************************************
        //* Chilkat.JsonObject json = new Chilkat.JsonObject();
        //* bool success = true;
        //* (json,success) = lb.clsUID.get_record("DBO",1);
        public static (Chilkat.JsonObject, bool) get_record(string p_table, Int32 p_uid)
        {
            Chilkat.JsonObject json = new Chilkat.JsonObject();
            bool success;
            try
            {
                success = json.AddStringAt(-1, "table", p_table);
                success = json.AddIntAt(-1, "uid", p_uid);
                return (json, success);
            }
            catch (Exception ex)
            {
                success = false;
                return (json, success);
            }
        }
        //*********************************************************************************************
        //* Get UID for a Table
        //*********************************************************************************************
        //* Chilkat.JsonObject json = new Chilkat.JsonObject();
        //* Int32 uid = 1;
        //* uid = lb.clsUID.get_uid("DBO");
        public static Int32 get_uid(string p_table)
        {
            Chilkat.JsonObject json = new Chilkat.JsonObject();
            string path = "";
            string file = "uid.txt";
            string sPath = System.IO.Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
            //string ConfigPath = ConfigurationManager.AppSettings["configPath"];
            string ConfigPath = "\\config\\uid.txt";
            string fullConfigPath = "";
            bool success;
            Int32 p_uid = 0;
            try
            {
                fullConfigPath = sPath + ConfigPath + file;
                if (File.Exists(fullConfigPath))
                {

                }
                else
                {
                    p_uid = 1;
                    (json, success) = lb.clsUID.get_record(p_table, p_uid);
                    lb.clsFile.write_text_to_file(fullConfigPath, json.Emit());
                }
                    // if file exist
                    //     if TABLE exist
                    //         Store UID + add 1 and save again
                    //     else
                    //         uid = 1 and add table to file and create file
                    // else
                    //     uid = 1 and add table to file and create file
                    return p_uid;
            }
            catch (Exception ex)
            {
                success = false;
                return p_uid;
            }
        }
    }
}
