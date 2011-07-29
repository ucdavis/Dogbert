CREATE TABLE [dbo].[Projects] (
    [Id]               INT           IDENTITY (1, 1) NOT NULL,
    [ProjectTypeId]    CHAR (2)      NOT NULL,
    [Name]             VARCHAR (50)  NOT NULL,
    [Priority]         CHAR (1)      NULL,
    [Complexity]       SMALLINT      NULL,
    [Deadline]         DATE          NULL,
    [ProjectedBegin]   DATE          NULL,
    [Begin]            DATE          NULL,
    [ProjectedEnd]     DATE          NULL,
    [End]              DATE          NULL,
    [Contact]          VARCHAR (100) NOT NULL,
    [ContactEmail]     VARCHAR (100) NULL,
    [Unit]             VARCHAR (50)  NOT NULL,
    [StatusCode]       CHAR (1)      NOT NULL,
    [ProjectManagerId] INT           NULL,
    [LeadProgrammerId] INT           NULL,
    [DateAdded]        DATETIME      NULL,
    [LastUpdate]       DATETIME      NULL,
    [Hide]             BIT           NOT NULL,
    [IsPublic]         BIT           NULL
);





