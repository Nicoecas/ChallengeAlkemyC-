Para que la API funcione se debe:

1. Crear las tablas dentro del documento "sql Disney Tablas.txt"

2. Cargarla de datos dentro del documento "Datos para la BD.txt"

3.Se debe utilizar SqlServer y cambiar la linea 31 del archivo "Models/PelisDisneyContext.cs" por sus propios datos:

optionsBuilder.UseSqlServer("Data source=[servidor]; Initial Catalog=[Basededatos]; user id=[Usuario]; password=[Contraseña];");

4. Luego cargar los mimos datos del punto 3 en la linea 5 del archivo "appsettings.json":

"ConnectionStrings": { "ConectionDB": "Data source=[servidor]; Initial Catalog=[Basededatos]; user id=[Usuario]; password=[Contraseña];" },

PD: Todos los datos que se carguen deben ser en el body, las vistas de carga estan desabilitadas para facilitar el testeo.

PD2: Los GETs serán enviados en vistas y no en formato JSON.



CUMPLIMIENTO DE LOS REQUERIMIENTOS TECNICOS

1. Modelo de base de datos (cumplido)



2. Autenticacion de usuarios.

para ingresar a la autenticación de usuario se debe dirigir a los endpoits

"/auth/register" para registrarse

"/auth/login" para loggearse (guardarse el token generado)

El login se debe cargar
{
    "Nombre":"[nombredelusuario]",
    "Contraseña":"[contraseña]"
}

3. Listado de Personajes
endpoints:

"/characters"



4.Creación, edición y eliminación de Personajes (CRUD)
Enpoints
Para Crear: "characters/Create"
Para Editar: "characters/Edit/[nombredelpersonaje]"
Para Eliminar: "characters/Delete/[nombredelpersonaje]"



5.Detalle del Personaje
endpoints:

"/characters/name/[nombredelpersonaje]"
(en caso de poseer espacios completar el endpoint con "_" )


6.Busqueda de Personaje
endpoints:

"/characters/name/[nombredelpersonaje]"
"/characters/age/[Intedad]"



7.Listado de Peliculas
endpoints:

"/movies"


8.Detalle de Pelicula/serie con sus personajes (incumplido)



9.Creacion, Edicion y Eliminacion de Pelicula/serie
endpoints:

Para Crear: "movies/Create"
Para Editar: "movies/Edit/[nombredelpelicula]"
Para Eliminar: "movies/Delete/[nombredepelicula]"



10.Busqueda de Peliculas o Series
endpoints:

"/movies/name/[nombredepelicula]"
"/movies/genre/[generodepelicula]"
"/movies/order/[asd o desc]"
(en caso de poseer espacios completar el endpoint con "_" )


11. Envio de emails
No cuento con cuenta en SendGrid ya que no poseo una empresa vàlida, sin embargo, la API
envia un Mail a la casilla la cual se registre de forma particular.