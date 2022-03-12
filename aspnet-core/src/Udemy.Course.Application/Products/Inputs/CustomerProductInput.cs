using System.ComponentModel.DataAnnotations;

namespace Udemy.Course.Products.Inputs
{
    public class CustomerProductInput
    {
        [Required]
        public int CustomerId { get; set; }
    }
}