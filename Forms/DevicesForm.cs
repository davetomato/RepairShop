using System;
using System.Windows.Forms;
using FixFlow.Models;
using FixFlow.Services;

namespace FixFlow.Forms
{
    public partial class DevicesForm : Form
    {
        private readonly DeviceService _deviceService = new DeviceService();
        private readonly CustomerService _customerService = new CustomerService();
        private int _selectedDeviceId = 0;

        public DevicesForm()
        {
            InitializeComponent();
            LoadCustomersComboBox();
            LoadDevices();
        }

        private void LoadCustomersComboBox()
        {
            try
            {
                cmbCustomer.DataSource = _customerService.GetAll();
                cmbCustomer.DisplayMember = "Name";
                cmbCustomer.ValueMember = "CustomerId";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error Loading Customers", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void LoadDevices()
        {
            try
            {
                dgvDevices.DataSource = _deviceService.GetAll();
                ClearForm();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error Loading Devices", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnAddDevice_Click(object sender, EventArgs e)
        {
            try
            {
                int customerId = Convert.ToInt32(cmbCustomer.SelectedValue);
                _deviceService.Add(customerId, txtDeviceType.Text, txtBrand.Text, txtModel.Text, txtSerialNo.Text);
                LoadDevices();
                MessageBox.Show("Device registered successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Registration Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgvDevices_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvDevices.Rows[e.RowIndex];
                _selectedDeviceId = Convert.ToInt32(row.Cells["DeviceId"].Value);

                if (row.Cells["CustomerId"].Value != null)
                {
                    cmbCustomer.SelectedValue = Convert.ToInt32(row.Cells["CustomerId"].Value);
                }

                txtDeviceType.Text = row.Cells["DeviceType"]?.Value?.ToString() ?? string.Empty;
                txtBrand.Text = row.Cells["Brand"]?.Value?.ToString() ?? string.Empty;
                txtModel.Text = row.Cells["Model"]?.Value?.ToString() ?? string.Empty;
                txtSerialNo.Text = row.Cells["SerialNo"]?.Value?.ToString() ?? string.Empty;
            }
        }

        private void btnDeleteDevice_Click(object sender, EventArgs e)
        {
            if (_selectedDeviceId == 0)
            {
                MessageBox.Show("Please select a device from the grid to delete.", "Selection Required", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DialogResult confirm = MessageBox.Show(
                "Are you sure you want to delete this device?",
                "Confirm Deletion",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (confirm == DialogResult.Yes)
            {
                try
                {
                    _deviceService.Delete(_selectedDeviceId);
                    LoadDevices();
                    MessageBox.Show("Device deleted successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Deletion Blocked", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        private void ClearForm()
        {
            _selectedDeviceId = 0;
            txtDeviceType.Clear();
            txtBrand.Clear();
            txtModel.Clear();
            txtSerialNo.Clear();
        }
    }
}