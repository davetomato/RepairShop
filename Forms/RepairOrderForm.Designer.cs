namespace FixFlow.Forms
{
    partial class RepairOrderForm
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
            grpDetails = new GroupBox();
            txtProblem = new TextBox();
            label3 = new Label();
            cmbServiceType = new ComboBox();
            label2 = new Label();
            txtDeviceId = new TextBox();
            label1 = new Label();
            grpExecution = new GroupBox();
            lblBalance = new Label();
            label9 = new Label();
            lblTotalCost = new Label();
            label8 = new Label();
            lblStatus = new Label();
            label7 = new Label();
            btnUpdateProgress = new Button();
            numHoursWorked = new NumericUpDown();
            label6 = new Label();
            txtDiagnosis = new TextBox();
            label5 = new Label();
            cmbStatus = new ComboBox();
            label4 = new Label();
            grpParts = new GroupBox();
            btnRemovePart = new Button();
            btnAdd = new Button();
            numPartQty = new NumericUpDown();
            cmbAvailableParts = new ComboBox();
            dgvOrderParts = new DataGridView();
            sqlCommand1 = new Microsoft.Data.SqlClient.SqlCommand();
            grpDetails.SuspendLayout();
            grpExecution.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)numHoursWorked).BeginInit();
            grpParts.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)numPartQty).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dgvOrderParts).BeginInit();
            SuspendLayout();
            // 
            // grpDetails
            // 
            grpDetails.Controls.Add(txtProblem);
            grpDetails.Controls.Add(label3);
            grpDetails.Controls.Add(cmbServiceType);
            grpDetails.Controls.Add(label2);
            grpDetails.Controls.Add(txtDeviceId);
            grpDetails.Controls.Add(label1);
            grpDetails.Location = new Point(84, 27);
            grpDetails.Name = "grpDetails";
            grpDetails.Size = new Size(400, 200);
            grpDetails.TabIndex = 0;
            grpDetails.TabStop = false;
            grpDetails.Text = "Repair Order Details";
            // 
            // txtProblem
            // 
            txtProblem.Location = new Point(176, 130);
            txtProblem.Multiline = true;
            txtProblem.Name = "txtProblem";
            txtProblem.Size = new Size(125, 34);
            txtProblem.TabIndex = 5;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(48, 133);
            label3.Name = "label3";
            label3.Size = new Size(65, 20);
            label3.TabIndex = 4;
            label3.Text = "Problem";
            // 
            // cmbServiceType
            // 
            cmbServiceType.FormattingEnabled = true;
            cmbServiceType.Location = new Point(176, 90);
            cmbServiceType.Name = "cmbServiceType";
            cmbServiceType.Size = new Size(151, 28);
            cmbServiceType.TabIndex = 3;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(48, 93);
            label2.Name = "label2";
            label2.Size = new Size(91, 20);
            label2.TabIndex = 2;
            label2.Text = "Service Type";
            // 
            // txtDeviceId
            // 
            txtDeviceId.Location = new Point(176, 49);
            txtDeviceId.Name = "txtDeviceId";
            txtDeviceId.Size = new Size(125, 27);
            txtDeviceId.TabIndex = 1;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(48, 52);
            label1.Name = "label1";
            label1.Size = new Size(73, 20);
            label1.TabIndex = 0;
            label1.Text = "Device ID";
            // 
            // grpExecution
            // 
            grpExecution.Controls.Add(lblBalance);
            grpExecution.Controls.Add(label9);
            grpExecution.Controls.Add(lblTotalCost);
            grpExecution.Controls.Add(label8);
            grpExecution.Controls.Add(lblStatus);
            grpExecution.Controls.Add(label7);
            grpExecution.Controls.Add(btnUpdateProgress);
            grpExecution.Controls.Add(numHoursWorked);
            grpExecution.Controls.Add(label6);
            grpExecution.Controls.Add(txtDiagnosis);
            grpExecution.Controls.Add(label5);
            grpExecution.Controls.Add(cmbStatus);
            grpExecution.Controls.Add(label4);
            grpExecution.Location = new Point(87, 258);
            grpExecution.Name = "grpExecution";
            grpExecution.Size = new Size(400, 342);
            grpExecution.TabIndex = 1;
            grpExecution.TabStop = false;
            grpExecution.Text = "Technician Execution and Status";
            // 
            // lblBalance
            // 
            lblBalance.AutoSize = true;
            lblBalance.Location = new Point(205, 289);
            lblBalance.Name = "lblBalance";
            lblBalance.Size = new Size(61, 20);
            lblBalance.TabIndex = 12;
            lblBalance.Text = "balance";
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Location = new Point(111, 289);
            label9.Name = "label9";
            label9.Size = new Size(61, 20);
            label9.TabIndex = 11;
            label9.Text = "Balance";
            // 
            // lblTotalCost
            // 
            lblTotalCost.AutoSize = true;
            lblTotalCost.Location = new Point(205, 259);
            lblTotalCost.Name = "lblTotalCost";
            lblTotalCost.Size = new Size(75, 20);
            lblTotalCost.TabIndex = 10;
            lblTotalCost.Text = "Total Cost";
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Location = new Point(111, 259);
            label8.Name = "label8";
            label8.Size = new Size(90, 20);
            label8.TabIndex = 9;
            label8.Text = "Total Cost: P";
            // 
            // lblStatus
            // 
            lblStatus.AutoSize = true;
            lblStatus.Location = new Point(174, 36);
            lblStatus.Name = "lblStatus";
            lblStatus.Size = new Size(47, 20);
            lblStatus.TabIndex = 8;
            lblStatus.Text = "status";
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(97, 36);
            label7.Name = "label7";
            label7.Size = new Size(55, 20);
            label7.TabIndex = 7;
            label7.Text = "current";
            // 
            // btnUpdateProgress
            // 
            btnUpdateProgress.Location = new Point(76, 210);
            btnUpdateProgress.Name = "btnUpdateProgress";
            btnUpdateProgress.Size = new Size(247, 29);
            btnUpdateProgress.TabIndex = 6;
            btnUpdateProgress.Text = "Save Progress and Move Status";
            btnUpdateProgress.UseVisualStyleBackColor = true;
            btnUpdateProgress.Click += btnUpdateProgress_Click;
            // 
            // numHoursWorked
            // 
            numHoursWorked.DecimalPlaces = 2;
            numHoursWorked.Location = new Point(174, 163);
            numHoursWorked.Name = "numHoursWorked";
            numHoursWorked.Size = new Size(150, 27);
            numHoursWorked.TabIndex = 5;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(58, 165);
            label6.Name = "label6";
            label6.Size = new Size(103, 20);
            label6.TabIndex = 4;
            label6.Text = "Hours Worked";
            // 
            // txtDiagnosis
            // 
            txtDiagnosis.Location = new Point(173, 115);
            txtDiagnosis.Multiline = true;
            txtDiagnosis.Name = "txtDiagnosis";
            txtDiagnosis.Size = new Size(125, 34);
            txtDiagnosis.TabIndex = 3;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(87, 118);
            label5.Name = "label5";
            label5.Size = new Size(74, 20);
            label5.TabIndex = 2;
            label5.Text = "Diagnosis";
            // 
            // cmbStatus
            // 
            cmbStatus.FormattingEnabled = true;
            cmbStatus.Location = new Point(156, 71);
            cmbStatus.Name = "cmbStatus";
            cmbStatus.Size = new Size(151, 28);
            cmbStatus.TabIndex = 1;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(96, 74);
            label4.Name = "label4";
            label4.Size = new Size(49, 20);
            label4.TabIndex = 0;
            label4.Text = "Status";
            // 
            // grpParts
            // 
            grpParts.Controls.Add(btnRemovePart);
            grpParts.Controls.Add(btnAdd);
            grpParts.Controls.Add(numPartQty);
            grpParts.Controls.Add(cmbAvailableParts);
            grpParts.Controls.Add(dgvOrderParts);
            grpParts.Location = new Point(84, 629);
            grpParts.Name = "grpParts";
            grpParts.Size = new Size(448, 275);
            grpParts.TabIndex = 2;
            grpParts.TabStop = false;
            grpParts.Text = "Parts Allocated to Repair";
            // 
            // btnRemovePart
            // 
            btnRemovePart.Location = new Point(231, 178);
            btnRemovePart.Name = "btnRemovePart";
            btnRemovePart.Size = new Size(104, 34);
            btnRemovePart.TabIndex = 4;
            btnRemovePart.Text = "Return Part";
            btnRemovePart.UseVisualStyleBackColor = true;
            // 
            // btnAdd
            // 
            btnAdd.Location = new Point(231, 130);
            btnAdd.Name = "btnAdd";
            btnAdd.Size = new Size(94, 29);
            btnAdd.TabIndex = 3;
            btnAdd.Text = "Add Part";
            btnAdd.UseVisualStyleBackColor = true;
            // 
            // numPartQty
            // 
            numPartQty.Location = new Point(209, 71);
            numPartQty.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            numPartQty.Name = "numPartQty";
            numPartQty.Size = new Size(150, 27);
            numPartQty.TabIndex = 2;
            numPartQty.Value = new decimal(new int[] { 1, 0, 0, 0 });
            // 
            // cmbAvailableParts
            // 
            cmbAvailableParts.FormattingEnabled = true;
            cmbAvailableParts.Location = new Point(208, 26);
            cmbAvailableParts.Name = "cmbAvailableParts";
            cmbAvailableParts.Size = new Size(151, 28);
            cmbAvailableParts.TabIndex = 1;
            // 
            // dgvOrderParts
            // 
            dgvOrderParts.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvOrderParts.Dock = DockStyle.Left;
            dgvOrderParts.Location = new Point(3, 23);
            dgvOrderParts.Name = "dgvOrderParts";
            dgvOrderParts.RowHeadersWidth = 51;
            dgvOrderParts.Size = new Size(147, 249);
            dgvOrderParts.TabIndex = 0;
            // 
            // sqlCommand1
            // 
            sqlCommand1.CommandTimeout = 30;
            sqlCommand1.EnableOptimizedParameterBinding = false;
            // 
            // RepairOrderForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 941);
            Controls.Add(grpParts);
            Controls.Add(grpExecution);
            Controls.Add(grpDetails);
            Name = "RepairOrderForm";
            Text = " Repair Order Form";
            grpDetails.ResumeLayout(false);
            grpDetails.PerformLayout();
            grpExecution.ResumeLayout(false);
            grpExecution.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)numHoursWorked).EndInit();
            grpParts.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)numPartQty).EndInit();
            ((System.ComponentModel.ISupportInitialize)dgvOrderParts).EndInit();
            ResumeLayout(false);
        }


        #endregion

        private GroupBox grpDetails;
        private TextBox txtProblem;
        private Label label3;
        private ComboBox cmbServiceType;
        private Label label2;
        private TextBox txtDeviceId;
        private Label label1;
        private GroupBox grpExecution;
        private NumericUpDown numHoursWorked;
        private Label label6;
        private TextBox txtDiagnosis;
        private Label label5;
        private ComboBox cmbStatus;
        private Label label4;
        private Button btnUpdateProgress;
        private GroupBox grpParts;
        private DataGridView dgvOrderParts;
        private Microsoft.Data.SqlClient.SqlCommand sqlCommand1;
        private Button btnRemovePart;
        private Button btnAdd;
        private NumericUpDown numPartQty;
        private ComboBox cmbAvailableParts;
        private Label lblTotalCost;
        private Label label8;
        private Label lblStatus;
        private Label label7;
        private Label lblBalance;
        private Label label9;
    }
}