using System;
using UniRx;

public class ScrapWalletAggregate
{
	private readonly Subject<ScrapWalletEvent> _events = new Subject<ScrapWalletEvent>();

	public IObservable<ScrapWalletEvent> Events => _events;

	public ScrapWalletParameters Parameters { get; }

	private int _currentAmount;

	public ScrapWalletAggregate(ScrapWalletParameters parameters)
	{
		Parameters = parameters;

		_currentAmount = parameters.InitialAmount;

		Observable.NextFrame().Subscribe(_ => Emit(new ScrapWalletEvent.AvailableScrapChanged(parameters.InitialAmount, parameters.InitialAmount)));
	}

	public void Decrease(int amount)
	{
		_currentAmount -= amount;

		Emit(new ScrapWalletEvent.AvailableScrapChanged(-amount, _currentAmount));
	}

	public void Increase(int amount)
	{
		_currentAmount += amount;

		Emit(new ScrapWalletEvent.AvailableScrapChanged(amount, _currentAmount));
	}

	private void Emit(ScrapWalletEvent @event)
		=> _events.OnNext(@event);
}

public class ScrapWalletParameters
{
	public ScrapWalletParameters(int initialScrap)
	{
		InitialAmount = initialScrap;
	}

	public int InitialAmount { get; }
}

public abstract class ScrapWalletEvent
{
	public class AvailableScrapChanged : ScrapWalletEvent
	{
		public AvailableScrapChanged(int changeAmount, int currentAmount)
		{
			ChangeAmount = changeAmount;
			CurrentAmount = currentAmount;
		}

		public int ChangeAmount { get; }
		public int CurrentAmount { get; }
	}
}