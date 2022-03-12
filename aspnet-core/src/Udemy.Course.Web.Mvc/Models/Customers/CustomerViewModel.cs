using Udemy.Course.Customers.Dto;

namespace Udemy.Course.Web.Models.Customers
{
    public class CustomerViewModel
    {
        public bool IsEdit { get; set; } = false;
        public CustomerDto Customer { get; set; }
    }
}