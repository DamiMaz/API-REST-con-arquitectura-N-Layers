# NLayers Architecture - API REST en C# .NET 9

Plantilla de arquitectura limpia basada en capas (**N-Layers**) para aplicaciones ASP.NET Core Web API en .NET 9, implementada siguiendo la guía técnica oficial de diseño en capas.

---

## 🏛️ Estructura de la Solución

```text
NLayersArchitecture/
├── backend/
│   └── NLayers.Api/                   # Host / Composition Root (Web API)
│       ├── Program.cs                 # Configuración de DI, DbContext, Swagger y Middlewares
│       ├── appsettings.json           # Cadenas de conexión y configuración
│       └── appsettings.Development.json
│
├── core/
│   └── NLayers.Core/                  # Funcionalidades transversales y compartidas
│       ├── Constants/                 # Constantes globales
│       ├── Helpers/                   # PasswordHelper, TokenHelper, MappingHelper
│       └── Utils/                     # DateUtils, StringUtils, ValidationUtils
│
└── layers/
    ├── NLayers.Presentation/          # Capa de Entrada / Exposición HTTP
    │   ├── Base/BaseController.cs     # Controlador base con formato estándar (code, data, message)
    │   ├── Controllers/               # Controladores de la API (ProductController)
    │   ├── Inputs/                    # DTOs de entrada (ProductCreateInput, ProductUpdateInput)
    │   └── Outputs/                   # DTOs de salida (ProductOutput)
    │
    ├── NLayers.BusinessLogic/         # Capa de Lógica y Reglas de Negocio
    │   ├── Interfaces/                # Contratos de negocio (IProductManager)
    │   ├── Managers/                  # Coordinación y reglas (ProductManager)
    │   └── Services/                  # Servicios especializados (EmailService)
    │
    ├── NLayers.DataAccess/            # Capa de Persistencia y Acceso a Datos
    │   ├── AppDbContext.cs            # DbContext con Entity Framework Core 9
    │   ├── Interfaces/                # Contratos de acceso a datos (IProductStore)
    │   └── Stores/                    # Implementación de acceso a DB (ProductStore)
    │
    └── NLayers.Entities/              # Capa de Dominio Puro
        ├── Entities/                  # Modelos anémicos de dominio (Product)
        └── Enums/                     # Enumeraciones de la aplicación (ProductStatus)
```

---

## 🔄 Flujo de Datos

```text
Cliente HTTP
    │  (POST /api/products con JSON)
    ▼
NLayers.Api (Punto de entrada / Routing)
    ▼
ProductController (NLayers.Presentation)
    │  (Mapea JSON a ProductCreateInput)
    ▼
ProductManager (NLayers.BusinessLogic)
    │  (Valida reglas de negocio: precio > 0, nombre requerido)
    ▼
ProductStore (NLayers.DataAccess)
    │  (Invoca Entity Framework Core)
    ▼
AppDbContext / Base de Datos
    │  (Persiste el registro y genera el Id)
    ▼
ProductStore ➔ ProductManager ➔ ProductController
    │  (Convierte entidad a ProductOutput DTO)
    ▼
Respuesta HTTP JSON estandarizada:
{
  "code": 201,
  "data": {
    "id": 1,
    "name": "Teclado",
    "price": 25000,
    "status": 1
  },
  "message": "El producto se creó exitosamente"
}
```

---

## 🧭 Reglas de Dependencias

> **Regla fundamental**: Una capa no debe depender de una capa superior.

- `Entities` no depende de ninguna otra capa de la solución.
- `Core` contiene utilidades y helpers transversales.
- `DataAccess` solo conoce `Entities` y `Core`.
- `BusinessLogic` conoce `DataAccess`, `Entities` y `Core`.
- `Presentation` conoce `BusinessLogic`, `Entities` y `Core`.
- `Api` es el **Composition Root**: referencia a todas las capas para configurar el Contenedor de Inyección de Dependencias y la canalización HTTP.

---

## 🚀 Cómo Iniciar el Proyecto

### 1. Requisitos Previos
- [.NET 9 SDK](https://dotnet.microsoft.com/download/dotnet/9.0) instalado.

### 2. Compilar la Solución
```bash
dotnet build NLayers.sln
```

### 3. Ejecutar la API
```bash
dotnet run --project backend/NLayers.Api/NLayers.Api.csproj
```

Una vez iniciada, abre Swagger en tu navegador:
- **Swagger UI**: `http://localhost:5186/swagger`

> **Nota para desarrollo local**: Si la cadena de conexión de SQL Server en `appsettings.Development.json` está vacía o apunta a un servidor inexistente, la aplicación utiliza automáticamente `UseInMemoryDatabase` para que puedas probar todos los endpoints CRUD inmediatamente sin dependencias externas.

---

## 📦 Conectar con tu Repositorio Remoto en Git (GitHub / GitLab / Bitbucket)

El proyecto ya está inicializado localmente con Git y tiene su commit listo. Para subirlo a tu repositorio remoto:

1. Crea un repositorio vacío en GitHub o tu plataforma favorita (sin inicializar con README ni .gitignore).
2. Ejecuta los siguientes comandos en la terminal desde la raíz de este proyecto:

```bash
git remote add origin https://github.com/TU_USUARIO/TU_REPOSITORIO.git
git branch -M main
git push -u origin main
```
