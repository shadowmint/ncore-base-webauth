using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;

namespace NCore.Base.WebAuth
{
  public interface IPolicy
  {
    string PolicyName { get; }
    void Require(AuthorizationPolicyBuilder policyOptions);
  }
}