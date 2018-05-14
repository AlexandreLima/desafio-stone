using MundiPagg.Api.Products.Domain.Departaments;
using MundiPagg.Api.Products.Domain.Utilities;
using System;
using System.Collections.Generic;
using System.Text;

namespace MundiPagg.Api.Products.Domain.Products
{
    public class Product : AggregateRoot
    {
        private Product() : base() { }

        public Product(string name, string code, string departamentName)
        {
            Name = name;
            Code = code;
            Departament = new Departament(departamentName);
        }

        public string Name { get; private set; }

        public string Code { get; private set; }

        public Departament Departament { get; private set; }

    }
}
