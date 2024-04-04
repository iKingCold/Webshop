using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Webshop.Data;
using Webshop.Models;

namespace Webshop.Pages.Webstore
{
    public class DetailsModel : PageModel
    {
        private readonly AppDbContext database;
        private readonly AccessControl accessControl;

        public DetailsModel(AppDbContext database, AccessControl accessControl)
        {
            this.database = database;
            this.accessControl = accessControl;
        }

        public Account_Product Account_Product { get; set; } = new Account_Product();
        public Product Product { get; set; }
        public Account Account { get; set; }
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
                Account_Product.Account = await database.Accounts.Where(a => a.ID == accessControl.LoggedInAccountID).FirstAsync();
                Account_Product.Product = product;
                Account_Product.Quantity = quantity;
            }

            database.Account_Products.Add(Account_Product);
            await database.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
