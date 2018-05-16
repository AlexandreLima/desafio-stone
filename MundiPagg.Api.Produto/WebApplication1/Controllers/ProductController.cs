using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MundiPagg.Api.Products.Application.Products.Contracts;
using MundPagg.Api.Product.Dto.Products;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    public class ProductController : Controller
    {
        private readonly IProductApplication productApplication;
        public ProductController(IProductApplication productApplication) =>
            this.productApplication = productApplication;

        // GET api/values
        [HttpGet("pagin/{pagin}/itemsPerPagin/{itemsPerPagin}")]
        public IEnumerable<ProductDto> Get(int pagin, int itemsPerPagin)
        {
            return productApplication.All(itemsPerPagin, pagin);
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ProductDto Get(Guid id)
        {
            return productApplication.Detail(id);
        }

        // POST api/values
        [HttpPost]
        public StatusCodeResult Post([FromBody]AddProductDto productDto)
        {
            productApplication.Add(productDto);
            return new StatusCodeResult(201);
        }
            

        // PUT api/values/5
        [HttpPut("{id}")]
        public StatusCodeResult Put(Guid id, [FromBody]EditProductDto productDto)
        {
            productApplication.Edit(productDto);
            return NoContent();
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public StatusCodeResult Delete(Guid id)
        {
            productApplication.Remove(id);
            return NoContent();
        }
    }
}
