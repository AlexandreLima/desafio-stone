using MongoDB.Bson;
using MundiPagg.Api.Products.Domain.Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace MundiPagg.Api.Products.Data.Mongo.Repository.Products.Contract
{
    public interface IProductMongoRepository : IRepository<Domain.Products.Product, ObjectId>
    {

    }
}
