# CartaOnline - Sistema Web Multiempresa

Sistema web multiempresa que permite a distintos locales gastronómicos (rotiserías, restaurantes o deliveries) publicar su carta digital (menú de productos) y administrarla desde un panel web independiente por empresa. Cada empresa podrá gestionar sus categorías, productos y mostrar su carta online pública mediante una URL única.

## 🚀 Características

- **Arquitectura Multiempresa**: Aislamiento completo de datos por empresa
- **CRUD Completo**: Operaciones completas para empresas, categorías y productos
- **Carta Pública**: Visualización pública de menús por empresa
- **Frontend Modular**: Desarrollado con Angular Material
- **API REST**: Backend en ASP.NET Core con documentación Swagger

## 🛠 Tecnologías Utilizadas

### Backend
- **ASP.NET Core Web API** (.NET 6)
- **Entity Framework Core** con SQL Server
- **Swagger/OpenAPI** para documentación de API
- **C#** con patrones de diseño RESTful

### Frontend
- **Angular 17** con TypeScript
- **Angular Material** para UI/UX
- **HttpClient** para comunicación con API

### Base de Datos
- **SQL Server** con estructura relacional normalizada

## 📋 Requisitos Previos

- **.NET 6 SDK** o superior
- **Node.js** 18.x o superior
- **Angular CLI** 17.x
- **SQL Server** (Express, Developer o LocalDB)
- **Git** para control de versiones

## 🏗 Instalación y Configuración

### 1. Clonar el Repositorio

```bash
git clone https://github.com/Ezkm2024/CartaOnlineApi.git
cd CartaOnlineApi
```

### 2. Configurar la Base de Datos

1. Abrir **SQL Server Management Studio** (SSMS)
2. Ejecutar el script `database-script.sql` incluido en el proyecto
3. Verificar que se creó la base de datos `CartaOnlineDB` con datos de ejemplo

### 3. Configurar el Backend (API)

1. Navegar al directorio del backend:
   ```bash
   cd CartaOnline.API
   ```

2. Restaurar paquetes NuGet:
   ```bash
   dotnet restore
   ```

3. Configurar la cadena de conexión en `appsettings.json`:
   ```json
   {
     "ConnectionStrings": {
       "DefaultConnection": "Server=localhost;Database=CartaOnlineDB;Trusted_Connection=True;TrustServerCertificate=True;"
     }
   }
   ```

4. Ejecutar migraciones (si es necesario):
   ```bash
   dotnet ef database update
   ```

5. Ejecutar la API:
   ```bash
   dotnet run
   ```

   La API estará disponible en: `http://localhost:5000`
   Documentación Swagger: `http://localhost:5000/swagger`

### 4. Configurar el Frontend (Angular)

1. Abrir una nueva terminal y navegar al directorio del frontend:
   ```bash
   cd ../carta-online
   ```

2. Instalar dependencias:
   ```bash
   npm install
   ```

3. Configurar la URL de la API en los servicios (si es necesario):
   - Los servicios están configurados para `http://localhost:5000/api/`

4. Ejecutar la aplicación Angular:
   ```bash
   ng serve
   ```

   El frontend estará disponible en: `http://localhost:4200`

## 🧪 Pruebas del Sistema

### Panel de Administración

1. **Empresas**: `http://localhost:4200/admin/companies`
   - Crear, editar, eliminar empresas
   - Campos: Nombre, Dirección, Teléfono, Email, Logo (opcional)

2. **Categorías**: `http://localhost:4200/admin/categories`
   - Gestionar categorías por empresa
   - Campos: Nombre, Empresa (FK)

3. **Productos**: `http://localhost:4200/admin/products`
   - Administrar productos por categoría y empresa
   - Campos: Nombre, Descripción, Precio, Categoría, Empresa, Imagen (opcional)

### Carta Pública

Acceder a las cartas públicas usando:
- Por ID de empresa: `http://localhost:4200/menu/{companyId}`
- Por nombre de empresa: `http://localhost:4200/menu/company/{companyName}`

**Ejemplos de URLs públicas:**
- Rotisería El Buen Sabor: `http://localhost:4200/menu/1` o `http://localhost:4200/menu/company/Rotiseria%20El%20Buen%20Sabor`
- Restaurante La Parrilla: `http://localhost:4200/menu/2`
- Sushi Delivery: `http://localhost:4200/menu/3`

### Datos de Prueba Incluidos

El script SQL incluye 3 empresas de ejemplo con categorías y productos completos:
- **Rotisería El Buen Sabor**: Pizzas, entradas, bebidas
- **Restaurante La Parrilla**: Carnes, achuras, ensaladas, bebidas
- **Sushi Delivery**: Sushi rolls, sashimi, entradas, bebidas

## 📁 Estructura del Proyecto

```
CartaOnline/
├── CartaOnline.API/          # Backend ASP.NET Core
│   ├── Controllers/          # Controladores API REST
│   ├── Models/              # Modelos de datos
│   ├── DTOs/                # Data Transfer Objects
│   ├── Data/                # Contexto de base de datos
│   ├── database-script.sql  # Script de base de datos
│   └── appsettings.json     # Configuración
└── carta-online/            # Frontend Angular
    ├── src/app/
    │   ├── components/      # Componentes Angular
    │   ├── services/        # Servicios para API
    │   └── models/          # Modelos TypeScript
    └── angular.json         # Configuración Angular
```

## 🔧 API Endpoints

### Empresas
- `GET /api/Companies` - Listar todas las empresas
- `POST /api/Companies` - Crear empresa
- `PUT /api/Companies/{id}` - Actualizar empresa
- `DELETE /api/Companies/{id}` - Eliminar empresa

### Categorías
- `GET /api/Categories?companyId={id}` - Listar categorías por empresa
- `POST /api/Categories` - Crear categoría
- `PUT /api/Categories/{id}` - Actualizar categoría
- `DELETE /api/Categories/{id}` - Eliminar categoría

### Productos
- `GET /api/Products?companyId={id}&categoryId={id}` - Listar productos con filtros
- `POST /api/Products` - Crear producto
- `PUT /api/Products/{id}` - Actualizar producto
- `DELETE /api/Products/{id}` - Eliminar producto

### Menú Público
- `GET /api/Menu/company/{companyId}` - Obtener menú por ID de empresa
- `GET /api/Menu/company-name/{companyName}` - Obtener menú por nombre de empresa

## 🎯 Criterios de Evaluación Cumplidos

- ✅ **Diseño e implementación multiempresa (25%)**: Arquitectura con aislamiento por CompanyId
- ✅ **Funcionamiento de CRUDs (25%)**: Operaciones completas en backend y frontend
- ✅ **Visualización de carta por empresa (25%)**: URLs públicas independientes
- ✅ **Buenas prácticas, validaciones y diseño UI (25%)**: Validaciones, Material Design, estructura modular

## 📸 Capturas de Pantalla

### Panel de Administración

#### CRUD de Empresas
![CRUD de Empresas](screenshots/CRUD%20de%20Empresas.png)

#### CRUD de Categorías
![CRUD de Categorías](screenshots/CRUD%20de%20Categorías.png)

#### CRUD de Productos
![CRUD de Productos](screenshots/CRUD%20de%20Productos.png)

### Cartas Públicas

#### Carta Pública Empresa 1 - Rotisería El Buen Sabor
![Carta Pública Empresa 1](screenshots/Carta%20Pública%20Empresa%201.png)

#### Carta Pública Empresa 2 - Restaurante La Parrilla
![Carta Pública Empresa 2](screenshots/Carta%20Pública%20Empresa%202.png)

#### Carta Pública Empresa 3 - Sushi Delivery
![Carta Pública Empresa 3](screenshots/Carta%20Pública%20Empresa%203.png)

#### Carta Pública Empresa 4 - Cafetería Express
![Carta Pública Empresa 4](screenshots/Carta%20Pública%20Empresa%204.png)

## 📝 Notas Adicionales

- El sistema incluye validaciones tanto en frontend como backend
- Manejo de errores y mensajes informativos
- Diseño responsive con Angular Material
- Documentación completa de API con Swagger
- Datos de ejemplo incluidos para pruebas inmediatas

## 🤝 Contribución

1. Fork el proyecto
2. Crear rama para feature (`git checkout -b feature/AmazingFeature`)
3. Commit cambios (`git commit -m 'Add some AmazingFeature'`)
4. Push a la rama (`git push origin feature/AmazingFeature`)
5. Abrir Pull Request

## 📄 Licencia

Este proyecto es para fines educativos - Tecnicatura Superior en Análisis de Sistemas II.

---

**Desarrollado con ❤️ para la materia de Algoritmos y Estructuras de Datos II**
