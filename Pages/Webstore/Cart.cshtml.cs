using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Webshop.Data;

namespace Webshop.Pages.Webstore
{
    public class CartModel : PageModel
    {
        private readonly AppDbContext database;

        public CartModel(AppDbContext database)
        {
            this.database = database;
        }

        public void OnGet()
        {
        }
    }
}
