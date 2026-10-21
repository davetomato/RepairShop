using System;
using System.Windows.Forms;
using FixFlow.Models;
using FixFlow.Services;

namespace FixFlow.Forms
{
    public partial class PaymentsForm : Form
    {
        private readonly BillingService _billingService = new BillingService();

        public PaymentsForm()
        {
            InitializeComponent();
        }

        private void btnRecordPayment_Click(object sender, EventArgs e)
        {
            try
            {
                if (!int.TryParse(txtOrderId.Text, out int orderId))
                {
                    MessageBox.Show("Please enter a valid Order ID.", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                decimal amount = numAmount.Value;
                _billingService.RecordPayment(orderId, amount);

                MessageBox.Show("Payment recorded successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtOrderId.Clear();
                numAmount.Value = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Payment Blocked", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}