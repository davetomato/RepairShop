using System;
using System.Collections.Generic;
using FixFlow.Models;
using Microsoft.Data.SqlClient;

namespace FixFlow.Data
{
    public class RepairOrderRepository
    {
        private const string SelectOrders =
            "SELECT o.OrderId, o.DeviceId, o.TechnicianId, o.ServiceType, sr.Rate, o.Problem, " +
            "o.Diagnosis, o.HoursWorked, o.Status, o.DateIn, o.DateOut " +
            "FROM dbo.RepairOrders AS o " +
            "JOIN dbo.ServiceRates AS sr ON sr.ServiceName = o.ServiceType ";

        private const string SelectOrderParts =
            "SELECT op.OrderId, op.PartId, op.Quantity, op.UnitPrice " +
            "FROM dbo.OrderParts AS op " +
            "JOIN dbo.RepairOrders AS o ON o.OrderId = op.OrderId ";

        private static RepairOrder Read(SqlDataReader reader)
        {
            ServiceType serviceType = ServiceType.Create(reader.GetString(3), reader.GetDecimal(4));
            RepairOrder order = new RepairOrder(
                reader.GetInt32(0),
                reader.GetInt32(1),
                serviceType,
                reader.GetString(5));

            if (!reader.IsDBNull(2))
                order.TechnicianId = reader.GetInt32(2);
            order.Diagnosis = DbHelper.GetStringOrEmpty(reader, 6);
            order.HoursWorked = reader.GetDecimal(7);
            order.LoadStatus((RepairStatus)Enum.Parse(typeof(RepairStatus), reader.GetString(8)));
            order.DateIn = reader.GetDateTime(9);
            if (!reader.IsDBNull(10))
                order.DateOut = reader.GetDateTime(10);
            return order;
        }

        private List<RepairOrder> Query(string condition, Action<SqlCommand> addParameters)
        {
            List<RepairOrder> orders = new List<RepairOrder>();
            Dictionary<int, RepairOrder> byId = new Dictionary<int, RepairOrder>();

            using (SqlConnection conn = Database.GetConnection())
            {
                conn.Open();

                using (SqlCommand cmd = new SqlCommand(
                    SelectOrders + condition + " ORDER BY o.DateIn DESC, o.OrderId DESC", conn))
                {
                    if (addParameters != null)
                        addParameters(cmd);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            RepairOrder order = Read(reader);
                            orders.Add(order);
                            byId[order.OrderId] = order;
                        }
                    }
                }

                if (orders.Count > 0)
                {
                    using (SqlCommand cmd = new SqlCommand(
                        SelectOrderParts + condition + " ORDER BY op.OrderId, op.PartId", conn))
                    {
                        if (addParameters != null)
                            addParameters(cmd);
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                RepairOrder owner;
                                if (!byId.TryGetValue(reader.GetInt32(0), out owner))
                                    continue;

                                RepairPart part = new RepairPart();
                                part.OrderId = reader.GetInt32(0);
                                part.PartId = reader.GetInt32(1);
                                part.Quantity = reader.GetInt32(2);
                                part.UnitPrice = reader.GetDecimal(3);
                                owner.AddPart(part);
                            }
                        }
                    }
                }
            }
            return orders;
        }

        private static object TechnicianValue(int technicianId)
        {
            return technicianId <= 0 ? (object)DBNull.Value : technicianId;
        }

        public List<RepairOrder> GetAll()
        {
            return Query(string.Empty, null);
        }

        public RepairOrder GetById(int orderId)
        {
            List<RepairOrder> result = Query(
                "WHERE o.OrderId = @OrderId",
                cmd => cmd.Parameters.AddWithValue("@OrderId", orderId));
            return result.Count > 0 ? result[0] : null;
        }

        public List<RepairOrder> GetByStatus(RepairStatus status)
        {
            return Query(
                "WHERE o.Status = @Status",
                cmd => cmd.Parameters.AddWithValue("@Status", status.ToString()));
        }

        public List<RepairOrder> GetByTechnician(int technicianId)
        {
            return Query(
                "WHERE o.TechnicianId = @TechnicianId",
                cmd => cmd.Parameters.AddWithValue("@TechnicianId", technicianId));
        }

        public List<RepairOrder> GetByDevice(int deviceId)
        {
            return Query(
                "WHERE o.DeviceId = @DeviceId",
                cmd => cmd.Parameters.AddWithValue("@DeviceId", deviceId));
        }

        public List<RepairOrder> GetByCustomer(int customerId)
        {
            return Query(
                "WHERE o.DeviceId IN (SELECT DeviceId FROM dbo.Devices WHERE CustomerId = @CustomerId)",
                cmd => cmd.Parameters.AddWithValue("@CustomerId", customerId));
        }

        public void Add(RepairOrder order)
        {
            if (order == null)
                throw new ArgumentNullException("order");
            if (order.ServiceType == null)
                throw new ArgumentException("Service type is required.");

            try
            {
                using (SqlConnection conn = Database.GetConnection())
                using (SqlCommand cmd = new SqlCommand(
                    "INSERT INTO dbo.RepairOrders " +
                    "(DeviceId, TechnicianId, ServiceType, Problem, Diagnosis, HoursWorked, Status, DateIn) " +
                    "OUTPUT INSERTED.OrderId " +
                    "VALUES (@DeviceId, @TechnicianId, @ServiceType, @Problem, @Diagnosis, " +
                    "@HoursWorked, @Status, @DateIn)", conn))
                {
                    cmd.Parameters.AddWithValue("@DeviceId", order.DeviceId);
                    cmd.Parameters.AddWithValue("@TechnicianId", TechnicianValue(order.TechnicianId));
                    cmd.Parameters.AddWithValue("@ServiceType", order.ServiceType.Name);
                    cmd.Parameters.AddWithValue("@Problem", order.Problem);
                    cmd.Parameters.AddWithValue("@Diagnosis", DbHelper.ToDbValue(order.Diagnosis));
                    cmd.Parameters.AddWithValue("@HoursWorked", order.HoursWorked);
                    cmd.Parameters.AddWithValue("@Status", order.Status.ToString());
                    cmd.Parameters.AddWithValue("@DateIn", order.DateIn);
                    conn.Open();
                    order.OrderId = (int)cmd.ExecuteScalar();
                }
            }
            catch (SqlException ex)
            {
                if (DbHelper.IsForeignKeyError(ex))
                    throw new InvalidOperationException(
                        "The device, technician, or service type does not exist.");
                throw;
            }
        }

        public void Update(RepairOrder order)
        {
            if (order == null)
                throw new ArgumentNullException("order");
            if (order.ServiceType == null)
                throw new ArgumentException("Service type is required.");

            try
            {
                using (SqlConnection conn = Database.GetConnection())
                using (SqlCommand cmd = new SqlCommand(
                    "UPDATE dbo.RepairOrders SET TechnicianId = @TechnicianId, ServiceType = @ServiceType, " +
                    "Problem = @Problem, Diagnosis = @Diagnosis, HoursWorked = @HoursWorked, " +
                    "Status = @Status, DateOut = @DateOut WHERE OrderId = @OrderId", conn))
                {
                    cmd.Parameters.AddWithValue("@OrderId", order.OrderId);
                    cmd.Parameters.AddWithValue("@TechnicianId", TechnicianValue(order.TechnicianId));
                    cmd.Parameters.AddWithValue("@ServiceType", order.ServiceType.Name);
                    cmd.Parameters.AddWithValue("@Problem", order.Problem);
                    cmd.Parameters.AddWithValue("@Diagnosis", DbHelper.ToDbValue(order.Diagnosis));
                    cmd.Parameters.AddWithValue("@HoursWorked", order.HoursWorked);
                    cmd.Parameters.AddWithValue("@Status", order.Status.ToString());
                    cmd.Parameters.AddWithValue("@DateOut",
                        order.DateOut.HasValue ? (object)order.DateOut.Value : DBNull.Value);
                    conn.Open();
                    if (cmd.ExecuteNonQuery() == 0)
                        throw new InvalidOperationException("Repair order not found.");
                }
            }
            catch (SqlException ex)
            {
                if (DbHelper.IsForeignKeyError(ex))
                    throw new InvalidOperationException(
                        "The technician or service type does not exist.");
                throw;
            }
        }

        public void Delete(int orderId)
        {
            try
            {
                using (SqlConnection conn = Database.GetConnection())
                using (SqlCommand cmd = new SqlCommand(
                    "DELETE FROM dbo.RepairOrders WHERE OrderId = @OrderId", conn))
                {
                    cmd.Parameters.AddWithValue("@OrderId", orderId);
                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
            catch (SqlException ex)
            {
                if (DbHelper.IsForeignKeyError(ex))
                    throw new InvalidOperationException(
                        "This repair already has parts or payments, so it cannot be deleted.");
                throw;
            }
        }
    }
}