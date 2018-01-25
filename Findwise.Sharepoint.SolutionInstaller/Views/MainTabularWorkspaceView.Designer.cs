namespace Findwise.Sharepoint.SolutionInstaller.Views
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
            Findwise.Sharepoint.SolutionInstaller.Views.TableLayout tableLayout1 = new Findwise.Sharepoint.SolutionInstaller.Views.TableLayout();
            Findwise.Sharepoint.SolutionInstaller.Views.TableLayout tableLayout2 = new Findwise.Sharepoint.SolutionInstaller.Views.TableLayout();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.InstallerModuleMainView1 = new Findwise.Sharepoint.SolutionInstaller.Views.WorkspaceInstallerModulesView();
            this.installerModuleMainView2 = new Findwise.Sharepoint.SolutionInstaller.Views.WorkspaceInstallerModulesView();
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
            this.InstallerModuleMainView1.Icon = global::Findwise.Sharepoint.SolutionInstaller.Properties.Resources.brick_icon;
            tableLayout1.Column = 0;
            tableLayout1.Row = 0;
            this.InstallerModuleMainView1.Layout = tableLayout1;
            this.InstallerModuleMainView1.Order = 0;
            this.InstallerModuleMainView1.SelectedObjects = new Findwise.Configuration.ConfigurationBase[0];
            this.InstallerModuleMainView1.Title = "Installer Modules";
            this.InstallerModuleMainView1.ToolBoxAvailable = true;
            // 
            // installerModuleMainView2
            // 
            this.installerModuleMainView2.Controllers = null;
            this.installerModuleMainView2.DataSource = null;
            this.installerModuleMainView2.Icon = global::Findwise.Sharepoint.SolutionInstaller.Properties.Resources.if_Delete_46730;
            tableLayout2.Column = 0;
            tableLayout2.Row = 0;
            this.installerModuleMainView2.Layout = tableLayout2;
            this.installerModuleMainView2.Order = 1;
            this.installerModuleMainView2.SelectedObjects = new Findwise.Configuration.ConfigurationBase[0];
            this.installerModuleMainView2.Title = "Test panel";
            this.installerModuleMainView2.ToolBoxAvailable = false;
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
        private WorkspaceInstallerModulesView installerModuleMainView2;
        internal WorkspaceInstallerModulesView InstallerModuleMainView1;
    }
}
