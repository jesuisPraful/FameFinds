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

--Final categories
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
Select * from Category 

--Final cities
INSERT INTO City VALUES
(1, 'HYDERABAD'),
(2, 'MUMBAI'),
(3, 'KOLKATA'),
(4, 'BANGALORE'),
(5, 'DELHI'),
(6, 'CHENNAI');
select * from City

--Hyderabad
INSERT INTO Products (CityId, ProductName, Description, CategoryId)
VALUES 
(1, 'Hyderabadi Biryani', 'Spicy layered rice delicacy', 10),
(1, 'Irani Chai', 'Strong tea with creamy milk', 10),
(1, 'Pearls', 'Elegant white Hyderabad pearls', 7),
(1, 'Pochampally Sarees', 'Traditional Ikat woven sarees', 1),
(1, 'Mutton Haleem', 'Ramzan special meat porridge', 10),
(1, 'Attar', 'Natural fragrant oil perfumes', 3),
(1, 'Laad Bazaar Bangles', 'Colorful traditional glass bangles', 7),
(1, 'Maska Bun', 'Soft bun with fresh butter', 10);

--Mumbai
INSERT INTO Products (CityId, ProductName, Description, CategoryId)
VALUES
(2, 'Kolhapuri Chappals', 'Traditional handcrafted leather sandals', 5),
(2, 'Spices and Dry Fruits', 'Rich aromatic blends and nuts', 4),
(2, 'Pav Bhaji', 'Spicy mashed veggies with bread', 10),
(2, 'Ragda Pattice', 'Potato patties with white pea curry', 10),
(2, 'Paithani Sarees', 'Rich silk sarees with zari work', 1),
(2, 'Copper & Brass Kitchenware', 'Traditional utensils with antique look', 4),
(2, 'Worli Paintings', 'Traditional tribal art of Maharashtra', 3);

--Delhi
INSERT INTO Products (CityId, ProductName, Description, CategoryId)
VALUES
(5, 'Silver Jewelry', 'Delicate silver jewelry with ethnic flair', 2),
(5, 'Juttis', 'Embroidered Punjabi leather footwear', 5),
(5, 'Hand-painted Pottery', 'Artisanal pottery with folk designs', 4),
(5, 'Ethnic Dupattas & Shawls', 'Colorful wraps with embroidery and prints', 1),
(5, 'Handicrafts (Dilli Haat)', 'All-India crafts under one roof', 3),
(5, 'Spices', 'Aromatic spices from Asia’s biggest market', 10),
(5, 'Ittar (Old Delhi)', 'Natural oil-based long-lasting perfumes', 8);

--KOLKATA
INSERT INTO Products (CityId, ProductName, Description, CategoryId)
VALUES
(3, 'Tant Sarees', 'Lightweight, handwoven cotton Bengali sarees', 1),
(3, 'Jute Handicrafts', 'Eco-friendly bags, mats, and home items', 3),
(3, 'Books (College Street)', 'Affordable and rare books from book hub', 9),
(3, 'Terracotta & Clay Idols', 'Handmade Durga idols & figurines', 3),
(3, 'Rasgulla & Sandesh', 'Iconic soft Bengali milk-based sweets', 10),
(3, 'Kantha Embroidered Items', 'Hand-stitched quilts, sarees, and dupattas', 1);

--chennai
INSERT INTO Products (CityId, ProductName, Description, CategoryId)
VALUES
(6, 'Kanchipuram Silk Sarees', 'Luxurious handwoven silk with gold borders', 1),
(6, 'Herbal Hair Oils', 'Natural oils for hair growth and shine', 7),
(6, 'Temple Jewellery', 'Traditional, ornate temple-style jewellery', 2),
(6, 'Filter Coffee Powder', 'Strong, aromatic South Indian coffee blend', 10),
(6, 'Wooden Toys (Maduravoyal)', 'Handcrafted eco-friendly wooden toys', 5),
(6, 'Handmade Incense Sticks', 'Fragrant incense for prayers and peace', 6),
(6, 'Brass Lamps & Utensils', 'Polished brassware for decor and rituals', 6),
(6, 'Tanjore Paintings', 'Classical South Indian gold-leaf artwork', 3);

--Bangalore
INSERT INTO Products (CityId, ProductName, Description, CategoryId)
VALUES
(4, 'Mysore Silk Sarees', 'Rich silk sarees with golden borders', 1),
(4, 'Channapatna Toys', 'Colorful wooden toys, safe and natural', 5),
(4, 'Traditional Footwear (Kolhapuris)', 'Comfortable and handmade ethnic footwear', 4),
(4, 'Sandalwood Products', 'Fragrant soaps, oils and perfumes', 7),
(4, 'Handmade Soaps & Oils', 'Organic skincare from local artisans', 7),
(4, 'Electronics', 'Affordable electronic items and gadgets', 9);

select * from Products

--to reset identtity value 
DBCC CHECKIDENT ('Products', RESEED, 0);

