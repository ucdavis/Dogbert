ALTER TABLE [dbo].[Requirements]
    ADD CONSTRAINT [DF_Requirements_IsComplete] DEFAULT ((0)) FOR [IsComplete];

