ALTER TABLE [dbo].[ProjectTexts]
    ADD CONSTRAINT [DF_ProjectTexts_LastUpdate] DEFAULT (getdate()) FOR [LastUpdate];

