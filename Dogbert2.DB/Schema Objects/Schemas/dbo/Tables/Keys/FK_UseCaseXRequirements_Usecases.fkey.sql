ALTER TABLE [dbo].[UseCaseXRequirements]
    ADD CONSTRAINT [FK_UseCaseXRequirements_Usecases] FOREIGN KEY ([UseCaseId]) REFERENCES [dbo].[Usecases] ([id]) ON DELETE NO ACTION ON UPDATE NO ACTION;

