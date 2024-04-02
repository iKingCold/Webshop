using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using Webshop.Models;

namespace Webshop.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<Account> Accounts { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }
    }
}
