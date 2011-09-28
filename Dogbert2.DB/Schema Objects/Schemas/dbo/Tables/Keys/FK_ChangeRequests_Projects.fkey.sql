ALTER TABLE [dbo].[ChangeRequests]
    ADD CONSTRAINT [FK_ChangeRequests_Projects] FOREIGN KEY ([ProjectId]) REFERENCES [dbo].[Projects] ([Id]) ON DELETE NO ACTION ON UPDATE NO ACTION;

