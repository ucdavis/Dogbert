CREATE TABLE [dbo].[Workgroups] (
    [Id]           INT           IDENTITY (1, 1) NOT NULL,
    [Name]         VARCHAR (100) NOT NULL,
    [IsActive]     BIT           NOT NULL,
    [DepartmentId] CHAR (4)      NOT NULL
);





