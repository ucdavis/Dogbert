ALTER TABLE [dbo].[Usecases]
    ADD CONSTRAINT [FK_Usecases_Projects] FOREIGN KEY ([ProjectId]) REFERENCES [dbo].[Projects] ([Id]) ON DELETE NO ACTION ON UPDATE NO ACTION;

