ALTER TABLE [dbo].[Projects]
    ADD CONSTRAINT [FK_Projects_Workers1] FOREIGN KEY ([ProjectManagerId]) REFERENCES [dbo].[Workers] ([Id]) ON DELETE NO ACTION ON UPDATE NO ACTION;

