﻿using Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SQLDataAccess
{
    public interface IProductRepository
    {
        public Task<int> InsertOne(ProductModel productModel);
        public Task<int> InsertMany(List<ProductModel> products);
        public Task<List<ProductModel>> GetAllProductsWithCompaniesBySelect();
    }
}