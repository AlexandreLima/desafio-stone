using System;
using System.Collections.Generic;
using System.Text;

namespace MundiPagg.Api.Products.Domain.Departaments
{
    public class Departament
    {
        public Departament(string name)
        {
            Name = name;
        }

        public string Name { get; private set; }

    }
}
