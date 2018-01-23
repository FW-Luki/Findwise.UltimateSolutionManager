namespace Findwise.Sharepoint.SolutionInstaller.Views
{
    partial class MainLogViewDesigner
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
            this.LogPanel = new Findwise.Sharepoint.SolutionInstaller.Controls.SizeablePanel();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.LogPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // LogPanel
            // 
            this.LogPanel.Caption = "Log";
            this.LogPanel.Controls.Add(this.richTextBox1);
            this.LogPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.LogPanel.GripPosition = System.Windows.Forms.DockStyle.Top;
            this.LogPanel.Location = new System.Drawing.Point(0, 0);
            this.LogPanel.Name = "LogPanel";
            this.LogPanel.Size = new System.Drawing.Size(150, 150);
            this.LogPanel.TabIndex = 7;
            // 
            // richTextBox1
            // 
            this.richTextBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.richTextBox1.Location = new System.Drawing.Point(0, 0);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.ReadOnly = true;
            this.richTextBox1.Size = new System.Drawing.Size(150, 150);
            this.richTextBox1.TabIndex = 1;
            this.richTextBox1.Text = "";
            // 
            // MainLogViewDesigner
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.LogPanel);
            this.Name = "MainLogViewDesigner";
            this.LogPanel.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private Controls.SizeablePanel LogPanel;
        private System.Windows.Forms.RichTextBox richTextBox1;
    }
}
