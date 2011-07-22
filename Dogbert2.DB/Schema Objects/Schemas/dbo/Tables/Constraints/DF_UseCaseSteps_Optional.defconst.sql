ALTER TABLE [dbo].[UseCaseSteps]
    ADD CONSTRAINT [DF_UseCaseSteps_Optional] DEFAULT ((0)) FOR [Optional];

