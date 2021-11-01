create database PelisDisney

use PelisDisney

create table Personaje(
Nombre varchar(50) primary key not null,
Edad int,
Peso float,
Historia varchar(Max),
Imagen image
);

create table Genero(
Nombre varchar(20) primary key not null,
Imagen image
);

create table Film(
Titulo varchar(50) primary key not null,
Fecha date,
Clasificacion int,
Imagen image,
GeneroN varchar(20) not null,
CONSTRAINT fk_GeneroN FOREIGN KEY (GeneroN) REFERENCES Genero(Nombre)
);

create table PersonajeFilm(
ID int primary key not null,
NPersonaje varchar(50) not null,
NFilm varchar(50) not null,
CONSTRAINT fk_Personaje FOREIGN KEY (NPersonaje) REFERENCES Personaje(Nombre),
CONSTRAINT fk_Film FOREIGN KEY (NFilm) REFERENCES Film(Titulo)
);
create table Usuario(
Nombre varchar(20) primary key not null,
Contraseña varchar(20) not null,
Email varchar(30) not null
);