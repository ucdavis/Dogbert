﻿CREATE TABLE [dbo].[Usecases] (
    [id]                    INT           IDENTITY (1, 1) NOT NULL,
    [Name]                  VARCHAR (100) NOT NULL,
    [Description]           VARCHAR (MAX) NOT NULL,
    [Precondition]          VARCHAR (MAX) NULL,
    [Postcondition]         VARCHAR (MAX) NULL,
    [ProjectId]             INT           NOT NULL,
    [RequirementCategoryId] INT           NOT NULL,
    [DateAdded]             DATETIME      NOT NULL,
    [DateModified]          DATETIME      NOT NULL
);
