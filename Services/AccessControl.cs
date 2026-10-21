using System;
using FixFlow.Models;

namespace FixFlow.Services
{
    public static class AccessControl
    {
        public static void RequireLogin()
        {
            if (!Session.IsLoggedIn)
                throw new InvalidOperationException("Please log in first.");
        }

        public static void RequireRole(params UserRole[] allowedRoles)
        {
            RequireLogin();

            foreach (UserRole role in allowedRoles)
            {
                if (Session.CurrentUser.Role == role)
                    return;
            }

            throw new InvalidOperationException(
                "Your role (" + Session.CurrentUser.Role + ") is not allowed to do this.");
        }
    }
}
