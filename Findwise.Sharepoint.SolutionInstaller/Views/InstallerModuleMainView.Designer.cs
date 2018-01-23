﻿namespace Findwise.Sharepoint.SolutionInstaller.Views
{
    partial class InstallerModuleMainViewDesigner
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(InstallerModuleMainViewDesigner));
            this.TableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.NumberColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.IconColumn = new System.Windows.Forms.DataGridViewImageColumn();
            this.FriendlyNameColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NameColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.StatusColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.InstallColumn = new Findwise.Sharepoint.SolutionInstaller.Controls.DataGridViewHideableButtonColumn();
            this.ToolStrip = new System.Windows.Forms.ToolStrip();
            this.DuplicateToolStripButton = new Findwise.Sharepoint.SolutionInstaller.Controls.LockableToolStripButton();
            this.DeleteToolStripButton = new Findwise.Sharepoint.SolutionInstaller.Controls.LockableToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.MoveUpToolStripButton = new Findwise.Sharepoint.SolutionInstaller.Controls.LockableToolStripButton();
            this.MoveDownToolStripButton = new Findwise.Sharepoint.SolutionInstaller.Controls.LockableToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.RefreshToolStripButton = new Findwise.Sharepoint.SolutionInstaller.Controls.LockableToolStripButton();
            this.InstallAllToolStripButton = new Findwise.Sharepoint.SolutionInstaller.Controls.LockableToolStripButton();
            this.TableLayoutPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.ToolStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // TableLayoutPanel
            // 
            this.TableLayoutPanel.ColumnCount = 1;
            this.TableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.TableLayoutPanel.Controls.Add(this.dataGridView1, 0, 1);
            this.TableLayoutPanel.Controls.Add(this.ToolStrip, 0, 0);
            this.TableLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TableLayoutPanel.Location = new System.Drawing.Point(0, 0);
            this.TableLayoutPanel.Name = "TableLayoutPanel";
            this.TableLayoutPanel.RowCount = 2;
            this.TableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.TableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.TableLayoutPanel.Size = new System.Drawing.Size(320, 240);
            this.TableLayoutPanel.TabIndex = 0;
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AllowUserToResizeRows = false;
            this.dataGridView1.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dataGridView1.BackgroundColor = System.Drawing.SystemColors.Window;
            this.dataGridView1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dataGridView1.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.NumberColumn,
            this.IconColumn,
            this.FriendlyNameColumn,
            this.NameColumn,
            this.StatusColumn,
            this.InstallColumn});
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.Location = new System.Drawing.Point(3, 42);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.Size = new System.Drawing.Size(314, 195);
            this.dataGridView1.TabIndex = 4;
            // 
            // NumberColumn
            // 
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle1.Format = "#\\.";
            dataGridViewCellStyle1.NullValue = null;
            this.NumberColumn.DefaultCellStyle = dataGridViewCellStyle1;
            this.NumberColumn.HeaderText = "";
            this.NumberColumn.Name = "NumberColumn";
            this.NumberColumn.ReadOnly = true;
            this.NumberColumn.Width = 28;
            // 
            // IconColumn
            // 
            this.IconColumn.DataPropertyName = "Icon";
            this.IconColumn.HeaderText = "";
            this.IconColumn.Name = "IconColumn";
            this.IconColumn.ReadOnly = true;
            this.IconColumn.Width = 28;
            // 
            // FriendlyNameColumn
            // 
            this.FriendlyNameColumn.DataPropertyName = "FriendlyName";
            this.FriendlyNameColumn.HeaderText = "Name";
            this.FriendlyNameColumn.Name = "FriendlyNameColumn";
            // 
            // NameColumn
            // 
            this.NameColumn.DataPropertyName = "Name";
            this.NameColumn.HeaderText = "Type";
            this.NameColumn.Name = "NameColumn";
            this.NameColumn.ReadOnly = true;
            // 
            // StatusColumn
            // 
            this.StatusColumn.DataPropertyName = "Status";
            this.StatusColumn.HeaderText = "Status";
            this.StatusColumn.Name = "StatusColumn";
            this.StatusColumn.ReadOnly = true;
            this.StatusColumn.Width = 120;
            // 
            // InstallColumn
            // 
            this.InstallColumn.HeaderText = "";
            this.InstallColumn.Name = "InstallColumn";
            this.InstallColumn.ReadOnly = true;
            // 
            // ToolStrip
            // 
            this.ToolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.DuplicateToolStripButton,
            this.DeleteToolStripButton,
            this.toolStripSeparator1,
            this.MoveUpToolStripButton,
            this.MoveDownToolStripButton,
            this.toolStripSeparator2,
            this.RefreshToolStripButton,
            this.InstallAllToolStripButton});
            this.ToolStrip.Location = new System.Drawing.Point(0, 0);
            this.ToolStrip.Name = "ToolStrip";
            this.ToolStrip.Size = new System.Drawing.Size(320, 39);
            this.ToolStrip.TabIndex = 3;
            // 
            // DuplicateToolStripButton
            // 
            this.DuplicateToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.DuplicateToolStripButton.Enabled = false;
            this.DuplicateToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("DuplicateToolStripButton.Image")));
            this.DuplicateToolStripButton.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.DuplicateToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.DuplicateToolStripButton.LockingBehavior = Findwise.Sharepoint.SolutionInstaller.Controls.LockingBehavior.AlwaysDisabled;
            this.DuplicateToolStripButton.Name = "DuplicateToolStripButton";
            this.DuplicateToolStripButton.OperationTrait = null;
            this.DuplicateToolStripButton.Size = new System.Drawing.Size(36, 36);
            this.DuplicateToolStripButton.Text = "Duplicate";
            // 
            // DeleteToolStripButton
            // 
            this.DeleteToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.DeleteToolStripButton.Image = global::Findwise.Sharepoint.SolutionInstaller.Properties.Resources.if_Delete_46730;
            this.DeleteToolStripButton.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.DeleteToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.DeleteToolStripButton.LockingBehavior = Findwise.Sharepoint.SolutionInstaller.Controls.LockingBehavior.Normal;
            this.DeleteToolStripButton.Name = "DeleteToolStripButton";
            this.DeleteToolStripButton.OperationTrait = null;
            this.DeleteToolStripButton.Size = new System.Drawing.Size(36, 36);
            this.DeleteToolStripButton.Text = "Delete";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 39);
            // 
            // MoveUpToolStripButton
            // 
            this.MoveUpToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.MoveUpToolStripButton.Image = global::Findwise.Sharepoint.SolutionInstaller.Properties.Resources.if_up_alt_11140;
            this.MoveUpToolStripButton.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.MoveUpToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.MoveUpToolStripButton.LockingBehavior = Findwise.Sharepoint.SolutionInstaller.Controls.LockingBehavior.Normal;
            this.MoveUpToolStripButton.Name = "MoveUpToolStripButton";
            this.MoveUpToolStripButton.OperationTrait = null;
            this.MoveUpToolStripButton.Size = new System.Drawing.Size(36, 36);
            this.MoveUpToolStripButton.Text = "Move up";
            // 
            // MoveDownToolStripButton
            // 
            this.MoveDownToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.MoveDownToolStripButton.Image = global::Findwise.Sharepoint.SolutionInstaller.Properties.Resources.if_down_alt_11066;
            this.MoveDownToolStripButton.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.MoveDownToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.MoveDownToolStripButton.LockingBehavior = Findwise.Sharepoint.SolutionInstaller.Controls.LockingBehavior.Normal;
            this.MoveDownToolStripButton.Name = "MoveDownToolStripButton";
            this.MoveDownToolStripButton.OperationTrait = null;
            this.MoveDownToolStripButton.Size = new System.Drawing.Size(36, 36);
            this.MoveDownToolStripButton.Text = "Move down";
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 39);
            // 
            // RefreshToolStripButton
            // 
            this.RefreshToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.RefreshToolStripButton.Image = global::Findwise.Sharepoint.SolutionInstaller.Properties.Resources.if_refresh_11116;
            this.RefreshToolStripButton.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.RefreshToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.RefreshToolStripButton.LockingBehavior = Findwise.Sharepoint.SolutionInstaller.Controls.LockingBehavior.Normal;
            this.RefreshToolStripButton.Name = "RefreshToolStripButton";
            this.RefreshToolStripButton.OperationTrait = null;
            this.RefreshToolStripButton.Size = new System.Drawing.Size(36, 36);
            this.RefreshToolStripButton.Text = "Refresh";
            // 
            // InstallAllToolStripButton
            // 
            this.InstallAllToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.InstallAllToolStripButton.Image = global::Findwise.Sharepoint.SolutionInstaller.Properties.Resources.if_document_save_as_15271;
            this.InstallAllToolStripButton.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.InstallAllToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.InstallAllToolStripButton.LockingBehavior = Findwise.Sharepoint.SolutionInstaller.Controls.LockingBehavior.Normal;
            this.InstallAllToolStripButton.Name = "InstallAllToolStripButton";
            this.InstallAllToolStripButton.OperationTrait = null;
            this.InstallAllToolStripButton.Size = new System.Drawing.Size(36, 36);
            this.InstallAllToolStripButton.Text = "Install All";
            // 
            // InstallerModuleMainViewDesigner
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.TableLayoutPanel);
            this.Name = "InstallerModuleMainViewDesigner";
            this.Size = new System.Drawing.Size(320, 240);
            this.TableLayoutPanel.ResumeLayout(false);
            this.TableLayoutPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ToolStrip.ResumeLayout(false);
            this.ToolStrip.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.DataGridViewTextBoxColumn NumberColumn;
        private System.Windows.Forms.DataGridViewImageColumn IconColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn FriendlyNameColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn NameColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn StatusColumn;
        private Controls.DataGridViewHideableButtonColumn InstallColumn;
        internal System.Windows.Forms.TableLayoutPanel TableLayoutPanel;
        internal System.Windows.Forms.ToolStrip ToolStrip;
        internal Controls.LockableToolStripButton DuplicateToolStripButton;
        internal Controls.LockableToolStripButton DeleteToolStripButton;
        internal Controls.LockableToolStripButton MoveUpToolStripButton;
        internal Controls.LockableToolStripButton MoveDownToolStripButton;
        internal Controls.LockableToolStripButton RefreshToolStripButton;
        internal Controls.LockableToolStripButton InstallAllToolStripButton;
        internal System.Windows.Forms.DataGridView dataGridView1;
    }
}
