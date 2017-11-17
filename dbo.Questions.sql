USE [Questions]
GO

/****** Object: Table [dbo].[Questions] Script Date: 17.11.2017 23:06:08 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Questions] (
    [Id]     INT  IDENTITY (1, 1) NOT NULL,
    [title]  TEXT COLLATE Cyrillic_General_CI_AS NULL,
    [text]   TEXT COLLATE Cyrillic_General_CI_AS NULL,
    [pos]    INT  NOT NULL,
    [answer] BIT  NOT NULL,
    [other]  BIT  NOT NULL,
    [parent] INT  NOT NULL
);


