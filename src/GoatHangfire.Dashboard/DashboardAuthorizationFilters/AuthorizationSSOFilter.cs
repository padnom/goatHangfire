using Hangfire.Dashboard;

namespace GoatHangfire.Dashboard.DashboardAuthorizationFilters;

public class AuthorizationSsoFilter : IDashboardAuthorizationFilter
{
  public bool Authorize(DashboardContext context)
  {
    HttpContext? httpContext = context.GetHttpContext();

    // Check if the user is authenticated.
    return httpContext.User.Identity is { IsAuthenticated: true };
  }
}