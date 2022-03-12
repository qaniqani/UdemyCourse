using System;
using Abp.Domain.Entities;
using Udemy.Course.Customers;
using Udemy.Course.MultiTenancy;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities.Auditing;

namespace Udemy.Course.Products
{
    [Table("productmappings")]
    public class ProductMapping : FullAuditedEntity, IMayHaveTenant
    {
        public int? CustomerId { get; set; }
        [ForeignKey("CustomerId")]
        public Customer Customer { get; set; }

        public int? TenantId { get; set; }
        [ForeignKey("TenantId")]
        public Tenant Tenant { get; set; }
        
        public int ProductId { get; set; }
    }
}