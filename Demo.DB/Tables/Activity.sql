CREATE TABLE [dbo].[Activity]
(
	-- PK --
	[Id] INT NOT NULL PRIMARY KEY IDENTITY(1, 1),

	-- FK --
	[ClassId] INT NULL,
    [TermId] INT NOT NULL, 
	[ActivityTypeId] INT NOT NULL,

	-- Value --
    [Name] NVARCHAR(50) NOT NULL,
	[Enrolled] INT NOT NULL DEFAULT 0,
    [MaxNumber] INT NOT NULL DEFAULT 0, 
    [Price] DECIMAL(10, 2) NULL,
	[PriceIncGST] DECIMAL(10, 2) NOT NULL,
	[PriceConcession] DECIMAL(10, 2) NULL,
    [PriceMember] DECIMAL(10, 2) NULL,
    [PriceDiscount] DECIMAL(10, 2) NULL,
    [DateEarlyBird] DATETIME NULL, 
    [IsDayEvent] BIT NOT NULL DEFAULT 0, 
    [IsHidden] BIT NOT NULL DEFAULT 0, 
    [IsValid] BIT NOT NULL DEFAULT 1, 
    [IsDeleted] BIT NOT NULL DEFAULT 0, 

    [DateUpdated] DATETIME NOT NULL DEFAULT SYSDATETIME(),
    [DateCreated] DATETIME NOT NULL DEFAULT SYSDATETIME()

    CONSTRAINT [FK_Activity_ToClass] FOREIGN KEY ([ClassId]) REFERENCES [Class]([Id]),
    CONSTRAINT [FK_Activity_ToTerm] FOREIGN KEY ([TermId]) REFERENCES [Term]([Id]),
    CONSTRAINT [FK_Activity_ToActivity] FOREIGN KEY ([ActivityTypeId]) REFERENCES [ActivityType]([Id])
)
