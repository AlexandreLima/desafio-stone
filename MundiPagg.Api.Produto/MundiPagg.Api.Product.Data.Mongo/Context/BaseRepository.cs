using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;

namespace MundiPagg.Api.Product.Data.Mongo.Context
{
    public abstract class BaseRepository
    {
        private IConfiguration _configuration;
        protected IMongoDatabase db;

        public BaseRepository(IConfiguration config)
        {
            _configuration = config;

            MongoClient client = new MongoClient(
                _configuration.GetConnectionString("ConexaoCatalogo"));

            db = client.GetDatabase("DBCatalogo");
        }
    }
}
