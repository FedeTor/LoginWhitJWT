## 📂 **_Proyecto de Microservicios - Login y User API_**

Este proyecto consiste en dos microservicios: **Login API** y **User API**, los cuales son servicios independientes que se comunican entre sí a través de solicitudes HTTP. 
La aplicación sigue los principios de **Programación Orientada a Objetos (POO)** y las **buenas prácticas de desarrollo**, utilizando **JWT** para la autenticación y autorización de 
los usuarios.

🛠️ **_Tecnologías Utilizadas_**

- **Lenguaje**: C# (.NET Core)
- **JWT (JSON Web Token)** - Autenticación basada en tokens para la seguridad.
- **Framework**: ASP.NET Core Web API
- **ORM**: Entity Framework Core para la gestión de la base de datos.
- **Base de Datos**: PostgreSQL.
- **Swagger** - Para documentar las APIs y probarlas de manera interactiva.
- **Logger**: SeriLog para la gestión de logs.
- **Inyección de Dependencias**: Gestión nativa de .NET Core

🎨 **_Patrones de Diseño y Arquitectura_**

Este proyecto incorpora patrones de diseño y principios para mantener el código limpio, escalable y fácil de mantener:

- **Dependency Injection**: Gestiona las dependencias entre componentes y servicios.
- **DTOs (Data Transfer Objects)**: Facilitan el transporte de datos entre capas, protegiendo la integridad de las entidades.
- **Error Handling y Logging**: Estructura la gestión de errores y logs, optimizando la detección y solución de problemas en producción. En cada una de las apis se utiliza la gestion de errores de manera diferente, la eleccion adecuada depende del negocio y sus necesidades.
- **Factory Method**: Controla la creación de instancias mediante métodos especializados como `Create`, encapsulando la lógica de inicialización y permitiendo flexibilidad en la configuración de los objetos. Este patrón desacopla el proceso de creación de instancias, facilita la extensibilidad y respeta principios como el Open/Closed Principle y el Single Responsibility Principle.
- **Repository Design Pattern**: Permite separar la lógica de acceso a datos de la lógica de negocio, mejorando así la flexibilidad y mantenibilidad del código.
- **Principios SOLID**:
  - **S**ingle Responsibility Principle (SRP): Cada clase tiene una única responsabilidad, facilitando el mantenimiento y la comprensión del código.
  - **O**pen/Closed Principle (OCP): Las clases están abiertas a la extensión pero cerradas a la modificación, permitiendo agregar funcionalidades sin alterar el código existente.
  - **L**iskov Substitution Principle (LSP): Interfaces e implementaciones que son intercambiables sin romper el funcionamiento del sistema.
  - **I**nterface Segregation Principle (ISP): Las interfaces se dividen en contratos específicos para que las clases no implementen métodos que no necesitan.
  - **D**ependency Inversion Principle (DIP): Dependencias abstraídas e inyectadas, promoviendo el desacoplamiento.


🏛️ **_Arquitectura_**

El proyecto está diseñado con una arquitectura modular DDD basada en Clean Architecture, que separa la lógica de negocio (Domino y Aplicación) de la infraestructura y la capa de presentación. 
Esto facilita el mantenimiento y escalabilidad de la aplicación.

**Las capas principales incluyen**:

- Domain: Entidades y lógica de negocio.
- Application: Lógica de aplicación, patrones de diseño.
- Infrastructure: Configuración de acceso a bases de datos y lógica específica del proveedor.
- Presentation: Exposición de la API mediante controladores.

👨‍🏫 **_Buenas Prácticas Implementadas_**

- **Principios SOLID**: Código modular, con baja dependencia entre clases y alta cohesión.
- **POO** (Programación Orientada a Objetos): Uso de abstraccion, encapsulamiento, herencia, polimorfismo, composición, inversión de dependencias, para crear componentes reutilizables y flexibles.
- **DRY** (Don't Repeat Yourself): Minimiza la repetición innecesaria de código.
- **Resumen de Reglas OWASP Implementadas:**
- **Validación de datos**: Se valida toda la entrada del usuario para garantizar que los datos sean correctos y evitar inyecciones de código.
- **Autenticación con JWT** y **Cifrado de datos sensibles**, utilizando claves fuertes y expiración corta.
- **Configuración segura**: evitando la exposición de datos sensibles, especialmente claves y configuraciones.
- **Registro y monitoreo adecuado** para detectar y responder a incidentes de seguridad.


🗃️ **_Base de Datos_**

La base de datos predeterminada es PostgreSQL.

Dentro de la carpeta "src/Documentation" se encuentra el script "SQLQuery" para crear la base de datos de forma manual con las tablas correspondientes, opcionalmente se puede hacer mediante un enfoque Code First.

- **Enfoque Code First**: A continuación se describen los pasos para configurar y migrar la base de datos:

**Requisitos Previos**
- Las entidades y el DbContext ya se encuentran definidas.
- Verificar que la configuración de la cadena de conexión en el archivo appsettings.json sea correcta.
  
**Pasos**
- Abre PowerShell y navega a la raíz de tu solución:
- Una vez posicionado en la raíz de tu solución, ejecuta el siguiente comando de migración con las rutas correctas en PowerShell:
```
dotnet ef migrations add InitialCreate --project "src/Login/Infrastructure/Infrastructure.csproj" --startup-project "src/Login/Api.Presentation/Api.Presentation.csproj"
```
- Actualizar la base de datos: Ahora que has creado la migración, puedes aplicarla a la base de datos. Para hacerlo, ejecuta el siguiente comando:
```
dotnet ef database update --project src/Login/Infrastructure/Infrastructure.csproj --startup-project src/Login/Api.Presentation/Api.Presentation.csproj
```

## ⚙️ **_Instrucciones de Ejecución_**

**Requisitos Previos**
- .NET 8. (.NET 7.0 SDK o superior).
- PostgreSQL u otro motor de base de datos compatible.
- IDE compatible con .NET (Visual Studio o VS Code).
**Configuración del Proyecto**
- Clona el repositorio:
  ```
  https://github.com/FedeTor/LoginWhitJWT.git
  ```
- Configura la base de datos: En el archivo appsettings.json, ajusta la cadena de conexión a la base de datos.
- Ejecuta la aplicación.

**Probar la API**

La API Login documentada con Swagger estará disponible en ```https://localhost:7249/swagger```
La API User documentada con Swagger estará disponible en ```https://localhost:7275/swagger```

Además se agregó una carpeta "src/Documentation" con la coleccion de postman "LoginWhitJWT.postman_collection.json", solo queda descargarla e importarla si se desea utilizar.

## 📜 **_Estructura del Proyecto_**

El proyecto está organizado en dos carpetas principales que corresponden a los microservicios:

- **Login API**: Un servicio que valida las credenciales de los usuarios y genera tokens JWT para la autenticación.
- **User API**: Un servicio CRUD que permite la administración (ABM) de usuarios, facilitando la creación, lectura, actualización y eliminación de datos, mientras utiliza JWT para la autorización segura.

## ✅ _**Funcionamiento de las APIs**_

_**Login API**_

La **Login API** tiene como objetivo autenticar a los usuarios utilizando sus credenciales (email y password), y luego genera un **JWT** que se usa para la autorización en 
otras API del sistema.

**Endpoints**

- **POST ```/api/login/authorize```**
  - Recibe las credenciales del usuario (email y password).
  - Verifica las credenciales contra la base de datos.
  - Si las credenciales son correctas, genera un **JWT**.
  - Devuelve el token JWT al usuario.
  
- **POST ```/api/login/refreshToken```**
  - Permite a los usuarios renovar su token JWT utilizando un **Refresh Token**.
  - El refresh token se valida y, si es correcto, se genera un nuevo JWT.

- **POST ```/api/login/revokeToken```**
  - Permite revocar un token JWT en caso de que el usuario cierre sesión o quiera invalidar su sesión actual.
 
_**User API**_

La **User API** proporciona un conjunto de operaciones para gestionar usuarios. Esta API requiere que las solicitudes estén autenticadas a través de un **JWT** para poder acceder a 
los endpoints de usuario.

**Endpoints**
- **GET ```/api/user/all```**: Obtiene todos los usuarios
  - Obtiene todos los usuarios.
- **GET ```/api/user/get```**: Obtiene un usuario por ID
  - Obtiene un usuario por ID.
- **POST ```/api/user/create```**: Agrega un nuevo usuario
  - Agrega un nuevo usuario.
  - Nota: Este endpoint no requiere autorización. El usuario puede crear un nuevo usuario sin necesidad de un JWT, lo cual es útil para realizar pruebas de la API.
- **PUT ```/api/user/update```**: Actualiza un usuario existente
  - Actualiza un usuario existente.
- **DELETE ```/api/user/delete```**: Elimina de forma lógica un usuario
  - Elimina de forma lógica un usuario.

**Autenticación y autorización**
- Para acceder a los endpoints de la User API (excepto para crear usuarios), es necesario incluir un JWT en la cabecera de la solicitud. La estructura correcta para ingresar el token es la siguiente:
```
Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJJZFVzZXIiOiIyIiwibmJmIjoxNzM3OTI4MDUwLCJleHAiOjE3Mzc5MzE2NTAsImlhdCI6MTczNzkyODA1MH0.jz5oqnLX6EMwB0aqWUYwzmqNJcp5ulet4EZfs__LP84
```

**Configuración de Swagger**
- Swagger está configurado para realizar las pruebas necesarias de la Login API y User API.

  - Login API: Swagger permite probar los endpoints de autorización, refresco de token y revocación de token sin necesidad de configurar manualmente el token.
  - User API: Para los endpoints de la User API, solo es necesario incluir el JWT en el encabezado de la solicitud como se indica arriba, y Swagger maneja la autorización automáticamente para realizar las pruebas.
