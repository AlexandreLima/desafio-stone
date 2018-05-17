using AutoMapper;
using MundiPagg.Api.Products.Infrastructrure.Mapping;
using System;
using System.Collections.Generic;
using System.Text;

namespace MundiPagg.Api.Products.UnitTest.Utils
{
    public class MapperContext
    {
        private static IMapper mapper;

        public static IMapper GetMapper()
        {
            Mapper.Reset();

            try
            {
                Mapper.Initialize(x => x.AddProfile(new MappingProfile()));
            }
            catch 
            {
            }
           
            mapper = Mapper.Configuration.CreateMapper();
            return mapper;
        }
    }
}
