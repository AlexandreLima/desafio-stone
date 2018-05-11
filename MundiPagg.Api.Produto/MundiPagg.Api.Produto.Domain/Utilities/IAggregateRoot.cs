using System;
using System.Collections.Generic;
using System.Text;

namespace MundiPagg.Api.Product.Domain.Utilities
{
    public interface IAggregateRoot<T>
    {
        T ID { get; } 
    }
}
