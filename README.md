# Tienda Don Pedro

Proyecto ejemplo para POO. CRUD de Productos con ASP.NET Core Web API, Entity Framework Core, MySQL, REST con Swagger y GraphQL con HotChocolate.

## Objetivo

Mostrar cómo se organiza una Web API con las siguientes capas:

- Models: Entidades que representan la tabla de la base de datos
- Data: DbContext y configuración de EF Core
- DTOs: Objetos para recibir y enviar datos
- Services: Lógica del CRUD
- Controllers: Endpoints HTTP (REST)
- GraphQL: Queries y Mutations para acceder a los datos con GraphQL
- appsettings.json: Cadena de conexión

## Estructura

```
Tienda-don-Pedro/
├── Controllers/        Endpoints REST (GET, POST, PUT, DELETE)
├── Data/               AppDbContext
├── DTOs/               CrearProductoDto, ActualizarProductoDto, ProductoDto
├── GraphQL/            Consultas y mutaciones con HotChocolate
│   ├── Queries/        ProductoQuery.cs
│   └── Mutations/      ProductoMutation.cs
├── Models/             Producto
├── Properties/         launchSettings.json
├── Services/           IProductoService y ProductoService
├── appsettings.json    Cadena de conexión a MySQL
├── Program.cs          Configuración de la app (REST + Swagger + GraphQL)
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
    "DefaultConnection": "Server=localhost;Port=3306;Database=TiendaPedro;User=tiendapedro;Password=tu_contraseña;"
  }
}
```

Explicación de los campos:

- Server: Dirección del servidor MySQL (localhost para local)
- Port: Puerto (por defecto 3306)
- Database: Nombre de la base de datos
- User: Usuario de MySQL con permisos sobre la base
- Password: Contraseña del usuario

### 3) Configurar Program.cs

En `Program.cs` se lee esa cadena y se conecta con Pomelo:

```csharp
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));
```

`ServerVersion.AutoDetect` detecta la versión de MySQL automáticamente para generar las consultas correctas con EF Core.

### 4) AppDbContext

En `Data/AppDbContext.cs` se define el DbSet que mapea la entidad `Producto` con la tabla `Productos`.

## Paquetes necesarios

Ya están en el .csproj:

- Microsoft.EntityFrameworkCore
- Microsoft.EntityFrameworkCore.Tools
- Pomelo.EntityFrameworkCore.MySql
- Swashbuckle.AspNetCore
- HotChocolate.AspNetCore
- HotChocolate.Data.EntityFramework

## Cómo correr el proyecto

Teniendo MySQL levantado, base `TiendaPedro` creada y `appsettings.json` con el usuario/contraseña correctos:

```bash
cd /home/roque/Tienda-don-Pedro
dotnet restore
dotnet ef migrations add Inicial
dotnet ef database update
dotnet run
```

Al ejecutar `dotnet ef database update` se crea la tabla `Productos` en la base `TiendaPedro`.

## Probar REST con Swagger

Al levantar la app abrir:

```text
http://localhost:5270/swagger
```

## Endpoints REST

| Método | Ruta | Acción |
| --- | --- | --- |
| GET | api/productos | Listar todos los productos |
| GET | api/productos/{id} | Obtener un producto por ID |
| POST | api/productos | Crear un producto |
| PUT | api/productos/{id} | Actualizar un producto |
| DELETE | api/productos/{id} | Eliminar un producto |

## Probar GraphQL con HotChocolate

Al levantar la app abrir:

```text
http://localhost:5270/graphql
```

Se abre Banana Cake Pop para probar queries y mutations.

### Query para listar productos

```graphql
query {
  productos {
    id
    nombre
    precio
  }
}
```

### Query para obtener un producto por ID

```graphql
query {
  producto(id: 1) {
    id
    nombre
    precio
  }
}
```

### Mutation para crear un producto

```graphql
mutation {
  crearProducto(input: {
    nombre: "Gaseosa"
    precio: 850.50
  }) {
    id
    nombre
    precio
  }
}
```

### Mutation para actualizar un producto

```graphql
mutation {
  actualizarProducto(id: 1, input: {
    nombre: "Gaseosa 2L"
    precio: 950.00
  }) {
    id
    nombre
    precio
  }
}
```

### Mutation para eliminar un producto

```graphql
mutation {
  eliminarProducto(id: 1)
}
```
