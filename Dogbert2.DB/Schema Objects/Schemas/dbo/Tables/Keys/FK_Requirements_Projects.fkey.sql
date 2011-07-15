ALTER TABLE [dbo].[Requirements]
    ADD CONSTRAINT [FK_Requirements_Projects] FOREIGN KEY ([ProjectId]) REFERENCES [dbo].[Projects] ([Id]) ON DELETE NO ACTION ON UPDATE NO ACTION;

