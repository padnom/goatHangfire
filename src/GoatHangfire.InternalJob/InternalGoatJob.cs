using Hangfire;
using Microsoft.Extensions.Hosting;

namespace GoatHangfire.InternalJob;
public class InternalJob1 : BackgroundService
{
  private readonly IRecurringJobManager _recurringJobs;
  private readonly IGoatService _goatService;

  public InternalJob1(IRecurringJobManager recurringJobs,IGoatService goatService)
  {
    _recurringJobs = recurringJobs;
    _goatService = goatService;
  }
  protected override Task ExecuteAsync(CancellationToken stoppingToken)
  {
    _recurringJobs.AddOrUpdate("internal-job-1", () => _goatService.ExecuteAsync(), Cron.Minutely);
    return Task.CompletedTask;

  }
}