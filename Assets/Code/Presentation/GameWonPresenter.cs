using UniRx;
using UnityEngine;
using Zenject;

public class GameWonPresenter : MonoBehaviour
{
	public Canvas GameWonCanvas;

	[Inject]
	public GameStatusAggregate GameStatus { private get; set; }

	private void Start()
	{
		GameWonCanvas.enabled = false;

		GameStatus.Events
			.OfType<GameStatusEvent, GameStatusEvent.GameWon>()
			.Subscribe(_ => ShowGameWon());
	}

	private void ShowGameWon()
	{
		GameWonCanvas.enabled = true;
	}
}
