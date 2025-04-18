CREATE DATABASE FameFinds
GO 

Use FameFinds
GO
-- 1. UserCustomer Table
CREATE TABLE Customer (
    CustomerId INT PRIMARY KEY IDENTITY,
    FullName NVARCHAR(100),
    Email NVARCHAR(100) UNIQUE,
    PasswordHash NVARCHAR(100),  -- Encrypted password
    PhoneNumber NVARCHAR(15),
);

INSERT INTO [Customer] (FullName, Email, PasswordHash, PhoneNumber)
VALUES 
('Alice Johnson', 'alice@example.com', '2Xx9NmZ8y3A9eI3Zel==', '9876543210'),
('Bob Smith', 'bobsmith@example.com', '7Kp9DjY62zLfN9Fk0Qa==', '9123456789'),
('Catherine Lee', 'catlee@example.com', '0Qj3Llm5PvKJ9dFp5Rt==', '7890123456');
GO
SELECT * FROM Customer;
GO
-- 2. UserVendor Table
CREATE TABLE Vendor (
    VendorId INT PRIMARY KEY IDENTITY,
    VendorName NVARCHAR(100),
    Email NVARCHAR(100) UNIQUE,
    PasswordHash NVARCHAR(100),  -- Encrypted password
    PhoneNumber NVARCHAR(15),
);
GO

INSERT INTO Vendor (VendorName, Email, PasswordHash, PhoneNumber)
VALUES 
('Craft Bazaar', 'vendor1@bazaar.com', '9Pq7HsL2tYv==', '9988776655'),
('Silk Heritage', 'vendor2@silk.com', '7Tj2KuR8aZx==', '9876123450');
GO
SELECT * FROM Vendor;
GO
-- 3. Shop Table
CREATE TABLE Shop (
    ShopId INT PRIMARY KEY IDENTITY,
    ShopName NVARCHAR(150),
    City NVARCHAR(100),
    Address NVARCHAR(255),
    VendorId INT,
    FOREIGN KEY (VendorId) REFERENCES Vendor(VendorId)
);
GO
INSERT INTO Shop (ShopName, City, Address, VendorId)
VALUES 
('Cauvery Handicrafts Emporium', 'Mysore', 'Sayyaji Rao Road, Mysore', 1),
('Mysore Silk Palace', 'Mysore', 'Devaraja Market, Mysore', 2);
GO
SELECT * FROM Shop;
GO

-- 4. Category Table
CREATE TABLE Category (
    CategoryId INT PRIMARY KEY IDENTITY,
    CategoryName NVARCHAR(100) UNIQUE,
);
GO
INSERT INTO Category (CategoryName)
VALUES 
('Handicrafts'),
('Silk Sarees'),
('Sandalwood Products'),
('Jewellery');
GO
SELECT * FROM Category
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
INSERT INTO Products (ProductName, Description, CategoryId)
VALUES 
('Wooden Elephant Figurine', 'Handcrafted wooden elephant made from rosewood', 1),
('Mysore Silk Saree', 'Traditional silk saree with golden zari work', 2),
('Sandalwood Soap', 'Natural sandalwood-scented bathing soap', 3),
('Silver Anklets', 'Traditional Indian silver anklets', 4);
GO
SELECT * FROM Products
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
INSERT INTO ShopProduct (ShopId, ProductId, Price, Stock)
VALUES 
(1, 1, 850.00, 20),
(1, 3, 120.00, 100),
(2, 2, 4500.00, 15),
(2, 4, 950.00, 10);
GO
SELECT * FROM ShopProduct

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
INSERT INTO Rating (CustomerId, ShopId, RatingValue, Review)
VALUES 
(1, 1, 5, 'Amazing collection of handicrafts and great staff.'),
(2, 2, 4, 'Beautiful silk sarees. Bit expensive but worth it.'),
(3, 1, 4, 'Loved the sandalwood items. Neatly arranged shop.');
GO
SELECT * FROM Rating