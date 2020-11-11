using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CRM.Persistence.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CRMServer.Controllers
{
    //ToDo - Move DB related logic to Persistence component

    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly ProductContext _productContext;

        public ProductsController(ProductContext prodContext)
        {
            _productContext = prodContext;
        }

        [HttpGet]
        public Task<IList<Product>> GetProducts()
        {
            return Task.Run(() => _productContext.Product.Select(x => x).ToList() as IList<Product>);
        }

        [HttpGet("getSingleProduct")]
        public Task<Product> GetProduct(int prodId)
        {
            return Task.Run(() => _productContext.Product.First(x => x.Id == prodId));
        }

        [HttpPost("add")]
        public async Task<bool> AddProduct([FromBody] Product prod)
        {
            await Task.Run(() =>
            {
                var res = _productContext.Product.Add(prod);
                if (res.State == Microsoft.EntityFrameworkCore.EntityState.Added)
                {
                    _productContext.SaveChanges();
                }
            }).ConfigureAwait(false);

            return true;
        }

        [HttpDelete]
        public async Task<bool> DeleteProduct(int prodId)
        {
            var prodToRemove = await Task.Run(() => _productContext.Product.First(x => x.Id == prodId)).ConfigureAwait(false);
            await Task.Run(() =>
            {
                var res = _productContext.Product.Remove(prodToRemove);
                if (res.State == Microsoft.EntityFrameworkCore.EntityState.Deleted)
                {
                    _productContext.SaveChanges();
                }
            }
            ).ConfigureAwait(false);

            return true;
        }

        //ToDo - Add missing REST methods (PUT, PATCH)
    }
}
