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

-- For Customers
CREATE TABLE CustomerPasswordResetTokens (
    Id INT IDENTITY PRIMARY KEY,
    CustomerId INT NOT NULL,
    Token NVARCHAR(255) UNIQUE NOT NULL,
    Expiry DATETIME NOT NULL,
    IsUsed BIT DEFAULT 0,
    RequestedAt DATETIME DEFAULT GETDATE(),
    FOREIGN KEY (CustomerId) REFERENCES Customer(CustomerId)
);

-- For Vendors
CREATE TABLE VendorPasswordResetTokens (
    Id INT IDENTITY PRIMARY KEY,
    VendorId INT NOT NULL,
    Token NVARCHAR(255) UNIQUE NOT NULL,
    Expiry DATETIME NOT NULL,
    IsUsed BIT DEFAULT 0,
    RequestedAt DATETIME DEFAULT GETDATE(),
    FOREIGN KEY (VendorId) REFERENCES Vendor(VendorId)
);




--City Table 
CREATE TABLE City (
    CityId INT PRIMARY KEY,
    CityName VARCHAR(100) UNIQUE NOT NULL
);


-- Shop Table
CREATE TABLE Shop (
    ShopId INT PRIMARY KEY IDENTITY,
    ShopName VARCHAR(150),
    EmailId VARCHAR(100) NOT NULL,
    CityId INT,
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
    FOREIGN KEY (CityId) REFERENCES City(CityId)
     
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
    CityId INT,
    ProductName NVARCHAR(150),
    Description NVARCHAR(255),
    CategoryId INT,
    FOREIGN KEY (CategoryId) REFERENCES Category(CategoryId),
    FOREIGN KEY (CityId) REFERENCES City(CityId)
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

SELECT * FROM Products

INSERT INTO City VALUES 
(1,'HYDERABAD'),
( 2, 'MUMBAI')

select * from City

INSERT INTO Category  VALUES
( 'Clothing'),
('Electronics'),
('Beauty & Personal Care'),
('Home & Kitchen'),
('Footwear'),
('Handicrafts'),
('Jewelry'),
('Stationery & Books'),
( 'Toys & Games'),
('Local Snacks & Food Items');

Select * from city
select * from category
select * from products