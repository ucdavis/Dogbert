CREATE TABLE [dbo].[SectionTypes] (
    [Id]          CHAR (2)      NOT NULL,
    [Name]        VARCHAR (50)  NOT NULL,
    [Description] VARCHAR (MAX) NULL,
    [IsActive]    BIT           NOT NULL,
    [Order]       INT           NOT NULL,
    [IsSpecial]   BIT           NOT NULL
);

