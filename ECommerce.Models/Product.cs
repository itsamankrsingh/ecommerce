using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace ECommerce.Models
{
    public partial class Product
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        public string? Description { get; set; }
        [Required]
        public string ISBN { get; set; }
        [Required]
        public string Author { get; set; }
        [Required]
        [Display(Name ="List Price")]
        [Range(1, 10000, ErrorMessage = "The List Price must be between 1-10000.")]
        public double ListPrice{ get; set; }
        [Required]
        [Display(Name = "Price for 1-50")]
        [Range(1, 10000, ErrorMessage = "The List Price must be between 1-10000.")]
        public double Price { get; set; }
        [Required]
        [Display(Name = "Price for 50+")]
        [Range(1, 10000, ErrorMessage = "The List Price must be between 1-10000.")]
        public double Price50 { get; set; }
        [Required]
        [Display(Name = "Price for 100+")]
        [Range(1, 10000, ErrorMessage = "The List Price must be between 1-10000.")]
        public double Price100 { get; set; }
        [Display(Name = "Category Id")]
        public int CategoryId { get; set; }   // FK column
        [ValidateNever]
        public Category Category { get; set; } // Navigation property
        [Display(Name = "Image Url")]
        public string? ImageUrl { get; set; }
    }
}
