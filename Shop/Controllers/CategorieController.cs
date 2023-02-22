using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Shop.Data;
using Shop.Models;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Shop.Controllers
{
    public class CategorieController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CategorieController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: /<controller>/
        public IActionResult Index()
        {
            List<Categorie> cat = _context.Categories.ToList();
            return View(cat);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Categorie model)
        {
            ModelState.Remove("CategorieID");
            if (ModelState.IsValid)
            {
                _context.Categories.Add(model);
                _context.SaveChanges();

                return RedirectToAction("Index");
            }

            return View();
        }

        public IActionResult Edit(int ID)
        {
            Categorie data = this._context.Categories.Where(c => c.CategorieID == ID).FirstOrDefault();
            return View("Create", data);
        }

        [HttpPost]
        public IActionResult Edit(Categorie model)
        {
            ModelState.Remove("CategorieID");
            if (ModelState.IsValid)
            {
                _context.Categories.Update(model);
                _context.SaveChanges();

                return RedirectToAction("Index");
            }

            return View("Create", model);
        }

        public IActionResult Delete(int ID)
        {
            Categorie cat = this._context.Categories.Where(c => c.CategorieID == ID).FirstOrDefault();
            if (cat != null)
            {
                _context.Categories.Remove(cat);
                _context.SaveChanges();
            }
            return RedirectToAction("Index");
        }
    }
}

