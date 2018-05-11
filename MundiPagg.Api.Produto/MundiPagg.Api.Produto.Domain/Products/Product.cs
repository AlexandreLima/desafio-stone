using MundiPagg.Api.Product.Domain.Departaments;
using MundiPagg.Api.Product.Domain.Utilities;
using System;
using System.Collections.Generic;
using System.Text;

namespace MundiPagg.Api.Product.Domain.Products
{
    public class Product : AggregateRoot
    {
        public Product() : base() { }
        public string Name { get; private set; }

        public string Code { get; private set; }

        public Departament Departament { get; private set; }

    }
}
