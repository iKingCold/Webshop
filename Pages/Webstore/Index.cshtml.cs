using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Webshop.Data;
using Webshop.Models;

namespace Webshop.Pages.Webstore
{
    public class IndexModel : PageModel
    {
        private readonly AppDbContext database;
        private readonly AccessControl accessControl;

        public IndexModel(AppDbContext database, AccessControl accessControl)
        {
            this.database = database;
            this.accessControl = accessControl;
        }

        public List<Product> Products { get; set; }

        [BindProperty(SupportsGet = true)]
        public string? SearchString { get; set; }
        public SelectList? CategoryList { get; set; }

        [BindProperty(SupportsGet = true)]
        public int? SelectedCategoryId { get; set; }
        public int CurrentPage { get; set; }

        public Account_Product Account_Product { get; set; } = new Account_Product();

        public async Task OnGet(int pageId = 1)
        {
            CurrentPage = pageId;
            int pageSize = 10;
            //int pageNumber = page ?? 1;

            IQueryable<Category> categoryNameQuery = from c in database.Categories
                                                     orderby c.Name
                                                     select c;

            CategoryList = new SelectList(await categoryNameQuery.ToListAsync(), "Id", "Name");


            var product = from p in database.Products
                          select p;

            if (!string.IsNullOrEmpty(SearchString))
            {
                product = product.Where(b => b.Name.Contains(SearchString));
            }

            if (SelectedCategoryId != null)
            {
                product = product.Where(p => p.Category.Id == SelectedCategoryId);
            }

            int totalCount = await product.CountAsync();
            int totalPages = (int)Math.Ceiling((double)totalCount / pageSize);

            Products = await product.ToListAsync();
        }

        public async Task<IActionResult> OnPostFastPurchase(int? id)
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
                var existingProduct = await database.Account_Products.FirstOrDefaultAsync(ap => ap.Product.Id == product.Id && ap.Account.ID == accessControl.LoggedInAccountID);

                if (existingProduct != null)
                {
                    existingProduct.Quantity += 1;
                }
                else
                {
                    Account_Product.Account = await database.Accounts.Where(a => a.ID == accessControl.LoggedInAccountID).FirstAsync();
                    Account_Product.Product = product;
                    Account_Product.Quantity = 1;

                    database.Account_Products.Add(Account_Product);
                }

                await database.SaveChangesAsync();
            }

            return RedirectToPage("/Webstore/Cart");
        }

        public IActionResult OnGetReset()
        {
            return Page();
        }
    }
}
