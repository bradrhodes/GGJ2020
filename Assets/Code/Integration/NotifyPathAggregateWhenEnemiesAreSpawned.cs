using UniRx;

namespace Assets.Code.Integration
{
    public class NotifyPathAggregateWhenEnemiesAreSpawned
    {
        public NotifyPathAggregateWhenEnemiesAreSpawned(EnemiesAggregate enemy, TilesAggregate tile)
        {
            enemy.Events.OfType<EnemiesEvent, EnemiesEvent.EnemySpawned>().Subscribe(spawned => HandleEnemiesSpawnedEvent(spawned, tile));
        }

        private void HandleEnemiesSpawnedEvent(EnemiesEvent.EnemySpawned e, TilesAggregate tiles)
        {
            tiles.AddEnemy(e.EnemyId);
        }
    }
}