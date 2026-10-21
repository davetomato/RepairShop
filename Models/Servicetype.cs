using System;

namespace FixFlow.Models
{
    
    public abstract class ServiceType
    {
        private string _name = string.Empty;
        private decimal _rate;

        protected ServiceType(string name, decimal rate)
        {
            Name = name;
            Rate = rate;
        }

        public string Name
        {
            get { return _name; }
            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("Service name cannot be empty.");
                _name = value;
            }
        }

        
        public decimal Rate
        {
            get { return _rate; }
            set
            {
                if (value < 0)
                    throw new ArgumentException("Rate cannot be negative.");
                _rate = value;
            }
        }

        
        public abstract decimal ComputeLabor(decimal hours);

        
        public static ServiceType Create(string name, decimal rate)
        {
            switch (name)
            {
                case "Software Fix":
                    return new SoftwareFix(rate);
                case "Hardware Repair":
                    return new HardwareRepair(rate);
                case "Diagnostic Only":
                    return new DiagnosticOnly(rate);
                default:
                    throw new ArgumentException("Unknown service type: " + name);
            }
        }

        public override string ToString()
        {
            return Name;
        }
    }

    
    public class SoftwareFix : ServiceType
    {
        public SoftwareFix(decimal rate) : base("Software Fix", rate) { }

        public override decimal ComputeLabor(decimal hours)
        {
            return Rate;                      
        }
    }

    public class HardwareRepair : ServiceType
    {
        public HardwareRepair(decimal rate) : base("Hardware Repair", rate) { }

        public override decimal ComputeLabor(decimal hours)
        {
            if (hours < 0)
                throw new ArgumentException("Hours cannot be negative.");
            return hours * Rate;              
        }
    }

    public class DiagnosticOnly : ServiceType
    {
        public DiagnosticOnly(decimal rate) : base("Diagnostic Only", rate) { }

        public override decimal ComputeLabor(decimal hours)
        {
            return Rate;                      
        }
    }
}