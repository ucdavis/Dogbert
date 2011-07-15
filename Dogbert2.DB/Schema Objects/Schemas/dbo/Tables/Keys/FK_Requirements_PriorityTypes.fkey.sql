ALTER TABLE [dbo].[Requirements]
    ADD CONSTRAINT [FK_Requirements_PriorityTypes] FOREIGN KEY ([PriorityTypeId]) REFERENCES [dbo].[PriorityTypes] ([Id]) ON DELETE NO ACTION ON UPDATE NO ACTION;

