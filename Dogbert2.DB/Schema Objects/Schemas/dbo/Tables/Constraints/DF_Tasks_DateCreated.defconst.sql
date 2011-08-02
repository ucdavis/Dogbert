ALTER TABLE [dbo].[Tasks]
    ADD CONSTRAINT [DF_Tasks_DateCreated] DEFAULT (getdate()) FOR [DateCreated];

