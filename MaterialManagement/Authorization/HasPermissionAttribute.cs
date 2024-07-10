using Microsoft.AspNetCore.Authorization;

namespace MaterialManagement.Authorization
{
    public class HasPermissionAttribute : AuthorizeAttribute
    {

        public HasPermissionAttribute(string permission) : base(policy: permission) 
        {
        
        }
    }
}
