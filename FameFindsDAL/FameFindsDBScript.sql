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
----------------------------------------------------------------------------------------------------------------------------------------
---Inserting 40 vendor details
INSERT INTO Vendor (VendorName, Email, PasswordHash, PhoneNumber) VALUES
('Amurutha','amrutha@gmail.com','AQAAAAIAAYagAAAAEM3Sp8nvFXziQ9Q990kLqeeca27jzln42kw/OwdJsEHksR3eoLM8X9OegEUlYCvygQ==','9192939495'),
('Pavan','pavan@gmail.com','AQAAAAIAAYagAAAAEE1sOeaC4kdX/xfgYW7ECTLWqHo16l1CyKpE9ia29Y4roc0sBmB4vwJ5FxGIafuNDw==','9090909090'),
('Praful','praful@gmail.com','AQAAAAIAAYagAAAAECx4DdNXxEKyC7ZdBwKNDl2cJAe8fw0bBYIomhA2dm7GMR2h3Eyn2mtJpx6jVWrNsA==','9191919191'),
('Onkar','onkar@gmail.com','AQAAAAIAAYagAAAAEA4ZqNYWdgjKfleKiaqxT2eh4vAKtMqfKV1dQqptIR70IcNNnTmfBcC2fjPPJCxL4g==','9292929292'),
('Chun Chun','chunchun@gmail.com','AQAAAAIAAYagAAAAEDsNOoJyqDYtfWP747Irda9fGLrLGhlgHgC/E6eEPyAQNSC+gwTQIA+nXnAcxZu1jw=="','9898989898'),
('Sruthireddy','sruthi@gmail.com','AQAAAAIAAYagAAAAECeVOOnDCElGmzgHQoLB0QYvrOPzrfxV87w4/mIdplddiLYFfgQEYVr/E1Hyk4VkIg==','9595959595'),
('Neha','neha@gmail.com','AQAAAAIAAYagAAAAEMBoiboQwaB6EhFyN6RM57J0U7DZOS4WfYQHELrmJ4NCM3LsR977NtPPj1UvcsEjLw==','9797979797'),
('Lavanya','lavanya@gmail.com','AQAAAAIAAYagAAAAEMNIH9bfK5iawI+frRel9OqEUK3Idyh+iATQJ6xYnwfiik4D7C3CkASQ439h+4Dbbg==','7418529630'),
('Basker','basker@gmail.com','AQAAAAIAAYagAAAAEG5gn6s+I4hRrzbpvMfVQ7FBQdVFu51g7LTH74d07PlqRTAS+sjc8bdpJLW1stfyUA==','8520963741'),
('Vaibav','Vaibav@gmail.com','AQAAAAIAAYagAAAAEP3+HtqbF7eQdxI79i8HBE67sqyJrEpf/rjHe5fgzkNLf/v0CC9+VvTRroCL+eF1bw==','9876542105'),
('Amurutha','amrutha2@gmail.com','AQAAAAIAAYagAAAAEM3Sp8nvFXziQ9Q990kLqeeca27jzln42kw/OwdJsEHksR3eoLM8X9OegEUlYCvygQ==','9192939495'),
('Pavan','pavan2@gmail.com','AQAAAAIAAYagAAAAEE1sOeaC4kdX/xfgYW7ECTLWqHo16l1CyKpE9ia29Y4roc0sBmB4vwJ5FxGIafuNDw==','9090909090'),
('Praful','praful2@gmail.com','AQAAAAIAAYagAAAAECx4DdNXxEKyC7ZdBwKNDl2cJAe8fw0bBYIomhA2dm7GMR2h3Eyn2mtJpx6jVWrNsA==','9191919191'),
('Onkar','onkar2@gmail.com','AQAAAAIAAYagAAAAEA4ZqNYWdgjKfleKiaqxT2eh4vAKtMqfKV1dQqptIR70IcNNnTmfBcC2fjPPJCxL4g==','9292929292'),
('Chun Chun','chunchun2@gmail.com','AQAAAAIAAYagAAAAEDsNOoJyqDYtfWP747Irda9fGLrLGhlgHgC/E6eEPyAQNSC+gwTQIA+nXnAcxZu1jw=="','9898989898'),
('Sruthireddy','sruthi2@gmail.com','AQAAAAIAAYagAAAAECeVOOnDCElGmzgHQoLB0QYvrOPzrfxV87w4/mIdplddiLYFfgQEYVr/E1Hyk4VkIg==','9595959595'),
('Neha','neha2@gmail.com','AQAAAAIAAYagAAAAEMBoiboQwaB6EhFyN6RM57J0U7DZOS4WfYQHELrmJ4NCM3LsR977NtPPj1UvcsEjLw==','9797979797'),
('Lavanya','lavanya2@gmail.com','AQAAAAIAAYagAAAAEMNIH9bfK5iawI+frRel9OqEUK3Idyh+iATQJ6xYnwfiik4D7C3CkASQ439h+4Dbbg==','7418529630'),
('Basker','basker2@gmail.com','AQAAAAIAAYagAAAAEG5gn6s+I4hRrzbpvMfVQ7FBQdVFu51g7LTH74d07PlqRTAS+sjc8bdpJLW1stfyUA==','8520963741'),
('Vaibav','Vaibav2@gmail.com','AQAAAAIAAYagAAAAEP3+HtqbF7eQdxI79i8HBE67sqyJrEpf/rjHe5fgzkNLf/v0CC9+VvTRroCL+eF1bw==','9876542105'),
('Amurutha','amrutha3@gmail.com','AQAAAAIAAYagAAAAEM3Sp8nvFXziQ9Q990kLqeeca27jzln42kw/OwdJsEHksR3eoLM8X9OegEUlYCvygQ==','9192939495'),
('Pavan','pavan3@gmail.com','AQAAAAIAAYagAAAAEE1sOeaC4kdX/xfgYW7ECTLWqHo16l1CyKpE9ia29Y4roc0sBmB4vwJ5FxGIafuNDw==','9090909090'),
('Praful','praful3@gmail.com','AQAAAAIAAYagAAAAECx4DdNXxEKyC7ZdBwKNDl2cJAe8fw0bBYIomhA2dm7GMR2h3Eyn2mtJpx6jVWrNsA==','9191919191'),
('Onkar','onkar3@gmail.com','AQAAAAIAAYagAAAAEA4ZqNYWdgjKfleKiaqxT2eh4vAKtMqfKV1dQqptIR70IcNNnTmfBcC2fjPPJCxL4g==','9292929292'),
('Chun Chun','chunchun3@gmail.com','AQAAAAIAAYagAAAAEDsNOoJyqDYtfWP747Irda9fGLrLGhlgHgC/E6eEPyAQNSC+gwTQIA+nXnAcxZu1jw=="','9898989898'),
('Sruthireddy','sruthi3@gmail.com','AQAAAAIAAYagAAAAECeVOOnDCElGmzgHQoLB0QYvrOPzrfxV87w4/mIdplddiLYFfgQEYVr/E1Hyk4VkIg==','9595959595'),
('Neha','neha3@gmail.com','AQAAAAIAAYagAAAAEMBoiboQwaB6EhFyN6RM57J0U7DZOS4WfYQHELrmJ4NCM3LsR977NtPPj1UvcsEjLw==','9797979797'),
('Lavanya','lavanya3@gmail.com','AQAAAAIAAYagAAAAEMNIH9bfK5iawI+frRel9OqEUK3Idyh+iATQJ6xYnwfiik4D7C3CkASQ439h+4Dbbg==','7418529630'),
('Basker','basker3@gmail.com','AQAAAAIAAYagAAAAEG5gn6s+I4hRrzbpvMfVQ7FBQdVFu51g7LTH74d07PlqRTAS+sjc8bdpJLW1stfyUA==','8520963741'),
('Vaibav','Vaibav3@gmail.com','AQAAAAIAAYagAAAAEP3+HtqbF7eQdxI79i8HBE67sqyJrEpf/rjHe5fgzkNLf/v0CC9+VvTRroCL+eF1bw==','9876542105'),
('Amurutha','amrutha4@gmail.com','AQAAAAIAAYagAAAAEM3Sp8nvFXziQ9Q990kLqeeca27jzln42kw/OwdJsEHksR3eoLM8X9OegEUlYCvygQ==','9192939495'),
('Pavan','pavan4@gmail.com','AQAAAAIAAYagAAAAEE1sOeaC4kdX/xfgYW7ECTLWqHo16l1CyKpE9ia29Y4roc0sBmB4vwJ5FxGIafuNDw==','9090909090'),
('Praful','praful4@gmail.com','AQAAAAIAAYagAAAAECx4DdNXxEKyC7ZdBwKNDl2cJAe8fw0bBYIomhA2dm7GMR2h3Eyn2mtJpx6jVWrNsA==','9191919191'),
('Onkar','onkar4@gmail.com','AQAAAAIAAYagAAAAEA4ZqNYWdgjKfleKiaqxT2eh4vAKtMqfKV1dQqptIR70IcNNnTmfBcC2fjPPJCxL4g==','9292929292'),
('Chun Chun','chunchun4@gmail.com','AQAAAAIAAYagAAAAEDsNOoJyqDYtfWP747Irda9fGLrLGhlgHgC/E6eEPyAQNSC+gwTQIA+nXnAcxZu1jw=="','9898989898'),
('Sruthireddy','sruthi4@gmail.com','AQAAAAIAAYagAAAAECeVOOnDCElGmzgHQoLB0QYvrOPzrfxV87w4/mIdplddiLYFfgQEYVr/E1Hyk4VkIg==','9595959595'),
('Neha','neha4@gmail.com','AQAAAAIAAYagAAAAEMBoiboQwaB6EhFyN6RM57J0U7DZOS4WfYQHELrmJ4NCM3LsR977NtPPj1UvcsEjLw==','9797979797'),
('Lavanya','lavanya4@gmail.com','AQAAAAIAAYagAAAAEMNIH9bfK5iawI+frRel9OqEUK3Idyh+iATQJ6xYnwfiik4D7C3CkASQ439h+4Dbbg==','7418529630'),
('Basker','basker4@gmail.com','AQAAAAIAAYagAAAAEG5gn6s+I4hRrzbpvMfVQ7FBQdVFu51g7LTH74d07PlqRTAS+sjc8bdpJLW1stfyUA==','8520963741'),
('Vaibav','Vaibav4@gmail.com','AQAAAAIAAYagAAAAEP3+HtqbF7eQdxI79i8HBE67sqyJrEpf/rjHe5fgzkNLf/v0CC9+VvTRroCL+eF1bw==','9876542105');
------------------------------------------------------------------------------------------------------------------------------
---Inserting shops
--Hyderabad City 
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
---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
--Inserting  ShopProducts
--Hyderabad
Insert into ShopProduct(ShopId, ProductId, Price, Stock)Values
(1, 1, 359, 50), 
(2, 1, 299, 200),
(3, 1, 599, 1000),
(4, 1, 450, 300),
(5, 1, 650, 900);

Insert into ShopProduct(ShopId, ProductId, Price, Stock)Values
(6, 2, 60, 500), 
(7, 2, 20, 200),
(8, 2, 25, 100),
(9, 2, 12, 300),
(10, 2, 20, 900);

Insert into ShopProduct(ShopId, ProductId, Price, Stock)Values
(11, 4, 299, 200),
(12, 4, 300, 100),
(13, 4, 450, 300),
(14, 4, 680, 900),
(15, 4, 800, 500);

Insert into ShopProduct(ShopId, ProductId, Price, Stock)Values
(16, 3, 160, 200),
(17, 3, 80, 1000),
(18, 3, 120, 300),
(19, 3, 180, 900),
(20, 3, 260, 300);
--------------------------------------------------------------------------------
--kolkata
Insert into ShopProduct(ShopId, ProductId, Price, Stock)Values
(21, 16, 700, 200),
(22, 16, 1000, 1000),
(23, 16, 1500, 300),
(24, 16, 2500, 900),
(25, 16, 3000, 500),
(26, 16, 4000, 300),
(27, 16, 3500, 600),
(28, 16, 6000, 200);

Insert into ShopProduct(ShopId, ProductId, Price, Stock)Values
(29, 17, 150, 200),
(30, 17, 300, 100),
(31, 17, 200, 300),
(32, 17, 80, 900),
(33, 17, 300, 500),
(34, 17, 400, 300),
(35, 17, 350, 600),
(36, 17, 220, 200);

Insert into ShopProduct(ShopId, ProductId, Price, Stock)Values
(37, 18, 150, 200),
(38, 18, 300, 100),
(39, 18, 200, 300),
(40, 18, 800, 900),
(41, 18, 300, 500),
(42, 18, 400, 300),
(43, 18, 350, 600),
(44, 18, 220, 200);

Insert into ShopProduct(ShopId, ProductId, Price, Stock)Values
(45, 19, 150, 2000),
(46, 19, 300, 1000),
(47, 19, 700, 3000),
(48, 19, 800, 9000),
(49, 19, 900, 5000),
(50, 19, 400, 3000),
(51, 19, 350, 6000);
-----------------------------------------------------------------------
--Mumbai
Insert into ShopProduct(ShopId, ProductId, Price, Stock)Values
(52, 11, 50, 200),
(53, 11, 30, 100),
(54, 11, 70, 300),
(55, 11, 80, 900),
(56, 11, 90, 500),
(57, 11, 40, 300);

Insert into ShopProduct(ShopId, ProductId, Price, Stock)Values
(58, 9, 350, 200),
(59, 9, 530, 100),
(60, 9, 470, 300),
(61, 9, 280, 900),
(62, 9, 590, 500),
(63, 9, 440, 300);

Insert into ShopProduct(ShopId, ProductId, Price, Stock)Values
(64, 10, 550, 200),
(65, 10, 730, 100),
(66, 10, 870, 300),
(67, 10, 980, 900),
(68, 10, 690, 500),
(69, 10, 640, 300);

Insert into ShopProduct(ShopId, ProductId, Price, Stock)Values
(70, 12, 150, 200),
(71, 12, 230, 100),
(72, 12, 170, 300),
(73, 12, 180, 900),
(74, 12, 90, 500),
(75,12, 240, 300);

-----------------------------------------------------------------------------------------------------
---Banglore
Insert into ShopProduct(ShopId, ProductId, Price, Stock)Values
(76, 22, 1150, 200),
(77, 22, 2230, 100),
(78, 22, 3170, 300),
(79, 22, 2180, 900),
(80, 22, 990, 500),
(81, 22, 4240, 300);

Insert into ShopProduct(ShopId, ProductId, Price, Stock)Values
(82, 23, 150, 200),
(83, 23, 230, 100),
(84, 23, 170, 350),
(85, 23, 180, 630),
(86, 23, 90, 520),
(87, 23, 240, 300);

Insert into ShopProduct(ShopId, ProductId, Price, Stock)Values
(88, 24, 150, 200),
(89, 24, 230, 100),
(90, 24, 170, 300),
(91, 24, 180, 900),
(92, 24, 90, 500),
(93, 24, 240, 300);

Insert into ShopProduct(ShopId, ProductId, Price, Stock)Values
(94, 25, 150, 200),
(95, 25, 230, 100),
(96, 25, 170, 300),
(97, 25, 180, 900),
(98, 25, 90, 500);
-------------------------------------------------------------------------------------------------------
--Chennai
Insert into ShopProduct(ShopId, ProductId, Price, Stock)Values
(99, 35, 150, 2000),
(100, 35, 230, 1700),
(101, 35, 170, 3000),
(102, 35, 180, 5900),
(103, 35, 90, 5600);

Insert into ShopProduct(ShopId, ProductId, Price, Stock)Values
(104, 36, 150, 200),
(105, 36, 230, 100),
(106, 36, 170, 300),
(107, 36, 180, 900),
(108, 36, 90, 500);

Insert into ShopProduct(ShopId, ProductId, Price, Stock)Values
(109, 37, 15000, 200),
(110, 37, 23000, 100),
(111, 37, 17000, 300),
(112, 37, 18000, 900),
(113, 37, 5000, 600);

Insert into ShopProduct(ShopId, ProductId, Price, Stock)Values
(114, 38, 90, 500),
(115, 38, 150, 200),
(116, 38, 230, 100),
(117, 38, 170, 300),
(118, 38, 180, 900);
--------------------------------------------------------------------------------------------------------
--Delhi
Insert into ShopProduct(ShopId, ProductId, Price, Stock)Values
(119, 28, 90000, 500),
(120, 28, 15000, 200),
(121, 28, 230000, 100),
(122, 28, 170000, 300),
(123, 28, 18000, 900);

Insert into ShopProduct(ShopId, ProductId, Price, Stock)Values
(124, 29, 900, 500),
(125, 29, 450, 200),
(126, 29, 230, 100),
(127, 29, 190, 300),
(128, 29, 120, 900);

Insert into ShopProduct(ShopId, ProductId, Price, Stock)Values
(129, 30, 100, 500),
(130, 30, 150, 500),
(131, 30, 220, 800),
(132, 30, 170, 300),
(133, 30, 180, 900);

Insert into ShopProduct(ShopId, ProductId, Price, Stock)Values
(134, 31, 150, 500),
(135, 31, 260, 200),
(136, 31, 290, 100),
(137, 31, 350, 300),
(138, 31, 180, 900);


select * from ShopProduct;