CREATE TABLE Indtægt (Indtægt_ID int NOT NULL PRIMARY KEY,
					  Dato DateTime NOT NULL,
					  Beløb Float NOT NULL,
					  Salg_ID int NOT NULL,
					  FOREIGN KEY (Salg_ID) REFERENCES Salg (Salg_ID)
);