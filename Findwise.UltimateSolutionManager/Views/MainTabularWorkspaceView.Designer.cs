namespace Findwise.SolutionManager.Views
{
    partial class MainTabularWorkspaceViewDesigner
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            Findwise.SolutionManager.Views.TableLayout tableLayout1 = new Findwise.SolutionManager.Views.TableLayout();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainTabularWorkspaceViewDesigner));
            Findwise.SolutionManager.Views.TableLayout tableLayout2 = new Findwise.SolutionManager.Views.TableLayout();
            Findwise.SolutionManager.Views.TableLayout tableLayout3 = new Findwise.SolutionManager.Views.TableLayout();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.InstallerModuleMainView1 = new Findwise.SolutionManager.Views.WorkspaceInstallerModulesView();
            this.BindingsMainView1 = new Findwise.SolutionManager.Views.WorkspaceBindingsView();
            this.MasterConfigMainView1 = new Findwise.SolutionManager.Views.WorkspaceBindingsView();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.ImageList = this.imageList1;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(150, 150);
            this.tabControl1.TabIndex = 0;
            // 
            // imageList1
            // 
            this.imageList1.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit;
            this.imageList1.ImageSize = new System.Drawing.Size(22, 22);
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // InstallerModuleMainView1
            // 
            this.InstallerModuleMainView1.Controllers = null;
            this.InstallerModuleMainView1.DataSource = null;
            this.InstallerModuleMainView1.Icon = global::Findwise.SolutionManager.Properties.Resources.brick_icon;
            tableLayout1.Column = 0;
            tableLayout1.Row = 0;
            this.InstallerModuleMainView1.Layout = tableLayout1;
            this.InstallerModuleMainView1.Order = 0;
            this.InstallerModuleMainView1.SelectedObjects = new object[0];
            this.InstallerModuleMainView1.Title = "Installer Modules";
            this.InstallerModuleMainView1.ToolBoxAvailable = true;
            // 
            // BindingsMainView1
            // 
            this.BindingsMainView1.Controllers = null;
            this.BindingsMainView1.DataSource = null;
            this.BindingsMainView1.Icon = ((System.Drawing.Image)(resources.GetObject("BindingsMainView1.Icon")));
            tableLayout2.Column = 0;
            tableLayout2.Row = 0;
            this.BindingsMainView1.Layout = tableLayout2;
            this.BindingsMainView1.Order = 1;
            this.BindingsMainView1.SelectedObjects = new object[0];
            this.BindingsMainView1.Title = "Data Binding Sources";
            this.BindingsMainView1.ToolBoxAvailable = false;
            this.BindingsMainView1.UseDefaultIcon = true;
            // 
            // MasterConfigMainView1
            // 
            this.MasterConfigMainView1.Controllers = null;
            this.MasterConfigMainView1.DataSource = null;
            this.MasterConfigMainView1.Icon = global::Findwise.SolutionManager.Properties.Resources.if_magic_17537;
            tableLayout3.Column = 0;
            tableLayout3.Row = 0;
            this.MasterConfigMainView1.Layout = tableLayout3;
            this.MasterConfigMainView1.Order = 1;
            this.MasterConfigMainView1.SelectedObjects = new object[0];
            this.MasterConfigMainView1.Title = "Master Configurations";
            this.MasterConfigMainView1.ToolBoxAvailable = false;
            this.MasterConfigMainView1.UseDefaultIcon = false;
            // 
            // MainTabularWorkspaceViewDesigner
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tabControl1);
            this.Name = "MainTabularWorkspaceViewDesigner";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.ImageList imageList1;
        internal WorkspaceInstallerModulesView InstallerModuleMainView1;
        internal WorkspaceBindingsView BindingsMainView1;
        internal WorkspaceBindingsView MasterConfigMainView1;
    }
}
