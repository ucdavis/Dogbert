CREATE TABLE [dbo].[Workers] (
    [Id]        INT          IDENTITY (1, 1) NOT NULL,
    [LoginId]   VARCHAR (10) NOT NULL,
    [FirstName] VARCHAR (50) NOT NULL,
    [LastName]  VARCHAR (50) NOT NULL,
    [IsActive]  BIT          NOT NULL
);



