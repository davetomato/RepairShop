using System;

namespace FixFlow.Models
{
    public class Customer
    {
        private int _customerId;
        private string _name = string.Empty;
        private string _contact = string.Empty;
        private string _address = string.Empty;

        public Customer() { }

        public Customer(int customerId, string name, string contact, string address)
        {
            CustomerId = customerId;
            Name = name;
            Contact = contact;
            Address = address;
        }

        public int CustomerId
        {
            get { return _customerId; }
            set { _customerId = value; }
        }

        public string Name
        {
            get { return _name; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("Customer name cannot be empty.");
                _name = value.Trim();
            }
        }

        public string Contact
        {
            get { return _contact; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("Contact number cannot be empty.");
                _contact = value.Trim();
            }
        }

        
        public string Address
        {
            get { return _address; }
            set { _address = (value == null) ? string.Empty : value.Trim(); }
        }

        public override string ToString()
        {
            return Name;
        }
    }
}