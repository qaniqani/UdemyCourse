using Abp.AutoMapper;
using Abp.Application.Services.Dto;

namespace Udemy.Course.Customers.Dto
{
    [AutoMap(typeof(Customer))]
    public class CustomerDto : EntityDto
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string GsmNr { get; set; }
    }
}