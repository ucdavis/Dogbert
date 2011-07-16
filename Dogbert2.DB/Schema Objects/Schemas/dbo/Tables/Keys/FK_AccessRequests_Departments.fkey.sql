ALTER TABLE [dbo].[AccessRequests]
    ADD CONSTRAINT [FK_AccessRequests_Departments] FOREIGN KEY ([DepartmentId]) REFERENCES [dbo].[Departments] ([Id]) ON DELETE NO ACTION ON UPDATE NO ACTION;

