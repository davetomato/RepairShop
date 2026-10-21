using System;
using System.Collections.Generic;
using FixFlow.Models;
using Microsoft.Data.SqlClient;

namespace FixFlow.Data
{
    public class ServiceRateRepository
    {
        public List<ServiceType> GetAll()
        {
            List<ServiceType> services = new List<ServiceType>();
            using (SqlConnection conn = Database.GetConnection())
            using (SqlCommand cmd = new SqlCommand(
                "SELECT ServiceName, Rate FROM dbo.ServiceRates ORDER BY ServiceName", conn))
            {
                conn.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                        services.Add(ServiceType.Create(reader.GetString(0), reader.GetDecimal(1)));
                }
            }
            return services;
        }

        public ServiceType GetByName(string serviceName)
        {
            using (SqlConnection conn = Database.GetConnection())
            using (SqlCommand cmd = new SqlCommand(
                "SELECT ServiceName, Rate FROM dbo.ServiceRates WHERE ServiceName = @ServiceName", conn))
            {
                cmd.Parameters.AddWithValue("@ServiceName", serviceName ?? string.Empty);
                conn.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                        return ServiceType.Create(reader.GetString(0), reader.GetDecimal(1));
                }
            }
            return null;
        }

        public void UpdateRate(string serviceName, decimal rate)
        {
            if (rate < 0)
                throw new ArgumentException("Rate cannot be negative.");

            using (SqlConnection conn = Database.GetConnection())
            using (SqlCommand cmd = new SqlCommand(
                "UPDATE dbo.ServiceRates SET Rate = @Rate WHERE ServiceName = @ServiceName", conn))
            {
                cmd.Parameters.AddWithValue("@ServiceName", serviceName ?? string.Empty);
                cmd.Parameters.AddWithValue("@Rate", rate);
                conn.Open();
                if (cmd.ExecuteNonQuery() == 0)
                    throw new InvalidOperationException("Service type not found.");
            }
        }
    }
}
