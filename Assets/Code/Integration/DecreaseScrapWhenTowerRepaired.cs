using UniRx;

public class DecreaseScrapWhenTowerRepaired
{
	public DecreaseScrapWhenTowerRepaired(TowersAggregate towers, ScrapWalletAggregate scrapWallet, RepairCosts repairCosts)
	{
		towers.Events
			.OfType<TowersEvent, TowersEvent.TowerRepaired>()
			.Subscribe(_ => scrapWallet.Decrease(repairCosts.Tower));
	}
}
