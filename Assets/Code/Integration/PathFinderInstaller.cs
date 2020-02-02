using Assets.Code.Integration;
using Zenject;

public class PathFinderInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.Bind<PathFinderAggregate>().AsSingle();
        Container.Bind<NotifyPathAggregateWhenCellsAreOccupied>().AsSingle().NonLazy();
        Container.Bind<NotifyPathAggregateWhenEnemiesAreSpawned>().AsSingle().NonLazy();
    }
}