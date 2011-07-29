ALTER TABLE [dbo].[Projects]
    ADD CONSTRAINT [DF_Projects_IsPublic] DEFAULT ((0)) FOR [IsPublic];

