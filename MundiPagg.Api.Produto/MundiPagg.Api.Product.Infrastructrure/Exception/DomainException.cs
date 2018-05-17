using System;
using System.Collections.Generic;
using System.Text;

namespace MundiPagg.Api.Products.Infrastructrure.Exception
{
    public class DomainException : System.Exception
    {
        public DomainException(string message) : base(message)
        {
        }
    }
}
