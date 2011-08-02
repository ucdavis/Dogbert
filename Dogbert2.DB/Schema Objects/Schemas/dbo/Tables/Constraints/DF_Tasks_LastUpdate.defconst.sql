ALTER TABLE [dbo].[Tasks]
    ADD CONSTRAINT [DF_Tasks_LastUpdate] DEFAULT (getdate()) FOR [LastUpdate];

