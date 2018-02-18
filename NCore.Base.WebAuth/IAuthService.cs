using System.Collections.Generic;
using System.Data.Common;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace NCore.Base.WebAuth
{
  public interface IAuthService
  {
    Task<ClaimsPrincipal> SignInAsync(HttpContext context, string identityToken, IEnumerable<Claim> claims);
    Task<ClaimsPrincipal> SignInAsync(HttpContext context, string identityToken, IClaims claims);
    Task<ClaimsPrincipal> SignInAsync<TClaim>(HttpContext context, string identityToken) where TClaim : IClaims;
    Task SignOutAsync(HttpContext context);
  }
}