using System;
using System.Collections.Generic;
using System.Text;

namespace MundiPagg.Api.Products.Domain.Utilities
{
    public interface IAggregateRoot<T>
    {
        T Id { get; } 
    }
}
