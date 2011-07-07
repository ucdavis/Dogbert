ALTER TABLE [dbo].[Files]
    ADD CONSTRAINT [FK_Files_Projects] FOREIGN KEY ([ProjectId]) REFERENCES [dbo].[Projects] ([Id]) ON DELETE NO ACTION ON UPDATE NO ACTION;

