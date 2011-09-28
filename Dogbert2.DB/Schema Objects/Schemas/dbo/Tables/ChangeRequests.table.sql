CREATE TABLE [dbo].[ChangeRequests] (
    [Id]            INT           IDENTITY (1, 1) NOT NULL,
    [Name]          VARCHAR (200) NOT NULL,
    [Description]   VARCHAR (MAX) NULL,
    [AffectedRole]  VARCHAR (MAX) NULL,
    [TimeToDevelop] VARCHAR (50)  NOT NULL,
    [RequestedBy]   VARCHAR (100) NULL,
    [RequestedDate] DATE          NULL,
    [Approved]      BIT           NULL,
    [Pending]       BIT           NOT NULL,
    [ProjectId]     INT           NOT NULL
);

