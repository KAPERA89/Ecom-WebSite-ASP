using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Shop.Models
{
    [Table("Product", Schema = "Project")]
    public class Product
	{
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "Product ID")]
        public int ProductID { get; set; }

        [Display(Name = "Product Name")]
        [Column(TypeName = "varchar(30)")]
        [Required]

        public string ProductName { get; set; } = string.Empty;

        [Display(Name = "Product Image")]
        [Column(TypeName = "varchar(300)")]
        public string? ProductImg { get; set; }

        [Display(Name = "Product price")]
        [Column(TypeName = "decimal(12,2)")]
        public decimal ProductPrice { get; set; }

        [Display(Name = "Product Description")]
        [Column(TypeName = "varchar(1000)")]
        public string? ProductDescription { get; set; }

        [ForeignKey("Categorie")]
        public int CategorieID { get; set; }

        [Display(Name = "Categorie")]
        [NotMapped]
        public string CategorieName { get; set; }

        public virtual Categorie Categorie { get; set; }

        //public virtual ICollection<LignePanier> LignePaniers { get; set; }
    }
}

