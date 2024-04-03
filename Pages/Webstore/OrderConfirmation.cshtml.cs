using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Webshop.Data;

namespace Webshop.Pages.Webstore
{
    public class OrderConfirmationModel : PageModel
    {
        private readonly AppDbContext database;

        public OrderConfirmationModel(AppDbContext database)
        {
            this.database = database;
        }

        public void OnGet()
        {
        }
    }
}
