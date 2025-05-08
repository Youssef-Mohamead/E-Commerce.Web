using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using DomainLayer.Contracts;
using DomainLayer.Exceptions;
using DomainLayer.Models.Order_Module;
using DomainLayer.Models.ProductModule;
using ServiceAbstraction;
using Shared.DataTransferObjects.IdentityDTOs;
using Shared.DataTransferObjects.OrderDTOs;

namespace Service
{
    public class OrderService(IMapper _mapper, IBasketRepository _basketRepository, IUnitOfWork _unitOfWork) : IOrderService
    {
        public async Task<OrderToReturnDTO> CreateOrder(OrderDTO orderDTO, string Email)
        {
            // Map Address To Order Address

            var OrderAddress = _mapper.Map<AddressDTO, OrderAddress>(orderDTO.Address);

            // Get Basket

            var Basket = await _basketRepository.GetBasketAsync(orderDTO.BasketId)
                ?? throw new BasketNotFoundException(orderDTO.BasketId);

            // Create OrderItem Method

            List<OrderItem> OrderItems = [];
            var productRepo = _unitOfWork.GetRepository<Product, int>();
            var orderItems = new List<OrderItem>();

            foreach (var item in Basket.Items)
            {
                var product = await productRepo.GetByIdAsunc(item.Id)
                    ?? throw new ProductNotFoundException(item.Id);
                orderItems.Add(CreateOrderItem(item, product));
            }

            // Get Delivery Method

            var DeliveryMethod = await _unitOfWork.GetRepository<DeliveryMethod, int>().GetByIdAsunc(orderDTO.DeliveryMethodId)
                ?? throw new DeliveryMethodNotFoundException(orderDTO.DeliveryMethodId);

            // Calculate Sub Total

            var SubTotal = OrderItems.Sum(I => I.Quantity * I.Price);

            var Order = new Order(Email, OrderAddress, DeliveryMethod, OrderItems, SubTotal);
         
            await _unitOfWork.GetRepository<Order,Guid>().AddAsync(Order);
            await _unitOfWork.SaveChanges();
            return _mapper.Map<Order , OrderToReturnDTO>(Order);
        }

        private static OrderItem CreateOrderItem(DomainLayer.Models.BasketModule.BasketItem item, Product product)
        {
            return new OrderItem
            {
                Product = new ProductItemOrdered()
                {
                    Id = product.Id,
                    ProductUrl = product.PictureUrl,
                    ProductName = product.Name
                },
                Price = product.Price,
                Quantity = item.Quantity
            };
        }
    }
}
