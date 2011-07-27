ALTER TABLE [dbo].[UseCasePostconditions]
    ADD CONSTRAINT [FK_UseCasePostconditions_Usecases] FOREIGN KEY ([UseCaseId]) REFERENCES [dbo].[Usecases] ([id]) ON DELETE NO ACTION ON UPDATE NO ACTION;

