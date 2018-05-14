using MongoDB.Bson.Serialization;
using System;
using System.Collections.Generic;
using System.Text;
using MundiPagg.Api.Products.Domain.Products;

namespace MundiPagg.Api.Product.Ioc
{
    public class MapMongoDatabase
    {
        public static void Map()
        {
            BsonClassMap.RegisterClassMap<Products.Domain.Products.Product> (cm =>
            {
                cm.AutoMap();
                cm.MapIdMember(c => c.ID);
            });
        }
    }
}
