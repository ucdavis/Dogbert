ALTER TABLE [dbo].[UseCaseSteps]
    ADD CONSTRAINT [DF_UseCaseSteps_DateModified] DEFAULT (getdate()) FOR [DateModified];

