ALTER TABLE [dbo].[ChangeRequests]
    ADD CONSTRAINT [DF_ChangeRequests_Pending] DEFAULT ((1)) FOR [Pending];

