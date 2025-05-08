using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using DomainLayer.Models.Order_Module;
using DomainLayer.Models.ProductModule;
using Shared.DataTransferObjects.IdentityDTOs;
using Shared.DataTransferObjects.ProductDTOs;

namespace Service.MappingProfiles
{
    public class Orderprofile : Profile
    {
        public Orderprofile()
        {
            CreateMap<AddressDTO, OrderAddress>();
        }
    }
}
