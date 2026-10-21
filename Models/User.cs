using System;
using System.Collections.Generic;

namespace FixFlow.Models
{
   
    public abstract class User
    {
        private int _userId;
        private string _username = string.Empty;
        private string _passwordHash = string.Empty;

        protected User() { }

        protected User(int userId, string username, string passwordHash)
        {
            UserId = userId;
            Username = username;
            PasswordHash = passwordHash;
        }

        public int UserId
        {
            get { return _userId; }
            set { _userId = value; }
        }

        public string Username
        {
            get { return _username; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("Username cannot be empty.");
                _username = value.Trim();
            }
        }

        
        public string PasswordHash
        {
            get { return _passwordHash; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("Password hash cannot be empty.");
                _passwordHash = value;
            }
        }

       
        public abstract UserRole Role { get; }
        public abstract IList<string> GetAllowedMenus();

        
        public static User Create(UserRole role, int userId, string username, string passwordHash)
        {
            switch (role)
            {
                case UserRole.Administrator:
                    return new Administrator(userId, username, passwordHash);
                case UserRole.Receptionist:
                    return new Receptionist(userId, username, passwordHash);
                case UserRole.Technician:
                    return new Technician(userId, username, passwordHash);
                default:
                    throw new ArgumentException("Unknown role.");
            }
        }

        public override string ToString()
        {
            return Username + " (" + Role + ")";
        }
    }

    
    public class Administrator : User
    {
        public Administrator() { }
        public Administrator(int userId, string username, string passwordHash)
            : base(userId, username, passwordHash) { }

        public override UserRole Role
        {
            get { return UserRole.Administrator; }
        }

        public override IList<string> GetAllowedMenus()
        {
            return new List<string>
            {
                "Repair Board", "Customers", "Devices", "Parts",
                "Payments", "Reports", "Users"
            };
        }
    }

    public class Receptionist : User
    {
        public Receptionist() { }
        public Receptionist(int userId, string username, string passwordHash)
            : base(userId, username, passwordHash) { }

        public override UserRole Role
        {
            get { return UserRole.Receptionist; }
        }

        public override IList<string> GetAllowedMenus()
        {
            return new List<string> { "Repair Board", "Customers", "Devices", "Payments" };
        }
    }

    public class Technician : User
    {
        public Technician() { }
        public Technician(int userId, string username, string passwordHash)
            : base(userId, username, passwordHash) { }

        public override UserRole Role
        {
            get { return UserRole.Technician; }
        }

        public override IList<string> GetAllowedMenus()
        {
            return new List<string> { "Repair Board", "Parts" };
        }
    }
}