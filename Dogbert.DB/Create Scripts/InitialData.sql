INSERT INTO [dbo].[PriorityTypes]([id], [Name], [IsActive])  VALUES(1, 'High', 1)
INSERT INTO [dbo].[PriorityTypes]([id], [Name], [IsActive])  VALUES(2, 'Normal', 1)
INSERT INTO [dbo].[PriorityTypes]([id], [Name], [IsActive])  VALUES(3, 'Low', 1)

INSERT INTO [dbo].[ProjectTypes]([id], [Name], [IsActive])  VALUES('IN', 'Infrastructure', 1)
INSERT INTO [dbo].[ProjectTypes]([id], [Name], [IsActive])  VALUES('SH', 'Sharepoint', 0)
INSERT INTO [dbo].[ProjectTypes]([id], [Name], [IsActive])  VALUES('WA', 'Web Application', 1)
INSERT INTO [dbo].[ProjectTypes]([id], [Name], [IsActive])  VALUES('WS', 'Web Site', 1)

INSERT INTO [dbo].[RequirementTypes]([id], [Name], [IsActive])  VALUES('FC', 'Functional', 1)
INSERT INTO [dbo].[RequirementTypes]([id], [Name], [IsActive])  VALUES('NF', 'Non-Functional', 1)

INSERT INTO [dbo].[StatusCodes]([id], [Name], [IsActive], [IsComplete])  VALUES('CA', 'Cancelled', 1, 1)
INSERT INTO [dbo].[StatusCodes]([id], [Name], [IsActive], [IsComplete])  VALUES('PE', 'Pending', 1, 0)
INSERT INTO [dbo].[StatusCodes]([id], [Name], [IsActive], [IsComplete])  VALUES('PR', 'Production', 1, 1)
INSERT INTO [dbo].[StatusCodes]([id], [Name], [IsActive], [IsComplete])  VALUES('WO', 'Working', 1, 0)

 