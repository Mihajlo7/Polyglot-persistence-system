﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models
{
    public class SubCategoryModel
    {
        public Guid Id { get; set; }
        public int SubCategoryNumber { get; set; }
        public string Name { get; set; }

        public Guid CategoryId { get; set; }
    }
}
