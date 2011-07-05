ALTER TABLE [dbo].[WorkgroupsXWorkers]
    ADD CONSTRAINT [FK_WorkgroupsXWorkers_Workgroups] FOREIGN KEY ([WorkgroupId]) REFERENCES [dbo].[Workgroups] ([Id]) ON DELETE NO ACTION ON UPDATE NO ACTION;

