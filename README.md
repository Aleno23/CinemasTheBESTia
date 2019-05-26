
# Cinemas The BESTia -Alex Edwin Velez

### Configuración requerida

Para la ejecución de l solución se debe configurar las cadenas de conexión en dos proyectos.

#####  - CinemasTheBESTia.CinemaBooking.API
#####  - CinemasTheBESTia.IdentityServer


En la raiz de ambos proyectos existe el archivo 'appsettings.json', en este se deben ingresar los datos para la creación de las bases de datos

```sh
"DefaultConnection": "{server};Database=BookingBD; Trusted_Connection=True;MultipleActiveResultSets=true; User Id={user}; Password={password}"
```

Reemplazando {server}, {user} y password.

### Consideraciones

  - Se consumió el API https://api.themoviedb.org/3/movie/{0}?api_key={1} para obtener los detalles de la pelicula
  - Se implementó Identity Server para manejar la autenticación del cliente MVC
  - Las funciones se ingresan automaticamente, cuando el usuario selecciona los detalles de la pelicula
 - Todas las funciones tienen el mismo precio y cantidad de asientos, esto puede ser configurado en el appSettings.Json de CinemasTheBESTia.CinemaBooking.API
```sh
"CinemaFunctions": {
    "SeatsNumber": "10",
    "BasePrice": "8000"
  },- 
```
 
  - El cinema ofrece funciones todos los dias a las 3 ,  6  y 9 pm
  - No se implementó seguridad para consumir las API's 

