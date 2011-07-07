ALTER TABLE [dbo].[WorkgroupsXWorkers]
    ADD CONSTRAINT [DF_WorkgroupsXWorkers_Limited] DEFAULT ((0)) FOR [Limited];

