CREATE TABLE Salg (Salg_ID int NOT NULL PRIMARY KEY,
				   Ark_ID int NOT NULL,
				   Pris float NOT NULL,
				   Børn_ID int NOT NULL,
				   FOREIGN KEY (Børn_ID) REFERENCES Børn (Børn_ID)
);