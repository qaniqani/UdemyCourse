using Abp.Application.Services;
using Udemy.Course.MultiTenancy.Dto;

namespace Udemy.Course.MultiTenancy
{
    public interface ITenantAppService : IAsyncCrudAppService<TenantDto, int, PagedTenantResultRequestDto, CreateTenantDto, TenantDto>
    {
    }
}

