using System.Collections.Generic;
using FixFlow.Data;
using FixFlow.Models;

namespace FixFlow.Services
{
    public class DeviceService
    {
        private readonly DeviceRepository _devices = new DeviceRepository();

        public List<Device> GetAll()
        {
            AccessControl.RequireLogin();
            return _devices.GetAll();
        }

        public List<Device> GetByCustomer(int customerId)
        {
            AccessControl.RequireLogin();
            return _devices.GetByCustomer(customerId);
        }

        public Device GetById(int deviceId)
        {
            AccessControl.RequireLogin();
            return _devices.GetById(deviceId);
        }

        public Device Add(int customerId, string deviceType, string brand, string model, string serialNo)
        {
            AccessControl.RequireRole(UserRole.Administrator, UserRole.Receptionist);

            Device device = new Device(0, customerId, deviceType, brand, model, serialNo);
            _devices.Add(device);
            return device;
        }

        public void Update(int deviceId, int customerId, string deviceType, string brand, string model, string serialNo)
        {
            AccessControl.RequireRole(UserRole.Administrator, UserRole.Receptionist);
            _devices.Update(new Device(deviceId, customerId, deviceType, brand, model, serialNo));
        }

        public void Delete(int deviceId)
        {
            AccessControl.RequireRole(UserRole.Administrator);
            _devices.Delete(deviceId);
        }
    }
}
