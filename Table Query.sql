CREATE DATABASE LodSalgsSystemDB

CREATE TABLE [dbo].[Bruger] (
    [BrugerNavn] NVARCHAR (50)  NOT NULL,
    [Password]   NVARCHAR (255) NOT NULL
);


CREATE TABLE [dbo].[Børn] (
    [Børn_ID]                INT          NOT NULL,
    [Navn]                   VARCHAR (50) NOT NULL,
    [Adresse]                VARCHAR (50) NOT NULL,
    [Telefon]                VARCHAR (50) NOT NULL,
    [Børnegruppe_ID]         INT          NOT NULL,
    [GivetLodsedler]         INT          NOT NULL,
    [AntalSolgteLodseddeler] INT          NULL
);

CREATE TABLE [dbo].[Børnegruppe] (
    [Børnegruppe_ID]                 INT          NOT NULL,
    [Gruppenavn]                     VARCHAR (50) NOT NULL,
    [Lokale]                         VARCHAR (50) NOT NULL,
    [Antalbørn]                      INT          NOT NULL,
    [Leder_ID]                       INT          NOT NULL,
    [AntalLodSeddelerPrGruppe]       INT          NOT NULL,
    [AntalSolgteLodSeddelerPrGruppe] INT          NOT NULL
);

CREATE TABLE [dbo].[Indtægt] (
    [Indtægt_ID] INT NOT NULL,
    [Salg_ID]    INT NOT NULL
);

CREATE TABLE [dbo].[Leder] (
    [Leder_ID]          INT          NOT NULL,
    [Navn]              VARCHAR (50) NOT NULL,
    [Adresse]           VARCHAR (50) NOT NULL,
    [Telefon]           VARCHAR (50) NOT NULL,
    [Email]             VARCHAR (50) NOT NULL,
    [ErLotteriBestyrer] BIT          NOT NULL,
    [Børnegruppe_ID]    INT          NOT NULL
);

CREATE TABLE [dbo].[Salg] (
    [Salg_ID]                      INT        NOT NULL,
    [Børn_ID]                      INT        NOT NULL,
    [Børnegruppe_ID]               INT        NOT NULL,
    [Leder_ID]                     INT        NOT NULL,
    [Dato]                         DATETIME   NOT NULL,
    [AntalLodseddelerRetur]        INT        NOT NULL,
    [AntalSolgteLodseddelerPrSalg] INT        NOT NULL,
    [Pris]                         FLOAT (53) NOT NULL
);