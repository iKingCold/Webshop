using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net.Http.Headers;
using Webshop.Data;
using Webshop.Models;

namespace Webshop.Controllers
{
    [Route("/api")]
    [ApiController]
    [AllowAnonymous]
    public class APIController : ControllerBase
    {
        private readonly AppDbContext database;

        public APIController(AppDbContext database)
        {
            this.database = database;
        }

        [HttpGet]
        public List<Product> Products(string? productName, string? category, int page = 1)
        {
            List<Product> products = database.Products.Include(p => p.Category).ToList();

            // Filter on name
            if (productName != null)
            {
                products = products.Where(p => p.Name.ToLower().Contains(productName)).ToList();
            }

            // Filter on category
            if (category != null)
            {
                products = products.Where(p => p.Category.Name.ToLower() == category).ToList();
            }

            // Pagination
            products = products.Skip((page - 1) * 10).Take(10).ToList();


            // Get image absolute url
            var absoluteUrl = $"{Request.Scheme}://{Request.Host}";

            foreach (var product in products)
            {
                product.Image = $"{absoluteUrl}/Images/{product.Image}";
            }

            return products;
        }
        
    }
}
