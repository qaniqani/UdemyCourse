using System.Threading.Tasks;
using Udemy.Course.Configuration.Dto;

namespace Udemy.Course.Configuration
{
    public interface IConfigurationAppService
    {
        Task ChangeUiTheme(ChangeUiThemeInput input);
    }
}
