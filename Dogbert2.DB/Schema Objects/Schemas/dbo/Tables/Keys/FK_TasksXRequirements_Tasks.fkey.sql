ALTER TABLE [dbo].[TasksXRequirements]
    ADD CONSTRAINT [FK_TasksXRequirements_Tasks] FOREIGN KEY ([TaskId]) REFERENCES [dbo].[Tasks] ([Id]) ON DELETE NO ACTION ON UPDATE NO ACTION;

