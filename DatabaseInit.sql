
DROP TABLE patient_updates;

CREATE TABLE patients ( 	patient_id bigint NOT NULL PRIMARY KEY, surname varchar(255), 	first_name varchar(255) );

INSERT INTO patients (patient_id, surname, first_name)
VALUES
(0, 'Hancock', 'Amy'),
(1, 'Smith', 'John'),
(2, 'Doe', 'Jane');

CREATE TABLE patient_updates
(
	patient_id bigint NOT NULL FOREIGN KEY REFERENCES patients(patient_id),
    logged_at datetime NOT NULL,
    respiration_rate int NOT NULL,
	oxygen_saturation int NOT NULL,
	temperature float NOT NULL,
	systolic_bp int NOT NULL,
    heart_rate int NOT NULL
);