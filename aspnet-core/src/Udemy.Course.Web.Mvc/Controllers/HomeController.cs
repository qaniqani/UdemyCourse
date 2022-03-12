using Microsoft.AspNetCore.Mvc;
using Abp.AspNetCore.Mvc.Authorization;
using Udemy.Course.Controllers;

namespace Udemy.Course.Web.Controllers
{
    [AbpMvcAuthorize]
    public class HomeController : CourseControllerBase
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}
