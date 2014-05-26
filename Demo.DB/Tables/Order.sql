﻿CREATE TABLE [dbo].[Order]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY(1, 1),

	[ClientId] INT NOT NULL,
	[ActivityId] INT NOT NULL,
	[PriceTypeId] INT NOT NULL,
	
	[Price] DECIMAL(10,2) NULL DEFAULT 0.0,
	[GST] DECIMAL(10,2) NULL DEFAULT 0.0,
	[PriceIncGST] DECIMAL(10,2) NOT NULL DEFAULT 0.0,
	[AmountPaid] DECIMAL(10,2) NOT NULL DEFAULT 0.0,
	[Balance] DECIMAL(10,2) NOT NULL DEFAULT 0.0,
	[Memo] NVARCHAR(2000) NULL,
	[IsSuccess] BIT NOT NULL DEFAULT 0,
	[DateCreated] DATETIME NOT NULL DEFAULT SYSDATETIME(),
	[DateUpdated] DATETIME NOT NULL DEFAULT SYSDATETIME(), 

    CONSTRAINT [FK_Order_ToClient] FOREIGN KEY ([ClientId]) REFERENCES [Client]([Id]),
    CONSTRAINT [FK_Order_ToActivity] FOREIGN KEY ([ActivityId]) REFERENCES [Activity]([Id]),
    CONSTRAINT [FK_Order_ToPriceType] FOREIGN KEY ([PriceTypeId]) REFERENCES [PriceType]([Id])
)