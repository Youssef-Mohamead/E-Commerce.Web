using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServiceAbstraction;
using Shared.DataTransferObjects.OrderDTOs;

namespace Presentation.Controllers
{
    public class OrdersController(IServiceManager _serviceManager) : ApiBaseController
    {
        // Create Order
        [Authorize]
        [HttpPost]
        public async Task<ActionResult<OrderToReturnDTO>> CreateOrder(OrderDTO orderDTO)
        {
            var Order = await _serviceManager.OrderService.CreateOrder(orderDTO, GetEmailFromToken());
            return Ok(Order);
        }
    }
}
