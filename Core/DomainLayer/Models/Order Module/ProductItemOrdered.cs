using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Models.Order_Module
{
    public class ProductItemOrdered
    {
        public int Id {  get; set; }
        public string ProductName { get; set; } = default!;
        public string ProductUrl { get; set; } = default!;
    }
}
