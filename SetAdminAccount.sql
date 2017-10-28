/****** Script for SelectTopNRows command from SSMS  ******/
SELECT TOP (1000) [UserId]
      ,[AspNetUserId]
      ,[username]
      ,[IsManager]
      ,[IsStudent]
      ,[IsAdmin]
      ,[StudentId]
  FROM [dbo].[Users]

  --Assuming website published successfully and is able to connect to database.
  --The very first account created on the website will be the admin account
  --*After you created the first account on the website 
  --*Run the script below on the sql server, it will set the account to admin

  update dbo.Users 
  set IsAdmin = 1,
  IsManager = 1,
  IsStudent = 0
  where UserId = 1