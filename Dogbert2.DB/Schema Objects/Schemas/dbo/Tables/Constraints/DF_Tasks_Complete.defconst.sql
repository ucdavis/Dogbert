ALTER TABLE [dbo].[Tasks]
    ADD CONSTRAINT [DF_Tasks_Complete] DEFAULT ((1)) FOR [Complete];

