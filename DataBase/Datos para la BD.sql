Insert into Usuario
(Nombre, Contraseña, Email)
values ('nico','1234','nico@gmail.com'),
('Ezequiel','asd','ezequiel@gmail.com'),
('castillo','qwerty','castillo@gmail.com');

insert into Genero
(Nombre,Imagen)
values ('Fantasia',null),
('Aventura',null);


insert into Film
(Titulo,Fecha,Clasificacion,Imagen,GeneroN)
values ('La Sirenita',null,'4',null,'fantasia'),
('La Lampara de Aladin',null,'5',null,'fantasia'),
('Buscando a Nemo',null,'5',null,'Aventura');

insert into Personaje
(Nombre,Edad,Peso,Historia,Imagen)
values ('Aladin','20','50','Habia una vez 1',null),
('Ariel','15','45','Habia una vez 2',null),
('Nemo','1','5','Habia una vez...',null),
('Dori','1','1','Habia una vez',null);

insert into PersonajeFilm
(ID,NPersonaje,Nfilm)
values ('1','Aladin','La Lampara de Aladin'),
('2','Nemo','Buscando a Nemo'),
('3','Ariel','La Sirenita'),
('4','Dori','Buscando a Nemo');