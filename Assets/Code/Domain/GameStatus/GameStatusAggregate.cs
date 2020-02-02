using System;
using UniRx;

public class GameStatusAggregate
{
	private Subject<GameStatusEvent> _events = new Subject<GameStatusEvent>();
	private readonly GameStatusParameters _parameters;

	private int _currentLives;

	public IObservable<GameStatusEvent> Events => _events;

	public GameStatusAggregate(GameStatusParameters parameters)
	{
		_parameters = parameters;

		_currentLives = parameters.Lives;

		Observable.NextFrame()
			.Subscribe(_ => Emit(new GameStatusEvent.LivesInitialized(_currentLives)));
	}

	public void LoseLife()
	{
		_currentLives--;

		Emit(new GameStatusEvent.LifeLost(_currentLives));

		if (_currentLives == 0)
			Emit(new GameStatusEvent.GameOver());
	}

	private void Emit(GameStatusEvent @event)
		=> _events.OnNext(@event);
}

public class GameStatusParameters
{
	public GameStatusParameters(int lives)
	{
		Lives = lives;
	}

	public int Lives { get; }
}

public class GameStatusEvent
{
	public class GameOver : GameStatusEvent
	{
	}

	public class LivesInitialized : GameStatusEvent
	{
		public LivesInitialized(int lives)
		{
			Lives = lives;
		}

		public int Lives { get; }
	}

	public class LifeLost : GameStatusEvent
	{
		public LifeLost(int lives)
		{
			Lives = lives;
		}

		public int Lives { get; }
	}
}