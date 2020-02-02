using UniRx;

namespace Assets.Code.Integration
{
	public class NotifyPathAggregateWhenEnemiesAreSpawned
	{
		public NotifyPathAggregateWhenEnemiesAreSpawned(EnemiesAggregate enemy, PathFinderAggregate pathFinder)
		{
			enemy.Events
				.OfType<EnemiesEvent, EnemiesEvent.EnemySpawned>()
				.DelayFrame(1)
				.Subscribe(spawned => HandleEnemiesSpawnedEvent(spawned, pathFinder));
		}

		private void HandleEnemiesSpawnedEvent(EnemiesEvent.EnemySpawned e, PathFinderAggregate pathFinder)
		{
			pathFinder.AddEnemy(e.EnemyId);
		}
	}
}