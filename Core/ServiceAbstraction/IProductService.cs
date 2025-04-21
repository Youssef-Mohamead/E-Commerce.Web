using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shared.DataTransferObjects;

namespace ServiceAbstraction
{
    public interface IProductService
    {
        // Get All Products
        Task<IEnumerable<ProductDto>> GetAllProductsAsync();

        // Get Product By Id
        Task<ProductDto> GetProductByIdAsync(int Id);

        // Get All Types
        Task<IEnumerable<TypeDto>> GetAllTypeAsync();

        // Get All Brands
        Task<IEnumerable<BrandDto>> GetAllBrandAsync();
    }
}
