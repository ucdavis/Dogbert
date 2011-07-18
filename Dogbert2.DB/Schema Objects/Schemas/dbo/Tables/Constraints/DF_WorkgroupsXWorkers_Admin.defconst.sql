ALTER TABLE [dbo].[WorkgroupsXWorkers]
    ADD CONSTRAINT [DF_WorkgroupsXWorkers_Admin] DEFAULT ((0)) FOR [Admin];

