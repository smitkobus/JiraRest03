using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lb
{
    class clsChilKat
    {
        //*
        //***********************************************************************************************
        //*
        //*  Get STRING BUILDER from String
        //*
        //***********************************************************************************************
        //*Chilkat.StringBuilder sbResponseBody = lb.clsChilKat.get_stringbuilder_from_string(m_parameter);
        public static Chilkat.StringBuilder get_stringbuilder_from_string(string p_string)
        {
            Chilkat.StringBuilder sbResponseBody = new Chilkat.StringBuilder();
            sbResponseBody.Append(p_string);
            return sbResponseBody;
        }
        //*
        //***********************************************************************************************
        //*
        //*  Get STRING BUILDER from FILE PATH
        //*
        //***********************************************************************************************
        //*StringBuilder sbResponseBody = lb.clsChilKat.get_stringbuilder_from_file_path(file_path);
        public static Chilkat.StringBuilder get_stringbuilder_from_file_path(string file_path)
        {
            string text = File.ReadAllText(file_path);
            Chilkat.StringBuilder sbResponseBody = new Chilkat.StringBuilder();
            sbResponseBody.Append(text);
            return sbResponseBody;
        }
        //public static StringBuilder get_stringbuilder_from_file_path(string file_path)
        //{

        //    string text = File.ReadAllText(file_path);
        //    StringBuilder sbResponseBody = new StringBuilder(text);
        //    return sbResponseBody;
        //}
        public static string convert_JsonObject_to_StringBuilder(Chilkat.StringBuilder json_string)
        {
            Chilkat.JsonObject jsonResponse = new Chilkat.JsonObject();
            Chilkat.StringBuilder sbResponseBody = new Chilkat.StringBuilder();
            jsonResponse.LoadSb(json_string);
            return "";
        }
        public static Chilkat.JsonArray convert_text_to_json_array(string strJsonArray)

        {
            Chilkat.JsonArray jsonA = new Chilkat.JsonArray();
            bool success = jsonA.Load(strJsonArray);
            return jsonA;
        }
    }
}
