﻿using MundiPagg.Api.Products.Dto.Products;
using System;
using System.Collections.Generic;
using System.Text;

namespace MundPagg.Api.Product.Dto.Products
{
    public class AddProductDto
    {
        public string Name { get; set; }

        public string Code { get;  set; }

        public DepartamentDto Departament { get; set; } = new DepartamentDto();

    }
}
