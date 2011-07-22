CREATE TABLE [dbo].[UseCaseSteps] (
    [id]           INT           IDENTITY (1, 1) NOT NULL,
    [Description]  VARCHAR (MAX) NOT NULL,
    [Order]        INT           NOT NULL,
    [Optional]     BIT           NOT NULL,
    [DateAdded]    DATETIME      NOT NULL,
    [DateModified] DATETIME      NOT NULL,
    [UseCaseId]    INT           NOT NULL
);

