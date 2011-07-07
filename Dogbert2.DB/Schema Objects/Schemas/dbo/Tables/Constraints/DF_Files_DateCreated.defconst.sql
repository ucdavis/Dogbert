ALTER TABLE [dbo].[Files]
    ADD CONSTRAINT [DF_Files_DateCreated] DEFAULT (getdate()) FOR [DateCreated];

