using System;
using UniRx;

public class EnemiesAggregate
{
	private readonly Subject<TimeEvent> _timeEvents = new Subject<TimeEvent>();
	private Subject<EnemiesEvent> _events = new Subject<EnemiesEvent>();

	private readonly EnemiesAggregateParameters _parameters;

	private int _enemiesDestroyed;

	private float _enemyVelocity;

	public EnemiesAggregate(EnemiesAggregateParameters parameters)
	{
		_parameters = parameters;

		_enemyVelocity = parameters.InitialVelocity;

		_timeEvents
			.Sample(_parameters.SpawnRate)
			.Subscribe(_ => SpawnEnemy());

		_timeEvents
			.Sample(TimeSpan.FromSeconds(_parameters.VelocityIncrementRate))
			.Subscribe(_ => _enemyVelocity += _parameters.VelocityIncrement);
	}

	private void SpawnEnemy()
	{
		var enemyId = EnemyIdentifier.Create();

		Emit(new EnemiesEvent.EnemySpawned(enemyId, _enemyVelocity));
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
	public EnemiesAggregateParameters(TimeSpan spawnRate, float initialVelocity, float velocityIncrement, float velocityIncrementRate)
	{
        SpawnRate = spawnRate;
		InitialVelocity = initialVelocity;
		VelocityIncrement = velocityIncrement;
		VelocityIncrementRate = velocityIncrementRate;
	}

	public TimeSpan SpawnRate { get; }
	public float InitialVelocity { get; }
	public float VelocityIncrement { get; }
	public float VelocityIncrementRate { get; }
}

public abstract class EnemiesEvent
{
	public class EnemySpawned : EnemiesEvent
	{
		public EnemyIdentifier EnemyId { get; }
		public float Velocity { get; }

		public EnemySpawned(EnemyIdentifier enemyId, float velocity)
		{
			EnemyId = enemyId;
			Velocity = velocity;
		}
	}
}