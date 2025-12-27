using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ECommerce.Models
{
    public class ShoppingCart
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        // Navigation
        [ValidateNever]
        public Product Product { get; set; }
        [Range(1, 1000, ErrorMessage = "Please select a valid quantity (1-1000).")]
        public int Count { get; set; }
        public string ApplicationUserId { get; set; }
        [NotMapped]
        public double Price { get; set; }

    }
}
