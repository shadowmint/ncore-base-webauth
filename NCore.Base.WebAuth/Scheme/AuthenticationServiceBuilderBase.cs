using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.DependencyInjection;

namespace NCore.Base.WebAuth.Scheme
{
  public abstract class AuthenticationServiceBuilderBase : IAuthServiceBuilder
  {
    private const string DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    private const string DefaultTokenType = ClaimsIdentity.DefaultNameClaimType;

    private readonly string _identifer;
    private readonly string _tokenClaimType;
    private readonly List<IPolicy> _policies;

    protected AuthenticationServiceBuilderBase(string authSchemeIdentifier = DefaultScheme,
      string identityTokenClaimType = DefaultTokenType)
    {
      _tokenClaimType = identityTokenClaimType;
      _identifer = authSchemeIdentifier;
      _policies = new List<IPolicy>();
    }

    public IAuthServiceBuilder AddPolicy(IPolicy policy)
    {
      _policies.Add(policy);
      return this;
    }

    public IAuthServiceBuilder AddPolicy<T>() where T : IPolicy
    {
      AddPolicy(Activator.CreateInstance<T>());
      return this;
    }

    public IAuthService Build(IServiceCollection services)
    {
      var authBuilder = services.AddAuthentication(options =>
      {
        options.DefaultChallengeScheme = _identifer;
        options.DefaultAuthenticateScheme = _identifer;
        options.DefaultScheme = _identifer;
      });

      authBuilder.AddCookie(_identifer, options =>
      {
        options.Events.OnRedirectToLogin = (ctx) =>
        {
          ctx.Response.StatusCode = 403;
          return Task.CompletedTask;
        };
        options.Events.OnRedirectToAccessDenied = (ctx) =>
        {
          ctx.Response.StatusCode = 403;
          return Task.CompletedTask;
        };
      });

      Configure();
      services.AddAuthorization(ConfigureAuthorization);
      return new AuthenticationService(_identifer, _tokenClaimType);
    }

    private void ConfigureAuthorization(AuthorizationOptions authOptions)
    {
      foreach (var policy in _policies)
      {
        authOptions.AddPolicy(policy.PolicyName, policyOptions => ConfigurePolicy(policyOptions, policy));
      }
    }

    private static void ConfigurePolicy(AuthorizationPolicyBuilder policyOptions, IPolicy policy)
    {
      policy.Require(policyOptions);
    }

    /// <summary>
    /// Add any custom policy items in this implementation
    /// </summary>
    protected abstract void Configure();
  }
}