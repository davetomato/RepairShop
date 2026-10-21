namespace FixFlow.Forms
{
    partial class DevicesForm
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
            dgvDevices = new DataGridView();
            grpDevice = new GroupBox();
            btnAddDevice = new Button();
            txtSerialNo = new TextBox();
            txtModel = new TextBox();
            txtBrand = new TextBox();
            txtDeviceType = new TextBox();
            cmbCustomer = new ComboBox();
            label5 = new Label();
            label4 = new Label();
            label3 = new Label();
            label2 = new Label();
            label1 = new Label();
            btnDeleteDevice = new Button();
            ((System.ComponentModel.ISupportInitialize)dgvDevices).BeginInit();
            grpDevice.SuspendLayout();
            SuspendLayout();
            // 
            // dgvDevices
            // 
            dgvDevices.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvDevices.Dock = DockStyle.Left;
            dgvDevices.Location = new Point(0, 0);
            dgvDevices.Name = "dgvDevices";
            dgvDevices.RowHeadersWidth = 51;
            dgvDevices.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvDevices.Size = new Size(300, 450);
            dgvDevices.TabIndex = 0;
            dgvDevices.CellClick += dgvDevices_CellClick;
            // 
            // grpDevice
            // 
            grpDevice.Controls.Add(btnDeleteDevice);
            grpDevice.Controls.Add(btnAddDevice);
            grpDevice.Controls.Add(txtSerialNo);
            grpDevice.Controls.Add(txtModel);
            grpDevice.Controls.Add(txtBrand);
            grpDevice.Controls.Add(txtDeviceType);
            grpDevice.Controls.Add(cmbCustomer);
            grpDevice.Controls.Add(label5);
            grpDevice.Controls.Add(label4);
            grpDevice.Controls.Add(label3);
            grpDevice.Controls.Add(label2);
            grpDevice.Controls.Add(label1);
            grpDevice.Dock = DockStyle.Fill;
            grpDevice.Location = new Point(300, 0);
            grpDevice.Name = "grpDevice";
            grpDevice.Size = new Size(500, 450);
            grpDevice.TabIndex = 1;
            grpDevice.TabStop = false;
            grpDevice.Text = "Register New Device";
            // 
            // btnAddDevice
            // 
            btnAddDevice.Location = new Point(55, 378);
            btnAddDevice.Name = "btnAddDevice";
            btnAddDevice.Size = new Size(139, 29);
            btnAddDevice.TabIndex = 10;
            btnAddDevice.Text = "Register Device";
            btnAddDevice.UseVisualStyleBackColor = true;
            btnAddDevice.Click += btnAddDevice_Click;
            // 
            // txtSerialNo
            // 
            txtSerialNo.Location = new Point(160, 300);
            txtSerialNo.Name = "txtSerialNo";
            txtSerialNo.Size = new Size(125, 27);
            txtSerialNo.TabIndex = 9;
            // 
            // txtModel
            // 
            txtModel.Location = new Point(160, 258);
            txtModel.Name = "txtModel";
            txtModel.Size = new Size(125, 27);
            txtModel.TabIndex = 8;
            // 
            // txtBrand
            // 
            txtBrand.Location = new Point(160, 198);
            txtBrand.Name = "txtBrand";
            txtBrand.Size = new Size(125, 27);
            txtBrand.TabIndex = 7;
            // 
            // txtDeviceType
            // 
            txtDeviceType.Location = new Point(160, 146);
            txtDeviceType.Name = "txtDeviceType";
            txtDeviceType.Size = new Size(125, 27);
            txtDeviceType.TabIndex = 6;
            // 
            // cmbCustomer
            // 
            cmbCustomer.FormattingEnabled = true;
            cmbCustomer.Location = new Point(160, 87);
            cmbCustomer.Name = "cmbCustomer";
            cmbCustomer.Size = new Size(151, 28);
            cmbCustomer.TabIndex = 5;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(55, 303);
            label5.Name = "label5";
            label5.Size = new Size(70, 20);
            label5.TabIndex = 4;
            label5.Text = "Serial No";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(55, 258);
            label4.Name = "label4";
            label4.Size = new Size(52, 20);
            label4.TabIndex = 3;
            label4.Text = "Model";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(55, 198);
            label3.Name = "label3";
            label3.Size = new Size(48, 20);
            label3.TabIndex = 2;
            label3.Text = "Brand";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(55, 146);
            label2.Name = "label2";
            label2.Size = new Size(89, 20);
            label2.TabIndex = 1;
            label2.Text = "Device Type";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(55, 87);
            label1.Name = "label1";
            label1.Size = new Size(52, 20);
            label1.TabIndex = 0;
            label1.Text = "Owner";
            // 
            // btnDeleteDevice
            // 
            btnDeleteDevice.BackColor = SystemColors.ControlDark;
            btnDeleteDevice.Location = new Point(217, 378);
            btnDeleteDevice.Name = "btnDeleteDevice";
            btnDeleteDevice.Size = new Size(149, 29);
            btnDeleteDevice.TabIndex = 11;
            btnDeleteDevice.Text = "Delete Device";
            btnDeleteDevice.UseVisualStyleBackColor = false;
            btnDeleteDevice.Click += btnDeleteDevice_Click;
            // 
            // DevicesForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(grpDevice);
            Controls.Add(dgvDevices);
            Name = "DevicesForm";
            Text = "Devices Form";
            ((System.ComponentModel.ISupportInitialize)dgvDevices).EndInit();
            grpDevice.ResumeLayout(false);
            grpDevice.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private DataGridView dgvDevices;
        private GroupBox grpDevice;
        private Label label5;
        private Label label4;
        private Label label3;
        private Label label2;
        private Label label1;
        private Button btnAddDevice;
        private TextBox txtSerialNo;
        private TextBox txtModel;
        private TextBox txtBrand;
        private TextBox txtDeviceType;
        private ComboBox cmbCustomer;
        private Button btnDeleteDevice;
    }
}