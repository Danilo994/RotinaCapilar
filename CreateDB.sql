-- DB RotinaCapilar
CREATE DATABASE RotinaCapilar;
GO

USE RotinaCapilar;
GO

-- Produtos
CREATE TABLE Produtos (
	IdProduto INT PRIMARY KEY IDENTITY(1,1),
	NomeProduto NVARCHAR(255) NOT NULL
);

-- Tipo Lavagem
CREATE TABLE Lavagem (
	IdLavagem INT PRIMARY KEY IDENTITY(1,1),
	NomeLavagem NVARCHAR(255) NOT NULL
);

-- CuidadoDia
CREATE TABLE Cuidado (
	IdCuidado INT PRIMARY KEY IDENTITY(1,1),
	IdLavagem INT NOT NULL,
	DataCuidado DATETIME NOT NULL,
	DataCriacao DATETIME NOT NULL,
	DataModificacao DATETIME NULL,
	CONSTRAINT FK_Cuidado_Lavagem FOREIGN KEY (IdLavagem) REFERENCES Lavagem(IdLavagem)
)

CREATE TABLE CuidadoProdutos (
	IdCuidadoProdutos INT PRIMARY KEY IDENTITY(1,1),
	IdCuidado INT,
	IdProduto INT,
	CONSTRAINT FK_CuidadoProdutos_Cuidado FOREIGN KEY (IdCuidado) REFERENCES Cuidado(IdCuidado),
	CONSTRAINT FK_CuidadoProdutos_Produto FOREIGN KEY (IdProduto) REFERENCES Produtos(IdProduto)
);

CREATE TABLE Avaliacao (
    IdAvaliacao INT PRIMARY KEY IDENTITY(1,1),
    IdCuidado INT NOT NULL,
    Nota INT CHECK (Nota BETWEEN 1 AND 10),
    Observacao NVARCHAR(500) NULL,
    DataAvaliacao DATETIME NOT NULL,
    CONSTRAINT FK_Avaliacao_Cuidado FOREIGN KEY (IdCuidado) REFERENCES Cuidado(IdCuidado)
);

CREATE TABLE Foto (
    IdFoto INT PRIMARY KEY IDENTITY(1,1),
    IdCuidado INT NOT NULL,
    UrlImagem NVARCHAR(300) NOT NULL,
    Descricao NVARCHAR(200) NULL,
    DataUpload DATETIME NOT NULL,
    CONSTRAINT FK_Foto_Cuidado FOREIGN KEY (IdCuidado) REFERENCES Cuidado(IdCuidado)
);