using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lb
{
    class clsParameters
    {
        //*********************************************************************************************
        //* Get Hostname
        //*********************************************************************************************
        //*string hostname = lb.clsParameters.get_hostname();
        public static string get_hostname()
        {
            string hostname = "atc.bmwgroup.net";
            try
            {
                return hostname;
            }
            catch (Exception ex)
            {
                return hostname;
            }
        }

    }
}
