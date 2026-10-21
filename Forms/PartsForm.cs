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
    public partial class PartsForm : Form
    {
        private readonly PartService _partService = new PartService();
        private int _selectedPartId = 0;

        public PartsForm()
        {
            InitializeComponent();
            LoadParts();
        }

        private void LoadParts()
        {
            try
            {
                dgvParts.DataSource = _partService.GetAll();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Access Denied / Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                _partService.Add(txtName.Text, numPrice.Value, (int)numStock.Value);
                LoadParts();
                ClearForm();
                MessageBox.Show("Part added successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
            private void dgvParts_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvParts.Rows[e.RowIndex];
                _selectedPartId = Convert.ToInt32(row.Cells["PartId"].Value);
                txtName.Text = row.Cells["Name"].Value.ToString();
                numPrice.Value = Convert.ToDecimal(row.Cells["Price"].Value);
                numStock.Value = Convert.ToInt32(row.Cells["Stock"].Value);
            }

        }
        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (_selectedPartId == 0)
            {
                MessageBox.Show("Please select a part from the grid to update.", "Selection Required", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                // Calls PartService to update the selected part details[cite: 42]
                _partService.Update(_selectedPartId, txtName.Text, numPrice.Value, (int)numStock.Value);
                LoadParts();
                ClearForm();
                MessageBox.Show("Part updated successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (_selectedPartId == 0)
            {
                MessageBox.Show("Please select a part from the grid to delete.", "Selection Required", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DialogResult confirm = MessageBox.Show("Are you sure you want to delete this part?", "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (confirm == DialogResult.Yes)
            {
                try
                {
                    _partService.Delete(_selectedPartId);
                    LoadParts();
                    ClearForm();
                    MessageBox.Show("Part deleted successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Deletion Blocked", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

        }

        private void ClearForm()
        {
            _selectedPartId = 0;
            txtName.Clear();
            numPrice.Value = 0;
            numStock.Value = 0;
        }

        
    }
}
