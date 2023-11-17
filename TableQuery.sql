CREATE TABLE [dbo].[Lodseddelark] (
    [Ark_ID]         INT        NOT NULL,
    [Antallodsedler] INT        NOT NULL,
    [PrisPrLod]      FLOAT (53) NOT NULL,
    [PrisPrArk]      FLOAT (53) NOT NULL,
    PRIMARY KEY CLUSTERED ([Ark_ID] ASC)
);

CREATE TABLE [dbo].[BrugerLogin] (
    [Bruger_ID]   INT          NOT NULL,
    [Bruger_Navn] INT          NOT NULL,
    [Kodeord]     VARCHAR (50) NOT NULL,
    [Email]       VARCHAR (50) NULL,
    PRIMARY KEY CLUSTERED ([Bruger_ID] ASC)
);

CREATE TABLE [dbo].[Leder] (
    [Leder_ID]          INT          NOT NULL,
    [Navn]              VARCHAR (50) NOT NULL,
    [Adresse]           VARCHAR (50) NOT NULL,
    [Telefon]           VARCHAR (50) NOT NULL,
    [Email]             VARCHAR (50) NOT NULL,
    [ErLotteriBestyrer] BIT          NOT NULL,
    [Ark_ID] INT NOT NULL, 
    [Børnegruppe_ID] INT NOT NULL, 
    PRIMARY KEY CLUSTERED ([Leder_ID] ASC), 
    CONSTRAINT [FK_Leder_Lodseddelark] FOREIGN KEY ([Ark_ID]) REFERENCES [Lodseddelark]([Ark_ID]), 
    CONSTRAINT [FK_Leder_Børnegruppe] FOREIGN KEY ([Børnegruppe_ID]) REFERENCES [Børnegruppe]([Børnegruppe_ID])
);

CREATE TABLE [dbo].[Børnegruppe] (
    [Børnegruppe_ID] INT          NOT NULL,
    [Gruppenavn]     VARCHAR (50) NOT NULL,
    [Lokale]         VARCHAR (50) NOT NULL,
    [Antalbørn]      INT          NOT NULL,
    [Leder_ID] INT NOT NULL, 
    PRIMARY KEY CLUSTERED ([Børnegruppe_ID] ASC), 
    CONSTRAINT [FK_Børnegruppe_Leder] FOREIGN KEY ([Leder_ID]) REFERENCES [Leder]([Leder_ID])
);

CREATE TABLE [dbo].[Børn] (
    [Børn_ID] INT          NOT NULL,
    [Navn]    VARCHAR (50) NOT NULL,
    [Adresse] VARCHAR (50) NOT NULL,
    [Telefon] VARCHAR (50) NOT NULL,
    [Ark_ID]  INT          NOT NULL,
    [Børnegruppe_ID] INT NOT NULL, 
    PRIMARY KEY CLUSTERED ([Børn_ID] ASC),
    FOREIGN KEY ([Ark_ID]) REFERENCES [dbo].[Lodseddelark] ([Ark_ID]), 
    CONSTRAINT [FK_Børn_] FOREIGN KEY ([Børnegruppe_ID]) REFERENCES [Børnegruppe]([Børnegruppe_ID])
);

CREATE TABLE [dbo].[Salg] (
    [Salg_ID] INT        NOT NULL,
    [Ark_ID]  INT        NOT NULL,
    [Børn_ID] INT        NOT NULL,
    [Pris] FLOAT NOT NULL, 
    PRIMARY KEY CLUSTERED ([Salg_ID] ASC),
    FOREIGN KEY ([Børn_ID]) REFERENCES [dbo].[Børn] ([Børn_ID]), 
    CONSTRAINT [FK_Salg_Lodseddelark] FOREIGN KEY ([Ark_ID]) REFERENCES [Lodseddelark]([Ark_ID])
);

CREATE TABLE [dbo].[Indtægt] (
    [Indtægt_ID] INT        NOT NULL,
    [Dato]       DATETIME   NOT NULL,
    [Beløb]      FLOAT (53) NOT NULL,
    [Salg_ID]    INT        NOT NULL,
    PRIMARY KEY CLUSTERED ([Indtægt_ID] ASC),
    FOREIGN KEY ([Salg_ID]) REFERENCES [dbo].[Salg] ([Salg_ID])
);