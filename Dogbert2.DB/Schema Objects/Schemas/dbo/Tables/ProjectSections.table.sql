CREATE TABLE [dbo].[ProjectSections] (
    [Id]            INT           IDENTITY (1, 1) NOT NULL,
    [Text]          VARCHAR (MAX) NOT NULL,
    [ProjectId]     INT           NOT NULL,
    [SectionTypeId] CHAR (2)      NOT NULL,
    [DateCreated]   DATETIME      NOT NULL,
    [LastUpdate]    DATETIME      NOT NULL
);

