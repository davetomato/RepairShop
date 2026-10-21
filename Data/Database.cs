using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;

namespace FixFlow.Data
{
    public static class Database
    {
        private static readonly object Padlock = new object();
        private static string _connectionString;
        private static string _server;

        public static SqlConnection GetConnection()
        {
            return new SqlConnection(Resolve());
        }

        public static string ConnectedServer()
        {
            Resolve();
            return _server;
        }

        private static string Resolve()
        {
            lock (Padlock)
            {
                if (_connectionString != null)
                    return _connectionString;

                List<string> servers = new List<string>();
                string fromEnvironment = Environment.GetEnvironmentVariable("FIXFLOW_SERVER");
                if (!string.IsNullOrWhiteSpace(fromEnvironment))
                    servers.Add(fromEnvironment.Trim());
                servers.Add(@"(localdb)\MSSQLLocalDB");
                servers.Add(@"(localdb)\ProjectModels");

                string lastError = string.Empty;
                foreach (string server in servers)
                {
                    string candidate = "Server=" + server +
                        ";Database=FixFlowDB;Trusted_Connection=True;TrustServerCertificate=True;Connect Timeout=30;";
                    try
                    {
                        using (SqlConnection test = new SqlConnection(candidate))
                        {
                            test.Open();
                        }
                        _connectionString = candidate;
                        _server = server;
                        return candidate;
                    }
                    catch (SqlException ex)
                    {
                        lastError = ex.Message;
                    }
                }

                throw new InvalidOperationException(
                    "Cannot connect to the FixFlowDB database. Tried: " + string.Join(", ", servers) +
                    ". Run Database\\create_tables.sql first. Last error: " + lastError);
            }
        }
    }
}