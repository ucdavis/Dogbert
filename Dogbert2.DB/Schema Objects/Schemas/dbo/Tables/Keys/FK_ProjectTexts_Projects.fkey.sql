ALTER TABLE [dbo].[ProjectTexts]
    ADD CONSTRAINT [FK_ProjectTexts_Projects] FOREIGN KEY ([ProjectId]) REFERENCES [dbo].[Projects] ([Id]) ON DELETE NO ACTION ON UPDATE NO ACTION;

