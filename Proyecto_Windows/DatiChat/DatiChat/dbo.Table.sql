CREATE TABLE [dbo].[Mensajes]
(
	[Id] INT NOT NULL PRIMARY KEY, 
    [User_from] INT NOT NULL, 
    [User_to] INT NOT NULL, 
    [Message ] NVARCHAR(MAX) NOT NULL, 
    [DateTime ] DATETIME NOT NULL
)
