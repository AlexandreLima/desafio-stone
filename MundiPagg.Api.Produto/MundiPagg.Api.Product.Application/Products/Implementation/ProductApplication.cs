using MundiPagg.Api.Product.Application.Products.Contracts;
using MundPagg.Api.Product.Dto.Products;
using System;
using System.Collections.Generic;
using System.Text;

namespace MundiPagg.Api.Product.Application.Products.Implementation
{
    public class ProductApplication : IProductApplication
    {
        public void Add(AddProductDto dto)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ProductDto> All(int rowsPerPagin, int pagin)
        {
            throw new NotImplementedException();
        }

        public ProductDto Detail(Guid guid)
        {
            throw new NotImplementedException();
        }

        public void Remove(Guid guid)
        {
            throw new NotImplementedException();
        }
    }
}
