namespace GoatHangfire.InternalJob;
public interface IGoatService
{
  Task RecurringExecuteAsync(CancellationToken cancellationToken);
  Task QueueExecuteAsync(CancellationToken cancellationToken);
  void QueueExecute();
}