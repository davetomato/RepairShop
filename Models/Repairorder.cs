using System;
using System.Collections.Generic;

namespace FixFlow.Models
{
    
    public class RepairOrder
    {
        private int _orderId;
        private int _deviceId;
        private int _technicianId;                 
        private ServiceType _serviceType;
        private string _problem = string.Empty;
        private string _diagnosis = string.Empty;
        private decimal _hoursWorked;
        private RepairStatus _status = RepairStatus.Received;
        private DateTime _dateIn = DateTime.Now;
        private DateTime? _dateOut;
        private readonly List<RepairPart> _parts = new List<RepairPart>();

        public RepairOrder() { }

        public RepairOrder(int orderId, int deviceId, ServiceType serviceType, string problem)
        {
            OrderId = orderId;
            DeviceId = deviceId;
            ServiceType = serviceType;
            Problem = problem;
        }

        public int OrderId
        {
            get { return _orderId; }
            set { _orderId = value; }
        }

        public int DeviceId
        {
            get { return _deviceId; }
            set { _deviceId = value; }
        }

        
        public int TechnicianId
        {
            get { return _technicianId; }
            set { _technicianId = value; }
        }

        public ServiceType ServiceType
        {
            get { return _serviceType; }
            set
            {
                if (value == null)
                    throw new ArgumentException("Service type is required.");
                _serviceType = value;
            }
        }

        public string Problem
        {
            get { return _problem; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("Please describe the problem.");
                _problem = value.Trim();
            }
        }

        public string Diagnosis
        {
            get { return _diagnosis; }
            set { _diagnosis = (value == null) ? string.Empty : value.Trim(); }
        }

        public decimal HoursWorked
        {
            get { return _hoursWorked; }
            set
            {
                if (value < 0)
                    throw new ArgumentException("Hours cannot be negative.");
                _hoursWorked = value;
            }
        }

        
        public RepairStatus Status
        {
            get { return _status; }
        }

        
        public void LoadStatus(RepairStatus status)
        {
            _status = status;
        }

        public DateTime DateIn
        {
            get { return _dateIn; }
            set { _dateIn = value; }
        }

        public DateTime? DateOut
        {
            get { return _dateOut; }
            set { _dateOut = value; }
        }

        public IList<RepairPart> Parts
        {
            get { return _parts; }
        }

        public void AddPart(RepairPart part)
        {
            if (part == null)
                throw new ArgumentNullException("part");
            _parts.Add(part);
        }

        
        public decimal ComputeLaborFee()
        {
            if (_serviceType == null)
                return 0m;
            return _serviceType.ComputeLabor(_hoursWorked);
        }

        public decimal PartsCost
        {
            get
            {
                decimal total = 0m;
                foreach (RepairPart p in _parts)
                    total += p.LineTotal;
                return total;
            }
        }

        public decimal TotalCost
        {
            get { return ComputeLaborFee() + PartsCost; }
        }

        
        public bool CanMoveTo(RepairStatus next)
        {
            int difference = Math.Abs((int)next - (int)_status);
            return difference == 1;
        }

        public void MoveTo(RepairStatus next)
        {
            if (!CanMoveTo(next))
                throw new InvalidOperationException(
                    "Cannot move a repair from " + _status + " to " + next + ".");
            _status = next;
            if (next == RepairStatus.Released)
                _dateOut = DateTime.Now;
        }
    }
}