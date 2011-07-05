ALTER TABLE [dbo].[Workgroups]
    ADD CONSTRAINT [FK_Workgroups_Departments] FOREIGN KEY ([DepartmentId]) REFERENCES [dbo].[Departments] ([Id]) ON DELETE NO ACTION ON UPDATE NO ACTION;

