using System;
using System.Windows.Forms;
using FixFlow.Models;
using FixFlow.Services;

namespace FixFlow.Forms
{
    public partial class CustomersForm : Form
    {
        private readonly CustomerService _customerService = new CustomerService();
        private int _selectedCustomerId = 0;

        public CustomersForm()
        {
            InitializeComponent();
            LoadCustomers();
        }

        private void LoadCustomers()
        {
            try
            {
                dgvCustomers.DataSource = _customerService.GetAll();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                _customerService.Add(txtName.Text, txtContact.Text, txtAddress.Text);
                LoadCustomers();
                ClearForm();
                MessageBox.Show("Customer added successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgvCustomers_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvCustomers.Rows[e.RowIndex];
                _selectedCustomerId = Convert.ToInt32(row.Cells["CustomerId"].Value);
                txtName.Text = row.Cells["Name"].Value.ToString();
                txtContact.Text = row.Cells["Contact"].Value.ToString();
                txtAddress.Text = row.Cells["Address"]?.Value?.ToString() ?? string.Empty;
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (_selectedCustomerId == 0)
            {
                MessageBox.Show("Select a customer from the grid to update.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                _customerService.Update(_selectedCustomerId, txtName.Text, txtContact.Text, txtAddress.Text);
                LoadCustomers();
                ClearForm();
                MessageBox.Show("Customer updated successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (_selectedCustomerId == 0) return;

            try
            {
                _customerService.Delete(_selectedCustomerId);
                LoadCustomers();
                ClearForm();
                MessageBox.Show("Customer deleted successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Deletion Blocked", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ClearForm()
        {
            _selectedCustomerId = 0;
            txtName.Clear();
            txtContact.Clear();
            txtAddress.Clear();
        }
    }
}