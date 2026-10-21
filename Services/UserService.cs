using System;
using System.Collections.Generic;
using FixFlow.Data;
using FixFlow.Models;

namespace FixFlow.Services
{
    public class UserService
    {
        private readonly UserRepository _users = new UserRepository();

        public List<User> GetAll()
        {
            AccessControl.RequireRole(UserRole.Administrator);
            return _users.GetAll();
        }

        public List<User> GetTechnicians()
        {
            AccessControl.RequireRole(UserRole.Administrator, UserRole.Receptionist);
            return _users.GetByRole(UserRole.Technician);
        }

        public void Update(int userId, string username, UserRole role)
        {
            AccessControl.RequireRole(UserRole.Administrator);

            User existing = _users.GetById(userId);
            if (existing == null)
                throw new InvalidOperationException("User not found.");

            if (existing.Role == UserRole.Administrator && role != UserRole.Administrator
                && _users.GetByRole(UserRole.Administrator).Count <= 1)
                throw new InvalidOperationException(
                    "The last administrator cannot be changed to another role.");

            User updated = User.Create(role, userId, username, existing.PasswordHash);
            _users.UpdateDetails(updated);

            if (Session.CurrentUser.UserId == userId)
                Session.Start(updated);
        }

        public void Delete(int userId)
        {
            AccessControl.RequireRole(UserRole.Administrator);

            if (Session.CurrentUser.UserId == userId)
                throw new InvalidOperationException("You cannot delete your own account.");

            User existing = _users.GetById(userId);
            if (existing == null)
                throw new InvalidOperationException("User not found.");

            if (existing.Role == UserRole.Administrator
                && _users.GetByRole(UserRole.Administrator).Count <= 1)
                throw new InvalidOperationException("The last administrator cannot be deleted.");

            _users.Delete(userId);
        }
    }
}
