﻿ALTER TABLE [dbo].[ProjectTerms]
    ADD CONSTRAINT [FK_Definitions_Projects] FOREIGN KEY ([ProjectId]) REFERENCES [dbo].[Projects] ([Id]) ON DELETE NO ACTION ON UPDATE NO ACTION;

