using Abp.Domain.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace Udemy.Course.Products
{
    [Table("products")]
    public class Product : Entity
    {
        public string Name { get; set; }
        public decimal? Price { get; set; }
        public string Description { get; set; }
    }
}