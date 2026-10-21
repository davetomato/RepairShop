using System.Collections.Generic;
using FixFlow.Data;
using FixFlow.Models;

namespace FixFlow.Services
{
    public class ServiceRateService
    {
        private readonly ServiceRateRepository _rates = new ServiceRateRepository();

        public List<ServiceType> GetAll()
        {
            AccessControl.RequireLogin();
            return _rates.GetAll();
        }

        public void UpdateRate(string serviceName, decimal rate)
        {
            AccessControl.RequireRole(UserRole.Administrator);
            _rates.UpdateRate(serviceName, rate);
        }
    }
}
