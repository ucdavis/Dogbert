ALTER TABLE [dbo].[ProjectTexts]
    ADD CONSTRAINT [DF_ProjectTexts_DateCreated] DEFAULT (getdate()) FOR [DateCreated];

