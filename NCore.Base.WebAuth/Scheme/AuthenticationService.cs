using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;

namespace NCore.Base.WebAuth.Scheme
{
  public class AuthenticationService : IAuthService
  {
    private readonly string _identifier;
    private readonly string _identityTokenType;

    public AuthenticationService(string authSchemeIdentifier, string identityTokenType)
    {
      _identifier = authSchemeIdentifier;
      _identityTokenType = identityTokenType;
    }

    public async Task<ClaimsPrincipal> SignInAsync(HttpContext context, string identityToken, IEnumerable<Claim> claims)
    {
      var activeClaims = claims.ToList();
      activeClaims.RemoveAll(i => i.Type == _identityTokenType);
      activeClaims.Add(new Claim(_identityTokenType, identityToken));

      var userIdentity = new ClaimsIdentity(activeClaims, _identifier);
      var userPrincipal = new ClaimsPrincipal(userIdentity);
      await context.SignInAsync(_identifier, userPrincipal);
      return userPrincipal;
    }

    public async Task<ClaimsPrincipal> SignInAsync(HttpContext context, string identityToken, IClaims claims)
    {
      return await SignInAsync(context, identityToken, claims.Claims);
    }

    public async Task<ClaimsPrincipal> SignInAsync<TClaim>(HttpContext context, string identityToken) where TClaim : IClaims
    {
      var claims = Activator.CreateInstance<TClaim>();
      return await SignInAsync(context, identityToken, claims);
    }

    public async Task SignOutAsync(HttpContext context)
    {
      await context.SignOutAsync(_identifier);
    }
  }
}