namespace GoatHangfire.ExternalJob;
public class Worker : BackgroundService
{
  private readonly ILogger<Worker> _logger;

  private readonly IRecurringJobManager _recurringJobs;
  private readonly IGoatService _goatService;

  public ExternalGoatJob(IRecurringJobManager recurringJobs,IGoatService goatService)
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