# Examen Técnico Bansi - Gestión de Exámenes

Solución desarrollada como prueba técnica, implementando un mantenimiento de catálogo (CRUD) para la entidad Examen. El proyecto está construido bajo los principios de Clean Architecture y prácticas de .NET 8, asegurando un código escalable, mantenible y desacoplado.

## 🚀 Tecnologías y Arquitectura

- **Framework:** .NET 8
- **Arquitectura:** Clean Architecture (Domain-Centric) / Patrón Repositorio
- **Base de Datos:** SQL Server (Entity Framework Core 8 + Stored Procedures)
- **Validaciones:** FluentValidation
- **Frontend:** ASP.NET Core MVC (Consumiendo el API mediante `HttpClient`)
- **Backend:** ASP.NET Core Web API

## 🏗️ Estructura de la Solución

La solución está dividida en proyectos físicos para garantizar límites estrictos de dependencia:

1. **`Bansi.Domain`**: El núcleo del sistema. Contiene las entidades puras (`Examen`) y las abstracciones (`IExamenRepository`). No tiene dependencias externas.
2. **`Bansi.Application`**: Contiene la lógica de negocio, validaciones (`FluentValidation`), los DTOs para evitar el *Over-posting* y el servicio principal (`clsExamen` / `IExamenService`).
3. **`Bansi.Infrastructure`**: Encargada de la persistencia de datos. Implementa el `DbContext` utilizando *Fluent API* e incluye dos implementaciones del repositorio: una puramente transaccional con Entity Framework y otra ejecutando Procedimientos Almacenados.
4. **`Bansi.Api`**: Web API RESTful que expone los endpoints, delegando toda la responsabilidad a la capa de Aplicación.
5. **`Bansi.WebUi`**: Interfaz de usuario MVC desarrollada cumpliendo las reglas visuales requeridas (DataGrid con colores intercalados) y con la capacidad de alternar dinámicamente la tecnología de guardado.

## ✨ Características Destacadas (Decisiones de Diseño)

- **Inyección de Dependencias Avanzada:** Para resolver el requerimiento de alternar entre Entity Framework y Stored Procedures, se implementaron *Keyed Services* permitiendo a la capa de aplicación decidir dinámicamente qué repositorio invocar sin acoplarse a la infraestructura.
- **Segregación de DTOs:** Se separaron los modelos de lectura y escritura (`ExamenInputDto` vs `ExamenDto`) para proteger el campo autoincrementable `IdExamen` durante las inserciones.
- **Validación Intermedia:** Las reglas de negocio y longitudes de campos no ensucian los controladores ni la base de datos; son gestionadas por `FluentValidation` en la capa de Aplicación.
- **Code-First + Procedimientos Almacenados:** La migración BansiDBInitialCreate de EF Core fue personalizada para que al ejecutarse, no solo cree la tabla `tblExamen`, sino que también inyecte los scripts de los Procedimientos Almacenados (`spAgregar`, `spActualizar`, `spEliminar`, `spConsultar`).

## ⚙️ Instrucciones de Ejecución

### 1. Requisitos Previos
- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- SQL Server (LocalDB, Express o Developer)
- Visual Studio 2022 o compatible

### 2. Configurar la Base de Datos
La aplicación utiliza *Code-First*. La base de datos, la tabla y los Stored Procedures se generarán automáticamente.
1. Abre el archivo `appsettings.json` en el proyecto **`Bansi.Api`**.
2. Verifica que la cadena de conexión `BansiDB` apunte a tu servidor SQL local.
3. Abre la Consola del Administrador de Paquetes en Visual Studio.
4. Selecciona `Bansi.Infrastructure` como proyecto predeterminado.
5. Ejecuta el comando:
   ```powershell```
   Update-Database

### 3. Ejecutar la Aplicación
El sistema requiere que tanto el API como la interfaz MVC estén corriendo simultáneamente.
1. Haz clic derecho sobre la Solución en Visual Studio -> Propiedades.
2. Selecciona Proyectos de inicio múltiples.
3. Cambia la acción de `Bansi.Api` y `Bansi.WebUi` a Iniciar. 
(Opcional) Verifica en Program.cs de Bansi.WebUi que el puerto del HttpClient coincida con el puerto asignado a tu API local.

Presiona F5 o el botón de Iniciar.


Autor: César Ramos Pérez
Desarrollado para la evaluación técnica de Selección Bansi.