﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using BuildMaterials.Core.Contracts;
using BuildMaterials.Data;
using BuildMaterials.Infrastructure.Data.Domain;

namespace BuildMaterials.Core.Services
{
    public class BrandService : IBrandService
    {
        private readonly ApplicationDbContext _context; 
        public BrandService(ApplicationDbContext context) 
        {
            _context = context; 
        }

        public Brand GetBrandById(int brandId)
        {
            return _context.Brands.Find(brandId); 
        }

        public List<Brand> GetBrands() 
        { 
            List<Brand> brands = _context.Brands.ToList(); return brands; 
        }

        public List<Product> GetProductByBrand(int brandId) 
        { 
            return _context.Products.Where(x => x.BrandId == brandId).ToList(); 
        }
    }
   
}
