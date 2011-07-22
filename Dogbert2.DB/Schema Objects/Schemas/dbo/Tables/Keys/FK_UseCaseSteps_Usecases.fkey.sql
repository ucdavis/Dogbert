ALTER TABLE [dbo].[UseCaseSteps]
    ADD CONSTRAINT [FK_UseCaseSteps_Usecases] FOREIGN KEY ([UseCaseId]) REFERENCES [dbo].[Usecases] ([id]) ON DELETE NO ACTION ON UPDATE NO ACTION;

