using System;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Shop.Models
{
	public class Categorie
	{
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "Categorie ID")]
        public int CategorieID { get; set; }

        [Display(Name = "Categorie Name")]
        [Column(TypeName = "varchar(30)")]
        public string CategorieName { get; set; } = string.Empty;
    }
}

