
# Cinemas The BESTia -Alex Edwin Velez

[![Build Status](https://dev.azure.com/aevelez/Cinemas%20The%20Bestia/_apis/build/status/Aleno23.CinemasTheBESTia?branchName=master-dev)](https://dev.azure.com/aevelez/Cinemas%20The%20Bestia/_build/latest?definitionId=2&branchName=master-dev)

### Configuración requerida

Para la ejecución de la solución se deben configurar las cadenas de conexión en dos proyectos.

#####  - CinemasTheBESTia.CinemaBooking.API
#####  - CinemasTheBESTia.IdentityServer


En la raiz de ambos proyectos existe el archivo 'appsettings.json', en este se deben ingresar los datos de conexión a SQL Server para la creación de las bases de datos. (esta creación se realiza automaticamente al iniciar la aplicación)

```sh
"DefaultConnection": "{server};Database=BookingBD; Trusted_Connection=True;MultipleActiveResultSets=true; User Id={user}; Password={password}"
```

Reemplazando {server}, {user} y {password}.

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
  - Se utilizó la librería Polly para el manejo de los reintentos y fallback
  - La solución contiene multiples proyectos de inicio, pero con el que se debe interactuar es: CinemasTheBESTia.Web.MVC https://localhost:44386/
