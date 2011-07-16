ALTER TABLE [dbo].[ProjectSections]
    ADD CONSTRAINT [DF_ProjectTexts_DateCreated] DEFAULT (getdate()) FOR [DateCreated];

