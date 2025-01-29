﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using BuildMaterials.Infrastructure.Data.Domain;

namespace BuildMaterials.Core.Contracts
{
    public interface IBrandService
    {
        List<Brand> GetBrands();
        Brand GetBrandById(int brandId);
        List<Product>GetProductByBrand(int brandId);
    }
}
