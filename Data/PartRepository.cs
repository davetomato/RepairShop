using System;
using System.Collections.Generic;
using FixFlow.Models;
using Microsoft.Data.SqlClient;

namespace FixFlow.Data
{
    public class PartRepository
    {
        public List<Part> GetAll()
        {
            List<Part> parts = new List<Part>();
            using (SqlConnection conn = Database.GetConnection())
            using (SqlCommand cmd = new SqlCommand(
                "SELECT PartId, Name, Price, Stock FROM dbo.Parts ORDER BY Name", conn))
            {
                conn.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        parts.Add(new Part(
                            reader.GetInt32(0),
                            reader.GetString(1),
                            reader.GetDecimal(2),
                            reader.GetInt32(3)));
                    }
                }
            }
            return parts;
        }

        public Part GetById(int partId)
        {
            using (SqlConnection conn = Database.GetConnection())
            using (SqlCommand cmd = new SqlCommand(
                "SELECT PartId, Name, Price, Stock FROM dbo.Parts WHERE PartId = @PartId", conn))
            {
                cmd.Parameters.AddWithValue("@PartId", partId);
                conn.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return new Part(
                            reader.GetInt32(0),
                            reader.GetString(1),
                            reader.GetDecimal(2),
                            reader.GetInt32(3));
                    }
                }
            }
            return null;
        }

        public void Add(Part part)
        {
            if (part == null)
                throw new ArgumentNullException("part");

            using (SqlConnection conn = Database.GetConnection())
            using (SqlCommand cmd = new SqlCommand(
                "INSERT INTO dbo.Parts (Name, Price, Stock) " +
                "OUTPUT INSERTED.PartId " +
                "VALUES (@Name, @Price, @Stock)", conn))
            {
                cmd.Parameters.AddWithValue("@Name", part.Name);
                cmd.Parameters.AddWithValue("@Price", part.Price);
                cmd.Parameters.AddWithValue("@Stock", part.Stock);
                conn.Open();
                part.PartId = (int)cmd.ExecuteScalar();
            }
        }

        public void Update(Part part)
        {
            if (part == null)
                throw new ArgumentNullException("part");

            using (SqlConnection conn = Database.GetConnection())
            using (SqlCommand cmd = new SqlCommand(
                "UPDATE dbo.Parts SET Name = @Name, Price = @Price, Stock = @Stock " +
                "WHERE PartId = @PartId", conn))
            {
                cmd.Parameters.AddWithValue("@PartId", part.PartId);
                cmd.Parameters.AddWithValue("@Name", part.Name);
                cmd.Parameters.AddWithValue("@Price", part.Price);
                cmd.Parameters.AddWithValue("@Stock", part.Stock);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public void Delete(int partId)
        {
            try
            {
                using (SqlConnection conn = Database.GetConnection())
                using (SqlCommand cmd = new SqlCommand(
                    "DELETE FROM dbo.Parts WHERE PartId = @PartId", conn))
                {
                    cmd.Parameters.AddWithValue("@PartId", partId);
                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
            catch (SqlException ex)
            {
                if (ex.Number == 547)
                    throw new InvalidOperationException(
                        "This part was used in a repair order, so it cannot be deleted.");
                throw;
            }
        }

        public void UpdateStock(int partId, int newStock)
        {
            using (SqlConnection conn = Database.GetConnection())
            using (SqlCommand cmd = new SqlCommand(
                "UPDATE dbo.Parts SET Stock = @Stock WHERE PartId = @PartId", conn))
            {
                cmd.Parameters.AddWithValue("@PartId", partId);
                cmd.Parameters.AddWithValue("@Stock", newStock);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }
    }
}