# NLayers Architecture - API REST en C# .NET 9

Plantilla de arquitectura limpia basada en capas (**N-Layers**) para aplicaciones ASP.NET Core Web API en .NET 9, implementada siguiendo la guÃ­a tÃ©cnica oficial de diseÃ±o en capas.

---

## ðŸ›ï¸ Estructura de la SoluciÃ³n

```text
NLayersArchitecture/
â”œâ”€â”€ backend/
â”‚   â””â”€â”€ NLayers.Api/                   # Host / Composition Root (Web API)
â”‚       â”œâ”€â”€ Program.cs                 # ConfiguraciÃ³n de DI, DbContext, Swagger y Middlewares
â”‚       â”œâ”€â”€ appsettings.json           # Cadenas de conexiÃ³n y configuraciÃ³n
â”‚       â””â”€â”€ appsettings.Development.json
â”‚
â”œâ”€â”€ core/
â”‚   â””â”€â”€ NLayers.Core/                  # Funcionalidades transversales y compartidas
â”‚       â”œâ”€â”€ Constants/                 # Constantes globales
â”‚       â”œâ”€â”€ Helpers/                   # PasswordHelper, TokenHelper, MappingHelper
â”‚       â””â”€â”€ Utils/                     # DateUtils, StringUtils, ValidationUtils
â”‚
â””â”€â”€ layers/
    â”œâ”€â”€ NLayers.Presentation/          # Capa de Entrada / ExposiciÃ³n HTTP
    â”‚   â”œâ”€â”€ Base/BaseController.cs     # Controlador base con formato estÃ¡ndar (code, data, message)
    â”‚   â”œâ”€â”€ Controllers/               # Controladores de la API (ProductController)
    â”‚   â”œâ”€â”€ Inputs/                    # DTOs de entrada (ProductCreateInput, ProductUpdateInput)
    â”‚   â””â”€â”€ Outputs/                   # DTOs de salida (ProductOutput)
    â”‚
    â”œâ”€â”€ NLayers.BusinessLogic/         # Capa de LÃ³gica y Reglas de Negocio
    â”‚   â”œâ”€â”€ Interfaces/                # Contratos de negocio (IProductManager)
    â”‚   â”œâ”€â”€ Managers/                  # CoordinaciÃ³n y reglas (ProductManager)
    â”‚   â””â”€â”€ Services/                  # Servicios especializados (EmailService)
    â”‚
    â”œâ”€â”€ NLayers.DataAccess/            # Capa de Persistencia y Acceso a Datos
    â”‚   â”œâ”€â”€ AppDbContext.cs            # DbContext con Entity Framework Core 9
    â”‚   â”œâ”€â”€ Interfaces/                # Contratos de acceso a datos (IProductStore)
    â”‚   â””â”€â”€ Stores/                    # ImplementaciÃ³n de acceso a DB (ProductStore)
    â”‚
    â””â”€â”€ NLayers.Entities/              # Capa de Dominio Puro
        â”œâ”€â”€ Entities/                  # Modelos anÃ©micos de dominio (Product)
        â””â”€â”€ Enums/                     # Enumeraciones de la aplicaciÃ³n (ProductStatus)
```

---

## ðŸ”„ Flujo de Datos

```text
Cliente HTTP
    â”‚  (POST /api/products con JSON)
    â–¼
NLayers.Api (Punto de entrada / Routing)
    â–¼
ProductController (NLayers.Presentation)
    â”‚  (Mapea JSON a ProductCreateInput)
    â–¼
ProductManager (NLayers.BusinessLogic)
    â”‚  (Valida reglas de negocio: precio > 0, nombre requerido)
    â–¼
ProductStore (NLayers.DataAccess)
    â”‚  (Invoca Entity Framework Core)
    â–¼
AppDbContext / Base de Datos
    â”‚  (Persiste el registro y genera el Id)
    â–¼
ProductStore âž” ProductManager âž” ProductController
    â”‚  (Convierte entidad a ProductOutput DTO)
    â–¼
Respuesta HTTP JSON estandarizada:
{
  "code": 201,
  "data": {
    "id": 1,
    "name": "Teclado",
    "price": 25000,
    "status": 1
  },
  "message": "El producto se creÃ³ exitosamente"
}
```

---

## ðŸ§­ Reglas de Dependencias

> **Regla fundamental**: Una capa no debe depender de una capa superior.

- `Entities` no depende de ninguna otra capa de la soluciÃ³n.
- `Core` contiene utilidades y helpers transversales.
- `DataAccess` solo conoce `Entities` y `Core`.
- `BusinessLogic` conoce `DataAccess`, `Entities` y `Core`.
- `Presentation` conoce `BusinessLogic`, `Entities` y `Core`.
- `Api` es el **Composition Root**: referencia a todas las capas para configurar el Contenedor de InyecciÃ³n de Dependencias y la canalizaciÃ³n HTTP.

---

## ðŸš€ CÃ³mo Iniciar el Proyecto

### 1. Requisitos Previos
- [.NET 9 SDK](https://dotnet.microsoft.com/download/dotnet/9.0) instalado.

### 2. Compilar la SoluciÃ³n
```bash
dotnet build NLayers.sln
```

### 3. Ejecutar la API
```bash
dotnet run --project backend/NLayers.Api/NLayers.Api.csproj
```

Una vez iniciada, abre Swagger en tu navegador:
- **Swagger UI**: `http://localhost:5000/swagger` (o el puerto HTTPS asignado en consola)

> **Nota para desarrollo local**: Si la cadena de conexiÃ³n de SQL Server en `appsettings.Development.json` estÃ¡ vacÃ­a o apunta a un servidor inexistente, la aplicaciÃ³n utiliza automÃ¡ticamente `UseInMemoryDatabase` para que puedas probar todos los endpoints CRUD inmediatamente sin dependencias externas.

---

## ðŸ“¦ Conectar con tu Repositorio Remoto en Git (GitHub / GitLab / Bitbucket)

El proyecto ya estÃ¡ inicializado localmente con Git y tiene su primer commit listo. Para subirlo a tu repositorio remoto:

1. Crea un repositorio vacÃ­o en GitHub o tu plataforma favorita (sin inicializar con README ni .gitignore).
2. Ejecuta los siguientes comandos en la terminal desde la raÃ­z de este proyecto:

```bash
git remote add origin https://github.com/TU_USUARIO/TU_REPOSITORIO.git
git branch -M main
git push -u origin main
```
