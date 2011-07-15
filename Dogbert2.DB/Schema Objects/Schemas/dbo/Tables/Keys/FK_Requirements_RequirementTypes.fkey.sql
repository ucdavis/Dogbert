ALTER TABLE [dbo].[Requirements]
    ADD CONSTRAINT [FK_Requirements_RequirementTypes] FOREIGN KEY ([RequirementTypeId]) REFERENCES [dbo].[RequirementTypes] ([Id]) ON DELETE NO ACTION ON UPDATE NO ACTION;

