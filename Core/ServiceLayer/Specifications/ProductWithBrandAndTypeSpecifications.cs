using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainLayer.Contracts;
using DomainLayer.Models;

namespace Service.Specifications
{
    public class ProductWithBrandAndTypeSpecifications : BaseSpecifications<Product , int>
    {
        //Get All Products With Types And Brands
        public ProductWithBrandAndTypeSpecifications() : base(null)
        {
            AddInclude(p => p.ProductBrand);
            AddInclude(p => p.ProductType);
        }
        //Get Product By Id
        public ProductWithBrandAndTypeSpecifications(int id) : base(p=>p.Id==id)
        {
            AddInclude(p => p.ProductBrand);
            AddInclude(p => p.ProductType);

        }
    }
}
