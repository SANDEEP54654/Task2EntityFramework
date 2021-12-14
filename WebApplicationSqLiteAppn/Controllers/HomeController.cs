using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using WebApplicationSqLiteAppn.Models;

namespace WebApplicationSqLiteAppn.Controllers
{
    public class HomeController: Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            List<Product> productList = null;
            using (var db = new ProductContext())
            {
                // Note: This sample requires the database to be created before running.
                Console.WriteLine($"Database path: {db.DbPath}.");

                // Get List
                productList = db.Product
                    .OrderBy(b => b.ProductId)
                    .ToList();


                // delete
                if (productList != null && productList.Count > 0)
                {
                    db.Product.RemoveRange(productList);
                    db.SaveChanges();
                }
               


                // Create
                Console.WriteLine("Inserting a new products");
                db.Add(new Product { ProductId = 1, name = "Laptop HP", price = "5000" });
                db.Add(new Product { ProductId = 2, name = "Laptop Dell", price = "4000" });
                db.Add(new Product { ProductId = 3, name = "Laptop Lenovo", price = "7000" });
                db.SaveChanges();

                // Read single
                Console.WriteLine("Querying for a blog");
                var productObj = db.Product
                    .OrderBy(b => b.ProductId)
                    .First();

                //// Update
                //Console.WriteLine("Updating the blog and adding a post");
                //blog.price = "6000";
                //db.SaveChanges();

                // Get List
                productList = db.Product
                    .OrderBy(b => b.ProductId)
                    .ToList();
            }

            return View(productList);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
