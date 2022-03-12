using System.ComponentModel.DataAnnotations;

namespace Udemy.Course.Products.Inputs
{
    public class GetProductInput
    {
        [Required]
        public int CustomerId { get; set; }

        public int? TenantId { get; set; }
    }
}