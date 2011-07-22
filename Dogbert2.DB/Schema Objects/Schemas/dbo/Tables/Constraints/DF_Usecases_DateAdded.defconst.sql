ALTER TABLE [dbo].[Usecases]
    ADD CONSTRAINT [DF_Usecases_DateAdded] DEFAULT (getdate()) FOR [DateAdded];

