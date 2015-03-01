CREATE PROCEDURE [dbo].[ResetCoreSettings]
	
AS
	Delete from CoreSetting;
	DBCC checkident ('CoreSetting', reseed, 0);
RETURN 0