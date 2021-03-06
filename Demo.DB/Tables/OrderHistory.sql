﻿CREATE TABLE [dbo].[OrderHistory]
(
	--PK
	[Id] INT NOT NULL PRIMARY KEY IDENTITY(1, 1),

	--FK
	[OrderId] INT NOT NULL,

	[Content] NVARCHAR(500) NOT NULL,

    [DateUpdated] DATETIME NOT NULL DEFAULT SYSDATETIME(),
    [DateCreated] DATETIME NOT NULL DEFAULT SYSDATETIME()

    CONSTRAINT [FK_OrderHistory_ToOrder] FOREIGN KEY ([OrderId]) REFERENCES [Order]([Id])
)
