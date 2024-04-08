using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Webshop.Data;
using Webshop.Models;

namespace Webshop.Pages.Webstore
{
    public class CartModel : PageModel
    {
        private readonly AppDbContext database;
        private readonly AccessControl accessControl;

        public CartModel(AppDbContext database, AccessControl accessControl)
        {
            this.database = database;
            this.accessControl = accessControl;
        }

        public List<Account_Product> Account_Products { get; set; }

        public void OnGet()
        {
            LoadAccountProducts();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            LoadAccountProducts();

            if (Account_Products != null)
            {
                foreach (var account in Account_Products)
                {
                    database.Account_Products.Remove(account);
                    await database.SaveChangesAsync();
                }
            }
            else
            {
                return NotFound();
            }
            return RedirectToPage("./OrderConfirmation");
        }

        public async Task<IActionResult> OnPostClearCartAsync()
        {
            LoadAccountProducts();

            if (Account_Products != null)
            {
                foreach (var cartItems in Account_Products)
                {
                    database.Account_Products.Remove(cartItems);
                    await database.SaveChangesAsync();
                }
            }
            return RedirectToPage("./Cart");
        }

        public void LoadAccountProducts()
        {
            Account_Products = database.Account_Products.Where(ap => ap.Account.ID == accessControl.LoggedInAccountID).Include(ap => ap.Product).ToList();
        }
    }
}
