using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Webshop.Data;
using Webshop.Models;

namespace Webshop.Pages.Webstore
{
    public class IndexModel : PageModel
    {
        private readonly AppDbContext database;

        public IndexModel(AppDbContext database)
        {
            this.database = database;
        }

        public List<Product> Products { get; set; }

        [BindProperty(SupportsGet = true)]
        public string SearchString { get; set; }

        public async Task OnGet()
        {
            var baits = from bait in database.Products
                        select bait;

            if (!string.IsNullOrEmpty(SearchString))
            {
                baits = baits.Where(b => b.Name.Contains(SearchString));
            }

            Products = await baits.ToListAsync();
        }

        public IActionResult OnGetReset()
        {
            SearchString = "";
            return Page();
        }
    }
}
