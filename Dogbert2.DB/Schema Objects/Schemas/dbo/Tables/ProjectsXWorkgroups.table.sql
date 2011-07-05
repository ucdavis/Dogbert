CREATE TABLE [dbo].[ProjectsXWorkgroups] (
    [Id]          INT IDENTITY (1, 1) NOT NULL,
    [ProjectId]   INT NOT NULL,
    [WorkgroupId] INT NOT NULL,
    [Order]       INT NULL
);

