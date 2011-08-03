CREATE TABLE [dbo].[Tasks] (
    [Id]            INT           IDENTITY (1, 1) NOT NULL,
    [Description]   VARCHAR (MAX) NOT NULL,
    [Comments]      VARCHAR (MAX) NULL,
    [Complete]      BIT           NOT NULL,
    [ProjectId]     INT           NOT NULL,
    [RequirementId] INT           NOT NULL,
    [WorkerId]      INT           NOT NULL,
    [DateCreated]   DATETIME      NOT NULL,
    [LastUpdate]    DATETIME      NOT NULL,
    [TaskId]        VARCHAR (5)   NOT NULL
);



