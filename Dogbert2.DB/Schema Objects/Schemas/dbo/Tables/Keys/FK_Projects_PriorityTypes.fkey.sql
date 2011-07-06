ALTER TABLE [dbo].[Projects]
    ADD CONSTRAINT [FK_Projects_PriorityTypes] FOREIGN KEY ([Priority]) REFERENCES [dbo].[PriorityTypes] ([Id]) ON DELETE NO ACTION ON UPDATE NO ACTION;

