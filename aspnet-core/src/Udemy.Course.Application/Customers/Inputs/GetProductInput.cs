using System.ComponentModel.DataAnnotations;
using Abp.Application.Services.Dto;

namespace Udemy.Course.Customers.Inputs
{
    public class GetProductInput
    {
        [Required]
        public int CustomerId { get; set; }

        public int? TenantId { get; set; }
    }
}