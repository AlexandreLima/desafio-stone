using MongoDB.Bson;
using MundiPagg.Api.Product.Domain.Repository;
using MundiPagg.Api.Product.Domain.Products;
using System;
using System.Collections.Generic;
using System.Text;
using MundiPagg.Api.Product.Data.Mongo.Context;
using Microsoft.Extensions.Configuration;

namespace MundiPagg.Api.Product.Data.Mongo.Repository.Products
{
    public class ProductMongoRepository : BaseRepository, IRepository<Domain.Products.Product, ObjectId>
    {
        public ProductMongoRepository(IConfiguration configuration) 
            : base(configuration)
        {

        }
        public void Add(Domain.Products.Product entity)
        {
            throw new NotImplementedException();
        }

        public void Edit(Domain.Products.Product entity)
        {
            throw new NotImplementedException();
        }

        public void Get(ObjectId id)
        {
            throw new NotImplementedException();
        }

        public void GetAll(int pagin, int paginRows)
        {
            throw new NotImplementedException();
        }

        public void Remove(ObjectId id)
        {
            throw new NotImplementedException();
        }
    }
}
