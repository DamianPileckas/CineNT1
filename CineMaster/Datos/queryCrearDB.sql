/*CREATE TABLE Cliente(
    Cliente_ID int IDENTITY (1,1) PRIMARY KEY,
    Nombre_Cliente varchar(255),
    Email varchar(255),
    Nro_Tarjeta varchar(255),
    Contrasenia varchar(255)
	 
);*/

CREATE TABLE Sala (
    Sala_ID int IDENTITY (1,1) PRIMARY KEY,
    Numero_Sala varchar(255),
);

CREATE TABLE Pelicula (
    Pelicula_ID int IDENTITY (1,1) PRIMARY KEY,
    Nombre_Pelicula varchar(255),
    Genero varchar(255),
    Descripcion varchar(255),
    Duracion varchar(255),
);


CREATE TABLE Entrada (
    Entrada_ID int IDENTITY (1,1) PRIMARY KEY,
    Asiento varchar(255),
    Fecha varchar(255),
    Hora varchar(255),
    Sala integer NOT NULL CONSTRAINT FK_Sala_SalaID REFERENCES Sala(Sala_ID),
	Pelicula integer NOT NULL CONSTRAINT FK_Pelicula_PeliculaID REFERENCES Pelicula(Pelicula_ID)
);

CREATE TABLE Compra (
    Compra_ID int IDENTITY (1,1) PRIMARY KEY,
	Entrada integer NOT NULL CONSTRAINT FK_Entrada_EntradaID REFERENCES Entrada(Entrada_ID),
  	Cliente integer NOT NULL CONSTRAINT FK_Cliente_ClienteID REFERENCES Cliente(Cliente_ID)
);