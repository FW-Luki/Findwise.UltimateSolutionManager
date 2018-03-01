﻿namespace Findwise.SolutionManager.Views
{
    partial class MainToolStripViewDesigner
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainToolStripViewDesigner));
            this.PrimaryToolStrip = new Findwise.SolutionManager.Controls.FancyToolStrip();
            this.NewToolStripButton = new Findwise.SolutionManager.Controls.LockableToolStripButton();
            this.OpenToolStripButton = new Findwise.SolutionManager.Controls.LockableToolStripButton();
            this.SaveToolStripButton = new Findwise.SolutionManager.Controls.LockableToolStripButton();
            this.toolStripSeparator1 = new Findwise.SolutionManager.Controls.FancyToolStripSeparator();
            this.SecondaryToolStrip = new System.Windows.Forms.ToolStrip();
            this.CancelToolStripButton = new Findwise.SolutionManager.Controls.LockableToolStripButton();
            this.fancyToolStripSeparator1 = new Findwise.SolutionManager.Controls.FancyToolStripSeparator();
            this.FeedbackToolStripButton = new Findwise.SolutionManager.Controls.LockableToolStripButton();
            this.PrimaryToolStrip.SuspendLayout();
            this.SecondaryToolStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // PrimaryToolStrip
            // 
            this.PrimaryToolStrip.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.PrimaryToolStrip.BackgroundGradientColor = System.Drawing.Color.FromArgb(((int)(((byte)(39)))), ((int)(((byte)(39)))), ((int)(((byte)(39)))));
            this.PrimaryToolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.NewToolStripButton,
            this.OpenToolStripButton,
            this.SaveToolStripButton,
            this.toolStripSeparator1});
            this.PrimaryToolStrip.Location = new System.Drawing.Point(0, 0);
            this.PrimaryToolStrip.Name = "PrimaryToolStrip";
            this.PrimaryToolStrip.Padding = new System.Windows.Forms.Padding(0, 0, 4, 0);
            this.PrimaryToolStrip.Size = new System.Drawing.Size(512, 39);
            this.PrimaryToolStrip.SpecialBackgroundImage = ((System.Drawing.Image)(resources.GetObject("PrimaryToolStrip.SpecialBackgroundImage")));
            this.PrimaryToolStrip.TabIndex = 1;
            // 
            // NewToolStripButton
            // 
            this.NewToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.NewToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("NewToolStripButton.Image")));
            this.NewToolStripButton.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.NewToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.NewToolStripButton.LockingBehavior = Findwise.SolutionManager.Controls.LockingBehavior.Normal;
            this.NewToolStripButton.Name = "NewToolStripButton";
            this.NewToolStripButton.OperationTrait = null;
            this.NewToolStripButton.Size = new System.Drawing.Size(36, 36);
            this.NewToolStripButton.Text = "&New";
            // 
            // OpenToolStripButton
            // 
            this.OpenToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.OpenToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("OpenToolStripButton.Image")));
            this.OpenToolStripButton.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.OpenToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.OpenToolStripButton.LockingBehavior = Findwise.SolutionManager.Controls.LockingBehavior.Normal;
            this.OpenToolStripButton.Name = "OpenToolStripButton";
            this.OpenToolStripButton.OperationTrait = null;
            this.OpenToolStripButton.Size = new System.Drawing.Size(36, 36);
            this.OpenToolStripButton.Text = "&Open";
            // 
            // SaveToolStripButton
            // 
            this.SaveToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.SaveToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("SaveToolStripButton.Image")));
            this.SaveToolStripButton.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.SaveToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.SaveToolStripButton.LockingBehavior = Findwise.SolutionManager.Controls.LockingBehavior.AlwaysEnabled;
            this.SaveToolStripButton.Name = "SaveToolStripButton";
            this.SaveToolStripButton.OperationTrait = null;
            this.SaveToolStripButton.Size = new System.Drawing.Size(36, 36);
            this.SaveToolStripButton.Text = "&Save";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 39);
            // 
            // SecondaryToolStrip
            // 
            this.SecondaryToolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.CancelToolStripButton,
            this.fancyToolStripSeparator1,
            this.FeedbackToolStripButton});
            this.SecondaryToolStrip.Location = new System.Drawing.Point(0, 39);
            this.SecondaryToolStrip.Name = "SecondaryToolStrip";
            this.SecondaryToolStrip.Size = new System.Drawing.Size(512, 39);
            this.SecondaryToolStrip.TabIndex = 2;
            this.SecondaryToolStrip.Text = "toolStrip2";
            // 
            // CancelToolStripButton
            // 
            this.CancelToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.CancelToolStripButton.Enabled = false;
            this.CancelToolStripButton.Image = global::Findwise.SolutionManager.Properties.Resources.if_process_stop_15322;
            this.CancelToolStripButton.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.CancelToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.CancelToolStripButton.LockingBehavior = Findwise.SolutionManager.Controls.LockingBehavior.Invert;
            this.CancelToolStripButton.Name = "CancelToolStripButton";
            this.CancelToolStripButton.OperationTrait = null;
            this.CancelToolStripButton.Size = new System.Drawing.Size(36, 36);
            this.CancelToolStripButton.Text = "Cancel";
            this.CancelToolStripButton.Click += new System.EventHandler(this.CancelToolStripButton_Click);
            // 
            // fancyToolStripSeparator1
            // 
            this.fancyToolStripSeparator1.Name = "fancyToolStripSeparator1";
            this.fancyToolStripSeparator1.Size = new System.Drawing.Size(6, 39);
            // 
            // FeedbackToolStripButton
            // 
            this.FeedbackToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.FeedbackToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("FeedbackToolStripButton.Image")));
            this.FeedbackToolStripButton.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.FeedbackToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.FeedbackToolStripButton.LockingBehavior = Findwise.SolutionManager.Controls.LockingBehavior.AlwaysEnabled;
            this.FeedbackToolStripButton.Name = "FeedbackToolStripButton";
            this.FeedbackToolStripButton.OperationTrait = null;
            this.FeedbackToolStripButton.Size = new System.Drawing.Size(36, 36);
            this.FeedbackToolStripButton.Text = "Send Feedback";
            this.FeedbackToolStripButton.Click += new System.EventHandler(this.FeedbackToolStripButton_Click);
            // 
            // MainToolStripViewDesigner
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.SecondaryToolStrip);
            this.Controls.Add(this.PrimaryToolStrip);
            this.Name = "MainToolStripViewDesigner";
            this.Size = new System.Drawing.Size(512, 80);
            this.PrimaryToolStrip.ResumeLayout(false);
            this.PrimaryToolStrip.PerformLayout();
            this.SecondaryToolStrip.ResumeLayout(false);
            this.SecondaryToolStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private Controls.FancyToolStripSeparator toolStripSeparator1;
        internal Controls.LockableToolStripButton CancelToolStripButton;
        internal Controls.FancyToolStrip PrimaryToolStrip;
        internal System.Windows.Forms.ToolStrip SecondaryToolStrip;
        internal Controls.LockableToolStripButton NewToolStripButton;
        internal Controls.LockableToolStripButton OpenToolStripButton;
        internal Controls.LockableToolStripButton SaveToolStripButton;
        private Controls.FancyToolStripSeparator fancyToolStripSeparator1;
        internal Controls.LockableToolStripButton FeedbackToolStripButton;
    }
}
