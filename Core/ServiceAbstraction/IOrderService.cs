using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shared.DataTransferObjects.OrderDTOs;

namespace ServiceAbstraction
{
    public interface IOrderService
    {
        Task<OrderToReturnDTO> CreateOrder(OrderDTO orderDTO, string Email);

        // Get Delivery methods

        Task<IEnumerable<DeliveryMethodDTOs>> GetDeliveryMethodsAsync();

        // Get All Orders

        Task<IEnumerable<OrderToReturnDTO>> GetAllOrdersAsync(string Email);


        // Get Order By Id

        Task<OrderToReturnDTO> GetOrderByIdAsync(Guid Id);
    }
}
