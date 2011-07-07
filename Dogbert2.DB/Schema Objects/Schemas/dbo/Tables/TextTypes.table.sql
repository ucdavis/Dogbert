CREATE TABLE [dbo].[TextTypes] (
    [Id]          CHAR (2)      NOT NULL,
    [Name]        VARCHAR (50)  NOT NULL,
    [Description] VARCHAR (MAX) NULL,
    [IsActive]    BIT           NOT NULL,
    [Order]       INT           NOT NULL
);

