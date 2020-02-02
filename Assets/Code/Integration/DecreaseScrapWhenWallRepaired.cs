using UniRx;

public class DecreaseScrapWhenWallRepaired
{
	public DecreaseScrapWhenWallRepaired(WallsAggregate walls, ScrapWalletAggregate scrapWallet, RepairCosts repairCosts)
	{
		walls.Events
			.OfType<WallsEvent, WallsEvent.WallRepaired>()
			.Subscribe(_ => scrapWallet.Decrease(repairCosts.Wall));
	}
}
