using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplicationSqLiteAppn.Models
{
    public class ProductContext: DbContext
    {
        public DbSet<Product> Product { get; set; }
        public string DbPath { get; }

        public ProductContext()
        {
            var folder = Environment.SpecialFolder.LocalApplicationData;
            var path = Environment.GetFolderPath(folder);
            DbPath = System.IO.Path.Join(path, "Product.db");
        }

        // The following configures EF to create a Sqlite database file in the
        // special "local" folder for your platform.
        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlite($"Data Source={DbPath}");
    }

    public class Product
    {
        [Key]
        public int ProductId { get; set; }
        [Required()]
        public string name { get; set; }
        [Required()]
        public string price { get; set; }
    }
}
