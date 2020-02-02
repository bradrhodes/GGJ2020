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

		Observable.NextFrame().Subscribe(_ => Emit(new ScrapWalletEvent.Initialized(parameters.InitialAmount)));
	}

	public void Decrease(int amount)
	{
		_currentAmount -= amount;

		Emit(new ScrapWalletEvent.Decreased(amount, _currentAmount));
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
	public class Initialized : ScrapWalletEvent
	{
		public Initialized(int amount)
		{
			Amount = amount;
		}

		public int Amount { get; }
	}

	public class Decreased : ScrapWalletEvent
	{
		public Decreased(int decreaseAmount, int currentAmount)
		{
			DecreasedAmount = decreaseAmount;
			CurrentAmount = currentAmount;
		}

		public int DecreasedAmount { get; }
		public int CurrentAmount { get; }
	}
}