CREATE TABLE [dbo].[AccessRequests] (
    [Id]           INT           IDENTITY (1, 1) NOT NULL,
    [LoginId]      VARCHAR (10)  NOT NULL,
    [FirstName]    VARCHAR (50)  NOT NULL,
    [LastName]     VARCHAR (50)  NOT NULL,
    [DepartmentId] CHAR (4)      NOT NULL,
    [Email]        VARCHAR (50)  NOT NULL,
    [OtherUsers]   VARCHAR (MAX) NULL,
    [DateCreated]  DATETIME      NOT NULL,
    [Pending]      BIT           NOT NULL
);

