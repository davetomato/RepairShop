using System;
using Microsoft.Data.SqlClient;

namespace FixFlow.Data
{
    internal static class DbHelper
    {
        public static string GetStringOrEmpty(SqlDataReader reader, int index)
        {
            return reader.IsDBNull(index) ? string.Empty : reader.GetString(index);
        }

        public static object ToDbValue(string value)
        {
            return string.IsNullOrWhiteSpace(value) ? (object)DBNull.Value : value;
        }

        public static bool IsForeignKeyError(SqlException ex)
        {
            return ex.Number == 547;
        }

        public static bool IsDuplicateError(SqlException ex)
        {
            return ex.Number == 2627 || ex.Number == 2601;
        }
    }
}
