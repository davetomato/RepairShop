using System;
using System.Collections.Generic;
using FixFlow.Data;
using FixFlow.Models;

namespace FixFlow.Services
{
    public class InventoryService
    {
        private readonly PartRepository _parts = new PartRepository();
        private readonly RepairOrderRepository _orders = new RepairOrderRepository();
        private readonly RepairPartRepository _repairParts = new RepairPartRepository();

        public void UsePart(int orderId, int partId, int quantity)
        {
            AccessControl.RequireRole(UserRole.Administrator, UserRole.Technician);
            LoadEditableOrder(orderId);
            _repairParts.UsePart(orderId, partId, quantity);
        }

        public void ReturnPart(int orderId, int partId)
        {
            AccessControl.RequireRole(UserRole.Administrator, UserRole.Technician);
            LoadEditableOrder(orderId);
            _repairParts.ReturnPart(orderId, partId);
        }

        public void Restock(int partId, int quantity)
        {
            AccessControl.RequireRole(UserRole.Administrator);

            if (quantity <= 0)
                throw new ArgumentException("Quantity must be greater than zero.");

            Part part = _parts.GetById(partId);
            if (part == null)
                throw new InvalidOperationException("Part not found.");

            part.AddStock(quantity);
            _parts.UpdateStock(part.PartId, part.Stock);
        }

        public List<Part> GetLowStock(int threshold)
        {
            AccessControl.RequireLogin();

            List<Part> low = new List<Part>();
            foreach (Part part in _parts.GetAll())
            {
                if (part.Stock <= threshold)
                    low.Add(part);
            }
            return low;
        }

        private RepairOrder LoadEditableOrder(int orderId)
        {
            RepairOrder order = _orders.GetById(orderId);
            if (order == null)
                throw new InvalidOperationException("Repair order not found.");
            if (order.Status == RepairStatus.Released)
                throw new InvalidOperationException("A released repair cannot be changed.");

            User user = Session.CurrentUser;
            if (user.Role == UserRole.Technician && order.TechnicianId != user.UserId)
                throw new InvalidOperationException("You can only change repairs assigned to you.");

            return order;
        }
    }
}