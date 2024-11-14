CREATE TABLE Products(
	id BIGINT CONSTRAINT product_d_pk PRIMARY KEY(id),
	name NVARCHAR(50) NOT NULL,
	price DECIMAL(7,2) NOT NULL,
	detail NVARCHAR(MAX) NULL,
	subCategoryId BIGINT
)
GO

CREATE OR ALTER PROC CreateProductWithDetail
	@Id BIGINT,
	@Name NVARCHAR(50),
	@Price DECIMAL(7,2),
	@Detail NVARCHAR(MAX),
	@SubCategoryId BIGINT
AS
BEGIN
	INSERT INTO Products (id,name,price,detail,subCategoryId) VALUES (@Id,@Name,@Price,@Detail,@SubCategoryId)
END
GO

DROP TYPE IF EXISTS ProductWithDetailType;
GO

CREATE TYPE ProductWithDetailType AS TABLE(
	Id BIGINT,
	Name NVARCHAR(50),
	Price DECIMAL(7,2),
	Detail NVARCHAR(MAX),
	SubCategoryId BIGINT
);
GO

CREATE OR ALTER PROC CreateProductsWithDetailsBulk
	@ProductsWithDetails ProductWithDetailType READONLY
AS
BEGIN
	INSERT INTO Products (id,name,price,detail,subCategoryId)
	SELECT Id, Name,Price,Detail,SubCategoryId
	FROM @ProductsWithDetails
END;

SELECT * FROM Products;

SELECT p.id Id, p.name Name,p.price Price,p.detail Detail,p.subCategoryId SubCategoryId FROM Products p;