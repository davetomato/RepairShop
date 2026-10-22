-- =====================================================================
-- FixFlow: Computer Repair Shop Service Tracking and Billing System
-- Database script for SQL Server (LocalDB / Express)
-- Safe to run again: it drops and rebuilds the tables each time.
-- =====================================================================

IF DB_ID('FixFlowDB') IS NULL
    CREATE DATABASE FixFlowDB;
GO

USE FixFlowDB;
GO

-- ---------- Drop old objects (view first, then tables, children first) ----------
IF OBJECT_ID('dbo.vw_OrderBalances', 'V') IS NOT NULL DROP VIEW dbo.vw_OrderBalances;
IF OBJECT_ID('dbo.Payments', 'U')     IS NOT NULL DROP TABLE dbo.Payments;
IF OBJECT_ID('dbo.OrderParts', 'U')   IS NOT NULL DROP TABLE dbo.OrderParts;
IF OBJECT_ID('dbo.RepairOrders', 'U') IS NOT NULL DROP TABLE dbo.RepairOrders;
IF OBJECT_ID('dbo.Devices', 'U')      IS NOT NULL DROP TABLE dbo.Devices;
IF OBJECT_ID('dbo.Customers', 'U')    IS NOT NULL DROP TABLE dbo.Customers;
IF OBJECT_ID('dbo.Parts', 'U')        IS NOT NULL DROP TABLE dbo.Parts;
IF OBJECT_ID('dbo.ServiceRates', 'U') IS NOT NULL DROP TABLE dbo.ServiceRates;
IF OBJECT_ID('dbo.Users', 'U')        IS NOT NULL DROP TABLE dbo.Users;
GO

-- ---------- 1. Users ----------
CREATE TABLE dbo.Users (
    UserId       INT IDENTITY(1,1) PRIMARY KEY,
    Username     NVARCHAR(50)  NOT NULL UNIQUE,
    PasswordHash NVARCHAR(200) NOT NULL,
    Role         NVARCHAR(20)  NOT NULL
        CONSTRAINT CK_Users_Role CHECK (Role IN ('Administrator', 'Receptionist', 'Technician'))
);
GO

-- ---------- 2. Customers ----------
CREATE TABLE dbo.Customers (
    CustomerId INT IDENTITY(1,1) PRIMARY KEY,
    Name       NVARCHAR(100) NOT NULL,
    Contact    NVARCHAR(30)  NOT NULL,
    Address    NVARCHAR(200) NULL
);
GO

-- ---------- 3. Devices (each belongs to one customer) ----------
CREATE TABLE dbo.Devices (
    DeviceId   INT IDENTITY(1,1) PRIMARY KEY,
    CustomerId INT NOT NULL
        CONSTRAINT FK_Devices_Customers REFERENCES dbo.Customers(CustomerId),
    DeviceType NVARCHAR(50) NOT NULL,
    Brand      NVARCHAR(50) NULL,
    Model      NVARCHAR(50) NULL,
    SerialNo   NVARCHAR(50) NULL
);
GO

-- ---------- 4. ServiceRates (the admin can edit these) ----------
-- ServiceName must match the ServiceType names used in the C# code.
CREATE TABLE dbo.ServiceRates (
    ServiceName NVARCHAR(50)  NOT NULL PRIMARY KEY,
    Rate        DECIMAL(10,2) NOT NULL
        CONSTRAINT CK_ServiceRates_Rate CHECK (Rate >= 0)
);
GO

-- ---------- 5. Parts ----------
CREATE TABLE dbo.Parts (
    PartId INT IDENTITY(1,1) PRIMARY KEY,
    Name   NVARCHAR(100) NOT NULL UNIQUE,
    Price  DECIMAL(10,2) NOT NULL
        CONSTRAINT CK_Parts_Price CHECK (Price >= 0),
    Stock  INT NOT NULL
        CONSTRAINT CK_Parts_Stock CHECK (Stock >= 0)
);
GO

-- ---------- 6. RepairOrders ----------
CREATE TABLE dbo.RepairOrders (
    OrderId      INT IDENTITY(1,1) PRIMARY KEY,
    DeviceId     INT NOT NULL
        CONSTRAINT FK_RepairOrders_Devices REFERENCES dbo.Devices(DeviceId),
    TechnicianId INT NULL
        CONSTRAINT FK_RepairOrders_Users REFERENCES dbo.Users(UserId),
    ServiceType  NVARCHAR(50) NOT NULL
        CONSTRAINT FK_RepairOrders_ServiceRates REFERENCES dbo.ServiceRates(ServiceName),
    Problem      NVARCHAR(500) NOT NULL,
    Diagnosis    NVARCHAR(500) NULL,
    HoursWorked  DECIMAL(6,2) NOT NULL DEFAULT 0
        CONSTRAINT CK_RepairOrders_Hours CHECK (HoursWorked >= 0),
    Status       NVARCHAR(20) NOT NULL DEFAULT 'Received'
        CONSTRAINT CK_RepairOrders_Status CHECK (Status IN
            ('Received', 'Diagnosing', 'Repairing', 'Testing', 'Completed', 'Released')),
    DateIn       DATETIME2 NOT NULL DEFAULT SYSDATETIME(),
    DateOut      DATETIME2 NULL
);
GO

-- ---------- 7. OrderParts (parts used in a repair; the RepairPart class) ----------
CREATE TABLE dbo.OrderParts (
    OrderId   INT NOT NULL
        CONSTRAINT FK_OrderParts_RepairOrders REFERENCES dbo.RepairOrders(OrderId),
    PartId    INT NOT NULL
        CONSTRAINT FK_OrderParts_Parts REFERENCES dbo.Parts(PartId),
    Quantity  INT NOT NULL
        CONSTRAINT CK_OrderParts_Quantity CHECK (Quantity > 0),
    UnitPrice DECIMAL(10,2) NOT NULL
        CONSTRAINT CK_OrderParts_UnitPrice CHECK (UnitPrice >= 0),
    CONSTRAINT PK_OrderParts PRIMARY KEY (OrderId, PartId)
);
GO

-- ---------- 8. Payments ----------
CREATE TABLE dbo.Payments (
    PaymentId   INT IDENTITY(1,1) PRIMARY KEY,
    OrderId     INT NOT NULL
        CONSTRAINT FK_Payments_RepairOrders REFERENCES dbo.RepairOrders(OrderId),
    Amount      DECIMAL(10,2) NOT NULL
        CONSTRAINT CK_Payments_Amount CHECK (Amount > 0),
    PaymentDate DATETIME2 NOT NULL DEFAULT SYSDATETIME()
);
GO

-- ---------- Indexes on the foreign keys (faster joins and lookups) ----------
CREATE INDEX IX_Devices_CustomerId        ON dbo.Devices(CustomerId);
CREATE INDEX IX_RepairOrders_DeviceId     ON dbo.RepairOrders(DeviceId);
CREATE INDEX IX_RepairOrders_TechnicianId ON dbo.RepairOrders(TechnicianId);
CREATE INDEX IX_RepairOrders_ServiceType  ON dbo.RepairOrders(ServiceType);
CREATE INDEX IX_RepairOrders_Status       ON dbo.RepairOrders(Status);
CREATE INDEX IX_OrderParts_PartId         ON dbo.OrderParts(PartId);
CREATE INDEX IX_Payments_OrderId          ON dbo.Payments(OrderId);
GO

-- ---------- View: cost, payments, and balance of every repair order ----------
-- Labor rule (same as the C# classes): Hardware Repair = hours x rate;
-- Software Fix and Diagnostic Only = the rate as a flat/fixed fee.
CREATE VIEW dbo.vw_OrderBalances AS
SELECT t.OrderId,
       t.CustomerId,
       t.Customer,
       t.ServiceType,
       t.Status,
       t.DateIn,
       t.Labor,
       t.PartsCost,
       t.Labor + t.PartsCost               AS TotalCost,
       t.AmountPaid,
       t.Labor + t.PartsCost - t.AmountPaid AS Balance
FROM (
    SELECT o.OrderId,
           c.CustomerId,
           c.Name AS Customer,
           o.ServiceType,
           o.Status,
           o.DateIn,
           CASE WHEN o.ServiceType = 'Hardware Repair'
                THEN o.HoursWorked * sr.Rate
                ELSE sr.Rate
           END                       AS Labor,
           COALESCE(pc.PartsCost, 0) AS PartsCost,
           COALESCE(pay.Paid, 0)     AS AmountPaid
    FROM dbo.RepairOrders AS o
    JOIN dbo.Devices      AS d  ON d.DeviceId   = o.DeviceId
    JOIN dbo.Customers    AS c  ON c.CustomerId = d.CustomerId
    JOIN dbo.ServiceRates AS sr ON sr.ServiceName = o.ServiceType
    LEFT JOIN (SELECT OrderId, SUM(Quantity * UnitPrice) AS PartsCost
               FROM dbo.OrderParts GROUP BY OrderId) AS pc ON pc.OrderId = o.OrderId
    LEFT JOIN (SELECT OrderId, SUM(Amount) AS Paid
               FROM dbo.Payments GROUP BY OrderId) AS pay ON pay.OrderId = o.OrderId
) AS t;
GO

-- =====================================================================
-- SAMPLE DATA (for testing and for the demo)
-- =====================================================================

-- Placeholder hashes: real password hashing is added later in the login service.
INSERT INTO dbo.Users (Username, PasswordHash, Role) VALUES
    ('admin',  'PLACEHOLDER_HASH', 'Administrator'),
    ('reception1', 'PLACEHOLDER_HASH', 'Receptionist'),
    ('tech1',  'PLACEHOLDER_HASH', 'Technician'),
    ('tech2',  'PLACEHOLDER_HASH', 'Technician');

INSERT INTO dbo.ServiceRates (ServiceName, Rate) VALUES
    ('Software Fix',    500.00),   -- flat rate
    ('Hardware Repair', 300.00),   -- per hour
    ('Diagnostic Only', 200.00);   -- fixed fee

INSERT INTO dbo.Customers (Name, Contact, Address) VALUES
    ('Juan Dela Cruz', '09171234567', 'Quezon City'),
    ('Maria Santos',   '09181234567', 'Caloocan City'),
    ('Pedro Reyes',    '09191234567', NULL);

INSERT INTO dbo.Devices (CustomerId, DeviceType, Brand, Model, SerialNo) VALUES
    (1, 'Laptop',  'Acer',   'Aspire 5', 'SN-ACER-001'),
    (2, 'Desktop', 'Custom', 'Tower',    'SN-CUST-002'),
    (3, 'Laptop',  'Lenovo', 'IdeaPad',  'SN-LEN-003');

INSERT INTO dbo.Parts (Name, Price, Stock) VALUES
    ('SSD 256GB',      1800.00, 10),
    ('RAM 8GB DDR4',    900.00, 8),
    ('Laptop Battery', 1500.00, 5),
    ('Power Supply',   1200.00, 4),
    ('Keyboard',        600.00, 6),
    ('Thermal Paste',   150.00, 20);

INSERT INTO dbo.RepairOrders (DeviceId, TechnicianId, ServiceType, Problem, Diagnosis, HoursWorked, Status) VALUES
    (1, 3, 'Hardware Repair', 'Slow and freezing',  'Old hard drive, replace with SSD', 2.0, 'Repairing'),
    (2, 4, 'Software Fix',    'Windows will not boot', NULL,                           0,   'Diagnosing'),
    (3, NULL, 'Diagnostic Only', 'Battery drains fast', NULL,                          0,   'Received');

INSERT INTO dbo.OrderParts (OrderId, PartId, Quantity, UnitPrice) VALUES
    (1, 1, 1, 1800.00);

INSERT INTO dbo.Payments (OrderId, Amount) VALUES
    (1, 1000.00);
GO

-- Quick check: row counts (you should see 4, 3, 3, 3, 6, 3, 1, 1)
SELECT 'Users' AS TableName, COUNT(*) AS Rows FROM dbo.Users
UNION ALL SELECT 'Customers',    COUNT(*) FROM dbo.Customers
UNION ALL SELECT 'Devices',      COUNT(*) FROM dbo.Devices
UNION ALL SELECT 'ServiceRates', COUNT(*) FROM dbo.ServiceRates
UNION ALL SELECT 'Parts',        COUNT(*) FROM dbo.Parts
UNION ALL SELECT 'RepairOrders', COUNT(*) FROM dbo.RepairOrders
UNION ALL SELECT 'OrderParts',   COUNT(*) FROM dbo.OrderParts
UNION ALL SELECT 'Payments',     COUNT(*) FROM dbo.Payments;
GO