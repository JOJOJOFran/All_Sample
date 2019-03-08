using IdentityServer4.Models;
using IdentityServer4.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace IdentityServerCenter
{
    public class RescourceOwnerPasswordValidator : IResourceOwnerPasswordValidator
    {
        public Task ValidateAsync(ResourceOwnerPasswordValidationContext context)
        {
            var claims = new List<Claim> {
                new Claim("menu","Manage"),
                new Claim("menu","User"),
                new Claim("menu","Role"),
                new Claim("menu","Vehicle"),
            };

            Console.WriteLine(context.Request.Raw["username"]);
            Console.WriteLine(context.Request.Raw["password"]);
            context.Result = new GrantValidationResult("1","pwd",claims);
            //context.Result = new GrantValidationResult(TokenRequestErrors.InvalidGrant, "invalid custom credential");
            return Task.CompletedTask;
        }
    }
}
