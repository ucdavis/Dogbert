ALTER TABLE [dbo].[SectionTypes]
    ADD CONSTRAINT [DF_ProjectTexts_IsActive] DEFAULT ((1)) FOR [IsActive];

