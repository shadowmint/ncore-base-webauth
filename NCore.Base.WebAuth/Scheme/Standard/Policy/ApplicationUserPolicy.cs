using System.Collections.Generic;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

namespace NCore.Base.WebAuth.Scheme.Standard.Policy
{
  public class ApplicationUserPolicy : IPolicy, IClaims
  {
    public IEnumerable<Claim> Claims => new[]
    {
      new Claim(StandardAuthentication.UserType, StandardAuthentication.UserType)
    };

    public string PolicyName => StandardAuthentication.ApplicationUserPolicy;

    public void Require(AuthorizationPolicyBuilder policyOptions)
    {
      policyOptions.RequireAuthenticatedUser();
      policyOptions.RequireClaim(StandardAuthentication.UserType, StandardAuthentication.ApplicationUser);
    }
  }
}