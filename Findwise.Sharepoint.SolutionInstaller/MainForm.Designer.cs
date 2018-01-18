namespace Findwise.Sharepoint.SolutionInstaller
{
    partial class MainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.propertyGrid1 = new Findwise.Sharepoint.SolutionInstaller.Controls.PropertyGridEx();
            this.PropertyGridMergeToolStrip = new System.Windows.Forms.ToolStrip();
            this.BindingWindowToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.SelectedObjectToolStrip = new Findwise.Sharepoint.SolutionInstaller.Controls.FancyToolStrip();
            this.toolStrip1 = new Findwise.Sharepoint.SolutionInstaller.Controls.FancyToolStrip();
            this.NewToolStripButton = new Findwise.Sharepoint.SolutionInstaller.Controls.LockableToolStripButton();
            this.OpenToolStripButton = new Findwise.Sharepoint.SolutionInstaller.Controls.LockableToolStripButton();
            this.SaveToolStripButton = new Findwise.Sharepoint.SolutionInstaller.Controls.LockableToolStripButton();
            this.ToolboxPanel = new Findwise.Sharepoint.SolutionInstaller.Controls.SizeablePanel();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.Indicator = new Findwise.Sharepoint.SolutionInstaller.Controls.FancyIndicator();
            this.toolStripProgressBar1 = new System.Windows.Forms.ToolStripProgressBar();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.sizeablePanel2 = new Findwise.Sharepoint.SolutionInstaller.Controls.SizeablePanel();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.LogWindowContextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.ClearLogWindowToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.dataGridViewHideableButtonColumn1 = new Findwise.Sharepoint.SolutionInstaller.Controls.DataGridViewHideableButtonColumn();
            this.dataGridViewDisableButtonColumn1 = new Findwise.Sharepoint.SolutionInstaller.Controls.DataGridViewHideableButtonColumn();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.PropertyGridMergeToolStrip.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
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
            this.tableLayoutPanel1.Controls.Add(this.ToolboxPanel, 0, 1);
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
            this.splitContainer1.Panel1.Controls.Add(this.tabControl1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.groupBox2);
            this.splitContainer1.Size = new System.Drawing.Size(836, 574);
            this.splitContainer1.SplitterDistance = 512;
            this.splitContainer1.TabIndex = 3;
            // 
            // tabControl1
            // 
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.ImageList = this.imageList1;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(512, 574);
            this.tabControl1.TabIndex = 0;
            this.tabControl1.SelectedIndexChanged += new System.EventHandler(this.tabControl1_SelectedIndexChanged);
            this.tabControl1.Selecting += new System.Windows.Forms.TabControlCancelEventHandler(this.tabControl1_Selecting);
            this.tabControl1.Selected += new System.Windows.Forms.TabControlEventHandler(this.tabControl1_Selected);
            // 
            // imageList1
            // 
            this.imageList1.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit;
            this.imageList1.ImageSize = new System.Drawing.Size(24, 24);
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.propertyGrid1);
            this.groupBox2.Controls.Add(this.PropertyGridMergeToolStrip);
            this.groupBox2.Controls.Add(this.SelectedObjectToolStrip);
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
            this.propertyGrid1.Location = new System.Drawing.Point(3, 66);
            this.propertyGrid1.Name = "propertyGrid1";
            this.propertyGrid1.PropertySort = System.Windows.Forms.PropertySort.Categorized;
            this.propertyGrid1.Size = new System.Drawing.Size(314, 505);
            this.propertyGrid1.TabIndex = 4;
            this.propertyGrid1.PropertyValueChanged += new System.Windows.Forms.PropertyValueChangedEventHandler(this.propertyGrid1_PropertyValueChanged);
            // 
            // PropertyGridMergeToolStrip
            // 
            this.PropertyGridMergeToolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.BindingWindowToolStripButton});
            this.PropertyGridMergeToolStrip.Location = new System.Drawing.Point(3, 41);
            this.PropertyGridMergeToolStrip.Name = "PropertyGridMergeToolStrip";
            this.PropertyGridMergeToolStrip.Padding = new System.Windows.Forms.Padding(2, 0, 1, 0);
            this.PropertyGridMergeToolStrip.Size = new System.Drawing.Size(314, 25);
            this.PropertyGridMergeToolStrip.TabIndex = 3;
            this.PropertyGridMergeToolStrip.Text = "toolStrip3";
            // 
            // BindingWindowToolStripButton
            // 
            this.BindingWindowToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.BindingWindowToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("BindingWindowToolStripButton.Image")));
            this.BindingWindowToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.BindingWindowToolStripButton.Name = "BindingWindowToolStripButton";
            this.BindingWindowToolStripButton.Size = new System.Drawing.Size(23, 22);
            this.BindingWindowToolStripButton.Text = "toolStripButton1";
            this.BindingWindowToolStripButton.Click += new System.EventHandler(this.BindingWindowToolStripButton_Click);
            // 
            // SelectedObjectToolStrip
            // 
            this.SelectedObjectToolStrip.BackgroundGradientColor = System.Drawing.Color.Empty;
            this.SelectedObjectToolStrip.Location = new System.Drawing.Point(3, 16);
            this.SelectedObjectToolStrip.Name = "SelectedObjectToolStrip";
            this.SelectedObjectToolStrip.Padding = new System.Windows.Forms.Padding(2, 0, 1, 0);
            this.SelectedObjectToolStrip.Size = new System.Drawing.Size(314, 25);
            this.SelectedObjectToolStrip.SpecialBackgroundImage = null;
            this.SelectedObjectToolStrip.TabIndex = 1;
            // 
            // toolStrip1
            // 
            this.toolStrip1.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.toolStrip1.BackgroundGradientColor = System.Drawing.Color.FromArgb(((int)(((byte)(39)))), ((int)(((byte)(39)))), ((int)(((byte)(39)))));
            this.tableLayoutPanel1.SetColumnSpan(this.toolStrip1, 2);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.NewToolStripButton,
            this.OpenToolStripButton,
            this.SaveToolStripButton});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Padding = new System.Windows.Forms.Padding(0, 0, 4, 0);
            this.toolStrip1.Size = new System.Drawing.Size(1008, 39);
            this.toolStrip1.SpecialBackgroundImage = ((System.Drawing.Image)(resources.GetObject("toolStrip1.SpecialBackgroundImage")));
            this.toolStrip1.TabIndex = 0;
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
            this.OpenToolStripButton.OperationTrait = null;
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
            this.SaveToolStripButton.OperationTrait = null;
            this.SaveToolStripButton.Size = new System.Drawing.Size(36, 36);
            this.SaveToolStripButton.Text = "&Save";
            this.SaveToolStripButton.Click += new System.EventHandler(this.SaveToolStripButton_Click);
            // 
            // ToolboxPanel
            // 
            this.ToolboxPanel.Caption = "Toolbox";
            this.ToolboxPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ToolboxPanel.GripPosition = System.Windows.Forms.DockStyle.Right;
            this.ToolboxPanel.Location = new System.Drawing.Point(3, 42);
            this.ToolboxPanel.Name = "ToolboxPanel";
            this.ToolboxPanel.Size = new System.Drawing.Size(160, 574);
            this.ToolboxPanel.TabIndex = 4;
            // 
            // statusStrip1
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.statusStrip1, 2);
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.Indicator,
            this.toolStripProgressBar1,
            this.toolStripStatusLabel1});
            this.statusStrip1.Location = new System.Drawing.Point(0, 725);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(1008, 22);
            this.statusStrip1.TabIndex = 5;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // Indicator
            // 
            this.Indicator.Active = false;
            this.Indicator.ActiveColor = System.Drawing.Color.Red;
            this.Indicator.AutoSize = false;
            this.Indicator.BufferSize = 24;
            this.Indicator.Enabled = true;
            this.Indicator.IdleColor = System.Drawing.Color.Green;
            this.Indicator.Name = "Indicator";
            this.Indicator.Size = new System.Drawing.Size(32, 20);
            this.Indicator.Text = "toolStripStatusLabel1";
            // 
            // toolStripProgressBar1
            // 
            this.toolStripProgressBar1.Name = "toolStripProgressBar1";
            this.toolStripProgressBar1.Size = new System.Drawing.Size(128, 16);
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(26, 17);
            this.toolStripStatusLabel1.Text = "Idle";
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
            this.ClearLogWindowToolStripMenuItem.Size = new System.Drawing.Size(101, 22);
            this.ClearLogWindowToolStripMenuItem.Text = "Clear";
            this.ClearLogWindowToolStripMenuItem.Click += new System.EventHandler(this.ClearLogWindowToolStripMenuItem_Click);
            // 
            // saveFileDialog1
            // 
            this.saveFileDialog1.Filter = "XML Files|*.xml";
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.Filter = "XML Files|*.xml|All files|*.*";
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
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1008, 747);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MainForm";
            this.Text = "Findwise Sharepoint Solution Installer";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.Shown += new System.EventHandler(this.Form1_Shown);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.PropertyGridMergeToolStrip.ResumeLayout(false);
            this.PropertyGridMergeToolStrip.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.sizeablePanel2.ResumeLayout(false);
            this.LogWindowContextMenuStrip.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private Controls.FancyToolStrip toolStrip1;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.GroupBox groupBox2;
        private Controls.LockableToolStripButton NewToolStripButton;
        private Controls.LockableToolStripButton OpenToolStripButton;
        private Controls.LockableToolStripButton SaveToolStripButton;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private Controls.SizeablePanel sizeablePanel2;
        private System.Windows.Forms.RichTextBox richTextBox1;
        private Controls.DataGridViewHideableButtonColumn dataGridViewDisableButtonColumn1;
        private Controls.DataGridViewHideableButtonColumn dataGridViewHideableButtonColumn1;
        private System.Windows.Forms.ContextMenuStrip LogWindowContextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem ClearLogWindowToolStripMenuItem;
        private Controls.FancyIndicator Indicator;
        private System.Windows.Forms.ToolStripProgressBar toolStripProgressBar1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private Controls.FancyToolStrip SelectedObjectToolStrip;
        private Controls.PropertyGridEx propertyGrid1;
        private System.Windows.Forms.ToolStrip PropertyGridMergeToolStrip;
        private System.Windows.Forms.ToolStripButton BindingWindowToolStripButton;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.ImageList imageList1;
        private Controls.SizeablePanel ToolboxPanel;
    }
}

