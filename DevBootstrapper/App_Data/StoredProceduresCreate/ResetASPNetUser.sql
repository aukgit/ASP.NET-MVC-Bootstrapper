CREATE PROCEDURE [dbo].[ResetASPNetUser]

AS
	Delete from RegisterCodeUserRelation;
	Delete from AspNetUserRoles;
	Delete from AspNetUsers;
	
	Update CoreSetting 
	SET 
		IsFirstUserFound =0 
	WHERE 
		CoreSettingID = 1;
		
	DBCC checkident('AspNetUserRoles', reseed, 0);
	DBCC checkident('RegisterCodeUserRelation', reseed, 0);
	DBCC checkident('AspNetUsers', reseed, 0);
RETURN 0