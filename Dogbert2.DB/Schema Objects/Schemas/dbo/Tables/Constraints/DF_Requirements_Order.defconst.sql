ALTER TABLE [dbo].[Requirements]
    ADD CONSTRAINT [DF_Requirements_Order] DEFAULT ((9999999)) FOR [Order];

