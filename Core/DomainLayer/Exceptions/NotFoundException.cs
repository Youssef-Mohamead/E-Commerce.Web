using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Exceptions
{
    public abstract class NotFoundException(String Message) : Exception(Message)
    {

    }
}
