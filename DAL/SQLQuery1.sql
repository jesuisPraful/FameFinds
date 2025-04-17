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

SELECT * FROM Customer;
-- 2. UserVendor Table
CREATE TABLE Vendor (
    VendorId INT PRIMARY KEY IDENTITY,
    VendorName NVARCHAR(100),
    Email NVARCHAR(100) UNIQUE,
    PasswordHash NVARCHAR(100),  -- Encrypted password
    PhoneNumber NVARCHAR(15),
);

-- 3. Shop Table
CREATE TABLE Shop (
    ShopId INT PRIMARY KEY IDENTITY,
    ShopName NVARCHAR(150),
    City NVARCHAR(100),
    Address NVARCHAR(255),
    VendorId INT,
    FOREIGN KEY (VendorId) REFERENCES Vendor(VendorId)
);

-- 4. Category Table
CREATE TABLE Category (
    CategoryId INT PRIMARY KEY IDENTITY,
    CategoryName NVARCHAR(100) UNIQUE,
);

-- 5. Products Table
CREATE TABLE Products (
    ProductId INT PRIMARY KEY IDENTITY,
    ProductName NVARCHAR(150),
    Description NVARCHAR(255),
    CategoryId INT,
    FOREIGN KEY (CategoryId) REFERENCES Category(CategoryId)
);

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