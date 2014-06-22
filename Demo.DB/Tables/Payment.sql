CREATE TABLE [dbo].[Payment]
(
	-- P Key --
	[Id] INT NOT NULL PRIMARY KEY IDENTITY(1, 1),

	-- F Key --
	[PaymentTypeId] INT NOT NULL,
	[OrderId] INT NOT NULL,

	-- Value --
	[Paid] DECIMAL NOT NULL DEFAULT 0,
	[AuthoredBy] NVARCHAR(100),
	
    [DateUpdated] DATETIME NOT NULL DEFAULT SYSDATETIME(),
    [DateCreated] DATETIME NOT NULL DEFAULT SYSDATETIME(),

    CONSTRAINT [FK_Payment_ToPaymentType] FOREIGN KEY ([PaymentTypeId]) REFERENCES [PaymentType]([Id]),
    CONSTRAINT [FK_Payment_ToOrder] FOREIGN KEY ([OrderId]) REFERENCES [Order]([Id])
)
