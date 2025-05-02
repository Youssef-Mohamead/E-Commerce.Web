using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ServiceAbstraction;
using Shared.DataTransferObjects.BasketModuleDTOs;

namespace Presentation.Controllers
{
   
    public class BasketController(IServiceManager _serviceManager) : ApiBaseController
    {
        // Get Basket
        [HttpGet] // GET BaseUrl/api/Basket

        public async Task<ActionResult<BasketDTO>> GetBasket(string Key)
        {
            var Basket = await _serviceManager.BasketService.GetBasketAsync(Key);
            return Ok(Basket);
        }
        // Create Or Update Basket
        [HttpPost] // POST BaseUrl/api/Basket

        public async Task<ActionResult<BasketDTO>> CreateOrUpdateBasket(BasketDTO basket)
        {
            var Basket = await _serviceManager.BasketService.CreateOrUpdateBasketAsync(basket);
            return Ok(Basket);
        }
        // Delete
        [HttpDelete("{Key}")]  // DELETE BaseUrl/api/Basket/ id

        public async Task<ActionResult<bool>> DeleteBasket(string Key)
        {
            var Basket = await _serviceManager.BasketService.DeleteBasketAsync(Key);
            return Ok(Basket);
        }
    }
}
