using UniRx;

public class TimeIntegration
{
    public TimeIntegration(EnemiesAggregate enemies)
    {
        var timeEvents = Observable.EveryUpdate().Select(_ => new TimeEvent());

        timeEvents.Subscribe(enemies.TimeObserver);
    }
}
