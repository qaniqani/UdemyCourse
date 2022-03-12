using System.Collections.Generic;
using Udemy.Course.Roles.Dto;

namespace Udemy.Course.Web.Models.Users
{
    public class UserListViewModel
    {
        public IReadOnlyList<RoleDto> Roles { get; set; }
    }
}
