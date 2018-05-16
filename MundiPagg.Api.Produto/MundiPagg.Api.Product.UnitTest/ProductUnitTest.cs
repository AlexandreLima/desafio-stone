using AutoMapper;
using Moq;
using MundiPagg.Api.Products.Application.Products.Implementation;
using MundiPagg.Api.Products.Data.Mongo.Repository.Products.Contract;
using MundiPagg.Api.Products.Domain.Products;
using MundiPagg.Api.Products.Domain.Repository;
using MundiPagg.Api.Products.UnitTest.Builder;
using MundPagg.Api.Product.Dto.Products;
using System;
using Xunit;
using FluentAssertions;
using MongoDB.Bson;
using System.Collections.Generic;
using System.Linq;
using MundiPagg.Api.Products.Infrastructrure.Mapping;
using MundiPagg.Api.Products.Infrastructrure.Pagin;

namespace MundiPagg.Api.Products.UnitTest
{
    public class ProductUnitTest
    {
        private readonly Mock<IProductMongoRepository> repositorioMock;
        private readonly Mock<IMapper> mapper;
        private readonly ProductApplication productApplication;

        public ProductUnitTest()
        {
            this.repositorioMock = new Mock<IProductMongoRepository>();
            mapper = new Mock<IMapper>();
            productApplication = new ProductApplication(repositorioMock.Object, mapper.Object);
        }

        [Fact]
        public void InsertProduct()
        {
            var produto = ProductBuilder.Create.Instance();

            repositorioMock.Setup(x => x.Add(It.IsAny<Product>())).Verifiable();

            mapper.Setup(x => x.Map<Product>(It.IsAny<AddProductDto>()))
                .Returns(() => produto);

            productApplication.Add(new AddProductDto());

        }

        [Fact]
        public void UpdateProduct()
        {
            var produto = ProductBuilder.Create.Instance();

            repositorioMock.Setup(x => x.Edit(It.IsAny<Product>())).Verifiable();

            mapper.Setup(x => x.Map<Product>(It.IsAny<EditProductDto>()))
                .Returns(() => produto);

            productApplication.Edit(new EditProductDto());

        }

        [Fact]
        public void RemoveProduct()
        {
            repositorioMock.Setup(x => x.Remove(It.IsAny<Guid>())).Verifiable();

            productApplication.Remove(Guid.NewGuid());

        }

        [Fact]
        public void GetProduct()
        {
            var produto = ProductBuilder
                            .Create
                            .WithCode("testCode")
                            .WithName("testName")
                            .WithDepartment("testDepartament")
                            .Instance();

            repositorioMock.Setup(x => x.Get(It.IsAny<Guid>())).Returns(() => produto);
            Guid guid = Guid.NewGuid();

            mapper.Setup(x => x.Map<ProductDto>(It.IsAny<Product>()))
                .Returns(() => 
                            new ProductDto
                            {
                                Id = guid,
                                Code = "testCode",
                                Name = "testName",
                                Departament =  new DepartamentDto { Name = "testDepartament" }
                            });

            var productDto = productApplication.Detail(guid);

            productDto.Should().NotBeNull();
            productDto.Should().BeOfType<ProductDto>();
            productDto.Name.Should().Be(produto.Name);
            productDto.Code.Should().Be(produto.Code);
            productDto.Id.Should().Be(guid);
            productDto.Departament.Name.Should().Be(produto.Departament.Name);

        }

        [Fact]
        public void GetProductsPagin()
        {
            
            var produtos = new List<Product>();

            for (int i = 0; i < 25; i++)
            {
                var produto = ProductBuilder
                            .Create
                            .WithCode($"testCode{i}")
                            .WithName($"testName{i}")
                            .WithDepartment($"testDepartament{i}")
                            .Instance();

                produtos.Add(produto);
            }

            var pagin = 2;
            var paginItems = 5;

            var produtosQueryable = produtos.Page(paginItems, pagin);

            repositorioMock.Setup(x => x.GetAll(It.IsAny<int>(), It.IsAny<int>())).Returns(() => produtosQueryable);
            Guid guid = Guid.NewGuid();

            Mapper.Initialize(x => x.AddProfile(new MappingProfile()));

            mapper.Setup(x => x.Map<IEnumerable<ProductDto>>(It.IsAny<IEnumerable<Product>>()))
                .Returns(() =>
                           Mapper.Map(produtosQueryable, produtosQueryable.GetType(), typeof(IEnumerable<ProductDto>)) as IEnumerable<ProductDto>);

            
            var productsDto = productApplication.All(paginItems, pagin);

            productsDto.Should().NotBeNull();
            productsDto.Should().BeOfType<List<ProductDto>>();
            productsDto.Count().Should().Be(5);

        }
    }
}
