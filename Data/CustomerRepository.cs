using System;
using System.Collections.Generic;
using FixFlow.Models;
using Microsoft.Data.SqlClient;

namespace FixFlow.Data
{
    public class CustomerRepository
    {
        private const string SelectCustomers =
            "SELECT CustomerId, Name, Contact, Address FROM dbo.Customers ";

        private static Customer Read(SqlDataReader reader)
        {
            return new Customer(
                reader.GetInt32(0),
                reader.GetString(1),
                reader.GetString(2),
                DbHelper.GetStringOrEmpty(reader, 3));
        }

        private List<Customer> Query(string tail, Action<SqlCommand> addParameters)
        {
            List<Customer> customers = new List<Customer>();
            using (SqlConnection conn = Database.GetConnection())
            using (SqlCommand cmd = new SqlCommand(SelectCustomers + tail, conn))
            {
                if (addParameters != null)
                    addParameters(cmd);
                conn.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                        customers.Add(Read(reader));
                }
            }
            return customers;
        }

        public List<Customer> GetAll()
        {
            return Query("ORDER BY Name", null);
        }

        public List<Customer> Search(string term)
        {
            string pattern = "%" + (term ?? string.Empty).Trim() + "%";
            return Query(
                "WHERE Name LIKE @Term OR Contact LIKE @Term ORDER BY Name",
                cmd => cmd.Parameters.AddWithValue("@Term", pattern));
        }

        public Customer GetById(int customerId)
        {
            List<Customer> result = Query(
                "WHERE CustomerId = @CustomerId",
                cmd => cmd.Parameters.AddWithValue("@CustomerId", customerId));
            return result.Count > 0 ? result[0] : null;
        }

        public void Add(Customer customer)
        {
            if (customer == null)
                throw new ArgumentNullException("customer");

            using (SqlConnection conn = Database.GetConnection())
            using (SqlCommand cmd = new SqlCommand(
                "INSERT INTO dbo.Customers (Name, Contact, Address) " +
                "OUTPUT INSERTED.CustomerId " +
                "VALUES (@Name, @Contact, @Address)", conn))
            {
                cmd.Parameters.AddWithValue("@Name", customer.Name);
                cmd.Parameters.AddWithValue("@Contact", customer.Contact);
                cmd.Parameters.AddWithValue("@Address", DbHelper.ToDbValue(customer.Address));
                conn.Open();
                customer.CustomerId = (int)cmd.ExecuteScalar();
            }
        }

        public void Update(Customer customer)
        {
            if (customer == null)
                throw new ArgumentNullException("customer");

            using (SqlConnection conn = Database.GetConnection())
            using (SqlCommand cmd = new SqlCommand(
                "UPDATE dbo.Customers SET Name = @Name, Contact = @Contact, Address = @Address " +
                "WHERE CustomerId = @CustomerId", conn))
            {
                cmd.Parameters.AddWithValue("@CustomerId", customer.CustomerId);
                cmd.Parameters.AddWithValue("@Name", customer.Name);
                cmd.Parameters.AddWithValue("@Contact", customer.Contact);
                cmd.Parameters.AddWithValue("@Address", DbHelper.ToDbValue(customer.Address));
                conn.Open();
                if (cmd.ExecuteNonQuery() == 0)
                    throw new InvalidOperationException("Customer not found.");
            }
        }

        public void Delete(int customerId)
        {
            try
            {
                using (SqlConnection conn = Database.GetConnection())
                using (SqlCommand cmd = new SqlCommand(
                    "DELETE FROM dbo.Customers WHERE CustomerId = @CustomerId", conn))
                {
                    cmd.Parameters.AddWithValue("@CustomerId", customerId);
                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
            catch (SqlException ex)
            {
                if (DbHelper.IsForeignKeyError(ex))
                    throw new InvalidOperationException(
                        "This customer still has devices, so the customer cannot be deleted.");
                throw;
            }
        }
    }
}
