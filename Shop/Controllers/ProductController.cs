using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Shop.Data;
using Shop.Models;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Shop.Controllers
{
    public class ProductController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ProductController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: /<controller>/
        public IActionResult Index(string searchItem, string sortItem, string sortDate, int? i)
        {
            ViewBag.NameSort = String.IsNullOrEmpty(sortItem) ? "name_desc" : "";
            ViewBag.PriceSort = String.IsNullOrEmpty(sortItem) ? "price_desc" : "";
            ViewBag.IdSort = String.IsNullOrEmpty(sortItem) ? "Id_desc" : "";
            ViewBag.Categories = this._context.Categories.ToList();


            var product = (from prod in _context.Products
                           join cat in _context.Categories on prod.CategorieID equals cat.CategorieID
                           select new Product
                           {
                               ProductID = prod.ProductID,
                               ProductName = prod.ProductName,
                               ProductImg = prod.ProductImg,
                               ProductPrice = prod.ProductPrice,
                               ProductDescription = prod.ProductDescription,
                               CategorieID = prod.CategorieID,
                               CategorieName = cat.CategorieName
                           }).ToList();


            if (!String.IsNullOrEmpty(searchItem))
            {
                product = product.Where(s => s.ProductName.ToUpper().Contains(searchItem.ToUpper())
                         || s.ProductID.ToString().Contains(searchItem)).ToList();
            }

            switch (sortItem)
            {
                case "name_desc":
                    product = product.OrderByDescending(s => s.ProductName).ToList();
                    break;
                case "price_desc":
                    product = product.OrderByDescending(s => s.ProductPrice).ToList();
                    break;
                case "Id_desc":
                    product = product.OrderByDescending(s => s.ProductID).ToList();
                    break;
            }

            return View(product);
        }

        public IActionResult Afficher()
        {
            var prod = _context.Products.Include(c => c.Categorie);
            return View(prod.ToList());
        }

        public IActionResult Create()
        {
            ViewBag.Categories = this._context.Categories.ToList();
            return View();
        }

        [HttpPost]
        public IActionResult Create(Product model)
        {
            ModelState.Remove("ProductID");
            ModelState.Remove("Categorie");
            ModelState.Remove("CategorieName");
            ModelState.Remove("LignePaniers");
            UploadImage(model);
            if (ModelState.IsValid)
            {
                _context.Products.Add(model);
                _context.SaveChanges();

                return RedirectToAction(nameof(Index));
            }
            ViewBag.Categories = this._context.Categories.ToList();
            return View();
        }

        private void UploadImage(Product model)
        {
            var file = HttpContext.Request.Form.Files;
            if (file.Count() > 0)
            {
                //Upload Image
                string ImageName = Guid.NewGuid().ToString() + Path.GetExtension(file[0].FileName);
                var fileStream = new FileStream(Path.Combine(@"wwwroot/", "images", ImageName), FileMode.Create);
                file[0].CopyTo(fileStream);
                model.ProductImg = ImageName;
            }
            else if (model.ProductImg == null && model.ProductID == null)
            {
                //Not Uploade Image and new Product
                model.ProductImg = "DefaultImg.jpg";
            }
            else
            {
                //Edit
                model.ProductImg = model.ProductImg;
            }
        }

        public IActionResult Edit(int ID)
        {
            Product data = this._context.Products.Where(c => c.ProductID == ID).FirstOrDefault();
            ViewBag.Categories = this._context.Categories.ToList();
            return View("Create", data);
        }

        [HttpPost]
        public IActionResult Edit(Product model)
        {
            ModelState.Remove("ProductID");
            ModelState.Remove("Categorie");
            ModelState.Remove("CategorieName");
            ModelState.Remove("LignePaniers");

            UploadImage(model);
            if (ModelState.IsValid)
            {
                _context.Products.Update(model);
                _context.SaveChanges();

                return RedirectToAction("Index");
            }
            ViewBag.Categories = this._context.Categories.ToList();
            return View("Create", model);
        }

        public IActionResult Delete(int? ID)
        {
            var result = _context.Products.Find(ID);
            if (result != null)
            {
                _context.Products.Remove(result);
                _context.SaveChanges();
            }
            return RedirectToAction(nameof(Index));
        }

        //[HttpPost]
        public IActionResult Details(int ID)
        {
            Product result = _context.Products.Find(ID);
            ViewBag.ID = ID;
            return View(result);
        }
    }
}

