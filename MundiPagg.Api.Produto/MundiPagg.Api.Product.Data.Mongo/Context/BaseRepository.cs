using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;

namespace MundiPagg.Api.Products.Data.Mongo.Context
{
    public abstract class BaseRepository
    {
        private IConfiguration _configuration;
        protected IMongoDatabase db;

        public BaseRepository(IConfiguration config)
        {
            _configuration = config;

            MongoClient client = new MongoClient("mongodb+srv://product:product@cluster0-hima1.mongodb.net/test?retryWrites=true");
                //_configuration.GetConnectionString("ConexaoCatalogo"));

            db = client.GetDatabase("ProductCatalog");
        }
    }
}
