ALTER TABLE [dbo].[Projects]
    ADD CONSTRAINT [FK_Projects_ProjectTypes] FOREIGN KEY ([ProjectTypeId]) REFERENCES [dbo].[ProjectTypes] ([Id]) ON DELETE NO ACTION ON UPDATE NO ACTION;

