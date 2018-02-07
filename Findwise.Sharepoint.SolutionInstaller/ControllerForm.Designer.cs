﻿namespace Findwise.Sharepoint.SolutionInstaller
{
    partial class ControllerForm
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
            Findwise.Sharepoint.SolutionInstaller.Views.TableLayout tableLayout1 = new Findwise.Sharepoint.SolutionInstaller.Views.TableLayout();
            Findwise.Sharepoint.SolutionInstaller.Views.TableLayout tableLayout2 = new Findwise.Sharepoint.SolutionInstaller.Views.TableLayout();
            Findwise.Sharepoint.SolutionInstaller.Views.TableLayout tableLayout3 = new Findwise.Sharepoint.SolutionInstaller.Views.TableLayout();
            Findwise.Sharepoint.SolutionInstaller.Views.TableLayout tableLayout4 = new Findwise.Sharepoint.SolutionInstaller.Views.TableLayout();
            Findwise.Sharepoint.SolutionInstaller.Views.TableLayout tableLayout5 = new Findwise.Sharepoint.SolutionInstaller.Views.TableLayout();
            Findwise.Sharepoint.SolutionInstaller.Views.TableLayout tableLayout6 = new Findwise.Sharepoint.SolutionInstaller.Views.TableLayout();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ControllerForm));
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.MainToolStripView1 = new Findwise.Sharepoint.SolutionInstaller.Views.MainToolStripView();
            this.MainStatusStripView1 = new Findwise.Sharepoint.SolutionInstaller.Views.MainStatusStripView();
            this.MainPropertyGridView1 = new Findwise.Sharepoint.SolutionInstaller.Views.MainPropertyGridView();
            this.ProjectManager1 = new Findwise.Sharepoint.SolutionInstaller.Controllers.ProjectManager();
            this.MainToolboxView1 = new Findwise.Sharepoint.SolutionInstaller.Views.MainToolboxView();
            this.MainTabularWorkspaceView1 = new Findwise.Sharepoint.SolutionInstaller.Views.MainTabularWorkspaceView();
            this.MainLogView1 = new Findwise.Sharepoint.SolutionInstaller.Views.MainLogView();
            this.ModuleLoader1 = new Findwise.Sharepoint.SolutionInstaller.Controllers.ModuleLoader();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(384, 261);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // MainToolStripView1
            // 
            this.MainToolStripView1.Controllers = null;
            tableLayout1.Column = 0;
            tableLayout1.ColumnSpan = 3;
            tableLayout1.Row = 0;
            tableLayout1.RowStyle = new System.Windows.Forms.RowStyle();
            this.MainToolStripView1.Layout = tableLayout1;
            // 
            // MainStatusStripView1
            // 
            this.MainStatusStripView1.Controllers = null;
            tableLayout2.Column = 0;
            tableLayout2.ColumnSpan = 3;
            tableLayout2.Row = 3;
            tableLayout2.RowStyle = new System.Windows.Forms.RowStyle();
            this.MainStatusStripView1.Layout = tableLayout2;
            // 
            // MainPropertyGridView1
            // 
            this.MainPropertyGridView1.Controllers = null;
            tableLayout3.Column = 2;
            tableLayout3.ColumnStyle = new System.Windows.Forms.ColumnStyle();
            tableLayout3.Row = 1;
            this.MainPropertyGridView1.Layout = tableLayout3;
            this.MainPropertyGridView1.ProjectManager = this.ProjectManager1;
            this.MainPropertyGridView1.SelectedObjectName = "";
            this.MainPropertyGridView1.SelectedObjects = new object[0];
            this.MainPropertyGridView1.PropertyGridSelectedValueChanged += new System.EventHandler(this.MainPropertyGridView1_PropertyGridItemChanged);
            this.MainPropertyGridView1.SelectedMasterConfigurationChanged += new System.EventHandler(this.MainPropertyGridView1_SelectedMasterConfigurationChanged);
            // 
            // ProjectManager1
            // 
            this.ProjectManager1.EmptyProjectName = "New project";
            this.ProjectManager1.IsModified = false;
            this.ProjectManager1.WindowTitleBase = null;
            this.ProjectManager1.PropertyChanged += new System.ComponentModel.PropertyChangedEventHandler(this.ProjectManager1_PropertyChanged);
            this.ProjectManager1.ModuleStatusChanged += new System.EventHandler(this.ProjectManager1_ModuleStatusChanged);
            // 
            // MainToolboxView1
            // 
            this.MainToolboxView1.Controllers = null;
            tableLayout4.Column = 0;
            tableLayout4.ColumnStyle = new System.Windows.Forms.ColumnStyle();
            tableLayout4.Row = 1;
            this.MainToolboxView1.Layout = tableLayout4;
            this.MainToolboxView1.ModuleAdded += new System.EventHandler<Findwise.Sharepoint.SolutionInstaller.Views.MainToolboxView.ModuleAddedEventArgs>(this.MainToolboxView1_ModuleAdded);
            // 
            // MainTabularWorkspaceView1
            // 
            this.MainTabularWorkspaceView1.Controllers = null;
            tableLayout5.Column = 1;
            tableLayout5.ColumnStyle = new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F);
            tableLayout5.Row = 1;
            tableLayout5.RowStyle = new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F);
            this.MainTabularWorkspaceView1.Layout = tableLayout5;
            this.MainTabularWorkspaceView1.ProjectManager = this.ProjectManager1;
            this.MainTabularWorkspaceView1.ViewChanged += new System.EventHandler(this.MainTabularWorkspaceView1_ViewChanged);
            this.MainTabularWorkspaceView1.SelectedObjectsChanged += new System.EventHandler(this.MainTabularWorkspaceView1_SelectedObjectsChanged);
            // 
            // MainLogView1
            // 
            this.MainLogView1.Controllers = null;
            tableLayout6.Column = 0;
            tableLayout6.ColumnSpan = 3;
            tableLayout6.Row = 2;
            tableLayout6.RowStyle = new System.Windows.Forms.RowStyle();
            this.MainLogView1.Layout = tableLayout6;
            this.MainLogView1.LoggerName = "ColoredTextBox";
            // 
            // ModuleLoader1
            // 
            this.ModuleLoader1.PluginLoaded += new System.EventHandler<Findwise.Sharepoint.SolutionInstaller.Controllers.ModuleLoader.PluginLoadedEventArgs>(this.ModuleLoader1_PluginLoaded);
            this.ModuleLoader1.LoadingFinished += new System.EventHandler(this.ModuleLoader1_LoadingFinished);
            // 
            // ControllerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(384, 261);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "ControllerForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.WindowsDefaultBounds;
            this.Text = "Findwise Sharepoint Solution Installer";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ControllerForm_FormClosing);
            this.Load += new System.EventHandler(this.ControllerForm_Load);
            this.Shown += new System.EventHandler(this.ControllerForm_Shown);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private Views.MainToolStripView MainToolStripView1;
        private Views.MainStatusStripView MainStatusStripView1;
        private Views.MainPropertyGridView MainPropertyGridView1;
        private Views.MainToolboxView MainToolboxView1;
        private Views.MainTabularWorkspaceView MainTabularWorkspaceView1;
        private Views.MainLogView MainLogView1;
        private Controllers.ModuleLoader ModuleLoader1;
        private Controllers.ProjectManager ProjectManager1;
    }
}