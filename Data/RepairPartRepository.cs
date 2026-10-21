using System;
using System.Collections.Generic;
using FixFlow.Models;
using Microsoft.Data.SqlClient;

namespace FixFlow.Data
{
    public class RepairPartRepository
    {
        public List<RepairPart> GetByOrder(int orderId)
        {
            List<RepairPart> parts = new List<RepairPart>();
            using (SqlConnection conn = Database.GetConnection())
            using (SqlCommand cmd = new SqlCommand(
                "SELECT OrderId, PartId, Quantity, UnitPrice FROM dbo.OrderParts " +
                "WHERE OrderId = @OrderId ORDER BY PartId", conn))
            {
                cmd.Parameters.AddWithValue("@OrderId", orderId);
                conn.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        RepairPart part = new RepairPart();
                        part.OrderId = reader.GetInt32(0);
                        part.PartId = reader.GetInt32(1);
                        part.Quantity = reader.GetInt32(2);
                        part.UnitPrice = reader.GetDecimal(3);
                        parts.Add(part);
                    }
                }
            }
            return parts;
        }

        public void UsePart(int orderId, int partId, int quantity)
        {
            if (quantity <= 0)
                throw new ArgumentException("Quantity must be greater than zero.");

            try
            {
                using (SqlConnection conn = Database.GetConnection())
                {
                    conn.Open();
                    using (SqlTransaction tx = conn.BeginTransaction())
                    {
                        int deducted;
                        using (SqlCommand cmd = new SqlCommand(
                            "UPDATE dbo.Parts SET Stock = Stock - @Quantity " +
                            "WHERE PartId = @PartId AND Stock >= @Quantity", conn, tx))
                        {
                            cmd.Parameters.AddWithValue("@PartId", partId);
                            cmd.Parameters.AddWithValue("@Quantity", quantity);
                            deducted = cmd.ExecuteNonQuery();
                        }
                        if (deducted == 0)
                            throw new InvalidOperationException(
                                "Not enough stock, or the part does not exist.");

                        int updated;
                        using (SqlCommand cmd = new SqlCommand(
                            "UPDATE dbo.OrderParts SET Quantity = Quantity + @Quantity " +
                            "WHERE OrderId = @OrderId AND PartId = @PartId", conn, tx))
                        {
                            cmd.Parameters.AddWithValue("@OrderId", orderId);
                            cmd.Parameters.AddWithValue("@PartId", partId);
                            cmd.Parameters.AddWithValue("@Quantity", quantity);
                            updated = cmd.ExecuteNonQuery();
                        }

                        if (updated == 0)
                        {
                            using (SqlCommand cmd = new SqlCommand(
                                "INSERT INTO dbo.OrderParts (OrderId, PartId, Quantity, UnitPrice) " +
                                "SELECT @OrderId, PartId, @Quantity, Price FROM dbo.Parts " +
                                "WHERE PartId = @PartId", conn, tx))
                            {
                                cmd.Parameters.AddWithValue("@OrderId", orderId);
                                cmd.Parameters.AddWithValue("@PartId", partId);
                                cmd.Parameters.AddWithValue("@Quantity", quantity);
                                cmd.ExecuteNonQuery();
                            }
                        }

                        tx.Commit();
                    }
                }
            }
            catch (SqlException ex)
            {
                if (DbHelper.IsForeignKeyError(ex))
                    throw new InvalidOperationException("The repair order does not exist.");
                throw;
            }
        }

        public void ReturnPart(int orderId, int partId)
        {
            using (SqlConnection conn = Database.GetConnection())
            {
                conn.Open();
                using (SqlTransaction tx = conn.BeginTransaction())
                {
                    object found;
                    using (SqlCommand cmd = new SqlCommand(
                        "SELECT Quantity FROM dbo.OrderParts WHERE OrderId = @OrderId AND PartId = @PartId",
                        conn, tx))
                    {
                        cmd.Parameters.AddWithValue("@OrderId", orderId);
                        cmd.Parameters.AddWithValue("@PartId", partId);
                        found = cmd.ExecuteScalar();
                    }
                    if (found == null)
                        throw new InvalidOperationException("That part is not used in this repair order.");
                    int quantity = Convert.ToInt32(found);

                    using (SqlCommand cmd = new SqlCommand(
                        "DELETE FROM dbo.OrderParts WHERE OrderId = @OrderId AND PartId = @PartId",
                        conn, tx))
                    {
                        cmd.Parameters.AddWithValue("@OrderId", orderId);
                        cmd.Parameters.AddWithValue("@PartId", partId);
                        cmd.ExecuteNonQuery();
                    }

                    using (SqlCommand cmd = new SqlCommand(
                        "UPDATE dbo.Parts SET Stock = Stock + @Quantity WHERE PartId = @PartId",
                        conn, tx))
                    {
                        cmd.Parameters.AddWithValue("@PartId", partId);
                        cmd.Parameters.AddWithValue("@Quantity", quantity);
                        cmd.ExecuteNonQuery();
                    }

                    tx.Commit();
                }
            }
        }
    }
}
