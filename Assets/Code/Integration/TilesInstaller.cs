using Assets.Code.Integration;
using Zenject;

public class TilesInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.Bind<TilesAggregate>().AsSingle();
        Container.Bind<NotifyPathAggregateWhenCellsAreOccupied>().AsSingle().NonLazy();
        Container.Bind<NotifyPathAggregateWhenEnemiesAreSpawned>().AsSingle().NonLazy();
    }
}