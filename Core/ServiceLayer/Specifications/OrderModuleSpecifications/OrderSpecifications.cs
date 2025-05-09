using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainLayer.Models.Order_Module;

namespace Service.Specifications.OrderModuleSpecifications
{
     class OrderSpecifications :BaseSpecifications<Order ,Guid>
    {
        // Get all Orders By Email
        public OrderSpecifications(string Email):base(O=>O.UserEmail ==Email)
        {
            AddInclude(O => O.DeliveryMethod);
            AddInclude(O => O.Items);
            AddOrderByDescending(O => O.OrderDate);
        }
        
        // Get all Orders By Id
        public OrderSpecifications(Guid Id):base(O=>O.Id ==Id)
        {
            AddInclude(O => O.DeliveryMethod);
            AddInclude(O => O.Items);
        }
    }
}
