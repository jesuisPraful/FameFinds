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

--KOLKATA
INSERT INTO Products (CityId, ProductName, Description, CategoryId)
VALUES
(3, 'Tant Sarees', 'Lightweight, handwoven cotton Bengali sarees', 1),
(3, 'Jute Handicrafts', 'Eco-friendly bags, mats, and home items', 3),
(3, 'Books (College Street)', 'Affordable and rare books from book hub', 9),
(3, 'Terracotta & Clay Idols', 'Handmade Durga idols & figurines', 3),
(3, 'Rasgulla & Sandesh', 'Iconic soft Bengali milk-based sweets', 10),
(3, 'Kantha Embroidered Items', 'Hand-stitched quilts, sarees, and dupattas', 1);

--BANGALORE
INSERT INTO Products (CityId, ProductName, Description, CategoryId)
VALUES
(4, 'Mysore Silk Sarees', 'Rich silk sarees with golden borders', 1),
(4, 'Channapatna Toys', 'Colorful wooden toys, safe and natural', 5),
(4, 'Traditional Footwear (Kolhapuris)', 'Comfortable and handmade ethnic footwear', 4),
(4, 'Sandalwood Products', 'Fragrant soaps, oils and perfumes', 7),
(4, 'Handmade Soaps & Oils', 'Organic skincare from local artisans', 7),
(4, 'Electronics', 'Affordable electronic items and gadgets', 9);

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

--CHENNAI
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



INSERT INTO Shop (ShopName,EmailId,CityId,PINCODE,ContactNumber,Full_Address,Latitude,Longitude,Opening_time,ClosingTime,VendorId)
VALUES ('Bandra Electronics Hub','bandra.electronics@example.com',2,'400050','9876543210','Near Bandra Station, Mumbai',19.06000000,72.83000000,'10:00:00','21:00:00',1);

INSERT INTO Vendor (VendorName,Email,PasswordHash,PhoneNumber)
VALUES ('Ravi Enterprises','ravi@example.com','hashed_password_123','9876543210');

INSERT INTO ShopProduct(ShopId,ProductId,Price, Stock)
VALUES (1,10,79999.00,25)

Select * from ShopProduct
select * from City
select * from Shop
SELECT * FROM Vendor
SELECT * FROM Products
Select * from Shop
SELECT * From Rating

--30 vendors registration
INSERT INTO Vendor (VendorName, Email, PasswordHash, PhoneNumber) VALUES
('Ravi Kumar', 'ravi.kumar@gmail.com', 'pass1234', '9876543210'),
('Sneha Reddy', 'sneha.reddy@gmail.com', 'sneha456', '9876512345'),
('Amit Verma', 'amit.verma@yahoo.com', 'amitpass', '9876598765'),
('Pooja Singh', 'pooja.singh@outlook.com', 'pooja789', '7894561230'),
('Arjun Mehta', 'arjun.mehta@gmail.com', 'arjunpass', '9123456789'),
('Kavya Sharma', 'kavya.sharma@gmail.com', 'kavya456', '9988776655'),
('Vikram Rao', 'vikram.rao@gmail.com', 'vikram001', '9123434567'),
('Neha Joshi', 'neha.joshi@hotmail.com', 'nehaj123', '9988001122'),
('Rahul Desai', 'rahul.desai@gmail.com', 'rahul789', '9001122334'),
('Divya Nair', 'divya.nair@gmail.com', 'divya321', '9876123456'),
('Suresh Patel', 'suresh.patel@gmail.com', 'suresh777', '7899871234'),
('Lakshmi Menon', 'lakshmi.menon@yahoo.com', 'lakshmi998', '9011223344'),
('Aakash Jain', 'aakash.jain@gmail.com', 'aakash123', '9876034561'),
('Meena Iyer', 'meena.iyer@gmail.com', 'meena456', '9856123478'),
('Rohan Kapoor', 'rohan.kapoor@gmail.com', 'rohan321', '9990011223'),
('Swati Chawla', 'swati.chawla@gmail.com', 'swati007', '9765432109'),
('Harish Babu', 'harish.babu@gmail.com', 'harish111', '9865432109'),
('Anita Rao', 'anita.rao@gmail.com', 'anita000', '9988771122'),
('Karan Grover', 'karan.grover@gmail.com', 'karan321', '9787654321'),
('Ritu Agarwal', 'ritu.agarwal@gmail.com', 'ritu456', '9645321987'),
('Santosh Yadav', 'santosh.yadav@gmail.com', 'santosh321', '9998887776'),
('Preeti Sharma', 'preeti.sharma@gmail.com', 'preeti123', '9877778886'),
('Jayant Tiwari', 'jayant.tiwari@gmail.com', 'jayant456', '9090909090'),
('Farah Khan', 'farah.khan@gmail.com', 'farah321', '9876000011'),
('Deepak Jha', 'deepak.jha@gmail.com', 'deepakpass', '9760011223'),
('Mitali Shah', 'mitali.shah@gmail.com', 'mitali123', '9830011223'),
('Naveen Krishnan', 'naveen.krishnan@gmail.com', 'naveen456', '9753124680'),
('Tanvi Gupta', 'tanvi.gupta@gmail.com', 'tanvi321', '9900112233'),
('Gaurav Das', 'gaurav.das@gmail.com', 'gaurav987', '9845012345'),
('Shalini Mishra', 'shalini.mishra@gmail.com', 'shalini789', '9812345678');

------------------------------------------------------------------------------------------------------------------------------
--Inserting  ShopProducts
--Hyderabad

Insert into ShopProduct(ShopId, ProductId, Price, Stock)Values
(26, 1, 359, 50), 
(27, 1, 299, 200),
(28, 1, 599, 1000),
(29, 1, 450, 300),
(30, 1, 650, 900);

Insert into ShopProduct(ShopId, ProductId, Price, Stock)Values
(31, 2, 60, 500), 
(32, 2, 20, 200),
(33, 2, 25, 100),
(34, 2, 12, 300),
(35, 2, 20, 900);

Insert into ShopProduct(ShopId, ProductId, Price, Stock)Values
(36, 4, 299, 200),
(37, 4, 300, 100),
(38, 4, 450, 300),
(39, 4, 680, 900);

Insert into ShopProduct(ShopId, ProductId, Price, Stock)Values
(41, 3, 160, 200),
(42, 3, 80, 1000),
(43, 3, 120, 300),
(44, 3, 180, 900);
--------------------------------------------------------------------------------
--kolkata
Insert into ShopProduct(ShopId, ProductId, Price, Stock)Values
(46, 37, 700, 200),
(47, 37, 1000, 1000),
(48, 37, 1500, 300),
(49, 37, 2500, 900),
(50, 37, 3000, 500),
(51, 37, 4000, 300),
(52, 37, 3500, 600),
(53, 37, 6000, 200);


Insert into ShopProduct(ShopId, ProductId, Price, Stock)Values
(54, 38, 150, 200),
(55, 38, 300, 100),
(56, 38, 200, 300),
(57, 38, 80, 900),
(58, 38, 300, 500),
(59, 38, 400, 300),
(60, 38, 350, 600),
(61, 38, 220, 200);

Insert into ShopProduct(ShopId, ProductId, Price, Stock)Values
(62, 39, 150, 200),
(63, 39, 300, 100),
(64, 39, 200, 300),
(65, 39, 800, 900),
(66, 39, 300, 500),
(67, 39, 400, 300),
(68, 39, 350, 600),
(69, 39, 220, 200);

Insert into ShopProduct(ShopId, ProductId, Price, Stock)Values
(70, 40, 150, 2000),
(71, 40, 300, 1000),
(72, 40, 700, 3000),
(73, 40, 800, 9000),
(74, 40, 900, 5000),
(75, 40, 400, 3000),
(76, 40, 350, 6000);
-----------------------------------------------------------------------
--Mumbai
Insert into ShopProduct(ShopId, ProductId, Price, Stock)Values
(77, 11, 50, 200),
(78, 11, 30, 100),
(79, 11, 70, 300),
(80, 11, 80, 900),
(81, 11, 90, 500),
(82, 11, 40, 300);

Insert into ShopProduct(ShopId, ProductId, Price, Stock)Values
(83, 9, 350, 200),
(84, 9, 530, 100),
(85, 9, 470, 300),
(86, 9, 280, 900),
(87, 9, 590, 500),
(88, 9, 440, 300);

Insert into ShopProduct(ShopId, ProductId, Price, Stock)Values
(89, 10, 550, 200),
(90, 10, 730, 100),
(91, 10, 870, 300),
(92, 10, 980, 900),
(93, 10, 690, 500),
(94, 10, 640, 300);

Insert into ShopProduct(ShopId, ProductId, Price, Stock)Values
(95, 12, 150, 200),
(96, 12, 230, 100),
(97, 12, 170, 300),
(98, 12, 180, 900),
(99, 12, 90, 500),
(100,12, 240, 300);

-----------------------------------------------------------------------------------------------------
---Banglore
Insert into ShopProduct(ShopId, ProductId, Price, Stock)Values
(101, 23, 1150, 200),
(102, 23, 2230, 100),
(102, 23, 3170, 300),
(104, 23, 2180, 900),
(105, 23, 990, 500),
(106, 23, 4240, 300);

Insert into ShopProduct(ShopId, ProductId, Price, Stock)Values
(107, 24, 150, 200),
(108, 24, 230, 100),
(108, 24, 170, 350),
(110, 24, 180, 630),
(111, 24, 90, 520),
(112, 24, 240, 300);

Insert into ShopProduct(ShopId, ProductId, Price, Stock)Values
(113, 25, 150, 200),
(114, 25, 230, 100),
(115, 25, 170, 300),
(116, 25, 180, 900),
(117, 25, 90, 500),
(118, 25, 240, 300);

Insert into ShopProduct(ShopId, ProductId, Price, Stock)Values
(129, 26, 150, 200),
(130, 26, 230, 100),
(131, 26, 170, 300),
(132, 26, 180, 900),
(133, 26, 90, 500);
-------------------------------------------------------------------------------------------------------
--Chennai
Insert into ShopProduct(ShopId, ProductId, Price, Stock)Values
(134, 29, 150, 2000),
(135, 29, 230, 1700),
(136, 29, 170, 3000),
(137, 29, 180, 5900),
(139, 29, 90, 5600);

Insert into ShopProduct(ShopId, ProductId, Price, Stock)Values
(140, 30, 150, 200),
(141, 30, 230, 100),
(142, 30, 170, 300),
(143, 30, 180, 900),
(144, 30, 90, 500);

Insert into ShopProduct(ShopId, ProductId, Price, Stock)Values
(145, 31, 150, 200),
(146, 31, 230, 100),
(147, 31, 170, 300),
(148, 31, 180, 900);

Insert into ShopProduct(ShopId, ProductId, Price, Stock)Values
(149, 32, 90, 500),
(150, 32, 150, 200),
(151, 32, 230, 100),
(152, 32, 170, 300),
(153, 32, 180, 900);
---------------------------------------------------------------------------------------------------------
--Delhi
Insert into ShopProduct(ShopId, ProductId, Price, Stock)Values
(154, 16, 90000, 500),
(155, 16, 15000, 200),
(156, 16, 230000, 100),
(157, 16, 170000, 300),
(158, 16, 18000, 900);

Insert into ShopProduct(ShopId, ProductId, Price, Stock)Values
(159, 17, 900, 500),
(160, 17, 450, 200),
(161, 17, 230, 100),
(162, 17, 190, 300),
(163, 17, 120, 900);

Insert into ShopProduct(ShopId, ProductId, Price, Stock)Values
(164, 18, 100, 500),
(165, 18, 150, 500),
(166, 18, 220, 800),
(167, 18, 170, 300),
(168, 18, 180, 900);

Insert into ShopProduct(ShopId, ProductId, Price, Stock)Values
(189, 19, 150, 500),
(190, 19, 260, 200),
(191, 19, 290, 100),
(192, 19, 350, 300),
(193, 19, 180, 900);
---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

--Hyderabad City Inserting shops
INSERT INTO Shop (ShopName, EmailId, CityId, PINCODE, ContactNumber, Full_Address, Latitude, Longitude, Opening_time, ClosingTime,VendorId) VALUES
('Hotel Shadab', 'contact@hotelsihadab.com', 1, '500002', '9100001111', 'Plot 21, High Court Rd, Ghansi Bazaar, Hyderabad', 17.3611, 78.4744, '06:00', '02:00', 7),
('Cafe Bahar', 'info@cafebaharhyd.com', 1, '500029', '9100001112', '3-5/815-A, Hyderguda, Basheer Bagh, Hyderabad', 17.4046, 78.4868, '12:00', '23:00', 8),
('Paradise Biryani', 'support@paradisehyderabad.com', 1, '500016', '9100001113', 'Green Hedges Annexe, Begumpet, Hyderabad', 17.4200, 78.4550, '11:00', '23:00',9),
('Bawarchi', 'hello@bawarchihyderabad.com', 1, '500082', '9100001114', 'RTC X Roads, Punjagutta, Hyderabad', 17.4323, 78.4451, '12:00', '22:00', 7),
('Shah Ghouse Café', 'contact@shahghouse.com', 1, '500032', '9100001115', 'Gachibowli Rd, Madhura Nagar Colony, Gachibowli, Hyderabad', 17.4510, 78.3910, '11:00', '00:00', 10);


INSERT INTO Shop (ShopName, EmailId, CityId, PINCODE, ContactNumber, Full_Address, Latitude, Longitude, Opening_time, ClosingTime, VendorId) VALUES
('Cafe Bahar', 'info@cafebaharhyd.com', 1, '500029', '9100001112', '3-5/815-A, Basheer Bagh, Hyderabad', 17.4046, 78.4868, '12:00', '23:00',11),
('Grand Hotel', 'contact@grandhotelhyd.com', 1, '500001', '9100001116', 'Bank Street, Abids, Hyderabad', 17.4014, 78.4724, '06:00', '22:00',12),
('Cafe Niloufer', 'help@cafeneeloufer.com', 1, '500001', '9100001117', 'Lakdikapul, Hyderabad', 17.4050, 78.4840, '07:00', '20:00',13),
('Alpha Hotel', 'contact@alphahotelhyd.com', 1, '500003', '9100001118', 'Secunderabad, Hyderabad', 17.4530, 78.5020, '06:00', '21:00',14),
('Cafe Iqbal', 'info@cafeiqbalhyd.com', 1, '500028', '9100001119', 'Nampally, Hyderabad', 17.3885, 78.4800, '08:00', '22:00',15);

INSERT INTO Shop (ShopName, EmailId, CityId, PINCODE, ContactNumber, Full_Address, Latitude, Longitude, Opening_time, ClosingTime, VendorId) VALUES
('Kankatala – Queen of Sarees', 'hello@kankatala.com', 1, '500033', '9121168693', '470, Rd Number 36, Jubilee Hills, Hyderabad', 17.4125, 78.4268, '10:30', '21:00',16),
('Unnati Silk Prints Pvt Ltd', 'unnati@silkprints.com', 1, '500035', '9100001120', 'PVT Market, Kothapet, Hyderabad', 17.3570, 78.5450, '11:00', '20:00',17),
('L Square Collection', 'contact@lsquare.com', 1, '500035', '9100001121', 'PVT Market, Kothapet, Hyderabad', 17.3570, 78.5450, '11:00', '20:00',18),
('Ramdev Textiles', 'ramdev@textiles.com', 1, '500012', '9849474000', '5-3-1040/7, Gaffar Hussain Complex, Osmangunj, Hyderabad', 17.3719,78.4810, '10:00', '18:00', 19),
('Sri Sheshu Handlooms', 'info@sri-sheshu.com', 1, '500035', '8106895000', 'Plot No 40/H, Weavers Colony, Bhavana Nagar, Kapra, Hyderabad', 17.4520,78.5480, '10:00', '19:00', 20);

INSERT INTO Shop (ShopName, EmailId, CityId, PINCODE, ContactNumber, Full_Address, Latitude, Longitude, Opening_time, ClosingTime,VendorId) VALUES
('Modi Pearls', 'contact@modipearls.com', 1, '500002', '9000001001', '22-5-92, Beside Gulzar Houz Circle, Charminar Road, Hyderabad', 17.3610, 78.4740, '11:00', '20:30',21),
('Sri Bansilal Pearls', 'info@sri-bansilal.com', 1, '500002', '9000001002', 'Charminar Main Road, Near Bazaar, Hyderabad', 17.3608, 78.4742, '10:00', '19:00',22),
('Real Hyderabadi Pearls', 'sales@hyderabadpearlshop.com', 1, '500002', '9000001003', 'Char Minar Bazaar, Hyderabad', 17.3611, 78.4745, '10:00', '18:00',23),
('Pearl House', 'info@pearlhousehyd.com', 1, '500003', '9000001004', 'Gulzar Houz Circle, Koti, Hyderabad', 17.3830, 78.4860, '11:00', '19:30',24),
('City Pearls', 'contact@citypearls.com', 1, '500001', '9000001005', 'Abids Rd, Hyderabad', 17.4050, 78.4700, '10:30', '20:00',25);

--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
--KolKata

INSERT INTO Shop (ShopName, EmailId, CityId, PINCODE, ContactNumber, Full_Address, Latitude, Longitude, Opening_time, ClosingTime,VendorId) VALUES
('RMGC Basak', 'info@rmgcbasak.com', 3, '700029', '03324617277', '1 Nandy Street, Gariahat, Kolkata', 22.5185, 88.3560, '10:30','19:30', 26),
('Rajgharana Sarees', 'contact@rajgharana.com', 3, '700016', '03322268381', '87 Park Street, Kolkata', 22.5440, 88.3520, '11:00','20:00',27 ),
('Tanti Handwoven Exclusives', 'info@tanti.com', 3, '700064', '03340012345', 'Salt Lake Sector 1, Kolkata', 22.5726, 88.4295, '10:00', '20:00',28 ),
('Balaram Saha & Sons', 'sales@balaramsaha.com', 3, '700019', '03324408676', '14/6 Gariahat Rd, Ekdalia', 22.5130, 88.3705, '10:00','20:00', 29),
('Ananda Boutique', 'contact@anandaboutique.com', 3, '700017', '03322292275', '13 Russell St, Park St', 22.5430, 88.3570, '10:00','18:30',30 ),
('Kanishka', 'hello@kanishka.com', 3, '700029', '03324630465', 'Hindusthan Rd, Gariahat', 22.5135, 88.3670, '10:30','19:30', 31),
('Byloom', 'support@byloom.com', 3, '700029', '03324198727', 'Hindusthan Park, Gariahat', 22.5125, 88.3675, '11:00','20:00', 32),
('Simaaya Boutique', 'info@simaaya.com', 3, '700029', '03340034688', 'Elgin Rd, Bhowanipore', 22.5110, 88.3630, '11:00','20:30',33 );


INSERT INTO Shop (ShopName, EmailId, CityId, PINCODE, ContactNumber, Full_Address, Latitude, Longitude, Opening_time, ClosingTime,VendorId) VALUES
('Gariahat Jute Emporium', 'info@gariahatjute.com', 3, '700029', '03324650000', 'Gariahat Market, Kolkata', 22.5140, 88.3700, '10:00','20:00',8 ),
('Dakshinapan Handicrafts', 'contact@dakshinapan.com', 3, '700029', '03324651234', 'Dakshinapan Shopping Centre', 22.5145, 88.3710, '10:00','21:00', 9),
('New Market Jute Stall', 'info@newmarketjute.com', 3, '700087', '03322451234', 'New Market, Lindsay St', 22.5410, 88.3500, '10:00','20:00', 10),
('College Street Jute', 'support@csjute.com', 3, '700073', '03322451235', 'College Square', 22.5740, 88.3610, '10:00','19:00',11 ),
('Hatibagan Jute Mart', 'sales@hatibaganjute.com', 3, '700005', '03322891234', 'Hatibagan Market', 22.6020, 88.3660, '10:00','19:00',12 ),
('Ballygunge Jute House', 'contact@ballygungejute.com', 3, '700019', '03324781234', 'Ballygunge Market', 22.5090, 88.3690, '10:00','20:00',13 ),
('Esplanade Jute Lane', 'info@esplanadejute.com', 3, '700069', '03322301234', 'Esplanade, Central', 22.5730, 88.3600, '10:00','20:00',14 ),
('South City Mall Jute', 'support@scmjute.com', 3, '700068', '03340081234', 'South City Mall, Prince Anwar Shah Rd', 22.5070, 88.3720, '10:00','21:00', 15);


INSERT INTO Shop (ShopName, EmailId, CityId, PINCODE, ContactNumber, Full_Address, Latitude, Longitude, Opening_time, ClosingTime,VendorId) VALUES
('Ananda Book Depot', 'info@anandadepot.com', 3, '700073', '033243067071', 'College Square, Kolkata', 22.5740, 88.3610, '10:00','20:30', 16),
('National Book Store', 'contact@nationalbooks.com', 3, '700073', '03322451236', 'College Street', 22.5745, 88.3615, '10:00','20:00', 17),
('Gitanjali Books', 'support@gitanjalibooks.com', 3, '700073', '03322451237', 'College Street', 22.5742, 88.3612, '10:00','20:00', 18),
('TM Book Stall', 'info@tmbooks.com', 3, '700073', '03322451238', 'College Square', 22.5741, 88.3611, '10:00','19:00', 19),
('Bharat Stationers', 'contact@bharatstationers.com', 3, '700073', '03322451239', 'College Square', 22.5743, 88.3613, '10:00','19:00',20 ),
('Book Corner', 'info@bookcorner.com', 3, '700073', '03322451240', 'Opp Presidency College', 22.5750, 88.3620, '10:00','19:00', 21),
('Chuckerverty & Chatterjee', 'contact@chuckchatterjee.com', 3, '700073', '03322451241', 'College Street', 22.5746, 88.3614, '10:00','20:00', 22),
('Techno World', 'info@technoworldbooks.com', 3, '700073', '03322451242', 'College Street', 22.5744, 88.3613, '10:00','20:00', 23);

INSERT INTO Shop (ShopName, EmailId, CityId, PINCODE, ContactNumber, Full_Address, Latitude, Longitude, Opening_time, ClosingTime,VendorId) VALUES
('Kumartuli Studios', 'info@kumartulistudios.com', 3, '700012', '03322301235', 'Kumartuli, North Kolkata', 22.6290, 88.3630, '09:00','18:00', 24),
('Durga Artist House', 'info@durgaartist.com', 3, '700012', '03322301237', 'Kumartuli Lane', 22.6288, 88.3628, '09:00','18:00', 25),
('Clay Idols Kolkata', 'contact@clayidolskolkata.com', 3, '700012', '03322301238', 'Kumortuli', 22.6292, 88.3632, '09:00','18:00',26 ),
('Kumartuli Heritage Idols', 'support@heritageidols.com', 3, '700012', '03322301239', 'Kumartuli', 22.6291, 88.3631, '09:00','18:00',27 ),
('North Kolkata Terracotta', 'info@northterracotta.com', 3, '700012', '03322301240', 'Kumartuli', 22.6287, 88.3627, '09:00','18:00',28 ),
('Clay Ghat Idols', 'contact@clayghat.com', 3, '700009', '03322201234', 'Barabazar', 22.6140, 88.3635, '10:00','18:00', 29),
('Eco Clay Idols', 'support@ecoclayidols.com', 3, '700013', '03322701234', 'Shovabazar', 22.6360, 88.3600, '10:00','18:00',30 );

----------------------------------------------------------------------------------------------------------------------------------------------------------------------------

--(Mumbai)
INSERT INTO Shop (ShopName, EmailId, CityId, PINCODE, ContactNumber, Full_Address, Latitude, Longitude, Opening_time, ClosingTime,VendorId)
VALUES
('Sardar Pav Bhaji', 'sardar@pavbhaji.in', 2, '400007', '02223530208', 'Tardeo Road Junction, Mumbai', 19.0090, 72.8265, '10:00','02:00',31 ),
('Cannon Pav Bhaji', 'cannon@pavbhaji.com', 2, '400001', '02222074205', 'Opposite CST Station, Fort, Mumbai', 18.9407, 72.8355, '07:00', '01:00', 32),
('Amar Juice Centre', 'amar@pavbhaji.com', 2, '400056', '02226731234', 'Gulmohar Rd, Vile Parle West', 19.0980,72.8250, '11:00','23:00', 33),
('Maruti Pav Bhaji', 'maruti@pavbhaji.com', 2, '400056', '02226731235', 'Swami Vivekananda Rd, Vile Parle West', 19.0985,72.8255, '12:00','01:00',34 ),
('Gypsy Corner', 'gypsy@pavbhaji.com', 2, '400028', '02224318756', 'Shivaji Park, Dadar', 19.0170,72.8360, '11:00','23:00', 35),
('Sukh Sagar', 'sukh@pavbhaji.com', 2, '400006', '02223636856', 'Chowpatty, Marine Drive', 18.9670,72.8135, '11:00','23:30', 36);

INSERT INTO Shop (ShopName, EmailId, CityId, PINCODE, ContactNumber, Full_Address, Latitude, Longitude, Opening_time, ClosingTime,VendorId)
VALUES
('Dadar Kolhapuri Wala', 'kolhapuri@dadarwala.com', 2, '400014', '02224325678', 'Behind Dadar Station, Dadar', 19.0200,72.8400, '10:00','20:00', 37),
('Kurla Road Kolhapuri', 'kolhapuri@kurlawalacom', 2, '400070', '02225001234', 'Thakkar Bappa Rd, Kurla East', 19.0730,72.8610, '10:00','20:00',6 ),
('Matheran Leather', 'leather@matheranleather.in', 2, '400071', '02225011234', 'Central Mall, Kurla East', 19.0735,72.8605, '10:00','20:00', 7),
('Colaba Kolhapuri', 'colaba@kolhapuri.com', 2, '400005', '02222841234', 'Colaba Causeway', 18.9100,72.8140, '11:00','20:00',8 ),
('Girgaon Footwear', 'girgaon@kolhapuri.com', 2, '400004', '02223841235', 'Girgaon Chowpatty',18.9510,72.8115,'10:00','20:00',9),
('Bandra Kolhapuri', 'bandra@kolhapuri.com', 2, '400050', '02226441234', 'Hill Rd, Bandra West',19.0560,72.8250,'10:00','20:00',10);

INSERT INTO Shop (ShopName, EmailId, CityId, PINCODE, ContactNumber, Full_Address, Latitude, Longitude, Opening_time, ClosingTime,VendorId)
VALUES
('New Crawford Dry Fruits', 'crawford@dryfruits.com', 2, '400002', '02222601234', 'Crawford Market', 18.9580,72.8320, '09:00','21:00',11),
('Masjid Bunder Spices', 'masjid@spices.com', 2, '400003', '02223761234', 'Mohammed Ali Rd, Masjid Bunder',18.9500,72.8345,'09:00','21:30',12),
('Kalbadevi Dry Fruits', 'kalbadevi@dryfruits.com',2,'400002','02222651234','Kalbadevi Street',18.9585,72.8300,'09:00','21:00',13),
('Zaveri Bazaar Spices', 'zaveri@spices.com',2,'400002','02222661234','Zaveri Bazaar',18.9570,72.8310,'09:00','20:00',14),
('Bandar Spice House', 'bandar@spices.com',2,'400011','02223771234','CST Area',18.9400,72.8350,'09:00','20:00',15),
('Dadar Spice Stop', 'dadar@spices.com',2,'400028','02224321234','Dadar Market',19.0170,72.8400,'09:00','20:00',16);

INSERT INTO Shop (ShopName, EmailId, CityId, PINCODE, ContactNumber, Full_Address, Latitude, Longitude, Opening_time, ClosingTime,VendorId)
VALUES
('Juhu Pav Bhaji & Ragda', 'juhu@ragda.com',2,'400049','02226471235','Juhu Beach Rd',19.0962,72.8267,'11:00','23:00',17),
('Girgaum Snacks', 'girgaon@ragda.com',2,'400004','02223841236','Girgaon Chowpatty',18.9525,72.8118,'10:00','22:00',18),
('Cannon Ragda Pattice Stall', 'cannon@ragda.com',2,'400001','02222071234','Opposite CST, Fort',18.9407,72.8355,'10:00','20:00',19),
('Bandra Ragda Point', 'bandra@ragda.com',2,'400050','02226481234','Bandra West',19.0570,72.8257,'11:00','22:00',20),
('Dadar Ragda Junction', 'dadar@ragda.com',2,'400028','02224331234','Dadar TT Circle',19.0188,72.8405,'10:00','22:00',21),
('Vile Parle Ragda Centre', 'vparle@ragda.com',2,'400056','02226791235','Vile Parle West',19.0988,72.8258,'11:00','22:00',22);

----------------------------------------------------------------------------------------------------------------------------------------------------------------
---Banglore
INSERT INTO Shop (ShopName, EmailId, CityId, PINCODE, ContactNumber, Full_Address, Latitude, Longitude, Opening_time, ClosingTime, VendorId) VALUES
('Mysore Saree Udyog', 'contact@mysoresarees.com', 4, '560001', '08022202330', '316 Mahaveer Rd, Gandhi Nagar, Bengaluru', 12.9750, 77.6030, '10:30','20:30',23),
('Nalli Silks', 'support@nalli.com', 4, '560025', '08023534501', '299 Sampige Rd, Malleshwaram', 13.0100, 77.5690, '10:00','20:30',24),
('Prasiddhi Silks', 'info@prasiddhisilks.com', 4, '560001', '08025580433', '46 MG Rd, Shivaji Nagar', 12.9718, 77.5936, '10:30','20:30',25),
('Deepam Silks', 'contact@deepamsilks.com', 4, '560042', '0809886041100', '7 MG Rd, Haridevpur', 12.9710, 77.5980, '10:30','21:00',26),
('Girija Silks', 'support@girijasilks.com', 4, '560003', '08023340466', 'Sri Maruthi Complex, Malleshwaram', 13.0105, 77.5665, '10:00','20:30',27),
('Sudarshan Silks', 'info@sudarshansilks.com', 4, '560053', '08043300400', 'Chickpet, Ragipet', 12.9610, 77.5560, '10:30','20:00',28);

INSERT INTO Shop (ShopName, EmailId, CityId, PINCODE, ContactNumber, Full_Address, Latitude, Longitude, Opening_time, ClosingTime, VendorId) VALUES
('Cauvery Emporium', 'info@cauveryemporium.com', 4, '560001', '08025581118', 'MG Rd, Bengaluru', 12.9699, 77.5980, '10:00','20:00',29),
('FairKraft Creations', 'contact@fairkraft.com', 4, '560078', '08026751234', 'JP Nagar 2nd Phase', 12.9270, 77.5850, '11:00','20:00',30),
('Varnam Collective', 'info@varnam.in', 4, '560038', '08025250360', '1332 Paramahansa Rd, Indiranagar', 12.9718, 77.6412, '10:00','20:00',31),
('Bharat Art & Crafts', 'support@bharatarts.com', 4, '562160', '08123611123', 'Channapatna Town', 12.6975, 77.2077, '10:00','19:00',32),
('Sri Kauvery Arts', 'info@skauveryart.com', 4, '562160', '08123611124', 'NH275, Bengaluru-Mysuru Hwy', 12.6970, 77.2050, '10:00','19:00',33),
('Channapatna Toy Shop', 'support@channapatnatoyshop.com', 4, '560078', '08026751235', 'JP Nagar', 12.9265, 77.5852, '10:00','20:00',34);

INSERT INTO Shop (ShopName, EmailId, CityId, PINCODE, ContactNumber, Full_Address, Latitude, Longitude, Opening_time, ClosingTime, VendorId) VALUES
('Shoe Walk', 'info@shoewalk.in', 4, '560002', '08041151234', 'Veerapillai St, Commercial St', 12.9731, 77.6081, '10:00','21:00',35),
('Kolhapuri', 'contact@kolhapuris.com', 4, '560025', '08041121234', 'Patricks Complex, Brigade Rd', 12.9692, 77.6035, '10:00','21:00',36),
('Ritz Shoes', 'info@ritzshoes.com', 4, '560002', '08041131234', 'Shivaji Nagar', 12.9740, 77.6100, '10:00','21:00',37),
('Exotic Footwear', 'support@exoticfootwear.in', 4, '560002', '08041141234', 'Commercial St', 12.9730, 77.6080, '10:00','21:00',6),
('Jaypore', 'contact@jaypore.com', 4, '560038', '08041351234', 'Indiranagar', 12.9718, 77.6412, '10:00','21:00',7),
('Naadia Kolhapuris', 'info@naadia.com', 4, '560001', '08041561234', '1st Block, Koramangala', 12.9352, 77.6201, '10:00','21:00',8);

INSERT INTO Shop (ShopName, EmailId, CityId, PINCODE, ContactNumber, Full_Address, Latitude, Longitude, Opening_time, ClosingTime, VendorId) VALUES
('Cauvery Handicrafts', 'info@cauveryhandicrafts.in', 4, '560001', '08025581118', '49 MG Rd, Halasuru', 12.9712, 77.6190, '09:30','20:30',9),
('Cauvery Emporium Jayanagar', 'contact@cauveryemporium.com', 4, '560011', '08026581234', '4th Block, Jayanagar', 12.9250, 77.5936, '10:00','20:00',10),
('Mysore Sandal Soap Store', 'info@mysoresoap.com', 4, '560025', '08041191234', 'Near Yeshwanthpur Metro', 13.0380, 77.5720, '09:00','21:00',11),
('Govt Sandal Shop', 'support@govtsandal.com', 4, '560025', '08041191235', 'Yeshwanthpur', 13.0385, 77.5725, '09:00','21:00',12),
('Ana Royal Sandal', 'contact@anaroyal.com', 4, '560017', '08041231234', 'HAL Area', 12.9622, 77.6555, '10:00','20:00',14);
-----------------------------------------------------------------------------------------------------------------------------------------------------------
--Chennai

INSERT INTO Shop (ShopName, EmailId, CityId, PINCODE, ContactNumber, Full_Address, Latitude, Longitude, Opening_time, ClosingTime, VendorId) VALUES
('Kanakavalli', 'info@kanakavalli.com', 6, '600042', '044-24312345', 'Chetpet, Chennai', 13.0745, 80.2509, '10:00', '20:00', 13),
('Nalli Silks', 'support@nalli.com', 6, '600008', '044-28512345', 'T. Nagar, Chennai', 13.0416, 80.2375, '09:00', '21:00',15 ),
('Palam Silks', 'contact@palamsilks.com', 6, '600034', '044-22312345', 'Adyar, Chennai', 13.0110, 80.2440, '10:00', '20:00', 16),
('Sundari Silks', 'info@sundarisilks.com', 6, '600028', '044-22212345', 'T. Nagar, Chennai', 13.0408, 80.2337, '10:00', '20:00',17 ),
('Hayagrivas Silk House', 'sales@hayagrivas.com', 6, '600028', '044-22254321', 'T. Nagar', 13.0407, 80.2340, '10:00', '20:00',18 );

INSERT INTO Shop (ShopName, EmailId, CityId, PINCODE, ContactNumber, Full_Address, Latitude, Longitude, Opening_time, ClosingTime, VendorId) VALUES
('Khadi Natural Herbal Store', 'info@khadinatural.com', 6, '600028', '044-24312311', 'T. Nagar', 13.0405, 80.2354, '10:00', '20:00', 19),
('Aroma & Art', 'contact@aromaartchennai.com', 6, '600042', '044-24367890', 'Chetpet', 13.0740, 80.2510, '10:00', '20:00', 20),
('Veena Herbal', 'support@veenaindia.com', 6, '600041', '044-24781234', 'Anna Nagar', 13.0670, 80.2230, '10:00', '20:00',21 ),
('Himalaya Herbals', 'info@himalayawellness.com', 6, '600040', '044-24512345', 'Kilpauk', 13.0810, 80.2300, '10:00', '20:00',22 ),
('Patanjali Store', 'support@patanjaliayurved.net', 6, '600028', '044-22234567', 'T. Nagar', 13.0413, 80.2361, '10:00', '21:00', 23);

INSERT INTO Shop (ShopName, EmailId, CityId, PINCODE, ContactNumber, Full_Address, Latitude, Longitude, Opening_time, ClosingTime, VendorId) VALUES
('Sukra Jewellery', 'support@sukra.com', 6, '600004', '044-24901234', 'Mylapore', 13.0350, 80.2590, '10:30', '20:00', 24),
('JCS Jewellers', 'info@jcsjewellers.com', 6, '600035', '044-24956789', 'Anna Nagar', 13.0830, 80.2150, '10:30', '20:00',25 ),
('Shanthi Tailors Jewellery', 'shop@shanthitailor.com', 6, '600017', '044-24321098', 'T. Nagar', 13.0415, 80.2350, '10:00', '20:00', 26),
('Sri Krishna Jewellers', 'info@srikrishnajewels.com', 6, '600028', '044-22254444', 'T. Nagar', 13.0409, 80.2352, '10:00', '20:00',27 ),
('Mythili Jewels', 'support@mythilijewels.com', 6, '600034', '044-22313131', 'Adyar', 13.0108, 80.2442, '10:00', '20:00', 28);

INSERT INTO Shop (ShopName, EmailId, CityId, PINCODE, ContactNumber, Full_Address, Latitude, Longitude, Opening_time, ClosingTime, VendorId) VALUES
('Saravana Coffee', 'info@saravanacoffee.com', 6, '600017', '044-24324567', 'T. Nagar', 13.0412, 80.2360, '08:00', '20:00', 29),
('The South Indian Store', 'support@southindianstore.com', 6, '600017', '044-24345678', 'T. Nagar', 13.0410, 80.2355, '10:00', '20:00',30 ),
('Narasu''s Coffee Shop', 'info@narasus.com', 6, '600110', '044-26451234', 'Mount Road', 13.0730, 80.2560, '08:00', '20:00',31 ),
('Carmel Biomatrix Coffee', 'contact@carmelbios.com', 6, '600018', '044-24956780', 'Nungambakkam', 13.0620, 80.2450, '08:00', '20:00', 32),
('Buhari Filter Coffee', 'info@buharicoffee.com', 6, '600018', '044-24387654', 'Nungambakkam', 13.0615, 80.2445, '08:00', '20:00', 33);

-----------------------------------------------------------------------------------------------------------------------------------------------------------
--Delhi
INSERT INTO Shop (ShopName, EmailId, CityId, PINCODE, ContactNumber, Full_Address, Latitude, Longitude, Opening_time, ClosingTime, VendorId) VALUES
('Dariba Kalan Silver', 'info@daribakalan.com', 5, '110006', '011-23456701', 'Dariba Kalan, Chandni Chowk, Old Delhi', 28.6560, 77.2300, '10:00', '20:00', 34),
('Silverline Khan Market', 'support@silverline.com', 5, '110001', '011-23456702', 'Khan Market, New Delhi', 28.6139, 77.2167, '10:30', '20:00',35 ),
('Silofer Fine Silver', 'contact@silofer.com', 5, '110048', '011-23456703', 'Near Kailash Colony Metro, South Delhi', 28.5450, 77.2330, '10:00', '21:00', 36),
('Celestial Silver', 'info@celestialjewels.com', 5, '110048', '011-23456704', 'South Delhi', 28.5500, 77.2200, '10:00', '20:00', 37),
('Arts & Jewels', 'hello@artsjewels.com', 5, '110048', '011-23456705', 'South Delhi', 28.5505, 77.2250, '10:00', '20:00',6 );

INSERT INTO Shop (ShopName, EmailId, CityId, PINCODE, ContactNumber, Full_Address, Latitude, Longitude, Opening_time, ClosingTime, VendorId) VALUES
('Fizzy Goblet', 'info@fizzygoblet.com', 5, '110070', '011-23456709', 'Vasant Kunj, New Delhi', 28.5244, 77.1550, '10:00', '20:00', 7),
('The Shoe Garage', 'support@theshoegarage.com', 5, '110030', '011-23456710', 'Shahpur Jat, South Delhi', 28.5435, 77.2010, '11:00', '20:00',8 ),
('Jutti Choo', 'hello@juttichoo.com', 5, '110001', '011-23456711', 'Janpath Market, New Delhi', 28.6260, 77.2170, '11:00', '19:00',9 ),
('Needledust', 'info@needledust.com', 5, '110030', '011-23456712', 'Select Citywalk Mall, Saket', 28.5244, 77.2100, '10:00', '21:00',10 ),
('World of Adira', 'support@adira.com', 5, '110017', '011-23456713', 'Malviya Nagar', 28.5370, 77.2250, '10:00', '20:00', 11);

INSERT INTO Shop (ShopName, EmailId, CityId, PINCODE, ContactNumber, Full_Address, Latitude, Longitude, Opening_time, ClosingTime, VendorId) VALUES
('Dilli Haat Pottery Stall', 'info@dillihaatpottery.com', 5, '110049', '011-23456717', 'Dilli Haat INA Market', 28.5525, 77.2235, '10:30', '21:00',12 ),
('Pottery by Tribes', 'support@potterytribes.com', 5, '110030', '011-23456718', 'Select Citywalk Mall, Saket', 28.5244, 77.2100, '10:00', '21:00',13 ),
('State Emporium Pottery', 'contact@stateemporium.com', 5, '110001', '011-23456719', 'Baba Kharak Singh Marg', 28.6325, 77.2290, '11:00', '19:00', 14),
('Janpath Pottery Stall', 'hello@janpathpottery.com', 5, '110001', '011-23456720', 'Janpath Market', 28.6260, 77.2170, '11:00', '19:00',15 ),
('Amrita Carpets & Crafts', 'info@amritacarpets.com', 5, '110003', '011-23456721', 'Lodhi Colony Market', 28.5830, 77.2290, '10:00', '20:00',16 );

INSERT INTO Shop (ShopName, EmailId, CityId, PINCODE, ContactNumber, Full_Address, Latitude, Longitude, Opening_time, ClosingTime, VendorId) VALUES
('Phulkari Stall Dilli Haat', 'info@phulkarihaat.com', 5, '110049', '011-23456725', 'Dilli Haat INA Market', 28.5525, 77.2235, '10:30', '21:00',17 ),
('Kantha Stall Dilli Haat', 'contact@kanthahaat.com', 5, '110049', '011-23456726', 'Dilli Haat INA Market', 28.5525, 77.2235, '10:30', '21:00', 18),
('Banarasi Shawl Stall', 'support@banarasmall.com', 5, '110049', '011-23456727', 'Dilli Haat INA Market', 28.5525, 77.2235, '10:30', '21:00',19 ),
('Chikankari Stall', 'info@chikankarihaat.com', 5, '110049', '011-23456728', 'Dilli Haat INA Market', 28.5525, 77.2235, '10:30', '21:00',20 ),
('Kashmiri Shawls', 'contact@kashmirishawl.com', 5, '110049', '011-23456729', 'Dilli Haat INA Market', 28.5525, 77.2235, '10:30', '21:00',21 );