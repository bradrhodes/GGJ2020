using Assets.Code.Domain.Map;
using Zenject;

public class TestMapInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.Bind<MapAggregate>().AsSingle();
        Container.Bind<IMapGenerator>().To<TestMapGenerator>().AsTransient();
        Container.Bind<IPathGenerator>().To<FixedPathGenerator>().AsTransient();
    }
}