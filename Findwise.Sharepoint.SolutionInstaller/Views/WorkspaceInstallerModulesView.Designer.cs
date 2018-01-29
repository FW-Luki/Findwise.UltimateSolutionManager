namespace Findwise.Sharepoint.SolutionInstaller.Views
{
    partial class WorkspaceInstallerModulesViewDesigner
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(WorkspaceInstallerModulesViewDesigner));
            this.TableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.DataGridView1 = new System.Windows.Forms.DataGridView();
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
            this.toolStripComboBox1 = new System.Windows.Forms.ToolStripComboBox();
            this.dataGridViewHideableButtonColumn1 = new Findwise.Sharepoint.SolutionInstaller.Controls.DataGridViewHideableButtonColumn();
            this.SingleClickInstallButtonTimer = new System.Windows.Forms.Timer(this.components);
            this.SingleClickInstallButtonToolTip = new System.Windows.Forms.ToolTip(this.components);
            this.TableLayoutPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DataGridView1)).BeginInit();
            this.ToolStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // TableLayoutPanel
            // 
            this.TableLayoutPanel.ColumnCount = 1;
            this.TableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.TableLayoutPanel.Controls.Add(this.DataGridView1, 0, 1);
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
            // DataGridView1
            // 
            this.DataGridView1.AllowUserToAddRows = false;
            this.DataGridView1.AllowUserToDeleteRows = false;
            this.DataGridView1.AllowUserToResizeRows = false;
            this.DataGridView1.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.DataGridView1.BackgroundColor = System.Drawing.SystemColors.Window;
            this.DataGridView1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.DataGridView1.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            this.DataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.NumberColumn,
            this.IconColumn,
            this.FriendlyNameColumn,
            this.NameColumn,
            this.StatusColumn,
            this.InstallColumn});
            this.DataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.DataGridView1.Location = new System.Drawing.Point(3, 42);
            this.DataGridView1.Name = "DataGridView1";
            this.DataGridView1.RowHeadersVisible = false;
            this.DataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.DataGridView1.Size = new System.Drawing.Size(314, 195);
            this.DataGridView1.TabIndex = 4;
            // 
            // NumberColumn
            // 
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle4.Format = "#\\.";
            dataGridViewCellStyle4.NullValue = null;
            this.NumberColumn.DefaultCellStyle = dataGridViewCellStyle4;
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
            this.InstallAllToolStripButton,
            this.toolStripComboBox1});
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
            // toolStripComboBox1
            // 
            this.toolStripComboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.toolStripComboBox1.DropDownWidth = 160;
            this.toolStripComboBox1.FlatStyle = System.Windows.Forms.FlatStyle.Standard;
            this.toolStripComboBox1.IntegralHeight = false;
            this.toolStripComboBox1.Name = "toolStripComboBox1";
            this.toolStripComboBox1.Size = new System.Drawing.Size(160, 48);
            // 
            // dataGridViewHideableButtonColumn1
            // 
            this.dataGridViewHideableButtonColumn1.HeaderText = "";
            this.dataGridViewHideableButtonColumn1.Name = "dataGridViewHideableButtonColumn1";
            this.dataGridViewHideableButtonColumn1.ReadOnly = true;
            // 
            // SingleClickInstallButtonTimer
            // 
            this.SingleClickInstallButtonTimer.Interval = 5000;
            // 
            // SingleClickInstallButtonToolTip
            // 
            this.SingleClickInstallButtonToolTip.ToolTipIcon = System.Windows.Forms.ToolTipIcon.Info;
            // 
            // WorkspaceInstallerModulesViewDesigner
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.TableLayoutPanel);
            this.Name = "WorkspaceInstallerModulesViewDesigner";
            this.Size = new System.Drawing.Size(320, 240);
            this.TableLayoutPanel.ResumeLayout(false);
            this.TableLayoutPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DataGridView1)).EndInit();
            this.ToolStrip.ResumeLayout(false);
            this.ToolStrip.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        internal System.Windows.Forms.DataGridViewTextBoxColumn NumberColumn;
        private System.Windows.Forms.DataGridViewImageColumn IconColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn FriendlyNameColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn NameColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn StatusColumn;
        internal Controls.DataGridViewHideableButtonColumn InstallColumn;
        internal System.Windows.Forms.TableLayoutPanel TableLayoutPanel;
        internal System.Windows.Forms.ToolStrip ToolStrip;
        internal Controls.LockableToolStripButton DuplicateToolStripButton;
        internal Controls.LockableToolStripButton DeleteToolStripButton;
        internal Controls.LockableToolStripButton MoveUpToolStripButton;
        internal Controls.LockableToolStripButton MoveDownToolStripButton;
        internal Controls.LockableToolStripButton RefreshToolStripButton;
        internal Controls.LockableToolStripButton InstallAllToolStripButton;
        internal System.Windows.Forms.DataGridView DataGridView1;
        private Controls.DataGridViewHideableButtonColumn dataGridViewHideableButtonColumn1;
        internal System.Windows.Forms.Timer SingleClickInstallButtonTimer;
        internal System.Windows.Forms.ToolTip SingleClickInstallButtonToolTip;
        private System.Windows.Forms.ToolStripComboBox toolStripComboBox1;
    }
}
