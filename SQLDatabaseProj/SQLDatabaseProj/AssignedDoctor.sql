CREATE TABLE [dbo].[AssignedDoctor]
(
	[Patient] INT NOT NULL REFERENCES Patients(Id),
	Doctor INT NOT NULL REFERENCES Doctors(Id), 
    CONSTRAINT [PK_AssignedDoctor] PRIMARY KEY ([Patient], [Doctor])
)
