namespace Webshop.Models
{
    public class Account_Product
    {
        public int Id { get; set; }
        public Account Account { get; set; }
        public Product Product { get; set; }
        public int Quantity { get; set; }
    }
}
