using System.Collections.Generic;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

namespace NCore.Base.WebAuth.Demo
{
  public class AppUser : IPolicy, IClaims
  {
    public IEnumerable<Claim> Claims => new Claim[]
    {
      new Claim(AppAuth.UserType, AppAuth.ApplicationUser)
    };

    public string PolicyName => AppAuth.ApplicationUserPolicy;

    public void Require(AuthorizationPolicyBuilder policyOptions)
    {
      policyOptions.RequireAuthenticatedUser();
      policyOptions.RequireClaim(AppAuth.UserType, AppAuth.ApplicationUser);
    }
  }
}