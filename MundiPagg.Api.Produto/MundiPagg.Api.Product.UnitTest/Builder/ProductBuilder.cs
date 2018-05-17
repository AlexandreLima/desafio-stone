using MundiPagg.Api.Products.Domain.Products;
using MundPagg.Api.Product.Dto.Products;
using System;
using System.Collections.Generic;
using System.Text;

namespace MundiPagg.Api.Products.UnitTest.Builder
{
    public class ProductBuilder
    {
        private string name;
        private string code;
        private string departmentName;
        private Guid id;

        public static ProductBuilder Create
            => new ProductBuilder();

        public ProductBuilder WithName(string name)
        {
            this.name = name;
            return this;
        }

        public ProductBuilder WithCode(string code)
        {
            this.code = code;
            return this;
        }

        public ProductBuilder WithDepartment(string departmentName)
        {
            this.departmentName = departmentName;
            return this;
        }

        public ProductBuilder WithId(Guid id)
        {
            this.id = id;
            return this;
        }

        public Product Instance() 
            => new Product(this.name, this.code, this.departmentName);

        public AddProductDto ToAddProductDto()
        {
            return new AddProductDto
            {
                Code = this.code,
                Departament = new Dto.Products.DepartamentDto { Name = departmentName },
                Name = this.name
            };
        }

        public EditProductDto ToEditProductDto()
        {
            return new EditProductDto
            {
                Id = id,
                Code = this.code,
                Departament = new Dto.Products.DepartamentDto { Name = departmentName },
                Name = this.name
            };
        }

    }
}
