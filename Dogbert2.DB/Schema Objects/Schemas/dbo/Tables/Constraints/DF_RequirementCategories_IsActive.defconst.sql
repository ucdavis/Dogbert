ALTER TABLE [dbo].[RequirementCategories]
    ADD CONSTRAINT [DF_RequirementCategories_IsActive] DEFAULT ((1)) FOR [IsActive];

