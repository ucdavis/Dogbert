ALTER TABLE [dbo].[UseCasePreconditions]
    ADD CONSTRAINT [FK_UseCasePreconditions_Usecases] FOREIGN KEY ([UseCaseId]) REFERENCES [dbo].[Usecases] ([id]) ON DELETE NO ACTION ON UPDATE NO ACTION;

