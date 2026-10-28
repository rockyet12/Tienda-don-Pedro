# Tienda Don Pedro

Proyecto ejemplo para POO. CRUD simple de Productos con ASP.NET Core Web API, Entity Framework Core, MySQL y Swagger.

## Objetivo

Mostrar cómo se organiza una Web API con las siguientes capas:

- Models: Entidades que representan la tabla de la base de datos
- Data: DbContext y configuración de EF Core
- DTOs: Objetos para recibir y enviar datos
- Services: Lógica del CRUD
- Controllers: Endpoints HTTP
- appsettings.json: Cadena de conexión

## Estructura

```
Tienda-don-Pedro/
├── Controllers/        Endpoints (GET, POST, PUT, DELETE)
├── Data/               AppDbContext
├── DTOs/               CrearProductoDto, ActualizarProductoDto, ProductoDto
├── Models/             Producto
├── Properties/         launchSettings.json
├── Services/           IProductoService y ProductoService
├── appsettings.json    Cadena de conexión a MySQL
├── Program.cs          Configuración de la app
└── Tienda-don-Pedro.csproj
```

## Conexión a la base de datos

Este proyecto usa MySQL con el paquete Pomelo.EntityFrameworkCore.MySql.

### 1) Crear la base de datos

Antes de correr las migraciones, crear la base de datos en MySQL:

```sql
CREATE DATABASE TiendaPedro;
```

### 2) Configurar la cadena de conexión

Editar `appsettings.json`:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost;Port=3306;Database=TiendaPedro;User=root;Password=tu_contraseña;"
  }
}
```

Explicación de los campos:

- Server: Dirección del servidor MySQL
- Port: Puerto (por defecto 3306)
- Database: Nombre de la base de datos
- User: Usuario de MySQL
- Password: Contraseña del usuario

### 3) Configurar Program.cs

En `Program.cs` se lee esa cadena y se conecta con Pomelo:

```csharp
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));
```

`ServerVersion.AutoDetect` detecta la versión de MySQL automáticamente. Con eso EF Core sabe cómo generar las consultas.

## Paquetes necesarios

Ya están en el .csproj:

- Microsoft.EntityFrameworkCore
- Microsoft.EntityFrameworkCore.Tools
- Pomelo.EntityFrameworkCore.MySql
- Swashbuckle.AspNetCore

## Cómo correr el proyecto

Desde la carpeta raíz:

```bash
dotnet restore
dotnet ef migrations add Inicial
dotnet ef database update
dotnet run
```

Al ejecutar `dotnet ef database update` se crea la tabla `Productos` en la base `TiendaPedro`.

## Probar con Swagger

Al levantar la app abrir:

```
http://localhost:5270/swagger
```

o

```
https://localhost:7093/swagger
```

## Endpoints

| Método | Ruta | Acción |
| --- | --- | --- |
| GET | api/productos | Listar todos los productos |
| GET | api/productos/{id} | Obtener un producto por ID |
| POST | api/productos | Crear un producto |
| PUT | api/productos/{id} | Actualizar un producto |
| DELETE | api/productos/{id} | Eliminar un producto |
```
