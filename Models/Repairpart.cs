using System;

namespace FixFlow.Models
{
    
    public class RepairPart
    {
        private int _orderId;
        private int _partId;
        private int _quantity;
        private decimal _unitPrice;

        public RepairPart() { }

        public RepairPart(Part part, int quantity)
        {
            if (part == null)
                throw new ArgumentNullException("part");
            PartId = part.PartId;
            UnitPrice = part.Price;
            Quantity = quantity;
        }

        public int OrderId
        {
            get { return _orderId; }
            set { _orderId = value; }
        }

        public int PartId
        {
            get { return _partId; }
            set { _partId = value; }
        }

        public int Quantity
        {
            get { return _quantity; }
            set
            {
                if (value <= 0)
                    throw new ArgumentException("Quantity must be greater than zero.");
                _quantity = value;
            }
        }

        public decimal UnitPrice
        {
            get { return _unitPrice; }
            set
            {
                if (value < 0)
                    throw new ArgumentException("Unit price cannot be negative.");
                _unitPrice = value;
            }
        }

        public decimal LineTotal
        {
            get { return _unitPrice * _quantity; }
        }
    }
}
