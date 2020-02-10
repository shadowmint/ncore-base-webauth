using NCore.Base.WebAuth.Scheme;

namespace NCore.Base.WebAuth.Demo
{
    public class AuthBuilder : AuthenticationServiceBuilderBase
    {
        public AuthBuilder() : base(AppAuth.IdentityScheme)
        {
        }

        protected override void Configure()
        {
            AddPolicy<AppUser>();
        }
    }
}