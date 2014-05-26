CREATE TABLE [dbo].[Class]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY(1, 1), 

	[LevelId] INT NOT NULL,
    --[CategoryId] INT NULL, 
    --[SubcategoryId] INT NULL, 
    --[SubjectId] INT NULL, 

    [DayTime] NVARCHAR(200) NULL, 
    [Location] NVARCHAR(200) NULL,
    [Room] NVARCHAR(200) NULL,
    [Teacher] NVARCHAR(200) NULL,
    [Duration] NVARCHAR(200) NULL,
    [IsValid] BIT NOT NULL DEFAULT 1, 
    [IsDeleted] BIT NOT NULL DEFAULT 0, 
    [DateCreated] DATETIME NOT NULL DEFAULT SYSDATETIME(),
	
    CONSTRAINT [FK_Class_ToLevel] FOREIGN KEY ([LevelId]) REFERENCES [Level]([Id])
    --CONSTRAINT [FK_Class_ToCategory] FOREIGN KEY ([CategoryId]) REFERENCES [Category]([Id]),
    --CONSTRAINT [FK_Class_ToSubcategory] FOREIGN KEY ([SubcategoryId]) REFERENCES [Subcategory]([Id]),
    --CONSTRAINT [FK_Class_ToSubject] FOREIGN KEY ([SubjectId]) REFERENCES [Subject]([Id])
)
