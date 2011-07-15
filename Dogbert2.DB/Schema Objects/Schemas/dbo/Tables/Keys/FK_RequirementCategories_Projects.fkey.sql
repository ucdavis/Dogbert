ALTER TABLE [dbo].[RequirementCategories]
    ADD CONSTRAINT [FK_RequirementCategories_Projects] FOREIGN KEY ([ProjectId]) REFERENCES [dbo].[Projects] ([Id]) ON DELETE NO ACTION ON UPDATE NO ACTION;

