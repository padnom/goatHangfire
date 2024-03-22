namespace GoatHangfire.InternalJob;
public class GoatService : IGoatService
{
  public async Task RecurringExecuteAsync(CancellationToken cancellationToken)
  {
    await Task.Delay(5000,cancellationToken);
    Console.WriteLine($"GoatService RecurringJobExecuteAsync Executed: {DateTime.Now}");
  }

  public async Task QueueExecuteAsync(CancellationToken cancellationToken)
  {
    await Task.Delay(5000,cancellationToken);
    Console.WriteLine("GoatService QueueJobExecuteAsync Executed");
  }

  public void QueueExecute()
  {
    Console.WriteLine("GoatService Executed");
  }
}