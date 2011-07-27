CREATE TABLE [dbo].[UseCasePostconditions] (
    [Id]          INT           IDENTITY (1, 1) NOT NULL,
    [Description] VARCHAR (MAX) NOT NULL,
    [UseCaseId]   INT           NOT NULL
);

