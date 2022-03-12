using Abp.Authorization;
using Abp.Localization;
using Abp.MultiTenancy;

namespace Udemy.Course.Authorization
{
    public class CourseAuthorizationProvider : AuthorizationProvider
    {
        public override void SetPermissions(IPermissionDefinitionContext context)
        {
            context.CreatePermission(PermissionNames.Pages_Users, L("Users"));
            context.CreatePermission(PermissionNames.Pages_Roles, L("Roles"));
            context.CreatePermission(PermissionNames.Pages_Products, L("Products"));
            context.CreatePermission(PermissionNames.Pages_Products_CustomerProduct, L("CustomerProduct"));
            context.CreatePermission(PermissionNames.Pages_Users_Activation, L("UsersActivation"));
            
            context.CreatePermission(PermissionNames.Pages_Tenants, L("Tenants"), multiTenancySides: MultiTenancySides.Host);
            context.CreatePermission(PermissionNames.Pages_Products_CreateOrUpdate, L("ProductCreateOrUpdate"), multiTenancySides: MultiTenancySides.Host);
            
            context.CreatePermission(PermissionNames.Pages_Customers, L("Customers"), multiTenancySides:MultiTenancySides.Tenant);
            context.CreatePermission(PermissionNames.Pages_Customers_Create, L("CustomerCreate"), multiTenancySides:MultiTenancySides.Tenant);
            context.CreatePermission(PermissionNames.Pages_Customers_Update, L("CustomerUpdate"), multiTenancySides:MultiTenancySides.Tenant);
            context.CreatePermission(PermissionNames.Pages_Customers_Delete, L("CustomerDelete"), multiTenancySides:MultiTenancySides.Tenant);
            context.CreatePermission(PermissionNames.Pages_Customers_GetAll, L("CustomerGetAll"), multiTenancySides:MultiTenancySides.Tenant);
        }

        private static ILocalizableString L(string name)
        {
            return new LocalizableString(name, CourseConsts.LocalizationSourceName);
        }
    }
}
