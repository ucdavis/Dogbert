ALTER TABLE [dbo].[RequirementTypes]
    ADD CONSTRAINT [DF_RequirementTypes_IsActive] DEFAULT ((1)) FOR [IsActive];

