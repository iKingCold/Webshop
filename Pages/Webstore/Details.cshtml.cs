using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Webshop.Data;
using Webshop.Models;

namespace Webshop.Pages.Webstore
{
    public class DetailsModel : PageModel
    {
        private readonly AppDbContext database;

        public DetailsModel(AppDbContext database)
        {
            this.database = database;
        }

        public Product Product { get; set; }
        public int Quantity { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await database.Products.FindAsync(id);

            if (product == null)
            {
                return NotFound();
            }
            else
            {
                Product = product;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id, int quantity)
        {
            if (id == null || quantity <= 0)
            {
                return NotFound();
            }

            var product = await database.Products.FindAsync(id);

            if (product == null)
            {
                return NotFound();
            }
            else
            {
                Product = product;
                Quantity = quantity;
            }

            //Lägg till i Account_Product

            return Page();
        }
    }
}
