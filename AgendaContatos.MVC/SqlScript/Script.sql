create table Usuario(
	IdUsuario int identity (1,1) not null,
	Nome nvarchar (200) not null,
	Email nvarchar (150) not null unique,
	Senha nvarchar (50) not null,
	DataCadastro datetime not null,
	primary key (IdUsuario)
)
GO

create table Contato(
	IdContato int identity (1,1) not null,
	IdUsuario int not null,
	Nome nvarchar (200) not null,
	Email nvarchar (150),
	Telefone1 nvarchar (20) not null,
	Telefone2 nvarchar (20),
	Observacao nvarchar (200),
	DataCadastro datetime not null,
	primary key (IdContato), 
	foreign key (IdUsuario) references Usuario (IdUsuario)
)
GO

