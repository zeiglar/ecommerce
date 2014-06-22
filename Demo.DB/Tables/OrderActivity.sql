CREATE TABLE [dbo].[OrderActivity]
(
	-- PK --
    [Id] UNIQUEIDENTIFIER NOT NULL,

	-- FK --
	[OrderId] INT NOT NULL,
	[ActivityId] INT NOT NULL, 
	[PriceTypeId] INT NOT NULL,

	-- Value --
	[PriceIncGST] DECIMAL NOT NULL,
	[TotalIncGST] DECIMAL NOT NULL,
    [DateUpdated] DATETIME NOT NULL DEFAULT SYSDATETIME(),
    [DateCreated] DATETIME NOT NULL DEFAULT SYSDATETIME(),

    CONSTRAINT [PK_OrderActivity] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_OrderActivity_ToOrder] FOREIGN KEY ([OrderId]) REFERENCES [Order]([Id]),
    CONSTRAINT [FK_OrderActivity_ToActivity] FOREIGN KEY ([ActivityId]) REFERENCES [Activity]([Id]),
    CONSTRAINT [FK_OrderActivity_ToPriceType] FOREIGN KEY ([PriceTypeId]) REFERENCES [PriceType]([Id])
)
