using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lb
{
    class clsDBO
    {
        //*********************************************************************************************
        //* Get JSON object for DBO
        //* json.Emit()
        //*********************************************************************************************
        //* Chilkat.JsonObject json = new Chilkat.JsonObject();
        //* bool success = true;
        //* (json,success) = lb.clsDBO.get_record();
        public static (Chilkat.JsonObject,bool) get_record(Int32 p_dbo_id,string p_dbo_display,string p_dbo_project_tag, string p_epic_project_id, string p_issue_project_id)
        {
            Chilkat.JsonObject json = new Chilkat.JsonObject();
            bool success;
            Int32 uid = 0;
            try
            {
                uid = lb.clsUID.get_uid("DBO");
                success = json.AddIntAt(-1, "dbo_id", uid);
                success = json.AddStringAt(-1, "dbo_display", p_dbo_display);
                success = json.AddStringAt(-1, "dbo_project_tag", p_dbo_project_tag);
                success = json.AddStringAt(-1, "epic_project_id", p_epic_project_id);
                success = json.AddStringAt(-1, "issue_project_id", p_issue_project_id);
                return (json,success);
            }
            catch (Exception ex)
            {
                success = false;
                return (json, success);
            }
        }
    }
}
