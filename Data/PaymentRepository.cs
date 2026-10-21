using System;
using System.Collections.Generic;
using FixFlow.Models;
using Microsoft.Data.SqlClient;

namespace FixFlow.Data
{
    public class PaymentRepository
    {
        private const string SelectPayments =
            "SELECT PaymentId, OrderId, Amount, PaymentDate FROM dbo.Payments ";

        private static Payment Read(SqlDataReader reader)
        {
            return new Payment(
                reader.GetInt32(0),
                reader.GetInt32(1),
                reader.GetDecimal(2),
                reader.GetDateTime(3));
        }

        private List<Payment> Query(string tail, Action<SqlCommand> addParameters)
        {
            List<Payment> payments = new List<Payment>();
            using (SqlConnection conn = Database.GetConnection())
            using (SqlCommand cmd = new SqlCommand(SelectPayments + tail, conn))
            {
                if (addParameters != null)
                    addParameters(cmd);
                conn.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                        payments.Add(Read(reader));
                }
            }
            return payments;
        }

        public List<Payment> GetAll()
        {
            return Query("ORDER BY PaymentDate DESC, PaymentId DESC", null);
        }

        public List<Payment> GetByOrder(int orderId)
        {
            return Query(
                "WHERE OrderId = @OrderId ORDER BY PaymentDate, PaymentId",
                cmd => cmd.Parameters.AddWithValue("@OrderId", orderId));
        }

        public decimal GetTotalPaid(int orderId)
        {
            using (SqlConnection conn = Database.GetConnection())
            using (SqlCommand cmd = new SqlCommand(
                "SELECT COALESCE(SUM(Amount), 0) FROM dbo.Payments WHERE OrderId = @OrderId", conn))
            {
                cmd.Parameters.AddWithValue("@OrderId", orderId);
                conn.Open();
                return Convert.ToDecimal(cmd.ExecuteScalar());
            }
        }

        public void Add(Payment payment)
        {
            if (payment == null)
                throw new ArgumentNullException("payment");

            try
            {
                using (SqlConnection conn = Database.GetConnection())
                using (SqlCommand cmd = new SqlCommand(
                    "INSERT INTO dbo.Payments (OrderId, Amount, PaymentDate) " +
                    "OUTPUT INSERTED.PaymentId " +
                    "VALUES (@OrderId, @Amount, @PaymentDate)", conn))
                {
                    cmd.Parameters.AddWithValue("@OrderId", payment.OrderId);
                    cmd.Parameters.AddWithValue("@Amount", payment.Amount);
                    cmd.Parameters.AddWithValue("@PaymentDate", payment.PaymentDate);
                    conn.Open();
                    payment.PaymentId = (int)cmd.ExecuteScalar();
                }
            }
            catch (SqlException ex)
            {
                if (DbHelper.IsForeignKeyError(ex))
                    throw new InvalidOperationException("The repair order does not exist.");
                throw;
            }
        }

        public void Delete(int paymentId)
        {
            using (SqlConnection conn = Database.GetConnection())
            using (SqlCommand cmd = new SqlCommand(
                "DELETE FROM dbo.Payments WHERE PaymentId = @PaymentId", conn))
            {
                cmd.Parameters.AddWithValue("@PaymentId", paymentId);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }
    }
}
