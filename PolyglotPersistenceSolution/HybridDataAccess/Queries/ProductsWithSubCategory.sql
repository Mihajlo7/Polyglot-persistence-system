
-- Get all products --
SELECT product.id Id,product.name Name,product.price Price, s.id SubCategoryId, s.name SubCategoryName 
FROM SubCategories s
CROSS APPLY OPENJSON(products)
WITH (
	id BIGINT '$.id',
	name NVARCHAR(30) '$.name',
	price DECIMAL(7,2) '$.price'
	)AS product;

-- Get product by Id --
SELECT product.id Id,product.name Name,product.price Price, s.id SubCategoryId, s.name SubCategoryName 
FROM SubCategories s
CROSS APPLY OPENJSON(products)
WITH (
	id BIGINT '$.id',
	name NVARCHAR(30) '$.name',
	price DECIMAL(7,2) '$.price'
	)AS product
WHERE product.id=@ProductId;

-- Get products By SubCategoryId --
SELECT products
FROM SubCategories
WHERE id=@SubCategoryId;

-- Get products By SubCategoryName
SELECT products
FROM SubCategories
WHERE name=@SubCategoryName;

-- Update price by ProductId --
WITH cte AS(
	SELECT [key],products
	FROM SubCategories CROSS APPLY OPENJSON(products) p
	WHERE JSON_VALUE(p.value,'$.Id')=@ProductId
)
UPDATE cte
SET products = JSON_MODIFY(products,'$['+cte.[key]+'].Price',@Price)

-- Update price by SubCategoryId
WITH cte AS(
	SELECT [key],[value], JSON_VALUE([value],'$.Price') Price, id
	FROM SubCategories CROSS APPLY OPENJSON(products)
	WHERE id=1
)
UPDATE cte
SET products = JSON_MODIFY(products,'$['+cte.[key]+'].Price',Price*0.2+Price)

