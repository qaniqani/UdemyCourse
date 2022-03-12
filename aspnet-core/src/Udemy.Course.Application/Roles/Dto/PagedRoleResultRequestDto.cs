using Abp.Application.Services.Dto;

namespace Udemy.Course.Roles.Dto
{
    public class PagedRoleResultRequestDto : PagedResultRequestDto
    {
        public string Keyword { get; set; }
    }
}

