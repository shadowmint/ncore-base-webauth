namespace NCore.Base.WebAuth.Demo
{
  public static class AppAuth
  {
    // Identity and cookie name
    public static string IdentityScheme = "DemoIdentity";
    
    // Types of user values; each value should match a claim type
    public const string UserType = "UserType";

    // The user type claim; you could have other claims like access level, is staff, etc.
    public const string ApplicationUser = "Application User";

    // A policy for application user.
    public const string ApplicationUserPolicy = "Application User";
  }
}