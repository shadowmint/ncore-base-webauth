using NCore.Base.WebAuth.Scheme.Standard.Policy;

namespace NCore.Base.WebAuth.Scheme.Standard
{
  public class StandardAuthenticationServiceBuilder : AuthenticationServiceBuilderBase
  {
    protected override void Configure()
    {
      AddPolicy<ApplicationUserPolicy>();
    }
  }
}