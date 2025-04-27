using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shared.DataTransferObjects.BasketModuleDTOs;

namespace ServiceAbstraction
{
    public interface IBasketService
    {
        Task<BasketDTO> GetBasketAsync(string Key);
        Task<BasketDTO> CreateOrUpdateBasketAsync(BasketDTO basket);
        Task<bool> DeleteBasketAsync(string Key);
    }
}
