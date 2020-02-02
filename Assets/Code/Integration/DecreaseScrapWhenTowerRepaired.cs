using UniRx;

public class DecreaseScrapWhenTowerRepaired
{
	public DecreaseScrapWhenTowerRepaired(TowersAggregate towers, ScrapWalletAggregate scrapWallet, int repairCost)
	{
		towers.Events
			.OfType<TowersEvent, TowersEvent.TowerRepaired>()
			.Subscribe(_ => scrapWallet.Decrease(repairCost));
	}
}
