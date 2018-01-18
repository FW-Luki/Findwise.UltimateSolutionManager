namespace Findwise.Sharepoint.SolutionInstaller.Views
{
    partial class InstallerModuleView
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(InstallerModuleView));
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.NumberColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.IconColumn = new System.Windows.Forms.DataGridViewImageColumn();
            this.FriendlyNameColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NameColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.StatusColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.InstallColumn = new Findwise.Sharepoint.SolutionInstaller.Controls.DataGridViewHideableButtonColumn();
            this.SingleClickInstallButtonTimer = new System.Windows.Forms.Timer(this.components);
            this.SingleClickInstallButtonToolTip = new System.Windows.Forms.ToolTip(this.components);
            this.dataGridViewHideableButtonColumn1 = new Findwise.Sharepoint.SolutionInstaller.Controls.DataGridViewHideableButtonColumn();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.DuplicateToolStripButton = new Findwise.Sharepoint.SolutionInstaller.Controls.LockableToolStripButton();
            this.DeleteToolStripButton = new Findwise.Sharepoint.SolutionInstaller.Controls.LockableToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.MoveUpToolStripButton = new Findwise.Sharepoint.SolutionInstaller.Controls.LockableToolStripButton();
            this.MoveDownToolStripButton = new Findwise.Sharepoint.SolutionInstaller.Controls.LockableToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.RefreshToolStripButton = new Findwise.Sharepoint.SolutionInstaller.Controls.LockableToolStripButton();
            this.InstallAllToolStripButton = new Findwise.Sharepoint.SolutionInstaller.Controls.LockableToolStripButton();
            this.CancelToolStripButton = new Findwise.Sharepoint.SolutionInstaller.Controls.LockableToolStripButton();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
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
            this.dataGridView1.Location = new System.Drawing.Point(0, 39);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.Size = new System.Drawing.Size(400, 261);
            this.dataGridView1.TabIndex = 1;
            this.dataGridView1.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellContentClick);
            this.dataGridView1.CellContentDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellContentDoubleClick);
            this.dataGridView1.CellPainting += new System.Windows.Forms.DataGridViewCellPaintingEventHandler(this.dataGridView1_CellPainting);
            this.dataGridView1.SelectionChanged += new System.EventHandler(this.dataGridView1_SelectionChanged);
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
            // SingleClickInstallButtonTimer
            // 
            this.SingleClickInstallButtonTimer.Interval = 5000;
            // 
            // SingleClickInstallButtonToolTip
            // 
            this.SingleClickInstallButtonToolTip.ToolTipIcon = System.Windows.Forms.ToolTipIcon.Info;
            // 
            // dataGridViewHideableButtonColumn1
            // 
            this.dataGridViewHideableButtonColumn1.HeaderText = "";
            this.dataGridViewHideableButtonColumn1.Name = "dataGridViewHideableButtonColumn1";
            this.dataGridViewHideableButtonColumn1.ReadOnly = true;
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripSeparator3,
            this.DuplicateToolStripButton,
            this.DeleteToolStripButton,
            this.toolStripSeparator1,
            this.MoveUpToolStripButton,
            this.MoveDownToolStripButton,
            this.toolStripSeparator2,
            this.RefreshToolStripButton,
            this.InstallAllToolStripButton,
            this.CancelToolStripButton});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(400, 39);
            this.toolStrip1.TabIndex = 2;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 39);
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
            this.DuplicateToolStripButton.Size = new System.Drawing.Size(36, 36);
            this.DuplicateToolStripButton.Text = "Duplicate";
            this.DuplicateToolStripButton.Click += new System.EventHandler(this.DuplicateToolStripButton_Click);
            // 
            // DeleteToolStripButton
            // 
            this.DeleteToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.DeleteToolStripButton.Image = global::Findwise.Sharepoint.SolutionInstaller.Properties.Resources.if_Delete_46730;
            this.DeleteToolStripButton.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.DeleteToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.DeleteToolStripButton.LockingBehavior = Findwise.Sharepoint.SolutionInstaller.Controls.LockingBehavior.Normal;
            this.DeleteToolStripButton.Name = "DeleteToolStripButton";
            this.DeleteToolStripButton.Size = new System.Drawing.Size(36, 36);
            this.DeleteToolStripButton.Text = "Delete";
            this.DeleteToolStripButton.Click += new System.EventHandler(this.DeleteToolStripButton_Click);
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
            this.MoveUpToolStripButton.Size = new System.Drawing.Size(36, 36);
            this.MoveUpToolStripButton.Text = "Move up";
            this.MoveUpToolStripButton.Click += new System.EventHandler(this.MoveUpToolStripButton_Click);
            // 
            // MoveDownToolStripButton
            // 
            this.MoveDownToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.MoveDownToolStripButton.Image = global::Findwise.Sharepoint.SolutionInstaller.Properties.Resources.if_down_alt_11066;
            this.MoveDownToolStripButton.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.MoveDownToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.MoveDownToolStripButton.LockingBehavior = Findwise.Sharepoint.SolutionInstaller.Controls.LockingBehavior.Normal;
            this.MoveDownToolStripButton.Name = "MoveDownToolStripButton";
            this.MoveDownToolStripButton.Size = new System.Drawing.Size(36, 36);
            this.MoveDownToolStripButton.Text = "Move down";
            this.MoveDownToolStripButton.Click += new System.EventHandler(this.MoveDownToolStripButton_Click);
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
            this.RefreshToolStripButton.Size = new System.Drawing.Size(36, 36);
            this.RefreshToolStripButton.Text = "Refresh";
            this.RefreshToolStripButton.Click += new System.EventHandler(this.RefreshToolStripButton_Click);
            // 
            // InstallAllToolStripButton
            // 
            this.InstallAllToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.InstallAllToolStripButton.Image = global::Findwise.Sharepoint.SolutionInstaller.Properties.Resources.if_document_save_as_15271;
            this.InstallAllToolStripButton.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.InstallAllToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.InstallAllToolStripButton.LockingBehavior = Findwise.Sharepoint.SolutionInstaller.Controls.LockingBehavior.Normal;
            this.InstallAllToolStripButton.Name = "InstallAllToolStripButton";
            this.InstallAllToolStripButton.Size = new System.Drawing.Size(36, 36);
            this.InstallAllToolStripButton.Text = "Install All";
            this.InstallAllToolStripButton.Click += new System.EventHandler(this.InstallAllToolStripButton_Click);
            // 
            // CancelToolStripButton
            // 
            this.CancelToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.CancelToolStripButton.Enabled = false;
            this.CancelToolStripButton.Image = global::Findwise.Sharepoint.SolutionInstaller.Properties.Resources.if_process_stop_15322;
            this.CancelToolStripButton.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.CancelToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.CancelToolStripButton.LockingBehavior = Findwise.Sharepoint.SolutionInstaller.Controls.LockingBehavior.Invert;
            this.CancelToolStripButton.Name = "CancelToolStripButton";
            this.CancelToolStripButton.Size = new System.Drawing.Size(36, 36);
            this.CancelToolStripButton.Text = "Cancel";
            this.CancelToolStripButton.Click += new System.EventHandler(this.CancelToolStripButton_Click);
            // 
            // InstallerModuleView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.toolStrip1);
            this.Name = "InstallerModuleView";
            this.Size = new System.Drawing.Size(400, 300);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DataGridViewTextBoxColumn NumberColumn;
        private System.Windows.Forms.DataGridViewImageColumn IconColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn FriendlyNameColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn NameColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn StatusColumn;
        private Controls.DataGridViewHideableButtonColumn InstallColumn;
        private System.Windows.Forms.Timer SingleClickInstallButtonTimer;
        private System.Windows.Forms.ToolTip SingleClickInstallButtonToolTip;
        private Controls.DataGridViewHideableButtonColumn dataGridViewHideableButtonColumn1;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private Controls.LockableToolStripButton DuplicateToolStripButton;
        private Controls.LockableToolStripButton DeleteToolStripButton;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private Controls.LockableToolStripButton MoveUpToolStripButton;
        private Controls.LockableToolStripButton MoveDownToolStripButton;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private Controls.LockableToolStripButton RefreshToolStripButton;
        private Controls.LockableToolStripButton InstallAllToolStripButton;
        private Controls.LockableToolStripButton CancelToolStripButton;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
    }
}
