using UniRx;

public class DecreaseScrapWhenWallRepaired
{
	public DecreaseScrapWhenWallRepaired(WallsAggregate walls, ScrapWalletAggregate scrapWallet, int repairCost)
	{
		walls.Events
			.OfType<WallsEvent, WallsEvent.WallRepaired>()
			.Subscribe(_ => scrapWallet.Decrease(repairCost));
	}
}
