using System;
using UniRx;

public class ScrapWalletAggregate
{
	private readonly Subject<ScrapWalletEvent> _events = new Subject<ScrapWalletEvent>();

	public IObservable<ScrapWalletEvent> Events => _events;

	public ScrapWalletParameters Parameters { get; }

	public ScrapWalletAggregate(ScrapWalletParameters parameters)
	{
		Parameters = parameters;

		Observable.NextFrame().Subscribe(_ => Emit(new ScrapWalletEvent.Initialized(parameters.InitialScrap)));
	}

	private void Emit(ScrapWalletEvent @event)
		=> _events.OnNext(@event);
}

public class ScrapWalletParameters
{
	public ScrapWalletParameters(int initialScrap)
	{
		InitialScrap = initialScrap;
	}

	public int InitialScrap { get; }
}

public abstract class ScrapWalletEvent
{
	public class Initialized : ScrapWalletEvent
	{
		public Initialized(int amount)
		{
			Amount = amount;
		}

		public int Amount { get; }
	}
}