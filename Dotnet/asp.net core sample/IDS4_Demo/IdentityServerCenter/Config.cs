using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IdentityServer4.Models;
using IdentityServer4.Test;

namespace IdentityServerCenter
{
    public static  class Config
    {
        public static IEnumerable<ApiResource> GetApiResources()
        {
            return new List<ApiResource> { new ApiResource("api1", "api1"),  new ApiResource ("2", "Api2") };
        }

        public static IEnumerable<Client> GetClients()
        {
            //Validation.ClientSecretValidator
            //必须有AllowedScopes
            return new List<Client>
            {
                new Client {
                            ClientName = "IOS",
                            ClientId = "1",
                            AllowedGrantTypes=GrantTypes.ClientCredentials,
                            AllowedScopes={ "api1"},
                            ClientSecrets={new Secret("secret".Sha256()) }

                            },
                new Client{
                            ClientName="PC",
                            ClientId="pwdClient",
                            RequireClientSecret=false,
                            AllowedScopes={"api1" },
                            AllowedGrantTypes=GrantTypes.ResourceOwnerPassword
                            }
               
            };
        }

        public static IEnumerable<TestUser> GetTestUsers()
        {
            //必须有subjectId
            return new List<TestUser> {
                new TestUser{ SubjectId="1", Username="fran",Password="111"}
            };
        }
    }
}
