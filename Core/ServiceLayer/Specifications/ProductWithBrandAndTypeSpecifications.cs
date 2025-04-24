using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainLayer.Contracts;
using DomainLayer.Models;
using Shared;

namespace Service.Specifications
{
    public class ProductWithBrandAndTypeSpecifications : BaseSpecifications<Product, int>
    {
        //Get All Products With Types And Brands
        public ProductWithBrandAndTypeSpecifications(ProductQueryParams queryParams) : base(p => (!queryParams.BrandId.HasValue || p.BrandId == queryParams.BrandId) && (!queryParams.TypeId.HasValue || p.TypeId == queryParams.TypeId))
        {
            AddInclude(p => p.ProductBrand);
            AddInclude(p => p.ProductType);
            switch (queryParams.SortingOption)
            {
                case ProductSortingOptions.NameAsc:
                    AddOrderBy(p => p.Name);
                    break;

                case ProductSortingOptions.NameDesc:
                    AddOrderByDescending(p => p.Name);
                    break;

                case ProductSortingOptions.PriceAsc:
                    AddOrderBy(p => p.Price);
                    break;

                case ProductSortingOptions.PriceDesc:
                    AddOrderByDescending(p => p.Price);
                    break;
                default:
                    break;
            }
        }
        //Get Product By Id
        public ProductWithBrandAndTypeSpecifications(int id) : base(p => p.Id == id)
        {
            AddInclude(p => p.ProductBrand);
            AddInclude(p => p.ProductType);

        }
    }
}
