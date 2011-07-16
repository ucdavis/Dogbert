ALTER TABLE [dbo].[AccessRequests]
    ADD CONSTRAINT [DF_AccessRequests_Pending] DEFAULT ((1)) FOR [Pending];

