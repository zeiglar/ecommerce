CREATE TABLE [dbo].[SubcategorySubject]
(
	[SubcategoryId] INT NOT NULL, 
	[SubjectId] INT NOT NULL,

    CONSTRAINT [PK_SubcategorySubject] PRIMARY KEY ([SubcategoryId], [SubjectId]),
	
    CONSTRAINT [FK_SubcategorySubject_ToSubcategory] FOREIGN KEY ([SubcategoryId]) REFERENCES [Subcategory]([Id]),
    CONSTRAINT [FK_SubcategorySubject_ToSubject] FOREIGN KEY ([SubjectId]) REFERENCES [Subject]([Id])
)
