using Abp.Authorization;
using Udemy.Course.Authorization.Roles;
using Udemy.Course.Authorization.Users;

namespace Udemy.Course.Authorization
{
    public class PermissionChecker : PermissionChecker<Role, User>
    {
        public PermissionChecker(UserManager userManager)
            : base(userManager)
        {
        }
    }
}
