using System;
using FixFlow.Models;

namespace FixFlow.Services
{
    public static class Session
    {
        public static User CurrentUser { get; private set; }

        public static bool IsLoggedIn
        {
            get { return CurrentUser != null; }
        }

        public static void Start(User user)
        {
            if (user == null)
                throw new ArgumentNullException("user");
            CurrentUser = user;
        }

        public static void End()
        {
            CurrentUser = null;
        }

        public static bool IsInRole(UserRole role)
        {
            return CurrentUser != null && CurrentUser.Role == role;
        }
    }
}
