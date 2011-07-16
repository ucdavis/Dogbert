ALTER TABLE [dbo].[ProjectSections]
    ADD CONSTRAINT [DF_ProjectTexts_LastUpdate] DEFAULT (getdate()) FOR [LastUpdate];

