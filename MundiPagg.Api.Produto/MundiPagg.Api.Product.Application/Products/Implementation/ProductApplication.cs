using AutoMapper;
using MongoDB.Bson;
using MundiPagg.Api.Products.Application.Products.Contracts;
using MundiPagg.Api.Products.Data.Mongo.Repository.Products.Contract;
using MundiPagg.Api.Products.Domain.Products;
using MundiPagg.Api.Products.Domain.Repository;
using MundPagg.Api.Product.Dto.Products;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace MundiPagg.Api.Products.Application.Products.Implementation
{
    public class ProductApplication : IProductApplication
    {
        private readonly IProductMongoRepository productMongoRepository;
        private readonly IMapper _mapper;

        public ProductApplication(IProductMongoRepository productMongoRepository, IMapper mapper)
        {
            this.productMongoRepository = productMongoRepository;
            _mapper = mapper;
        }
     
        public void Add(AddProductDto dto)
        {
            var product = _mapper.Map<Product>(dto);
            productMongoRepository.Add(product);
        }

        public IEnumerable<ProductDto> All(int rowsPerPagin, int pagin)
        {
            var products = productMongoRepository.GetAll(pagin, rowsPerPagin);
            return _mapper.Map<IEnumerable<ProductDto>>(products);
        }

        public ProductDto Detail(Guid guid)
        {
            var product = productMongoRepository.Get(Guid.Parse(guid.ToString()));
            return _mapper.Map<ProductDto>(product);
        }

        public void Edit(EditProductDto dto)
        {
            var product =  _mapper.Map<Product>(dto);
            productMongoRepository.Edit(product);
        }

        public void Remove(Guid guid)
        {
            productMongoRepository.Remove(Guid.Parse(guid.ToString()));
        }
    }
}
