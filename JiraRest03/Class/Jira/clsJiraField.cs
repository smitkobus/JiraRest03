using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lb
{
    class clsJiraField
    {
        //m_json = lb.clsJiraField.add_current_acceptance_criteria_customfield_11100_child(m_json,dtAcceptance);
        //*********************************************************************************************
        //* Add Current Acceptance to CHILKAT JSON opject
        //*********************************************************************************************
        public static Chilkat.JsonObject add_new_acceptance_to_current_acceptance(Chilkat.JsonObject json, DataTable dtCurrentAcceptance,string p_new_acceptance)
        {
            Int32 m_id = 0;
            string p_acceptance = "";
            string p_checked = "";
            string field_name = "";
            Int32 row_number = 0;
            bool success;
            bool m_checked = true;
            try
            {
                // Add the array for Price
                success = json.AddArrayAt(-1, "customfield_11100");
                Chilkat.JsonArray aCustomfield_11100 = json.ArrayAt(json.Size - 1);

                //json.EmitSb(sbRequestBody);
                Chilkat.JsonObject priceObj;
                foreach (DataRow row in dtCurrentAcceptance.Rows)
                {
                    success = aCustomfield_11100.AddObjectAt(-1);
                    // Get the object that was just appended.
                    priceObj = aCustomfield_11100.ObjectAt(aCustomfield_11100.Size - 1);
                    // ID
                    m_id = Convert.ToInt32(row["id"]);
                    //success = priceObj.AddIntAt(-1, "id", m_id);
                    // Name
                    p_acceptance = row["name"].ToString();
                    success = priceObj.AddStringAt(-1, "name", p_acceptance);
                    //success = priceObj.AddBoolAt(-1, "checked", m_checked);
                }
                //*
                //* Add NEW Acceptance
                //*
               
                success = aCustomfield_11100.AddObjectAt(-1);
                // Get the object that was just appended.
                priceObj = aCustomfield_11100.ObjectAt(aCustomfield_11100.Size - 1);
                // ID
                m_id = m_id + 1;
                //success = priceObj.AddIntAt(-1, "id", m_id);
                // Name
                m_checked = false;
                success = priceObj.AddStringAt(-1, "name", p_new_acceptance);
                //success = priceObj.AddBoolAt(-1, "checked", m_checked);
                return json;
            }
            catch (Exception ex)
            {
                return json;
            }
        }
        //m_json = lb.clsJiraField.add_acceptance_criteria_customfield_11100_child(m_json,dtAcceptance);
        //*********************************************************************************************
        //* Add Acceptance to CHILKAT JSON opject
        //*********************************************************************************************
        public static Chilkat.JsonObject add_acceptance_criteria_customfield_11100_child(Chilkat.JsonObject json, DataTable dtAcceptance)
        {
            string p_acceptance = "";
            string field_name = "";
            Int32 row_number = 0;
            try
            {
                //Chilkat.StringBuilder sbRequestBody = new Chilkat.StringBuilder();
                //json.EmitSb(sbRequestBody);
                foreach (DataRow row in dtAcceptance.Rows)
                {
                    row_number = row_number + 1;
                    p_acceptance = row["template_child_acceptance"].ToString();
                    field_name = "fields.customfield_11100[xxx].name";
                    field_name = field_name.Replace("xxx", row_number.ToString());
                    json.UpdateString(field_name, p_acceptance);
                }
                return json;
            }
            catch (Exception ex)
            {
                return json;
            }
        }
        //m_json = lb.clsJiraField.add_acceptance_criteria_customfield_11100_parent(m_json,dtAcceptance);
        //*********************************************************************************************
        //* Add Acceptance to CHILKAT JSON opject
        //*********************************************************************************************
        public static Chilkat.JsonObject add_acceptance_criteria_customfield_11100_parent(Chilkat.JsonObject json, DataTable dtAcceptance )
        {
            string m_acceptance = "";
            bool m_isHeader = false;
            string field_name = "";
            Int32 row_number = 0;
            try
            {
                foreach (DataRow row in dtAcceptance.Rows)
                {
                    row_number = row_number + 1;
                    //*
                    //* Acceptance
                    //*
                    m_acceptance = row["template_parent_acceptance"].ToString();
                    field_name = "fields.customfield_11100[xxx].name";
                    field_name = field_name.Replace("xxx", row_number.ToString());
                    json.UpdateString(field_name, m_acceptance);
                    //*
                    //* Is Header
                    //*
                    m_isHeader = Convert.ToBoolean(row["isHeader"]);
                    field_name = "fields.customfield_11100[xxx].isHeader";
                    field_name = field_name.Replace("xxx", row_number.ToString());
                    json.UpdateBool(field_name, m_isHeader);
                }
                return json;
            }
            catch (Exception ex)
            {
                return json;
            }
        }
        //m_json = lb.clsJiraField.add_assignee_by_name(m_json,p_name);
        //*********************************************************************************************
        //* Add Assignee to CHILKAT JSON opject
        //*********************************************************************************************
        public static Chilkat.JsonObject add_assignee_by_name(Chilkat.JsonObject json, string p_name)
        {
            try
            {
                json.UpdateString("fields.assignee.name", p_name);
                return json;
            }
            catch (Exception ex)
            {
                return json;
            }
        }
        //m_json = lb.clsJiraField.add_component(m_json,dtComponents);
        //*********************************************************************************************
        //* Add Acceptance to CHILKAT JSON opject
        //*********************************************************************************************
        public static Chilkat.JsonObject add_component(Chilkat.JsonObject json, DataTable dtComponents)
        {
            string p_component = "";
            string field_name = "";
            Int32 row_number = 0;
            try
            {

                foreach (DataRow row in dtComponents.Rows)
                {
                    row_number = row_number + 1;
                    p_component = row["component"].ToString();
                    field_name = "fields.components[xxx].name";
                    field_name = field_name.Replace("xxx", row_number.ToString());
                    json.UpdateString(field_name, p_component);
                }
                
                return json;
            }
            catch (Exception ex)
            {
                return json;
            }
        }
        //m_json = lb.clsJiraField.add_description(m_json,p_description);
        //*********************************************************************************************
        //* Add Description to CHILKAT JSON opject
        //*********************************************************************************************
        public static Chilkat.JsonObject add_description(Chilkat.JsonObject json, string p_description)
        {
            try
            {
                json.UpdateString("fields.description", p_description);
                return json;
            }
            catch (Exception ex)
            {
                return json;
            }
        }
        //m_json = lb.clsJiraField.add_epic_link(m_json,p_epic_link);
        //*********************************************************************************************
        //* Add Parent Link to CHILKAT JSON opject
        //*********************************************************************************************
        public static Chilkat.JsonObject add_epic_link(Chilkat.JsonObject json, string p_epic_link)
        {
            try
            {
                json.UpdateString("fields.customfield_10001", p_epic_link);
                return json;
            }
            catch (Exception ex)
            {
                return json;
            }
        }
        //m_json = lb.clsJiraField.add_fix_version(m_json,p_fix_version);
        //*********************************************************************************************
        //* Add FIX VERSION to CHILKAT JSON opject
        //*********************************************************************************************
        public static Chilkat.JsonObject add_fix_version(Chilkat.JsonObject json, string p_fix_version)
        {
            try
            {
                json.UpdateString("fields.fixVersions[0].name", p_fix_version);
                return json;
            }
            catch (Exception ex)
            {
                return json;
            }
        }

        //m_json = lb.clsJiraField.add_issuetype_id(m_json,p_issue_type);
        //*********************************************************************************************
        //* Add Issue Type to CHILKAT JSON opject
        //*********************************************************************************************
        public static Chilkat.JsonObject add_issuetype_id(Chilkat.JsonObject json, string p_issue_type)
        {
            try
            {
                json.UpdateString("fields.issuetype.id", p_issue_type);
                return json;
            }
            catch (Exception ex)
            {
                return json;
            }
        }
        //m_json = lb.clsJiraField.add_parent_link(m_json,p_parent_link);
        //*********************************************************************************************
        //* Add Parent Link to CHILKAT JSON opject
        //*********************************************************************************************
        public static Chilkat.JsonObject add_parent_link(Chilkat.JsonObject json, string p_parent_link)
        {
            try
            {
                json.UpdateString("fields.customfield_11201", p_parent_link);
                return json;
            }
            catch (Exception ex)
            {
                return json;
            }
        }
        //m_json = lb.clsJiraField.add_priority(m_json,p_priority);
        //*********************************************************************************************
        //* Add Priority to CHILKAT JSON opject
        //* 3 = Medium
        //*********************************************************************************************
        public static Chilkat.JsonObject add_priority(Chilkat.JsonObject json, string p_priority)
        {
            try
            {
                json.UpdateString("fields.priority.id", p_priority);
                return json;
            }
            catch (Exception ex)
            {
                return json;
            }
        }
        //m_json = lb.clsJiraField.add_project_key(m_json,p_project);
        //*********************************************************************************************
        //* Add Project KEY to CHILKAT JSON opject
        //*********************************************************************************************
        public static Chilkat.JsonObject add_project_key(Chilkat.JsonObject json, string p_project)
        {
            try
            {
                json.UpdateString("fields.project.key", p_project);
                return json;
            }
            catch (Exception ex)
            {
                return json;
            }
        }
        //m_json = lb.clsJiraField.add_reporter_by_name(m_json,p_name);
        //*********************************************************************************************
        //* Add Reporter to CHILKAT JSON opject
        //*********************************************************************************************
        public static Chilkat.JsonObject add_reporter_by_name(Chilkat.JsonObject json, string p_name)
        {
            try
            {
                json.UpdateString("fields.reporter.name", p_name);
                return json;
            }
            catch (Exception ex)
            {
                return json;
            }
        }
        //m_json = lb.clsJiraField.add_status(m_json,p_status);
        //*********************************************************************************************
        //* Add STATUS to CHILKAT JSON opject
        //*********************************************************************************************
        public static Chilkat.JsonObject add_status(Chilkat.JsonObject json, string p_status)
        {
            try
            {
                json.UpdateString("fields.status.name", p_status);
                return json;
            }
            catch (Exception ex)
            {
                return json;
            }
        }
        //m_json = lb.clsJiraField.add_summary(m_json,p_summary);
        //*********************************************************************************************
        //* Add Summary to CHILKAT JSON opject
        //*********************************************************************************************
        public static Chilkat.JsonObject add_summary(Chilkat.JsonObject json, string p_summary)
        {
            try
            {
                json.UpdateString("fields.summary", p_summary);
                return json;
            }
            catch (Exception ex)
            {
                return json;
            }
        }
    }
}
