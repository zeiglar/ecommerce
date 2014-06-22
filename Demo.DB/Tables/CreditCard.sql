CREATE TABLE [dbo].[CreditCard]
(
	[Id] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY, 
    [CreditCardTypeId] INT NOT NULL, 
    [CardNumber] NVARCHAR(13) NOT NULL, 
    [ExpiryMonth] SMALLINT NOT NULL, 
    [ExpiryYear] SMALLINT NOT NULL, 
    [SecurityCode] SMALLINT NOT NULL,
    [IsValid] BIT NOT NULL DEFAULT 1, 
    [IsDeleted] BIT NOT NULL DEFAULT 0, 

    [DateUpdated] DATETIME NOT NULL DEFAULT SYSDATETIME(),
    [DateCreated] DATETIME NOT NULL DEFAULT SYSDATETIME()

    CONSTRAINT [FK_CreditCard_ToCreditCardType] FOREIGN KEY ([CreditCardTypeId]) REFERENCES [CreditCardType]([Id])
)
