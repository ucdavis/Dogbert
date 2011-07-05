ALTER TABLE [dbo].[Workgroups]
    ADD CONSTRAINT [DF_Workgroups_IsActive] DEFAULT ((1)) FOR [IsActive];

