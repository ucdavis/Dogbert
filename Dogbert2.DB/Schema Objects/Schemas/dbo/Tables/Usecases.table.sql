CREATE TABLE [dbo].[Usecases] (
    [id]                    INT           IDENTITY (1, 1) NOT NULL,
    [Name]                  VARCHAR (100) NOT NULL,
    [Description]           VARCHAR (MAX) NOT NULL,
    [Roles]                 VARCHAR (100) NULL,
    [ProjectId]             INT           NOT NULL,
    [RequirementCategoryId] INT           NOT NULL,
    [DateAdded]             DATETIME      NOT NULL,
    [DateModified]          DATETIME      NOT NULL
);



