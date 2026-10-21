using System;
using System.Collections.Generic;
using FixFlow.Models;
using Microsoft.Data.SqlClient;

namespace FixFlow.Data
{
    public class DeviceRepository
    {
        private const string SelectDevices =
            "SELECT DeviceId, CustomerId, DeviceType, Brand, Model, SerialNo FROM dbo.Devices ";

        private static Device Read(SqlDataReader reader)
        {
            return new Device(
                reader.GetInt32(0),
                reader.GetInt32(1),
                reader.GetString(2),
                DbHelper.GetStringOrEmpty(reader, 3),
                DbHelper.GetStringOrEmpty(reader, 4),
                DbHelper.GetStringOrEmpty(reader, 5));
        }

        private List<Device> Query(string tail, Action<SqlCommand> addParameters)
        {
            List<Device> devices = new List<Device>();
            using (SqlConnection conn = Database.GetConnection())
            using (SqlCommand cmd = new SqlCommand(SelectDevices + tail, conn))
            {
                if (addParameters != null)
                    addParameters(cmd);
                conn.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                        devices.Add(Read(reader));
                }
            }
            return devices;
        }

        public List<Device> GetAll()
        {
            return Query("ORDER BY DeviceId", null);
        }

        public List<Device> GetByCustomer(int customerId)
        {
            return Query(
                "WHERE CustomerId = @CustomerId ORDER BY DeviceId",
                cmd => cmd.Parameters.AddWithValue("@CustomerId", customerId));
        }

        public Device GetById(int deviceId)
        {
            List<Device> result = Query(
                "WHERE DeviceId = @DeviceId",
                cmd => cmd.Parameters.AddWithValue("@DeviceId", deviceId));
            return result.Count > 0 ? result[0] : null;
        }

        public void Add(Device device)
        {
            if (device == null)
                throw new ArgumentNullException("device");

            try
            {
                using (SqlConnection conn = Database.GetConnection())
                using (SqlCommand cmd = new SqlCommand(
                    "INSERT INTO dbo.Devices (CustomerId, DeviceType, Brand, Model, SerialNo) " +
                    "OUTPUT INSERTED.DeviceId " +
                    "VALUES (@CustomerId, @DeviceType, @Brand, @Model, @SerialNo)", conn))
                {
                    cmd.Parameters.AddWithValue("@CustomerId", device.CustomerId);
                    cmd.Parameters.AddWithValue("@DeviceType", device.DeviceType);
                    cmd.Parameters.AddWithValue("@Brand", DbHelper.ToDbValue(device.Brand));
                    cmd.Parameters.AddWithValue("@Model", DbHelper.ToDbValue(device.Model));
                    cmd.Parameters.AddWithValue("@SerialNo", DbHelper.ToDbValue(device.SerialNo));
                    conn.Open();
                    device.DeviceId = (int)cmd.ExecuteScalar();
                }
            }
            catch (SqlException ex)
            {
                if (DbHelper.IsForeignKeyError(ex))
                    throw new InvalidOperationException("The customer does not exist.");
                throw;
            }
        }

        public void Update(Device device)
        {
            if (device == null)
                throw new ArgumentNullException("device");

            using (SqlConnection conn = Database.GetConnection())
            using (SqlCommand cmd = new SqlCommand(
                "UPDATE dbo.Devices SET DeviceType = @DeviceType, Brand = @Brand, " +
                "Model = @Model, SerialNo = @SerialNo WHERE DeviceId = @DeviceId", conn))
            {
                cmd.Parameters.AddWithValue("@DeviceId", device.DeviceId);
                cmd.Parameters.AddWithValue("@DeviceType", device.DeviceType);
                cmd.Parameters.AddWithValue("@Brand", DbHelper.ToDbValue(device.Brand));
                cmd.Parameters.AddWithValue("@Model", DbHelper.ToDbValue(device.Model));
                cmd.Parameters.AddWithValue("@SerialNo", DbHelper.ToDbValue(device.SerialNo));
                conn.Open();
                if (cmd.ExecuteNonQuery() == 0)
                    throw new InvalidOperationException("Device not found.");
            }
        }

        public void Delete(int deviceId)
        {
            try
            {
                using (SqlConnection conn = Database.GetConnection())
                using (SqlCommand cmd = new SqlCommand(
                    "DELETE FROM dbo.Devices WHERE DeviceId = @DeviceId", conn))
                {
                    cmd.Parameters.AddWithValue("@DeviceId", deviceId);
                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
            catch (SqlException ex)
            {
                if (DbHelper.IsForeignKeyError(ex))
                    throw new InvalidOperationException(
                        "This device has repair orders, so it cannot be deleted.");
                throw;
            }
        }
    }
}
