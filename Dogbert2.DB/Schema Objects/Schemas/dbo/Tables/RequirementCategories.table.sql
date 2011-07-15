CREATE TABLE [dbo].[RequirementCategories] (
    [id]        INT          IDENTITY (1, 1) NOT NULL,
    [Name]      VARCHAR (50) NOT NULL,
    [IsActive]  BIT          NOT NULL,
    [ProjectId] INT          NOT NULL
);

