namespace FixFlow.Forms
{
    partial class CustomersForm
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
            dgvCustomers = new DataGridView();
            grpCustomer = new GroupBox();
            btnDelete = new Button();
            btnUpdate = new Button();
            btnAdd = new Button();
            txtAddress = new TextBox();
            txtContact = new TextBox();
            txtName = new TextBox();
            label3 = new Label();
            label2 = new Label();
            label1 = new Label();
            ((System.ComponentModel.ISupportInitialize)dgvCustomers).BeginInit();
            grpCustomer.SuspendLayout();
            SuspendLayout();
            // 
            // dgvCustomers
            // 
            dgvCustomers.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvCustomers.Dock = DockStyle.Left;
            dgvCustomers.Location = new Point(0, 0);
            dgvCustomers.Name = "dgvCustomers";
            dgvCustomers.RowHeadersWidth = 51;
            dgvCustomers.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvCustomers.Size = new Size(300, 450);
            dgvCustomers.TabIndex = 0;
            dgvCustomers.CellClick += dgvCustomers_CellClick;
            // 
            // grpCustomer
            // 
            grpCustomer.Controls.Add(btnDelete);
            grpCustomer.Controls.Add(btnUpdate);
            grpCustomer.Controls.Add(btnAdd);
            grpCustomer.Controls.Add(txtAddress);
            grpCustomer.Controls.Add(txtContact);
            grpCustomer.Controls.Add(txtName);
            grpCustomer.Controls.Add(label3);
            grpCustomer.Controls.Add(label2);
            grpCustomer.Controls.Add(label1);
            grpCustomer.Dock = DockStyle.Fill;
            grpCustomer.Location = new Point(300, 0);
            grpCustomer.Name = "grpCustomer";
            grpCustomer.Size = new Size(500, 450);
            grpCustomer.TabIndex = 1;
            grpCustomer.TabStop = false;
            grpCustomer.Text = "Customer Management";
            // 
            // btnDelete
            // 
            btnDelete.Location = new Point(89, 372);
            btnDelete.Name = "btnDelete";
            btnDelete.Size = new Size(138, 29);
            btnDelete.TabIndex = 8;
            btnDelete.Text = "Delete Customer";
            btnDelete.UseVisualStyleBackColor = true;
            btnDelete.Click += btnDelete_Click;
            // 
            // btnUpdate
            // 
            btnUpdate.Location = new Point(87, 309);
            btnUpdate.Name = "btnUpdate";
            btnUpdate.Size = new Size(140, 29);
            btnUpdate.TabIndex = 7;
            btnUpdate.Text = "Update Customer";
            btnUpdate.UseVisualStyleBackColor = true;
            btnUpdate.Click += btnUpdate_Click;
            // 
            // btnAdd
            // 
            btnAdd.Location = new Point(84, 251);
            btnAdd.Name = "btnAdd";
            btnAdd.Size = new Size(143, 29);
            btnAdd.TabIndex = 6;
            btnAdd.Text = "Add Customer";
            btnAdd.UseVisualStyleBackColor = true;
            btnAdd.Click += btnAdd_Click;
            // 
            // txtAddress
            // 
            txtAddress.Location = new Point(181, 169);
            txtAddress.Multiline = true;
            txtAddress.Name = "txtAddress";
            txtAddress.Size = new Size(125, 34);
            txtAddress.TabIndex = 5;
            // 
            // txtContact
            // 
            txtContact.Location = new Point(181, 115);
            txtContact.Name = "txtContact";
            txtContact.Size = new Size(125, 27);
            txtContact.TabIndex = 4;
            // 
            // txtName
            // 
            txtName.Location = new Point(181, 64);
            txtName.Name = "txtName";
            txtName.Size = new Size(125, 27);
            txtName.TabIndex = 3;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(60, 172);
            label3.Name = "label3";
            label3.Size = new Size(62, 20);
            label3.TabIndex = 2;
            label3.Text = "Address";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(60, 115);
            label2.Name = "label2";
            label2.Size = new Size(60, 20);
            label2.TabIndex = 1;
            label2.Text = "Contact";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(61, 67);
            label1.Name = "label1";
            label1.Size = new Size(49, 20);
            label1.TabIndex = 0;
            label1.Text = "Name";
            // 
            // CustomersForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(grpCustomer);
            Controls.Add(dgvCustomers);
            Name = "CustomersForm";
            Text = "Customers Form";
            ((System.ComponentModel.ISupportInitialize)dgvCustomers).EndInit();
            grpCustomer.ResumeLayout(false);
            grpCustomer.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private DataGridView dgvCustomers;
        private GroupBox grpCustomer;
        private Button btnDelete;
        private Button btnUpdate;
        private Button btnAdd;
        private TextBox txtAddress;
        private TextBox txtContact;
        private TextBox txtName;
        private Label label3;
        private Label label2;
        private Label label1;
    }
}