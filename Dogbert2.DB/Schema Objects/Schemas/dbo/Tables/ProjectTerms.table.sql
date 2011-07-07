CREATE TABLE [dbo].[ProjectTerms] (
    [Id]         INT           IDENTITY (1, 1) NOT NULL,
    [Term]       VARCHAR (50)  NOT NULL,
    [Definition] VARCHAR (MAX) NOT NULL,
    [Src]        VARCHAR (50)  NULL,
    [ProjectId]  INT           NOT NULL
);

