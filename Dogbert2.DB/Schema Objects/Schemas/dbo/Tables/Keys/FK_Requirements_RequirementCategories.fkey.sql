ALTER TABLE [dbo].[Requirements]
    ADD CONSTRAINT [FK_Requirements_RequirementCategories] FOREIGN KEY ([CategoryId]) REFERENCES [dbo].[RequirementCategories] ([id]) ON DELETE NO ACTION ON UPDATE NO ACTION;

