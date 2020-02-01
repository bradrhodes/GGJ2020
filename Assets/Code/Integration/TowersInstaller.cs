using Zenject;

public class TowersInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.Bind<TowersAggregate>().AsSingle();
        Container.Bind<InitializeTowersWhenMapInitialized>().AsSingle().NonLazy();
    }
}

public class WallsInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.Bind<WallsAggregate>().AsSingle();
        Container.Bind<InitializeWallsWhenMapInitialized>().AsSingle().NonLazy();
    }
}
