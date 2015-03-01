CREATE PROCEDURE [dbo].[ResetRoles]
AS
	Delete from AspNetUserRoles;
	Delete from AspNetRoles;
	
	DBCC checkident('AspNetUserRoles', reseed, 0);
	DBCC checkident('AspNetRoles', reseed, 0);
RETURN 0