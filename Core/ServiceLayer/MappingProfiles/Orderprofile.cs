using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using DomainLayer.Models.Order_Module;
using DomainLayer.Models.ProductModule;
using Shared.DataTransferObjects.IdentityDTOs;
using Shared.DataTransferObjects.OrderDTOs;
using Shared.DataTransferObjects.ProductDTOs;

namespace Service.MappingProfiles
{
    public class Orderprofile : Profile
    {
        public Orderprofile()
        {
            CreateMap<AddressDTO, OrderAddress>().ReverseMap();

            CreateMap<Order, OrderToReturnDTO>()
                .ForMember(dist => dist.DeliveryMethod, Options => Options.MapFrom(Src => Src.DeliveryMethod.ShortName));


            CreateMap<OrderItem, OrderItemDTO>()
                .ForMember(dist => dist.ProductName, Options => Options.MapFrom(Src => Src.Product.ProductName))
                .ForMember(dist => dist.PictureUrl, Options => Options.MapFrom<OrderItemPictureUrlResolver>());

            CreateMap<DeliveryMethod, DeliveryMethodDTOs>();
        }
    }
}
