using Crud.Models;
using Crud.Services;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;
using System.Data.Common;
using System.Reflection.Metadata.Ecma335;

namespace Crud.Controllers
{
    public class ProductsController : Controller
    {
        private readonly ApplicationDbcontext context;

        // call the applicationdbcontext from the service file and call as context
        public ProductsController(ApplicationDbcontext context)
        {
            this.context = context;
        }
        public IActionResult Index()
        {
            //to read the data 
            var products = context.Products.OrderByDescending(p=>p.Id).ToList();
            return View(products);
        }
        public IActionResult Create() 
        {
            return View();
                }
        //to create the data
        [HttpPost]
        public IActionResult Create(ProductCreatecs productCreatecs)
        {
            if (!ModelState.IsValid)
            {
                return View(productCreatecs);
            }
            //save the new data into the database
            Product product = new Product()
            {
                Name = productCreatecs.Name,
                Brand = productCreatecs.Brand,
                Category = productCreatecs.Category,
                Price = productCreatecs.Price,
                Description = productCreatecs.Description,
                CreatedAt = DateTime.Now
            };

            context.Products.Add(product);
            context.SaveChanges();



            return RedirectToAction("Index","Products");
        }
        //to work on the edit page. getting the content
        public IActionResult Edit(int id)
        {
            var product = context.Products.Find(id);
            if (product == null)
            {
                return RedirectToAction("Index", "Products");

            }
            //create ProductCreatecs from product
            var productCreatecs = new ProductCreatecs()
            {
                Name = product.Name,
                Brand = product.Brand,
                Category = product.Category,
                Price = product.Price,
                Description = product.Description,

            };

            ViewData["ProductId"] = product.Id;
            ViewData["CreatedAt"] = product.CreatedAt.ToString("MM/dd/yyyy");
            return View(productCreatecs);
        }
        [HttpPost]
        public IActionResult Edit(int id, ProductCreatecs productCreatecs)
        {
            var product = context.Products.Find(id);
            if (product == null)
            {
                return RedirectToAction("Index", "Products");
            }
            if (!ModelState.IsValid)
            {
                ViewData["ProductId"] = product.Id;
                ViewData["CreatedAt"] = product.CreatedAt.ToString("MM/dd/yyyy");
                return View(productCreatecs);



            }
            //update the product in the database
            product.Name = productCreatecs.Name;
            product.Brand = productCreatecs.Brand;
            product.Description = productCreatecs.Description;
            product.Price = productCreatecs.Price;
            product.Description = productCreatecs.Description;
            context.SaveChanges();
            return RedirectToAction("Index", "Products");
        }
        
        public IActionResult Delete(int id)
        {
            var product = context.Products.Find(id);
            if (product == null)
            {
                return RedirectToAction("Index", "Products");
            }
            context.Products.Remove(product);
            context.SaveChanges(true);
            return RedirectToAction("Index", "Products");
        }
    }
}
