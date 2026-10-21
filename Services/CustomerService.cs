using System.Collections.Generic;
using FixFlow.Data;
using FixFlow.Models;

namespace FixFlow.Services
{
    public class CustomerService
    {
        private readonly CustomerRepository _customers = new CustomerRepository();

        public List<Customer> GetAll()
        {
            AccessControl.RequireLogin();
            return _customers.GetAll();
        }

        public List<Customer> Search(string term)
        {
            AccessControl.RequireLogin();
            return _customers.Search(term);
        }

        public Customer GetById(int customerId)
        {
            AccessControl.RequireLogin();
            return _customers.GetById(customerId);
        }

        public Customer Add(string name, string contact, string address)
        {
            AccessControl.RequireRole(UserRole.Administrator, UserRole.Receptionist);

            Customer customer = new Customer(0, name, contact, address);
            _customers.Add(customer);
            return customer;
        }

        public void Update(int customerId, string name, string contact, string address)
        {
            AccessControl.RequireRole(UserRole.Administrator, UserRole.Receptionist);
            _customers.Update(new Customer(customerId, name, contact, address));
        }

        public void Delete(int customerId)
        {
            AccessControl.RequireRole(UserRole.Administrator);
            _customers.Delete(customerId);
        }
    }
}
