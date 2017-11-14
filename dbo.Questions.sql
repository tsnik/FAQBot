USE [Questions]
GO

/****** Object: Table [dbo].[Questions] Script Date: 14.11.2017 11:24:23 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Questions] (
    [Id]     INT  IDENTITY (1, 1) NOT NULL,
    [title]  TEXT NULL,
    [text]   TEXT NULL,
    [pos]    INT  NOT NULL,
    [answer] BIT  NOT NULL,
    [other]  BIT  NOT NULL,
    [parent] INT  NOT NULL
);


