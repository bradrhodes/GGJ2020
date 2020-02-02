using UniRx;

public class UpdateTowersAndWallsAvailableScrap
{
	public UpdateTowersAndWallsAvailableScrap(TowersAggregate towers, WallsAggregate walls, ScrapWalletAggregate scrapWallet)
	{
		scrapWallet.Events
			.OfType<ScrapWalletEvent, ScrapWalletEvent.AvailableScrapChanged>()
			.Subscribe(changed => 
			{
				towers.UpdateAvailableScrap(changed.CurrentAmount);
				walls.UpdateAvailableScrap(changed.CurrentAmount);
			});
	}
}