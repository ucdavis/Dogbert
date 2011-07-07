ALTER TABLE [dbo].[Files]
    ADD CONSTRAINT [DF_Files_Append] DEFAULT ((0)) FOR [Append];

