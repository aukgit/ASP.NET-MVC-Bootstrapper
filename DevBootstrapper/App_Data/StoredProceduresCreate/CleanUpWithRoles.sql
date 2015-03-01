CREATE PROCEDURE [dbo].[CleanUpWithRoles]
	
AS
	SET NOCOUNT ON;
	DELETE FROM   dbo.AspNetUserClaims;
	DELETE FROM   dbo.AspNetUserLogins;
	DELETE FROM   dbo.AspNetUserRoles;
	DELETE FROM   dbo.AspNetUsers;
	DBCC checkident ('AspNetUsers', reseed, 0);

RETURN 0