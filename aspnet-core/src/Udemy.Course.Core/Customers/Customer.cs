using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;

namespace Udemy.Course.Customers
{
    [Table("customers")]
    public class Customer : FullAuditedEntity, IMustHaveTenant
    {
        public int TenantId { get; set; }

        public string Name { get; set; }
        public string Surname { get; set; }
        public string GsmNr { get; set; }
    }
}