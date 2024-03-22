using Hangfire;

namespace GoatHangfire.InternalJob;

public class InternalGoatJob
{
    private readonly IBackgroundJobClient _backgroundJobs;
    private readonly IRecurringJobManager _recurringJobs;
    private readonly IGoatService _goatService;

    public InternalGoatJob(IBackgroundJobClient backgroundJobs,
                           IRecurringJobManager recurringJobs,
                           IGoatService goatService)
    {
        _backgroundJobs = backgroundJobs;
        _recurringJobs = recurringJobs;
        _goatService = goatService;
    }

    public Task ExecuteRecurringJobAsync(string cronExpession, CancellationToken stoppingToken)
    {
        _recurringJobs.AddOrUpdate<IGoatService>("internal-goat-recurring-job",
                                                x => x.RecurringExecuteAsync(stoppingToken),
                                                cronExpession);

        return Task.CompletedTask;
    }

    public void ExecuteQueueJobAsync(CancellationToken stoppingToken)
    {
        _backgroundJobs.Enqueue<IGoatService>(x => x.QueueExecuteAsync(stoppingToken));
    }

    public void ExecuteQueueJob()
    {
        _backgroundJobs.Enqueue(() => _goatService.QueueExecute());
    }
}