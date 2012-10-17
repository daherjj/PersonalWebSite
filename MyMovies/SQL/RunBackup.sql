    -- Insert statements for procedure here
	DECLARE @Path NVARCHAR(250) = 'C:\Users\Jovan\MovieBackUp\MovieDBContext.bak'
	-- allow advanced options to be changed
	EXEC sp_configure 'show advanced options', 1

	-- update the currently configured value for advanced options
	RECONFIGURE

	-- enable the feature
	EXEC sp_configure 'xp_cmdshell',1

	-- update the currently configured value for this feature
	RECONFIGURE
	
	
	DECLARE @Dir NVARCHAR(250)
	set @Dir = 'xp_cmdshell ''md ' + @Path + ''''
	exec (@dir)

	
	-- allow advanced options to be changed
	EXEC sp_configure 'show advanced options', 1

	-- update the currently configured value for advanced options
	RECONFIGURE

	-- enable the feature
	EXEC sp_configure 'xp_cmdshell',0

	-- update the currently configured value for this feature
	RECONFIGURE







USE MOVIES_9e8a412e066e48d7938286a6947a5cdc
GO
BACKUP DATABASE MOVIES_9e8a412e066e48d7938286a6947a5cdc
TO DISK = "@Path"
   WITH FORMAT,
      MEDIANAME = 'MovieDBContext',
      NAME = 'Full Backup of MovieDBContext';
GO