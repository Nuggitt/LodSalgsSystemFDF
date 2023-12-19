INSERT INTO [dbo].[Bruger] ([BrugerNavn], [Password]) VALUES (N'admin', N'AQAAAAIAAYagAAAAECUGL661u75+y5Iy15JdWohs+3fq1l1iMjooyPRLuyL7hCgpz01A5VL4dSVzNS+08A==')
INSERT INTO [dbo].[Bruger] ([BrugerNavn], [Password]) VALUES (N'bestyrer', N'AQAAAAIAAYagAAAAEC2fUJ/IlwPPx8l4Q0QWtKscC/LdVtjnh3VPanTkRFfr/uc2mCiwyHct4hPX780s/w==')
INSERT INTO [dbo].[Bruger] ([BrugerNavn], [Password]) VALUES (N'gey', N'AQAAAAIAAYagAAAAEGiwJBsSR80FV+NIQ66CPPIUffcY2LalCspAwWYp6qhlYypSj0/qkuiZM+NvdhdsLw==')
INSERT INTO [dbo].[Bruger] ([BrugerNavn], [Password]) VALUES (N'leder', N'AQAAAAIAAYagAAAAELmU0+AedrS5avgSvwZjLF1uB761TvCpc68UuUC+uVDd8QuwdHlwvarwY98lMsI+3A==')
INSERT INTO [dbo].[Bruger] ([BrugerNavn], [Password]) VALUES (N'lotteribestyrer', N'AQAAAAIAAYagAAAAEAtD+PmvlA5ppBgNRWcw7HOBYK0JqV1mknKHic+/2cihdk2xZTWgeaikMyq0D42sDA==')
INSERT INTO [dbo].[Bruger] ([BrugerNavn], [Password]) VALUES (N'testerbruger', N'AQAAAAIAAYagAAAAEEFpctrlUPRgq4qLER4UNFgym9cau7G3b8imN/KswGQZdE0mDMm48Ymdew3DJ3Losw==')

INSERT INTO [dbo].[Børnegruppe] ([Børnegruppe_ID], [Gruppenavn], [Lokale], [Antalbørn], [Leder_ID], [AntalLodSeddelerPrGruppe], [AntalSolgteLodSeddelerPrGruppe]) VALUES (1, N'Puslinger', N'1E', 6, 1, 0, 0)
INSERT INTO [dbo].[Børnegruppe] ([Børnegruppe_ID], [Gruppenavn], [Lokale], [Antalbørn], [Leder_ID], [AntalLodSeddelerPrGruppe], [AntalSolgteLodSeddelerPrGruppe]) VALUES (2, N'Rævene', N'6F', 10, 2, 0, 0)
INSERT INTO [dbo].[Børnegruppe] ([Børnegruppe_ID], [Gruppenavn], [Lokale], [Antalbørn], [Leder_ID], [AntalLodSeddelerPrGruppe], [AntalSolgteLodSeddelerPrGruppe]) VALUES (3, N'Uglerne', N'4J', 15, 3, 0, 0)
INSERT INTO [dbo].[Børnegruppe] ([Børnegruppe_ID], [Gruppenavn], [Lokale], [Antalbørn], [Leder_ID], [AntalLodSeddelerPrGruppe], [AntalSolgteLodSeddelerPrGruppe]) VALUES (4, N'Ulvene', N'UE69', 12, 2, 0, 0)
INSERT INTO [dbo].[Børnegruppe] ([Børnegruppe_ID], [Gruppenavn], [Lokale], [Antalbørn], [Leder_ID], [AntalLodSeddelerPrGruppe], [AntalSolgteLodSeddelerPrGruppe]) VALUES (69, N'testergruppe', N'testerhavn', 0, 1, 85, 15)

INSERT INTO [dbo].[Børn] ([Børn_ID], [Navn], [Adresse], [Telefon], [Børnegruppe_ID], [GivetLodsedler], [AntalSolgteLodseddeler]) VALUES (1, N'Eva', N'Roskildevej 123, 2610 Rødovre', N'26917212', 1, 0, 0)
INSERT INTO [dbo].[Børn] ([Børn_ID], [Navn], [Adresse], [Telefon], [Børnegruppe_ID], [GivetLodsedler], [AntalSolgteLodseddeler]) VALUES (2, N'Adam', N'Munkeleddet 321, 2720 Vanløse', N'27917291', 2, 0, 0)
INSERT INTO [dbo].[Børn] ([Børn_ID], [Navn], [Adresse], [Telefon], [Børnegruppe_ID], [GivetLodsedler], [AntalSolgteLodseddeler]) VALUES (3, N'Peter', N'Stenbjerg 49, 2600 Albertslund', N'46271917', 3, 0, 0)
INSERT INTO [dbo].[Børn] ([Børn_ID], [Navn], [Adresse], [Telefon], [Børnegruppe_ID], [GivetLodsedler], [AntalSolgteLodseddeler]) VALUES (4, N'Tom', N'Tårnvej 21 2500 Valby', N'66559876', 4, 0, 0)
INSERT INTO [dbo].[Børn] ([Børn_ID], [Navn], [Adresse], [Telefon], [Børnegruppe_ID], [GivetLodsedler], [AntalSolgteLodseddeler]) VALUES (5, N'Benedicte', N'Frederiksberg Allé 21 2000 Frederiksberg', N'84332561', 4, 0, 0)
INSERT INTO [dbo].[Børn] ([Børn_ID], [Navn], [Adresse], [Telefon], [Børnegruppe_ID], [GivetLodsedler], [AntalSolgteLodseddeler]) VALUES (69, N'BøgeTess', N'testerhavn', N'69696969', 69, 5, 15)
INSERT INTO [dbo].[Børn] ([Børn_ID], [Navn], [Adresse], [Telefon], [Børnegruppe_ID], [GivetLodsedler], [AntalSolgteLodseddeler]) VALUES (70, N'Bøgetester', N'testerhavn', N'70707070', 69, 0, 0)

INSERT INTO [dbo].[Leder] ([Leder_ID], [Navn], [Adresse], [Telefon], [Email], [ErLotteriBestyrer], [Børnegruppe_ID]) VALUES (1, N'Kristof', N'Gadevej 4 2720 Vanløse', N' 22338678', N'mrkristof@gmail.com', 0, 1)
INSERT INTO [dbo].[Leder] ([Leder_ID], [Navn], [Adresse], [Telefon], [Email], [ErLotteriBestyrer], [Børnegruppe_ID]) VALUES (2, N'Emma', N'Allevej 9 Hvidovre', N'23456789', N'emmadj@gmail.com', 0, 2)
INSERT INTO [dbo].[Leder] ([Leder_ID], [Navn], [Adresse], [Telefon], [Email], [ErLotteriBestyrer], [Børnegruppe_ID]) VALUES (3, N'Jack', N'Vejgade 5 Valby', N'87654321', N'captainjack@gmail.com', 0, 3)
INSERT INTO [dbo].[Leder] ([Leder_ID], [Navn], [Adresse], [Telefon], [Email], [ErLotteriBestyrer], [Børnegruppe_ID]) VALUES (4, N'Peter', N'Stenagervej 21 Glostrup', N'21232134', N'Ppeter@gmail.com', 0, 4)

INSERT INTO [dbo].[Salg] ([Salg_ID], [Børn_ID], [Børnegruppe_ID], [Leder_ID], [Dato], [AntalLodseddelerRetur], [AntalSolgteLodseddelerPrSalg], [Pris]) VALUES (69, 69, 69, 1, N'2023-12-18 15:39:00', 5, 15, 300)

INSERT INTO [dbo].[Indtægt] ([Indtægt_ID], [Salg_ID]) VALUES (69, 69)

