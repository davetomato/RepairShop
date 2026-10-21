using System;
using System.Windows.Forms;
using FixFlow.Models;
using FixFlow.Services;

namespace FixFlow.Forms
{
    public partial class RepairBoardForm : Form
    {
        private readonly RepairService _repairService = new RepairService();

        public RepairBoardForm()
        {
            InitializeComponent();
            LoadBoard();
        }

        private void LoadBoard()
        {
            try
            {
                dgvOrders.DataSource = _repairService.GetBoard()[RepairStatus.Received];
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error Loading Board", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void OpenSelectedOrder()
        {
            if (dgvOrders.SelectedRows.Count > 0)
            {
                int orderId = Convert.ToInt32(dgvOrders.SelectedRows[0].Cells["OrderId"].Value);
                RepairOrderForm orderForm = new RepairOrderForm(orderId);
                orderForm.ShowDialog();
                LoadBoard();
            }
            else
            {
                MessageBox.Show("Please select a repair order to open.", "Selection Required", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnOpenOrder_Click(object sender, EventArgs e)
        {
            OpenSelectedOrder();
        }

        private void dgvOrders_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                OpenSelectedOrder();
            }
        }
    }
}