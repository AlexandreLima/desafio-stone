using MundPagg.Api.Product.Dto.Products;
using System;
using System.Collections.Generic;
using System.Text;

namespace MundiPagg.Api.Products.Application.Products.Contracts
{
    public interface IProductApplication
    {
        void Add(AddProductDto dto);
        ProductDto Detail(Guid guid);
        void Remove(Guid guid);
        IEnumerable<ProductDto> All(int rowsPerPagin, int pagin);

    }
}
