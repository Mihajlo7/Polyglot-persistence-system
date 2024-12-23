﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SQLDataAccess.Resources {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "17.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    internal class ProductsQueries {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal ProductsQueries() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("SQLDataAccess.Resources.ProductsQueries", typeof(ProductsQueries).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   Overrides the current thread's CurrentUICulture property for all
        ///   resource lookups using this strongly typed resource class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to SELECT ph.productId ProductId,ph.name Name, ph.price Price,
        ///cp.Id ProduceId, cp.dunsNumber ProduceDunsNumber, cp.name ProduceName, cp.telephone ProduceTelephone,cp.country ProduceCountry , p.address ProduceAddress, p.city ProduceCity, p.hasShop ProduceShop,
        ///cs.Id StoreId, cs.dunsNumber StoreDunsNumber, cs.name StoreName, cs.telephone StoreTelephone,cs.country StoreCountry , s.address StoreAddress, s.city StoreCity, s.hasShop StoreShop,d.distributionPrice DistributionPrice,
        ///cd.Id DistributeId, cd.dunsNumb [rest of string was truncated]&quot;;.
        /// </summary>
        internal static string GetProductById {
            get {
                return ResourceManager.GetString("GetProductById", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to SELECT ph.productId ProductId,ph.name Name, ph.price Price,
        ///cp.Id ProduceId, cp.dunsNumber ProduceDunsNumber, cp.name ProduceName, cp.telephone ProduceTelephone,cp.country ProduceCountry , p.address ProduceAddress, p.city ProduceCity, p.hasShop ProduceShop,
        ///cs.Id StoreId, cs.dunsNumber StoreDunsNumber, cs.name StoreName, cs.telephone StoreTelephone,cs.country StoreCountry , s.address StoreAddress, s.city StoreCity, s.hasShop StoreShop,d.distributionPrice DistributionPrice,
        ///cd.Id DistributeId, cd.dunsNumb [rest of string was truncated]&quot;;.
        /// </summary>
        internal static string GetProductsByCountryAndPrice {
            get {
                return ResourceManager.GetString("GetProductsByCountryAndPrice", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to SELECT ph.productId ProductId,ph.name Name, ph.price Price,
        ///cp.Id ProduceId, cp.dunsNumber ProduceDunsNumber, cp.name ProduceName, cp.telephone ProduceTelephone,cp.country ProduceCountry , p.address ProduceAddress, p.city ProduceCity, p.hasShop ProduceShop,
        ///cs.Id StoreId, cs.dunsNumber StoreDunsNumber, cs.name StoreName, cs.telephone StoreTelephone,cs.country StoreCountry , s.address StoreAddress, s.city StoreCity, s.hasShop StoreShop,d.distributionPrice DistributionPrice,
        ///cd.Id DistributeId, cd.dunsNumb [rest of string was truncated]&quot;;.
        /// </summary>
        internal static string GetProductsByJoinOptimised {
            get {
                return ResourceManager.GetString("GetProductsByJoinOptimised", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to SELECT * FROM ProductsHeader ph
        ///INNER JOIN Companies cp ON (cp.Id=ph.produced) INNER JOIN Sellers p ON (ph.produced=p.id)
        ///LEFT JOIN Companies cs ON (cs.Id=ph.store) LEFT JOIN Sellers s ON(s.id=ph.store)
        ///INNER JOIN DistributeProducts d ON (d.productId=ph.productId)
        ///INNER JOIN Companies cd ON(cd.Id=d.sellerId) INNER JOIN Sellers sd ON(d.sellerId=sd.id);
        ///.
        /// </summary>
        internal static string GetProductsByJoinPlain {
            get {
                return ResourceManager.GetString("GetProductsByJoinPlain", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to SELECT ph.productId ProductId,ph.name Name, ph.price Price,
        ///cp.Id ProduceId, cp.dunsNumber ProduceDunsNumber, cp.name ProduceName, cp.telephone ProduceTelephone,cp.country ProduceCountry , p.address ProduceAddress, p.city ProduceCity, p.hasShop ProduceShop,
        ///cs.Id StoreId, cs.dunsNumber StoreDunsNumber, cs.name StoreName, cs.telephone StoreTelephone,cs.country StoreCountry , s.address StoreAddress, s.city StoreCity, s.hasShop StoreShop,d.distributionPrice DistributionPrice,
        ///cd.Id DistributeId, cd.dunsNumb [rest of string was truncated]&quot;;.
        /// </summary>
        internal static string GetProductsByName {
            get {
                return ResourceManager.GetString("GetProductsByName", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to SELECT ph.productId ProductId,ph.name Name, ph.price Price,
        ///cp.Id ProduceId, cp.dunsNumber ProduceDunsNumber, cp.name ProduceName, cp.telephone ProduceTelephone,cp.country ProduceCountry , p.address ProduceAddress, p.city ProduceCity, p.hasShop ProduceShop,
        ///cs.Id StoreId, cs.dunsNumber StoreDunsNumber, cs.name StoreName, cs.telephone StoreTelephone,cs.country StoreCountry , s.address StoreAddress, s.city StoreCity, s.hasShop StoreShop,d.distributionPrice DistributionPrice,
        ///cd.Id DistributeId, cd.dunsNumb [rest of string was truncated]&quot;;.
        /// </summary>
        internal static string GetProductsByNameAndDistributionCountryAndPrice {
            get {
                return ResourceManager.GetString("GetProductsByNameAndDistributionCountryAndPrice", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to SELECT ph.productId ProductId,ph.name Name, ph.price Price,
        ///cp.Id ProduceId, cp.dunsNumber ProduceDunsNumber, cp.name ProduceName, cp.telephone ProduceTelephone,cp.country ProduceCountry , p.address ProduceAddress, p.city ProduceCity, p.hasShop ProduceShop,
        ///cs.Id StoreId, cs.dunsNumber StoreDunsNumber, cs.name StoreName, cs.telephone StoreTelephone,cs.country StoreCountry , s.address StoreAddress, s.city StoreCity, s.hasShop StoreShop,d.distributionPrice DistributionPrice,
        ///cd.Id DistributeId, cd.dunsNumb [rest of string was truncated]&quot;;.
        /// </summary>
        internal static string GetProductsByNameWithLike {
            get {
                return ResourceManager.GetString("GetProductsByNameWithLike", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to SELECT 
        ///    ph.productId AS ProductId,
        ///    ph.name AS Name,
        ///    ph.price AS Price,
        ///    -- Podupit za proizvođača, sa ograničenjem na jedan red
        ///    (SELECT TOP 1 cp.Id FROM Companies cp WHERE cp.Id = ph.produced) AS ProduceId,
        ///    (SELECT TOP 1 cp.dunsNumber FROM Companies cp WHERE cp.Id = ph.produced) AS ProduceDunsNumber,
        ///    (SELECT TOP 1 cp.name FROM Companies cp WHERE cp.Id = ph.produced) AS ProduceName,
        ///    (SELECT TOP 1 cp.telephone FROM Companies cp WHERE cp.Id = ph.produced) AS ProduceTeleph [rest of string was truncated]&quot;;.
        /// </summary>
        internal static string GetProductsBySubQuery {
            get {
                return ResourceManager.GetString("GetProductsBySubQuery", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to SELECT 
        ///    ph.productId AS ProductId,
        ///    ph.name AS Name,
        ///    ph.price AS Price,
        ///    produceData.ProduceId,
        ///    produceData.ProduceDunsNumber,
        ///    produceData.ProduceName,
        ///    produceData.ProduceTelephone,
        ///    produceData.ProduceCountry,
        ///    produceData.ProduceAddress,
        ///    produceData.ProduceCity,
        ///    produceData.ProduceShop,
        ///    storeData.StoreId,
        ///    storeData.StoreDunsNumber,
        ///    storeData.StoreName,
        ///    storeData.StoreTelephone,
        ///    storeData.StoreCountry,
        ///    storeData.StoreAddress,        /// [rest of string was truncated]&quot;;.
        /// </summary>
        internal static string GetProductsBySubQueryWithApply {
            get {
                return ResourceManager.GetString("GetProductsBySubQueryWithApply", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to SELECT 
        ///    ph.productId AS ProductId,
        ///    ph.name AS ProductName,
        ///    ph.price AS Price,
        ///    ph.subCategoryId AS SubCategoryId,
        ///    ph.produced AS Produced,
        ///    ph.store AS Store,
        ///    pd.ProductDetailId AS ProductDetailId,
        ///    pd.ProductId AS DetailProductId,
        ///    pd.shortDescription AS ShortDescription,
        ///    pd.imageUrl AS ImageUrl,
        ///    c.yearManifactured AS CarYearManufactured,
        ///    c.model AS CarModel,
        ///    c.serialNumber AS CarSerialNumber,
        ///    c.engineDisplacement AS CarEngineDisplacement,
        /// [rest of string was truncated]&quot;;.
        /// </summary>
        internal static string GetProductsWithDetailsByJoinOptimised {
            get {
                return ResourceManager.GetString("GetProductsWithDetailsByJoinOptimised", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to 	SELECT 
        ///    ph.productId AS ProductId,
        ///    ph.name AS ProductName,
        ///    ph.price AS Price,
        ///    ph.subCategoryId AS SubCategoryId,
        ///    ph.produced AS Produced,
        ///    ph.store AS Store,
        ///    
        ///    -- Detalji proizvoda
        ///    (SELECT pd.ProductDetailId FROM ProductDetails pd WHERE ph.productId = pd.ProductId) AS ProductDetailId,
        ///    (SELECT pd.shortDescription FROM ProductDetails pd WHERE ph.productId = pd.ProductId) AS ShortDescription,
        ///    (SELECT pd.imageUrl FROM ProductDetails pd WHERE ph.productId = p [rest of string was truncated]&quot;;.
        /// </summary>
        internal static string GetProductsWithDetailsBySubQuery {
            get {
                return ResourceManager.GetString("GetProductsWithDetailsBySubQuery", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to SELECT 
        ///    ph.productId AS ProductId,
        ///    ph.name AS ProductName,
        ///    ph.price AS Price,
        ///    ph.subCategoryId AS SubCategoryId,
        ///    ph.produced AS Produced,
        ///    ph.store AS Store,
        ///
        ///    pd.ProductDetailId AS ProductDetailId,
        ///    pd.shortDescription AS ShortDescription,
        ///    pd.imageUrl AS ImageUrl,
        ///
        ///
        ///	c.ProductDetailId AS CarDetailsId,
        ///    c.yearManifactured AS CarYearManufactured,
        ///    c.model AS CarModel,
        ///    c.serialNumber AS CarSerialNumber,
        ///    c.engineDisplacement AS CarEngineDisplaceme [rest of string was truncated]&quot;;.
        /// </summary>
        internal static string GetProductsWithDetailsBySubQueryApply {
            get {
                return ResourceManager.GetString("GetProductsWithDetailsBySubQueryApply", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to SELECT 
        ///    ph.productId AS ProductId,
        ///    ph.name AS ProductName,
        ///    ph.price AS Price,
        ///    ph.subCategoryId AS SubCategoryId,
        ///    ph.produced AS Produced,
        ///    ph.store AS Store,
        ///    pd.ProductDetailId AS ProductDetailId,
        ///    --pd.ProductId AS DetailProductId,
        ///    pd.shortDescription AS ShortDescription,
        ///    pd.imageUrl AS ImageUrl,
        ///    c.yearManifactured AS CarYearManufactured,
        ///    c.model AS CarModel,
        ///    c.serialNumber AS CarSerialNumber,
        ///    c.engineDisplacement AS CarEngineDisplacement, [rest of string was truncated]&quot;;.
        /// </summary>
        internal static string GetProductWithDetailById {
            get {
                return ResourceManager.GetString("GetProductWithDetailById", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to SELECT * 
        ///FROM ProductsHeader ph LEFT JOIN ProductDetails pd ON (ph.productId=pd.ProductId)
        ///LEFT JOIN Cars c ON (c.ProductDetailId=pd.ProductDetailId)
        ///LEFT JOIN Devices d ON (d.ProductDetailId=pd.ProductDetailId) 
        ///LEFT JOIN Mobiles m ON (m.ProductDetailId=d.ProductDetailId)
        ///LEFT JOIN Laptops l ON (pd.ProductDetailId=l.ProductDetailId)
        ///LEFT JOIN Movies mo ON (mo.ProductDetailId=pd.ProductDetailId)
        ///.
        /// </summary>
        internal static string GetProductWithDetailsLeftJoin {
            get {
                return ResourceManager.GetString("GetProductWithDetailsLeftJoin", resourceCulture);
            }
        }
    }
}
