using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using FixFlow.Models;
using FixFlow.Services;

namespace FixFlow.Forms
{
    public partial class ServiceRatesForm : Form
    {
        private readonly ServiceRateService _rateService = new ServiceRateService();

        public ServiceRatesForm()
        {
            InitializeComponent();
            LoadRates();
        }

        private void LoadRates()
        {
            try
            {
                dgvRates.DataSource = _rateService.GetAll();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Access Denied / Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void dgvRates_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvRates.Rows[e.RowIndex];

                txtServiceName.Text = row.Cells["Name"].Value.ToString();
                numRate.Value = Convert.ToDecimal(row.Cells["Rate"].Value);
            }
        }

        private void btnSaveRate_Click(object sender, EventArgs e)
        {
            try
            {
                string serviceName = txtServiceName.Text;
                decimal newRate = numRate.Value;

                _rateService.UpdateRate(serviceName, newRate);

                LoadRates();
                MessageBox.Show("Service rate updated successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Update Blocked / Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}