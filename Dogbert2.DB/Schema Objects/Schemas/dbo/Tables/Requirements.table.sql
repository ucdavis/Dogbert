CREATE TABLE [dbo].[Requirements] (
    [Id]                  INT           IDENTITY (1, 1) NOT NULL,
    [Description]         VARCHAR (MAX) NOT NULL,
    [TechnicalDifficulty] INT           NULL,
    [IsComplete]          BIT           NOT NULL,
    [RequirementTypeId]   CHAR (2)      NOT NULL,
    [PriorityTypeId]      CHAR (1)      NOT NULL,
    [ProjectId]           INT           NOT NULL,
    [CategoryId]          INT           NOT NULL,
    [DateAdded]           DATETIME      NOT NULL,
    [LastModified]        DATETIME      NOT NULL,
    [RequirementId]       VARCHAR (5)   NOT NULL,
    [Order]               INT           NOT NULL
);



