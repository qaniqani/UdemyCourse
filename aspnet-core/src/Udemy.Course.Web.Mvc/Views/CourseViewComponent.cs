using Abp.AspNetCore.Mvc.ViewComponents;

namespace Udemy.Course.Web.Views
{
    public abstract class CourseViewComponent : AbpViewComponent
    {
        protected CourseViewComponent()
        {
            LocalizationSourceName = CourseConsts.LocalizationSourceName;
        }
    }
}
