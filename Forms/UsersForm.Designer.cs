namespace FixFlow.Forms
{
    partial class UsersForm
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
            dgvUsers = new DataGridView();
            grpUser = new GroupBox();
            label1 = new Label();
            txtUsername = new TextBox();
            label2 = new Label();
            txtPassword = new TextBox();
            cmbRole = new ComboBox();
            label3 = new Label();
            btnCreateUser = new Button();
            btnDeleteUser = new Button();
            ((System.ComponentModel.ISupportInitialize)dgvUsers).BeginInit();
            grpUser.SuspendLayout();
            SuspendLayout();
            // 
            // dgvUsers
            // 
            dgvUsers.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvUsers.Dock = DockStyle.Left;
            dgvUsers.Location = new Point(0, 0);
            dgvUsers.Name = "dgvUsers";
            dgvUsers.RowHeadersWidth = 51;
            dgvUsers.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvUsers.Size = new Size(300, 450);
            dgvUsers.TabIndex = 0;
            dgvUsers.CellClick += this.dgvUsers_CellClick;
            // 
            // grpUser
            // 
            grpUser.Controls.Add(btnDeleteUser);
            grpUser.Controls.Add(btnCreateUser);
            grpUser.Controls.Add(label3);
            grpUser.Controls.Add(cmbRole);
            grpUser.Controls.Add(txtPassword);
            grpUser.Controls.Add(label2);
            grpUser.Controls.Add(txtUsername);
            grpUser.Controls.Add(label1);
            grpUser.Dock = DockStyle.Fill;
            grpUser.Location = new Point(300, 0);
            grpUser.Name = "grpUser";
            grpUser.Size = new Size(500, 450);
            grpUser.TabIndex = 1;
            grpUser.TabStop = false;
            grpUser.Text = "User Account Management";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(43, 90);
            label1.Name = "label1";
            label1.Size = new Size(75, 20);
            label1.TabIndex = 0;
            label1.Text = "Username";
            // 
            // txtUsername
            // 
            txtUsername.Location = new Point(181, 87);
            txtUsername.Name = "txtUsername";
            txtUsername.Size = new Size(125, 27);
            txtUsername.TabIndex = 1;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(43, 141);
            label2.Name = "label2";
            label2.Size = new Size(70, 20);
            label2.TabIndex = 2;
            label2.Text = "Password";
            // 
            // txtPassword
            // 
            txtPassword.Location = new Point(181, 141);
            txtPassword.Name = "txtPassword";
            txtPassword.Size = new Size(125, 27);
            txtPassword.TabIndex = 3;
            txtPassword.UseSystemPasswordChar = true;
            // 
            // cmbRole
            // 
            cmbRole.FormattingEnabled = true;
            cmbRole.Location = new Point(181, 202);
            cmbRole.Name = "cmbRole";
            cmbRole.Size = new Size(151, 28);
            cmbRole.TabIndex = 4;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(43, 205);
            label3.Name = "label3";
            label3.Size = new Size(39, 20);
            label3.TabIndex = 5;
            label3.Text = "Role";
            // 
            // btnCreateUser
            // 
            btnCreateUser.Location = new Point(72, 281);
            btnCreateUser.Name = "btnCreateUser";
            btnCreateUser.Size = new Size(110, 29);
            btnCreateUser.TabIndex = 6;
            btnCreateUser.Text = "Create User";
            btnCreateUser.UseVisualStyleBackColor = true;
            btnCreateUser.Click += this.btnCreateUser_Click;
            // 
            // btnDeleteUser
            // 
            btnDeleteUser.Location = new Point(212, 281);
            btnDeleteUser.Name = "btnDeleteUser";
            btnDeleteUser.Size = new Size(120, 29);
            btnDeleteUser.TabIndex = 7;
            btnDeleteUser.Text = "Delete User";
            btnDeleteUser.UseVisualStyleBackColor = true;
            btnDeleteUser.Click += this.btnDeleteUser_Click;
            // 
            // UsersForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(grpUser);
            Controls.Add(dgvUsers);
            Name = "UsersForm";
            Text = "Users Form";
            ((System.ComponentModel.ISupportInitialize)dgvUsers).EndInit();
            grpUser.ResumeLayout(false);
            grpUser.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private DataGridView dgvUsers;
        private GroupBox grpUser;
        private TextBox txtPassword;
        private Label label2;
        private TextBox txtUsername;
        private Label label1;
        private Button btnDeleteUser;
        private Button btnCreateUser;
        private Label label3;
        private ComboBox cmbRole;
    }
}