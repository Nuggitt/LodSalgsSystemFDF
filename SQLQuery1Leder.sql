CREATE TABLE Leder (Leder_ID int NOT NULL PRIMARY KEY,
					Navn VARCHAR(50) NOT NULL, 
					Adresse VARCHAR(50) NOT NULL, 
					Telefon VARCHAR(50) NOT NULL, 
					Email VARCHAR(50) NOT NULL, 
					Børnegruppe_ID int NOT NULL,
					FOREIGN KEY (Børnegruppe_ID) REFERENCES Børnegruppe (Børnegruppe_ID)
					);