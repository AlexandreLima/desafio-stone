using AutoMapper;
using MundiPagg.Api.Products.Application.Products.Implementation;
using MundiPagg.Api.Products.Data.Mongo.Repository.Products;
using MundiPagg.Api.Products.Infrastructrure.Mapping;
using MundiPagg.Api.Products.UnitTest.Builder;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using Xunit;
using FluentAssertions;
using MundiPagg.Api.Products.Api;
using MundPagg.Api.Product.Dto.Products;
using MundiPagg.Api.Products.Domain.Products;
using MundiPagg.Api.Products.Infrastructrure.Exception;
using MundiPagg.Api.Products.UnitTest.Utils;

namespace MundiPagg.Api.Products.UnitTest
{
    public class ProductApiTest
    {
        private readonly ProductMongoRepository repositorio;
        private readonly IMapper mapper;
        private readonly ProductApplication productApplication;
        private readonly ProductController productController;

        public ProductApiTest()
        {
            this.repositorio = new ProductMongoRepository();
            mapper = MapperContext.GetMapper();

            productApplication = new ProductApplication(repositorio, mapper);
            productController = new ProductController(productApplication);
        }

        [Fact]
        public void InsertProduct()
        {
            var produtoBuilder = ProductBuilder.Create
                            .WithCode("testCode")
                            .WithName("testName")
                            .WithDepartment("testDepartament");

            var statusCodeResult = productController.Post(produtoBuilder.ToAddProductDto());

            statusCodeResult.StatusCode.Should().Be(201);

        }

        [Fact]
        public void UpdateProduct()
        {
            var produtoBuilder = ProductBuilder.Create
                                    .WithCode("testCode")
                                    .WithName("testName")
                                    .WithDepartment("testDepartament");

            var product = produtoBuilder.Instance();
            repositorio.Add(product);

            produtoBuilder
                .WithId(product.Id)
                .WithCode("testCode new")
                .WithName("testName new")
                .WithDepartment("testDepartament new");

            var statusCodeResult = productController.Put(product.Id, produtoBuilder.ToEditProductDto());
            statusCodeResult.StatusCode.Should().Be(204);

            var newProduct = repositorio.Get(product.Id);
            newProduct.Should().NotBeNull();
            newProduct.Name.Should().Be("testName new");
            newProduct.Code.Should().Be("testCode new");

        }

        [Fact]
        public void RemoveProduct()
        {
            var produtoBuilder = ProductBuilder.Create
                                    .WithCode("testCode")
                                    .WithName("testName")
                                    .WithDepartment("testDepartament");

            var product = produtoBuilder.Instance();
            repositorio.Add(product);

            var statusCodeResult = productController.Delete(product.Id);

            statusCodeResult.StatusCode.Should().Be(204);

            var newProduct = repositorio.Get(product.Id);
            newProduct.Should().BeNull();

        }

        [Fact]
        public void GetProduct()
        {
            var product = ProductBuilder
                            .Create
                            .WithCode("testCode")
                            .WithName("testName")
                            .WithDepartment("testDepartament")
                            .Instance();

            repositorio.Add(product);

            var productDto = productController.Get(product.Id);

            productDto.Should().NotBeNull();
            productDto.Should().BeOfType<ProductDto>();
            productDto.Name.Should().Be(product.Name);
            productDto.Code.Should().Be(product.Code);
            productDto.Id.Should().Be(product.Id);
            productDto.Departament.Name.Should().Be(product.Departament.Name);

        }

        [Fact]
        public void GetProductsPagin()
        {
            var produtos = new List<Product>();

            for (int i = 0; i < 25; i++)
            {
                var product = ProductBuilder
                            .Create
                            .WithCode($"testCode{i}")
                            .WithName($"testName{i}")
                            .WithDepartment($"testDepartament{i}")
                            .Instance();

                repositorio.Add(product);
            }

            var pagin = 2;
            var paginItems = 5;

            var productsDto = productController.Get(pagin, paginItems);

            productsDto.Should().NotBeNull();
            productsDto.Should().BeOfType<List<ProductDto>>();
            productsDto.Count().Should().Be(5);

        }

        [Fact]
        public void DeveRetornarDomainExceptionComProdutoVazio()
        {
            var produto = ProductBuilder.Create.Instance();

            Assert.Throws<DomainException>(() => productController.Post(new AddProductDto()));
            Assert.Throws<DomainException>(() => productController.Put(Guid.NewGuid(), new EditProductDto()));
        }
    }
}
