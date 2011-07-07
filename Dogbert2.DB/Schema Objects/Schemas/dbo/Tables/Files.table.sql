CREATE TABLE [dbo].[Files] (
    [Id]          INT             IDENTITY (1, 1) NOT NULL,
    [Filename]    VARCHAR (50)    NOT NULL,
    [Extension]   VARCHAR (5)     NOT NULL,
    [MimeType]    VARCHAR (15)    NOT NULL,
    [IsImage]     BIT             NOT NULL,
    [Append]      BIT             NOT NULL,
    [Contents]    VARBINARY (MAX) NOT NULL,
    [DateCreated] DATETIME        NOT NULL,
    [LastUpdate]  DATETIME        NOT NULL,
    [Caption]     VARCHAR (100)   NULL,
    [ProjectId]   INT             NOT NULL
);

