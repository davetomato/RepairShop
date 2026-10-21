using System;
using System.Collections.Generic;
using FixFlow.Data;
using FixFlow.Models;

namespace FixFlow.Services
{
    public class PartService
    {
        private readonly PartRepository _parts = new PartRepository();

        public List<Part> GetAll()
        {
            AccessControl.RequireLogin();
            return _parts.GetAll();
        }

        public Part GetById(int partId)
        {
            AccessControl.RequireLogin();
            return _parts.GetById(partId);
        }

        public Part Add(string name, decimal price, int stock)
        {
            AccessControl.RequireRole(UserRole.Administrator);

            Part part = new Part(0, name, price, stock);
            _parts.Add(part);
            return part;
        }

        public void Update(int partId, string name, decimal price, int stock)
        {
            AccessControl.RequireRole(UserRole.Administrator);

            if (_parts.GetById(partId) == null)
                throw new InvalidOperationException("Part not found.");

            _parts.Update(new Part(partId, name, price, stock));
        }

        public void Delete(int partId)
        {
            AccessControl.RequireRole(UserRole.Administrator);
            _parts.Delete(partId);
        }
    }
}
