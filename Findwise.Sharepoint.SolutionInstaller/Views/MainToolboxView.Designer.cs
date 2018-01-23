﻿namespace Findwise.Sharepoint.SolutionInstaller.Views
{
    partial class MainToolboxViewDesigner
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
            this.ToolboxPanel = new Findwise.Sharepoint.SolutionInstaller.Controls.SizeablePanel();
            this.SuspendLayout();
            // 
            // ToolboxPanel
            // 
            this.ToolboxPanel.Caption = "Toolbox";
            this.ToolboxPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ToolboxPanel.GripPosition = System.Windows.Forms.DockStyle.Right;
            this.ToolboxPanel.Location = new System.Drawing.Point(0, 0);
            this.ToolboxPanel.Name = "ToolboxPanel";
            this.ToolboxPanel.Size = new System.Drawing.Size(150, 150);
            this.ToolboxPanel.TabIndex = 5;
            // 
            // MainToolboxViewDesigner
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.ToolboxPanel);
            this.Name = "MainToolboxViewDesigner";
            this.ResumeLayout(false);

        }

        #endregion

        private Controls.SizeablePanel ToolboxPanel;
    }
}
