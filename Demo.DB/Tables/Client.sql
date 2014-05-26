CREATE TABLE [dbo].[Client]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY(1, 1),

	[ReferenceTypeId] INT NULL,
	[MemberTypeId] INT NOT NULL DEFAULT 1,
	[TitleTypeId] INT NOT NULL,

	[FirstName] NVARCHAR(50) NOT NULL,
	[LastName] NVARCHAR(50) NOT NULL,
	[Address] NVARCHAR(200) NOT NULL,
	[Suburb] NVARCHAR(50) NOT NULL,
	[Postcode] NVARCHAR(10) NOT NULL,
	[Mobile] NVARCHAR(20) NULL,
	[HomePone] NVARCHAR(20) NULL,
	[WorkPhone] NVARCHAR(20) NULL,
	[Email] NVARCHAR(200) NOT NULL,
    [IsValid] BIT NOT NULL DEFAULT 1, 
    [IsDeleted] BIT NOT NULL DEFAULT 0, 
    [DateCreated] DATETIME NOT NULL DEFAULT SYSDATETIME(), 

    CONSTRAINT [FK_Client_ToReferenceType] FOREIGN KEY ([ReferenceTypeId]) REFERENCES [ReferenceType]([Id]),
    CONSTRAINT [FK_Client_ToMemberType] FOREIGN KEY ([MemberTypeId]) REFERENCES [MemberType]([Id]),
    CONSTRAINT [FK_Client_ToTitleType] FOREIGN KEY ([TitleTypeId]) REFERENCES [TitleType]([Id])
)
