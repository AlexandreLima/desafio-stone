using AutoMapper;
using MundiPagg.Api.Products.Domain.Departaments;
using MundiPagg.Api.Products.Domain.Products;
using MundPagg.Api.Product.Dto.Products;
using System;
using System.Collections.Generic;
using System.Text;

namespace MundiPagg.Api.Products.Infrastructrure.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Product, ProductDto>();
            CreateMap<AddProductDto, Product>();
            CreateMap<EditProductDto, Product>();
            CreateMap<DepartamentDto, Departament>();
            CreateMap<Departament, DepartamentDto>();
        }
    }
}
