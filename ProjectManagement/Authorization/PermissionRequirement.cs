using Microsoft.AspNetCore.Authorization;

namespace ProjectManagement.Authorization
{
    public class PermissionRequirement : IAuthorizationRequirement
    {
        public PermissionRequirement(string permission)
        {
            Permission = permission;

        }

        public string Permission { get; }
    }
}
