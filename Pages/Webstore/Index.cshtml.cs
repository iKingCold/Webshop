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

        public IndexModel(AppDbContext database)
        {
            this.database = database;
        }

        public List<Product> Products { get; set; }

        [BindProperty(SupportsGet = true)]
        public string? SearchString { get; set; }
        public SelectList? CategoryList { get; set; }

        [BindProperty(SupportsGet = true)]
        public int? SelectedCategoryId { get; set; }


        public async Task OnGet(int? page)
        {
            int pageSize = 10;
            int pageNumber = page ?? 1;

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

        public IActionResult OnGetReset()
        {
            return Page();
        }
    }
}
