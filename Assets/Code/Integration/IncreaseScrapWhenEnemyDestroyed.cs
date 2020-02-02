using UniRx;

public class IncreaseScrapWhenEnemyDestroyed
{
	public IncreaseScrapWhenEnemyDestroyed(EnemiesAggregate enemies, ScrapWalletAggregate scrapWallet, int scrapAmount)
	{
		enemies.Events
			.OfType<EnemiesEvent, EnemiesEvent.EnemyDestroyed>()
			.Subscribe(_ => scrapWallet.Increase(scrapAmount));
	}
}
