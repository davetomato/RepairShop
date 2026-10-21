using System;
using FixFlow.Data;
using FixFlow.Models;

namespace FixFlow.Services
{
    public class AuthService
    {
        private const string PlaceholderHash = "PLACEHOLDER_HASH";
        private const int MinPasswordLength = 6;

        private readonly UserRepository _users = new UserRepository();

        public User Login(string username, string password)
        {
            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrEmpty(password))
                return null;

            User user = _users.GetByUsername(username.Trim());
            if (user == null || !PasswordHasher.Verify(password, user.PasswordHash))
                return null;

            Session.Start(user);
            return user;
        }

        public void Logout()
        {
            Session.End();
        }

        public User CreateUser(UserRole role, string username, string password)
        {
            AccessControl.RequireRole(UserRole.Administrator);
            ValidatePassword(password);

            User user = User.Create(role, 0, username, PasswordHasher.Hash(password));
            _users.Add(user);
            return user;
        }

        public void ChangePassword(int userId, string currentPassword, string newPassword)
        {
            AccessControl.RequireLogin();
            if (Session.CurrentUser.UserId != userId)
                throw new InvalidOperationException("You can only change your own password.");

            ValidatePassword(newPassword);

            User user = _users.GetById(userId);
            if (user == null)
                throw new InvalidOperationException("User not found.");
            if (!PasswordHasher.Verify(currentPassword, user.PasswordHash))
                throw new InvalidOperationException("The current password is incorrect.");

            _users.UpdatePassword(userId, PasswordHasher.Hash(newPassword));
        }

        public void ResetPassword(int userId, string newPassword)
        {
            AccessControl.RequireRole(UserRole.Administrator);
            ValidatePassword(newPassword);

            if (_users.GetById(userId) == null)
                throw new InvalidOperationException("User not found.");

            _users.UpdatePassword(userId, PasswordHasher.Hash(newPassword));
        }

        public int ReplacePlaceholderPasswords(string password)
        {
            ValidatePassword(password);

            int changed = 0;
            foreach (User user in _users.GetAll())
            {
                if (user.PasswordHash == PlaceholderHash)
                {
                    _users.UpdatePassword(user.UserId, PasswordHasher.Hash(password));
                    changed++;
                }
            }
            return changed;
        }

        private static void ValidatePassword(string password)
        {
            if (string.IsNullOrEmpty(password) || password.Length < MinPasswordLength)
                throw new ArgumentException(
                    "Password must be at least " + MinPasswordLength + " characters.");
        }
    }
}