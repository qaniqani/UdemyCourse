using Abp.Application.Services.Dto;

namespace Udemy.Course.Products.Dto
{
    public class ProductDto : EntityDto
    {
        public string Name { get; set; }
        public decimal? Price { get; set; }
        public string Description { get; set; }
    }
}