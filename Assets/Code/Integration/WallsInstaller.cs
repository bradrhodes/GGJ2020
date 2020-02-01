using Zenject;

public class WallsInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.Bind<WallsAggregate>().AsSingle();
        Container.Bind<InitializeWallsWhenMapInitialized>().AsSingle().NonLazy();
        Container.Bind<RepairWallsWhenMapCellClicked>().AsSingle().NonLazy();
    }
}