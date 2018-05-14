using MongoDB.Bson;
using MundiPagg.Api.Products.Domain.Repository;
using MundiPagg.Api.Products.Domain.Products;
using System;
using System.Collections.Generic;
using System.Text;
using MundiPagg.Api.Products.Data.Mongo.Context;
using Microsoft.Extensions.Configuration;
using MundiPagg.Api.Products.Data.Mongo.Repository.Products.Contract;
using MongoDB.Driver;
using System.Linq;

namespace MundiPagg.Api.Products.Data.Mongo.Repository.Products
{
    public class ProductMongoRepository : BaseRepository, IProductMongoRepository
    {
        public ProductMongoRepository(IConfiguration configuration) 
            : base(configuration)
        {

        }
        public void Add(Domain.Products.Product entity)
        {
            this.db.GetCollection<Product>("Product").InsertOne(entity);
        }

        public void Edit(Domain.Products.Product entity)
        {
            this.db.GetCollection<Product>("Product")
                    .ReplaceOne(x => x.ID == entity.ID, entity);
        }

        public Product Get(ObjectId id)
        {
            return this.db.GetCollection<Product>("Product")
                    .Find(x => x.ID == id)
                    .FirstOrDefault();
        }

        public IEnumerable<Product> GetAll(int pagin, int paginRows)
        {
            return this.db.GetCollection<Product>("Product")
                            .AsQueryable()
                            .Skip(pagin)
                            .Take(paginRows)
                            .ToList();
         }

        public void Remove(ObjectId id)
        {
            this.db.GetCollection<Product>("Product").DeleteOne(x => x.ID == id);
        }
    }
}
