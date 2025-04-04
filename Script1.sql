--OBSERVAÇÃO CRIAR O BANCO DE DADOS DBFLUXO1

USE [DBFLUXO1]
GO

CREATE TABLE dbo.TLancamentos
(
Codigo int identity(1,1) primary key,
TipoLancamento int not null,
DescricaoLancamento varchar(50) null,
ValorLancamento decimal(10,2) not null,
DataDoLancamento datetime not null,
)
GO

CREATE TABLE dbo.TConsolidados
(
Codigo int identity(1,1) primary key,
DataConsolidado datetime not null,
TotalEntrada decimal(10,2) not null,
TotalSaida decimal(10,2) not null,
ValorConsolidado decimal(10,2) not null
)
GO

INSERT INTO [dbo].[TLancamentos]
           ([TipoLancamento]
           ,[DescricaoLancamento]
           ,[ValorLancamento]
           ,[DataDoLancamento])
     VALUES
           (1,'entrada de dinheiro',1000.00,getdate());
GO


INSERT INTO [dbo].[TLancamentos]
           ([TipoLancamento]
           ,[DescricaoLancamento]
           ,[ValorLancamento]
           ,[DataDoLancamento])
     VALUES
           (2,'saida de dinheiro',1000.00,getdate());
GO


INSERT INTO dbo.TConsolidados 
values ('02/04/2025',1000,1000,0);



