ALTER TABLE [dbo].[Usecases]
    ADD CONSTRAINT [DF_Usecases_DateModified] DEFAULT (getdate()) FOR [DateModified];

