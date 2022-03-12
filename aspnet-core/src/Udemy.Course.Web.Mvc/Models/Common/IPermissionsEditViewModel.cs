using System.Collections.Generic;
using Udemy.Course.Roles.Dto;

namespace Udemy.Course.Web.Models.Common
{
    public interface IPermissionsEditViewModel
    {
        List<FlatPermissionDto> Permissions { get; set; }
    }
}