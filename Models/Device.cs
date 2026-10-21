using System;

namespace FixFlow.Models
{
    
    public class Device
    {
        private int _deviceId;
        private int _customerId;
        private string _deviceType = string.Empty;
        private string _brand = string.Empty;
        private string _model = string.Empty;
        private string _serialNo = string.Empty;

        public Device() { }

        public Device(int deviceId, int customerId, string deviceType,
                      string brand, string model, string serialNo)
        {
            DeviceId = deviceId;
            CustomerId = customerId;
            DeviceType = deviceType;
            Brand = brand;
            Model = model;
            SerialNo = serialNo;
        }

        public int DeviceId
        {
            get { return _deviceId; }
            set { _deviceId = value; }
        }

        
        public int CustomerId
        {
            get { return _customerId; }
            set { _customerId = value; }
        }

        public string DeviceType
        {
            get { return _deviceType; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("Device type cannot be empty.");
                _deviceType = value.Trim();
            }
        }

        public string Brand
        {
            get { return _brand; }
            set { _brand = (value == null) ? string.Empty : value.Trim(); }
        }

        public string Model
        {
            get { return _model; }
            set { _model = (value == null) ? string.Empty : value.Trim(); }
        }

        public string SerialNo
        {
            get { return _serialNo; }
            set { _serialNo = (value == null) ? string.Empty : value.Trim(); }
        }

        public override string ToString()
        {
            return (Brand + " " + Model).Trim();
        }
    }
}