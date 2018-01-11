namespace Findwise.Sharepoint.SolutionInstaller
{
    partial class Form1
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
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.NumberColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.IconColumn = new System.Windows.Forms.DataGridViewImageColumn();
            this.FriendlyNameColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NameColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.StatusColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.InstallColumn = new Findwise.Sharepoint.SolutionInstaller.Controls.DataGridViewHideableButtonColumn();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.propertyGrid1 = new System.Windows.Forms.PropertyGrid();
            this.toolStrip1 = new Findwise.Sharepoint.SolutionInstaller.Controls.FancyToolStrip();
            this.NewToolStripButton = new Findwise.Sharepoint.SolutionInstaller.Controls.LockableToolStripButton();
            this.OpenToolStripButton = new Findwise.Sharepoint.SolutionInstaller.Controls.LockableToolStripButton();
            this.SaveToolStripButton = new Findwise.Sharepoint.SolutionInstaller.Controls.LockableToolStripButton();
            this.toolStripSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.DuplicateToolStripButton = new Findwise.Sharepoint.SolutionInstaller.Controls.LockableToolStripButton();
            this.DeleteToolStripButton = new Findwise.Sharepoint.SolutionInstaller.Controls.LockableToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.MoveUpToolStripButton = new Findwise.Sharepoint.SolutionInstaller.Controls.LockableToolStripButton();
            this.MoveDownToolStripButton = new Findwise.Sharepoint.SolutionInstaller.Controls.LockableToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.RefreshToolStripButton = new Findwise.Sharepoint.SolutionInstaller.Controls.LockableToolStripButton();
            this.InstallAllToolStripButton = new Findwise.Sharepoint.SolutionInstaller.Controls.LockableToolStripButton();
            this.CancelToolStripButton = new Findwise.Sharepoint.SolutionInstaller.Controls.LockableToolStripButton();
            this.sizeablePanel1 = new Findwise.Sharepoint.SolutionInstaller.Controls.SizeablePanel();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.sizeablePanel2 = new Findwise.Sharepoint.SolutionInstaller.Controls.SizeablePanel();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.SingleClickInstallButtonTimer = new System.Windows.Forms.Timer(this.components);
            this.SingleClickInstallButtonToolTip = new System.Windows.Forms.ToolTip(this.components);
            this.dataGridViewHideableButtonColumn1 = new Findwise.Sharepoint.SolutionInstaller.Controls.DataGridViewHideableButtonColumn();
            this.dataGridViewDisableButtonColumn1 = new Findwise.Sharepoint.SolutionInstaller.Controls.DataGridViewHideableButtonColumn();
            this.LogWindowContextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.ClearLogWindowToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.sizeablePanel2.SuspendLayout();
            this.LogWindowContextMenuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.splitContainer1, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.toolStrip1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.sizeablePanel1, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.statusStrip1, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.sizeablePanel2, 0, 2);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 4;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1008, 747);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.splitContainer1.Location = new System.Drawing.Point(169, 42);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.groupBox1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.groupBox2);
            this.splitContainer1.Size = new System.Drawing.Size(836, 574);
            this.splitContainer1.SplitterDistance = 512;
            this.splitContainer1.TabIndex = 3;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.dataGridView1);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(512, 574);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Installer Modules";
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
            this.dataGridView1.Location = new System.Drawing.Point(3, 16);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.Size = new System.Drawing.Size(506, 555);
            this.dataGridView1.TabIndex = 0;
            this.dataGridView1.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellContentClick);
            this.dataGridView1.CellContentDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellContentDoubleClick);
            this.dataGridView1.CellPainting += new System.Windows.Forms.DataGridViewCellPaintingEventHandler(this.dataGridView1_CellPainting);
            this.dataGridView1.SelectionChanged += new System.EventHandler(this.dataGridView1_SelectionChanged);
            // 
            // NumberColumn
            // 
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle3.Format = "#\\.";
            dataGridViewCellStyle3.NullValue = null;
            this.NumberColumn.DefaultCellStyle = dataGridViewCellStyle3;
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
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.propertyGrid1);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Location = new System.Drawing.Point(0, 0);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(320, 574);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Properties";
            // 
            // propertyGrid1
            // 
            this.propertyGrid1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.propertyGrid1.LineColor = System.Drawing.SystemColors.ControlDark;
            this.propertyGrid1.Location = new System.Drawing.Point(3, 16);
            this.propertyGrid1.Name = "propertyGrid1";
            this.propertyGrid1.PropertySort = System.Windows.Forms.PropertySort.Categorized;
            this.propertyGrid1.Size = new System.Drawing.Size(314, 555);
            this.propertyGrid1.TabIndex = 0;
            this.propertyGrid1.PropertyValueChanged += new System.Windows.Forms.PropertyValueChangedEventHandler(this.propertyGrid1_PropertyValueChanged);
            // 
            // toolStrip1
            // 
            this.toolStrip1.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.toolStrip1.BackgroundGradientColor = System.Drawing.Color.FromArgb(((int)(((byte)(39)))), ((int)(((byte)(39)))), ((int)(((byte)(39)))));
            this.tableLayoutPanel1.SetColumnSpan(this.toolStrip1, 2);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.NewToolStripButton,
            this.OpenToolStripButton,
            this.SaveToolStripButton,
            this.toolStripSeparator,
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
            this.toolStrip1.Padding = new System.Windows.Forms.Padding(0, 0, 4, 0);
            this.toolStrip1.Size = new System.Drawing.Size(1008, 39);
            this.toolStrip1.SpecialBackgroundImage = ((System.Drawing.Image)(resources.GetObject("toolStrip1.SpecialBackgroundImage")));
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // NewToolStripButton
            // 
            this.NewToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.NewToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("NewToolStripButton.Image")));
            this.NewToolStripButton.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.NewToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.NewToolStripButton.LockingBehavior = Findwise.Sharepoint.SolutionInstaller.Controls.LockingBehavior.Normal;
            this.NewToolStripButton.Name = "NewToolStripButton";
            this.NewToolStripButton.Size = new System.Drawing.Size(36, 36);
            this.NewToolStripButton.Text = "&New";
            this.NewToolStripButton.Click += new System.EventHandler(this.NewToolStripButton_Click);
            // 
            // OpenToolStripButton
            // 
            this.OpenToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.OpenToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("OpenToolStripButton.Image")));
            this.OpenToolStripButton.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.OpenToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.OpenToolStripButton.LockingBehavior = Findwise.Sharepoint.SolutionInstaller.Controls.LockingBehavior.Normal;
            this.OpenToolStripButton.Name = "OpenToolStripButton";
            this.OpenToolStripButton.Size = new System.Drawing.Size(36, 36);
            this.OpenToolStripButton.Text = "&Open";
            this.OpenToolStripButton.Click += new System.EventHandler(this.OpenToolStripButton_Click);
            // 
            // SaveToolStripButton
            // 
            this.SaveToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.SaveToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("SaveToolStripButton.Image")));
            this.SaveToolStripButton.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.SaveToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.SaveToolStripButton.LockingBehavior = Findwise.Sharepoint.SolutionInstaller.Controls.LockingBehavior.AlwaysEnabled;
            this.SaveToolStripButton.Name = "SaveToolStripButton";
            this.SaveToolStripButton.Size = new System.Drawing.Size(36, 36);
            this.SaveToolStripButton.Text = "&Save";
            this.SaveToolStripButton.Click += new System.EventHandler(this.SaveToolStripButton_Click);
            // 
            // toolStripSeparator
            // 
            this.toolStripSeparator.Name = "toolStripSeparator";
            this.toolStripSeparator.Size = new System.Drawing.Size(6, 39);
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
            this.MoveUpToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("MoveUpToolStripButton.Image")));
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
            this.MoveDownToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("MoveDownToolStripButton.Image")));
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
            this.RefreshToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("RefreshToolStripButton.Image")));
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
            this.InstallAllToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("InstallAllToolStripButton.Image")));
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
            this.CancelToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("CancelToolStripButton.Image")));
            this.CancelToolStripButton.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.CancelToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.CancelToolStripButton.LockingBehavior = Findwise.Sharepoint.SolutionInstaller.Controls.LockingBehavior.Invert;
            this.CancelToolStripButton.Name = "CancelToolStripButton";
            this.CancelToolStripButton.Size = new System.Drawing.Size(36, 36);
            this.CancelToolStripButton.Text = "Cancel";
            this.CancelToolStripButton.Click += new System.EventHandler(this.CancelToolStripButton_Click);
            // 
            // sizeablePanel1
            // 
            this.sizeablePanel1.Caption = "Toolbox";
            this.sizeablePanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.sizeablePanel1.GripPosition = System.Windows.Forms.DockStyle.Right;
            this.sizeablePanel1.Location = new System.Drawing.Point(3, 42);
            this.sizeablePanel1.Name = "sizeablePanel1";
            this.sizeablePanel1.Size = new System.Drawing.Size(160, 574);
            this.sizeablePanel1.TabIndex = 4;
            // 
            // statusStrip1
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.statusStrip1, 2);
            this.statusStrip1.Location = new System.Drawing.Point(0, 725);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(1008, 22);
            this.statusStrip1.TabIndex = 5;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // sizeablePanel2
            // 
            this.sizeablePanel2.Caption = "Log";
            this.tableLayoutPanel1.SetColumnSpan(this.sizeablePanel2, 2);
            this.sizeablePanel2.Controls.Add(this.richTextBox1);
            this.sizeablePanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.sizeablePanel2.GripPosition = System.Windows.Forms.DockStyle.Top;
            this.sizeablePanel2.Location = new System.Drawing.Point(3, 622);
            this.sizeablePanel2.Name = "sizeablePanel2";
            this.sizeablePanel2.Size = new System.Drawing.Size(1002, 100);
            this.sizeablePanel2.TabIndex = 6;
            // 
            // richTextBox1
            // 
            this.richTextBox1.ContextMenuStrip = this.LogWindowContextMenuStrip;
            this.richTextBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.richTextBox1.Location = new System.Drawing.Point(0, 0);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.ReadOnly = true;
            this.richTextBox1.Size = new System.Drawing.Size(1002, 100);
            this.richTextBox1.TabIndex = 1;
            this.richTextBox1.Text = "";
            // 
            // saveFileDialog1
            // 
            this.saveFileDialog1.Filter = "XML Files|*.xml";
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.Filter = "XML Files|*.xml|All files|*.*";
            // 
            // SingleClickInstallButtonTimer
            // 
            this.SingleClickInstallButtonTimer.Interval = 5000;
            this.SingleClickInstallButtonTimer.Tick += new System.EventHandler(this.SingleClickInstallButtonTimer_Tick);
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
            // dataGridViewDisableButtonColumn1
            // 
            this.dataGridViewDisableButtonColumn1.HeaderText = "";
            this.dataGridViewDisableButtonColumn1.Name = "dataGridViewDisableButtonColumn1";
            this.dataGridViewDisableButtonColumn1.ReadOnly = true;
            // 
            // LogWindowContextMenuStrip
            // 
            this.LogWindowContextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ClearLogWindowToolStripMenuItem});
            this.LogWindowContextMenuStrip.Name = "LogWindowContextMenuStrip";
            this.LogWindowContextMenuStrip.Size = new System.Drawing.Size(102, 26);
            // 
            // ClearLogWindowToolStripMenuItem
            // 
            this.ClearLogWindowToolStripMenuItem.Image = global::Findwise.Sharepoint.SolutionInstaller.Properties.Resources.if_Delete_46730;
            this.ClearLogWindowToolStripMenuItem.Name = "ClearLogWindowToolStripMenuItem";
            this.ClearLogWindowToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.ClearLogWindowToolStripMenuItem.Text = "Clear";
            this.ClearLogWindowToolStripMenuItem.Click += new System.EventHandler(this.ClearLogWindowToolStripMenuItem_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1008, 747);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form1";
            this.Text = "Findwise Sharepoint Solution Installer";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.sizeablePanel2.ResumeLayout(false);
            this.LogWindowContextMenuStrip.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private Controls.FancyToolStrip toolStrip1;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private Controls.SizeablePanel sizeablePanel1;
        private Controls.LockableToolStripButton NewToolStripButton;
        private Controls.LockableToolStripButton OpenToolStripButton;
        private Controls.LockableToolStripButton SaveToolStripButton;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator;
        private Controls.LockableToolStripButton DuplicateToolStripButton;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.PropertyGrid propertyGrid1;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private Controls.LockableToolStripButton DeleteToolStripButton;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private Controls.LockableToolStripButton MoveUpToolStripButton;
        private Controls.LockableToolStripButton MoveDownToolStripButton;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private Controls.LockableToolStripButton RefreshToolStripButton;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private Controls.SizeablePanel sizeablePanel2;
        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.DataGridViewTextBoxColumn NumberColumn;
        private System.Windows.Forms.DataGridViewImageColumn IconColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn FriendlyNameColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn NameColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn StatusColumn;
        private Controls.DataGridViewHideableButtonColumn InstallColumn;
        private Controls.LockableToolStripButton InstallAllToolStripButton;
        private System.Windows.Forms.Timer SingleClickInstallButtonTimer;
        private System.Windows.Forms.ToolTip SingleClickInstallButtonToolTip;
        private Controls.DataGridViewHideableButtonColumn dataGridViewDisableButtonColumn1;
        private Controls.DataGridViewHideableButtonColumn dataGridViewHideableButtonColumn1;
        private Controls.LockableToolStripButton CancelToolStripButton;
        private System.Windows.Forms.ContextMenuStrip LogWindowContextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem ClearLogWindowToolStripMenuItem;
    }
}

