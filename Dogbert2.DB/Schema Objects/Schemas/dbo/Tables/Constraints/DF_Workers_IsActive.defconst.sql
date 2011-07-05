ALTER TABLE [dbo].[Workers]
    ADD CONSTRAINT [DF_Workers_IsActive] DEFAULT ((1)) FOR [IsActive];

