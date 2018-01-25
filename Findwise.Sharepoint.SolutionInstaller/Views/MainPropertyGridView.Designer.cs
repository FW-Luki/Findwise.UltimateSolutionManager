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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainPropertyGridViewDesigner));
            this.sizeablePanel1 = new Findwise.Sharepoint.SolutionInstaller.Controls.SizeablePanel();
            this.propertyGrid1 = new Findwise.Sharepoint.SolutionInstaller.Controls.PropertyGridEx();
            this.PropertyGridMergeToolStrip = new System.Windows.Forms.ToolStrip();
            this.BindingWindowToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.SelectedObjectToolStrip = new Findwise.Sharepoint.SolutionInstaller.Controls.FancyToolStrip();
            this.sizeablePanel1.SuspendLayout();
            this.PropertyGridMergeToolStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // sizeablePanel1
            // 
            this.sizeablePanel1.Caption = "Properties";
            this.sizeablePanel1.Controls.Add(this.propertyGrid1);
            this.sizeablePanel1.Controls.Add(this.PropertyGridMergeToolStrip);
            this.sizeablePanel1.Controls.Add(this.SelectedObjectToolStrip);
            this.sizeablePanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.sizeablePanel1.GripPosition = System.Windows.Forms.DockStyle.Left;
            this.sizeablePanel1.Location = new System.Drawing.Point(0, 0);
            this.sizeablePanel1.Name = "sizeablePanel1";
            this.sizeablePanel1.Size = new System.Drawing.Size(320, 150);
            this.sizeablePanel1.TabIndex = 0;
            // 
            // propertyGrid1
            // 
            this.propertyGrid1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.propertyGrid1.LineColor = System.Drawing.SystemColors.ControlDark;
            this.propertyGrid1.Location = new System.Drawing.Point(0, 50);
            this.propertyGrid1.Name = "propertyGrid1";
            this.propertyGrid1.PropertySort = System.Windows.Forms.PropertySort.Categorized;
            this.propertyGrid1.Size = new System.Drawing.Size(320, 100);
            this.propertyGrid1.TabIndex = 7;
            // 
            // PropertyGridMergeToolStrip
            // 
            this.PropertyGridMergeToolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.BindingWindowToolStripButton});
            this.PropertyGridMergeToolStrip.Location = new System.Drawing.Point(0, 25);
            this.PropertyGridMergeToolStrip.Name = "PropertyGridMergeToolStrip";
            this.PropertyGridMergeToolStrip.Padding = new System.Windows.Forms.Padding(2, 0, 1, 0);
            this.PropertyGridMergeToolStrip.Size = new System.Drawing.Size(320, 25);
            this.PropertyGridMergeToolStrip.TabIndex = 6;
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
            // 
            // SelectedObjectToolStrip
            // 
            this.SelectedObjectToolStrip.BackgroundGradientColor = System.Drawing.Color.Empty;
            this.SelectedObjectToolStrip.Location = new System.Drawing.Point(0, 0);
            this.SelectedObjectToolStrip.Name = "SelectedObjectToolStrip";
            this.SelectedObjectToolStrip.Padding = new System.Windows.Forms.Padding(2, 0, 1, 0);
            this.SelectedObjectToolStrip.Size = new System.Drawing.Size(320, 25);
            this.SelectedObjectToolStrip.SpecialBackgroundImage = null;
            this.SelectedObjectToolStrip.TabIndex = 5;
            // 
            // MainPropertyGridViewDesigner
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.sizeablePanel1);
            this.Name = "MainPropertyGridViewDesigner";
            this.Size = new System.Drawing.Size(320, 150);
            this.sizeablePanel1.ResumeLayout(false);
            this.sizeablePanel1.PerformLayout();
            this.PropertyGridMergeToolStrip.ResumeLayout(false);
            this.PropertyGridMergeToolStrip.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private Controls.SizeablePanel sizeablePanel1;
        private Controls.PropertyGridEx propertyGrid1;
        private System.Windows.Forms.ToolStrip PropertyGridMergeToolStrip;
        private System.Windows.Forms.ToolStripButton BindingWindowToolStripButton;
        private Controls.FancyToolStrip SelectedObjectToolStrip;
    }
}
