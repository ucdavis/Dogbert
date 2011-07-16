ALTER TABLE [dbo].[AccessRequests]
    ADD CONSTRAINT [DF_AccessRequests_DateCreated] DEFAULT (getdate()) FOR [DateCreated];

