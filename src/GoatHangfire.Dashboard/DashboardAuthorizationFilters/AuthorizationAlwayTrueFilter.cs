using Hangfire.Dashboard;

namespace GoatHangfire.Dashboard.DashboardAuthorizationFilters;
public class AuthorizationAlwayTrueFilter : IDashboardAuthorizationFilter
{
  public bool Authorize(DashboardContext context)
  {
    return true;
  }
}