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
    }
}
