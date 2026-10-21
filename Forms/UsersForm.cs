using System;
using System.Windows.Forms;
using FixFlow.Models;
using FixFlow.Services;

namespace FixFlow.Forms
{
    public partial class UsersForm : Form
    {
        private readonly UserService _userService = new UserService();
        private readonly AuthService _authService = new AuthService();
        private int _selectedUserId = 0;

        public UsersForm()
        {
            InitializeComponent();
            cmbRole.DataSource = Enum.GetValues(typeof(UserRole));
            LoadUsers();
        }

        private void LoadUsers()
        {
            try
            {
                dgvUsers.DataSource = _userService.GetAll();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Access Denied", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnCreateUser_Click(object sender, EventArgs e)
        {
            try
            {
                UserRole role = (UserRole)cmbRole.SelectedItem;
                _authService.CreateUser(role, txtUsername.Text, txtPassword.Text);
                LoadUsers();
                MessageBox.Show("User account created successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtUsername.Clear();
                txtPassword.Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Creation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgvUsers_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvUsers.Rows[e.RowIndex];
                _selectedUserId = Convert.ToInt32(row.Cells["UserId"].Value);
                txtUsername.Text = row.Cells["Username"].Value.ToString();
            }
        }

        private void btnDeleteUser_Click(object sender, EventArgs e)
        {
            if (_selectedUserId == 0) return;

            try
            {
                _userService.Delete(_selectedUserId);
                LoadUsers();
                MessageBox.Show("User deleted successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                _selectedUserId = 0;
                txtUsername.Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Deletion Blocked", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}