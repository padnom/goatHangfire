using Hangfire;

namespace GoatHangfire.ExternalJob;
public class ExternalGoatJob : BackgroundService
{
    private readonly IRecurringJobManager _recurringJobs;
    private readonly IGoatExternalService _goatService;

    public ExternalGoatJob(IRecurringJobManager recurringJobs, IGoatExternalService goatService)
    {
        _recurringJobs = recurringJobs;
        _goatService = goatService;
    }
    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _recurringJobs.AddOrUpdate("ExternalGoatJob", () => _goatService.ExecuteAsync(stoppingToken), Cron.Minutely);
        return Task.CompletedTask;
    }
}