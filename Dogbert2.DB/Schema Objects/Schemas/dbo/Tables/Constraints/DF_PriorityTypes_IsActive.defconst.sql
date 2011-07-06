ALTER TABLE [dbo].[PriorityTypes]
    ADD CONSTRAINT [DF_PriorityTypes_IsActive] DEFAULT ((1)) FOR [IsActive];

