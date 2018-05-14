using MongoDB.Bson;
using MundiPagg.Api.Products.Application.Products.Contracts;
using MundiPagg.Api.Products.Data.Mongo.Repository.Products.Contract;
using MundiPagg.Api.Products.Domain.Products;
using MundiPagg.Api.Products.Domain.Repository;
using MundPagg.Api.Product.Dto.Products;
using System;
using System.Collections.Generic;
using System.Text;

namespace MundiPagg.Api.Products.Application.Products.Implementation
{
    public class ProductApplication : IProductApplication
    {
        private readonly IProductMongoRepository productMongoRepository;

        public ProductApplication(IProductMongoRepository productMongoRepository)
            => this.productMongoRepository = productMongoRepository;

        public void Add(AddProductDto dto)
        {
            var product = new Product(dto.Name, dto.Code, dto.Departament.Name);
            productMongoRepository.Add(product);
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
