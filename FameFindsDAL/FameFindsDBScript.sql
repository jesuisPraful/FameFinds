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

Select * from CustomerPasswordResetTokens

Select * from Shop


INSERT INTO Vendor (VendorName, Email, PasswordHash, PhoneNumber)
VALUES 
('Global Traders Inc.', 'info@globaltraders.com', '2b1e5f4c1a3d6e2f8b7c1d9e5a4f3c7a', '123-456-7890'),
('TechSource Ltd.', 'support@techsource.com', 'a7f9c1d2e3b4a6c8d9f1e2a3c4b5d6e7', '234-567-8901'),
('GreenLeaf Supplies', 'contact@greenleaf.com', 'e5d4c3b2a1f6e7d8c9b0a2d3f4c5e6b7', '345-678-9012'),
('Oceanic Imports', 'sales@oceanicimports.com', 'd1e2f3a4b5c6d7e8f9a0b1c2d3e4f5a6', '456-789-0123'),
('Sunrise Traders', 'hello@sunrisetraders.com', 'c3b2a1d4e5f6b7c8a9d0e1f2c3b4a5d6', '567-890-1234');



INSERT INTO Shop (
    ShopName, EmailId, CityId, PINCODE, ContactNumber, Full_Address,
    Latitude, Longitude, Opening_time, ClosingTime, IsOpen, VendorId
)
VALUES
('TechZone Central', 'central@techzone.com', 1, '10001', '123-111-2222',
 '123 Main St, Manhattan, New York, NY', 40.712776, -74.005974, '09:00', '21:00', 1, 2),

('Green Mart', 'info@greenmart.com', 2, '60601', '234-222-3333',
 '456 Green Ave, Loop District, Chicago, IL', 41.878113, -87.629799, '08:30', '20:30', 1, 3),

('Sunrise Electronics', 'sales@sunriseelec.com', 2, '90001', '345-333-4444',
 '789 Sunset Blvd, Los Angeles, CA', 34.052235, -118.243683, '10:00', '22:00', 1, 5),

('Oceanic Home Needs', 'contact@oceanichome.com', 2, '77001', '456-444-5555',
 '321 Ocean Dr, Midtown, Houston, TX', 29.760427, -95.369804, '09:30', '19:30', 0, 4),

('SmartTech Outlet', 'hello@smarttech.com', 2, '85001', '567-555-6666',
 '654 Tech Park, Downtown, Phoenix, AZ', 33.448376, -112.074036, '08:00', '20:00', 1, 1);


 INSERT INTO Customer (FullName, Email, PasswordHash, PhoneNumber)
VALUES 
('Alice Johnson', 'alice.johnson@example.com', 'a1b2c3d4e5f678901234abcd5678ef90', '555-101-2020'),
('Bob Smith', 'bob.smith@example.com', 'b2c3d4e5f6a789012345bcde6789fa01', '555-202-3030'),
('Carol Martinez', 'carol.martinez@example.com', 'c3d4e5f6a7b890123456cdef7890ab12', '555-303-4040'),
('David Lee', 'david.lee@example.com', 'd4e5f6a7b8c901234567def8901abc23', '555-404-5050'),
('Eva Chen', 'eva.chen@example.com', 'e5f6a7b8c9d012345678ef9012bcd345', '555-505-6060');