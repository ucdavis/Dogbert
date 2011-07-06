ALTER TABLE [dbo].[ProjectTypes]
    ADD CONSTRAINT [DF_ProjectTypes_IsActive] DEFAULT ((1)) FOR [IsActive];

