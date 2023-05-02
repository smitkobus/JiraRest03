using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lb.Conn
{
    class clsConn
    {
        public static string get_conn(string db)
        {
            string Conn = "";
            switch (db)
            {
                case "dbzdtest09":
                    Conn = "Server=pta01608.bmwgroup.net;Port=62013;User ID=qqc04hb;Password=ver1e6st;Database=dbzdtest09";
                    break;
                case "tadb_next":
                    Conn = "Server=pc12444.bmwgroup.net;Port=40048;User ID=qqtbdb1;Password=XPms5F6WRu49WLVK6BTq;Database=tadbp";
                    break;
                default:
                    Conn = "Server=pta01608.bmwgroup.net;Port=62013;User ID=qqc04hb;Password=ver1e6st;Database=dbzdtest09";
                    break;
            }
            return Conn;
        }
    }
}
