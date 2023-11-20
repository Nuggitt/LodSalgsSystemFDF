Insert INTO BrugerLogin VALUES(1,'Frank','123','Frank1234@gmail.com');
Insert INTO BrugerLogin VALUES(2,'Tom','1234','Tomtom@gmail.com');
Insert INTO BrugerLogin VALUES(3,'Hans','123456','SejeFrank@gmail.com');

INSERT INTO [Lodseddelark] ([Ark_ID], [Antallodsedler], [PrisPrLod], [PrisPrArk])
VALUES 
    (1, 50, 20, 1000),
    (2, 50, 20, 1000),
    (3, 50, 20, 1000);


INSERT INTO Børnegruppe VALUES (1,'Puslinger','1E',6,1)
INSERT INTO Børnegruppe VALUES(1,'Rævene','6F',10,2)
INSERT INTO Børnegruppe VALUES (1,'Uglerne','4J',15,3)

INSERT INTO Børn VALUES( 1,'Eva' ,'Roskildevej 123, 2610 Rødovre' ,'26917212' ,1);
INSERT INTO Børn VALUES( 2,'Adam' ,'Munkeleddet 321, 2720 Vanløse' ,'27917291' ,2);
INSERT INTO Børn VALUES( 3,'Peter' ,'Stenbjerg 49, 2600 Albertslund' ,'46271917' ,3);

INSERT INTO Salg VALUES( 1, 1, 1, 1000);
INSERT INTO Salg VALUES( 2, 2, 2, 700);
INSERT INTO Salg VALUES( 3, 3, 3, 500);

INSERT INTO Leder VALUES (1, 'Kristof', 'Gadevej 4',' 12345678', 'placeholder@email.dk', 1, 1, 1);
INSERT INTO Leder VALUES (2, 'Emma', 'Allevej 9', '23456789', 'placeholder2@email.dk', 0, 2, 2);
INSERT INTO Leder VALUES (3, 'Jack', 'Vejgade 5', '87654321', 'placeholder3email.dk', 1, 3, 3);

INSERT INTO Salg VALUES( 1, 1, 1, 1000);
INSERT INTO Salg VALUES( 2, 2, 2, 700);
INSERT INTO Salg VALUES( 3, 3, 3, 500);