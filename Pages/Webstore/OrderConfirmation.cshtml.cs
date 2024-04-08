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

        public decimal Sum { get; set; }

        public void OnGet()
        {
            if (TempData.ContainsKey("TotalPrice") && TempData != null)
            {
                string? tempSum = TempData["TotalPrice"] as string;

                if (tempSum != null)
                {
                    Sum = decimal.Parse(tempSum);
                }
                else
                {
                    Sum = 0;
                }
            }
            else
            {
                Sum = 0;
            }
        }
    }
}
