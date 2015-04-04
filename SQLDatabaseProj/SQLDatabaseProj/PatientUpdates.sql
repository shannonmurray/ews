CREATE TABLE [dbo].[PatientUpdates]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY(1,1),
	[Patient] INT NOT NULL REFERENCES Patients(Id),
	[Reporter] INT NOT NULL REFERENCES Doctors(Id),
	[ReportedAt] DATETIME NOT NULL DEFAULT GETDATE(),
	[RespirationRate] INT NOT NULL,
	[OxygenSaturations] INT NOT NULL,
    [Temperature] FLOAT NOT NULL, 
    [SystolicBloodPresure] INT NOT NULL, 
    [HeartRate] INT NOT NULL,

)
