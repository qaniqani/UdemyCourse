using System.Threading.Tasks;
using Abp.Application.Services;
using Udemy.Course.Sessions.Dto;

namespace Udemy.Course.Sessions
{
    public interface ISessionAppService : IApplicationService
    {
        Task<GetCurrentLoginInformationsOutput> GetCurrentLoginInformations();
    }
}
