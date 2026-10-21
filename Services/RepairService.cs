using System;
using System.Collections.Generic;
using FixFlow.Data;
using FixFlow.Models;

namespace FixFlow.Services
{
    public class RepairService
    {
        private readonly RepairOrderRepository _orders = new RepairOrderRepository();
        private readonly DeviceRepository _devices = new DeviceRepository();
        private readonly ServiceRateRepository _rates = new ServiceRateRepository();
        private readonly UserRepository _users = new UserRepository();
        private readonly BillingService _billing = new BillingService();

        public RepairOrder CreateOrder(int deviceId, string serviceName, string problem)
        {
            AccessControl.RequireRole(UserRole.Administrator, UserRole.Receptionist);

            if (_devices.GetById(deviceId) == null)
                throw new InvalidOperationException("Device not found.");

            ServiceType serviceType = _rates.GetByName(serviceName);
            if (serviceType == null)
                throw new InvalidOperationException("Service type not found.");

            RepairOrder order = new RepairOrder(0, deviceId, serviceType, problem);
            _orders.Add(order);
            return order;
        }

        public RepairOrder GetOrder(int orderId)
        {
            AccessControl.RequireLogin();
            return _orders.GetById(orderId);
        }

        public List<RepairOrder> GetOrdersForTechnician(int technicianId)
        {
            AccessControl.RequireLogin();

            User user = Session.CurrentUser;
            if (user.Role == UserRole.Technician && user.UserId != technicianId)
                throw new InvalidOperationException("You can only view your own repairs.");

            return _orders.GetByTechnician(technicianId);
        }

        public List<RepairOrder> GetHistoryForCustomer(int customerId)
        {
            AccessControl.RequireRole(UserRole.Administrator, UserRole.Receptionist);
            return _orders.GetByCustomer(customerId);
        }

        public List<RepairOrder> GetHistoryForDevice(int deviceId)
        {
            AccessControl.RequireRole(UserRole.Administrator, UserRole.Receptionist);
            return _orders.GetByDevice(deviceId);
        }

        public Dictionary<RepairStatus, List<RepairOrder>> GetBoard()
        {
            AccessControl.RequireLogin();

            Dictionary<RepairStatus, List<RepairOrder>> board =
                new Dictionary<RepairStatus, List<RepairOrder>>();

            foreach (RepairStatus status in Enum.GetValues(typeof(RepairStatus)))
                board[status] = new List<RepairOrder>();

            foreach (RepairOrder order in _orders.GetAll())
                board[order.Status].Add(order);

            return board;
        }

        public void MoveOrder(int orderId, RepairStatus newStatus)
        {
            AccessControl.RequireLogin();

            RepairOrder order = Load(orderId);
            RepairStatus previous = order.Status;
            User user = Session.CurrentUser;

            if (user.Role == UserRole.Technician)
            {
                if (order.TechnicianId != user.UserId)
                    throw new InvalidOperationException("You can only update repairs assigned to you.");
                if (newStatus == RepairStatus.Released || previous == RepairStatus.Released)
                    throw new InvalidOperationException(
                        "Only the receptionist or administrator can release a device.");
            }

            if (!order.CanMoveTo(newStatus))
                throw new InvalidOperationException(
                    "Cannot move a repair from " + previous + " to " + newStatus + ".");

            if (newStatus == RepairStatus.Repairing && order.TechnicianId <= 0)
                throw new InvalidOperationException("Assign a technician before the repair starts.");

            if (newStatus == RepairStatus.Released && _billing.GetBalance(order) > 0)
                throw new InvalidOperationException(
                    "The balance must be fully paid before the device is released.");

            order.MoveTo(newStatus);
            if (previous == RepairStatus.Released)
                order.DateOut = null;

            _orders.Update(order);
        }

        public void AssignTechnician(int orderId, int technicianId)
        {
            AccessControl.RequireRole(UserRole.Administrator, UserRole.Receptionist);

            User technician = _users.GetById(technicianId);
            if (technician == null || technician.Role != UserRole.Technician)
                throw new InvalidOperationException("Choose a user with the Technician role.");

            RepairOrder order = LoadEditable(orderId);
            order.TechnicianId = technicianId;
            _orders.Update(order);
        }

        public void UpdateDetails(int orderId, string diagnosis, decimal hoursWorked)
        {
            AccessControl.RequireRole(UserRole.Administrator, UserRole.Technician);

            RepairOrder order = LoadEditable(orderId);
            User user = Session.CurrentUser;
            if (user.Role == UserRole.Technician && order.TechnicianId != user.UserId)
                throw new InvalidOperationException("You can only update repairs assigned to you.");

            order.Diagnosis = diagnosis;
            order.HoursWorked = hoursWorked;
            _orders.Update(order);
        }

        private RepairOrder Load(int orderId)
        {
            RepairOrder order = _orders.GetById(orderId);
            if (order == null)
                throw new InvalidOperationException("Repair order not found.");
            return order;
        }

        private RepairOrder LoadEditable(int orderId)
        {
            RepairOrder order = Load(orderId);
            if (order.Status == RepairStatus.Released)
                throw new InvalidOperationException("A released repair cannot be changed.");
            return order;
        }
    }
}