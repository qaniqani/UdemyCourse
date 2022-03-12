using Abp.AspNetCore.Mvc.Controllers;
using Abp.IdentityFramework;
using Microsoft.AspNetCore.Identity;

namespace Udemy.Course.Controllers
{
    public abstract class CourseControllerBase: AbpController
    {
        protected CourseControllerBase()
        {
            LocalizationSourceName = CourseConsts.LocalizationSourceName;
        }

        protected void CheckErrors(IdentityResult identityResult)
        {
            identityResult.CheckErrors(LocalizationManager);
        }
    }
}
