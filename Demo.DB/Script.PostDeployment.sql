/*
Post-Deployment Script Template							
--------------------------------------------------------------------------------------
 This file contains SQL statements that will be appended to the build script.		
 Use SQLCMD syntax to include a file in the post-deployment script.			
 Example:      :r .\myfile.sql								
 Use SQLCMD syntax to reference a variable in the post-deployment script.		
 Example:      :setvar TableName MyTable							
               SELECT * FROM [$(TableName)]					
--------------------------------------------------------------------------------------
*/

/*******************************************************/
/****************  Types Default Setting  **************/
/*******************************************************/
INSERT INTO [PriceType]
VALUES
('Default', 0),
('ConcessionPrice', 1),
('EarlyBirdPrice', 2),
('MemberPrice',3),
('DiscountPrice', 4)

INSERT INTO [ActivityType]
VALUES
('Class', 1),
('Member', 2),
('Event',3)

INSERT INTO [ReferenceType]
VALUES
('Advertising', 1),
('Already In', 2),
('Other',3)

INSERT INTO [MemberType]
VALUES
('None',1),
('Standard',2),
('Concession',3)

INSERT INTO [TitleType]
VALUES
('Mr',1),
('Mrs',2),
('Ms',3),
('Miss',4),
('Dr',5)

INSERT INTO [CreditCardType]
VALUES
('Visa',1),
('Mastercard',2)

INSERT INTO [OrderStatusType]
VALUES
('Initial',1),
('Paused',2),
('Failed',3),
('Success',4)

--- TODO: Update later
INSERT INTO [PaymentStatus]
VALUES
('Fail',1),
('Success',2)

INSERT INTO [Subcategory]
([Name])
VALUES
('Adult Classes'),
('La Petite Ecole'),
('HSC Cafe Blabla')

INSERT INTO [Category]
([Name])
VALUES
('Courses')

INSERT INTO [CategorySubcategory]
([CategoryId], [SubcategoryId])
VALUES
(1,1),
(1,2),
(1,3)

---------------------------------------------------------------------
----------------------------- Test Case -----------------------------
---------------------------------------------------------------------

INSERT INTO [Term]
([Year],[Name],[DateStart],[DateEnd],
[IsHidden],[IsValid],[IsDeleted],[DateCreated])
VALUES
(2015,'Term 1','2015-01-01 00:00:00.000','2015-12-31 00:00:00.000',1,0,0,'2014-05-05 23:41:15.800'),
(2014,'Term 1','2014-02-03 00:00:00.000','2014-06-27 00:00:00.000',1,0,0,'2014-05-05 23:41:15.800'),
(2014,'Term 2','2014-07-07 00:00:00.000','2014-11-28 00:00:00.000',1,0,0,'2014-05-06 23:41:15.800')

INSERT INTO [Subject]
([Name])
VALUES
('French for Travellers'),
('Y12 Beginners')

INSERT INTO [SubcategorySubject]
([SubcategoryId], [SubjectId])
VALUES
(1, 1),
(3, 2)

INSERT INTO [Level]
([Category],[Subcategory],[Subject], [Level])
VALUES
('Courses', 'Adult_Classes', 'Beginner', 'Courses-Adult_Classes-Beginner'),
('Courses', 'Adult_Classes', 'French for Travellers', 'Courses-Adult_Classes-French for Travellers'),
('Courses', 'Child_Classes', 'Beginner', 'Courses-Adult_Classes-Beginner')

INSERT INTO [Class]
([LevelId],[DayTime],[Location],[Room],[Teacher],[IsValid])
VALUES
(1, '12/May/2014 - 22/May/2014', 'Sydney', 'R504', 'Dr Test', 1),
(2, '12/May/2014 - 22/May/2014', 'Canberra', 'R504', 'Dr Test', 1)

INSERT INTO [Activity]
([ClassId], [TermId], [ActivityTypeId], [Name], [Enrolled], [MaxNumber], [PriceIncGST], [PriceConcession])
VALUES
(null, 1, 2, 'Member2014', 2, 0, 20, 15),
(1, 1, 1, 'class1', 1, 30, 240, null),
(2, 1, 1, 'class2', 1, 15, 240, null)

INSERT INTO [Client]
([TitleTypeId],[FirstName],[LastName],[Address],[Suburb],[Postcode],[Mobile],[Email])
VALUES
(1, 'FTest', 'LTest', 'Test Adddress', 'TestS', '2000', '0123456789', 'test@test.com'),
(2, 'FTest2', 'LTest2', 'Test2 Adddress', 'Test2S', '2000', '1234567890', 'test2@test2.com')

INSERT INTO [Order]
([ClientId], [ActivityId], [PriceTypeId], [PriceIncGST], [AmountPaid],[Balance], [Memo], [IsSuccess])
VALUES
(1, 1, 1, 20, 20, 0, 'It is a test for Mr Ftest to book a member', 1),
(1, 2, 1, 240, 240, 0, 'It is a test for Mr Ftest to book a class', 1),
(2, 1, 2, 15, 15, 0, 'It is a test2 for Mr Ftest to book a member in a concession price', 1),
(2, 3, 1, 240, 240, 0, 'It is a test2 for Mr Ftest to book a class', 1)

INSERT INTO [OrderHistory]
([OrderId], [Content])
VALUES
(1, 'Client Fill the info form'),
(1, 'Payment has been made'),
(2, 'Client Fill the info form'),
(2, 'Payment has been made'),
(3, 'Client Fill the info form'),
(3, 'Payment has been made'),
(4, 'Client Fill the info form'),
(4, 'Payment has been made')

INSERT INTO [OrderActivity]
([OrderId],[ActivityId])
VALUES
(1,1),
(1,2),
(2,1),
(2,3)