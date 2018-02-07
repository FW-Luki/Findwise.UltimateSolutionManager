namespace Findwise.Sharepoint.SolutionInstaller.Views
{
    partial class MainPropertyGridViewDesigner
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
            this.sizeablePanel1 = new Findwise.Sharepoint.SolutionInstaller.Controls.SizeablePanel();
            this.propertyGrid1 = new Findwise.Sharepoint.SolutionInstaller.Controls.PropertyGridEx();
            this.SelectedObjectToolStrip = new Findwise.Sharepoint.SolutionInstaller.Controls.FancyToolStrip();
            this.MasterConfigSelectToolStrip = new System.Windows.Forms.ToolStrip();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.MasterConfigSelectComboBox = new System.Windows.Forms.ToolStripComboBox();
            this.PropertyGridMergeToolStrip = new System.Windows.Forms.ToolStrip();
            this.RestoreDefaultToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.BindPropertyToolStripDropDownButton = new System.Windows.Forms.ToolStripDropDownButton();
            this.NewBindingSourceToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.BindingSourcesToolStripSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.UnbindPropertyToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.NonBindableToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.HelpToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.sizeablePanel1.SuspendLayout();
            this.MasterConfigSelectToolStrip.SuspendLayout();
            this.PropertyGridMergeToolStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // sizeablePanel1
            // 
            this.sizeablePanel1.Caption = "Properties";
            this.sizeablePanel1.Controls.Add(this.propertyGrid1);
            this.sizeablePanel1.Controls.Add(this.SelectedObjectToolStrip);
            this.sizeablePanel1.Controls.Add(this.MasterConfigSelectToolStrip);
            this.sizeablePanel1.Controls.Add(this.PropertyGridMergeToolStrip);
            this.sizeablePanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.sizeablePanel1.GripPosition = System.Windows.Forms.DockStyle.Left;
            this.sizeablePanel1.Location = new System.Drawing.Point(0, 0);
            this.sizeablePanel1.Name = "sizeablePanel1";
            this.sizeablePanel1.Size = new System.Drawing.Size(666, 150);
            this.sizeablePanel1.TabIndex = 0;
            // 
            // propertyGrid1
            // 
            this.propertyGrid1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.propertyGrid1.LineColor = System.Drawing.SystemColors.ControlDark;
            this.propertyGrid1.Location = new System.Drawing.Point(0, 83);
            this.propertyGrid1.Name = "propertyGrid1";
            this.propertyGrid1.PropertySort = System.Windows.Forms.PropertySort.Categorized;
            this.propertyGrid1.Size = new System.Drawing.Size(666, 67);
            this.propertyGrid1.TabIndex = 10;
            // 
            // SelectedObjectToolStrip
            // 
            this.SelectedObjectToolStrip.BackgroundGradientColor = System.Drawing.Color.Empty;
            this.SelectedObjectToolStrip.Location = new System.Drawing.Point(0, 58);
            this.SelectedObjectToolStrip.Name = "SelectedObjectToolStrip";
            this.SelectedObjectToolStrip.Padding = new System.Windows.Forms.Padding(2, 0, 1, 0);
            this.SelectedObjectToolStrip.Size = new System.Drawing.Size(666, 25);
            this.SelectedObjectToolStrip.SpecialBackgroundImage = null;
            this.SelectedObjectToolStrip.TabIndex = 9;
            // 
            // MasterConfigSelectToolStrip
            // 
            this.MasterConfigSelectToolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripLabel1,
            this.MasterConfigSelectComboBox});
            this.MasterConfigSelectToolStrip.Location = new System.Drawing.Point(0, 25);
            this.MasterConfigSelectToolStrip.Name = "MasterConfigSelectToolStrip";
            this.MasterConfigSelectToolStrip.Padding = new System.Windows.Forms.Padding(2, 1, 1, 0);
            this.MasterConfigSelectToolStrip.Size = new System.Drawing.Size(666, 33);
            this.MasterConfigSelectToolStrip.TabIndex = 7;
            this.MasterConfigSelectToolStrip.Text = "toolStrip1";
            this.MasterConfigSelectToolStrip.SizeChanged += new System.EventHandler(this.MasterConfigSelectToolStrip_SizeChanged);
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.Image = global::Findwise.Sharepoint.SolutionInstaller.Properties.Resources.if_magic_17537;
            this.toolStripLabel1.Margin = new System.Windows.Forms.Padding(2, 1, 4, 2);
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(16, 29);
            this.toolStripLabel1.ToolTipText = "Master Configuration";
            // 
            // MasterConfigSelectComboBox
            // 
            this.MasterConfigSelectComboBox.AutoSize = false;
            this.MasterConfigSelectComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.MasterConfigSelectComboBox.Margin = new System.Windows.Forms.Padding(1, 3, 1, 6);
            this.MasterConfigSelectComboBox.Name = "MasterConfigSelectComboBox";
            this.MasterConfigSelectComboBox.Size = new System.Drawing.Size(128, 23);
            // 
            // PropertyGridMergeToolStrip
            // 
            this.PropertyGridMergeToolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.RestoreDefaultToolStripButton,
            this.BindPropertyToolStripDropDownButton,
            this.UnbindPropertyToolStripButton,
            this.NonBindableToolStripButton,
            this.toolStripSeparator1,
            this.HelpToolStripButton});
            this.PropertyGridMergeToolStrip.Location = new System.Drawing.Point(0, 0);
            this.PropertyGridMergeToolStrip.Name = "PropertyGridMergeToolStrip";
            this.PropertyGridMergeToolStrip.Padding = new System.Windows.Forms.Padding(2, 0, 1, 0);
            this.PropertyGridMergeToolStrip.Size = new System.Drawing.Size(666, 25);
            this.PropertyGridMergeToolStrip.TabIndex = 6;
            this.PropertyGridMergeToolStrip.Text = "toolStrip3";
            // 
            // RestoreDefaultToolStripButton
            // 
            this.RestoreDefaultToolStripButton.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.RestoreDefaultToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.RestoreDefaultToolStripButton.Image = global::Findwise.Sharepoint.SolutionInstaller.Properties.Resources.if_draft_46845;
            this.RestoreDefaultToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.RestoreDefaultToolStripButton.Name = "RestoreDefaultToolStripButton";
            this.RestoreDefaultToolStripButton.Size = new System.Drawing.Size(23, 22);
            this.RestoreDefaultToolStripButton.Text = "Restore default value for selected property";
            this.RestoreDefaultToolStripButton.Click += new System.EventHandler(this.RestoreDefaultToolStripButton_Click);
            // 
            // BindPropertyToolStripDropDownButton
            // 
            this.BindPropertyToolStripDropDownButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.BindPropertyToolStripDropDownButton.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.NewBindingSourceToolStripMenuItem,
            this.BindingSourcesToolStripSeparator});
            this.BindPropertyToolStripDropDownButton.Image = global::Findwise.Sharepoint.SolutionInstaller.Properties.Resources.if_Lock_65762;
            this.BindPropertyToolStripDropDownButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.BindPropertyToolStripDropDownButton.Name = "BindPropertyToolStripDropDownButton";
            this.BindPropertyToolStripDropDownButton.Size = new System.Drawing.Size(29, 22);
            this.BindPropertyToolStripDropDownButton.Text = "Bind selected property";
            this.BindPropertyToolStripDropDownButton.Visible = false;
            // 
            // NewBindingSourceToolStripMenuItem
            // 
            this.NewBindingSourceToolStripMenuItem.Image = global::Findwise.Sharepoint.SolutionInstaller.Properties.Resources.if_list_add_15304;
            this.NewBindingSourceToolStripMenuItem.Name = "NewBindingSourceToolStripMenuItem";
            this.NewBindingSourceToolStripMenuItem.Size = new System.Drawing.Size(204, 22);
            this.NewBindingSourceToolStripMenuItem.Text = "Add new Binding Source";
            // 
            // BindingSourcesToolStripSeparator
            // 
            this.BindingSourcesToolStripSeparator.Name = "BindingSourcesToolStripSeparator";
            this.BindingSourcesToolStripSeparator.Size = new System.Drawing.Size(201, 6);
            // 
            // UnbindPropertyToolStripButton
            // 
            this.UnbindPropertyToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.UnbindPropertyToolStripButton.Image = global::Findwise.Sharepoint.SolutionInstaller.Properties.Resources.if_Lock_Open_65761;
            this.UnbindPropertyToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.UnbindPropertyToolStripButton.Name = "UnbindPropertyToolStripButton";
            this.UnbindPropertyToolStripButton.Size = new System.Drawing.Size(23, 22);
            this.UnbindPropertyToolStripButton.Text = "Unbind selected property";
            this.UnbindPropertyToolStripButton.Visible = false;
            // 
            // NonBindableToolStripButton
            // 
            this.NonBindableToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.NonBindableToolStripButton.Enabled = false;
            this.NonBindableToolStripButton.Image = global::Findwise.Sharepoint.SolutionInstaller.Properties.Resources.if_Lock_65762;
            this.NonBindableToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.NonBindableToolStripButton.Name = "NonBindableToolStripButton";
            this.NonBindableToolStripButton.Size = new System.Drawing.Size(23, 22);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // HelpToolStripButton
            // 
            this.HelpToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.HelpToolStripButton.Enabled = false;
            this.HelpToolStripButton.Image = global::Findwise.Sharepoint.SolutionInstaller.Properties.Resources.if_help_browser_15335;
            this.HelpToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.HelpToolStripButton.Name = "HelpToolStripButton";
            this.HelpToolStripButton.Size = new System.Drawing.Size(23, 22);
            this.HelpToolStripButton.Text = "Show help for selected property";
            // 
            // MainPropertyGridViewDesigner
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.sizeablePanel1);
            this.Name = "MainPropertyGridViewDesigner";
            this.Size = new System.Drawing.Size(666, 150);
            this.sizeablePanel1.ResumeLayout(false);
            this.sizeablePanel1.PerformLayout();
            this.MasterConfigSelectToolStrip.ResumeLayout(false);
            this.MasterConfigSelectToolStrip.PerformLayout();
            this.PropertyGridMergeToolStrip.ResumeLayout(false);
            this.PropertyGridMergeToolStrip.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private Controls.SizeablePanel sizeablePanel1;
        private System.Windows.Forms.ToolStrip PropertyGridMergeToolStrip;
        private System.Windows.Forms.ToolStripButton RestoreDefaultToolStripButton;
        private System.Windows.Forms.ToolStrip MasterConfigSelectToolStrip;
        private Controls.PropertyGridEx propertyGrid1;
        private Controls.FancyToolStrip SelectedObjectToolStrip;
        internal System.Windows.Forms.ToolStripComboBox MasterConfigSelectComboBox;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        internal System.Windows.Forms.ToolStripButton HelpToolStripButton;
        internal System.Windows.Forms.ToolStripDropDownButton BindPropertyToolStripDropDownButton;
        internal System.Windows.Forms.ToolStripButton UnbindPropertyToolStripButton;
        internal System.Windows.Forms.ToolStripButton NonBindableToolStripButton;
        internal System.Windows.Forms.ToolStripMenuItem NewBindingSourceToolStripMenuItem;
        internal System.Windows.Forms.ToolStripSeparator BindingSourcesToolStripSeparator;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
    }
}
