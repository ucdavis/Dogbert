ALTER TABLE [dbo].[StatusCodes]
    ADD CONSTRAINT [DF_StatusCodes_IsComplete] DEFAULT ((0)) FOR [Display];

