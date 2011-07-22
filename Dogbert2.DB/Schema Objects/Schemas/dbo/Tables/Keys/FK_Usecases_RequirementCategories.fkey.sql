ALTER TABLE [dbo].[Usecases]
    ADD CONSTRAINT [FK_Usecases_RequirementCategories] FOREIGN KEY ([RequirementCategoryId]) REFERENCES [dbo].[RequirementCategories] ([id]) ON DELETE NO ACTION ON UPDATE NO ACTION;

