using Assets.Code.Domain.Map;
using Zenject;

public class MapInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.Bind<MapAggregate>().AsSingle();
        Container.Bind<IMapGenerator>().To<MapGenerator>().AsTransient();
        Container.Bind<IPathGenerator>().To<FixedPathGenerator>().AsTransient();
    }
}