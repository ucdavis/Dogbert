SET IDENTITY_INSERT [dbo].[PriorityTypes] ON
INSERT INTO [dbo].[PriorityTypes]([id], [Name], [IsActive])  VALUES(1, 'High', 1)
INSERT INTO [dbo].[PriorityTypes]([id], [Name], [IsActive])  VALUES(2, 'Normal', 1)
INSERT INTO [dbo].[PriorityTypes]([id], [Name], [IsActive])  VALUES(3, 'Low', 1)
SET IDENTITY_INSERT [dbo].[TextTypes] OFF

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

SET IDENTITY_INSERT [dbo].[TextTypes] ON
INSERT INTO [dbo].[TextTypes]([id], [Name], [IsActive], [Priority], [hasImage]) VALUES(1, 'Description', 1, 1, 0)
INSERT INTO [dbo].[TextTypes]([id], [Name], [IsActive], [Priority], [hasImage]) VALUES(1, 'Purpose', 1, 2, 0)
INSERT INTO [dbo].[TextTypes]([id], [Name], [IsActive], [Priority], [hasImage]) VALUES(1, 'Introduction', 1, 3, 0)
INSERT INTO [dbo].[TextTypes]([id], [Name], [IsActive], [Priority], [hasImage]) VALUES(1, 'Scope', 1, 4, 0)
INSERT INTO [dbo].[TextTypes]([id], [Name], [IsActive], [Priority], [hasImage]) VALUES(1, 'System Main Features', 1, 5, 0)
INSERT INTO [dbo].[TextTypes]([id], [Name], [IsActive], [Priority], [hasImage]) VALUES(1, 'Glossary', 1, 6, 0)
INSERT INTO [dbo].[TextTypes]([id], [Name], [IsActive], [Priority], [hasImage]) VALUES(1, 'Operating Environment', 1, 7, 0)
INSERT INTO [dbo].[TextTypes]([id], [Name], [IsActive], [Priority], [hasImage]) VALUES(1, 'Expected Load', 1, 8, 0)
INSERT INTO [dbo].[TextTypes]([id], [Name], [IsActive], [Priority], [hasImage]) VALUES(1, 'Appendix A', 1, 30, 0)
INSERT INTO [dbo].[TextTypes]([id], [Name], [IsActive], [Priority], [hasImage]) VALUES(1, 'Appendix B', 1, 31, 0)
INSERT INTO [dbo].[TextTypes]([id], [Name], [IsActive], [Priority], [hasImage]) VALUES(1, 'Appendix C', 1, 32, 0)
INSERT INTO [dbo].[TextTypes]([id], [Name], [IsActive], [Priority], [hasImage]) VALUES(1, 'Appendix D', 1, 33, 0)
INSERT INTO [dbo].[TextTypes]([id], [Name], [IsActive], [Priority], [hasImage]) VALUES(1, 'System Context Diagram', 1, 20, 1)
INSERT INTO [dbo].[TextTypes]([id], [Name], [IsActive], [Priority], [hasImage]) VALUES(1, 'Development Timeline', 1, 21, 1)
INSERT INTO [dbo].[TextTypes]([id], [Name], [IsActive], [Priority], [hasImage]) VALUES(1, 'Proposed Screen Flow', 1, 22, 1)
SET IDENTITY_INSERT [dbo].[TextTypes] OFF