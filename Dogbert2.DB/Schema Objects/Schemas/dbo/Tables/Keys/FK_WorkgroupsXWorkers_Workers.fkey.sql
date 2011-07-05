ALTER TABLE [dbo].[WorkgroupsXWorkers]
    ADD CONSTRAINT [FK_WorkgroupsXWorkers_Workers] FOREIGN KEY ([WorkerId]) REFERENCES [dbo].[Workers] ([Id]) ON DELETE NO ACTION ON UPDATE NO ACTION;

