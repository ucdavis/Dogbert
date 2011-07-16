ALTER TABLE [dbo].[ProjectSections]
    ADD CONSTRAINT [FK_ProjectSections_SectionTypes] FOREIGN KEY ([SectionTypeId]) REFERENCES [dbo].[SectionTypes] ([Id]) ON DELETE NO ACTION ON UPDATE NO ACTION;

