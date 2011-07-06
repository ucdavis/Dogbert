CREATE TABLE [dbo].[PriorityTypes] (
    [Id]       CHAR (1)     NOT NULL,
    [Name]     VARCHAR (50) NOT NULL,
    [IsActive] BIT          NOT NULL,
    [Order]    INT          NULL
);

