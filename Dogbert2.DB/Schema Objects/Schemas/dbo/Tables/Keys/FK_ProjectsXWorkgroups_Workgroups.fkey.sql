ALTER TABLE [dbo].[ProjectsXWorkgroups]
    ADD CONSTRAINT [FK_ProjectsXWorkgroups_Workgroups] FOREIGN KEY ([WorkgroupId]) REFERENCES [dbo].[Workgroups] ([Id]) ON DELETE NO ACTION ON UPDATE NO ACTION;

