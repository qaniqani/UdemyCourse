using System.Collections.Generic;
using Udemy.Course.Roles.Dto;

namespace Udemy.Course.Web.Models.Roles
{
    public class RoleListViewModel
    {
        public IReadOnlyList<PermissionDto> Permissions { get; set; }
    }
}
