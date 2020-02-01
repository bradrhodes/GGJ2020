using System;
using UniRx;

public class EnemiesAggregate
{
	private readonly Subject<TimeEvent> _timeEvents = new Subject<TimeEvent>();
	private Subject<EnemiesEvent> _events = new Subject<EnemiesEvent>();

	private readonly EnemiesAggregateParameters _parameters;

	public EnemiesAggregate(EnemiesAggregateParameters parameters)
	{
		_parameters = parameters;

		_timeEvents
			.Sample(_parameters.SpawnRate)
			.Take(_parameters.EnemyCount)
			.Subscribe(_ => Emit(new EnemiesEvent.EnemySpawned(EnemyIdentifier.Create())));
	}

	public IObservable<EnemiesEvent> Events => _events;

	public IObserver<TimeEvent> TimeObserver => _timeEvents;

	private void Emit(EnemiesEvent @event)
		=> _events.OnNext(@event);
}

public class EnemiesAggregateParameters
{
	public EnemiesAggregateParameters(int enemyCount, TimeSpan spawnRate)
	{
		EnemyCount = enemyCount;
		SpawnRate = spawnRate;
	}

	public int EnemyCount { get; }
	public TimeSpan SpawnRate { get; }
}

public abstract class EnemiesEvent
{
	public class EnemySpawned : EnemiesEvent
	{
		public EnemyIdentifier EnemyId { get; }

		public EnemySpawned(EnemyIdentifier enemyId)
		{
			EnemyId = enemyId;
		}
	}
}