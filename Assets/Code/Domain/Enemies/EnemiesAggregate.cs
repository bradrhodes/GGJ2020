using System;
using UniRx;

public class EnemiesAggregate
{
	private readonly Subject<TimeEvent> _timeEvents = new Subject<TimeEvent>();
	private Subject<EnemiesEvent> _events = new Subject<EnemiesEvent>();

	private readonly EnemiesAggregateParameters _parameters;

	private int _enemiesDestroyed;

	public EnemiesAggregate(EnemiesAggregateParameters parameters)
	{
		_parameters = parameters;

		_timeEvents
			.Sample(_parameters.SpawnRate)
			.Subscribe(_ => SpawnEnemy());
	}

	private void SpawnEnemy()
	{
		var enemyId = EnemyIdentifier.Create();

		Emit(new EnemiesEvent.EnemySpawned(enemyId));
	}

	public IObservable<EnemiesEvent> Events => _events;

	public IObserver<TimeEvent> TimeObserver => _timeEvents;

	public void Destroy(EnemyIdentifier enemyId)
	{
		_enemiesDestroyed++;
	}

	private void Emit(EnemiesEvent @event)
		=> _events.OnNext(@event);
}

public class EnemiesAggregateParameters
{
	public EnemiesAggregateParameters(TimeSpan spawnRate)
	{
        SpawnRate = spawnRate;
	}

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