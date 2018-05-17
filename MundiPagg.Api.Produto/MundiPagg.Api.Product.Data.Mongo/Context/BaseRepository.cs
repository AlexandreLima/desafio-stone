using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;

namespace MundiPagg.Api.Products.Data.Mongo.Context
{
    public abstract class BaseRepository
    {
        protected IMongoDatabase db;

        public BaseRepository()
        {
            MongoClient client = new MongoClient("mongodb+srv://product:product@cluster0-hima1.mongodb.net/test?retryWrites=true");
            db = client.GetDatabase("ProductCatalog");
        }
    }
}
