using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Webshop.Data;
using Webshop.Models;

namespace Webshop.Pages
{
    public class IndexModel : PageModel
    {
        private readonly AppDbContext database;

        public IndexModel(AppDbContext database)
        {
            this.database = database;
        }

        public List<Product> Products { get; set; }

        public async Task OnGetAsync()
        {
            Products = await GetRandomProductAsync(3);
        }

        public async Task<List<Product>> GetRandomProductAsync(int count)
        {
            int totalProducts = await database.Products.CountAsync();

            List<Product> randomProducts = new List<Product>();

            Random random = new Random();
            while (randomProducts.Count < count)
            {
                int offset = random.Next(0, totalProducts);

                var product = await database.Products.Skip(offset).FirstOrDefaultAsync();

                if (product != null && !randomProducts.Contains(product))
                {
                    randomProducts.Add(product);
                }
            }

            return randomProducts;
        }
    }
}