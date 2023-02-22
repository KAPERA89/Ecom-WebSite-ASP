using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Shop.Models
{
    public class LignePanier
	{
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "Ligne ID")]
        public int LignePanierID { get; set; }

        [Display(Name = "Quantite")]
        [Column(TypeName = "int")]
        public int LigneQuantite { get; set; }

        /*
         public decimal Price {get; set;}
         */

        [ForeignKey("Product")]
        public int ProductID { get; set; }

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

        public virtual Product Product { get; set; }

        
    }
}

