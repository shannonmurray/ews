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

DELETE FROM AssignedDoctor;
DELETE FROM PatientUpdates;

DELETE FROM DOCTORS;
INSERT INTO Doctors
	(FirstName, Surname, Email, Phone)
VALUES
	('Auto', 'Probe',		'robots@sacredheart.com',		'+440000000000'),
	('Christopher', 'Turk',		'turk@sacredheart.com',		'+441234567898'),
	('Perry',		'Cox',		'perry@sacredheart.com',	'+441234547837'),
	('Bob',			'Kelso',	'kelso@sacredheart.com',	'+441234534698'),
	('John',		'Dorian',	'jd@sacredheart.com',		'+441237654698'),
	('Elliot',		'Reid',		'elliot@sacredheart.com',	'+441238734698');

DELETE FROM Patients;
INSERT INTO Patients
	(FirstName, Surname, Bed)
VALUES
	('Amy', 'Hancock', 0),
	('Natalie', 'Langlands', 1),
	('Jane', 'Doe', 2),
	('John', 'Smith', 3),
	('Fraser', 'Burnett', 4);
