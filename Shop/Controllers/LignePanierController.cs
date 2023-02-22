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
    public class LignePanierController : Controller
    {
        private readonly ApplicationDbContext _context;

        public LignePanierController(ApplicationDbContext context)
        {
            _context = context;
        }

        //Methode Index Ligne Panier Permet d'afficher les Produit dans Ligne Panier
        public IActionResult Index()
        {
            var ligne = (from l in _context.LignePaniers
                         join p in _context.Products on l.ProductID equals p.ProductID
                         select new LignePanier
                         {
                             LignePanierID = l.LignePanierID,
                             LigneQuantite = l.LigneQuantite,

                             ProductID = p.ProductID,
                             ProductName = p.ProductName,
                             ProductImg = p.ProductImg,
                             ProductPrice = p.ProductPrice * l.LigneQuantite,

                         });
            
            //var ligne = _context.LignePaniers.Include(c => c.Product);
            return View(ligne.ToList());
        }

        //Permet d'ajouter un produit dans ligne panier
        public IActionResult Add(int ID)
        {
            LignePanier lignePanier = new LignePanier();
            lignePanier.ProductID = ID;
            lignePanier.LigneQuantite = 1;
            _context.LignePaniers.Add(lignePanier);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        //Permet d'supprimer un produit dans ligne panier
        public IActionResult Delete(int? ID)
        {
            var result = _context.LignePaniers.Find(ID);
            if (result != null)
            {
                _context.LignePaniers.Remove(result);
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

