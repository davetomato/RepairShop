using System;
using FixFlow.Data;
using FixFlow.Models;

namespace FixFlow.Services
{
    public class BillingSummary
    {
        public int OrderId { get; set; }
        public decimal Labor { get; set; }
        public decimal PartsCost { get; set; }
        public decimal TotalCost { get; set; }
        public decimal AmountPaid { get; set; }
        public decimal Balance { get; set; }
    }

    public class BillingService
    {
        private readonly RepairOrderRepository _orders = new RepairOrderRepository();
        private readonly PaymentRepository _payments = new PaymentRepository();

        public BillingSummary GetSummary(int orderId)
        {
            AccessControl.RequireLogin();

            RepairOrder order = _orders.GetById(orderId);
            if (order == null)
                throw new InvalidOperationException("Repair order not found.");
            return GetSummary(order);
        }

        public BillingSummary GetSummary(RepairOrder order)
        {
            AccessControl.RequireLogin();

            if (order == null)
                throw new ArgumentNullException("order");

            decimal paid = _payments.GetTotalPaid(order.OrderId);
            return new BillingSummary
            {
                OrderId = order.OrderId,
                Labor = order.ComputeLaborFee(),
                PartsCost = order.PartsCost,
                TotalCost = order.TotalCost,
                AmountPaid = paid,
                Balance = order.TotalCost - paid
            };
        }

        public decimal GetBalance(RepairOrder order)
        {
            return GetSummary(order).Balance;
        }

        public Payment RecordPayment(int orderId, decimal amount)
        {
            AccessControl.RequireRole(UserRole.Administrator, UserRole.Receptionist);

            if (amount <= 0)
                throw new ArgumentException("Payment amount must be above zero.");

            RepairOrder order = _orders.GetById(orderId);
            if (order == null)
                throw new InvalidOperationException("Repair order not found.");

            decimal balance = GetBalance(order);
            if (amount > balance)
                throw new InvalidOperationException(
                    "The payment is more than the balance of " + balance.ToString("0.00") + ".");

            Payment payment = new Payment(0, orderId, amount, DateTime.Now);
            _payments.Add(payment);
            return payment;
        }
    }
}