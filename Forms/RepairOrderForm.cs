using FixFlow.Models;
using FixFlow.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace FixFlow.Forms
{
    public partial class RepairOrderForm : Form
    {
        // 1. Declare service instances and state variables at the class level
        private readonly RepairService _repairService = new RepairService();
        private readonly InventoryService _inventoryService = new InventoryService();
        private readonly BillingService _billingService = new BillingService();
        private int _currentOrderId;

        public RepairOrderForm()
        {
            InitializeComponent();
        }

        public RepairOrderForm(int orderId) : this()
        {
            LoadOrderDetails(orderId);
        }

        public void LoadOrderDetails(int orderId)
        {
            _currentOrderId = orderId;
            RepairOrder order = _repairService.GetOrder(orderId);
            if (order == null) return;

            txtProblem.Text = order.Problem;
            txtDiagnosis.Text = order.Diagnosis;
            numHoursWorked.Value = order.HoursWorked;
            lblStatus.Text = $"Current Status: {order.Status}";

            BillingSummary summary = _billingService.GetSummary(order);
            lblTotalCost.Text = $"Total: ₱{summary.TotalCost:0.00}";
            lblBalance.Text = $"Balance Due: ₱{summary.Balance:0.00}";
        }

        private void btnUpdateProgress_Click(object sender, EventArgs e)
        {
            try
            {
                _repairService.UpdateDetails(_currentOrderId, txtDiagnosis.Text, numHoursWorked.Value);

                RepairOrder order = _repairService.GetOrder(_currentOrderId);
                RepairStatus nextStatus = (RepairStatus)(((int)order.Status) + 1);

                _repairService.MoveOrder(_currentOrderId, nextStatus);

                LoadOrderDetails(_currentOrderId);

                MessageBox.Show("Repair progress updated successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Transition Blocked", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}
