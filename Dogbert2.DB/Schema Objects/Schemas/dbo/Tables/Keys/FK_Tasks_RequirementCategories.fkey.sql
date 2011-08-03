ALTER TABLE [dbo].[Tasks]
    ADD CONSTRAINT [FK_Tasks_RequirementCategories] FOREIGN KEY ([RequirementCategoryId]) REFERENCES [dbo].[RequirementCategories] ([id]) ON DELETE NO ACTION ON UPDATE NO ACTION;

