using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Abp.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Mvc;
using Udemy.Course.Authorization;
using Udemy.Course.Controllers;
using Udemy.Course.Customers;
using Udemy.Course.Web.Models.Customers;

namespace Udemy.Course.Web.Controllers
{
    [AbpMvcAuthorize(PermissionNames.Pages_Customers)]
    public class CustomerController : CourseControllerBase
    {
        private readonly ICustomerAppService _customerAppService;

        public CustomerController(ICustomerAppService customerAppService)
        {
            _customerAppService = customerAppService;
        }

        public ActionResult Index()
        {
            return View();
        }
        
        public ActionResult Create()
        {
            return View();
        }
        
        public async Task<ActionResult> EditModal(int customerId)
        {
            var output = await _customerAppService.GetAsync(new EntityDto<int> {Id = customerId});

            return PartialView("_EditModal", output);
        }
        
        public async Task<ActionResult> CreateEditModal(int? customerId)
        {
            if(!customerId.HasValue)
                return PartialView("_CreateEditModal", new CustomerViewModel());
            
            var output = await _customerAppService.GetAsync(new EntityDto<int> {Id = customerId.Value});
            
            var vm = new CustomerViewModel
            {
                IsEdit = true,
                Customer = output
            };
            
            return PartialView("_CreateEditModal", vm);
        }
    }
}

