using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Shop.Models
{
    public class Panier
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "Panier ID")]
        public int PanierID { get; set; }

        [Display(Name = "SubTotal")]
        [Column(TypeName = "decimal(12,2)")]
        public decimal Total { get; set; }

        [ForeignKey("KignePanier")]
        public int LignePanierID { get; set; }

        [Display(Name = "Quantite")]
        [Column(TypeName = "int")]
        [NotMapped]
        public int LigneQuantite { get; set; }

        [Display(Name = "Product Name")]
        [Column(TypeName = "varchar(30)")]
        [Required]
        [NotMapped]
        public string ProductName { get; set; } = string.Empty;

        [Display(Name = "Product Image")]
        [Column(TypeName = "varchar(300)")]
        [NotMapped]
        public string? ProductImg { get; set; }

        [Display(Name = "Product price")]
        [Column(TypeName = "decimal(12,2)")]
        [NotMapped]
        public decimal ProductPrice { get; set; }

        public virtual LignePanier LignePanier { get; set; }
    }
}

