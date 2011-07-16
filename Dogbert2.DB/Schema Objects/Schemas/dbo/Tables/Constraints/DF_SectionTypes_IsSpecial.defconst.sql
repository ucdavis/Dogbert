ALTER TABLE [dbo].[SectionTypes]
    ADD CONSTRAINT [DF_SectionTypes_IsSpecial] DEFAULT ((0)) FOR [IsSpecial];

