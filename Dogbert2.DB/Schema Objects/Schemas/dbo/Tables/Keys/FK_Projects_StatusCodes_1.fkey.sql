ALTER TABLE [dbo].[Projects]
    ADD CONSTRAINT [FK_Projects_StatusCodes] FOREIGN KEY ([StatusCode]) REFERENCES [dbo].[StatusCodes] ([Id]) ON DELETE NO ACTION ON UPDATE NO ACTION;

