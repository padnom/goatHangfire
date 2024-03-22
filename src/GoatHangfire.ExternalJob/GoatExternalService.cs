namespace GoatHangfire.ExternalJob;
public class GoatExternalService : IGoatExternalService
{
  public async Task ExecuteAsync(CancellationToken cancellationToken)
  {
    await Task.Delay(5000,cancellationToken);
    Console.WriteLine("GoatService QueueGoatExternalService Executed");
  }
}