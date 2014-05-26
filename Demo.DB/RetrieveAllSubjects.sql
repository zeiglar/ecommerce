

CREATE PROCEDURE [dbo].[RetrieveAllSubjects]
AS
	SELECT Cat.Name AS Category, Subcat.Name AS Subcategory, Subj.Name AS Subject FROM [Category] AS Cat
		JOIN [CategorySubcategory] AS CatSubcat ON Cat.Id = CatSubcat.CategoryId
		JOIN [Subcategory] AS Subcat ON Subcat.Id = CatSubcat.SubcategoryId
		JOIN [SubcategorySubject] AS SubcatSubj ON Subcat.Id = SubcatSubj.SubcategoryId
		JOIN [Subject] AS Subj ON Subj.Id = SubcatSubj.SubjectId
