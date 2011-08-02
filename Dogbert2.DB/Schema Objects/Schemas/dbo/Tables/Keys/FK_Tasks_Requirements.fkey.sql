ALTER TABLE [dbo].[Tasks]
    ADD CONSTRAINT [FK_Tasks_Requirements] FOREIGN KEY ([RequirementId]) REFERENCES [dbo].[Requirements] ([Id]) ON DELETE NO ACTION ON UPDATE NO ACTION;

