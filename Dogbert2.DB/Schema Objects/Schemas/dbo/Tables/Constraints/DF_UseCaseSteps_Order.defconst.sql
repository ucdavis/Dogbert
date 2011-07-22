ALTER TABLE [dbo].[UseCaseSteps]
    ADD CONSTRAINT [DF_UseCaseSteps_Order] DEFAULT ((999)) FOR [Order];

