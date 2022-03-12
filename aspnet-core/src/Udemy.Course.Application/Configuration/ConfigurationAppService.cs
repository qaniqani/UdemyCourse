using System.Threading.Tasks;
using Abp.Authorization;
using Abp.Runtime.Session;
using Udemy.Course.Configuration.Dto;

namespace Udemy.Course.Configuration
{
    [AbpAuthorize]
    public class ConfigurationAppService : CourseAppServiceBase, IConfigurationAppService
    {
        public async Task ChangeUiTheme(ChangeUiThemeInput input)
        {
            await SettingManager.ChangeSettingForUserAsync(AbpSession.ToUserIdentifier(), AppSettingNames.UiTheme, input.Theme);
        }
    }
}
