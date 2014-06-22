CREATE TABLE [dbo].[OrderStatus]
(
	[Id] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY, 

    [OrderId] INT NOT NULL, 
    [PaymentStatusId] INT NOT NULL DEFAULT 1, 
    [OrderStatusTypeId] INT NOT NULL DEFAULT 1, 

	[IsSuccess] BIT NOT NULL DEFAULT 0,

    [DateUpdated] DATETIME NOT NULL DEFAULT SYSDATETIME(),
    [DateCreated] DATETIME NOT NULL DEFAULT SYSDATETIME()

    CONSTRAINT [FK_OrderStatus_ToOrder] FOREIGN KEY ([OrderId]) REFERENCES [Order]([Id]),
    CONSTRAINT [FK_OrderStatus_ToPaymentStatus] FOREIGN KEY ([PaymentStatusId]) REFERENCES [PaymentStatus]([Id]),
    CONSTRAINT [FK_OrderStatus_ToOrderStatusType] FOREIGN KEY ([OrderStatusTypeId]) REFERENCES [OrderStatusType]([Id])
)
