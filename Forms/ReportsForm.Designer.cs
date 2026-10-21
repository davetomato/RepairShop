namespace FixFlow.Forms
{
    partial class ReportsForm
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
            label1 = new Label();
            cmbReportType = new ComboBox();
            btnRunReport = new Button();
            dgvReport = new DataGridView();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvReport).BeginInit();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.Controls.Add(btnRunReport);
            panel1.Controls.Add(cmbReportType);
            panel1.Controls.Add(label1);
            panel1.Dock = DockStyle.Top;
            panel1.Location = new Point(0, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(800, 70);
            panel1.TabIndex = 0;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(49, 22);
            label1.Name = "label1";
            label1.Size = new Size(101, 20);
            label1.TabIndex = 0;
            label1.Text = "Select Report:";
            // 
            // cmbReportType
            // 
            cmbReportType.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbReportType.FormattingEnabled = true;
            cmbReportType.Location = new Point(198, 19);
            cmbReportType.Name = "cmbReportType";
            cmbReportType.Size = new Size(151, 28);
            cmbReportType.TabIndex = 1;
            // 
            // btnRunReport
            // 
            btnRunReport.Location = new Point(401, 18);
            btnRunReport.Name = "btnRunReport";
            btnRunReport.Size = new Size(94, 29);
            btnRunReport.TabIndex = 2;
            btnRunReport.Text = "Run Report";
            btnRunReport.UseVisualStyleBackColor = true;
            btnRunReport.Click += this.btnRunReport_Click;
            // 
            // dgvReport
            // 
            dgvReport.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvReport.Dock = DockStyle.Fill;
            dgvReport.Location = new Point(0, 70);
            dgvReport.Name = "dgvReport";
            dgvReport.RowHeadersWidth = 51;
            dgvReport.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvReport.Size = new Size(800, 380);
            dgvReport.TabIndex = 1;
            // 
            // ReportsForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(dgvReport);
            Controls.Add(panel1);
            Name = "ReportsForm";
            Text = "Reports Form";
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvReport).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Panel panel1;
        private Label label1;
        private Button btnRunReport;
        private ComboBox cmbReportType;
        private DataGridView dgvReport;
    }
}