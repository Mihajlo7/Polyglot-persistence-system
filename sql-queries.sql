USE small_database;
GO

-------------------
CREATE TABLE Users (
	Id UNIQUEIDENTIFIER DEFAULT NEWSEQUENTIALID() CONSTRAINT user_pk PRIMARY KEY,
	email NVARCHAR(100) NOT NULL,
	passwordHash NVARCHAR(255) NOT NULL,
	passwordSalt VARBINARY(255) NOT NULL
);

CREATE TABLE Consumers (
	Id UNIQUEIDENTIFIER CONSTRAINT consumer_pk PRIMARY KEY,
	firstName NVARCHAR(255) NOT NULL,
	lastName NVARCHAR(255) NOT NULL,
	birthDate DATE NOT NULL,
	telephone VARCHAR(20) NOT NULL
	CONSTRAINT consumer_fk FOREIGN KEY (Id) REFERENCES Users(Id) ON DELETE CASCADE
)

CREATE TABLE CreditCard (
	Id UNIQUEIDENTIFIER DEFAULT NEWSEQUENTIALID() CONSTRAINT credit_card_pk PRIMARY KEY,
	consumerId UNIQUEIDENTIFIER NOT NULL CONSTRAINT credit_card_fk FOREIGN KEY (consumerId) REFERENCES Consumers(Id) ON DELETE CASCADE,
	number BIGINT NOT NULL,
	cardType NVARCHAR(20) NOT NULL
)


CREATE TABLE Administrators (
	Id UNIQUEIDENTIFIER CONSTRAINT admin_pk PRIMARY KEY,
	joinedDate DATE NOT NULL,
	role VARCHAR(20) NOT NULL CONSTRAINT admin_role CHECK (role IN ('BASIC_ADMIN','SUPER_ADMIN')),
	CONSTRAINT admin_fk FOREIGN KEY (Id) REFERENCES Users(Id)
);

INSERT INTO Users (email, passwordHash, passwordSalt)
VALUES 
    ('admin@ecommerce.com', HASHBYTES('SHA2_256', 'admin'), CAST('admin' AS VARBINARY(255))),
    ('mihajlo@ecommerce.com', HASHBYTES('SHA2_256', 'mihajlo'), CAST('mihajlo' AS VARBINARY(255)));

INSERT INTO Administrators (Id,joinedDate,role) SELECT id,'2024-10-26','SUPER_ADMIN' FROM Users WHERE email='admin@ecommerce.com';
INSERT INTO Administrators (Id,joinedDate,role) SELECT id,'2024-10-26','SUPER_ADMIN' FROM Users WHERE email='mihajlo@ecommerce.com';
----------------------------
CREATE TABLE Category (
	Id UNIQUEIDENTIFIER DEFAULT NEWSEQUENTIALID() CONSTRAINT category_pk PRIMARY KEY,
	name NVARCHAR(50) NOT NULL
);

-- create sequnce for product
CREATE SEQUENCE subCategory_seq AS INT START WITH 1 INCREMENT BY 7 NO CYCLE;

CREATE TABLE SubCategory (
	Id UNIQUEIDENTIFIER DEFAULT NEWSEQUENTIALID() CONSTRAINT subCategory_pk PRIMARY KEY,
	subCategoryNumber  INT DEFAULT NEXT VALUE FOR subCategory_seq CONSTRAINT subCategory_num UNIQUE,
	name NVARCHAR(50) NOT NULL,
	categoryId UNIQUEIDENTIFIER CONSTRAINT subCategory_fk FOREIGN KEY (categoryId) REFERENCES Category(Id) ON DELETE SET NULL ON UPDATE CASCADE
);

-- create sequnce for product
CREATE SEQUENCE product_seq AS INT START WITH 1 INCREMENT BY 3 NO CYCLE;

CREATE TABLE Company(
	Id UNIQUEIDENTIFIER DEFAULT NEWSEQUENTIALID() CONSTRAINT company_pk PRIMARY KEY,
	serialNumber NVARCHAR(100) NOT NULL,
	name NVARCHAR(100) NOT NULL,
	telephone NVARCHAR(100) NOT NULL,
	country NVARCHAR(50) NOT NULL
)

CREATE TABLE Seller (
	id UNIQUEIDENTIFIER CONSTRAINT seller_pk PRIMARY KEY,
	address NVARCHAR(255) NOT NULL,
	city NVARCHAR(100) NOT NULL,
	hasShop BIT NOT NULL
	CONSTRAINT seller_fk FOREIGN KEY (id) REFERENCES Company(id) ON DELETE CASCADE
)

CREATE TABLE Courier (
	id UNIQUEIDENTIFIER CONSTRAINT courier_pk PRIMARY KEY,
	deliveryPrice DECIMAL(7,2) NOT NULL
	CONSTRAINT courier_fk FOREIGN KEY (id) REFERENCES Company(id) ON DELETE CASCADE
)

CREATE TABLE CourierContact (
	courierId UNIQUEIDENTIFIER,
	companyId UNIQUEIDENTIFIER,
	serialNumContact NVARCHAR(255) NOT NULL,
	contactInfo NVARCHAR(500)
	CONSTRAINT courier_contact_pk PRIMARY KEY (courierId,companyId),
	CONSTRAINT courier_fk FOREIGN KEY (courierId) REFERENCES Courier(id) ON DELETE SET NULL,
	CONSTRAINT company_fk FOREIGN KEY (companyId) REFERENCES Company(id) ON DELETE SET NULL
)

CREATE TABLE ProductHeader (
	ProductId UNIQUEIDENTIFIER DEFAULT NEWSEQUENTIALID() CONSTRAINT product_pk PRIMARY KEY,
	ProductNumber INT DEFAULT NEXT VALUE FOR product_seq CONSTRAINT product_num UNIQUE,
	name NVARCHAR(100) NOT NULL,
	price DECIMAL(10,2) NOT NULL,
	subCategoryId UNIQUEIDENTIFIER CONSTRAINT product_fk FOREIGN KEY (subCategoryId) REFERENCES SubCategory(id) ON DELETE SET NULL ON UPDATE CASCADE,
	produced UNIQUEIDENTIFIER NOT NULL CONSTRAINT produced_seller_fk FOREIGN KEY (produced) REFERENCES Seller(id),
	store UNIQUEIDENTIFIER NULL CONSTRAINT store_seller_fk FOREIGN KEY (store) REFERENCES Seller (Id)
);

CREATE TABLE DistributeProduct (
	productId UNIQUEIDENTIFIER,
	sellerId UNIQUEIDENTIFIER,
	distributionPrice DECIMAL(7,2),
	CONSTRAINT dist_pk PRIMARY KEY (productId,sellerId),
	CONSTRAINT seller_fk FOREIGN KEY (sellerid) REFERENCES Seller(Id),
	CONSTRAINT product_fk FOREIGN KEY (productId) REFERENCES ProductHeader(ProductId)
)

CREATE TABLE ProductDetail (
	ProductDetailId UNIQUEIDENTIFIER CONSTRAINT product_detail_pk PRIMARY KEY,
	ProductId UNIQUEIDENTIFIER CONSTRAINT product_unique UNIQUE,
	shortDescribe NVARCHAR(255) NULL,
	imageUrl NVARCHAR(255) NOT NULL
	CONSTRAINT product_detail_fk FOREIGN KEY (ProductId) REFERENCES ProductHeader(ProductId) ON DELETE CASCADE ON UPDATE CASCADE
);

CREATE TABLE Car (
	ProductDetailId UNIQUEIDENTIFIER CONSTRAINT product_pk PRIMARY KEY,
	yearManifactured INT NOT NULL,
	model NVARCHAR(100) NOT NULL,
	serialNumber NVARCHAR(500) NOT NULL,
	engineDisplacement NVARCHAR(20) NOT NULL,
	enginePower NVARCHAR(50) NOT NULL,
	longDescription NVARCHAR(2000),
	CONSTRAINT product_fk FOREIGN KEY (ProductDetailId) REFERENCES ProductDetail (ProductDetailId)
)

CREATE TABLE Device (
	ProductDetailId UNIQUEIDENTIFIER CONSTRAINT product_pk PRIMARY KEY,
	yearManifactured INT NOT NULL,
	serialNumber NVARCHAR(500) NOT NULL,
	weight NVARCHAR(10) NULL,
	storage NVARCHAR(10) NOT NULL
	CONSTRAINT product_fk FOREIGN KEY (ProductDetailId) REFERENCES ProductDetail (ProductDetailId)
)

CREATE TABLE Movie (
	ProductDetailId UNIQUEIDENTIFIER CONSTRAINT product_pk PRIMARY KEY,
	yearRelease INT NOT NULL,
	genre VARCHAR(50) NOT NULL,
	duration INT NOT NULL,
	subtitling NVARCHAR(50) NOT NULL,
	CONSTRAINT product_fk FOREIGN KEY (ProductDetailId) REFERENCES ProductDetail (ProductDetailId)
)

CREATE TABLE Mobile (
	ProductDetailId UNIQUEIDENTIFIER CONSTRAINT product_pk PRIMARY KEY,
	screenDiagonal VARCHAR(10) NOT NULL,
	operativeSystem VARCHAR(50) NOT NULL,
	color VARCHAR(50) NOT NULL,
	CONSTRAINT product_fk FOREIGN KEY (ProductDetailId) REFERENCES Device (ProductDetailId)
)

CREATE TABLE Laptop (
	ProductDetailId UNIQUEIDENTIFIER CONSTRAINT product_pk PRIMARY KEY,
	processor VARCHAR(10) NOT NULL,
	ramMemory VARCHAR(50) NOT NULL,
	longDescription NVARCHAR(2000),
	CONSTRAINT product_fk FOREIGN KEY (ProductDetailId) REFERENCES Device (ProductDetailId)
)

CREATE TABLE Chart (
	id UNIQUEIDENTIFIER DEFAULT NEWSEQUENTIALID() CONSTRAINT chart_pk PRIMARY KEY,
	total DECIMAL(10,2) NOT NULL DEFAULT 0.0,
	status INT NOT NULL CONSTRAINT chart_status CHECK (status IN (0,1,2,3)),
	createdAt DATETIME NOT NULL,
	updatedAt DATETIME NOT NULL,
	consumer_id UNIQUEIDENTIFIER NOT NULL CONSTRAINT chart_fk FOREIGN KEY (consumer_id) REFERENCES Consumer(id)
)

CREATE TABLE ChartItem (
	chartId UNIQUEIDENTIFIER,
	productId UNIQUEIDENTIFIER,
	createdAt DATETIME NOT NULL,
	CONSTRAINT chart_item_pk PRIMARY KEY(chartId,productId),
	CONSTRAINT chart_fk FOREIGN KEY (chartId) REFERENCES Chart(id),
	CONSTRAINT product_fk FOREIGN KEY (productId) REFERENCES ProductHeader(ProductId)
)

GO

CREATE TRIGGER trgUpdateTotalOnInsertOrDelete
ON ChartItem
AFTER INSERT, DELETE
AS
BEGIN
	DECLARE @total DECIMAL(10,2)

	SELECT @total= COALESCE(SUM(p.price),0)
	FROM ChartItem ci INNER JOIN ProductHeader p ON ci.productId=p.ProductId;

	UPDATE c
	SET c.total=@total
	FROM Chart c
	WHERE c.id IN (
		SELECT chartId FROM inserted
		UNION
		SELECT chartid FROM deleted
	);
END;


CREATE TABLE Orders  (
	id UNIQUEIDENTIFIER  DEFAULT NEWSEQUENTIALID() CONSTRAINT order_pk PRIMARY KEY,
	orderStatus INT DEFAULT 0  CONSTRAINT order_value CHECK(orderStatus IN (0,1,2,3)),
	createdeAt DATETIME NOT NULL,
	updatedAt DATETIME NOT NULL,
	typeOrder INT DEFAULT 0 CONSTRAINT type_order_ck CHECK(typeOrder IN (0,1,2)),
	address NVARCHAR(100),
	postalCode NVARCHAR(20),
	city NVARCHAR(50),
	country NVARCHAR(20),
	creditCardId UNIQUEIDENTIFIER NOT NULL CONSTRAINT credit_card_fk FOREIGN KEY (creditCardId) REFERENCES CreditCard(Id),
	chartId UNIQUEIDENTIFIER UNIQUE CONSTRAINT chart_id FOREIGN KEY (chartId) REFERENCES Chart(id)
)