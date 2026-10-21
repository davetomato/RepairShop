namespace FixFlow.Forms
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
            panel1 = new Panel();
            lblUserSession = new Label();
            pnlSidebar = new Panel();
            pnlContainer = new Panel();
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.Controls.Add(lblUserSession);
            panel1.Dock = DockStyle.Top;
            panel1.Location = new Point(0, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(983, 50);
            panel1.TabIndex = 0;
            // 
            // lblUserSession
            // 
            lblUserSession.AutoSize = true;
            lblUserSession.Location = new Point(30, 18);
            lblUserSession.Name = "lblUserSession";
            lblUserSession.Size = new Size(50, 20);
            lblUserSession.TabIndex = 0;
            lblUserSession.Text = "label1";
            // 
            // pnlSidebar
            // 
            pnlSidebar.Dock = DockStyle.Left;
            pnlSidebar.Location = new Point(0, 50);
            pnlSidebar.Name = "pnlSidebar";
            pnlSidebar.Size = new Size(200, 554);
            pnlSidebar.TabIndex = 1;
            // 
            // pnlContainer
            // 
            pnlContainer.Dock = DockStyle.Fill;
            pnlContainer.Location = new Point(200, 50);
            pnlContainer.Name = "pnlContainer";
            pnlContainer.Size = new Size(783, 554);
            pnlContainer.TabIndex = 2;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(983, 604);
            Controls.Add(pnlContainer);
            Controls.Add(pnlSidebar);
            Controls.Add(panel1);
            Name = "MainForm";
            Text = "MainForm";
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Panel panel1;
        private Label lblUserSession;
        private Panel pnlSidebar;
        private Panel pnlContainer;
    }
}