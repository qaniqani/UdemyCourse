using Abp.AspNetCore;
using Abp.AspNetCore.TestBase;
using Abp.Modules;
using Abp.Reflection.Extensions;
using Udemy.Course.EntityFrameworkCore;
using Udemy.Course.Web.Startup;
using Microsoft.AspNetCore.Mvc.ApplicationParts;

namespace Udemy.Course.Web.Tests
{
    [DependsOn(
        typeof(CourseWebMvcModule),
        typeof(AbpAspNetCoreTestBaseModule)
    )]
    public class CourseWebTestModule : AbpModule
    {
        public CourseWebTestModule(CourseEntityFrameworkModule abpProjectNameEntityFrameworkModule)
        {
            abpProjectNameEntityFrameworkModule.SkipDbContextRegistration = true;
        } 
        
        public override void PreInitialize()
        {
            Configuration.UnitOfWork.IsTransactional = false; //EF Core InMemory DB does not support transactions.
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(CourseWebTestModule).GetAssembly());
        }
        
        public override void PostInitialize()
        {
            IocManager.Resolve<ApplicationPartManager>()
                .AddApplicationPartsIfNotAddedBefore(typeof(CourseWebMvcModule).Assembly);
        }
    }
}