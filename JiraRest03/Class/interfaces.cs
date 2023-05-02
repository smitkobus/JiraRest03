using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lb
{
    class interfaces
    {
        public static System.Data.DataTable get_interface_dt(string path)
        {
            string json2 = System.IO.File.ReadAllText(path);
            string strJson = json2;
            Chilkat.JsonObject json = new Chilkat.JsonObject();
            json.Load(strJson);
            json.EmitCompact = false;
            string strFormattedJson = json.Emit();
            //Chilkat.JsonArray jarr = new Chilkat.JsonArray();
            //jarr.AddObjectAt(-1);
            //Chilkat.JsonObject jsonObj_1 = jarr.ObjectAt(jarr.Size - 1);
            //jsonObj_1.UpdateString("interface.ingest_type", "Data Source");
            //jsonObj_1.UpdateString("interface.key", "VoCCR1");
            //jsonObj_1.UpdateString("interface.name", "Voice of the Customer1");

            //jarr.AddObjectAt(-1);
            //jsonObj_1 = jarr.ObjectAt(jarr.Size - 1);
            //jsonObj_1.UpdateString("interface.ingest_type", "Data Source");
            //jsonObj_1.UpdateString("interface.key", "VoCCR2");
            //jsonObj_1.UpdateString("interface.name", "Voice of the Customer2");

            //jarr.AddObjectAt(-1);
            //jsonObj_1 = jarr.ObjectAt(jarr.Size - 1);
            //jsonObj_1.UpdateString("interface.ingest_type", "Data Source");
            //jsonObj_1.UpdateString("interface.key", "VoCCR3");
            //jsonObj_1.UpdateString("interface.name", "Voice of the Customer3");

            //  Insert code here to load the above JSON array into the jarr object.

            //Chilkat.JsonObject json = null;
            //string interfaceIngest_type;
            //string interfaceKey;
            //string interfaceName;

            //int i = 0;
            //int count_i = jarr.Size;
            //while (i < count_i)
            //{
            //    json = jarr.ObjectAt(i);
            //    interfaceIngest_type = json.StringOf("interface.ingest_type");
            //    interfaceKey = json.StringOf("interface.key");
            //    interfaceName = json.StringOf("interface.name");

            //    i = i + 1;
            //}











            //Chilkat.JsonObject jsonResponse = new Chilkat.JsonObject();
            //Chilkat.StringBuilder sb = new Chilkat.StringBuilder();
            //sb.Append(jsontext);
            //jsonResponse.LoadSb(sb);
            //int i = 0;
            //int count_i = jsonResponse.SizeOfArray("interface");
            //string summary = "";
            //summary = jsonResponse.StringOf("fields.summary"); ;
            //return summary;
            System.Data.DataTable dt = new System.Data.DataTable();
            return dt;
        }
        public static string get_summary(Chilkat.JsonObject jsonResponse)
        {
            string summary = "";
            summary = jsonResponse.StringOf("fields.summary"); ;
            return summary;
        }
    }
}
