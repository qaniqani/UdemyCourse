using System.Threading.Tasks;
using Abp.Application.Services;
using Udemy.Course.Authorization.Accounts.Dto;

namespace Udemy.Course.Authorization.Accounts
{
    public interface IAccountAppService : IApplicationService
    {
        Task<IsTenantAvailableOutput> IsTenantAvailable(IsTenantAvailableInput input);

        Task<RegisterOutput> Register(RegisterInput input);
    }
}
