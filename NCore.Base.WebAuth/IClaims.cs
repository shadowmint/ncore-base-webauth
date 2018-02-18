using System.Collections.Generic;
using System.Security.Claims;

namespace NCore.Base.WebAuth
{
  /// <summary>
  /// A specific set of related claims.
  /// </summary>
  public interface IClaims
  {
    IEnumerable<Claim> Claims { get; }
  }
}