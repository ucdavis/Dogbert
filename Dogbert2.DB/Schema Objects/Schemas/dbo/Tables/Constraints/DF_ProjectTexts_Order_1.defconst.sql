ALTER TABLE [dbo].[SectionTypes]
    ADD CONSTRAINT [DF_ProjectTexts_Order] DEFAULT ((999)) FOR [Order];

