using Abp.Application.Services.Dto;
using Abp.AutoMapper;

namespace Udemy.Course.Customers.Inputs
{
    [AutoMap(typeof(Customer))]
    public class CustomerUpdateDto : EntityDto
    {
        public string Name { get; set; }
        public string Surname { get; set; }
    }
}