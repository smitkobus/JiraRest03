using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using Telerik.WinControls;
using Telerik.WinControls.UI;

namespace JiraRest03.Forms
{
    public partial class frmEpic_to_Story_Creator : Telerik.WinControls.UI.RadForm
    {
        public frmEpic_to_Story_Creator()
        {
            InitializeComponent();
        }
        //*********************************************************************************************
        //* Get DT Layout for an EPIC
        //*********************************************************************************************
        //DataTable dtEpics = get_dt_layout_for_epic();
        public static DataTable get_dt_layout_for_epic()
        {
            string m_method = "frmEpic_to_Story_Creator.get_dt_layout_for_epic";
            DataTable dt;
            dt = new DataTable();
            try
            {
                //********************************************************
                //* Add DataTable Columns According the field Selection
                //********************************************************
                dt.Columns.Add("Id");
                dt.Columns["Id"].DataType = Type.GetType("System.Int32");
                dt.Columns.Add("btn03");
                dt.Columns.Add("btn01");
                dt.Columns.Add("btn02");
                dt.Columns.Add("btn04"); // Change Issue
                dt.Columns.Add("issue_type_name");
                dt.Columns.Add("Epic Status");
                dt.Columns.Add("effort_size");
                dt.Columns.Add("story_points");
                dt.Columns.Add("parent_assignee_short");
                dt.Columns.Add("parent_assignee_name");
                dt.Columns.Add("parent_link");
                dt.Columns.Add("parent_link_url");
                dt.Columns.Add("parent_summary");
                dt.Columns.Add("Summary");
                dt.Columns.Add("epic_due");
                dt.Columns.Add("Epic Updated");
                dt.Columns.Add("btn_prio");
                dt.Columns.Add("epic_prio");
                dt.Columns.Add("issue_prio_low");
                dt.Columns.Add("issue_prio_med");
                dt.Columns.Add("issue_prio_high");
                dt.Columns.Add("issue_prio_critical");
                dt.Columns.Add("Key");
                dt.Columns.Add("New");
                dt.Columns.Add("Open");
                dt.Columns.Add("InProgress");
                dt.Columns.Add("InReview");
                dt.Columns.Add("InSpecification");
                dt.Columns.Add("Pending");
                dt.Columns.Add("Resolved");
                dt.Columns.Add("Closed");
                dt.Columns.Add("All Stories Resolved");
                dt.Columns.Add("acceptance_done");
                dt.Columns.Add("acceptance_outstanding");
                dt.Columns.Add("total_acceptance");
                dt.Columns.Add("Story Updated");

                dt.Columns.Add("Parent_Summary");
                dt.Columns.Add("storie_last_updated");
                dt.Columns.Add("label01");
                dt.Columns.Add("label02");
                dt.Columns.Add("label03");
                dt.Columns.Add("label_list");
                dt.Columns.Add("u00");
                dt.Columns.Add("u01");
                dt.Columns.Add("u02");
                dt.Columns.Add("u03");
                dt.Columns.Add("u04");
                dt.Columns.Add("u05");
                dt.Columns.Add("u06");
                dt.Columns.Add("u07");
                dt.Columns.Add("u07p");
                dt.Columns.Add("URL");
                dt.Columns.Add("parent_assignee_uid");
                dt.Columns.Add("error_in_line");
                dt.Columns.Add("error");



                return dt;
            }
            catch (Exception ex)
            {
                lb.clsLogger.WriteLog("Error in : " + m_method + " : " + ex.Message);
                return dt;

            }
        }
        private void frmEpic_to_Story_Creator_Load(object sender, EventArgs e)
        {
            try
            {
                load_cbo_dbo();
                load_cbo_files(cbo_DBO.Text);
                load_cbo_pi();
                load_cbo_rest();
                load_cbo_status();

            }
            catch (Exception ex)
            {

                throw;
            }
        }
        private void load_cbo_status()
        {
            string m_method = "frmEpic_to_Story_Creator.load_cbo_status";
            try
            {
                //CBO STATUS
                cbo_status.Items.Clear();
                cbo_status.Items.Add("New");
                cbo_status.Items.Add("Open");
                cbo_status.Items.Add("In Progress");
                cbo_status.Items.Add("Pending");
                cbo_status.Items.Add("In Specification");
                cbo_status.Items.Add("Resolved");
                cbo_status.Items.Add("Closed");
                cbo_status.Items.Add("New <-> In Progress");
                cbo_status.Items.Add("In Progress <-> Closed");
                cbo_status.Items.Add("New <-> Closed");
                cbo_status.Items.Add("Resolved & Closed");
                cbo_status.SelectedIndex = 9;
            }
            catch (Exception ex)
            {
                lb.clsLogger.WriteLog("Error in : " + m_method + " : " + ex.Message);
            }
        }
        private void load_cbo_dbo()
        {
            string m_method = "frmEpic_to_Story_Creator.load_cbo_dbo";
            string dbo_id = "";
            Int32 i_dbo_id = 0;
            string epic_project_id = "";
            string uid = "";
            try
            {
                //CBO DBO
                DataTable dt = lb.clsDB_dbo_gen.get_dt_for_all_dbo();
                cbo_DBO.DataSource = dt;
                cbo_DBO.ValueMember = "dbo_id";
                cbo_DBO.DisplayMember = "dbo";
                // Set PROJECT ID
                dbo_id = cbo_DBO.Items[cbo_DBO.SelectedIndex].Value.ToString();
                i_dbo_id = Convert.ToInt32(dbo_id);
                //epic_project_id = lb.clsDB_dbo_custom.get_epic_project_id_from_id(i_dbo_id);
                //txtProject.Text = epic_project_id; !!!!!! MUST STAY EXXD for Analitics !!!!!!
                uid = lb.clsDB_last_user_custom.log_uid();
                string default_value = lb.clsDB_user_default_custom.get_default_value_for_user_and_key(uid, "frmEpic_to_Story_Creator", "cboDBO");
                cbo_DBO.SelectedValue = default_value;
                // if (d_cbocbo_id>0)
                // {
                ////     cboDBO.SelectedValue = d_cbocbo_id.ToString();
                // }
            }
            catch (Exception ex)
            {
                lb.clsLogger.WriteLog("Error in : " + m_method + " : " + ex.Message);
            }
        }
        private void load_cbo_rest()
        {
            string m_method = "frmEpic_to_Story_Creator.load_cbo_rest";
            try
            {
                cbo_epic_from.Items.Add("File");
                cbo_epic_from.Items.Add("Jira");
                cbo_epic_from.SelectedIndex = 1;
                //CBO Load Story From
                cbo_load_story_from.Items.Clear();
                cbo_load_story_from.Items.Add("File");
                cbo_load_story_from.Items.Add("Jira");
                cbo_load_story_from.SelectedIndex = 0;
                //CBO Load Story CountBy
                cboStoryCountBy.Items.Clear();
                cboStoryCountBy.Items.Add("Story");
                cboStoryCountBy.Items.Add("Story Points");
                cboStoryCountBy.SelectedIndex = 0;
                //CBO Load Story From
                cboColumsInGrid.Items.Clear();
                cboColumsInGrid.Items.Add("All");
                cboColumsInGrid.Items.Add("Summary");
                cboColumsInGrid.SelectedIndex = 0;
                WindowState = FormWindowState.Maximized;
                string m_cboDBO_id = cbo_DBO.Items[cbo_DBO.SelectedIndex].Value.ToString();


            }
            catch (Exception ex)
            {
                lb.clsLogger.WriteLog("Error in : " + m_method + " : " + ex.Message);
            }
        }
        private void load_cbo_pi()
        {
            string m_method = "frmEpic_to_Story_Creator.load_cbo_pi";
            try
            {
                //CBO PI
                cbo_pi.Items.Clear();
                cbo_pi.Items.Add("PI I/2023");
                cbo_pi.Items.Add("PI II/2023");
                cbo_pi.Items.Add("PI III/2023");
                cbo_pi.Items.Add("PI IV/2023");

                cbo_pi.Items.Add("PI I/2022");
                cbo_pi.Items.Add("PI II/2022");
                cbo_pi.Items.Add("PI III/2022");
                cbo_pi.Items.Add("PI IV/2022");
                cbo_pi.SelectedIndex = 0;

            }
            catch (Exception ex)
            {
                lb.clsLogger.WriteLog("Error in : " + m_method + " : " + ex.Message);
            }
        }
        private void load_cbo_files(string p_sub_product)
        {
            string m_method = "frmEpic_to_Story_Creator.load_cbo_files";
            try
            {
                if (p_sub_product != "System.Data.DataRowView")
                {
                    //*
                    //******************************************************
                    //*
                    //* History Path
                    //*
                    //******************************************************
                    //*
                    string sPath = System.IO.Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
                    string sHistoryPath = sPath + @"\history\";
                    if (!Directory.Exists(sHistoryPath))
                    {
                        Directory.CreateDirectory(sHistoryPath);
                    }
                    txt_history_path.Text = sHistoryPath;
                    //*
                    //******************************************************
                    //*
                    //* Data Files
                    //*
                    //******************************************************
                    //*
                    p_sub_product = p_sub_product.Replace(" ", "_");
                    string[] txtFiles = Directory.GetFiles(sHistoryPath, "*" + p_sub_product + "*.csv", SearchOption.TopDirectoryOnly);
                    string m_path = "";
                    string m_file = "";
                    cbo_files.Items.Clear();
                    Int32 m_count_files = 0;
                    foreach (var txtFile in txtFiles)
                    {
                        m_count_files = m_count_files + 1;
                        lb.clsFile.SplitPath(txtFile, out m_path, out m_file);
                        string[] textSplit = m_file.Split('_');
                        //m_file = m_file.Replace(".csv", "");
                        cbo_files.Items.Add(m_file);
                        // loop ...
                    }
                    if (m_count_files > 0)
                    {
                        cbo_files.SelectedIndex = 0;
                    }

                }


            }
            catch (Exception ex)
            {
                lb.clsLogger.WriteLog("Error in : " + m_method + " : " + ex.Message);
            }
        }

        private void btn_load_prev_data_Click(object sender, EventArgs e)
        {
            string m_grd_epic_name = "";
            string m_method = "frmEpic_to_Story_Creator.btn_load_prev_data_Click";
            string m_file_path = "";
            try
            {

                txt_filter.Text = "";
                //m_file_path = "C:\\AAA\\A Programs\\C#\\JiraRest02\\JiraRest02\\bin\\Debug\\history\\quality_iii_2022_pending_jira_jira_20221025_22_06_33_264.csv";
                // m_file_path = "C:\\AAA\\A Programs\\C#\\JiraRest02\\JiraRest02\\bin\\Debug\\history\\quality_iv_2022_new_closed_jira_jira_20221027_06_36_50_031.csv";
                m_file_path = txt_history_path.Text + cbo_files.Text;
                DataTable dt = new DataTable();
                dt = lb.clsExcel.csvToDataTable(m_file_path, ';');
                //dt = lb.clsDataTable.dt_filter(dt, txt_filter.Text);
                //dt = lb.clsExcel.ConvertCSVtoDataTable_02(m_file_path);
                grdEpics.AutoGenerateColumns = true;
                grdEpics.DataSource = dt;
                format_grd_epic();
                grdEpics.BestFitColumns();
                //dt = (DataTable)grdEpics.DataSource;
                //string m_csv = lb.clsExcel.DataTableToCSV(dt, "\t");
                //File.WriteAllText(m_grd_epic_name, m_csv.ToString());
                // txtEpicCnt.Text = dt.Rows.Count.ToString();
            }
            catch (Exception ex)
            {

                lb.clsLogger.WriteLog("Error in : " + m_method + " : " + ex.Message);
            }
        }
        private void format_grd_epic()
        {
            string m_method = "frmEpic_to_Story_Creator.format_grd_epic";
            try
            {
                //GridViewCommandColumn commandColumn = new GridViewCommandColumn();
                //commandColumn.Name = "btn01";
                //commandColumn.UseDefaultText = true;
                //commandColumn.FieldName = "ProductName";
                //commandColumn.HeaderText = "Order";
                //grdEpics.MasterTemplate.Columns.Add(commandColumn);

                foreach (GridViewColumn col in this.grdEpics.Columns)
                {
                    if (col.Name == "btn01")
                    {
                        //col.IsVisible = false;
                        col.Width = 50;
                        col.HeaderText = "Stories";
                    }
                    if (col.Name == "btn02")
                    {
                        //col.IsVisible = false;
                        col.Width = 72;
                        col.HeaderText = "Open Epic";
                    }
                    if (col.Name == "btn03")
                    {
                        col.IsVisible = false;
                        col.Width = 72;
                        col.HeaderText = "Open Labels";
                    }
                    if (col.Name == "btn04")
                    {
                        //col.IsVisible = false;
                        col.Width = 72;
                        col.HeaderText = "Change Issue";
                    }
                    if (col.Name == "Id")
                    {
                        col.IsVisible = false;
                        col.Width = 70;
                        col.HeaderText = "ID";

                    }
                    if (col.Name == "Summary")
                    {
                        //col.IsVisible = false;
                        col.Width = 420;
                        col.HeaderText = "Summary";
                        //col.BestFit();
                    }
                    if (col.Name == "Key")
                    {
                        col.IsVisible = false; // !!!!!! ALREADY show the EPIC in the URL
                        col.Width = 80;
                        col.HeaderText = "Key";
                    }
                    if (col.Name == "Epic Status")
                    {
                        col.IsVisible = true;
                        col.Width = 80;
                        col.HeaderText = "Epic Status";
                        col.TextAlignment = ContentAlignment.MiddleLeft;
                        col.BestFit();
                    }
                    if (col.Name == "parent_link")
                    {
                        //col.IsVisible = false;
                        col.Width = 92;
                        col.HeaderText = "Parent";
                        col.TextAlignment = ContentAlignment.MiddleLeft;
                        col.BestFit();
                    }
                    if (col.Name == "parent_link_url")
                    {
                        col.IsVisible = false;
                        col.Width = 92;
                        col.HeaderText = "Parent URL";
                    }
                    if (col.Name == "parent_summary")
                    {
                        col.IsVisible = true;
                        col.Width = 132;
                        col.HeaderText = "Parent Summary";
                        col.BestFit();
                    }
                    if (col.Name == "btn_prio")
                    {
                        col.Width = 50;
                        col.HeaderText = "M Prio S";
                        col.BestFit();
                    }
                    if (col.Name == "epic_prio")
                    {
                        col.IsVisible = true;
                        col.Width = 80;
                        col.HeaderText = "Epic Priority";

                        col.BestFit();
                    }
                    if (col.Name == "issue_prio_low")
                    {
                        col.IsVisible = true;
                        col.Width = 80;
                        col.HeaderText = "Low";
                        col.TextAlignment = ContentAlignment.MiddleCenter;
                        col.BestFit();
                    }
                    if (col.Name == "issue_prio_med")
                    {
                        col.IsVisible = true;
                        col.Width = 80;
                        col.HeaderText = "Medium";
                        col.TextAlignment = ContentAlignment.MiddleCenter;
                        col.BestFit();
                    }
                    if (col.Name == "issue_prio_high")
                    {
                        col.IsVisible = true;
                        col.Width = 80;
                        col.HeaderText = "High";
                        col.TextAlignment = ContentAlignment.MiddleCenter;
                        col.BestFit();
                    }
                    if (col.Name == "issue_prio_critical")
                    {
                        col.IsVisible = true;
                        col.Width = 80;
                        col.HeaderText = "Critical";
                        col.TextAlignment = ContentAlignment.MiddleCenter;
                        col.BestFit();
                    }

                    //   dt.Columns.Add("Epic Status");
                    //dt.Columns.Add("Status New");
                    //dt.Columns.Add("Status Open");
                    //dt.Columns.Add("Status InProgress");
                    //dt.Columns.Add("Status Pending");
                    //dt.Columns.Add("Status Resolved");
                    if (col.Name == "New")
                    {
                        //col.IsVisible = false;
                        col.Width = 40;
                        col.HeaderText = "New";
                        col.TextAlignment = ContentAlignment.MiddleCenter;
                    }
                    if (col.Name == "Open")
                    {
                        //col.IsVisible = false;
                        col.Width = 40;
                        col.HeaderText = "Open";
                        col.TextAlignment = ContentAlignment.MiddleCenter;
                    }
                    if (col.Name == "InProgress")
                    {
                        //col.IsVisible = false;
                        col.Width = 68;
                        col.HeaderText = "In Progress";
                        col.TextAlignment = ContentAlignment.MiddleCenter;
                    }
                    if (col.Name == "InReview")
                    {
                        //col.IsVisible = false;
                        col.Width = 60;
                        col.HeaderText = "In Review";
                        col.TextAlignment = ContentAlignment.MiddleCenter;
                    }

                    if (col.Name == "InSpecification")
                    {
                        //col.IsVisible = false;
                        col.Width = 55;
                        col.HeaderText = "InSpecification";
                        col.TextAlignment = ContentAlignment.MiddleCenter;
                    }
                    if (col.Name == "Pending")
                    {
                        //col.IsVisible = false;
                        col.Width = 55;
                        col.HeaderText = "Pending";
                        col.TextAlignment = ContentAlignment.MiddleCenter;
                    }
                    if (col.Name == "Resolved")
                    {
                        //col.IsVisible = false;
                        col.Width = 60;
                        col.HeaderText = "Resolved";
                        col.TextAlignment = ContentAlignment.MiddleCenter;
                    }
                    if (col.Name == "Closed")
                    {
                        //col.IsVisible = false;
                        col.Width = 60;
                        col.HeaderText = "Closed";
                        col.TextAlignment = ContentAlignment.MiddleCenter;
                    }
                    if (col.Name == "All Stories Resolved")
                    {
                        //col.IsVisible = false;
                        col.Width = 80;
                        col.HeaderText = "All Resolved";
                        col.TextAlignment = ContentAlignment.MiddleCenter;
                    }

                    if (col.Name == "Epic Updated")
                    {
                        //col.IsVisible = false;
                        col.Width = 80;
                        col.HeaderText = "Epic Updated";
                        col.TextAlignment = ContentAlignment.MiddleCenter;
                    }
                    if (col.Name == "Story Updated")
                    {
                        col.IsVisible = false;
                        col.Width = 85;
                        col.HeaderText = "Story Updated";
                        col.TextAlignment = ContentAlignment.MiddleCenter;
                    }
                    if (col.Name == "epic_due")
                    {
                        col.IsVisible = true;
                        col.Width = 85;
                        col.HeaderText = "Epic Due";
                        col.TextAlignment = ContentAlignment.MiddleCenter;
                    }

                    if (col.Name == "dmgf_uid")
                    {
                        //col.IsVisible = false;
                        col.Width = 100;
                        col.HeaderText = "DMGF";
                    }
                    if (col.Name == "use_case_dmgf_uid")
                    {
                        //col.IsVisible = false;
                        col.Width = 80;
                        col.HeaderText = "DMGF - Use Case";
                    }
                    if (col.Name == "epic_project_id")
                    {
                        //col.IsVisible = false;
                        col.Width = 80;
                        col.HeaderText = "Epic - Project";
                    }
                    if (col.Name == "issue_project_id")
                    {
                        //col.IsVisible = false;
                        col.Width = 80;
                        col.HeaderText = "Issue - Project";
                    }
                    if (col.Name == "issue_type_name")
                    {
                        col.IsVisible = false;
                        col.Width = 80;
                        col.HeaderText = "Issue Type";
                    }

                    if (col.Name == "Parent_Summary")
                    {
                        col.IsVisible = false;
                        col.Width = 80;
                        col.HeaderText = "Parent Summary";
                    }
                    if (col.Name == "acceptance_outstanding")
                    {
                        //col.IsVisible = false;
                        col.Width = 80;
                        col.HeaderText = "Outstanding";
                        col.TextAlignment = ContentAlignment.MiddleCenter;
                    }
                    if (col.Name == "acceptance_done")
                    {
                        //col.IsVisible = false;
                        col.Width = 80;
                        col.HeaderText = "Done";
                        col.TextAlignment = ContentAlignment.MiddleCenter;
                    }
                    if (col.Name == "total_acceptance")
                    {
                        //col.IsVisible = false;
                        col.Width = 80;
                        col.HeaderText = "Total Accept";
                        col.TextAlignment = ContentAlignment.MiddleCenter;
                    }


                    //
                    if (col.Name == "storie_last_updated")
                    {
                        //col.IsVisible = false;
                        col.Width = 90;
                        col.HeaderText = "Storie Updated";
                        col.TextAlignment = ContentAlignment.MiddleCenter;
                    }
                    if (col.Name == "u00")
                    {
                        col.IsVisible = true;
                        col.Width = 60;
                        col.HeaderText = "U 0";
                        col.TextAlignment = ContentAlignment.MiddleCenter;
                        col.BestFit();
                    }
                    if (col.Name == "u01")
                    {
                        col.IsVisible = true;
                        col.Width = 60;
                        col.HeaderText = "U 1";
                        col.TextAlignment = ContentAlignment.MiddleCenter;
                        col.BestFit();
                    }
                    if (col.Name == "u02")
                    {
                        col.IsVisible = true;
                        col.Width = 60;
                        col.HeaderText = "U 2";
                        col.TextAlignment = ContentAlignment.MiddleCenter;
                        col.BestFit();
                    }
                    if (col.Name == "u03")
                    {
                        col.IsVisible = true;
                        col.Width = 60;
                        col.HeaderText = "U3";
                        col.TextAlignment = ContentAlignment.MiddleCenter;
                        col.BestFit();
                    }
                    if (col.Name == "u04")
                    {
                        col.IsVisible = true;
                        col.Width = 60;
                        col.HeaderText = "U 4";
                        col.TextAlignment = ContentAlignment.MiddleCenter;
                        col.BestFit();
                    }
                    if (col.Name == "u05")
                    {
                        col.IsVisible = true;
                        col.Width = 60;
                        col.HeaderText = "U 5";
                        col.TextAlignment = ContentAlignment.MiddleCenter;
                        col.BestFit();
                    }
                    if (col.Name == "u06")
                    {
                        col.IsVisible = true;
                        col.Width = 60;
                        col.HeaderText = "U 6";
                        col.TextAlignment = ContentAlignment.MiddleCenter;
                        col.BestFit();
                    }
                    if (col.Name == "u07")
                    {
                        col.IsVisible = true;
                        col.Width = 60;
                        col.HeaderText = "U 7";
                        col.TextAlignment = ContentAlignment.MiddleCenter;
                        col.BestFit();
                    }
                    if (col.Name == "u07p")
                    {
                        col.IsVisible = true;
                        col.Width = 60;
                        col.HeaderText = "U 7+";
                        col.TextAlignment = ContentAlignment.MiddleCenter;
                        col.BestFit();
                    }
                    if (col.Name == "u07p")
                    {
                        col.IsVisible = true;
                        col.Width = 60;
                        col.HeaderText = "U 7+";
                        col.TextAlignment = ContentAlignment.MiddleCenter;
                    }
                    if (col.Name == "parent_assignee_uid")
                    {
                        col.IsVisible = false;
                        col.Width = 60;
                        col.HeaderText = "Assignee UID";
                        col.TextAlignment = ContentAlignment.MiddleCenter;
                    }
                    if (col.Name == "parent_assignee_short")
                    {
                        col.IsVisible = true;
                        col.Width = 60;
                        col.HeaderText = "Assignee Short";
                        col.TextAlignment = ContentAlignment.MiddleCenter;
                    }
                    if (col.Name == "parent_assignee_name")
                    {
                        col.IsVisible = true;
                        col.Width = 160;
                        col.HeaderText = "Assignee Name";
                    }
                    if (col.Name == "label01")
                    {
                        col.IsVisible = true;
                        col.Width = 80;
                        col.HeaderText = "Label 01";
                        col.TextAlignment = ContentAlignment.MiddleCenter;
                        col.BestFit();
                    }
                    if (col.Name == "label02")
                    {
                        col.IsVisible = true;
                        col.Width = 80;
                        col.HeaderText = "Label 02";
                        col.TextAlignment = ContentAlignment.MiddleCenter;
                        col.BestFit();
                    }
                    if (col.Name == "label03")
                    {
                        col.IsVisible = true;
                        col.Width = 80;
                        col.HeaderText = "Label 03";
                        col.TextAlignment = ContentAlignment.MiddleCenter;
                        col.BestFit();
                    }
                    if (col.Name == "label_list")
                    {
                        col.IsVisible = true;
                        col.Width = 120;
                        col.HeaderText = "Label List";
                        col.TextAlignment = ContentAlignment.MiddleLeft;
                        col.BestFit();
                    }
                    if (col.Name == "effort_size")
                    {
                        col.IsVisible = true;
                        col.Width = 80;
                        col.HeaderText = "Effort";
                        col.TextAlignment = ContentAlignment.MiddleCenter;
                        col.BestFit();
                    }
                    if (col.Name == "story_points")
                    {
                        col.IsVisible = true;
                        col.Width = 80;
                        col.HeaderText = "Story Points";
                        col.TextAlignment = ContentAlignment.MiddleCenter;
                        col.BestFit();
                    }
                    if (col.Name == "error_in_line")
                    {
                        col.IsVisible = true;
                        col.Width = 80;
                        col.HeaderText = "Have Error";
                        col.TextAlignment = ContentAlignment.MiddleCenter;
                        col.BestFit();
                    }
                    if (col.Name == "error")
                    {
                        col.IsVisible = true;
                        col.Width = 80;
                        col.HeaderText = "Error";
                        col.TextAlignment = ContentAlignment.MiddleCenter;
                        col.BestFit();
                    }

                    //if (col.Name == "URL")
                    //{
                    //    col.IsVisible = false;
                    //    col.Width = 80;
                    //    col.HeaderText = "URL";
                    //    GridViewHyperlinkColumn column = new GridViewHyperlinkColumn("URL");
                    //    this.grdEpics.Columns.Add("URL");
                    //}

                }

                GridViewSummaryRowItem summaryRowItem = new GridViewSummaryRowItem();
                GridViewSummaryItem summaryItem_Count_epic_update = new GridViewSummaryItem();
                GridViewSummaryItem summaryItem_New = new GridViewSummaryItem();
                GridViewSummaryItem summaryItem_Open = new GridViewSummaryItem();
                GridViewSummaryItem summaryItem_InProgress = new GridViewSummaryItem();
                GridViewSummaryItem summaryItem_InReview = new GridViewSummaryItem();
                GridViewSummaryItem summaryItem_Pending = new GridViewSummaryItem();
                GridViewSummaryItem summaryItem_Resolved = new GridViewSummaryItem();
                GridViewSummaryItem summaryItem_accept_total = new GridViewSummaryItem();
                GridViewSummaryItem summaryItem_accept_outstanding = new GridViewSummaryItem();
                GridViewSummaryItem summaryItem_accept_done = new GridViewSummaryItem();

                summaryItem_Count_epic_update.Name = "Epic Updated";
                summaryItem_Count_epic_update.Aggregate = GridAggregateFunction.Count;
                summaryRowItem.Add(summaryItem_Count_epic_update);

                summaryItem_New.Name = "New";
                summaryItem_New.Aggregate = GridAggregateFunction.Sum;
                summaryRowItem.Add(summaryItem_New);

                summaryItem_Open.Name = "Open";
                summaryItem_Open.Aggregate = GridAggregateFunction.Sum;
                summaryRowItem.Add(summaryItem_Open);

                summaryItem_InProgress.Name = "InProgress";
                summaryItem_InProgress.Aggregate = GridAggregateFunction.Sum;
                summaryRowItem.Add(summaryItem_InProgress);

                summaryItem_InReview.Name = "InReview";
                summaryItem_InReview.Aggregate = GridAggregateFunction.Sum;
                summaryRowItem.Add(summaryItem_InReview);

                summaryItem_Pending.Name = "Pending";
                summaryItem_Pending.Aggregate = GridAggregateFunction.Sum;
                summaryRowItem.Add(summaryItem_Pending);

                summaryItem_Resolved.Name = "Resolved";
                summaryItem_Resolved.Aggregate = GridAggregateFunction.Sum;
                summaryRowItem.Add(summaryItem_Resolved);

                summaryItem_accept_total.Name = "total_acceptance";
                summaryItem_accept_total.Aggregate = GridAggregateFunction.Sum;
                summaryRowItem.Add(summaryItem_accept_total);



                summaryItem_accept_outstanding.Name = "acceptance_outstanding";
                summaryItem_accept_outstanding.Aggregate = GridAggregateFunction.Sum;
                summaryRowItem.Add(summaryItem_accept_outstanding);

                summaryItem_accept_done.Name = "acceptance_done";
                summaryItem_accept_done.Aggregate = GridAggregateFunction.Sum;
                summaryRowItem.Add(summaryItem_accept_done);
                // Storie Points
                GridViewSummaryItem story_points = new GridViewSummaryItem();
                summaryItem_accept_done.Name = "story_points";
                story_points.Aggregate = GridAggregateFunction.Sum;
                summaryRowItem.Add(story_points);
                //* U00
                GridViewSummaryItem summary_u00 = new GridViewSummaryItem();
                summary_u00.Name = "u00";
                summary_u00.Aggregate = GridAggregateFunction.Sum;
                summaryRowItem.Add(summary_u00);
                //* U01
                GridViewSummaryItem summary_u01 = new GridViewSummaryItem();
                summary_u01.Name = "u01";
                summary_u01.Aggregate = GridAggregateFunction.Sum;
                summaryRowItem.Add(summary_u01);
                //* U02
                GridViewSummaryItem summary_u02 = new GridViewSummaryItem();
                summary_u02.Name = "u02";
                summary_u02.Aggregate = GridAggregateFunction.Sum;
                summaryRowItem.Add(summary_u02);
                //* U03
                GridViewSummaryItem summary_u03 = new GridViewSummaryItem();
                summary_u03.Name = "u03";
                summary_u03.Aggregate = GridAggregateFunction.Sum;
                summaryRowItem.Add(summary_u03);
                //* U04
                GridViewSummaryItem summary_u04 = new GridViewSummaryItem();
                summary_u04.Name = "u04";
                summary_u04.Aggregate = GridAggregateFunction.Sum;
                summaryRowItem.Add(summary_u04);
                //* U05
                GridViewSummaryItem summary_u05 = new GridViewSummaryItem();
                summary_u05.Name = "u05";
                summary_u05.Aggregate = GridAggregateFunction.Sum;
                summaryRowItem.Add(summary_u05);
                //* U06
                GridViewSummaryItem summary_u06 = new GridViewSummaryItem();
                summary_u06.Name = "u06";
                summary_u06.Aggregate = GridAggregateFunction.Sum;
                summaryRowItem.Add(summary_u06);
                //* U07
                GridViewSummaryItem summary_u07 = new GridViewSummaryItem();
                summary_u07.Name = "u07";
                summary_u07.Aggregate = GridAggregateFunction.Sum;
                summaryRowItem.Add(summary_u07);
                //* U07p : Past 7 days
                GridViewSummaryItem summary_u07p = new GridViewSummaryItem();
                summary_u07p.Name = "u07p";
                summary_u07p.Aggregate = GridAggregateFunction.Sum;
                summaryRowItem.Add(summary_u07p);



                // summaryRowItem.Clear();
                grdEpics.SummaryRowsBottom.Clear();
                this.grdEpics.SummaryRowsBottom.Add(summaryRowItem);
            }
            catch (Exception ex)
            {
                lb.clsLogger.WriteLog("Error in : " + m_method + " : " + ex.Message);
            }
        }

        private void cbo_DBO_SelectedIndexChanged(object sender, Telerik.WinControls.UI.Data.PositionChangedEventArgs e)
        {
            string m_method = "frmEpic_to_Story_Creator.cbo_DBO_SelectedIndexChanged";
            try
            {
                //*
                //* Load DATA FILES
                //*
                load_cbo_files(cbo_DBO.Text);
               //lblMessage.Text = "When SAME DBO,Type the Template name needs to be diffent";
                //*
                //* Load Task Type
                //*
                //string m_dbo_id = cbo_sub_product.Items[cbo_sub_product.SelectedIndex].Value.ToString();
                //string m_task_type_id = cbo_task_type.Items[cbo_task_type.SelectedIndex].Value.ToString();
                //load_cbo_task_type(m_dbo_id);
                //load_grd_interface(m_dbo_id, m_task_type_id);
                //timer_clear_message.Enabled = true;
            }
            catch (Exception ex)
            {

                lb.clsLogger.WriteLog("Error in : " + m_method + " : " + ex.Message);
            }
        }

        private void btnAddEpicList_Click(object sender, EventArgs e)
        {
            string m_method = "frmEpic_to_Story_Creator.btnAddEpicList_Click";
            string UniqueID = "kobus";
            string screenlog = "";
            string get_epic_from = cbo_epic_from.Text;
            string get_story_from = cbo_load_story_from.Text;
            string sFull_Path = "";
            string data_path = "";
            string dbo = "";
            string pi = "";
            string folder_status = "";
            string qry_status = "";
            string dbo_key = "";
            string dbo_jira_tag = "";
            string dbo_id = "";
            Chilkat.StringBuilder sbResponseBody = new Chilkat.StringBuilder();
            DataTable dtEpics;
            DataTable dtStatusCnt;
            try
            {
                lblMessage.Text = "";
                var watchFullJira = System.Diagnostics.Stopwatch.StartNew();
                timerLoadItemProgressBar.Enabled = true;
                dbo = cbo_DBO.Text;
                dbo_id = cbo_DBO.Items[cbo_DBO.SelectedIndex].Value.ToString();
                DataTable dt = lb.clsDB_dbo_gen.get_dt_for_all_dbo();
                foreach (DataRow row in dt.Rows)
                {
                    if (row["dbo"].ToString() == dbo)
                    {
                        dbo_jira_tag = row["dbo_jira_tag"].ToString();
                    }
                }

                pi = cbo_pi.Text;
                pi = pi.Replace(" ", "_");
                pi = pi.Replace("/", "");

                switch (cbo_status.Text)
                {
                    case "New <-> In Progress":
                        qry_status = "'New','Open','In Progress','Pending','In Specification'";
                        folder_status = "In_Progress";
                        break;
                    case "In Progress <-> Closed":
                        qry_status = "'In Progress','Pending','Resolved','Closed'";
                        folder_status = "In_Progress";
                        break;
                    case "Resolved & Closed":
                        qry_status = "'Resolved','Closed'";
                        folder_status = "Resolved";
                        break;
                    case "New <-> Closed":
                        qry_status = "'New','Open','In Progress','Pending','In Specification','In Review','Resolved','Closed'";
                        folder_status = "In_Progress";
                        break;
                    //
                    default:
                        qry_status = "'" + cbo_status.Text + "'";
                        folder_status = qry_status.Replace(" ", "_");
                        break;
                }

                string project_id = txtProject.Text;
                string qry = "";
                string jira_url = "";
                // string qry = "project=EXXD and fixVersion = '" + cboPI.Text + "' and component = CEDA_Quality and status  = '" + cboStatus.Text + "'";
                //string qry = "project=EXXD and issuetype = Epic and fixVersion = '" + cboPI.Text + "' and component = '" + dbo_jira_tag + "' and status  = '" + cboStatus.Text + "'";
                //string qry = "project=" + project_id + " and issuetype = Epic and component = '" + dbo_jira_tag + "' and status  = '" + cboStatus.Text + "'";
                //                string qry = " issuetype = Epic and component = '" + dbo_jira_tag + "' and fixVersion = '" + cboPI.Text + "' and status  in (" + qry_status + ")";
                // string qry = " issuetype = Epic and component = '" + dbo_jira_tag + "' and fixVersion = '" + cboPI.Text + "' and status  in (" + qry_status + ")";
                //**************************************************************************************************************
                //* This is the current QRY
                qry = " issuetype = Epic and component in (" + dbo_jira_tag + ") and fixVersion = '" + cbo_pi.Text + "' and status  in (" + qry_status + ")";
                jira_url = lb.clsJira.get_url_from_url(project_id, qry);
                //*
                //**************************************************************************************************************
                //string qry = "project=ZXXNCCB and issuetype = Epic and status  = '" + cboStatus.Text + "'";
                //jira_url = "/jira/rest/api/2/search?jql=project=EDAD AND key = EDAD-1300";
                //  jira_url = "/jira/rest/api/2/search?jql=project = EDAD AND \"Epic Link\" = \"EDAD-1300\" ";
                //jira_url = lb.clsJira.make_url_to_rest_api_friendly(jira_url);
                string hostnamex = txtHostName.Text;
                //Get Now
                DateTimeOffset now = (DateTimeOffset)DateTime.Now;
                string ts = now.ToString("yyyyMMdd_HH_mm_ss_fff");
                //dbo_key = dbo + "_" + folder_status + "_epic";
                dbo_key = dbo + "_epic";
                switch (get_epic_from)
                {
                    case "File":

                        //sFull_Path = lb.clsFile.get_latest_file_for_epic(dbo,pi,status);
                        sFull_Path = lb.clsFile.get_latest_file_for_epic(dbo, dbo_key); //DBO is the Path and dbo_key is the file

                        if (sFull_Path == "")
                        {
                            //data_path = lb.clsFile.get_data_path() + dbo + "_" + pi + "_" + status + @"\" + dbo_key;
                            data_path = lb.clsFile.get_data_path() + dbo + @"\" + dbo_key;
                            sFull_Path = data_path + "_" + ts + ".json";
                            sbResponseBody = lb.clsJira.get_json_full_obj_for_url(jira_url, sFull_Path);
                            //sFull_Path = save_sbResponseBody_json(sFull_Path, sbResponseBody);
                        }
                        else
                        {
                            sbResponseBody = lb.clsChilKat.get_stringbuilder_from_file_path(sFull_Path);
                        }


                        break;
                    case "Jira":

                        //data_path = lb.clsFile.get_data_path() + dbo + "_" + pi + "_" + status + @"\" + dbo_key;
                        data_path = lb.clsFile.get_data_path() + dbo + @"\" + dbo_key;
                        sFull_Path = data_path + "_" + ts + ".json";
                        sbResponseBody = lb.clsJira.get_json_full_obj_for_url(jira_url, sFull_Path);
                        break;
                    default:
                        // code block
                        break;
                }
                dtEpics = lb.clsResponseBody.get_issue_list_dt(sbResponseBody);
                dtEpics = lb.clsJira.get_parent_info_dt(dbo_id, dtEpics);
                //get_story_from = "Jira"; // UNTIL it is tested
                if (get_story_from == "Jira")
                {
                    //*
                    //* Create ALL the EPIC files
                    //*
                    //await lb.clsJira.create_files_per_epics_from_an_epic_list_Async(sbResponseBody, dbo);
                    //await lb.clsJira.create_files_per_epics_from_an_epic_list_ParalleAsync(sbResponseBody, dbo);
                    lb.clsJira.create_files_per_epics_from_an_epic_list(sbResponseBody, dbo);
                }

                string jsonText = File.ReadAllText(sFull_Path);
                //get_from = "File";
                //string p_story_point_or_count = "story_points";
                //string p_story_point_or_count = "count";
                //string p_story_point_or_count = "story_point";
                string p_story_point_or_count = cboStoryCountBy.Text;
                string p_columns_in_grid = cboColumsInGrid.Text;
                bool m_store_user = cbStoreUsers.Checked;
                bool m_load_acceptance = cbLoadAcceptance.Checked;
                bool m_load_comments = cbLoadComments.Checked;
                bool m_load_parent_links = cbStoreParentLinks.Checked;

                //var watchFullJira = System.Diagnostics.Stopwatch.StartNew();
                var watchJiraStories = System.Diagnostics.Stopwatch.StartNew();
                txtFullPath.Text = sFull_Path;
                //(dtEpics,dtStatusCnt) = get_dt_4_epic_list(sbResponseBody, get_story_from,dbo_id,pi,status, p_story_point_or_count, p_columns_in_grid, m_store_user, m_load_acceptance);
                (dtEpics, dtStatusCnt) = get_dt_4_epic_list_old(sbResponseBody, dtEpics, dbo_id, pi, folder_status, p_story_point_or_count, p_columns_in_grid, m_store_user, m_load_acceptance, txtEpicContains.Text, m_load_comments, m_load_parent_links);
                // Story
                watchJiraStories.Stop();
                var elapsedMs02 = watchJiraStories.ElapsedMilliseconds;
                txtStorieExecuteTime.Text = elapsedMs02.ToString();
                // GEt the correct columns for DT
                //string fieldlist = "";
                //switch (p_columns_in_grid)
                //{
                //    case "Summary":
                //        fieldlist = "Summary";
                //        break;
                //    default:
                //        break;
                //}
                //if(p_columns_in_grid != "All")
                //{
                //    DataTable newTable = dtEpics.Clone();
                //    for (int i = newTable.Columns.Count - 1; i >= 0; i--)
                //    {
                //        DataColumn dc = newTable.Columns[i];
                //        string field = dc.ToString().Replace("{", "").Replace("}","");
                //        bool keep_field = fieldlist.Contains(field);
                //        if (keep_field == false)
                //        {
                //            dtEpics.Columns.Remove(field);
                //        }
                //    }
                //    //    foreach (DataColumn column in newTable.Columns)
                //    //{
                //    //    bool keep_field = fieldlist.Contains(column.ToString());
                //    //    if (keep_field == false)
                //    //    {
                //    //        dtEpics.Columns.Remove(column);
                //    //    }
                //    //}
                //}

                txtResult.Text = sbResponseBody.GetAsString();
                grdEpics.AutoGenerateColumns = true;

                grdEpics.DataSource = dtEpics;
                grdEpics.Columns["btn01"].IsPinned = true;
                grdEpics.Columns["btn02"].IsPinned = true;
                grdEpics.Columns["Id"].IsPinned = true;
                grdEpics.Columns["issue_type_name"].IsPinned = true;
                grdEpics.Columns["Summary"].IsPinned = true;
                format_grd_epic();
                //*
                //*************************************************************************************
                //*
                //*  Save the grid (ONLY if EPIC and STORIES is from JIRA
                //*
                //*************************************************************************************
                //*
                if (cbo_epic_from.Text.ToUpper() == "JIRA" && cbo_load_story_from.Text.ToUpper() == "JIRA")
                {
                    save_grid(cbo_DBO.Text, cbo_pi.Text, cbo_status.Text);
                    lblMessage.Text = "Data Save for later use...";
                    timer_clear_message.Enabled = true;
                }
                else
                {
                    lblMessage.Text = "Story and Epic MUST be JIRA before save it for history";
                    timer_clear_message.Enabled = true;
                }

                //format_status_chart(dtEpics);
                // FULL
                watchFullJira.Stop();
                var elapsedFull = watchFullJira.ElapsedMilliseconds;
                txtFullExecuteTime.Text = elapsedFull.ToString();

            }
            catch (Exception ex)
            {
                lb.clsLogger.WriteLog("Error:" + ex.Message);
                string error = "Error :" + ex;
                //clsLog.WriteLog(method, UniqueID, "Error :" + ex);
            }
        }
        public static (DataTable, DataTable) get_dt_4_epic_list_old(Chilkat.StringBuilder sbResponseBody_Epic, DataTable dtEpics_summary, string p_dbo_id, string parm_pi, string parm_status, string p_story_point_or_count, string p_columns_in_grid, bool p_store_users, bool p_load_acceptance, string p_summary_filter, bool p_load_comments, bool p_load_parent_links)
        {
            string m_method = "frmEpic_to_Story_Creator.get_dt_4_epic_list_old";
            Chilkat.StringBuilder sbResponseBody_Stories;
            Chilkat.StringBuilder sbResponseBody_Stories_with_detail;
            string applicationDirectory = Application.ExecutablePath;
            //            DataTable dt = new DataTable("AD Accounts");
            DataTable dtEpics;
            DataTable dtChart;
            //dtEpics = new DataTable();
            dtChart = new DataTable();
            //dtEpics.Clear();
            int i;
            int count_i;
            int value_id;
            string value_name;
            string issue_status;
            int issue_status_sort_id;
            Int32 count_gt_zero = 0;
            string ms_epic_prio = "";
            string issue_prio;
            string value_startdate;
            string value_enddate;
            string value_goal;
            string issue_summary;
            string m_issue_type_name = "";
            string issue_description;
            string data_path = "";
            string columns_in_grid = "";
            string m_name = "";
            string m_emailAddress = "";
            string m_displayName = "";
            string m_timezone = "";
            string m_issue_acceptance = "";
            Int32 m_acceptance_done = 0;
            Int32 m_acceptance_outstanding = 0;
            Int32 m_total_acceptance = 0;

            Int32 count_10502 = 0;
            Int32 count_10503 = 0;
            bool load_epic_row = true;
            DateTime m_last_update_date;
            Int32 u00 = 0;
            Int32 u01 = 0;
            Int32 u02 = 0;
            Int32 u03 = 0;
            Int32 u04 = 0;
            Int32 u05 = 0;
            Int32 u06 = 0;
            Int32 u07 = 0;
            Int32 u07p = 0;
            double elapsed_prio_total = 0;
            double elapsed_status_total = 0;
            string m_parent_summary = "";
            dtEpics = get_dt_layout_for_epic();

            try
            {
                data_path = lb.clsFile.get_data_path();
                //*****************************
                //* Get All Issues
                //*****************************
                Chilkat.JsonObject jsonResponse = new Chilkat.JsonObject();
                jsonResponse.LoadSb(sbResponseBody_Epic);
                count_i = jsonResponse.SizeOfArray("issues");

                i = 0;
                int cntNew = 0;
                int cntInSpecification = 0;
                int cntOpen = 0;
                int cntInProgress = 0;
                int cntInReview = 0;
                //string hostname = hostname;
                string[,] a_status = new string[10, 2];
                Int32 stop_num = 36;
                string m_parent_list = "";

                while (i < count_i)
                {
                    load_epic_row = true;
                    if (stop_num == 36)
                    {

                    }
                    var watch = System.Diagnostics.Stopwatch.StartNew();
                    var watch01 = System.Diagnostics.Stopwatch.StartNew();
                    m_acceptance_done = 0;
                    m_acceptance_outstanding = 0;
                    DataRow dr = dtEpics.NewRow();

                    jsonResponse.I = i;
                    string issue_json = jsonResponse.StringOf("issues[i].fields");
                    value_id = jsonResponse.IntOf("issues[i].id");
                    string m_issue_key = jsonResponse.StringOf("issues[i].key");
                    issue_status = jsonResponse.StringOf("issues[i].fields.status.name");
                    m_issue_acceptance = jsonResponse.StringOf("issues[i].fields.customfield_11100");
                    //Chilkat.StringBuilder sbResponseBodyAcceptance = lb.clsChilKat.get_stringbuilder_from_parameter(issue_acceptance);
                    //(Int32 m_acceptance_done, Int32 m_acceptance_outstanding) = lb.clsJira.get_acceptance_counts(sbResponseBodyAcceptance);
                    //*
                    //* Only store users if the STORE USER tic is selected
                    //*
                    if (p_store_users == true)
                    {

                        // Creator
                        m_name = jsonResponse.StringOf("issues[i].fields.creator.name");
                        if (m_name == "q140723")
                        {

                        }
                        m_emailAddress = jsonResponse.StringOf("issues[i].fields.creator.emailAddress");
                        m_displayName = jsonResponse.StringOf("issues[i].fields.creator.displayName");
                        m_timezone = jsonResponse.StringOf("issues[i].fields.creator.timeZone");
                        lb.clsDB_user_store_custom.store_user(m_name, m_displayName, m_emailAddress, m_timezone);
                        // Assignee
                        m_name = jsonResponse.StringOf("issues[i].fields.assignee.name");
                        m_emailAddress = jsonResponse.StringOf("issues[i].fields.assignee.emailAddress");
                        m_displayName = jsonResponse.StringOf("issues[i].fields.assignee.displayName");
                        m_timezone = jsonResponse.StringOf("issues[i].fields.assignee.timeZone");
                        lb.clsDB_user_store_custom.store_user(m_name, m_displayName, m_emailAddress, m_timezone);
                        if (m_name == "q140723")
                        {

                        }
                        // Reporter
                        m_name = jsonResponse.StringOf("issues[i].fields.reporter.name");
                        m_emailAddress = jsonResponse.StringOf("issues[i].fields.reporter.emailAddress");
                        m_displayName = jsonResponse.StringOf("issues[i].fields.reporter.displayName");
                        m_timezone = jsonResponse.StringOf("issues[i].fields.reporter.timeZone");
                        lb.clsDB_user_store_custom.store_user(m_name, m_displayName, m_emailAddress, m_timezone);
                        if (m_name == "q140723")
                        {

                        }
                        // customfield_10502
                        count_10502 = jsonResponse.SizeOfArray("issues[i].fields.customfield_10502");
                        if (count_10502 > 0)
                        {
                            m_name = jsonResponse.StringOf("issues[i].fields.customfield_10502[j].name");
                            if (m_name == "q140723")
                            {

                            }
                            m_emailAddress = jsonResponse.StringOf("issues[i].fields.customfield_10502[j].emailAddress");
                            m_displayName = jsonResponse.StringOf("issues[i].fields.customfield_10502[j].displayName");
                            m_timezone = jsonResponse.StringOf("issues[i].fields.customfield_10502[j].timeZone");
                            lb.clsDB_user_store_custom.store_user(m_name, m_displayName, m_emailAddress, m_timezone);
                        }
                        // customfield_10503
                        count_10503 = jsonResponse.SizeOfArray("issues[i].fields.customfield_10503");
                        if (count_10503 > 0)
                        {
                            m_name = jsonResponse.StringOf("issues[i].fields.customfield_10503[j].name");
                            if (m_name == "q140723")
                            {

                            }
                            m_emailAddress = jsonResponse.StringOf("issues[i].fields.customfield_10503[j].emailAddress");
                            m_displayName = jsonResponse.StringOf("issues[i].fields.customfield_10503[j].displayName");
                            m_timezone = jsonResponse.StringOf("issues[i].fields.customfield_10503[j].timeZone");
                            lb.clsDB_user_store_custom.store_user(m_name, m_displayName, m_emailAddress, m_timezone);
                        }
                    }
                    var elapsedMs01 = watch01.ElapsedMilliseconds;
                    watch01.Stop();
                    //*****************************
                    //* Get Issue Summary
                    //*****************************
                    issue_summary = jsonResponse.StringOf("issues[i].fields.summary");
                    string epic_last_updated = jsonResponse.StringOf("issues[i].fields.updated");
                    epic_last_updated = epic_last_updated.Substring(0, 10);
                    epic_last_updated = epic_last_updated.Replace('-', '/');
                    m_issue_type_name = jsonResponse.StringOf("issues[i].fields.issuetype.name");
                    string m_parent_link = jsonResponse.StringOf("issues[i].fields.customfield_11201");
                    //*****************************
                    //* Get Parent Summary
                    //*****************************
                    DataTable dtEpics_summary_filter = new DataTable();
                    //DataView dvEpics_summary;
                    //dvEpics_summary.RowFilter = "key = '" + m_issue_key + "'"; // query example = "id = 10"
                    DataView dv = new DataView(dtEpics_summary);
                    dv.RowFilter = "key='" + m_issue_key + "'";
                    dtEpics_summary_filter = dv.ToTable();
                    string m_parent_assignee_uid = "";
                    string m_parent_assignee_short = "";
                    string m_parent_assignee_name = "";
                    string m_label01 = "";
                    string m_label02 = "";
                    string m_label03 = "";
                    string m_label_list = "";
                    string m_effort_size = "";
                    string ms_story_points = "";
                    string m_epic_due = "";


                    Double m_story_points = 0;
                    foreach (DataRow row in dtEpics_summary_filter.Rows)
                    {
                        m_parent_summary = row["parent_summary"].ToString();
                        m_parent_assignee_uid = row["parent_assignee_uid"].ToString();
                        m_parent_assignee_short = row["parent_assignee_short"].ToString();
                        m_parent_assignee_name = row["parent_assignee_name"].ToString();
                        m_epic_due = row["epic_due"].ToString();
                        m_label01 = row["label01"].ToString();
                        m_label02 = row["label02"].ToString();
                        m_label03 = row["label03"].ToString();
                        m_label_list = row["label_list"].ToString();
                        m_label_list = m_label_list.Replace("qqqq",";");

                        m_effort_size = row["effort_size"].ToString();
                        ms_story_points = row["story_points"].ToString();
                        if (ms_story_points == "null")
                        {
                            m_story_points = 0;
                        }
                        else
                        {
                            ms_story_points = ms_story_points.Replace(".", ",");
                            m_story_points = Convert.ToDouble(ms_story_points);
                        }
                        //string m_parent_prio = lb.clsResponseBody.get_parent_prio(sbResponseBody_Epic);
                    }

                    //value_startdate = jsonResponse.StringOf("values[i].startDate");
                    //value_enddate = jsonResponse.StringOf("values[i].endDate");
                    //value_goal = jsonResponse.StringOf("values[i].goal");


                    //*****************************
                    //* build DataRow
                    //*****************************
                    dr["Id"] = value_id;
                    dr["Key"] = m_issue_key;
                    if (m_issue_key == "EDAD-1300")
                    {

                    }
                    dr["btn03"] = "Show Labels";
                    dr["btn01"] = "Stories";
                    dr["btn02"] = m_issue_key;
                    dr["btn04"] = "Change";
                    dr["parent_link"] = m_parent_link;
                    dr["parent_link_url"] = "https://atc.bmwgroup.net/jira/browse/" + m_parent_link;
                    dr["parent_summary"] = m_parent_summary;
                    dr["parent_assignee_uid"] = m_parent_assignee_uid;
                    dr["parent_assignee_short"] = m_parent_assignee_short;
                    dr["parent_assignee_name"] = m_parent_assignee_name;
                    dr["label01"] = m_label01;
                    dr["label02"] = m_label02;
                    dr["label03"] = m_label03;
                    dr["label_list"] = m_label_list;
                    dr["issue_type_name"] = m_issue_type_name;
                    dr["effort_size"] = m_effort_size;
                    dr["btn_prio"] = "MPS";
                    ms_epic_prio = lb.clsResponseBody.get_parent_prio(sbResponseBody_Epic, m_issue_key);
                    dr["epic_prio"] = ms_epic_prio;
                    dr["story_points"] = m_story_points;
                    dr["epic_due"] = m_epic_due;
                    dr["URL"] = "https://atc.bmwgroup.net/jira/browse/" + m_issue_key;

                    dr["Epic Updated"] = epic_last_updated;
                    //                sbResponseBody = lb.clsJira.get_user_stories_for_epic(hostname,value_key);
                    //*****************************
                    //* Build the Status
                    //*****************************
                    string sFull_Path = "";
                    var watch03 = System.Diagnostics.Stopwatch.StartNew();
                    sFull_Path = lb.clsFile.get_latest_file_for_dbo_id_and_jira_key(Convert.ToInt32(p_dbo_id), m_issue_key);
                    if (sFull_Path == "")
                    {
                        // sFull_Path = save_json_for_all_stories_linked_to_epic(hostname, value_key, p_dbo_id, parm_pi, parm_status);
                        sFull_Path = save_json_for_all_stories_linked_to_epic(m_issue_key, p_dbo_id);
                    }
                    //*
                    //*************************************************************************************************************************
                    //*
                    //* Now start with the USER STORIES 
                    //*
                    //*************************************************************************************************************************
                    //*
                    watch.Stop();
                    var elapsedMs = watch.ElapsedMilliseconds;
                    string text = File.ReadAllText(sFull_Path);
                    watch03.Stop();
                    var elapsedMs03 = watch03.ElapsedMilliseconds;
                    var watch04 = System.Diagnostics.Stopwatch.StartNew();
                    sbResponseBody_Stories = lb.clsChilKat.get_stringbuilder_from_file_path(sFull_Path);
                    //a_status = get_acceptance_counts(sbResponseBody_Stories, p_story_point_or_count);
                    if (p_load_acceptance == true)
                    {

                        if (m_issue_key == "EDAD-2651")
                        {

                        }
                        (m_acceptance_done, m_acceptance_outstanding, m_total_acceptance) = lb.clsJira.get_user_story_acceptance_cnt(p_dbo_id, m_issue_key, m_acceptance_done, m_acceptance_outstanding, m_total_acceptance);
                        dr["acceptance_done"] = m_acceptance_done;
                        dr["acceptance_outstanding"] = m_acceptance_outstanding;
                        dr["total_acceptance"] = m_total_acceptance;

                    }
                    else
                    {
                        dr["acceptance_done"] = "0";
                        dr["acceptance_outstanding"] = "0";
                        dr["total_acceptance"] = "0";
                    }
                    //*
                    //* Get Last Story UPDATE DATE
                    //*

                    (m_last_update_date, u00, u01, u02, u03, u04, u05, u06, u07, u07p) = lb.clsJira.get_last_update_date_for_epic(p_dbo_id, m_issue_key);
                    dr["storie_last_updated"] = (m_last_update_date.ToString()).Substring(0, 10);
                    dr["u00"] = u00;
                    dr["u01"] = u01;
                    dr["u02"] = u02;
                    dr["u03"] = u03;
                    dr["u04"] = u04;
                    dr["u05"] = u05;
                    dr["u06"] = u06;
                    dr["u07"] = u07;
                    dr["u07p"] = u07p;
                    //*
                    //* Get All the User Stories in the EPIC
                    //*
                    if (1 == 2)
                    {
                        string m_issue_list = lb.clsResponseBody.get_issue_list(sbResponseBody_Stories);
                        m_issue_list = m_issue_list.TrimEnd(',');
                        string m_issue_id_list = lb.clsResponseBody.get_issue_id_list(sbResponseBody_Stories);
                        m_issue_id_list = m_issue_id_list.TrimEnd(',');
                        //string jira_url = lb.clsJira.get_url_for_epic_from_epic_list(m_issue_list);
                        string jira_url = lb.clsJira.get_url_for_epic_from_epic_id_list(m_issue_id_list);
                        sbResponseBody_Stories_with_detail = lb.clsJira.get_json_obj_for_url(jira_url);
                        //*
                        //* Now take the USER STORY DETAIL
                        //*
                        string aaaa = lb.clsResponseBody.get_comments(sbResponseBody_Stories_with_detail);
                    }

                    //*
                    //* Get the STATUS from the USER STORY DETAIL
                    //*
                    if (m_issue_key == "EDAD-1300")
                    {

                    }
                    a_status = get_story_status_for_epic(sbResponseBody_Stories, p_story_point_or_count);

                    //string status = "";
                    //string status_val = "";
                    //bool is_all_user_stories_resolved = true;
                    //bool have_any_resolved_user_stories = false;
                    watch04.Stop();
                    var elapsedMs04 = watch04.ElapsedMilliseconds;
                    var watch06 = System.Diagnostics.Stopwatch.StartNew();
                    dr["Epic Status"] = issue_status;
                    //dr["Description"] = issue_description;
                    dr["Summary"] = issue_summary;
                    //dr["StartDate"] = value_startdate;
                    //dr["EndDate"] = value_enddate;
                    //dr["Goal"] = value_goal;
                    //dr["Marks"] = "500";

                    bool summary_is_valid = issue_summary.ToLower().Contains(p_summary_filter.ToLower());
                    if (summary_is_valid == false)
                    {
                        load_epic_row = false;
                    }
                    if (load_epic_row == true)
                    {
                        dtEpics.Rows.Add(dr);
                    }

                    //dr[i] = value_id;
                    //fieldskey = jsonResponse.StringOf("fields.issues[i].key");
                    //fieldsurl = jsonResponse.StringOf("fields.issues.self");
                    i = i + 1;
                    watch06.Stop();
                    var elapsedMs06 = watch06.ElapsedMilliseconds;
                    //************************************************************************
                    //* Issue Status (STORY)
                    //************************************************************************
                    var watch_status = System.Diagnostics.Stopwatch.StartNew();
                    (Int32 m_StatusNew, Int32 m_StatusOpen, Int32 m_StatusInProgress, Int32 m_StatusInSpecification, Int32 m_StatusPending, Int32 m_StatusInReview, Int32 m_StatusResolved, Int32 m_StatusClosed, String m_all_stories_resolved) = lb.clsResponseBody.get_issue_status(sbResponseBody_Stories);
                    watch_status.Stop();
                    var elapsed_status = watch_status.ElapsedMilliseconds;
                    elapsed_status_total = elapsed_prio_total + Convert.ToDouble(elapsed_status);
                    dr["New"] = m_StatusNew;
                    dr["Open"] = m_StatusOpen;
                    dr["InProgress"] = m_StatusInProgress;
                    dr["InSpecification"] = m_StatusInSpecification;
                    dr["Pending"] = m_StatusPending;
                    dr["InReview"] = m_StatusInReview;
                    dr["Resolved"] = m_StatusResolved;
                    dr["Closed"] = m_StatusClosed;
                    dr["All Stories Resolved"] = m_all_stories_resolved;
                    //************************************************************************
                    //* Issue Priority (STORY)
                    //************************************************************************
                    var watch_prio = System.Diagnostics.Stopwatch.StartNew();
                    (Int32 m_PrioLow, Int32 m_PrioMedium, Int32 m_PrioHigh, Int32 m_PrioCritical) = lb.clsResponseBody.get_issue_priority(sbResponseBody_Stories);
                    watch_prio.Stop();
                    var elapsed_prio = watch_prio.ElapsedMilliseconds;
                    elapsed_prio_total = elapsed_prio_total + Convert.ToDouble(elapsed_prio);
                    dr["issue_prio_low"] = m_PrioLow;
                    dr["issue_prio_med"] = m_PrioMedium;
                    dr["issue_prio_high"] = m_PrioHigh;
                    dr["issue_prio_critical"] = m_PrioCritical;
                    if (elapsed_prio > 0)
                    {
                        count_gt_zero = count_gt_zero + 1;
                    }
                    string stimes = "elapsedMs01:" + elapsedMs01 + " : elapsed_prio:" + elapsed_prio + " : elapsedMs03:" + elapsedMs03 + " : elapsedMs04:" + elapsedMs04 + " : elapsedMs06:" + elapsedMs06;
                    //************************************************************************
                    //* Check Errors
                    //************************************************************************
                    //* 1) Check if Epic Prio and Story Prio the same or story is higher
                    string m_have_error = "";
                    string m_error = "";
                    (m_have_error, m_error) = lb.clsJira.check_epic_prio_vs_child_prio(ms_epic_prio, m_PrioLow, m_PrioMedium, m_PrioHigh, m_PrioCritical);

                    dr["error_in_line"] = m_have_error;
                    dr["error"] = m_error;

                }
                //********************************
                //* Add DataTable Columns to Graph
                //********************************
                //dtChart.Columns.Add("Status");
                //dtChart.Columns.Add("Count");
                //*****************************
                //* build Status for Graph Row
                //*****************************
                DataRow drChart = dtChart.NewRow();
                //drChart["Status"] = "New";
                //drChart["Count"] = cntNew;
                //// In Specification
                //drChart["Status"] = "In Specification";
                //drChart["Count"] = cntInSpecification;
                //// Open
                //drChart["Status"] = "Open";
                //drChart["Count"] = cntOpen;
                //*****************************
                //* Return
                //*****************************
                return (dtEpics, dtChart);

            }
            catch (Exception ex)
            {
                lb.clsLogger.WriteLog("Error:" + ex.Message);
                return (dtEpics, dtChart);
            }
        }
        public static string[,] get_story_status_for_epic(Chilkat.StringBuilder sbResponseBody, string p_story_point_or_count)
        {
            string m_method = "frmEpic_to_Story_Creator.get_story_status_for_epic";
            string url_for_epic = "";
            string[,] a_status = new string[10, 2];
            try
            {
                if (p_story_point_or_count == "Story")
                {
                    a_status = lb.clsJira.get_status_count_from_Epic(sbResponseBody);
                }

                if (p_story_point_or_count == "Story Points")
                {
                    a_status = lb.clsJira.get_user_story_points_from_Epic(sbResponseBody);
                }
                return a_status;
            }
            catch (Exception ex)
            {
                lb.clsLogger.WriteLog("Error in : " + m_method + " : " + ex.Message);
                return a_status;
            }
        }
        //*
        //*******************************************************************************
        //*
        //*    Save JSON for ALL STORIES linked to a EPIC
        //*
        //*******************************************************************************
        //*string sFull_Path = save_json_for_all_stories_linked_to_epic(hostname,epic);
        //public static string save_json_for_all_stories_linked_to_epic(string hostname, string epic, string parm_dbo, string parm_pi, string parm_status)
        public static string save_json_for_all_stories_linked_to_epic(string epic, string p_dbo_id)
        {
            string m_method = "frmEpic_to_Story_Creator.save_json_for_all_stories_linked_to_epic";
            string sFull_Path = "";
            string m_dbo = "";
            try
            {
                m_dbo = lb.clsDB_dbo_custom.get_dbo_value_from_id(Convert.ToInt32(p_dbo_id));
                DateTimeOffset now = (DateTimeOffset)DateTime.Now;
                string ts = now.ToString("yyyyMMdd_HH_mm_ss_fff");
                string sPath = System.IO.Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
                //string sData_Path = sPath + @"\Data\" + parm_dbo + "_" + parm_pi + "_" + parm_status + @"\";
                string sData_Path = sPath + @"\Data\" + m_dbo + @"\";
                sFull_Path = sData_Path + @"\" + epic + @"_" + ts + @".json";

                Chilkat.StringBuilder sbResponseBody = lb.clsJira.get_user_stories_for_epic(epic);

                lb.clsFile.write_text_to_file_by_type("JSON", sFull_Path, sbResponseBody.GetAsString(), false);
                return sFull_Path;
            }
            catch (Exception ex)
            {
                lb.clsLogger.WriteLog("Error in : " + m_method + " : " + ex.Message);
                return sFull_Path;
            }
        }
        private void save_grid(string p_dbo, string p_pi, string p_status)
        {
            string m_path = "";
            string m_grd_epic_name = "";
            string m_method = "frmEpic_to_Story_Creator.save_grid";
            try
            {
                (m_path, m_grd_epic_name) = get_saved_grd_epics_name(p_dbo, p_pi, p_status);
                DataTable dt = new DataTable();
                dt = (DataTable)grdEpics.DataSource;
                string m_csv = lb.clsExcel.DataTableToCSV(dt, ";");
                File.WriteAllText(m_grd_epic_name, m_csv.ToString());
                txt_history_path.Text = m_path;
            }
            catch (Exception ex)
            {
                lb.clsLogger.WriteLog("Error in : " + m_method + " : " + ex.Message);
            }
        }
        public static (string, string) get_saved_grd_epics_name(string p_dbo, string p_pi, string p_status)
        {
            string m_path = "";
            string m_grd_epic_name = "";

            try
            {
                //m_grd_epic_name = p_dbo;
                //m_grd_epic_name = "pi_start_" + m_grd_epic_name + p_pi + "_pi_end_";
                //m_grd_epic_name = "status_start_" + m_grd_epic_name + p_status + "_status_end_";
                //m_grd_epic_name = "epic_start_" + m_grd_epic_name + p_epic_from + "_epic_end_";
                //m_grd_epic_name = "story_start_" + m_grd_epic_name + p_story_from + "_story_end_.csv";
                DateTimeOffset now = (DateTimeOffset)DateTime.Now;
                //string ts = now.ToString("yyyyMMdd_HH_mm_ss_fff");
                string ts = now.ToString("yyyyMMdd_HHmmss_fff");
                m_grd_epic_name = p_dbo + "_";
                m_grd_epic_name = m_grd_epic_name + p_pi + "_";
                m_grd_epic_name = m_grd_epic_name + p_status + "_" + ts + ".csv";
                //Fix
                //"QualityPI III/2022_New <-> Closed_Jira_File_"
                m_grd_epic_name = m_grd_epic_name.ToLower();
                m_grd_epic_name = m_grd_epic_name.Replace("/", "_");
                m_grd_epic_name = m_grd_epic_name.Replace(" ", "_");
                m_grd_epic_name = m_grd_epic_name.Replace("_pi_", "_pi-");
                m_grd_epic_name = m_grd_epic_name.Replace("<", "");
                m_grd_epic_name = m_grd_epic_name.Replace(">", "");
                m_grd_epic_name = m_grd_epic_name.Replace("__", "_");
                m_grd_epic_name = m_grd_epic_name.Replace("_-_", "_");
                //string sPath = System.IO.Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
                //string sHistoryPath = sPath + @"\history\";
                //if (!Directory.Exists(sHistoryPath))
                //{
                //    Directory.CreateDirectory(sHistoryPath);
                //}
                //******************************************************
                //*
                //* History Path
                //*
                //******************************************************
                //*
                string sPath = System.IO.Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
                string sHistoryPath = sPath + @"\history\";
                if (!Directory.Exists(sHistoryPath))
                {
                    Directory.CreateDirectory(sHistoryPath);
                }
                //string logPath = ConfigurationManager.AppSettings["logPath"];
                m_grd_epic_name = sHistoryPath + m_grd_epic_name;
                return (sHistoryPath, m_grd_epic_name);
            }
            catch (Exception ex)
            {
                lb.clsLogger.WriteLog("Error: " + ex.Message);
                return ("", m_grd_epic_name);
            }
        }

        private void btn_filter_Click(object sender, EventArgs e)
        {
            string m_grd_epic_name = "";
            string m_method = "frmEpic_to_Story_Creator.btn_load_prev_data_Click";
            string m_file_path = "";
            try
            {


                //m_file_path = "C:\\AAA\\A Programs\\C#\\JiraRest02\\JiraRest02\\bin\\Debug\\history\\quality_iii_2022_pending_jira_jira_20221025_22_06_33_264.csv";
                // m_file_path = "C:\\AAA\\A Programs\\C#\\JiraRest02\\JiraRest02\\bin\\Debug\\history\\quality_iv_2022_new_closed_jira_jira_20221027_06_36_50_031.csv";
                m_file_path = txt_history_path.Text + cbo_files.Text;
                DataTable dt = new DataTable();
                dt = lb.clsExcel.csvToDataTable(m_file_path, ';');
                if (txt_filter.Text.Trim().Length > 0)
                {
                    dt = lb.clsDataTable.dt_filter(dt, txt_filter.Text);
                }

                //dt = lb.clsExcel.ConvertCSVtoDataTable_02(m_file_path);
                grdEpics.AutoGenerateColumns = true;
                grdEpics.DataSource = dt;
                format_grd_epic();
                grdEpics.BestFitColumns();
                txtEpicCnt.Text = dt.Rows.Count.ToString();
                //dt = (DataTable)grdEpics.DataSource;
                //string m_csv = lb.clsExcel.DataTableToCSV(dt, "\t");
                //File.WriteAllText(m_grd_epic_name, m_csv.ToString());

            }
            catch (Exception ex)
            {

                lb.clsLogger.WriteLog("Error in : " + m_method + " : " + ex.Message);
            }
        }

        private void grdEpics_CellClick(object sender, GridViewCellEventArgs e)
        {
            string m_method = "frmEpic_to_Story_Creator.grdEpics_CellClick";
            var selected_cell_val = "";
            string url = "";
            string m_parent_link_url = "";
            try
            {
                if (e.Row is GridViewTableHeaderRowInfo)
                {
                    return;
                }


                //if (grdEpics.CurrentCell.Value != null)
                //{
                //    selected_cell_val = grdEpics.CurrentCell.Value.ToString();
                //}

                // If same then URL was selected
                if (grdEpics.CurrentColumn.HeaderText == "Stories")
                {
                    string epic = (string)grdEpics.SelectedRows[0].Cells["Key"].Value;
                    string dbo = cbo_DBO.Text;
                    string dbo_id = cbo_DBO.Items[cbo_DBO.SelectedIndex].Value.ToString();
                    string epic_description = (string)grdEpics.SelectedRows[0].Cells["Summary"].Value;
                    //frmStories_for_an_Epic f2 = new frmStories_for_an_Epic(dbo_id, epic, epic_description);
                    //f2.ShowDialog(); // Shows Form2
                }
                if (grdEpics.CurrentColumn.HeaderText == "Change Issue")
                {
                    string epic = (string)grdEpics.SelectedRows[0].Cells["Key"].Value;
                    txt_issue_number.Text = epic;
                    //string dbo = cbo_DBO_old.Text;
                    //string dbo_id = cbo_DBO_old.Items[cbo_DBO_old.SelectedIndex].Value.ToString();
                    //string epic_description = (string)grdEpics.SelectedRows[0].Cells["Summary"].Value;
                    //frmStories_for_an_Epic f2 = new frmStories_for_an_Epic(dbo_id, epic, epic_description);
                    //f2.ShowDialog(); // Shows Form2
                }
                if (grdEpics.CurrentColumn.HeaderText == "Open Epic")
                {
                    //string target = "http://www.microsoft.com";
                    //Use no more than one assignment when you test this code.
                    //string target = "ftp://ftp.microsoft.com";
                    //string target = "C:\\Program Files\\Microsoft Visual Studio\\INSTALL.HTM";
                    try
                    {
                        url = (string)grdEpics.SelectedRows[0].Cells["URL"].Value;

                        System.Diagnostics.Process.Start(url);
                    }
                    catch (System.Exception ex)
                    {
                        lb.clsLogger.WriteLog("Error in (2): " + m_method + " : " + ex.Message);
                        //MessageBox.Show(ex.Message);
                    }
                }
                if (grdEpics.CurrentColumn.FieldName == "parent_link")
                {
                    try
                    {
                        m_parent_link_url = (string)grdEpics.SelectedRows[0].Cells["parent_link_url"].Value;
                        System.Diagnostics.Process.Start(m_parent_link_url);
                    }
                    catch (Exception ex)
                    {
                        lb.clsLogger.WriteLog("Error in : " + m_method + " : " + ex.Message);
                    }
                }
                if (grdEpics.CurrentColumn.HeaderText == "Summary")
                {
                    txtEpicContains.Text = (string)grdEpics.SelectedRows[0].Cells["Summary"].Value;
                }
                if (selected_cell_val == url)
                {
                    //string target = "http://www.microsoft.com";
                    //Use no more than one assignment when you test this code.
                    //string target = "ftp://ftp.microsoft.com";
                    //string target = "C:\\Program Files\\Microsoft Visual Studio\\INSTALL.HTM";
                    try
                    {
                        System.Diagnostics.Process.Start(url);
                    }
                    catch (System.Exception ex)
                    {
                        lb.clsLogger.WriteLog("Error in (2) : " + m_method + " : " + ex.Message);
                        //MessageBox.Show(other.Message);
                    }
                }
            }
            catch (Exception ex)
            {
                lb.clsLogger.WriteLog("Error in : " + m_method + " : " + ex.Message);
            }
        }

        private void cmd_add_comment_Click(object sender, EventArgs e)
        {
            string m_issue_number = "";
            string m_comment = "";
            try
            {
                m_issue_number = txt_issue_number.Text.ToString();
                m_comment = txt_comment.Text.ToString();
                Int32 rc = lb.clsJira_Updates.add_comment(m_issue_number, m_comment);
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        private void cbo_comment_SelectedIndexChanged(object sender, Telerik.WinControls.UI.Data.PositionChangedEventArgs e)
        {
            string m_method = "frmEpic_to_Story_Creator.cbo_comment_SelectedIndexChanging";
            try
            {
                txt_comment.Text = cbo_comment.Text;
            }
            catch (Exception ex)
            {
                lb.clsLogger.WriteLog("Error in : " + m_method + " : " + ex.Message);
            }
        }
    }
}
