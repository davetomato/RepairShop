using System;

namespace FixFlow.Models
{
    public class Payment
    {
        private int _paymentId;
        private int _orderId;
        private decimal _amount;
        private DateTime _paymentDate = DateTime.Now;

        public Payment() { }

        public Payment(int paymentId, int orderId, decimal amount, DateTime paymentDate)
        {
            PaymentId = paymentId;
            OrderId = orderId;
            Amount = amount;
            PaymentDate = paymentDate;
        }

        public int PaymentId
        {
            get { return _paymentId; }
            set { _paymentId = value; }
        }

        
        public int OrderId
        {
            get { return _orderId; }
            set { _orderId = value; }
        }

        public decimal Amount
        {
            get { return _amount; }
            set
            {
                if (value <= 0)
                    throw new ArgumentException("Payment amount must be above zero.");
                _amount = value;
            }
        }

        public DateTime PaymentDate
        {
            get { return _paymentDate; }
            set { _paymentDate = value; }
        }
    }
}