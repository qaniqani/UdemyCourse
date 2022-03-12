using Abp.Application.Services.Dto;

namespace Udemy.Course.Customers.Inputs
{
    public class CustomerGetAllInput : PagedAndSortedResultRequestDto
    {
        public string Name { get; set; }
        public string Surname { get; set; }
    }
}