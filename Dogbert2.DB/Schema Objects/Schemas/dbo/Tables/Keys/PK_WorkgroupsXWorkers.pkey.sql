﻿ALTER TABLE [dbo].[WorkgroupsXWorkers]
    ADD CONSTRAINT [PK_WorkgroupsXWorkers] PRIMARY KEY CLUSTERED ([WorkerId] ASC, [WorkgroupId] ASC) WITH (ALLOW_PAGE_LOCKS = ON, ALLOW_ROW_LOCKS = ON, PAD_INDEX = OFF, IGNORE_DUP_KEY = OFF, STATISTICS_NORECOMPUTE = OFF);
