USE [master]
GO

IF (EXISTS (SELECT name FROM master.dbo.sysdatabases WHERE ('[' + name + ']' = N'FameFinds'OR name = N'FameFinds')))
DROP DATABASE FameFinds
GO

CREATE DATABASE FameFinds
GO

USE FameFinds
GO
-- 1. UserCustomer Table
CREATE TABLE Customer (
    CustomerId INT PRIMARY KEY IDENTITY,
    FullName NVARCHAR(100),
    Email NVARCHAR(100) UNIQUE,
    PasswordHash NVARCHAR(100),  -- Encrypted password
    PhoneNumber NVARCHAR(15)
);

--2. Vendor Table
CREATE TABLE Vendor (
    VendorId INT PRIMARY KEY IDENTITY,
    VendorName NVARCHAR(100),
    Email NVARCHAR(100) UNIQUE,
    PasswordHash NVARCHAR(100),  -- Encrypted password
    PhoneNumber NVARCHAR(15)
);
GO



-- Shop Table
CREATE TABLE Shop (
    ShopId INT PRIMARY KEY IDENTITY,
    ShopName VARCHAR(150),
    EmailId VARCHAR(100) NOT NULL,
    CityName VARCHAR(100) NOT NULL,
    PINCODE VARCHAR(6) NOT NULL,
    ContactNumber VARCHAR(15) NOT NULL,
    Full_Address TEXT NOT NULL,
    Latitude DECIMAL(10, 8) NOT NULL,
    Longitude DECIMAL(11, 8) NOT NULL,
    Opening_time TIME,
    ClosingTime TIME,
    IsOpen BIT DEFAULT 1,
    CreatedAt DATETIME DEFAULT GETDATE(),
    VendorId INT NOT NULL,
    FOREIGN KEY (VendorId) REFERENCES Vendor(VendorId),
    CHECK (
        (LOWER(CityName) = 'patna' AND PINCODE = '800001') OR
        (LOWER(CityName) = 'mumbai' AND PINCODE = '400001') OR
        (LOWER(CityName) = 'delhi' AND PINCODE = '110001') OR
        (LOWER(CityName) = 'hyderabad' AND PINCODE = '500001') OR
        (LOWER(CityName) = 'bengaluru' AND PINCODE = '560001')
    )
);
GO

 

-- 4. Category Table
CREATE TABLE Category (
    CategoryId INT PRIMARY KEY IDENTITY,
    CategoryName NVARCHAR(100) UNIQUE
);
GO


-- 5. Products Table
CREATE TABLE Products (
    ProductId INT PRIMARY KEY IDENTITY,
    ProductName NVARCHAR(150),
    Description NVARCHAR(255),
    CategoryId INT,
    FOREIGN KEY (CategoryId) REFERENCES Category(CategoryId)
);
GO

-- 6. ShopProduct Table (Mapping Shop to Products)
CREATE TABLE ShopProduct (
    ShopProductId INT PRIMARY KEY IDENTITY,
    ShopId INT,
    ProductId INT,
    Price DECIMAL(10,2),
    Stock INT,
    FOREIGN KEY (ShopId) REFERENCES Shop(ShopId),
    FOREIGN KEY (ProductId) REFERENCES Products(ProductId)
);
GO


-- 7. Rating Table
CREATE TABLE Rating (
    RatingId INT PRIMARY KEY IDENTITY,
    CustomerId INT,
    ShopId INT,
    RatingValue INT CHECK (RatingValue BETWEEN 1 AND 5),
    Review NVARCHAR(500),
    CreatedAt DATETIME DEFAULT GETDATE(),
    FOREIGN KEY (CustomerId) REFERENCES Customer(CustomerId),
    FOREIGN KEY (ShopId) REFERENCES Shop(ShopId)
);
GO


INSERT INTO Customer (FullName, Email, PasswordHash, PhoneNumber) VALUES
('Rahul Sharma', 'rahul@example.com', 'hash1', '9876543210'),
('Priya Mehta', 'priya@example.com', 'hash2', '9998877665');


GO
INSERT INTO Vendor (VendorName, Email, PasswordHash, PhoneNumber) VALUES
('StarStyles', 'starstyles@example.com', 'vendorhash1', '8888888888'),
('GlamZone', 'glamzone@example.com', 'vendorhash2', '7777777777');

GO
INSERT INTO Shop (ShopName, EmailId, CityName, PINCODE, ContactNumber, Full_Address, Latitude, Longitude, Opening_time, ClosingTime, VendorId)
VALUES
('Style Hub', 'stylehub@example.com', 'Patna', '800001', '9800000000', 'Boring Road, Patna', 25.615379, 85.101027, '10:00', '20:00', 1),
('Glam Villa', 'glamvilla@example.com', 'Mumbai', '400001', '9700000000', 'Bandra, Mumbai', 19.076090, 72.877426, '09:00', '21:00', 2);

GO
INSERT INTO Category (CategoryName) VALUES
('Clothing'),
('Accessories'),
('Footwear');

GO
INSERT INTO Products (ProductName, Description, CategoryId) VALUES
('Denim Jacket', 'Stylish blue denim jacket for men', 1),
('Leather Belt', 'Premium quality leather belt', 2),
('Sneakers', 'Trendy white sneakers for all seasons', 3);
GO

INSERT INTO ShopProduct (ShopId, ProductId, Price, Stock) VALUES
(1, 1, 1999.99, 20),
(1, 2, 499.50, 50),
(2, 3, 2999.00, 30);

GO
INSERT INTO Rating (CustomerId, ShopId, RatingValue, Review) VALUES
(1, 1, 5, 'Great collection and service!'),
(2, 2, 4, 'Nice variety but prices are high.');


-- 1. View Customers
SELECT * FROM Customer;

-- 2. View Vendors
SELECT * FROM Vendor;

-- 3. View Shops
SELECT * FROM Shop;

-- 4. View Categories
SELECT * FROM Category;

-- 5. View Products
SELECT * FROM Products;

-- 6. View Shop Products (with JOIN for clarity)
SELECT 
    SP.ShopProductId,
    S.ShopName,
    P.ProductName,
    SP.Price,
    SP.Stock
FROM ShopProduct SP
JOIN Shop S ON SP.ShopId = S.ShopId
JOIN Products P ON SP.ProductId = P.ProductId;

-- 7. View Ratings (with JOIN for details)
SELECT 
    R.RatingId,
    C.FullName AS CustomerName,
    S.ShopName,
    R.RatingValue,
    R.Review,
    R.CreatedAt
FROM Rating R
JOIN Customer C ON R.CustomerId = C.CustomerId
JOIN Shop S ON R.ShopId = S.ShopId;

DELETE FROM Customer WHERE CustomerId = 3;