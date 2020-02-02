using Zenject;

public class ScrapWalletInstaller : MonoInstaller
{
	public override void InstallBindings()
	{
		Container.Bind<ScrapWalletAggregate>().AsSingle().WithArguments(new ScrapWalletParameters(1000));

		Container.Bind<DecreaseScrapWhenTowerRepaired>().AsSingle().NonLazy();
		Container.Bind<DecreaseScrapWhenWallRepaired>().AsSingle().NonLazy();
		Container.Bind<UpdateTowersAndWallsAvailableScrap>().AsSingle().NonLazy();
		Container.Bind<IncreaseScrapWhenEnemyDestroyed>().AsSingle().WithArguments(50).NonLazy();

		Container.BindInstance(new RepairCosts(100, 50));
	}
}
