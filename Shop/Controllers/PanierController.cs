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
    public class PanierController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PanierController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: /<controller>/
        public IActionResult Index()
        {
            var panier = (from p in _context.Paniers
                          join l in _context.LignePaniers on p.LignePanierID equals l.LignePanierID
                          join pr in _context.Products on l.ProductID equals pr.ProductID
                          select new Panier
                          {
                              PanierID = p.PanierID,
                              Total = l.ProductPrice,
                              LigneQuantite = l.LigneQuantite,
                              LignePanierID = p.LignePanierID,
                              ProductName = pr.ProductName,
                              ProductImg = pr.ProductImg,
                              ProductPrice = pr.ProductPrice * l.LigneQuantite,

                          }) ;


            return View(panier.ToList());
        }

        //Permet d'ajouter un lignePanier dans panier
        public IActionResult Add(int ID)
        {
            Panier panier = new Panier();
            panier.LignePanierID = ID;
            _context.Paniers.Add(panier);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        //Permet d'supprimer un LignePanier depuis panier
        public IActionResult Delete(int? ID)
        {
            var result = _context.Paniers.Find(ID);
            if (result != null)
            {
                _context.Paniers.Remove(result);
                _context.SaveChanges();
            }
            return RedirectToAction(nameof(Index));
        }

        //Permet d'augmenter la quantite de produit dans ligne panier
        public async Task<IActionResult> Plus(int id)
        {
            var l = await _context.LignePaniers.FirstOrDefaultAsync(li => li.LignePanierID == id);
            l.LigneQuantite += 1;
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        //Permet de diminuer la quantite de produit dans ligne panier
        public async Task<IActionResult> minus(int id)
        {
            var l = await _context.LignePaniers.FirstOrDefaultAsync(li => li.LignePanierID == id);
            if (l.LigneQuantite > 0)
            {
                l.LigneQuantite -= 1;
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}

