using System;

namespace FixFlow.Models
{
    
    public class Part
    {
        private int _partId;
        private string _name = string.Empty;
        private decimal _price;
        private int _stock;

        
        public Part() { }

        public Part(int partId, string name, decimal price, int stock)
        {
            PartId = partId;
            Name = name;       
            Price = price;
            Stock = stock;
        }

        public int PartId
        {
            get { return _partId; }
            set { _partId = value; }
        }

        public string Name
        {
            get { return _name; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("Part name cannot be empty.");
                _name = value.Trim();
            }
        }

        public decimal Price
        {
            get { return _price; }
            set
            {
                if (value < 0)
                    throw new ArgumentException("Price cannot be negative.");
                _price = value;
            }
        }

        public int Stock
        {
            get { return _stock; }
            set
            {
                if (value < 0)
                    throw new ArgumentException("Stock cannot be negative.");
                _stock = value;
            }
        }

        
        public void DeductStock(int quantity)
        {
            if (quantity <= 0)
                throw new ArgumentException("Quantity must be greater than zero.");
            if (quantity > _stock)
                throw new InvalidOperationException("Not enough stock for " + _name + ".");
            _stock -= quantity;
        }

        
        public void AddStock(int quantity)
        {
            if (quantity <= 0)
                throw new ArgumentException("Quantity must be greater than zero.");
            _stock += quantity;
        }

        public override string ToString()
        {
            return Name;
        }
    }
}