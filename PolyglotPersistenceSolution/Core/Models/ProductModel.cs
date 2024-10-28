using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models
{
    public class ProductModel
    {
        public Guid Id { get; set; }
        public long ProductNumber { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public SellerModel Produced { get; set; }
        public SellerModel? Store {  get; set; }
        public ICollection<SellerModel> Distribute {  get; set; }
        public SubCategoryModel SubCategory { get; set; }
        public ProductDetailsModel Details { get; set; }
    }
}
