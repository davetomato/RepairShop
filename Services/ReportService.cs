using System;
using System.Data;
using FixFlow.Data;
using FixFlow.Models;
using Microsoft.Data.SqlClient;

namespace FixFlow.Services
{
    public class ReportService
    {
        private static readonly DateTime AllTimeStart = new DateTime(2000, 1, 1);
        private static readonly DateTime AllTimeEnd = new DateTime(2100, 12, 31);

        public DataTable RepairsPerStatus()
        {
            return RepairsPerStatus(AllTimeStart, AllTimeEnd);
        }

        public DataTable RepairsPerStatus(DateTime from, DateTime to)
        {
            return Run(
                @"SELECT Status, COUNT(*) AS Repairs
                  FROM dbo.RepairOrders
                  WHERE DateIn >= @From AND DateIn < DATEADD(DAY, 1, @To)
                  GROUP BY Status
                  ORDER BY CASE Status
                               WHEN 'Received'   THEN 1
                               WHEN 'Diagnosing' THEN 2
                               WHEN 'Repairing'  THEN 3
                               WHEN 'Testing'    THEN 4
                               WHEN 'Completed'  THEN 5
                               WHEN 'Released'   THEN 6
                           END",
                from, to);
        }

        public DataTable IncomePerMonth()
        {
            return IncomePerMonth(AllTimeStart, AllTimeEnd);
        }

        public DataTable IncomePerMonth(DateTime from, DateTime to)
        {
            return Run(
                @"SELECT YEAR(PaymentDate)  AS PaymentYear,
                         MONTH(PaymentDate) AS PaymentMonth,
                         COUNT(*)           AS Payments,
                         SUM(Amount)        AS TotalIncome
                  FROM dbo.Payments
                  WHERE PaymentDate >= @From AND PaymentDate < DATEADD(DAY, 1, @To)
                  GROUP BY YEAR(PaymentDate), MONTH(PaymentDate)
                  ORDER BY PaymentYear, PaymentMonth",
                from, to);
        }

        public DataTable PartsUsage()
        {
            return PartsUsage(AllTimeStart, AllTimeEnd);
        }

        public DataTable PartsUsage(DateTime from, DateTime to)
        {
            return Run(
                @"SELECT p.PartId,
                         p.Name,
                         COALESCE(SUM(u.Quantity), 0)              AS QuantityUsed,
                         COALESCE(SUM(u.Quantity * u.UnitPrice), 0) AS PartsRevenue,
                         p.Stock                                   AS StockLeft
                  FROM dbo.Parts AS p
                  LEFT JOIN (SELECT op.PartId, op.Quantity, op.UnitPrice
                             FROM dbo.OrderParts AS op
                             JOIN dbo.RepairOrders AS o ON o.OrderId = op.OrderId
                             WHERE o.DateIn >= @From AND o.DateIn < DATEADD(DAY, 1, @To)) AS u
                         ON u.PartId = p.PartId
                  GROUP BY p.PartId, p.Name, p.Stock
                  ORDER BY QuantityUsed DESC, p.Name",
                from, to);
        }

        public DataTable TechnicianWorkload()
        {
            return Run(
                @"SELECT u.UserId,
                         u.Username        AS Technician,
                         COUNT(o.OrderId)  AS ActiveRepairs
                  FROM dbo.Users AS u
                  LEFT JOIN dbo.RepairOrders AS o
                         ON o.TechnicianId = u.UserId
                        AND o.Status NOT IN ('Completed', 'Released')
                  WHERE u.Role = 'Technician'
                  GROUP BY u.UserId, u.Username
                  ORDER BY ActiveRepairs DESC, Technician",
                null, null);
        }

        public DataTable UnpaidBalances()
        {
            return Run(
                @"SELECT OrderId, Customer, ServiceType, Status,
                         Labor, PartsCost, TotalCost, AmountPaid, Balance
                  FROM dbo.vw_OrderBalances
                  WHERE Balance > 0
                  ORDER BY Balance DESC",
                null, null);
        }

        private static DataTable Run(string sql, DateTime? from, DateTime? to)
        {
            AccessControl.RequireRole(UserRole.Administrator);

            if (from.HasValue && to.HasValue && from.Value > to.Value)
                throw new ArgumentException("The start date must be on or before the end date.");

            DataTable table = new DataTable();
            using (SqlConnection conn = Database.GetConnection())
            using (SqlCommand cmd = new SqlCommand(sql, conn))
            {
                if (from.HasValue)
                    cmd.Parameters.Add("@From", SqlDbType.DateTime2).Value = from.Value.Date;
                if (to.HasValue)
                    cmd.Parameters.Add("@To", SqlDbType.DateTime2).Value = to.Value.Date;

                using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                {
                    adapter.Fill(table);
                }
            }
            return table;
        }
    }
}
