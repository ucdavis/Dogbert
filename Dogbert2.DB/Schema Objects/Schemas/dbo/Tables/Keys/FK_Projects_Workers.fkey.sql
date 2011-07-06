ALTER TABLE [dbo].[Projects]
    ADD CONSTRAINT [FK_Projects_Workers] FOREIGN KEY ([LeadProgrammerId]) REFERENCES [dbo].[Workers] ([Id]) ON DELETE NO ACTION ON UPDATE NO ACTION;

