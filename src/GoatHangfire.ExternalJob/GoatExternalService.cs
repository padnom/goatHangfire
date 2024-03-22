namespace GoatHangfire.ExternalJob;
public class GoatExeternalService : IGoatExternalService
{
  public async Task ExecuteAsync(CancellationToken cancellationToken)
  {
    await Task.Delay(100,cancellationToken);
    Console.WriteLine("GoatService QueueJobExecuteAsync Executed");
  }
}