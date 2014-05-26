CREATE TABLE [dbo].[CategorySubcategory]
(
	[CategoryId] INT NOT NULL,
	[SubcategoryId] INT NOT NULL, 

    CONSTRAINT [PK_CategorySubcategory] PRIMARY KEY ([CategoryId], [SubcategoryId]), 

    CONSTRAINT [FK_CategorySubcategory_ToCategory] FOREIGN KEY ([CategoryId]) REFERENCES [Category]([Id]),
    CONSTRAINT [FK_CategorySubcategory_ToSubcategory] FOREIGN KEY ([SubcategoryId]) REFERENCES [Subcategory]([Id])
)
