using IdentityServer4.Models;
using IdentityServer4.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace IdentityServerCenter
{
    public class ProfileService : IProfileService
    {
        public Task GetProfileDataAsync(ProfileDataRequestContext context)
        {
            var claims = new List<Claim> {
                new Claim("menu","Manage"),
                new Claim("menu","User"),
                new Claim("menu","Role"),
                new Claim("menu","Vehicle"),
                new Claim("role","admin"),
                new Claim("policy","get"),
                 new Claim("user","fran"),
            };

            context.AddRequestedClaims(claims);
            //context.IssuedClaims.AddRange(claims);
            return Task.CompletedTask;
        }

        public Task IsActiveAsync(IsActiveContext context)
        {
            context.IsActive = true;
            return Task.CompletedTask;
        }
    }
}
