[TOC]

# IdentityServer4

## 话题(Topics)

### 启动(StartUp)

#### 启动

IDS 是一组中间件和服务的结合。所有的配置都在startup.cs中完成

#### 配置服务

添加服务通过在DI（依赖注入）系统中调用，如下：

```c#
public void ConfigureServers(IServerCollection services)
{
    var builder =services.AddIdentityServer();
}
```

通常，你能通过传递options到这个调用函数中，点击 [ here](https://identityserver4.readthedocs.io/en/release/reference/options.html#refoptions) 查看详细的关于options的描述。

它将返回一个builder对象，这个对象提供了许多方便的方法来连接其他服务。

#### 密钥材料（Key material）

- AddSigningCredential

  添加一个签名密钥（Siging key）,它提供了指定的密钥材料给各种令牌（token）创建和验证服务。你也可以传递一个常量 X509Certificate2 ，一个签名认证或者一个来自证书库（Certificate Store）中的证书引用。

- AddDeveloperSigningCredential

  创建一个临时的密钥材料在启动的时候，这个仅仅用在当你没有证书可以提供使用的开发场景。生成的密钥会被保存到文件系统中，以便再服务器重启时能保持稳定（可传递flase禁用）。这解决了客户端/API元素据缓在开发期间不同步的问题

- AddValidationKey

  添加一个用来验证令牌的密钥。它能被用来进行内部令牌验证而且会被展示给显示文档。你可以传递一个常量 X509Certificate2 ，一个来自证书库（Certificate Store）中的证书引用。这对于密钥的转换场景非常有用

#### 内存中配置存储（In-Memory configuration stores）

多种“In-Memory”配置APIs允许从内存中的配置对象来配置IdentityServer。这些“内存中”集合能被硬编码在主机应用中，也能从配置文件或者数据库动态加载。但是，通过设计，这些集合仅在托管应用程序启动时创建。

- AddInMemoryClients

  基于Client配置对象的内存集合来注册IClientStore和ICorsPoliceService的实现。

- AddInMemoryIdentityResources

  基于IdentityResources配置对象的内存集合来注册IResourceStore的实现

- AddInmomoryApiResources

  基于ApiResources配置对象的内存集合来注册IResourceStore的实现

#### 额外服务（Additonal Services）

- AddExtensionGrantValidator
- AddSecretParser
- AddSecretValidator
- AddResourceOwenrValidator
- AddProfileService
- AddAuthorizeInteractionResponseGenerator
- AddCustomAuthorizeRequestValidator
- AddCustomTokenRequestValidator
- AddRedirectUriValidator
- AddAppAuthRedirectUriValidator
- AddJwtBreaClientAuthentication

#### 缓存（Caching）

客户端和资源配置数据经常被IdentityServer使用。如果从数据库或其他外部存储装载此数据，则经常重新加载相同数据可能会很占开销，所以提供缓存功能以降低开销。

- AddInMemoryCaching

  要使用这些缓存，必须在DI中注册ICache<T>的实现。这个API的注册默认在基于Asp.net Core的MemoryCache的ICache<T>的内存中实现。

- AddClientStoreCache

  注册一个IClientStore的装饰器实现，它将维持Client配置对象在内存中的缓存。这个Cache的持续时间是在可以在IdentityServerOptions上的Caching配置选项上配置。

- AddResourceStoreCache

  注册一个IResourceStore装饰器实现，它将维护IdentityResource和ApiResource配置对象的内存缓存。缓存持续时间可以在IdentityServerOptions上的Caching配置选项上配置。

- AddCorsPolicyCache

- 册ICorsPolicyService装饰器实现，该实现将维护CORS策略服务评估结果的内存缓存。缓存持续时间可以在IdentityServerOptions上的Caching配置选项上配置。

进一步客制化定义缓存也是可以的：

默认的缓存依赖于ICache<T>的实现。如果你想为指定的配置对象定义一个缓存行为，你也可以在DI中替换这些实现。

ICache<T>的默认实现是依赖于.NET提供的IMemoryCache接口（还有MemoryCache实现）。如果你想定义内存中的缓存行为，你可以在DI系统中替换IMemoryCache的实现。

#### 配置管道

```c#
public void Configure(IApplicationBuilder app)
{
    app.UseIdentityServer();
}
```

注意：UseIdentityServer已经包括了UseAuthentication的调用，所以不需要两种都来声明。

这段就是像中间件添加配置，也没有额外的option啥的来配置。

请注意，管道配置的顺序在管道中很重要。例如，您需要在实现登录屏幕的UI框架之前添加IdentitySever（比如要在AddMvc之前）。

### 资源定义（Defining Resource）

通常，第一件事是定义那些你想保护的资源。这些资源可能是你的用户信息，比如个人数据，电子邮件或者对Api的访问。

Note:

你可以用C#实体类来定义资源或者加载从数据库中加载他们，都是通过对IResourceStore的实现来处理这些二级细节。此文档将在内存中进行实现。

#### 定义身份资源（Defining identity resource）

身份资源通常都是指那些用户ID,名称，邮箱等信息。一个identity 资源有一个独一无二的名称，你能分配任意（arbitrary ）的claim类型。这些claim将被包含在用户的token中。客户端将使用scope参数请求对identity  resource的访问。

这个OpenID Connect规范指定了一组标准的身份资源。最低要求是，你为用户发行唯一ID的提供支持，也叫做subject id。这是通过暴露一个叫做OpenId的标准的身份资源来完成的。

```c#
public static IEnumerable<IdentityResource> GetIdentityResources()
{
    return new List<IdentityResource>
    {
        new IdentityResources.OpenId()
    };
}
```

这个IdentityResources类支持规范中所有的scopes 定义（比如，openid,email.profile,telephone,address）。如果你想支持这些，你可以在你的身份资源List中添加它们。

```c#
public static IEnumerable<IdentityResource> GetIdentityResources()
{
    return new List<IdentityResource>
    {
        new IdentityResources.OpenId(),
        new IdentityResources.Email(),
        new IdentityResources.Profile(),
        new IdentityResources.Phone(),
        new IdentityResources.Address()
    }
}
```

#### 自定义身份资源

你也可以自定义身份资源。创建一个新的IdentityResource类，给它一个名称和一个可选的显示名称和描述，并在请求此资源时定义哪些用户声明应该包含在身份令牌中。

```c#
public static IEnumerable<IdentityResource> GetIdentityResources()
{
    var customProfile =new IdentityResource(
    	name:"custom.Profile",
    	displayName:"cstProfile",
    	claimType:new[]{"name","email","status"});
    	
    return new List<IdentityResource>
    {
        new IdentityResources.OpenId(),
        new IdentityResources.Profile(),
        customProfile
    };
}
```

有关身份资源设置的更多信息，请参阅[reference](https://identityserver4.readthedocs.io/en/release/reference/api_resource.html#refapiresource) 部分。

#### 定义API resource

允许请求的access token访问哪些api,你需要定义这些api resource。

想得到关于APIs的accsee token,你需要以scope的身份注册它们。下面展示Resource类型的scope.

```c#
public static IEnumerable<ApiResource> GetApis()
{
    return new[]
    {
        
        new ApiResource("api1","Some API 1"),
        
        new ApiResource
        {
            Name = "api2",
            
            ApiSecrets=
            {
                new Secret("secret".Sha256())
            },
            Scopes=
            {
                 new Scope()
                {
                    Name = "api2.full_access",
                    DisplayName = "Full access to API 2",
                },
                new Scope
                {
                    Name = "api2.read_only",
                    DisplayName = "Read only access to API 2"
                }
            }
        }
    };
}
```

有关API资源设置的更多信息，请参阅[reference](https://identityserver4.readthedocs.io/en/release/reference/api_resource.html#refapiresource)部分。

Note:

由资源定义的用户声明由IProfileService可扩展性点加载。

### 客户端定义（Defining Client）

客户端可以从你的IDS服务器请求tokens。

通常，客户端需要遵循下面的通用设置：

- 一个唯一的Client ID
- 如果需要还可以提供密码
- 允许与token服务交互（授权类型）
- identity 和/或 token被发送到的网络位置（redirect URI）
- 客户端允许访问的scopes清单（resources）

Note:

在运行时，客户端通过实现IClientStore来检索。它允许在任意数据资源中加载比如配置文件或者数据库。此文档将使用内存版本进行客户端存储。你可以在ConfigureServices通过AddInMemoryClients额外方法连接内存存储。

#### 定义一个服务器到服务器通信的客户端

在这种情况（scenario）下没有交互用户，服务端（这里是客户端）想和API（Scope）通信：

```c#
public class Clients
{
    public static IEnumerable<Client> Get()
    {
        return new List<Client>
        {
            new Client
            {
                ClientId = "service.client",
                ClientSecrets = { new Secret("secret".Sha256()) },

                AllowedGrantTypes = GrantTypes.ClientCredentials,
                AllowedScopes = { "api1", "api2.read_only" }
            }
        };
    }
}
```

#### 定义基于浏览器的JavaScript客户端（例如SPA）以进行用户认证和授权访问和API

```c#
var jsClient =new Client
{
	ClientId="js",
	ClientName=""
    ClientName = "JavaScript Client",
    ClientUri = "http://identityserver.io",

    AllowedGrantTypes = GrantTypes.Implicit,
    AllowAccessTokensViaBrowser = true,

    RedirectUris =           { "http://localhost:7017/index.html" },
    PostLogoutRedirectUris = { "http://localhost:7017/index.html" },
    AllowedCorsOrigins =     { "http://localhost:7017" },

    AllowedScopes =
    {
        IdentityServerConstants.StandardScopes.OpenId,
        IdentityServerConstants.StandardScopes.Profile,
        IdentityServerConstants.StandardScopes.Email,

        "api1", "api2.read_only"
    }
}
```

#### 定义服务器端Web应用程序（例如MVC）以进行使用验证和授权API访问

交互式服务器端（或本地桌面/移动）应用程序使用混合流。此流程为您提供最佳的安全性，因为访问令牌仅通过反向通道呼叫传输（并允许您访问刷新令牌）

```c#
var mvcClient = new Client
{
    ClientId = "mvc",
    ClientName = "MVC Client",
    ClientUri = "http://identityserver.io",

    AllowedGrantTypes = GrantTypes.Hybrid,
    AllowOfflineAccess = true,
    ClientSecrets = { new Secret("secret".Sha256()) },

    RedirectUris =           { "http://localhost:21402/signin-oidc" },
    PostLogoutRedirectUris = { "http://localhost:21402/" },
    FrontChannelLogoutUri =  "http://localhost:21402/signout-oidc",

    AllowedScopes =
    {
        IdentityServerConstants.StandardScopes.OpenId,
        IdentityServerConstants.StandardScopes.Profile,
        IdentityServerConstants.StandardScopes.Email,

        "api1", "api2.read_only"
    },
};
```

### 登入

想让IdentityServer代表用户发布令牌，用户必须登入到IdentityServer端。

#### Cookie认证

通过Asp.Net Core的cookie认证处理程序管理cookie跟踪身份认证

IdentityServer注册两个cookie处理程序（一个是用于身份验证Session,另一个用于临时的外部cookie）。默认情况下都使用它们，如果你想手动引用它们，您可以从IdentityServerConstants类中获得它们的名称（分别是DefaultCookieAuthenticationScheme和ExternalCookieAuthenticationScheme）。

IDS仅仅为这些Cookie暴露基础的设置（expiration(过期) and sliding（滑动））。如果你需要更多的控制权，你能自己注册cookie handlers。当在ASP.NET CORE中使用AddAuthentication时，IDS使用无论哪个cookie handler都匹配配置在AuthenticationOptions上的DefaultAuthenticateScheme。

#### 重写cookie handler配置

如果你希望使用自己的cookie 认证处理程序（handler），你必须自己配置它。在IDS进行依赖注入注册后(AddIdentitySever)，必须在ConfigureServices中完成你所需的配置。

```c#
//注册AddIdentityServer到依赖注入管道中
services.AddIdentityServer()
    .AddInMemoryClient(Config.GetClients())
    .AddInMemoryIdentityResource(Config.GetIdentityResource())
    .AddInMemoryApiResource(Config.GetApiResource())
    .AddDevelopSigningCredential()
    .AddTestUsers(Config.GetTestUsers());

//配置自己的Cookie Handler
services.AddAuthentication("MyCookie")
    .AddCookie("MyCookie",options=>
    {
    	options.ExpireTimeSpan=...;
    })
```

注意：IDS内部都会使用自定义计划调用AddAuthentication 和 AddCookie(通过常量IdentityServerConstant.DefaultCookieAuthenticationScheme),所以想重写它们，必须在AddIdentityServer后面调用。

#### 登陆用户接口和认证管理系统

IDS并不为用户身份认证提供任何用户接口或者用户数据库。这些东西你可以自己实现或者借助别的提供。

你可以用IDS的quickstart UI。作为开始。

quickstart UI使用存储在内存中的模拟用户，你可以用真实的用户库来替代。IDS有些Samples使用ASP.NET Identity。

#### 登陆流程

当IdentityServer在授权登陆端点收到一个请求而且这个用户没有进行认证。用户将跳转到配置的登陆页面。你必须在options的设置中通过UserInteraction为IDS提供登陆页面的路径（默认是/account/login）。提供的returnUrl参数，将会告诉登陆页面在完成正确登陆后跳转到哪个新的页面（主页）。

![../_images/signin_flow.png](https://identityserver4.readthedocs.io/en/release/_images/signin_flow.png) 

注意：你应该意识到那些可能通过returnUrl进行的重定向攻击。您应该验证returnUrl是指向到已知的（well-known）位置。这里可以参考API的交互服务以及验证returnUrl参数。
