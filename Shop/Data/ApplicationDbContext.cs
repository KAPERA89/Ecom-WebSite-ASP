using System;
using Microsoft.EntityFrameworkCore;
using Shop.Models;

namespace Shop.Data
{
	public class ApplicationDbContext : DbContext
	{
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }

        public DbSet<Categorie> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<LignePanier> LignePaniers { get; set; }
        public DbSet<Panier> Paniers { get; set; }
    }
}

