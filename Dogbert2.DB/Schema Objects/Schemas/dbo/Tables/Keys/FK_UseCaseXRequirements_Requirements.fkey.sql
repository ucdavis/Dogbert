ALTER TABLE [dbo].[UseCaseXRequirements]
    ADD CONSTRAINT [FK_UseCaseXRequirements_Requirements] FOREIGN KEY ([RequirementId]) REFERENCES [dbo].[Requirements] ([Id]) ON DELETE NO ACTION ON UPDATE NO ACTION;

