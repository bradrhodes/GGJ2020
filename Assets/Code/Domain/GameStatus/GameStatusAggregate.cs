using System;
using UniRx;

public class GameStatusAggregate
{
	private Subject<GameStatusEvent> _events = new Subject<GameStatusEvent>();

	public IObservable<GameStatusEvent> Events => _events;

	public void Lose()
	{
		Emit(new GameStatusEvent.GameOver());
	}

	public void Win()
	{
		Emit(new GameStatusEvent.GameWon());
	}

	private void Emit(GameStatusEvent @event)
		=> _events.OnNext(@event);
}

public class GameStatusEvent
{
	public class GameOver : GameStatusEvent
	{
	}

	public class GameWon : GameStatusEvent
	{
	}
}