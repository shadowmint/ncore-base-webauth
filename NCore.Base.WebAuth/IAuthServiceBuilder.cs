using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.DependencyInjection;

namespace NCore.Base.WebAuth
{
  public interface IAuthServiceBuilder
  {
    IAuthServiceBuilder AddPolicy<T>() where T : IPolicy;
    IAuthServiceBuilder AddPolicy(IPolicy policy);
    IAuthService AddAuthorization(IServiceCollection services);
  }
}