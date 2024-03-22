namespace GoatHangfire.ExternalJob;
public interface IGoatExternalService
{
  Task ExecuteAsync(CancellationToken cancellationToken);
}