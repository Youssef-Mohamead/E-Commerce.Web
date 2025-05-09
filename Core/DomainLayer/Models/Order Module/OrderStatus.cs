using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Models.Order_Module
{
    public enum OrderStatus
    {
        pending = 0,
        paymentRecevied = 1,
        paymentFailed = 2
    }
}
