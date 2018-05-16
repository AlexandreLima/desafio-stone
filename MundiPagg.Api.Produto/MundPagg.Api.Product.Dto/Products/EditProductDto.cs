using System;
using System.Collections.Generic;
using System.Text;

namespace MundPagg.Api.Product.Dto.Products
{
    public class EditProductDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public DepartamentDto Departament { get; set; }
    }
}
