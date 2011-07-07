ALTER TABLE [dbo].[Files]
    ADD CONSTRAINT [DF_Files_LastUpdate] DEFAULT (getdate()) FOR [LastUpdate];

