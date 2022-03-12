using Abp.AutoMapper;

namespace Udemy.Course.Products.Dto
{
    [AutoMap(typeof(ProductMapping))]
    public class ProductMappingDto
    {
        public int? CustomerId { get; set; }
        public int? TenantId { get; set; }
        public int ProductId { get; set; }
    }
}