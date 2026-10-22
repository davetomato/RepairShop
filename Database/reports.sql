-- =====================================================================
-- FixFlow: report queries (SQL Server / T-SQL)
-- Run these in a query window connected to FixFlowDB:
-- right-click FixFlowDB in SQL Server Object Explorer > New Query.
-- Highlight ONE query at a time and press Ctrl+Shift+E, or run them all
-- and scroll through the five result tables.
-- Report 5 uses the view dbo.vw_OrderBalances created by create_tables.sql.
-- =====================================================================

USE FixFlowDB;
GO

-- REPORT 1: Repairs per status (GROUP BY, COUNT, ORDER BY with CASE)
SELECT Status, COUNT(*) AS Repairs
FROM dbo.RepairOrders
GROUP BY Status
ORDER BY CASE Status
            WHEN 'Received'   THEN 1
            WHEN 'Diagnosing' THEN 2
            WHEN 'Repairing'  THEN 3
            WHEN 'Testing'    THEN 4
            WHEN 'Completed'  THEN 5
            WHEN 'Released'   THEN 6
         END;
GO

-- REPORT 2: Income per month (GROUP BY on date parts, SUM)
SELECT YEAR(PaymentDate)  AS PaymentYear,
       MONTH(PaymentDate) AS PaymentMonth,
       COUNT(*)           AS Payments,
       SUM(Amount)        AS TotalIncome
FROM dbo.Payments
GROUP BY YEAR(PaymentDate), MONTH(PaymentDate)
ORDER BY PaymentYear, PaymentMonth;
GO

-- REPORT 3: Parts usage and current stock (LEFT JOIN, SUM, GROUP BY)
SELECT p.PartId,
       p.Name,
       COALESCE(SUM(op.Quantity), 0)                AS QuantityUsed,
       COALESCE(SUM(op.Quantity * op.UnitPrice), 0) AS PartsRevenue,
       p.Stock                                      AS StockLeft
FROM dbo.Parts AS p
LEFT JOIN dbo.OrderParts AS op ON op.PartId = p.PartId
GROUP BY p.PartId, p.Name, p.Stock
ORDER BY QuantityUsed DESC, p.Name;
GO

-- REPORT 4: Technician workload (LEFT JOIN with a condition, COUNT, NOT IN)
SELECT u.UserId,
       u.Username        AS Technician,
       COUNT(o.OrderId)  AS ActiveRepairs
FROM dbo.Users AS u
LEFT JOIN dbo.RepairOrders AS o
       ON o.TechnicianId = u.UserId
      AND o.Status NOT IN ('Completed', 'Released')
WHERE u.Role = 'Technician'
GROUP BY u.UserId, u.Username
ORDER BY ActiveRepairs DESC, Technician;
GO

-- REPORT 5: Unpaid balances per repair order (reads the view)
SELECT OrderId, Customer, ServiceType, Status,
       Labor, PartsCost, TotalCost, AmountPaid, Balance
FROM dbo.vw_OrderBalances
WHERE Balance > 0
ORDER BY Balance DESC;
GO