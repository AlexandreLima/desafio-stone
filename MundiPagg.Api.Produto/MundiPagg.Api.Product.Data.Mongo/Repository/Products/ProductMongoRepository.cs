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
using MundiPagg.Api.Products.Infrastructrure.Pagin;

namespace MundiPagg.Api.Products.Data.Mongo.Repository.Products
{
    public class ProductMongoRepository : BaseRepository, IProductMongoRepository
    {
        public ProductMongoRepository() 
        {

        }
        public void Add(Domain.Products.Product entity)
        {
            this.db.GetCollection<Product>("Product").InsertOne(entity);
        }

        public void Edit(Domain.Products.Product entity)
        {
            this.db.GetCollection<Product>("Product")
                    .ReplaceOne(x => x.Id == entity.Id, entity);
        }

        public Product Get(Guid id)
        {
            return this.db.GetCollection<Product>("Product")
                    .Find(x => x.Id == id)
                    .FirstOrDefault();
        }

        public IEnumerable<Product> GetAll(int pagin, int paginRows)
        {
            return this.db.GetCollection<Product>("Product")
                            .AsQueryable()
                            .Page(paginRows, pagin)
                            .ToList();
         }

        public void Remove(Guid id)
        {
            this.db.GetCollection<Product>("Product").DeleteOne(x => x.Id == id);
        }
    }
}
