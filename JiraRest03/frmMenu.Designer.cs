namespace JiraRest03
{
    partial class frmMenu
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.crystalDarkTheme1 = new Telerik.WinControls.Themes.CrystalDarkTheme();
            this.radMenu1 = new Telerik.WinControls.UI.RadMenu();
            this.radMenuItem3 = new Telerik.WinControls.UI.RadMenuItem();
            this.radMenuMaintenance_DBO = new Telerik.WinControls.UI.RadMenuItem();
            this.radMenuMaintenance_Projects = new Telerik.WinControls.UI.RadMenuItem();
            this.radMenuMaintenance_TaskTypes = new Telerik.WinControls.UI.RadMenuItem();
            this.radMenuMaintenance_Interface = new Telerik.WinControls.UI.RadMenuItem();
            this.radMenuMaintenance_Components = new Telerik.WinControls.UI.RadMenuItem();
            this.radMenuMaintenance_Template = new Telerik.WinControls.UI.RadMenuItem();
            this.radMenuItem1 = new Telerik.WinControls.UI.RadMenuItem();
            this.radMenuEpicCreator = new Telerik.WinControls.UI.RadMenuItem();
            this.radMenuEpic_to_StoryCreator = new Telerik.WinControls.UI.RadMenuItem();
            this.radMenuEpic_with_Stories_that_contains = new Telerik.WinControls.UI.RadMenuItem();
            this.radMenu_Stories_for_an_Epic = new Telerik.WinControls.UI.RadMenuItem();
            this.radMenu_Epic_vs_Template = new Telerik.WinControls.UI.RadMenuItem();
            this.radMenuItem2 = new Telerik.WinControls.UI.RadMenuItem();
            this.radMenuUpdateUserStory = new Telerik.WinControls.UI.RadMenuItem();
            this.radMenuUtils = new Telerik.WinControls.UI.RadMenuItem();
            this.radMenuFixJQL = new Telerik.WinControls.UI.RadMenuItem();
            this.radMenuGetIssueJSON = new Telerik.WinControls.UI.RadMenuItem();
            this.radMenuLogon = new Telerik.WinControls.UI.RadMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.radMenu1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            this.SuspendLayout();
            // 
            // radMenu1
            // 
            this.radMenu1.Items.AddRange(new Telerik.WinControls.RadItem[] {
            this.radMenuItem3,
            this.radMenuItem1,
            this.radMenuItem2,
            this.radMenuUtils});
            this.radMenu1.Location = new System.Drawing.Point(0, 0);
            this.radMenu1.Name = "radMenu1";
            this.radMenu1.Size = new System.Drawing.Size(959, 34);
            this.radMenu1.TabIndex = 1;
            this.radMenu1.ThemeName = "CrystalDark";
            // 
            // radMenuItem3
            // 
            this.radMenuItem3.Items.AddRange(new Telerik.WinControls.RadItem[] {
            this.radMenuMaintenance_DBO,
            this.radMenuMaintenance_Projects,
            this.radMenuMaintenance_TaskTypes,
            this.radMenuMaintenance_Interface,
            this.radMenuMaintenance_Components,
            this.radMenuMaintenance_Template});
            this.radMenuItem3.Name = "radMenuItem3";
            this.radMenuItem3.Text = "Maintenance";
            this.radMenuItem3.UseCompatibleTextRendering = false;
            // 
            // radMenuMaintenance_DBO
            // 
            this.radMenuMaintenance_DBO.Name = "radMenuMaintenance_DBO";
            this.radMenuMaintenance_DBO.Text = "DBO";
            this.radMenuMaintenance_DBO.UseCompatibleTextRendering = false;
            // 
            // radMenuMaintenance_Projects
            // 
            this.radMenuMaintenance_Projects.Name = "radMenuMaintenance_Projects";
            this.radMenuMaintenance_Projects.Text = "Projects";
            this.radMenuMaintenance_Projects.UseCompatibleTextRendering = false;
            // 
            // radMenuMaintenance_TaskTypes
            // 
            this.radMenuMaintenance_TaskTypes.Name = "radMenuMaintenance_TaskTypes";
            this.radMenuMaintenance_TaskTypes.Text = "Task Types";
            this.radMenuMaintenance_TaskTypes.UseCompatibleTextRendering = false;
            // 
            // radMenuMaintenance_Interface
            // 
            this.radMenuMaintenance_Interface.Name = "radMenuMaintenance_Interface";
            this.radMenuMaintenance_Interface.Text = "Interfaces";
            this.radMenuMaintenance_Interface.UseCompatibleTextRendering = false;
            // 
            // radMenuMaintenance_Components
            // 
            this.radMenuMaintenance_Components.Name = "radMenuMaintenance_Components";
            this.radMenuMaintenance_Components.Text = "Components";
            this.radMenuMaintenance_Components.UseCompatibleTextRendering = false;
            // 
            // radMenuMaintenance_Template
            // 
            this.radMenuMaintenance_Template.Name = "radMenuMaintenance_Template";
            this.radMenuMaintenance_Template.Text = "Templates";
            this.radMenuMaintenance_Template.UseCompatibleTextRendering = false;
            // 
            // radMenuItem1
            // 
            this.radMenuItem1.Items.AddRange(new Telerik.WinControls.RadItem[] {
            this.radMenuEpicCreator,
            this.radMenuEpic_to_StoryCreator,
            this.radMenuEpic_with_Stories_that_contains,
            this.radMenu_Stories_for_an_Epic,
            this.radMenu_Epic_vs_Template});
            this.radMenuItem1.Name = "radMenuItem1";
            this.radMenuItem1.Text = "Epic";
            this.radMenuItem1.UseCompatibleTextRendering = false;
            // 
            // radMenuEpicCreator
            // 
            this.radMenuEpicCreator.Name = "radMenuEpicCreator";
            this.radMenuEpicCreator.Text = "Epic Creator";
            this.radMenuEpicCreator.UseCompatibleTextRendering = false;
            this.radMenuEpicCreator.Click += new System.EventHandler(this.radMenuEpicCreator_Click);
            // 
            // radMenuEpic_to_StoryCreator
            // 
            this.radMenuEpic_to_StoryCreator.Name = "radMenuEpic_to_StoryCreator";
            this.radMenuEpic_to_StoryCreator.Text = "Epic to Story (Analyzer)";
            this.radMenuEpic_to_StoryCreator.UseCompatibleTextRendering = false;
            this.radMenuEpic_to_StoryCreator.Click += new System.EventHandler(this.radMenuEpic_to_StoryCreator_Click);
            // 
            // radMenuEpic_with_Stories_that_contains
            // 
            this.radMenuEpic_with_Stories_that_contains.Name = "radMenuEpic_with_Stories_that_contains";
            this.radMenuEpic_with_Stories_that_contains.Text = "Epic List - With Stories that Contains (For Test ONLY)";
            this.radMenuEpic_with_Stories_that_contains.UseCompatibleTextRendering = false;
            // 
            // radMenu_Stories_for_an_Epic
            // 
            this.radMenu_Stories_for_an_Epic.Name = "radMenu_Stories_for_an_Epic";
            this.radMenu_Stories_for_an_Epic.Text = "Stories for an EPIC (For Test ONLY)";
            this.radMenu_Stories_for_an_Epic.UseCompatibleTextRendering = false;
            // 
            // radMenu_Epic_vs_Template
            // 
            this.radMenu_Epic_vs_Template.Name = "radMenu_Epic_vs_Template";
            this.radMenu_Epic_vs_Template.Text = "Epic vs Template";
            this.radMenu_Epic_vs_Template.UseCompatibleTextRendering = false;
            // 
            // radMenuItem2
            // 
            this.radMenuItem2.Items.AddRange(new Telerik.WinControls.RadItem[] {
            this.radMenuUpdateUserStory});
            this.radMenuItem2.Name = "radMenuItem2";
            this.radMenuItem2.Text = "Issue";
            this.radMenuItem2.UseCompatibleTextRendering = false;
            // 
            // radMenuUpdateUserStory
            // 
            this.radMenuUpdateUserStory.Name = "radMenuUpdateUserStory";
            this.radMenuUpdateUserStory.Text = "Update Issue";
            this.radMenuUpdateUserStory.UseCompatibleTextRendering = false;
            // 
            // radMenuUtils
            // 
            this.radMenuUtils.Items.AddRange(new Telerik.WinControls.RadItem[] {
            this.radMenuFixJQL,
            this.radMenuGetIssueJSON,
            this.radMenuLogon});
            this.radMenuUtils.Name = "radMenuUtils";
            this.radMenuUtils.Text = "Utils";
            this.radMenuUtils.UseCompatibleTextRendering = false;
            // 
            // radMenuFixJQL
            // 
            this.radMenuFixJQL.Name = "radMenuFixJQL";
            this.radMenuFixJQL.Text = "Fix JQL";
            this.radMenuFixJQL.UseCompatibleTextRendering = false;
            // 
            // radMenuGetIssueJSON
            // 
            this.radMenuGetIssueJSON.Name = "radMenuGetIssueJSON";
            this.radMenuGetIssueJSON.Text = "Get Issue JSON";
            this.radMenuGetIssueJSON.UseCompatibleTextRendering = false;
            // 
            // radMenuLogon
            // 
            this.radMenuLogon.Name = "radMenuLogon";
            this.radMenuLogon.Text = "Logon";
            this.radMenuLogon.UseCompatibleTextRendering = false;
            // 
            // frmMenu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(959, 478);
            this.Controls.Add(this.radMenu1);
            this.Name = "frmMenu";
            this.Text = "Jira Tool";
            this.ThemeName = "CrystalDark";
            ((System.ComponentModel.ISupportInitialize)(this.radMenu1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Telerik.WinControls.Themes.CrystalDarkTheme crystalDarkTheme1;
        private Telerik.WinControls.UI.RadMenu radMenu1;
        private Telerik.WinControls.UI.RadMenuItem radMenuItem3;
        private Telerik.WinControls.UI.RadMenuItem radMenuMaintenance_DBO;
        private Telerik.WinControls.UI.RadMenuItem radMenuMaintenance_Projects;
        private Telerik.WinControls.UI.RadMenuItem radMenuMaintenance_TaskTypes;
        private Telerik.WinControls.UI.RadMenuItem radMenuMaintenance_Interface;
        private Telerik.WinControls.UI.RadMenuItem radMenuMaintenance_Components;
        private Telerik.WinControls.UI.RadMenuItem radMenuMaintenance_Template;
        private Telerik.WinControls.UI.RadMenuItem radMenuItem1;
        private Telerik.WinControls.UI.RadMenuItem radMenuEpicCreator;
        private Telerik.WinControls.UI.RadMenuItem radMenuEpic_to_StoryCreator;
        private Telerik.WinControls.UI.RadMenuItem radMenuEpic_with_Stories_that_contains;
        private Telerik.WinControls.UI.RadMenuItem radMenu_Stories_for_an_Epic;
        private Telerik.WinControls.UI.RadMenuItem radMenu_Epic_vs_Template;
        private Telerik.WinControls.UI.RadMenuItem radMenuItem2;
        private Telerik.WinControls.UI.RadMenuItem radMenuUpdateUserStory;
        private Telerik.WinControls.UI.RadMenuItem radMenuUtils;
        private Telerik.WinControls.UI.RadMenuItem radMenuFixJQL;
        private Telerik.WinControls.UI.RadMenuItem radMenuGetIssueJSON;
        private Telerik.WinControls.UI.RadMenuItem radMenuLogon;
    }
}