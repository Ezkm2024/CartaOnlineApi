-- Script SQL para crear la base de datos CartaOnlineDB
-- Ejecutar en SQL Server Management Studio

-- Crear la base de datos
CREATE DATABASE CartaOnlineDB;
GO

USE CartaOnlineDB;
GO

-- Crear tabla Companies
CREATE TABLE Companies (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Name NVARCHAR(200) NOT NULL,
    Address NVARCHAR(500) NOT NULL,
    Phone NVARCHAR(50) NOT NULL,
    Email NVARCHAR(200) NOT NULL,
    LogoUrl NVARCHAR(MAX) NULL
);

-- Crear tabla Categories
CREATE TABLE Categories (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Name NVARCHAR(200) NOT NULL,
    CompanyId INT NOT NULL,
    CONSTRAINT FK_Categories_Companies FOREIGN KEY (CompanyId) REFERENCES Companies(Id) ON DELETE CASCADE
);

-- Crear tabla Products
CREATE TABLE Products (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Name NVARCHAR(200) NOT NULL,
    Description NVARCHAR(1000) NULL,
    Price DECIMAL(18,2) NOT NULL,
    CategoryId INT NOT NULL,
    CompanyId INT NOT NULL,
    ImageUrl NVARCHAR(MAX) NULL,
    CONSTRAINT FK_Products_Categories FOREIGN KEY (CategoryId) REFERENCES Categories(Id) ON DELETE CASCADE,
    CONSTRAINT FK_Products_Companies FOREIGN KEY (CompanyId) REFERENCES Companies(Id) ON DELETE NO ACTION
);

-- Crear índices para mejorar rendimiento
CREATE INDEX IX_Categories_CompanyId ON Categories(CompanyId);
CREATE INDEX IX_Products_CategoryId ON Products(CategoryId);
CREATE INDEX IX_Products_CompanyId ON Products(CompanyId);

-- Insertar datos de ejemplo para múltiples empresas

-- Empresa 1: Rotiseria
INSERT INTO Companies (Name, Address, Phone, Email, LogoUrl) VALUES
(N'Rotiseria El Buen Sabor', N'Av. Corrientes 1234, CABA', N'011-4567-8901', N'contacto@buensabor.com', N'https://via.placeholder.com/150?text=El+Buen+Sabor');

-- Empresa 2: Parrilla
INSERT INTO Companies (Name, Address, Phone, Email, LogoUrl) VALUES
(N'Restaurante La Parrilla', N'Av. Santa Fe 5678, CABA', N'011-2345-6789', N'info@laparrilla.com', N'https://via.placeholder.com/150?text=La+Parrilla');

-- Empresa 3: Sushi Delivery
INSERT INTO Companies (Name, Address, Phone, Email, LogoUrl) VALUES
(N'Sushi Delivery', N'Av. Cabildo 2345, CABA', N'011-7890-1234', N'pedidos@sushidelivery.com', N'https://via.placeholder.com/150?text=Sushi+Delivery');

-- Categorías para Rotiseria El Buen Sabor (CompanyId = 1)
INSERT INTO Categories (Name, CompanyId) VALUES
(N'Pizzas Clásicas', 1),
(N'Pizzas Especiales', 1),
(N'Entradas', 1),
(N'Bebidas', 1);

-- Categorías para Restaurante La Parrilla (CompanyId = 2)
INSERT INTO Categories (Name, CompanyId) VALUES
(N'Carnes', 2),
(N'Achuras', 2),
(N'Ensaladas', 2),
(N'Bebidas', 2);

-- Categorías para Sushi Delivery (CompanyId = 3)
INSERT INTO Categories (Name, CompanyId) VALUES
(N'Sushi Rolls', 3),
(N'Sashimi', 3),
(N'Entradas', 3),
(N'Bebidas', 3);

-- Productos para Rotiseria El Buen Sabor
-- Pizzas Clásicas (CategoryId = 1)
INSERT INTO Products (Name, Description, Price, CategoryId, CompanyId, ImageUrl) VALUES
(N'Pizza Mozzarella', N'Pizza con salsa de tomate, queso mozzarella y orégano', 850.00, 1, 1, N'https://via.placeholder.com/200x150?text=Pizza+Mozzarella'),
(N'Pizza Napolitana', N'Pizza con salsa de tomate, queso mozzarella, tomates frescos y ajo', 950.00, 1, 1, N'https://via.placeholder.com/200x150?text=Pizza+Napolitana'),
(N'Pizza Fugazzetta', N'Pizza con queso mozzarella y cebolla', 900.00, 1, 1, N'https://via.placeholder.com/200x150?text=Pizza+Fugazzetta');

-- Pizzas Especiales (CategoryId = 2)
INSERT INTO Products (Name, Description, Price, CategoryId, CompanyId, ImageUrl) VALUES
(N'Pizza Calabresa', N'Pizza con salsa de tomate, queso mozzarella, longaniza calabresa y morrones', 1100.00, 2, 1, N'https://via.placeholder.com/200x150?text=Pizza+Calabresa'),
(N'Pizza Cuatro Quesos', N'Pizza con mozzarella, roquefort, parmesano y provolone', 1200.00, 2, 1, N'https://via.placeholder.com/200x150?text=Cuatro+Quesos'),
(N'Pizza Margherita', N'Pizza con salsa de tomate, queso mozzarella fresco, albahaca y aceite de oliva', 1050.00, 2, 1, N'https://via.placeholder.com/200x150?text=Pizza+Margherita');

-- Entradas (CategoryId = 3)
INSERT INTO Products (Name, Description, Price, CategoryId, CompanyId, ImageUrl) VALUES
(N'Bruschetta', N'Pan tostado con tomate, ajo, albahaca y aceite de oliva', 450.00, 3, 1, N'https://via.placeholder.com/200x150?text=Bruschetta'),
(N'Antipasto Italiano', N'Selección de fiambres italianos con aceitunas', 650.00, 3, 1, N'https://via.placeholder.com/200x150?text=Antipasto');

-- Bebidas (CategoryId = 4)
INSERT INTO Products (Name, Description, Price, CategoryId, CompanyId, ImageUrl) VALUES
(N'Coca Cola 500ml', N'Gaseosa Coca Cola', 250.00, 4, 1, N'https://via.placeholder.com/200x150?text=Coca+Cola'),
(N'Agua Mineral 500ml', N'Agua mineral sin gas', 180.00, 4, 1, N'https://via.placeholder.com/200x150?text=Agua+Mineral'),
(N'Cerveza Quilmes 330ml', N'Cerveza rubia Quilmes', 350.00, 4, 1, N'https://via.placeholder.com/200x150?text=Cerveza+Quilmes');

-- Productos para Restaurante La Parrilla
-- Carnes (CategoryId = 5)
INSERT INTO Products (Name, Description, Price, CategoryId, CompanyId, ImageUrl) VALUES
(N'Bife de Chorizo 500g', N'Corte premium de carne vacuna', 1800.00, 5, 2, N'https://via.placeholder.com/200x150?text=Bife+Chorizo'),
(N'Entraña 400g', N'Corte jugoso y tierno', 1650.00, 5, 2, N'https://via.placeholder.com/200x150?text=Entraña'),
(N'Asado de Tira 800g', N'Costillas de res con hueso', 1400.00, 5, 2, N'https://via.placeholder.com/200x150?text=Asado+Tira');

-- Achuras (CategoryId = 6)
INSERT INTO Products (Name, Description, Price, CategoryId, CompanyId, ImageUrl) VALUES
(N'Chorizo Parrillero', N'Chorizo criollo para parrilla', 450.00, 6, 2, N'https://via.placeholder.com/200x150?text=Chorizo'),
(N'Morcilla', N'Morcilla casera', 380.00, 6, 2, N'https://via.placeholder.com/200x150?text=Morcilla'),
(N'Riñones', N'Riñones de cordero', 520.00, 6, 2, N'https://via.placeholder.com/200x150?text=Riñones');

-- Ensaladas (CategoryId = 7)
INSERT INTO Products (Name, Description, Price, CategoryId, CompanyId, ImageUrl) VALUES
(N'Ensalada Mixta', N'Lechuga, tomate, cebolla y pepino', 350.00, 7, 2, N'https://via.placeholder.com/200x150?text=Ensalada+Mixta'),
(N'Ensalada de Rúcula', N'Rúcula fresca con parmesano', 420.00, 7, 2, N'https://via.placeholder.com/200x150?text=Rúcula');

-- Bebidas (CategoryId = 8)
INSERT INTO Products (Name, Description, Price, CategoryId, CompanyId, ImageUrl) VALUES
(N'Vino Malbec 750ml', N'Vino tinto Malbec de Mendoza', 850.00, 8, 2, N'https://via.placeholder.com/200x150?text=Vino+Malbec'),
(N'Cerveza Artesanal IPA', N'Cerveza artesanal estilo IPA', 280.00, 8, 2, N'https://via.placeholder.com/200x150?text=Cerveza+IPA');

-- Productos para Sushi Delivery
-- Sushi Rolls (CategoryId = 9)
INSERT INTO Products (Name, Description, Price, CategoryId, CompanyId, ImageUrl) VALUES
(N'California Roll', N'Roll con palta, pepino, cangrejo y sésamo', 680.00, 9, 3, N'https://via.placeholder.com/200x150?text=California+Roll'),
(N'Spicy Tuna Roll', N'Roll picante con atún fresco', 750.00, 9, 3, N'https://via.placeholder.com/200x150?text=Spicy+Tuna'),
(N'Vegetarian Roll', N'Roll vegetariano con palta, pepino y zanahoria', 580.00, 9, 3, N'https://via.placeholder.com/200x150?text=Vegetarian+Roll');

-- Sashimi (CategoryId = 10)
INSERT INTO Products (Name, Description, Price, CategoryId, CompanyId, ImageUrl) VALUES
(N'Sashimi de Salmón 200g', N'Salmón fresco cortado en sashimi', 1200.00, 10, 3, N'https://via.placeholder.com/200x150?text=Sashimi+Salmon'),
(N'Sashimi Mixto 250g', N'Mezcla de salmón, atún y pez mantequilla', 1350.00, 10, 3, N'https://via.placeholder.com/200x150?text=Sashimi+Mixto');

-- Entradas (CategoryId = 11)
INSERT INTO Products (Name, Description, Price, CategoryId, CompanyId, ImageUrl) VALUES
(N'Edamame', N'Porotos de soja salteados', 320.00, 11, 3, N'https://via.placeholder.com/200x150?text=Edamame'),
(N'Gyoza de Pollo', N'Empanaditas de pollo al vapor', 450.00, 11, 3, N'https://via.placeholder.com/200x150?text=Gyoza');

-- Bebidas (CategoryId = 12)
INSERT INTO Products (Name, Description, Price, CategoryId, CompanyId, ImageUrl) VALUES
(N'Té Verde', N'Té verde japonés tradicional', 220.00, 12, 3, N'https://via.placeholder.com/200x150?text=Té+Verde'),
(N'Cerveza Asahi 330ml', N'Cerveza japonesa Asahi', 380.00, 12, 3, N'https://via.placeholder.com/200x150?text=Cerveza+Asahi');

-- Verificar los datos insertados
SELECT 'Companies' as TableName, COUNT(*) as Count FROM Companies
UNION ALL
SELECT 'Categories' as TableName, COUNT(*) as Count FROM Categories
UNION ALL
SELECT 'Products' as TableName, COUNT(*) as Count FROM Products;

GO

-- Consultas útiles para verificar la estructura
-- SELECT * FROM Companies;
-- SELECT * FROM Categories WHERE CompanyId = 1;
-- SELECT * FROM Products WHERE CompanyId = 1;

PRINT 'Base de datos CartaOnlineDB creada exitosamente con datos de ejemplo.';
GO
