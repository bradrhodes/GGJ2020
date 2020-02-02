using Zenject;

public class ScrapWalletInstaller : MonoInstaller
{
	public override void InstallBindings()
	{
		Container.Bind<ScrapWalletAggregate>().AsSingle().WithArguments(new ScrapWalletParameters(1000));

		Container.Bind<DecreaseScrapWhenTowerRepaired>().AsSingle().WithArguments(100).NonLazy();
		Container.Bind<DecreaseScrapWhenWallRepaired>().AsSingle().WithArguments(10).NonLazy();
	}
}
