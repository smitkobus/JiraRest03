using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace lb
{
    class clsConfigParameters
    {
        //*********************************************************************************************
        //* Get UID
        //*********************************************************************************************
        //*string m_uid = lb.clsConfigParameters.get_uid();
        public static string get_uid()
        {
            string uid = "";
            string m_FullString = "";
            string m_variable = "";
            string m_encrypt_flag = "";
            string m_value = "";
            try
            {
                string sPath = System.IO.Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
                string sFull_Config_Path = sPath + "\\conn.ini";
                m_FullString = lb.clsFile.read_text_file(sFull_Config_Path);
                //*
                //* Now Split the FIle
                //*
                uid = "";
                string[] encrypted_lines = m_FullString.Split(new[] { Environment.NewLine }, StringSplitOptions.None);
                foreach (string encrypted_line in encrypted_lines)
                {
                    if (encrypted_line.Trim().Length > 0)
                    {
                        string[] encrypt_attributes = encrypted_line.Split(';');
                        Int32 m_index = 0;
                        m_variable = "";
                        m_encrypt_flag = "";
                        foreach (string encrypt_attribute in encrypt_attributes)
                        {
                            m_index = m_index + 1;
                            switch (m_index)
                            {
                                case 1:
                                    m_variable = encrypt_attribute;
                                    break;

                                case 2:
                                    m_encrypt_flag = encrypt_attribute;
                                    break;

                                case 3:
                                    if (m_encrypt_flag == "N")
                                    {
                                        m_value = encrypt_attribute;
                                    }
                                    else
                                    {
                                        m_value = lb.clsEncryption.DecryptConnectionString(encrypt_attribute);
                                    }

                                    break;
                            }
                        }
                        switch (m_variable)
                        {
                            case "HOST":
                                break;

                            case "UID":
                                uid = m_value;
                                break;
                            case "PW":
                                break;
                        }
                    }

                    //uid = "qqzabd0";
                    //  uid = "qx46522";
                }
                if(uid=="")
                {
                    uid = "qx46522";
                }
                return uid;
            }
            catch (Exception ex)
            {
                return uid;
            }
        }
        //*********************************************************************************************
        //* Get Password
        //*********************************************************************************************
        //*string m_pw = lb.clsConfigParameters.get_pw();
        public static string get_pw()
        {
            string pw = "";
            try
            {
                //pw = "DDsema1-SSsema1-AAsema1-";
                //pw = "DDsema-SSsema-DDsema-";
                pw = "DDsema-FFsema-GGsema-";
                return pw;
            }
            catch (Exception ex)
            {
                return pw;
            }
        }
    }
}
