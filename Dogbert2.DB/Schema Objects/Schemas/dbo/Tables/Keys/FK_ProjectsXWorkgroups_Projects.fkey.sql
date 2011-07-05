ALTER TABLE [dbo].[ProjectsXWorkgroups]
    ADD CONSTRAINT [FK_ProjectsXWorkgroups_Projects] FOREIGN KEY ([ProjectId]) REFERENCES [dbo].[Projects] ([Id]) ON DELETE NO ACTION ON UPDATE NO ACTION;

