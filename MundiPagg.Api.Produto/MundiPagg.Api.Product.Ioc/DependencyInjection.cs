using Microsoft.Extensions.DependencyInjection;
using MundiPagg.Api.Products.Data.Mongo.Repository.Products;
using MundiPagg.Api.Products.Domain.Repository;
using System;
using Xunit;
using MundiPagg.Api.Products.Domain.Products;
using MongoDB.Bson;
using MundiPagg.Api.Products.Application.Products.Contracts;
using MundiPagg.Api.Products.Application.Products.Implementation;
using MundiPagg.Api.Products.Data.Mongo.Repository.Products.Contract;

namespace MundiPagg.Api.Products.Ioc
{
    public static class DependencyInjection
    {
        public static void AddDepedencyInjection(this IServiceCollection services)
        {
            services.AddScoped<IProductMongoRepository, ProductMongoRepository>();
            services.AddScoped<IProductApplication, ProductApplication>();
        }
    }
}
