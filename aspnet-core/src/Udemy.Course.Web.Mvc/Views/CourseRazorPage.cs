using Abp.AspNetCore.Mvc.Views;
using Abp.Runtime.Session;
using Microsoft.AspNetCore.Mvc.Razor.Internal;

namespace Udemy.Course.Web.Views
{
    public abstract class CourseRazorPage<TModel> : AbpRazorPage<TModel>
    {
        [RazorInject]
        public IAbpSession AbpSession { get; set; }

        protected CourseRazorPage()
        {
            LocalizationSourceName = CourseConsts.LocalizationSourceName;
        }
    }
}
