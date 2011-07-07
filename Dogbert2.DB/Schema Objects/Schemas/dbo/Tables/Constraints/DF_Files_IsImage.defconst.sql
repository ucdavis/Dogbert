ALTER TABLE [dbo].[Files]
    ADD CONSTRAINT [DF_Files_IsImage] DEFAULT ((0)) FOR [IsImage];

