ALTER TABLE [dbo].[UseCaseSteps]
    ADD CONSTRAINT [DF_UseCaseSteps_DateAdded] DEFAULT (getdate()) FOR [DateAdded];

