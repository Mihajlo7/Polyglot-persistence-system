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
	number NVARCHAR(50) NOT NULL,
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

CREATE TYPE ConsumersTableType AS TABLE (
	FirstName NVARCHAR(255) ,
	LastName NVARCHAR(255) ,
	Email NVARCHAR(100),
	BirthDate DATE ,
	Telephone VARCHAR(20),
	Password NVARCHAR(255),
	CreditCardType1 NVARCHAR(20),
	CreditCardNumber1 NVARCHAR(50),
	CreditCardType2 NVARCHAR(20),
	CreditCardNumber2 NVARCHAR(50)
);
GO
CREATE OR ALTER PROC insertConsumer(
	@FirstName NVARCHAR(255) ,
	@LastName NVARCHAR(255) ,
	@BirthDate DATE ,
	@Email NVARCHAR(100),
	@Telephone VARCHAR(20),
	@Password NVARCHAR(255),
	@CreditCardType1 NVARCHAR(20),
	@CreditCardNumber1 NVARCHAR(50),
	@CreditCardType2 NVARCHAR(20),
	@CreditCardNumber2 NVARCHAR(50)
)
AS
BEGIN
	SET NOCOUNT ON;
	BEGIN TRAN;
	BEGIN TRY
		DECLARE @UserId UNIQUEIDENTIFIER;
		INSERT INTO Users (email, passwordHash, passwordSalt) VALUES (@Email, HASHBYTES('SHA2_256', @Password), CAST(@Password AS VARBINARY(255)));
		SELECT @UserId= id  FROM Users WHERE email=@Email;
		INSERT INTO Consumers (Id,firstname,lastname,birthdate,telephone) VALUES( @UserId, @FirstName,@LastName,@BirthDate,@Telephone);

		INSERT INTO CreditCard (consumerId,cardType,number) VALUES( @UserId, @CreditCardType1, @CreditCardNumber1);

		IF @CreditCardType2 IS NOT NULL AND @CreditCardNumber2 IS NOT NULL
			INSERT INTO CreditCard (consumerId,cardType,number) VALUES( @UserId, @CreditCardType2, @CreditCardNumber2);

		COMMIT;
	END TRY
	BEGIN CATCH
		IF @@TRANCOUNT>0
			ROLLBACK;
		THROW; 
	END CATCH;
END;

----------------------------
CREATE TABLE Categories (
	Id UNIQUEIDENTIFIER DEFAULT NEWSEQUENTIALID() CONSTRAINT category_pk PRIMARY KEY,
	name NVARCHAR(50) NOT NULL
);

-- create sequnce for product
CREATE SEQUENCE subCategory_seq AS INT START WITH 1 INCREMENT BY 7 NO CYCLE;

CREATE TABLE SubCategories (
	Id UNIQUEIDENTIFIER DEFAULT NEWSEQUENTIALID() CONSTRAINT subCategory_pk PRIMARY KEY,
	subCategoryNumber  INT DEFAULT NEXT VALUE FOR subCategory_seq CONSTRAINT subCategory_num UNIQUE,
	name NVARCHAR(50) NOT NULL,
	categoryId UNIQUEIDENTIFIER CONSTRAINT subCategory_fk FOREIGN KEY (categoryId) REFERENCES Categories(Id) ON DELETE SET NULL ON UPDATE CASCADE
);



CREATE TABLE Companies(
	Id UNIQUEIDENTIFIER DEFAULT NEWSEQUENTIALID() CONSTRAINT company_pk PRIMARY KEY,
	dunsNumber NVARCHAR(100) NOT NULL,
	name NVARCHAR(100) NOT NULL,
	telephone NVARCHAR(100) NOT NULL,
	country NVARCHAR(50) NOT NULL
)

CREATE TABLE Sellers (
	id UNIQUEIDENTIFIER CONSTRAINT seller_pk PRIMARY KEY,
	address NVARCHAR(255) NOT NULL,
	city NVARCHAR(100) NOT NULL,
	hasShop BIT NOT NULL
	CONSTRAINT seller_fk FOREIGN KEY (id) REFERENCES Companies(id) ON DELETE CASCADE
)

CREATE TABLE Couriers (
	id UNIQUEIDENTIFIER CONSTRAINT courier_pk PRIMARY KEY,
	deliveryPrice DECIMAL(7,2) NOT NULL
	CONSTRAINT courier_fk FOREIGN KEY (id) REFERENCES Companies(id) ON DELETE CASCADE
)

CREATE TABLE CourierContacts (
	courierId UNIQUEIDENTIFIER,
	companyId UNIQUEIDENTIFIER,
	serialNumContact NVARCHAR(255) NOT NULL,
	contactInfo NVARCHAR(500)
	CONSTRAINT courier_contact_pk PRIMARY KEY (courierId,companyId),
	CONSTRAINT courier_c_fk FOREIGN KEY (courierId) REFERENCES Couriers(id) ,
	CONSTRAINT company_c_fk FOREIGN KEY (companyId) REFERENCES Companies(id)
)

GO
CREATE OR ALTER PROC insertSeller 
(@DunsNumber NVARCHAR(100),@Name  NVARCHAR(100), @Telephone  NVARCHAR(100),@Country NVARCHAR(50),@Address  NVARCHAR(255),@City  NVARCHAR(100), @HasShop BIT) AS
BEGIN
	SET NOCOUNT ON;
	BEGIN TRAN;
	BEGIN TRY
		INSERT INTO Companies (dunsNumber,name,telephone,country) VALUES(@DunsNumber,@Name,@Telephone,@Country);

		INSERT INTO Sellers (id,address,city,hasShop) SELECT id, @Address,@City,@HasShop FROM Companies WHERE telephone=@Telephone AND dunsNumber=@DunsNumber;

		COMMIT;
	END TRY
	BEGIN CATCH
		IF @@TRANCOUNT>0
			ROLLBACK;
		THROW;
	END CATCH
END;

GO

CREATE OR ALTER PROC insertCourier 
(@DunsNumber NVARCHAR(100),@Name  NVARCHAR(100), @Telephone  NVARCHAR(100),@Country NVARCHAR(50),@DeliveryPrice DECIMAL(7,2)) AS
BEGIN
	SET NOCOUNT ON;
	BEGIN TRAN;
	BEGIN TRY
		INSERT INTO Companies (dunsNumber,name,telephone,country) VALUES(@DunsNumber,@Name,@Telephone,@Country);

		INSERT INTO Couriers (id,deliveryPrice) SELECT id, @DeliveryPrice FROM Companies WHERE telephone=@Telephone AND dunsNumber=@DunsNumber;

		COMMIT;
	END TRY
	BEGIN CATCH
		IF @@TRANCOUNT>0
			ROLLBACK;
		THROW;
	END CATCH
END;
-------------------------------
-- create sequnce for product
CREATE SEQUENCE product_seq AS INT START WITH 1000000 INCREMENT BY 1 NO CYCLE;
CREATE SEQUENCE product_detail_seq AS INT START WITH 1000000 INCREMENT BY 5 NO CYCLE;

CREATE TYPE DistributeProductType AS TABLE(
	SellerId BIGINT,
	Price DECIMAL(7,2)
);

INSERT INTO Categories(name) VALUES ('Car');
INSERT INTO Categories(name) VALUES ('Mobile');
INSERT INTO Categories(name) VALUES ('Laptop');
INSERT INTO Categories(name) VALUES ('Movie');

CREATE TABLE ProductsHeader (
	productId BIGINT DEFAULT NEXT VALUE FOR product_seq CONSTRAINT product_pk PRIMARY KEY,
	name NVARCHAR(100) NOT NULL,
	price DECIMAL(10,2) NOT NULL,
	subCategoryId BIGINT CONSTRAINT product_fk FOREIGN KEY (subCategoryId) REFERENCES SubCategories(id) ON DELETE SET NULL ON UPDATE CASCADE,
	produced BIGINT NOT NULL CONSTRAINT produced_seller_fk FOREIGN KEY (produced) REFERENCES Sellers(id),
	store BIGINT NULL CONSTRAINT store_seller_fk FOREIGN KEY (store) REFERENCES Sellers (Id)
);

CREATE TABLE DistributeProducts (
	productId BIGINT,
	sellerId BIGINT,
	distributionPrice DECIMAL(7,2),
	CONSTRAINT dist_pk PRIMARY KEY (productId,sellerId),
	CONSTRAINT p_seller_fk FOREIGN KEY (sellerid) REFERENCES Sellers(Id),
	CONSTRAINT product_header_fk FOREIGN KEY (productId) REFERENCES ProductsHeader(ProductId)
)

CREATE TABLE ProductDetails (
	ProductDetailId BIGINT DEFAULT NEXT VALUE FOR product_detail_seq CONSTRAINT product_detail_pk PRIMARY KEY,
	ProductId BIGINT CONSTRAINT product_unique UNIQUE,
	shortDescription NVARCHAR(255) NULL,
	imageUrl NVARCHAR(255) NOT NULL
	CONSTRAINT product_detail_fk FOREIGN KEY (ProductId) REFERENCES ProductsHeader(ProductId) ON DELETE CASCADE ON UPDATE CASCADE
);

CREATE TABLE Cars (
	ProductDetailId BIGINT CONSTRAINT cars_pk PRIMARY KEY,
	yearManifactured INT NOT NULL,
	model NVARCHAR(100) NOT NULL,
	serialNumber NVARCHAR(500) NOT NULL,
	engineDisplacement NVARCHAR(20) NOT NULL,
	enginePower NVARCHAR(50) NOT NULL,
	longDescription NVARCHAR(2000),
	CONSTRAINT cars_fk FOREIGN KEY (ProductDetailId) REFERENCES ProductDetails (ProductDetailId)
)

CREATE TABLE Devices (
	ProductDetailId BIGINT CONSTRAINT device_pk PRIMARY KEY,
	yearManifactured INT NOT NULL,
	serialNumber NVARCHAR(500) NOT NULL,
	weight NVARCHAR(10) NULL,
	storage NVARCHAR(10) NOT NULL
	CONSTRAINT device_fk FOREIGN KEY (ProductDetailId) REFERENCES ProductDetails (ProductDetailId)
)

CREATE TABLE Movies (
	ProductDetailId BIGINT CONSTRAINT movie_pk PRIMARY KEY,
	yearRelease INT NOT NULL,
	genre VARCHAR(50) NOT NULL,
	duration INT NOT NULL,
	subtitling NVARCHAR(50) NOT NULL,
	CONSTRAINT movie_fk FOREIGN KEY (ProductDetailId) REFERENCES ProductDetails (ProductDetailId)
)

CREATE TABLE Mobiles (
	ProductDetailId BIGINT CONSTRAINT mobile_pk PRIMARY KEY,
	screenDiagonal VARCHAR(10) NOT NULL,
	operatingSystem VARCHAR(50) NOT NULL,
	color VARCHAR(50) NOT NULL,
	CONSTRAINT mobile_fk FOREIGN KEY (ProductDetailId) REFERENCES Devices (ProductDetailId)
)

CREATE TABLE Laptops (
	ProductDetailId BIGINT CONSTRAINT laptop_pk PRIMARY KEY,
	processor VARCHAR(10) NOT NULL,
	ramMemory VARCHAR(50) NOT NULL,
	longDescription NVARCHAR(2000),
	CONSTRAINT laptop_fk FOREIGN KEY (ProductDetailId) REFERENCES Devices (ProductDetailId)
)
GO

CREATE OR ALTER PROCEDURE  usp_InsertProduct
    @ProductName NVARCHAR(100),
    @Price DECIMAL(10, 2),
    @SubCategoryName NVARCHAR(50),
    @ProducedBy BIGINT,
    @StoreId BIGINT = NULL,
    @ShortDescription NVARCHAR(255) = NULL,
    @ImageUrl NVARCHAR(255),
    @ProductType NVARCHAR(50), -- Tip proizvoda ('Car', 'Device', 'Movie', 'Mobile', 'Laptop')
    @DistributeProducts DistributeProductType READONLY, -- Lista companyId vrednosti za DistributeProducts
    -- Specifični parametri za Cars
    @YearManufactured INT = NULL,
    @CarModel NVARCHAR(100) = NULL,
    @SerialNumber NVARCHAR(500) = NULL,
    @EngineDisplacement NVARCHAR(20) = NULL,
    @EnginePower NVARCHAR(50) = NULL,
    @LongDescription NVARCHAR(2000) = NULL,
    -- Specifični parametri za Devices
    @Weight NVARCHAR(10) = NULL,
    @Storage NVARCHAR(10) = NULL,
    -- Specifični parametri za Movies
    @YearRelease INT = NULL,
    @Genre VARCHAR(50) = NULL,
    @Duration INT = NULL,
    @Subtitling NVARCHAR(50) = NULL,
    -- Specifični parametri za Mobiles
    @ScreenDiagonal VARCHAR(10) = NULL,
    @OperatingSystem VARCHAR(50) = NULL,
    @Color VARCHAR(50) = NULL,
    -- Specifični parametri za Laptops
    @Processor VARCHAR(10) = NULL,
    @RamMemory VARCHAR(50) = NULL
AS
BEGIN
    SET NOCOUNT ON;
    
    DECLARE @NewProductId BIGINT = NEXT VALUE FOR product_seq;
    DECLARE @NewProductDetailId BIGINT = NEXT VALUE FOR product_detail_seq -- Za GUID identifikator
    DECLARE @SubCategoryId BIGINT;
    
    BEGIN TRY
        BEGIN TRANSACTION;

        -- Proveravamo da li podkategorija postoji prema imenu i tipu proizvoda
		SELECT @SubCategoryId = Id 
		FROM SubCategories 
		WHERE Name = @SubCategoryName 
              AND categoryId = (SELECT Id FROM Categories WHERE name = @ProductType);

		-- Ako podkategorija ne postoji, unosimo novu
		IF @SubCategoryId IS NULL
		BEGIN
			INSERT INTO SubCategories (name, categoryId) 
            VALUES (@SubCategoryName, (SELECT Id FROM Categories WHERE name = @ProductType));

			-- Ponovno dobijanje @SubCategoryId
			SELECT @SubCategoryId = Id 
			FROM SubCategories
			WHERE Name = @SubCategoryName 
                  AND categoryId = (SELECT Id FROM Categories WHERE name = @ProductType);
		END;

        -- Umetanje u ProductsHeader
        INSERT INTO ProductsHeader (ProductId, name, price, subCategoryId, produced, store)
        VALUES (@NewProductId, @ProductName, @Price, @SubCategoryId, @ProducedBy, @StoreId);

        -- Umetanje u ProductDetails
        INSERT INTO ProductDetails (ProductDetailId, ProductId, shortDescription, imageUrl)
        VALUES (@NewProductDetailId, @NewProductId, @ShortDescription, @ImageUrl);

        -- Unos u specifične tabele na osnovu tipa proizvoda
        IF @ProductType = 'Car'
        BEGIN
            INSERT INTO Cars (ProductDetailId, yearManifactured, model, serialNumber, engineDisplacement, enginePower, longDescription)
            VALUES (@NewProductDetailId, @YearManufactured, @CarModel, @SerialNumber, @EngineDisplacement, @EnginePower, @LongDescription);
        END
        ELSE IF @ProductType = 'Device'
        BEGIN
            INSERT INTO Devices (ProductDetailId, yearManifactured, serialNumber, weight, storage)
            VALUES (@NewProductDetailId, @YearManufactured, @SerialNumber, @Weight, @Storage);
        END
        ELSE IF @ProductType = 'Movie'
        BEGIN
            INSERT INTO Movies (ProductDetailId, yearRelease, genre, duration, subtitling)
            VALUES (@NewProductDetailId, @YearRelease, @Genre, @Duration, @Subtitling);
        END
        ELSE IF @ProductType = 'Mobile'
        BEGIN
            INSERT INTO Mobiles (ProductDetailId, screenDiagonal, operatingSystem, color)
            VALUES (@NewProductDetailId, @ScreenDiagonal, @OperatingSystem, @Color);
        END
        ELSE IF @ProductType = 'Laptop'
        BEGIN
            INSERT INTO Laptops (ProductDetailId, processor, ramMemory, longDescription)
            VALUES (@NewProductDetailId, @Processor, @RamMemory, @LongDescription);
        END

        -- Unos u DistributeProducts za svaki SellerId i Price iz DistributeProductType liste
        INSERT INTO DistributeProducts (productId, sellerId, distributionPrice)
        SELECT @NewProductId, SellerId, Price 
        FROM @DistributeProducts;

        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO
------------------------------
CREATE TABLE Charts (
	id UNIQUEIDENTIFIER DEFAULT NEWSEQUENTIALID() CONSTRAINT chart_pk PRIMARY KEY,
	total DECIMAL(10,2) NOT NULL DEFAULT 0.0,
	status INT NOT NULL CONSTRAINT chart_status CHECK (status IN (0,1,2,3)),
	createdAt DATETIME NOT NULL,
	updatedAt DATETIME NOT NULL,
	consumer_id UNIQUEIDENTIFIER NOT NULL CONSTRAINT chart_fk FOREIGN KEY (consumer_id) REFERENCES Consumer(id)
)

CREATE TABLE ChartItems (
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
