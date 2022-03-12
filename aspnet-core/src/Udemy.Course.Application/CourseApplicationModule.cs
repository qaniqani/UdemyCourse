using Abp.AutoMapper;
using Abp.Modules;
using Abp.Reflection.Extensions;
using Udemy.Course.Authorization;

namespace Udemy.Course
{
    [DependsOn(
        typeof(CourseCoreModule), 
        typeof(AbpAutoMapperModule))]
    public class CourseApplicationModule : AbpModule
    {
        public override void PreInitialize()
        {
            Configuration.Authorization.Providers.Add<CourseAuthorizationProvider>();
            
            //Adding custom AutoMapper configuration
            Configuration.Modules.AbpAutoMapper().Configurators.Add(CustomDtoMapping.CreateMappings);
        }

        public override void Initialize()
        {
            var thisAssembly = typeof(CourseApplicationModule).GetAssembly();

            IocManager.RegisterAssemblyByConvention(thisAssembly);

            Configuration.Modules.AbpAutoMapper().Configurators.Add(
                // Scan the assembly for classes which inherit from AutoMapper.Profile
                cfg => cfg.AddMaps(thisAssembly)
            );
        }
    }
}
