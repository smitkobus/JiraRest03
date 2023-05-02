using JiraRest03.Forms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Telerik.WinControls;
using Telerik.WinControls.UI;

namespace JiraRest03
{
    public partial class frmMenu : Telerik.WinControls.UI.RadForm
    {
        public frmMenu()
        {
            InitializeComponent();
        }

        private void radMenuEpic_to_StoryCreator_Click(object sender, EventArgs e)
        {
            string m_method = "frmMenu.radMenuEpic_to_StoryCreator_Click";
            try
            {
                frmEpic_to_Story_Creator f1 = new frmEpic_to_Story_Creator();
                f1.ShowDialog();
            }
            catch (Exception ex)
            {

                lb.clsLogger.WriteLog("Error in : " + m_method + " : " + ex.Message);
            }
        }

        private void radMenuEpicCreator_Click(object sender, EventArgs e)
        {

        }
    }
}
