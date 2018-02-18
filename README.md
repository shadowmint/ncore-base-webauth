# NCore.Base.WebAuth

Basic consistent code first web authentication for .net core.

# Usage

Setup auth claims:

    using System;
    using System.Collections.Generic;
    using System.Security.Claims;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Logging;
    using NCore.Base.WebAuth;
    using NCore.Base.WebAuth.Scheme;
    
    namespace NCore.Base.WebAuthTests
    {
      public class Startup
      {
        public void ConfigureServices(IServiceCollection services)
        {
          services.AddMvc();
    
          var authService = new AuthBuilder().Build(services);
          services.AddSingleton(authService);
        }
    
        public void Configure(IApplicationBuilder app, ILoggerFactory loggerfactory)
        {
          app.UseAuthentication();
          app.UseMvc();
        }
      }
    
      public static class AppAuth
      {
        public const string UserType = "UserType";

        public const string Scheme = "Custom";
    
        public const string ApplicationUser = "Application User";
    
        public const string ApplicationUserPolicy = "Application User";
      }
    
      public class AppUser : IPolicy, IClaims
      {
        public IEnumerable<Claim> Claims => new Claim[]
        {
          new Claim(AppAuth.UserType, AppAuth.ApplicationUser)
        };
    
        public string PolicyName => AppAuth.ApplicationUserPolicy;
    
        public void Require(AuthorizationPolicyBuilder policyOptions)
        {
          policyOptions.AuthenticationSchemes.Add(AppAuth.Scheme);
          policyOptions.RequireAuthenticatedUser();
          policyOptions.RequireClaim(AppAuth.UserType, AppAuth.ApplicationUser);
        }
      }
    
      public class AuthBuilder : AuthenticationServiceBuilderBase
      {
        public AuthBuilder() : base(AppAuth.Scheme)
        {
        }

        protected override void Configure()
        {
          AddPolicy<AppUser>();
        }
      }
    }

...and use:

    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using NCore.Base.WebAuth;
    
    namespace NCore.Base.WebAuthTests.Controller
    {
      [Route("/api/[controller]/[action]")]
      public class AuthController : Microsoft.AspNetCore.Mvc.Controller
      {
        private readonly IAuthService _auth;
    
        public AuthController(IAuthService auth)
        {
          _auth = auth;
        }
    
        public async Task<IActionResult> Login()
        {
          await _auth.SignInAsync<AppUser>(HttpContext, "doug");
          return Ok("Logged In!");
        }
    
        public async Task<IActionResult> Logout()
        {
          await _auth.SignOutAsync(HttpContext);
          return Ok("Logged out!");
        }
    
        public IActionResult Claims()
        {
          var claims = $"User: {HttpContext.User.Identity.Name} (Auth: {HttpContext.User.Identity.IsAuthenticated})";
          claims = HttpContext.User.Claims.Aggregate(claims,
            (current, claim) => current + $"\n{claim.Type}: {claim.Value}");
          return Ok(claims);
        }
    
        [Authorize(Policy = AppAuth.ApplicationUserPolicy)]
        public IActionResult Test()
        {
          return Ok("Was authorized!");
        }
      }
    }

# Installing

    npm install --save shadowmint/ncore-base-webauth

Now add the `NuGet.Config` to the project folder:

    <?xml version="1.0" encoding="utf-8"?>
    <configuration>
     <packageSources>
        <add key="local" value="./packages" />
     </packageSources>
    </configuration>

Now you can install the package:

    dotnet add package NCore.Base.WebAuth

You may also want to use `dotnet nuget locals all --clear` to clear cached objects.

# Building a new package version

    npm run build

# Testing

    npm test
