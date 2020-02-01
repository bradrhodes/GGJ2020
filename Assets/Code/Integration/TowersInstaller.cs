using Zenject;

public class TowersInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.Bind<TowersAggregate>().AsSingle();
        Container.Bind<InitializeTowersWhenMapInitialized>().AsSingle().NonLazy();
    }
}
