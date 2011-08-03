ALTER TABLE [dbo].[TasksXRequirements]
    ADD CONSTRAINT [FK_TasksXRequirements_Requirements] FOREIGN KEY ([RequirementId]) REFERENCES [dbo].[Requirements] ([Id]) ON DELETE NO ACTION ON UPDATE NO ACTION;

