using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace NCore.Base.WebAuth.Demo.Controllers
{
  [Route("/api/[controller]/[action]")]
  public class AuthController : Controller
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

    [Authorize]
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