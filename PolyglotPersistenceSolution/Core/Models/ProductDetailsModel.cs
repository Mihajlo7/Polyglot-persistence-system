using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models
{
    public class ProductDetailsModel
    {
        public Guid Id { get; set; }
        public Guid ProductId { get; set; }

        public string? ShortDescription { get; set; }
        public string ImageUrl { get; set; }
    }
}
