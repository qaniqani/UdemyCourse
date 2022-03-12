using Udemy.Course.Products.Dto;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Udemy.Course.Customers.Inputs
{
    public class CreateCustomerAndProductInput
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Surname { get; set; }
        public List<ProductMappingDto> Products { get; set; }
    }
}