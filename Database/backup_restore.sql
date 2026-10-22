-- =====================================================================
-- FixFlow: backup and restore (SQL Server LocalDB)
-- 1. Create the folder C:\FixFlowBackup first.
-- 2. Run PART A to make a backup. Run PART B only to restore it.
-- Both parts run in a query window connected to the server (not inside FixFlowDB).
-- =====================================================================

-- PART A: BACKUP
USE master;
GO
BACKUP DATABASE FixFlowDB
TO DISK = N'C:\FixFlowBackup\FixFlowDB.bak'
WITH INIT, NAME = N'FixFlowDB full backup';
GO

-- PART B: RESTORE (replaces the current FixFlowDB with the backup)
USE master;
GO
ALTER DATABASE FixFlowDB SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
GO
RESTORE DATABASE FixFlowDB
FROM DISK = N'C:\FixFlowBackup\FixFlowDB.bak'
WITH REPLACE;
GO
ALTER DATABASE FixFlowDB SET MULTI_USER;
GO
