CREATE TABLE [dbo].[Patient] (
    [Id]        INT            IDENTITY (1, 1) NOT NULL,
    [FistName]  NVARCHAR (50)  NULL,
    [LastName]  NVARCHAR (50)  NULL,
    [Address]   NVARCHAR (500) NULL,
    [Gender]    NCHAR (100)    NULL,
    [Document]  NVARCHAR (13)  NULL,
    [Phone]     NVARCHAR (11)  NULL,
    [CreatedAt] DATETIME       NULL,
    [IsDeleted] BIT            NULL,
    CONSTRAINT [PK_Patient] PRIMARY KEY CLUSTERED ([Id] ASC)
);

